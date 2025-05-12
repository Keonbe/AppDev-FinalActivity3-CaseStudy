<%@ Page Title="" Language="C#" MasterPageFile="~/User/MainMasterpage.Master" AutoEventWireup="true" CodeBehind="ProfileManager.aspx.cs" Inherits="FinalActivity3_CaseStudy.User.ProfileManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 497px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Profile Manager</h2>
    <table class="auto-style1">
        <tr>
            <td class="auto-style2">Current Password</td>
            <td>
                <asp:TextBox ID="tbCurrentPassword" runat="server" Width="320px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                        ControlToValidate="tbCurrentPassword" ErrorMessage="*" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td class="auto-style2">New Password</td>
            <td>
                <asp:TextBox ID="tbNewPassword" runat="server" Width="321px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail0" runat="server" 
                        ControlToValidate="tbNewPassword" ErrorMessage="*" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td class="auto-style2">Confirm New Password</td>
            <td>
                <asp:TextBox ID="tbConfirmNewPassword" runat="server" Width="320px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail1" runat="server" 
                        ControlToValidate="tbConfirmNewPassword" ErrorMessage="*" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="tbNewPassword" ControlToValidate="tbConfirmNewPassword" ErrorMessage="CompareValidator" ForeColor="#CC0000">Password Mismatch</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>
                <asp:Button ID="btnUpdatePassword" runat="server" OnClick="btnUpdatePassword_Click" Text="Save Changes" />
            </td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
    <p>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </p>
</asp:Content>
