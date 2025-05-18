<%@ Page Title="" Language="C#" MasterPageFile="~/User/MainMasterpage.Master"
    AutoEventWireup="true" CodeBehind="ProductCatalog.aspx.cs"
    Inherits="FinalActivity3_CaseStudy.User.ProductPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /* Page-scoped styles for Product Catalog */
        .product-catalog {
            padding: 20px;
            font-family: Arial, sans-serif;
            background: #fafafa;
            border-radius: 8px;
        }
        .product-catalog h2 {
            margin-bottom: 10px;
            color: #333;
            font-size: 28px;
        }
        .product-catalog h3 {
            margin-top: 30px;
            color: #555;
            font-size: 22px;
            border-bottom: 2px solid #ddd;
            padding-bottom: 5px;
        }

        .product-catalog .pc-cart-link {
          float: right;
          font-size: 16px;
          margin-top: -30px;
        }
        .product-catalog .pc-cart-link a {
          text-decoration: none;
          color: #007bff;
        }
        .product-catalog .pc-cart-link a:hover {
          text-decoration: underline;
        }

        #gvProductTable {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }
        #gvProductTable th, 
        #gvProductTable td {
            padding: 8px 12px;
            border: 1px solid #ddd;
            text-align: left;
        }
        #gvProductTable th {
            background: #f0f0f0;
            font-weight: bold;
        }
        .quantity-input {
            width: 50px;
            padding: 4px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        .add-cart-btn {
            background: #28a745;
            color: #fff;
            border: none;
            padding: 6px 10px;
            border-radius: 4px;
            cursor: pointer;
        }
        .add-cart-btn:hover {
            background: #218838;
        }
        .message {
            margin-top: 15px;
            display: block;
            font-weight: bold;
            color: #d9534f;
        }
        /* clear float */
        .clearfix::after {
            content: "";
            display: table;
            clear: both;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="product-catalog">
        <h2>Product Catalog</h2>

        <div class="clearfix">
            <h3>Featured Products</h3>
            <div class="pc-cart-link">
                🛒 <a href="ShoppingCart.aspx">Your Shopping Cart</a>
            </div>
        </div>

        <asp:GridView ID="gvProductTable" runat="server" AutoGenerateColumns="False"
            OnRowCommand="gvProductTable_RowCommand"
            DataKeyNames="ProductID,Stocks"
            CssClass="gvProductTable">
            <Columns>
                <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                <asp:BoundField DataField="Price" HeaderText="Price" 
                    DataFormatString="{0:C}" HtmlEncode="false" />
                <asp:BoundField DataField="Stocks" HeaderText="In Stock" />

                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="txtQty" runat="server" Text="1"
                            CssClass="quantity-input" />
                        <asp:RangeValidator ID="rvQty" runat="server"
                            ControlToValidate="txtQty"
                            Type="Integer" MinimumValue="1"
                            MaximumValue='<%# Eval("Stocks") %>'
                            ErrorMessage="Invalid Qty" Display="Dynamic"
                            ForeColor="Red" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnAddToCart" runat="server"
                            Text="Add to Cart"
                            CssClass="add-cart-btn"
                            CommandName="AddToCart"
                            CommandArgument='<%# Container.DataItemIndex %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblMessage" runat="server" CssClass="message" />
    </div>
</asp:Content>
