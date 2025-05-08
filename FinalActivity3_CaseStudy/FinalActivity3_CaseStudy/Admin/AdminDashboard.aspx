<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterpage.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="FinalActivity3_CaseStudy.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        Welcome to Admin Page</p>
    <p>
        <br />
        <b><a href="AdminLogin.aspx">Admin Login</a></b> </p>
    <br />
        <b><a href="AdminView.aspx">View Records</a></b> </p>
            <br />
        <b><a href="RegisterAdmin.aspx">Add Admin User</a></b> </p>
</asp:Content>
