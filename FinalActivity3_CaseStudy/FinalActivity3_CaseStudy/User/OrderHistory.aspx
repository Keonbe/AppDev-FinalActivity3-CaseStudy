<%@ Page Title="" Language="C#" MasterPageFile="~/User/MainMasterpage.Master" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="FinalActivity3_CaseStudy.User.OrderHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <br /><b><a href="/User/ProductCatalog.aspx">Back to Catalog</a></b>
    </p>
    <p>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </p>
    <asp:Label ID="lblOrderhistory" runat="server">Transaction History</asp:Label>
    <div class="grid-container">
        <asp:GridView ID="gvOrderHistory" runat="server" CssClass="grid-view"
                      AutoGenerateColumns="True" EmptyDataText="No transaction history found.">
        </asp:GridView>
    </div>

    <br>
    <br>
    <asp:Label ID="Label1" runat="server">Order Details</asp:Label>
    <div class="grid-container">
    <asp:GridView ID="gvOrderDetails" runat="server" CssClass="grid-view"
                  AutoGenerateColumns="True" EmptyDataText="No transaction history found.">
    </asp:GridView>
</div>
</asp:Content>
