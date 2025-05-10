<%@ Page Title="" Language="C#" MasterPageFile="~/User/MainMasterpage.Master" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="FinalActivity3_CaseStudy.User.OrderHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <br /><b><a href="/User/ProductCatalog.aspx">Back to Catalog</a></b>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        <asp:GridView ID="gvOrderHistory" runat="server" AutoGenerateColumns="True" CssClass="table table-bordered">
        </asp:GridView>
    </p>
</asp:Content>
