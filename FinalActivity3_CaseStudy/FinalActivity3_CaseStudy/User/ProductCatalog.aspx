<%@ Page Title="" Language="C#" MasterPageFile="~/User/MainMasterpage.Master" AutoEventWireup="true" CodeBehind="ProductCatalog.aspx.cs" Inherits="FinalActivity3_CaseStudy.User.ProductPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        Product Catalog
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        &nbsp;
        <b><a href="ShoppingCart.aspx">Your shopping Cart</a></b>
    </p>
    </p>
    <div>
        <asp:GridView ID="gvProductTable" runat="server" AutoGenerateColumns="False"
            OnRowCommand="gvProductTable_RowCommand" DataKeyNames="ProductID,Stocks">

            <Columns>
                <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                <asp:BoundField DataField="Price" HeaderText="Price" />
                <asp:BoundField DataField="Stocks" HeaderText="Stocks" />

                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="txtQty" runat="server" Text="1" Width="40px" />
                        <asp:RangeValidator
                            ID="rvQty"
                            runat="server"
                            ControlToValidate="txtQty"
                            Type="Integer"
                            MinimumValue="1"
                            MaximumValue='<%# Eval("Stocks") %>'
                            ErrorMessage="Qty must be between 1 and available stock"
                            Display="Dynamic"
                            ForeColor="Red" />
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart"
                            CommandName="AddToCart"
                            CommandArgument='<%# Container.DataItemIndex %>' />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <br />
        <p>
            &nbsp;
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </p>

    </div>
</asp:Content>
