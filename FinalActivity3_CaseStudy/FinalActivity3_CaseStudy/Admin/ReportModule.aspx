<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterpage.Master" AutoEventWireup="true" CodeBehind="ReportModule.aspx.cs" Inherits="FinalActivity3_CaseStudy.Admin.ReportModule" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Report: Sales Summary</h2>
    <p>&nbsp;</p>
    <div class="report-container">
        <div>
            <rsweb:ReportViewer ID="rvSales" runat="server" Width="100%" Height="600px">
              <LocalReport ReportPath="ReportSales.rdlc" />
            </rsweb:ReportViewer>
        </div>
    </div>
    <p>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </p>
</asp:Content>