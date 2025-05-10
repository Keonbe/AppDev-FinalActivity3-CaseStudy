<%@ Page Title="" Language="C#" MasterPageFile="~/User/MainMasterpage.Master" AutoEventWireup="true" CodeBehind="ProductCatalog.aspx.cs" Inherits="FinalActivity3_CaseStudy.User.ProductPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    </p>
    <div>
        <asp:GridView ID="gvProductTable" runat="server" AutoGenerateColumns="False"
            OnRowCommand="gvProductTable_RowCommand" DataKeyNames="LoanID">

            <Columns>
                <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                <asp:BoundField DataField="Price" HeaderText="Price" />
                <asp:BoundField DataField="Stocks" HeaderText="Stocks" />

                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="txtQty" runat="server" Text="1" Width="40px" />
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
        </p>

    </div>
</asp:Content>
