<%@ Page Title="" Language="C#" MasterPageFile="~/User/MainMasterpage.Master" AutoEventWireup="true" CodeBehind="Regisration.aspx.cs" Inherits="FinalActivity3_CaseStudy.Regisration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .auto-style1 {
        width: 100%;
    }
    .auto-style2 {
        width: 306px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style1">
    <tr>
        <td class="auto-style2">Name</td>
        <td>
            <asp:TextBox ID="tbName" runat="server" Width="512px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbName" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="auto-style2">Email Address</td>
        <td>
            <asp:TextBox ID="tbEmail" runat="server" Width="512px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbEmail" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="auto-style2">Password</td>
        <td>
            <asp:TextBox ID="tbPassword" runat="server" Width="512px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbPassword" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="auto-style2">Confirm Password</td>
        <td>
            <asp:TextBox ID="tbConfirmPassword" runat="server" Width="512px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbConfirmPassword" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="tbPassword" ControlToValidate="tbEmail" ErrorMessage="CompareValidator" ForeColor="#CC0000">Password Mismatch</asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="auto-style2">Membership Type</td>
        <td>
            <asp:DropDownList ID="ddlMembershipType" runat="server" AutoPostBack="True">
                <asp:ListItem Value="0.5">Silver</asp:ListItem>
                <asp:ListItem Value="0.10">Gold</asp:ListItem>
                <asp:ListItem Value="0.15">Platinum</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="auto-style2">&nbsp;</td>
        <td>
            <asp:Button ID="btnRegister" runat="server" Text="Register Account" />
        </td>
    </tr>
</table>
<p>
    <asp:Label ID="lblMessage" runat="server"></asp:Label>
</p>
    <p>
</p>
    <p>
</p>
<p>
    <a href="/User/Login.aspx">Already Have an Account?</a>

</p>
</asp:Content>
