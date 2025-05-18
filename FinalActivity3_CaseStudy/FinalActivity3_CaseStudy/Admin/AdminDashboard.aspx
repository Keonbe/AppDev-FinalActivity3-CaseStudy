<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterpage.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="FinalActivity3_CaseStudy.Admin.AdminDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style type="text/css">
    /* Page-scoped Admin Dashboard styles */
    .admin-dashboard {
      padding: 30px;
      font-family: Arial, sans-serif;
      background: #f9f9f9;
      border-radius: 8px;
    }
    .admin-dashboard h2 {
      color: #333;
      margin-bottom: 20px;
      font-size: 28px;
    }
    .admin-actions {
      display: flex;
      flex-wrap: wrap;
      gap: 15px;
      margin-bottom: 30px;
    }
    .admin-actions a {
      display: inline-block;
      padding: 12px 20px;
      background: #007bff;
      color: #fff !important;
      text-decoration: none;
      border-radius: 5px;
      font-size: 16px;
      transition: background 0.2s;
    }
    .admin-actions a:hover {
      background: #0056b3;
    }
    .message {
      margin-top: 20px;
      font-weight: bold;
      color: #d9534f;
    }
  </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="admin-dashboard">
    <h2>Admin Homepage</h2>

    <div class="admin-actions">
      <!-- Summary of Sales -->
      <a href="ReportModule.aspx">Summary of Sales</a>
      <!-- View Record Tables -->
      <a href="AdminView.aspx">View Record Tables</a>
      <!-- Add Admin User -->
      <a href="RegisterAdmin.aspx">Add Admin User</a>
    </div>

    <asp:Label ID="lblMessage" runat="server" CssClass="message" />
  </div>
</asp:Content>