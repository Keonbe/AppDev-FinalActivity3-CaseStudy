using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using ClassLibrary;
using Microsoft.Identity.Client;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;

namespace FinalActivity3_CaseStudy.Admin
{
    public partial class ReportModule : System.Web.UI.Page
    {
        ClassMethods objMethod = new ClassMethods();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadReport();
                }
            }
            catch (SqlException sqlEx)
            {
                lblMessage.Text = $"Database Error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Application Error: {ex.Message}";
            }
        }

        public void LoadReport()
        {
            rvSales.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet1", objMethod.DisplaySalesSummary().Tables["myTable"]);
            rvSales.LocalReport.ReportPath = Server.MapPath("~/Admin/ReportSales.rdlc");
            rvSales.LocalReport.DataSources.Add(rds);
            rvSales.LocalReport.Refresh();
        }
    }
}