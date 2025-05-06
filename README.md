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
2. **Create a new `App_Data` folder**:  
   - Right-click project → **Add** → **New Folder** → Name it `App_Data`.  

---

## 3. Database Setup  
### 🚫 Why We Don’t Sync `.mdf` Files  
- **Binary files**: Git can’t track changes or merge `.mdf` files.  
- **Locked files**: SQL Server/Visual Studio often locks them, causing errors like `Permission Denied`.  

### ✅ Use SQL Scripts Instead  
1. **Run the scripts** in `/DatabaseScripts` in order:  
   ```sql
   01_CreateTables.sql → 02_InsertSeedData.sql → 03_StoredProcedures.sql  
   ```  
2. **Execute in SQL Server Management Studio (SSMS)**:  
   - Create a new database (e.g., `SalesInventoryDB`).  
   - Open each script → Execute (F5).  

### 🔄 Keeping the Database in Sync  
If you modify the database schema:  
- Add/update `.sql` files in `/DatabaseScripts`.  
- **Never commit `.mdf`/`.ldf` files** (they’re excluded via `.gitignore`).  

---

## 4. Configure Connection String  
Update the **hardcoded connection string** in your class file (e.g., `DatabaseHelper.cs`):  
```csharp
static string ConnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;
                        AttachDbFilename=C:\Your\Project\Path\App_Data\SalesInventoryDB.mdf;
                        Integrated Security=True";
```

### 🔧 Customization Notes:  
- Replace `C:\Your\Project\Path\` with your actual project directory.  
- **No `Web.config` changes**: The connection is managed directly in code.  
- Verify your SQL Server instance name (e.g., `(LocalDB)\MSSQLLocalDB`).  

---

## 5. Build and Run  
1. **Clean**: `Build` → `Clean Solution`.  
2. **Rebuild**: `Build` → `Rebuild Solution`.  
3. **Run**: `Debug` → `Start Debugging (F5)`.  

---

## 🚨 Troubleshooting  
| Issue                          | Solution                                      |  
|--------------------------------|-----------------------------------------------|  
| **"Database cannot be opened"** | Close SQL Server/VS → Delete `.mdf/.ldf` file.     |  
| **Login failed**               | Use `Integrated Security=True` for local auth.|  
| **Missing stored procedures**  | Re-run the SQL scripts.                       |  

**Need help?**  
Contact [Your Name] at [your.email@example.com] or propose fixes via a pull request.  

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

---
---
---
