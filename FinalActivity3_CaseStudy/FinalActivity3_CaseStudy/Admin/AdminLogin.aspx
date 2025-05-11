<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterpage.Master" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="FinalActivity3_CaseStudy.AdminLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            margin-left: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
    Admin Login Page</p>
    <table class="auto-style1">
        <tr>
            <td>Username</td>
            <td>
                <asp:TextBox ID="tbUsername" runat="server" CssClass="auto-style2" Width="512px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbUsername" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Password</td>
            <td>
                <asp:TextBox ID="tbPassword" runat="server" CssClass="auto-style2" Width="512px" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbPassword" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btnAdminLogin" runat="server" Text="Login AS Admin" OnClick="btnAdminLogin_Click" />
            </td>
        </tr>
    </table>
<p>
</p>
    <p>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
</p>
    <p>
</p>
    <p>
        <a href="/User/Login.aspx">Not an Admin?</a></p>
</asp:Content>
