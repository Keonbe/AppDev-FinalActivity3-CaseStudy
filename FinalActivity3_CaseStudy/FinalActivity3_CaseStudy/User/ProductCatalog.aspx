<%@ Page Title="" Language="C#" MasterPageFile="~/User/MainMasterpage.Master" AutoEventWireup="true" CodeBehind="ProductCatalog.aspx.cs" Inherits="FinalActivity3_CaseStudy.User.ProductPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Product Catalog</h2>
    <p>
        &nbsp;
    </p>
    <p>
        &nbsp;
        <h3>Featured Products</h3>
            <div style="float: right;">
            <b><a href="ShoppingCart.aspx">🛒 Your shopping Cart</a></b>
        </div>
        <br>
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
