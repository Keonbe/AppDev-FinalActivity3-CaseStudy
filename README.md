# FinalActivity3[LC] / Final Exam [LA]
---

# Sales and Inventory System - Case Study

## Overview
A company needs an **automated sales and inventory system**. As developers, your task is to create the following functionalities:

### 🔒 Admin Page
- Add New Products
- View Transactions
- View Records (Members and Products)
- Report Module (Summary of Sales)
   - Using "Microsoft.ReportingServices.ReportViewerControl.WebForm"

### 👤 User Page
- Profile Manager (Change Password only)
- View Order History
- Buy/Add Products to Cart
- Logout

---

## A. 🛒 Product Inventory

| ID | Product ID | Product Name        | Price     | Stocks | SRP (15% of Price) |
|----|------------|---------------------|-----------|--------|--------------------|
| 1  | MSE        | Mouse               | 350.00    | 100    |                    |
| 2  | PRN        | Printer Ink         | 7500.00   | 100    |                    |
| 3  | PRNDT      | Printer Dot Matrix  | 5000.00   | 100    |                    |
| 4  | MNTRLc     | LCD Monitor         | 6500.00   | 100    |                    |
| 5  | MNTRLe     | LED Monitor         | 7500.00   | 100    |                    |

---

## B. 💵 Sales System

| Product Name | Price   | Quantity | Amount   |
|--------------|---------|----------|----------|
| xxx          | xxx     | xxx      | xxx.xx   |
| **Total Amount** |         |          | **xxx.xx** |


### 💡 Formula:
Total Amount Purchase = (Total Amount + VAT) – Discount (if any)
<br>
VAT = 10% of Total Amount


### 🏷️ Membership Discounts
Discounts are only applicable to purchases with a total amount of **₱10,000.00 and above**.

| Membership Type | Discount Rate |
|------------------|----------------|
| Silver           | 5% of total amount          |
| Gold             | 10%  of total amount        |
| Platinum         | 15%  of total amount        |


---


## ✅ Requirements
- Design a GUI using **Master Page or CSS Template**
- Display **date and time** of each sales transaction
- Create your own **Database Design** (multiple tables allowed)
- Place all **computations inside a Class Library**
- Submit the following:
  - Screenshots of all webpages
  - Screen video showing system functionality (Use [Bandicam](https://www.bandicam.com/))

---

## 🔄 System Flowchart

### Basic Flow

```
START
  ↓
HOME PAGE <- [C]
  ↓
LOGIN
  ↓
ACCOUNT EXIST?
  ├── NO → REGISTRATION PAGE/[B] → [C]
  └── YES [A]
        ↓ {End Flow 1; Initial Page]
    USER/ADMIN? / [A]
      ├── USER
      │     ↓
      │  USER PAGE
      │     ├── Profile Manager (Change Password Only)
      │     ├── View Order History
      │     ├── Buy/Add Products to Cart
      │     └── Logout
      │         ↓
      │        END
      └── ADMIN
            ↓
        ADMIN PAGE
            ├── Add New Products
            ├── View Records
            │     ├── All Products
            │     ├── Members Records
            │     └── Transactions
            └── Report Module
                  ↓
                 END
```
---
---
---
<br>

# 🛠️ Project Setup Guide for Collaborators  

A C# ASP .NET WebForms + ADO.NET project for managing products, cart & checkout, and reporting.
---

## 1. Clone the Repository

1. In Visual Studio, **Git → Clone Repository**  
2. Paste URL: `https://github.com/Keonbe/AppDev-FinalActivity3-CaseStudy`  
3. Choose your local folder → **Clone**

---

## 2. App_Data & Database Scripts

### Why _not_ commit the `.mdf` file

- **Binary & locked** — Git can’t diff or merge it, and VS/SQL Server locks it.
- **Team use** — everyone runs the same SQL scripts instead.

### Prepare your local DB

1. Delete any existing **App_Data** folder.  
2. Add a new **App_Data** folder to the project.  
3. Open **SQL Server Management Studio (SSMS)** and create a new database, e.g. `SalesInventoryDB`.  
4. In SSMS, in this order execute the scripts in `/DatabaseScripts`:

   - `01_CreateTables.sql`  
   - `02_InsertSeedData.sql`  
   - `03_StoredProcedures.sql`  

   Each `GO` block will create tables, seed data, and install SPs.

---

## 3. Configure Connection String

In **ClassLibrary/DatabaseHelper.cs** (or wherever `ConnStr` is defined):

```csharp
static string ConnStr = @"
  Data Source=(LocalDB)\MSSQLLocalDB;
  AttachDbFilename=C:\FULL\PATH\TO\PROJECT\App_Data\SalesInventoryDB.mdf;
  Integrated Security=True";
````

* **Replace** `C:\FULL\PATH\TO\PROJECT\…` with your local path.
* Confirm your LocalDB instance name (often `(LocalDB)\MSSQLLocalDB`).
* No need to edit Web.config; the code reads it directly.

---

## 4. Build & Run

1. **Clean** → Build → Clean Solution
2. **Rebuild** → Build → Rebuild Solution
3. **Start** → Debug → Start Debugging (F5)

> **Tip:** Avoid rebuilding mid-session unless you’ve changed references—clean/rebuild can sometimes break the runtime ASPX↔code-behind mapping.

---

## 🔧 Troubleshooting

| Issue                                      | Cause & Fix                                                                                                                                                       |
| ------------------------------------------ | ----------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **“Database cannot be opened”**            | Close VS & SSMS, delete the stale `.mdf`/`.ldf` in **App\_Data**, then re-run the scripts in SSMS.                                                                |
| **Login failed**                           | Ensure your connection string uses `Integrated Security=True`; run SSMS as your Windows user; verify the `UserInfoTable` contains the credentials you’re testing. |
| **Default page (403.14)**                  | No root default document: <br>• In VS, right-click **Homepage.aspx** → **Set as Start Page** <br>• OR add `<defaultDocument>` in Web.config.                      |
| **MasterPage or hyperlink 404/403 errors** | Check `<asp:HyperLink NavigateUrl="~/User/Page.aspx">` paths; ensure the target `.aspx` exists under the correct folder. Use `~/` for site root.                  |
| **GridView won’t render Columns**          | If you declare `<Columns>…</Columns>`, set `AutoGenerateColumns="False"`. Correct syntax: add a space between attributes (`DataField="X" HeaderText="Y"`).        |
| **“Conversion failed…” SQL errors**        | Mismatch between table column types: confirm your SPs and table schema align (e.g. `ProductID` text vs. int PK). Adjust column type or SP JOIN accordingly.       |
| **Stored procedure not found**             | Make sure you ran `03_StoredProcedures.sql`, and that the procedure name in code matches exactly (including schema, e.g. `dbo.SC_ProcessCheckout`).               |
| **Pages not updating after code change**   | Clear browser cache or stop/start IIS Express. In VS, **Project → Clean**, then **Rebuild**.                                                                      |

---

## 5. Tips for Collaborators

* **When you change the schema**: add a new numbered SQL script (`04_…`) under `/DatabaseScripts` and update README.
* **Never** commit `*.mdf`/`*.ldf` — they’re in `.gitignore`.
* **Quick Checklist** below before each PR.

---

### 📝 Quick Checklist

1. **Define** the feature’s “why.”
2. **Plan** pages, methods & data flow.
3. **Build small** & test often.
4. **Understand** AI-generated code; ask “why.”
5. **Name** methods & SQL clearly.
6. **Document** complex logic in comments.
7. **Test** edge cases & error paths.


**Need help?**  
Contact [Your Name] at [your.email@example.com] or propose fixes via a pull request.  

---
---

### 📝 **DEVELOPER_NOTES.md** (New File)  
# Extended Project Documentation  

## Database Architecture  
- Tables, relations, stored procedures  
- Seed data reference  

## Master Page System  
- Path resolution rules  
- Fixes for "does not exist" errors  

## Contribution Guide  
- How to sync database changes  
- Naming conventions  
- Testing protocols
- Proper Commenting
