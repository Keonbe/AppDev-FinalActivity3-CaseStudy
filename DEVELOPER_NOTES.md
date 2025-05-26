# 🔍 Extended Developer Notes

<div align="right">
<strong>Last Updated:</strong> 2025-05-26 10:47:11 PHT<br>
<strong>Maintainer:</strong> Keonbe
</div>

## 📋 Table of Contents
- [Database Architecture](#-database-architecture)
- [Master Page System](#-master-page-system)
- [Data Access Layer](#-data-access-layer)
- [Database Setup Guide](#-database-setup-guide)
- [Testing Protocol](#-testing-protocol)
- [Commenting Standards](#-commenting-standards)
- [Deployment Considerations](#-deployment-considerations)
- [Troubleshooting Common Issues](#-troubleshooting-common-issues)
- [Collaboration Compatibility Guide](#-collaboration-compatibility-guide)
- [Contribution Workflow](#-contribution-workflow)

## 📊 Database Architecture

### Tables and Relationships

```
UserInfoTable
├── UserId (PK, IDENTITY)
├── Name
├── EmailAddress (Clustered PK)
├── Password
├── MembershipType
└── IsAdmin (BIT)

ProductInventoryTable
├── Id (IDENTITY(1,1) after initial setup)
├── ProductID (PK, NCHAR(10))
├── ProductName (NVARCHAR(150))
├── Price (DECIMAL(18))
└── Stocks (INT)

TransactionsTable
├── TransactionID (PK, IDENTITY)
├── UserID (FK -> UserInfoTable)
├── DateTime
├── TotalAmount (DECIMAL(18))
└── MembershipType (NCHAR(10))

TransactionDetails
├── DetailID (PK, IDENTITY)
├── TransactionID (FK -> TransactionsTable)
├── ProductID (FK -> ProductInventoryTable)
├── Quantity (INT)
├── Unit Price (DECIMAL(18,2))
├── Discount (DECIMAL(18,2))
├── TotalAmount (DECIMAL(18,2))
├── CreatedAt (DATETIME, DEFAULT getdate())
└── UpdatedAt (DATETIME, DEFAULT getdate())

AddCartTable
├── CartId (PK, IDENTITY)
├── UserID (INT)
├── ProductID (NCHAR(10))
└── Quantity (INT)
```

### Key Stored Procedures

- `SaveUserRegisration` - Saves user registration data to the database
- `LoginAccountCheck` - Validates login credentials for regular users
- `AdminLoginAccountCheck` - Validates login credentials for admin users
- `UpdateUserPassword` - Allows users to update their password
- `GetAllMembers` - Retrieves all user records for admin view
- `GetAllProducts` - Retrieves all products for admin view
- `GetAllTransactions` - Retrieves all transactions admin view with optional sorting
- `GetOrderHistoryUser` - Gets transaction history for a specific user
- `GetTransactionDetails` - Gets detailed information for a specific transaction
- `AddNewProducts` - Allows admins to add new products to inventory
- `AddProductToCart` - Adds a product to the user's shopping cart
- `SC_RemoveCartItem` - Removes an item from the shopping cart
- `SC_GetCartItems` - Retrieves all items in a user's shopping cart
- `SC_ProcessCheckout` - Handles the complete checkout process with transaction recording
- `rdlc_GetSalesSummaryByProduct` - Creates product sales summary reports

## 🎨 Master Page System

### Structure

The project uses ASP.NET Master Pages to maintain consistent layout:
- `LandingPage.Master` - Template for the landing page
- `AdminMasterpage.Master` - Template specific to admin pages
- `MainMasterpage.Master` - Template specific to user pages

### Directory Structure

```
Root/
├── Admin/
│   ├── AdminMaster.master   ← Admin master page
│   └── Dashboard.aspx
├── User/
│   ├── AdminMaster.master
│   ├── MainMasterpage.Master   ← Primary master page
│   ├── ProductCatalog.aspx
│   └── ShoppingCart.aspx
└── HomePage/
     ├── LandingPage.Master   ← Landing master page
     ├── newRegistration.aspx
     └── newLogin.aspx
```

### Path Resolution

Content pages reference master pages using the `MasterPageFile` attribute:

```html
<%@ Page Title="Page Title" Language="C#" MasterPageFile="~/User/MainMasterpage.Master" 
   AutoEventWireup="true" CodeBehind="Page.aspx.cs" Inherits="Namespace.Page" %>
```

### Path Reference Rules

1. **Always use `~` for root-relative paths** when crossing folder boundaries
   ```aspnet
   ❌ "MainMasterpage.Master" 
   ✅ "~/User/MainMasterpage.Master"
   ```

2. **Case sensitivity matters**
   ```aspnet
   ❌ "mainmasterpage.master"
   ✅ "MainMasterpage.Master"
   ```

3. **Set correct file properties**:
   - Build Action = Content
   - Copy to Output Directory = Copy if newer

### Common Issues and Fixes

- **"Master page not found"**:
  - Ensure the path includes `~/` for site root
  - Check for correct casing in filenames (important on Linux/macOS hosting)
  
- **"Content controls not found"**:
  - Verify that ContentPlaceHolder IDs match between master and content pages
  - Check for proper opening/closing of Content tags

## 💾 Data Access Layer

### Connection Management

The project follows a structured approach for database access:
- `ClassMethods.cs` - User methods like login and basic CRUD operations
- `AdminMethods.cs` - Admin-specific methods like adding products and generating reports
- `ClassComputations.cs` - Contains all calculation logic for pricing, discounts, etc.

### Database Connection Best Practice

Use Web.config connection strings:

```xml
<connectionStrings>
  <add name="DBMS" 
       connectionString="Data Source=(LocalDB)\MSSQLLocalDB;
                         AttachDbFilename=|DataDirectory|\SalesInvSystemDB.mdf;
                         Integrated Security=True;
                         Connect Timeout=30;" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

Access in code:

```csharp
using System.Configuration;

private readonly string connStr = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
```

### Example SqlConnection Usage

```csharp
using (SqlConnection conn = new SqlConnection(connStr))
{
    conn.Open();
    SqlCommand cmd = new SqlCommand("SELECT * FROM Sales", conn);
    SqlDataReader reader = cmd.ExecuteReader();

    while (reader.Read())
    {
        // handle data
    }
}
```

### Best Practices

- Use parameterized queries to prevent SQL injection
- Implement proper disposal of database connections (using statements)
- Use stored procedures for complex operations
- Keep business logic separate from data access code
- Use `|DataDirectory|` placeholder instead of hardcoded paths
- Avoid special characters in file names (like `&`)
- Use `Integrated Security=True` for local development
- Keep the `.mdf` file in the `App_Data` folder

## 🚀 Database Setup Guide

### Why We Don't Use `.mdf` Files with Git

This project uses a local SQL Server `.mdf` file for development, but `.mdf` files aren't suitable for version control because:

* They're **binary** files that Git can't track or merge properly
* They're often **locked** by SQL Server or Visual Studio
* Git may show errors like `Permission Denied` or `unable to process path`

### Database Setup Options

#### Option 1: Using the OneDrive .mdf File (Recommended)

1. **Clone the Project**
   ```bash
   git clone https://github.com/Keonbe/AppDev-FinalActivity3-CaseStudy.git
   git pull origin main
   ```

2. **Remove Existing `App_Data`**
   * In Solution Explorer, navigate to `App_Data` folder
   * Delete any existing `.mdf` and `.ldf` files

3. **Download the Working `.mdf` File**
   * Download from [OneDrive link](https://dlsudphl-my.sharepoint.com/my?id=%2Fpersonal%2Fbkm0866%5Fdlsud%5Fedu%5Fph%2FDocuments%2FPROG%5FPROGRAM%2DFILES%2FBIT23%2F2ndSem%2FAppDev%2FSample%20Code%2Fshared%2FCaseStudy%5BNEW%20UI%5D%2FFinalActivity3%2FFinalActivity3%5FCaseStudy%2FFinalActivity3%5FCaseStudy%2FApp%5FData&login_hint=BKM0866%40dlsud%2Eedu%2Eph)

4. **Add `.mdf` File to Project**
   * In Visual Studio, right-click on `App_Data` → **Add** → **Existing Item...**
   * Select the downloaded `.mdf` file

5. **Rebuild Solution**
   * **Build** → **Clean Solution**
   * **Build** → **Rebuild Solution**

6. **Run the Project**
   * Hit `F5` or click **Start Debugging**

#### Option 2: Using Database Scripts

If you prefer not to use the shared `.mdf` file, you can set up the database from scratch:

1. Open **SQL Server Management Studio**
2. Create a new local database (e.g., `SalesInvSystemDB`)
3. Execute the SQL scripts in `/DatabaseScripts` folder in this order:
   * `01_CreateTables.sql`
   * `02_InsertSeedData.sql`
   * `03_StoredProcedures.sql` (if applicable)

## 🧪 Testing Protocol

### Manual Testing Checklist

1. **User Authentication**
   - [ ] Registration with valid/invalid data
   - [ ] Login with correct/incorrect credentials
   - [ ] Password change functionality
   - [ ] Admin login verification

2. **Product Management**
   - [ ] Adding new products (admin)
   - [ ] Viewing product details
   - [ ] Stock updates after purchases

3. **Shopping Process**
   - [ ] Adding items to cart
   - [ ] Updating quantities
   - [ ] Removing items
   - [ ] Checkout process
   - [ ] Discount application based on membership type

4. **Reports and History**
   - [ ] Transaction history display
   - [ ] Sales report generation
   - [ ] Filtering and sorting

### Common Test Cases

| Test Case | Expected Result |
|-----------|-----------------|
| Login with valid credentials | Redirects to appropriate dashboard |
| Add product with zero/negative price | Shows validation error |
| Purchase exceeding available stock | Shows insufficient stock message |
| Apply discount to eligible order | Correctly calculates final amount |
| Process checkout with empty cart | Shows appropriate error message |
| Generate sales report | Displays summary by product |

## 📝 Commenting Standards

### Code Documentation

Use XML comments for public methods:

```csharp
/// <summary>
/// Processes the checkout for the current user's cart
/// </summary>
/// <param name="userId">The ID of the user making the purchase</param>
/// <param name="cartItems">Collection of items in the cart</param>
/// <returns>Transaction ID if successful, -1 if failed</returns>
public int ProcessCheckout(int userId, List<CartItem> cartItems)
{
    // Implementation...
}
```

### Inline Comments

Use inline comments for complex logic:

```csharp
// Calculate final price with appropriate discount
// Silver members: 5%, Gold: 10%, Platinum: 15%
decimal discountRate = 0;
switch (membershipType)
{
    case "Silver":
        discountRate = 0.05m;
        break;
    // Other cases...
}
```

## 🔄 Deployment Considerations

### Pre-Deployment Checklist

1. Update connection strings for production environment
2. Remove any test/debug code
3. Ensure error handling is production-ready
4. Update any hardcoded paths or URLs
5. Test all features in a staging environment
6. Verify that all stored procedures are properly created in the database

### Web.config Transformations

Use Web.config transformations for environment-specific settings:

```xml
<!-- Web.Release.config -->
<connectionStrings>
  <add name="DBMS" 
       connectionString="Production_Connection_String_Here"
       xdt:Transform="SetAttributes" xdt:Locator="Match(name)"/>
</connectionStrings>
```

## 🚨 Troubleshooting Common Issues

### Database Issues

| Issue | Solution |
|-------|----------|
| **"Conversion failed when converting nvarchar to int"** | Check data types in stored procedures and tables |
| **Stored Procedure returns nothing** | Verify parameters match actual data and check joins |
| **"Cannot open database .mdf"** | Ensure file is properly attached via **Add → Existing Item** |
| **Login failed for user** | Verify `Integrated Security=True` in connection string |
| **Special characters in DB path** | Rename files to avoid `&` and other special characters |

### ASP.NET Issues

| Issue | Solution |
|-------|----------|
| **GridView is blank** | Check `DataSource`, `DataBind()` and timing of data binding |
| **Object reference not set** | Add null checks before accessing controls or data |
| **ASP.NET links giving 404/403** | Use root-relative paths (`~/`) for navigation |
| **Page opens blank with no error** | Check for silently caught exceptions and `IsPostBack` logic |
| **CSS/JS not loading** | Use proper relative paths with `~/Content/` format |

### First-Run "Free Fixes"

#### 1. Roslyn Compiler Missing
```
Could not find a part of the path '…\bin\roslyn\csc.exe'
```
Install/update **Microsoft.CodeDom.Providers.DotNetCompilerPlatform** via NuGet

#### 2. Native SQL Server Types Missing
```
Could not copy '…\SqlServerTypes\x64\SqlServerSpatial140.dll'
```
Install/update **Microsoft.SqlServer.Types** via NuGet

#### 3. 403.14 Forbidden — No Default Document
Right-click `Homepage.aspx` → **Set as Start Page** → F5

## 🔄 Collaboration Compatibility Guide

### Compatibility Issues

#### 1️⃣ **Codebase Issues**
- **RDLC Reports Not Rendering**  
  Missing files, dataset name mismatches, or missing `Microsoft.ReportViewer.WebForms`
- **.csproj Conflicts**  
  Version mismatches or path issues causing merge conflicts
- **NuGet Inconsistencies**  
  Package version mismatches or missing restores

#### 2️⃣ **Environment Issues**
- **SQL Server Differences**  
  Missing databases or stored procedures
- **Report Viewer Not Installed**  
  Requires NuGet package:  
  ```bash
  Install-Package Microsoft.Reporting.WebForms
  ```
- **IIS/.NET Version Conflicts**  
  Ensure all use same target framework (check `.csproj`)

#### 3️⃣ **Tooling Issues**
- **Visual Studio Version Conflicts**  
  Solution file compatibility between VS2017/2019/2022
- **Git Line Ending Conflicts**  
  Set `.gitattributes` for consistent line endings

#### 4️⃣ **Workflow Issues**
- **Merge Conflicts**  
  Always pull before pushing changes
- **Untracked Files**  
  Regularly check `git status` for missing files
- **Missing Documentation**  
  Keep `README.md` updated with setup instructions

### ✅ Compatibility Checklist

| Requirement               | Status  | Notes                          |
|---------------------------|---------|--------------------------------|
| Proper `.gitignore`       | ✔️      | Excludes binaries/user files   |
| Updated `README.md`       | ✔️      | Contains setup instructions    |
| NuGet packages restored   | ✔️      | Via `packages.config`          |
| Consistent .NET version   | ✔️ 4.8  | Verified in `.csproj`          |
| SQL scripts available     | ✔️      | In `DatabaseScripts/` folder   |
| ReportViewer installed    | ✔️      | Via NuGet package              |

## 🤝 Contribution Workflow

1. **Fork** the repository
2. Create a **feature branch** (`git checkout -b feature/your-feature-name`)
3. Make your changes
4. Add database script updates if needed
5. Create a **pull request** with detailed description
6. Wait for code review and address feedback

### Database Change Protocol

When making database changes:
1. Create a new script in `/DatabaseScripts` with ascending number
2. Document changes in script comments
3. Test script against a clean database
4. Update this documentation with any structural changes
