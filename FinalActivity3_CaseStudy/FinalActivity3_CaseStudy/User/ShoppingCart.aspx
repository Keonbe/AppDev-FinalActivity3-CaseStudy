<%@ Page Title="" Language="C#" MasterPageFile="~/User/MainMasterpage.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="FinalActivity3_CaseStudy.User.ShoppingCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Shopping Cart</h2>
    
    <div>
        <asp:GridView ID="gvCartItems" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="CartId" OnRowCommand="gvCartItems_RowCommand" 
            EmptyDataText="Your cart is empty.">
            <Columns>
                <asp:BoundField DataField="CartId" HeaderText="Cart ID" Visible="false" />
                <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="SubTotal" HeaderText="Sub Total" DataFormatString="{0:C}" />
                
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnRemove" runat="server" Text="Remove" 
                            CommandName="RemoveItem" 
                            CommandArgument='<%# Eval("CartId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <div style="margin-top: 20px;">
        <table>
            <tr>
                <td style="font-weight: bold; padding-right: 10px;">Total Amount:</td>
                <td><asp:Label ID="lblTotalAmount" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-weight: bold; padding-right: 10px;">VAT (10%):</td>
                <td><asp:Label ID="lblVat" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-weight: bold; padding-right: 10px;">Discount:</td>
                <td><asp:Label ID="lblDiscount" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-weight: bold; padding-right: 10px;">Final Amount:</td>
                <td><asp:Label ID="lblFinalAmount" runat="server" Font-Bold="true"></asp:Label></td>
            </tr>
        </table>
    </div>

    <div style="margin-top: 20px;">
        <asp:Button ID="btnContinueShopping" runat="server" Text="Continue Shopping" 
            OnClick="btnContinueShopping_Click" />
        <asp:Button ID="btnCheckout" runat="server" Text="Checkout" 
            OnClick="btnCheckout_Click" />
    </div>
    
    <div style="margin-top: 10px;">
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>