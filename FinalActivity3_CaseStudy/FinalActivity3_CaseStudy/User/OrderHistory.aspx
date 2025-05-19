<%@ Page Title="" Language="C#" MasterPageFile="~/User/MainMasterpage.Master" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="FinalActivity3_CaseStudy.User.OrderHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            border-collapse: separate;
            border-spacing: 0;
            border-radius: var(--border-radius);
            overflow: hidden;
            font-size: 0.9rem;
            margin-bottom: var(--spacing-lg);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <br /><b><a href="/User/ProductCatalog.aspx">Back to Catalog</a></b>
    </p>
    <p>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </p>
    <br>
  <h2><asp:Label ID="lblOrderhistory" runat="server" Text="Transaction History" /></h2>
  <div class="grid-container">
    <asp:GridView 
        ID="gvOrderHistory" 
        runat="server" 
        CssClass="auto-style1"
        AutoGenerateColumns="False"             
        DataKeyNames="TransactionID"
        OnSelectedIndexChanged="gvOrderHistory_SelectedIndexChanged"
        OnRowCommand="gvOrderHistory_RowCommand"
        EmptyDataText="No transaction history found for your account." Height="282px">
      <Columns>
        <asp:BoundField DataField="TransactionID"   HeaderText="Order ID" />
        <asp:BoundField DataField="DateTime"        HeaderText="Date/Time" 
                        DataFormatString="{0:G}" />
        <asp:BoundField DataField="TotalAmount"     HeaderText="Total"    
                        DataFormatString="{0:C}" />
        <asp:BoundField DataField="MembershipType"  HeaderText="Tier" />
        <asp:CommandField ShowSelectButton="True"   SelectText="View Details" />
      </Columns>
    </asp:GridView>
  </div>

    <br>
    <br>
<h2>Order Details</h2>
<div class="grid-container">
  <asp:GridView ID="gvOrderDetails" runat="server"
      CssClass="grid-view"
      AutoGenerateColumns="False"
      EmptyDataText="Select an order to view its items.">
    <Columns>
      <asp:BoundField DataField="ProductID"    HeaderText="Product ID" />
      <asp:BoundField DataField="ProductName"  HeaderText="Product Name" />
      <asp:BoundField DataField="Quantity"     HeaderText="Qty" />
      <asp:BoundField DataField="Unit Price"   HeaderText="Unit Price"   DataFormatString="{0:C}" />
      <asp:BoundField DataField="Discount"     HeaderText="Discount Rate" DataFormatString="{0:P0}" />
      <asp:BoundField DataField="TotalAmount"  HeaderText="Line Total"    DataFormatString="{0:C}" />
    </Columns>
  </asp:GridView>
</div>
</asp:Content>
