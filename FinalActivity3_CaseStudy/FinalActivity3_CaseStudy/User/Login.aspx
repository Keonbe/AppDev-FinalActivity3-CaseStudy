<%@ Page Title="" Language="C#" MasterPageFile="~/User/MainMasterpage.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FinalActivity3_CaseStudy.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .login-container {
            display: flex;
            flex-direction: column;
            width: 100%;
        }
        
        .login-title {
            font-size: 1.25rem;
            font-weight: bold;
            margin-bottom: 1rem;
        }
        
        .login-form {
            display: flex;
            flex-direction: column;
            max-width: 500px;
        }
        
        .form-row {
            display: flex;
            margin-bottom: 1rem;
        }
        
        .form-label {
            width: 150px;
            padding-top: 5px;
        }
        
        .form-field {
            flex: 1;
        }
        
        .button-row {
            display: flex;
            margin-top: 0.5rem;
        }
        
        .admin-link {
            margin-top: 2rem;
            font-weight: bold;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <div class="login-title">
            Login Page
        </div>
        
        <div class="login-form">
            <div class="form-row">
                <div class="form-label">Email Address</div>
                <div class="form-field">
                    <asp:TextBox ID="TextBox1" runat="server" Width="100%"></asp:TextBox>
                </div>
            </div>
            
            <div class="form-row">
                <div class="form-label">Password</div>
                <div class="form-field">
                    <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" Width="100%"></asp:TextBox>
                </div>
            </div>
            
            <div class="form-row">
                <div class="form-label"></div>
                <div class="form-field button-row">
                    <asp:Button ID="Button1" runat="server" Text="Login" />
                </div>
            </div>
        </div>
        
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        
        <div class="admin-link">
            <a href="AdminLogin.aspx">Are you admin?</a>
        </div>
    </div>
</asp:Content>