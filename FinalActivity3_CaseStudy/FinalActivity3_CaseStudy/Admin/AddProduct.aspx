<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterpage.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="FinalActivity3_CaseStudy.Admin.AddProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            width: 263px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        Add Products
    </h2>
    <p>
        &nbsp;
    </p>
    <table class="auto-style1">
        <tr>
            <td class="auto-style2">Product ID:</td>
            <td>
                <asp:TextBox ID="tbProductID" runat="server" Width="330px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbProductID" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Product Name:</td>
            <td>
                <asp:TextBox ID="tbProductName" runat="server" Width="576px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbProductName" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Base Price:</td>
            <td>
             <asp:TextBox ID="tbPrice" runat="server" Width="249px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="tbPrice" ErrorMessage="Price is required" 
                ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvPrice" runat="server" 
                ControlToValidate="tbPrice" ErrorMessage="Must be a decimal number"
                Operator="DataTypeCheck" Type="Double" ForeColor="Red" Display="Dynamic">*</asp:CompareValidator>
    </td>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Stocks Available:</td>
            <td>
            <asp:TextBox ID="tbStocks" runat="server" Width="247px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="tbStocks" ErrorMessage="Stock is required" 
                ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvStocks" runat="server" 
                ControlToValidate="tbStocks" ErrorMessage="Must be a whole number"
                Operator="DataTypeCheck" Type="Integer" ForeColor="Red" Display="Dynamic">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" OnClick="btnAddProduct_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <p>
        &nbsp;
    </p>
    <p>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    <p>
    <p>
</asp:Content>
