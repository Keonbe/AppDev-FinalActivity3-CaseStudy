﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterpage.Master" AutoEventWireup="true" CodeBehind="AdminView.aspx.cs" Inherits="FinalActivity3_CaseStudy.Admin.TestAdminView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Admin View</h2>

    <asp:RadioButtonList
        ID="rblViewSelector"
        runat="server"
        AutoPostBack="True"
        OnSelectedIndexChanged="rblViewSelector_SelectedIndexChanged">
      <asp:ListItem Text="View Products"     Value="GetAllProducts"  Selected="True" />
      <asp:ListItem Text="View Members"      Value="GetAllMembers"   />
      <asp:ListItem Text="View Transactions" Value="GetAllTransactions" />
    </asp:RadioButtonList>

    <asp:DropDownList
        ID="ddlSortDir"
        runat="server"
        AutoPostBack="True"
        OnSelectedIndexChanged="rblViewSelector_SelectedIndexChanged"
        Visible="False">
      <asp:ListItem Text="Date ↑"  Value="ASC"  />
      <asp:ListItem Text="Date ↓"  Value="DESC" />
    </asp:DropDownList>


    <p>
        &nbsp;
    </p>
    <!--
    <asp:Button
        ID="btnLoad"
        runat="server"
        Text="Load"
        OnClick="btnLoad_Click" />
    -->
    <p>
        &nbsp;
    </p>
    <!-- Your GridView, no DataSourceID yet -->
    <asp:GridView
        ID="gvAdmin"
        runat="server"
        AutoGenerateColumns="True"
        CssClass="gridview"
        HeaderStyle-CssClass="gridview-header"
        RowStyle-CssClass="gridview-row"
        AlternatingRowStyle-CssClass="gridview-altrow"
        PagerStyle-CssClass="gridview-pager"
        AllowPaging="True"
        PageSize="10"
        OnPageIndexChanging="gvAdmin_PageIndexChanging">
        <HeaderStyle HorizontalAlign="Left" />
        <RowStyle    VerticalAlign="Top" />
    </asp:GridView>



    <p>
        &nbsp;
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </p>
</asp:Content>
