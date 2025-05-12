<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterpage.Master" AutoEventWireup="true" CodeBehind="ReportModule.aspx.cs" Inherits="FinalActivity3_CaseStudy.Admin.ReportModule" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2> Report</h2>
<p> &nbsp;</p>
<p> </p>
<rsweb:ReportViewer ID="ReportViewer1" runat="server">
</rsweb:ReportViewer>
</asp:Content>
