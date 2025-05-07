<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterpage.Master" AutoEventWireup="true" CodeBehind="TestAdminView.aspx.cs" Inherits="FinalActivity3_CaseStudy.Admin.TestAdminView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Test Admin View</h2>
    <asp:RadioButtonList
        ID="rblViewSelector"
        runat="server"
        AutoPostBack="True"
        OnSelectedIndexChanged="rblViewSelector_SelectedIndexChanged">
        <asp:ListItem Text="View Products" Value="GetAllProducts" Selected="True" />
        <asp:ListItem Text="View Members" Value="GetAllMembers" />
        <asp:ListItem Text="View Transactions" Value="GetAllTransactions" />
    </asp:RadioButtonList>
    <p>
        &nbsp;
    </p>
    <asp:Button
        ID="btnLoad"
        runat="server"
        Text="Load"
        OnClick="btnLoad_Click" />
    <p>
        &nbsp;
    </p>
    <!-- Your GridView, no DataSourceID yet -->
    <asp:GridView
        ID="gvAdmin"
        runat="server"
        AutoGenerateColumns="True"
        CssClass="table table-bordered">
    </asp:GridView>


    <p>
        &nbsp;
    </p>
</asp:Content>
