# FinalActivity3
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
---

<br>

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
