<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterpage.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="FinalActivity3_CaseStudy.Admin.AdminDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Admin Homepage</h2>
    <p>
        <br />
        <b><a href="AdminLogin.aspx">Admin Login</a></b>
    </p>
    <br />
    <b><a href="AdminView.aspx">View Records</a></b> </p>
            <br />
    <b><a href="RegisterAdmin.aspx">Add Admin User</a></b> </p>
    <p>
        <br />
    </p>
       <p>
       &nbsp;
   </p>
   <p>
       <asp:Label ID="lblMessage" runat="server"></asp:Label>
   <p>
</asp:Content>
