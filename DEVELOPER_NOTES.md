## 📦 Database Setup Instructions for Collaborators
This project uses a local SQL Server `.mdf` file for development, but `.mdf` files aren't good for version control (Git) because:

* `.mdf` files are **binary**, so Git can't track changes or merge properly.
* They're often **locked** by SQL Server or Visual Studio.
* Git may show errors like `Permission Denied` or `unable to process path`.

### ✅ Instead of syncing `.mdf` via Git...
We are using a folder called **`/DatabaseScripts`** inside the project. This contains SQL files for:
* Creating tables
* Inserting seed data
* Adding stored procedures

---

## 🚀 How to Set Up the Database Locally
1. Open **SQL Server Management Studio** or use Visual Studio's **Server Explorer**.
2. Create a new local database manually (e.g., `SalesAndInventoryDB`).
3. Open the `.sql` files inside `/DatabaseScripts`.
4. Execute them in the correct order:
   * `01_CreateTables.sql`
   * `02_InsertSeedData.sql`
   * `03_StoredProcedures.sql` (if any)
Once done, your database will match the current project structure, and the app will work normally.

---
## 🔁 Keeping the Database in Sync
If you:
* Add a new table
* Modify columns
* Insert default data
* Add a stored procedure

➡️ Please update or add a new `.sql` file in the `DatabaseScripts` folder so we can all stay in sync.
Avoid pushing `.mdf` and `.ldf` files to Git. They are **excluded in `.gitignore`** for safety.

---
---
# 🛠️ Project Setup Guide for Collaborators  

## 1. Clone the Repository  
**In Visual Studio:**  
1. Go to **Git** → **Clone Repository**  
2. Paste the repository URL: `https://github.com/YourUsername/FinalActivity3.git`  
3. Choose a local folder → Click **Clone**  

---

## 2. Clean Up App_Data  
After cloning:  
1. **Delete the existing `App_Data` folder** (if present) to avoid conflicts.  
2. **Create a new `App_Data` folder** in your project:  
   - Right-click project → **Add** → **New Folder** → Name it `App_Data`  

---

## 3. Set Up Local Database  
### Option A: Create New Database via SQL Scripts  
1. Open **SQL Server Management Studio (SSMS)** or **Visual Studio SQL Server Object Explorer**.  
2. Create a new database named `SalesInventoryDB`.  
3. Run the SQL scripts in this order (from `/DatabaseScripts`):  
   ```sql
   01_CreateTables.sql → 02_InsertSeedData.sql → 03_StoredProcedures.sql  
   ```  
4. The database will generate an `.mdf` file in your new `App_Data` folder automatically.  

### Option B: Attach Existing Database (Advanced)  
1. Create a blank `.mdf` file in `App_Data` (Right-click `App_Data` → **Add** → **New Item** → SQL Server Database).  
2. Use SSMS to attach the database:  
   - Right-click **Databases** → **Attach** → Browse to your `.mdf` file.  

---

## 4. Configure Connection String  
Update the connection string in your **class file** (e.g., `DatabaseHelper.cs`) to match your local SQL Server path:  

```csharp
static string ConnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;
                        AttachDbFilename=C:\Your\Project\Path\App_Data\SalesInventoryDB.mdf;
                        Integrated Security=True";
```

### Key Notes:  
1. **Customize the Path**:  
   Replace `C:\Your\Project\Path\` with your actual project directory.  
   - Tip: Right-click the `.mdf` file in **Solution Explorer** → **Properties** → Copy the full path.  

2. **No Web.config Changes Needed**  
   This project uses a **hardcoded connection string in the class file** instead of `Web.config`.  

3. **Verify SQL Server Instance**:  
   Ensure `(LocalDB)\MSSQLLocalDB` matches your local instance name.  

---

## 5. Build and Run  
1. Clean the solution: **Build** → **Clean Solution**  
2. Rebuild: **Build** → **Rebuild Solution**  
3. Run: **Debug** → **Start Debugging (F5)**  

---

## 🚨 Troubleshooting  
### Common Issues:  
- **"Database cannot be opened"**: Ensure the `.mdf` isn’t locked by SQL Server/Visual Studio.  
- **Login failed**: Verify `Integrated Security=True` matches your SQL Server authentication mode.  
- **Missing stored procedures**: Re-run the SQL scripts.  

---
---



# 📂 File Directory Guide for Collaborators

## Master Page Reference System
The project uses a structured folder system with `MainMasterpage.Master` located in the **User** folder. Here's how to properly reference files:

### Correct Path Formatting
```aspnet
<!-- For files in SAME directory -->
MasterPageFile="MainMasterpage.Master"

<!-- For files in SUBDIRECTORY -->
MasterPageFile="~/FolderName/Masterpage.Master"

<!-- For files in PARENT directory -->
MasterPageFile="../Masterpage.Master"
```

## Key Directory Structure
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

## Critical Rules for File References
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

## Troubleshooting Missing Files
If you encounter "does not exist" errors:
1. Verify physical file exists in Solution Explorer
2. Check for typos in:
   - File extensions (`.master` vs `.Master`)
   - Path separators (`/` not `\`)
   - CodeBehind declarations
3. Clean and rebuild solution

## Example Fixes
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

Pro Tip: Use Solution Explorer's "Copy Path" feature to ensure correct references!

---
---

| ❌ Issue                                                                                | 🧠 Cause & 🛠 Fix                                                                                                                                                                                                                                                                                                                                          |
| -------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **“Conversion failed when converting the nvarchar value 'MNTR-IPS' to data type int”** | Likely a mismatch in your database schema — e.g., you're passing a string like `ProductID='MNTR-IPS'` into a stored procedure expecting an `INT`. Check the `ProductID` type in both your tables and your SPs. Either: <br>• Change the column type to `NVARCHAR` in your tables/SPs <br>• Or make sure you pass actual integers if the schema is correct. |
| **Stored Procedure runs, but returns nothing**                                         | 1. Check if the `@TransactionID` or other parameters match actual data in the DB. <br>2. Verify that your joins are not filtering out data (e.g., inner join on `ProductID` that doesn’t match). Use `LEFT JOIN` for debugging.                                                                                                                            |
| **GridView is blank**                                                                  | Common causes: <br>• You’re binding before `DataTable` is filled. <br>• Your SQL query returned zero rows. <br>• You forgot to set `DataSource` or call `.DataBind()`. <br>• Check your ASPX GridView: If using `<Columns>`, set `AutoGenerateColumns="False"` explicitly.                                                                                 |
| **Error: Object reference not set to an instance of an object**                        | Usually occurs when you try to access a control, data row, or object that wasn’t initialized. Add null checks: <br>`if (myObject != null) { /* use it */ }`                                                                                                                                                                                                |
| **ASP.NET page links broken (404/403 errors)**                                         | • Check your `MasterPage.master` or other hyperlinks: always use `~/` (root-based paths) for ASP.NET, e.g.: <br>`<asp:HyperLink NavigateUrl="~/Admin/ProductPage.aspx">` <br>• Make sure the `.aspx` page exists at the specified path.                                                                                                                    |
| **Page opens blank with no error**                                                     | 1. Check if Page\_Load has `if (!IsPostBack)` around critical logic. <br>2. Verify no exception is being silently caught. <br>3. Try setting a breakpoint in Page\_Load to step through.                                                                                                                                                                   |
| **Stored Procedure not found / fails at runtime**                                      | 1. You forgot to run `03_StoredProcedures.sql` in SSMS. <br>2. You’re calling `SC_ProcessCheckout` but the actual name is different or not under `dbo`. Confirm full name. <br>3. Check for typos and case sensitivity.                                                                                                                                    |
| **.mdf file won’t attach / “cannot be opened because it is version XXX”**              | • Delete and recreate the `.mdf` using your SQL Server version. <br>• Or create the DB manually in SSMS and skip the `.mdf` entirely (just use the server-based DB).                                                                                                                                                                                       |
| **Login failed for user 'IIS APPPOOL\DefaultAppPool'**                                 | • Use `Integrated Security=True` to avoid hardcoding usernames/passwords. <br>• Or configure SQL Server to allow access to that user.                                                                                                                                                                                                                      |
| **ASP.NET designer.cs file not updating / code-behind error**                          | 1. Close and reopen the `.aspx` file. <br>2. Delete `bin/` and `obj/` folders. <br>3. Rebuild solution. <br>4. Make sure `Inherits="YourNamespace.YourPage"` matches the code-behind class.                                                                                                                                                                |
| **Default homepage won’t show**                                                        | Set `Homepage.aspx` (or whatever default) as **Start Page**: right-click the file in Solution Explorer → Set as Start Page.                                                                                                                                                                                                                                |
| **Changes to table or SP aren’t reflected in app**                                     | You may be editing an `.sql` file but not re-running it in SSMS. Every time you change schema/data logic, **re-execute the script manually in SSMS**.                                                                                                                                                                                                      |
| **Project won't build after clean**                                                    | If you get missing reference errors: <br>• Right-click the solution → Rebuild Solution. <br>• If that fails, check that all projects are properly referenced (Class Library → WebForms).                                                                                                                                                                   |
| **Error: “Keyword not supported: 'attachdbfilename'”**                                 | You’re likely trying to use `.mdf` with a non-LocalDB provider. Make sure your connection string uses `LocalDB`, not `SQLEXPRESS` or an actual SQL Server instance.                                                                                                                                                                                        |
| **Session errors or null session data**                                                | If you're trying to use session variables (`Session["X"]`) too early (e.g., before login or on the wrong page), they may be null. Add null checks and consider default redirects.                                                                                                                                                                          |
| **CSS/JS not loading**                                                                 | 1. Use relative paths properly in `<link>` and `<script>` tags: <br>• Prefer `~/Content/style.css` via `<asp:Content>` instead of plain HTML pathing. <br>2. ASP.NET treats folders like `/Scripts`, `/Content`, and `/App_Data` differently—ensure your static files are not in restricted folders.                                                       |

<br>

---
---

## 🔧 Quick “Free Fixes” for Fresh Clones

When you first clone the repo, you might hit one of these errors. Try these in order:

### 1. Roslyn Compiler Missing (`roslyn\csc.exe`)

**Symptom:**  
```
Could not find a part of the path '…\bin\roslyn\csc.exe'
```

**Fix:**  
1. In Visual Studio, right-click your WebForms project → **Manage NuGet Packages…**  
2. Install or update **Microsoft.CodeDom.Providers.DotNetCompilerPlatform**.  
3. Rebuild—Visual Studio will recreate `bin/roslyn/csc.exe`.

---

### 2. Native SQL Server Types Missing

**Symptom:** 
```
Could not copy '…\SqlServerTypes\x64\SqlServerSpatial140.dll' because it was not found.
```
**Fix:**  
1. In **Manage NuGet Packages**, install/update **Microsoft.SqlServer.Types**.  
2. In Solution Explorer, expand the package to **build\native\x64** and **x86**, right-click each folder → **Include In Project**.  
3. For each DLL, set **Copy to Output Directory = Always**.  
4. Rebuild so those native DLLs are deployed.

---

### 3. 403.14 Forbidden — No Default Document

**Symptom:**  
Visiting `http://localhost:xxxxx/` shows directory listing forbidden.

**Fix A (Quick):**  
- Right-click `Homepage.aspx` (or your landing page) → **Set as Start Page** → F5.

**Fix B (Persistent):**  
Add to `Web.config` under `<system.webServer>`:

```xml
<defaultDocument>
  <files>
    <clear />
    <add value="Homepage.aspx" />
  </files>
</defaultDocument>
````

Replace `Homepage.aspx` with the correct root page.

e most common startup blockers so your project builds and serves its homepage correctly.



