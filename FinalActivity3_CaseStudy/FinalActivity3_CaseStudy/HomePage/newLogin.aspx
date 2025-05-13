<%@ Page Title="" Language="C#" MasterPageFile="~/HomePage/LandingPage.Master" AutoEventWireup="true" CodeBehind="newLogin.aspx.cs" Inherits="FinalActivity3_CaseStudy.HomePage.newLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h2>Login Page</h2>
        <br />
        <table>
            <tr>
                <td>Email Address:</td>
                <td>
                    <asp:TextBox ID="tbEmailAddress" runat="server" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                        ControlToValidate="tbEmailAddress" ErrorMessage="*" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Password:</td>
                <td>
                    <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                        ControlToValidate="tbPassword" ErrorMessage="*" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" 
                        BackColor="#3366CC" ForeColor="White" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <b><a href="/Admin/AdminLogin.aspx">Are you admin?</a></b>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
