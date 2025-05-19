# 📦 Sales and Inventory System - Developer Guide

## Database Setup

### Why We Don't Use `.mdf` Files with Git

This project uses a local SQL Server `.mdf` file for development, but `.mdf` files aren't suitable for version control because:

* They're **binary** files that Git can't track or merge properly
* They're often **locked** by SQL Server or Visual Studio
* Git may show errors like `Permission Denied` or `unable to process path`

### 🚀 Setting Up the Local Database

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

## 🔗 Database Connection

### Best Practice: Using Web.config

Add this to your `Web.config` file:

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

Then access it in your code:

```csharp
using System.Configuration;

private readonly string connStr = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
```

### Database Best Practices

* Use `|DataDirectory|` placeholder instead of hardcoded paths
* Avoid special characters in file names (like `&`)
* Use `Integrated Security=True` for local development
* Keep the `.mdf` file in the `App_Data` folder

## 📂 File Directory Structure

### Master Page Reference System

The project uses a structured folder system with master pages in different locations.

```
Root/
├── Admin/
│   ├── AdminMaster.master
│   └── Dashboard.aspx
└── User/
    ├── MainMasterpage.Master   ← Primary master page
    ├── Registration.aspx
    └── Login.aspx
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

## 🔄 Keeping the Database in Sync

When you make database changes:

* Add a new table
* Modify columns 
* Insert default data
* Add a stored procedure

➡️ Please update or add a new `.sql` file in the `DatabaseScripts` folder so everyone stays in sync.

Do not push `.mdf` and `.ldf` files to Git (they should be **excluded in `.gitignore`**).

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

## 📌 Example SqlConnection Usage

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
