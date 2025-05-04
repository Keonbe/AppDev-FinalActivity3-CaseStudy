<%@ Page Title="" Language="C#" MasterPageFile="~/User/MainMasterpage.Master" AutoEventWireup="true" CodeBehind="UserLogout.aspx.cs" Inherits="FinalActivity3_CaseStudy.User.UserLogout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        Logout</p>
    <table class="auto-style1">
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Confirm Logout?</td>
            <td>
                <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
            </td>
        </tr>
    </table>
    <p>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </p>
    <p>
        &nbsp;</p>
</asp:Content>
