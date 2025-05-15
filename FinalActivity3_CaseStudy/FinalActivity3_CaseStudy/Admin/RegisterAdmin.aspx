<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterpage.Master" AutoEventWireup="true" CodeBehind="RegisterAdmin.aspx.cs" Inherits="FinalActivity3_CaseStudy.Admin.RegisterAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Add Admin User</h2>
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
                <asp:TextBox ID="tbEmailAddress" runat="server" Width="512px" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbEmailAddress" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Password</td>
            <td>
                <asp:TextBox ID="tbPassword" runat="server" Width="512px" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbPassword" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Confirm Password</td>
            <td>
                <asp:TextBox ID="tbConfirmPassword" runat="server" Width="512px" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbConfirmPassword" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="tbPassword" ControlToValidate="tbConfirmPassword" ErrorMessage="CompareValidator" ForeColor="#CC0000">Password Mismatch</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Membership Type</td>
            <td>
                <asp:DropDownList ID="ddlMembershipType" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="Platinum">Platinum</asp:ListItem>
                    <asp:ListItem Value="Gold">Gold</asp:ListItem>
                    <asp:ListItem Value="Silver">Silver</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>
                <asp:Button ID="btnRegister" runat="server" Text="Register Account" OnClick="btnRegister_Click" />
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
        <a href="AdminLogin.aspx">Already Have an Account?</a>
    </p>
</asp:Content>
