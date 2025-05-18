<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLoginPage.aspx.cs" Inherits="FinalActivity3_CaseStudy.Admin.AdminLoginPage" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Admin Login Page</title>
    <style>
        /* Base styles */
        :root {
            --primary-color: #2c3e50;
            --secondary-color: #34495e;
            --accent-color: #3498db;
            --text-color: #333;
            --light-bg: #f5f7fa;
            --white: #ffffff;
            --error: #e74c3c;
            --shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }
        
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: var(--light-bg);
            margin: 0;
            padding: 0;
            color: var(--text-color);
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }
        
        /* Login container */
        .login-container {
            background-color: var(--white);
            border-radius: 8px;
            box-shadow: var(--shadow);
            width: 100%;
            max-width: 450px;
            padding: 2rem;
        }
        
        /* Header */
        .header {
            text-align: center;
            margin-bottom: 2rem;
        }
        
        .title {
            color: var(--primary-color);
            font-size: 1.75rem;
            font-weight: bold;
            margin-bottom: 0.5rem;
        }
        
        h2 {
            color: var(--secondary-color);
            font-size: 1.25rem;
            font-weight: 500;
            margin-top: 0;
        }
        
        /* Form styles */
        .form-group {
            margin-bottom: 1.5rem;
        }
        
        .form-group label {
            display: block;
            margin-bottom: 0.5rem;
            font-weight: 500;
            color: var(--secondary-color);
        }
        
        .form-control {
            width: 100%;
            padding: 0.75rem;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-size: 1rem;
            box-sizing: border-box;
        }
        
        .form-control:focus {
            outline: none;
            border-color: var(--accent-color);
            box-shadow: 0 0 0 2px rgba(52, 152, 219, 0.25);
        }
        
        /* Button */
        .btn-primary {
            background-color: var(--accent-color);
            color: white;
            border: none;
            border-radius: 4px;
            padding: 0.75rem 1.5rem;
            font-size: 1rem;
            cursor: pointer;
            width: 100%;
            transition: background-color 0.2s;
        }
        
        .btn-primary:hover {
            background-color: #2980b9;
        }
        
        /* Validation */
        .error {
            color: var(--error);
        }
        
        /* Link */
        .redirect-link {
            display: block;
            text-align: center;
            margin-top: 1.5rem;
            color: var(--accent-color);
            text-decoration: none;
        }
        
        .redirect-link:hover {
            text-decoration: underline;
        }

        /* Message */
        .message {
            text-align: center;
            margin-top: 1rem;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <div class="header">
                <div class="title">Sales & Inventory System</div>
                <h2>Admin Login</h2>
            </div>
            
            <div class="form-group">
                <label for="tbUsername">Username</label>
                <asp:TextBox ID="tbUsername" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="tbUsername" ErrorMessage="Username is required" 
                    ForeColor="Red" CssClass="error" Display="Dynamic">*</asp:RequiredFieldValidator>
            </div>
            
            <div class="form-group">
                <label for="tbPassword">Password</label>
                <asp:TextBox ID="tbPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="tbPassword" ErrorMessage="Password is required" 
                    ForeColor="Red" CssClass="error" Display="Dynamic">*</asp:RequiredFieldValidator>
            </div>
            
            <div class="form-group">
                <asp:Button ID="btnAdminLogin" runat="server" Text="Login as Admin" 
                    OnClick="btnAdminLogin_Click" CssClass="btn-primary" />
            </div>
            
            <div class="message">
                <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
            </div>
            
            <a href="/HomePage/newLogin.aspx" class="redirect-link">Not an Admin? Login as User</a>
        </div>
    </form>
</body>
</html>