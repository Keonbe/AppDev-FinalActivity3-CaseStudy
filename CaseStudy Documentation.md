# Sales and Inventory System (FinalActivity3)
A **Sales and Inventory Management System** built using **ASP.NET Web Forms**, **ADO.NET**, and **MSSQL LocalDB**, structured for academic purposes. The system supports Admin and User roles with core features such as product management, member tracking, and transaction monitoring.

---

## Table of Contents
* [Overview](#overview)
* [Tech Stack](#tech-stack)
* [Project Structure](#project-structure)
* [Setup Instructions](#setup-instructions)
  * [1. Clone the Repository](#1-clone-the-repository)
  * [2. Clean Up App\_Data](#2-clean-up-app_data)
  * [3. Set Up Local Database](#3-set-up-local-database)
  * [4. Configure Connection String](#4-configure-connection-string)
  * [5. Build and Run](#5-build-and-run)
* [Usage](#usage)
* [Development Notes](#development-notes)
* [Database Setup](#database-setup)
* [File Directory Structure](#file-directory-structure)
* [Common Issues and Solutions](#common-issues-and-solutions)
* [Quick Fixes for Fresh Clones](#quick-fixes-for-fresh-clones)
* [Database Connection Best Practices](#database-connection-best-practices)
* [Contributing](#contributing)
* [Credits](#credits)

---

## Overview
This system is designed to:
* Manage products, members, and transactions
* Separate user views for Admin and Customers
* Enable database filtering, sorting, and secure login sessions

---

## Tech Stack
* **Frontend:** ASP.NET Web Forms (ASPX, C#)
* **Backend:** ADO.NET with SQL Server (LocalDB)
* **Database:** `.mdf` file in `App_Data`
* **Tools:** Visual Studio, SQL Server Management Studio (SSMS)

---

## Project Structure
```
/FinalActivity3
├── /Admin                ← Admin dashboard pages
├── /User                 ← Customer pages
├── /App_Data             ← Local .mdf database file
├── /assets/css           ← Stylesheets
├── /ClassLibrary         ← All business logic (use this for backend logic)
├── /DatabaseScripts      ← SQL scripts for setting up the DB
├── Web.config            ← Web application configuration
├── README.md             ← Project documentation
```

---

## Setup Instructions

### 1. Clone the Repository
1. In Visual Studio: `Git → Clone Repository`
2. Paste: `https://github.com/Keonbe/AppDev-FinalActivity3-CaseStudy/`
3. Choose a local folder → Click Clone

### 2. Clean Up App\_Data
* Delete existing `App_Data` if present
* Create a new one: `Right-click → Add → New Folder → App_Data`

### 3. Set Up Local Database
**Option A: Create New Database**
* Open SSMS or Visual Studio SQL Object Explorer
* Create a DB named `SalesInventoryDB`
* Run scripts in this order from `/DatabaseScripts`:
  1. `01_CreateTables.sql`
  2. `02_InsertSeedData.sql`
  3. `03_StoredProcedures.sql`
* The `.mdf` will appear in your `App_Data` automatically

**Option B: Attach Existing Database**
* Add new `.mdf`: `Right-click App_Data → Add → SQL Server Database`
* In SSMS: `Right-click Databases → Attach → Browse to .mdf`

### 4. Configure Connection String
In `Web.config`, make sure this connection string exists:
```xml
<connectionStrings>
  <add name="DBMS" connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SalesInventoryDB.mdf;Integrated Security=True" providerName="System.Data.SqlClient" />
</connectionStrings>
```
**Important:** Use `|DataDirectory|` to avoid absolute paths.

### 5. Build and Run
* `Build → Clean Solution`
* `Build → Rebuild Solution`
* `Debug → Start Debugging (F5)`

---

## Usage
* Login as Admin or Customer
* Admin can:
  * View Products, Members, and Transactions via GridView
  * Use RadioButtonList to switch between views
  * Sort Transactions by Date ascending/descending (ddlSortDir shown only when Transactions selected)
* Database logic (inserts, calculations, discounts, etc.) handled in `ClassLibrary`

---

## Development Notes
* **Admin View** dynamically loads stored procedures using the `rblViewSelector`
* **Connection String** moved from class files to `Web.config` for better management
* **Filtering and Sorting** uses stored procedures + GridView binding
* **Security:** Basic session checking using `Session["AdminEmailAddress"]`

---

## Database Setup

### Why We Don't Version Control `.mdf` Files
This project uses a local SQL Server `.mdf` file for development, but these files aren't suitable for version control because:
* They are **binary** files, so Git can't track changes effectively
* They're often **locked** by SQL Server or Visual Studio
* Git may show errors like `Permission Denied` or `unable to process path`

### Setting Up the Database Locally
1. Open **SQL Server Management Studio** or Visual Studio's **Server Explorer**
2. Create a new local database (e.g., `SalesInventoryDB`)
3. Execute the SQL files from `/DatabaseScripts` in this order:
   * `01_CreateTables.sql`
   * `02_InsertSeedData.sql`
   * `03_StoredProcedures.sql`

### Connection String Configuration
This project uses **Web.config** for database connection management, which is the recommended approach for ASP.NET applications:
1. **Check your Web.config file** for the correct connection string:

```xml
<connectionStrings>
  <add name="DBMS" 
       connectionString="Data Source=(LocalDB)\MSSQLLocalDB;
                         AttachDbFilename=|DataDirectory|\SalesInventoryDB.mdf;
                         Integrated Security=True;
                         Connect Timeout=30;" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

2. **Using the connection string in code**:

```csharp
using System.Configuration;
// ...
string connStr = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
```

3. **Key advantages**:
   * No hardcoded paths - the `|DataDirectory|` token automatically resolves to your `App_Data` folder
   * Portable across different developer machines
   * More secure than embedding connection strings in code

### Keeping the Database in Sync
When you make database changes:
* Add a new table
* Modify columns
* Insert default data
* Add a stored procedure

Please update or create a new `.sql` file in the `/DatabaseScripts` folder to keep everyone in sync.

> **Note:** `.mdf` and `.ldf` files are excluded in `.gitignore`.

---

## File Directory Structure

### Master Page Reference System

The project uses a structured folder system with `MainMasterpage.Master` located in the **User** folder.

#### Correct Path Formatting
```aspnet
<!-- For files in SAME directory -->
MasterPageFile="MainMasterpage.Master"
<!-- For files in SUBDIRECTORY -->
MasterPageFile="~/FolderName/Masterpage.Master"
<!-- For files in PARENT directory -->
MasterPageFile="../Masterpage.Master"
```

#### Key Directory Structure
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

### Critical Rules for File References

1. **Always use `~` for root-relative paths** when crossing folder boundaries
   ```aspnet
   ❌ "MainMasterpage.Master" 
   ✅ "~/User/MainMasterpage.Master"
   ```

2. **Case sensitivity matters** - Match exact filenames:
   ```aspnet
   ❌ "mainmasterpage.master"
   ✅ "MainMasterpage.Master"
   ```

3. **File Properties Checklist**:
   - Build Action = Content
   - Copy to Output Directory = Copy if newer

### Troubleshooting Missing Files

If you encounter "does not exist" errors:
1. Verify physical file exists in Solution Explorer
2. Check for typos in:
   - File extensions (`.master` vs `.Master`)
   - Path separators (`/` not `\`)
   - CodeBehind declarations
3. Clean and rebuild solution

#### Example Fixes

**Before (Broken):**
```aspnet
<%@ Page MasterPageFile="MainMasterpage.Master" %>
```

**After (Fixed):**
```aspnet
<%@ Page MasterPageFile="~/User/MainMasterpage.Master" %>
```

**Before (Typo):**
```aspnet
CodeBehind="Regisration.aspx.cs"
```

**After (Corrected):**
```aspnet
CodeBehind="Registration.aspx.cs"
```

> **Pro Tip:** Use Solution Explorer's "Copy Path" feature to ensure correct references!

---

## Common Issues and Solutions

| Issue | Cause & Fix |
|-------|-------------|
| **"Conversion failed when converting the nvarchar value 'MNTR-IPS' to data type int"** | Likely a mismatch in your database schema — e.g., you're passing a string like `ProductID='MNTR-IPS'` into a stored procedure expecting an `INT`. Check the `ProductID` type in both your tables and your SPs. Either: <br>• Change the column type to `NVARCHAR` in your tables/SPs <br>• Or make sure you pass actual integers if the schema is correct. |
| **Stored Procedure runs, but returns nothing** | 1. Check if the `@TransactionID` or other parameters match actual data in the DB. <br>2. Verify that your joins are not filtering out data (e.g., inner join on `ProductID` that doesn't match). Use `LEFT JOIN` for debugging. |
| **GridView is blank** | Common causes: <br>• You're binding before `DataTable` is filled. <br>• Your SQL query returned zero rows. <br>• You forgot to set `DataSource` or call `.DataBind()`. <br>• Check your ASPX GridView: If using `<Columns>`, set `AutoGenerateColumns="False"` explicitly. |
| **Error: Object reference not set to an instance of an object** | Usually occurs when you try to access a control, data row, or object that wasn't initialized. Add null checks: <br>`if (myObject != null) { /* use it */ }` |
| **ASP.NET page links broken (404/403 errors)** | • Check your `MasterPage.master` or other hyperlinks: always use `~/` (root-based paths) for ASP.NET, e.g.: <br>`<asp:HyperLink NavigateUrl="~/Admin/ProductPage.aspx">` <br>• Make sure the `.aspx` page exists at the specified path. |
| **Page opens blank with no error** | 1. Check if Page_Load has `if (!IsPostBack)` around critical logic. <br>2. Verify no exception is being silently caught. <br>3. Try setting a breakpoint in Page_Load to step through. |
| **Stored Procedure not found / fails at runtime** | 1. You forgot to run `03_StoredProcedures.sql` in SSMS. <br>2. You're calling `SC_ProcessCheckout` but the actual name is different or not under `dbo`. Confirm full name. <br>3. Check for typos and case sensitivity. |
| **.mdf file won't attach / "cannot be opened because it is version XXX"** | • Delete and recreate the `.mdf` using your SQL Server version. <br>• Or create the DB manually in SSMS and skip the `.mdf` entirely (just use the server-based DB). |
| **Login failed for user 'IIS APPPOOL\DefaultAppPool'** | • Use `Integrated Security=True` to avoid hardcoding usernames/passwords. <br>• Or configure SQL Server to allow access to that user. |
| **ASP.NET designer.cs file not updating / code-behind error** | 1. Close and reopen the `.aspx` file. <br>2. Delete `bin/` and `obj/` folders. <br>3. Rebuild solution. <br>4. Make sure `Inherits="YourNamespace.YourPage"` matches the code-behind class. |
| **Default homepage won't show** | Set `LandingPage.aspx` (or whatever default) as **Start Page**: right-click the file in Solution Explorer → Set as Start Page. |
| **Changes to table or SP aren't reflected in app** | You may be editing an `.sql` file but not re-running it in SSMS. Every time you change schema/data logic, **re-execute the script manually in SSMS**. |
| **Project won't build after clean** | If you get missing reference errors: <br>• Right-click the solution → Rebuild Solution. <br>• If that fails, check that all projects are properly referenced (Class Library → WebForms). |
| **Error: "Keyword not supported: 'attachdbfilename'"** | You're likely trying to use `.mdf` with a non-LocalDB provider. Make sure your connection string uses `LocalDB`, not `SQLEXPRESS` or an actual SQL Server instance. |
| **Session errors or null session data** | If you're trying to use session variables (`Session["X"]`) too early (e.g., before login or on the wrong page), they may be null. Add null checks and consider default redirects. |
| **CSS/JS not loading** | 1. Use relative paths properly in `<link>` and `<script>` tags: <br>• Prefer `~/Content/style.css` via `<asp:Content>` instead of plain HTML pathing. <br>2. ASP.NET treats folders like `/Scripts`, `/Content`, and `/App_Data` differently—ensure your static files are not in restricted folders. |
| **`.mdf` not attaching** | Rename file or reattach in SSMS |
| **Login session not detected** | Check if cookies or session expired |
| **No transactions shown** | Make sure SP `GetAllTransactions` runs and DB is seeded |
| **Sort not working** | Ensure `ddlSortDir` is visible only when Transaction view is selected |

---

## Quick Fixes for Fresh Clones

When you first clone the repo, you might hit one of these errors. Try these in order:

### 1. Roslyn Compiler Missing (`roslyn\csc.exe`)

**Symptom:**  
```
Could not find a part of the path '…\bin\roslyn\csc.exe'
```

**Fix:**  
1. In Visual Studio, right-click your WebForms project → **Manage NuGet Packages…**  
2. Install or update **Microsoft.CodeDom.Providers.DotNetCompilerPlatform**  
3. Rebuild—Visual Studio will recreate `bin/roslyn/csc.exe`

### 2. Native SQL Server Types Missing

**Symptom:** 
```
Could not copy '…\SqlServerTypes\x64\SqlServerSpatial140.dll' because it was not found.
```

**Fix:**  
1. In **Manage NuGet Packages**, install/update **Microsoft.SqlServer.Types**  
2. In Solution Explorer, expand the package to **build\native\x64** and **x86**, right-click each folder → **Include In Project**  
3. For each DLL, set **Copy to Output Directory = Always**  
4. Rebuild so those native DLLs are deployed

### 3. 403.14 Forbidden — No Default Document

**Symptom:**  
Visiting `http://localhost:xxxxx/` shows directory listing forbidden.

**Fix A (Quick):**  
- Right-click `LandingPage.aspx` (or your landing page) → **Set as Start Page** → F5.

**Fix B (Persistent):**  
Add to `Web.config` under `<system.webServer>`:
```xml
<defaultDocument>
  <files>
    <clear />
    <add value="LandingPage.aspx" />
  </files>
</defaultDocument>
```
Replace `LandingPage.aspx` with the correct root page.

---

## Database Connection Best Practices

This project uses a local SQL Server `.mdf` database file located in the `App_Data` folder. The recommended way to manage database connections is via the `Web.config` file using `<connectionStrings>`.

### Recommended Setup: Web.config + ConfigurationManager

#### 1. `Web.config` Configuration

Make sure you have the following inside your `<configuration>` section:
```xml
<connectionStrings>
  <add name="DBMS" 
       connectionString="Data Source=(LocalDB)\MSSQLLocalDB;
                         AttachDbFilename=|DataDirectory|\SalesInventoryDB.mdf;
                         Integrated Security=True;
                         Connect Timeout=30;" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

* `|DataDirectory|` resolves automatically to the `App_Data` folder at runtime
* No need to hardcode paths — works across machines and directories

#### 2. ASP.NET (Code-Behind) Usage

In your `*.aspx.cs` or `*.cs` class files, use the connection string via `ConfigurationManager`:
```csharp
using System.Configuration;
public partial class OrderHistory : System.Web.UI.Page
{
    private readonly string connStr = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
    // Use connStr for SqlConnection
}
```

### Common Mistakes and Fixes

#### ❌ Hardcoding Full Path in Connection String

Avoid this:
```csharp
private string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;
    AttachDbFilename=C:\Users\admin\Documents\...Sales&InvSystemDB.mdf;
    Integrated Security=True;";
```

**Why it's bad:**
* It breaks if moved to another PC
* It fails if the path includes special characters like `&`
* It causes errors like:
  * `Cannot open database requested by the login`
  * `A database with the same name exists, or specified file cannot be opened`

**Fix:** Use `|DataDirectory|` and keep the `.mdf` inside `App_Data`.

#### ❌ Special Characters in File Name

Avoid using characters like `&` in `.mdf` file names:
```
Sales&InvSystemDB.mdf ❌
```

Rename it to:
```
SalesInvSystemDB.mdf ✅
```

Then update the filename in both:
* `App_Data` folder
* `Web.config` connection string

### Troubleshooting

#### Error: "Login failed for user 'LAPTOP\admin'"
* This happens when the database requires a login, or if the `.mdf` is not attached properly
* Use `Integrated Security=True` and avoid `Initial Catalog` unless connecting to an installed DB instance

#### Error: "A database with the same name exists"
* This usually happens when:
  * The `.mdf` is already attached in SQL Server
  * The filename is reused across different projects

**Fix:**
* Close all running Visual Studio instances
* Open **SQL Server Object Explorer**, and **detach** any database using the same file
* Ensure only one project uses that `.mdf` at a time

### Summary

| Practice | Recommendation |
|----------|----------------|
| Connection String Location | Use `Web.config` |
| File Attach Path | Use `\|DataDirectory\|` |
| File Names | Avoid `&`, spaces, and special characters |
| SQL Login | Use `Integrated Security=True` |
| Portability | Never hardcode full file paths |

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

---

## Contributing
1. Fork this repo
2. Create a branch (`git checkout -b feature/yourFeature`)
3. Commit with clear messages (`git commit -m "Add member filter"`)
4. Push to your fork and open a Pull Request

**Contribution Rules:**
* Don't commit `.mdf` changes
* Add new SPs to `/DatabaseScripts`
* Write clear `<summary>` in public methods
* Only bind stored procedures via `LoadGrid()`

---

## Credits
Developed by: *X Group* — DLSU-D Information Technology (2025)
> For questions or support, please open an Issue.
