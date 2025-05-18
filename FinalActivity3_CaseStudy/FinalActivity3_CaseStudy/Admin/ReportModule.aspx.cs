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

        /*
        public void LoadReport()
        {
            // 1) Point directly to the RDLC in your virtual folder:
            string reportPath = Server.MapPath("~/Admin/report/ReportSalesSummary.rdlc");

            // 2) Clear any old data sources:
            rvSales.LocalReport.DataSources.Clear();

            // 3) Fetch your DataTable; 'SalesSummary' matches the table name you filled:
            var ds = objMethod.DisplaySalesSummary();
            var dt = ds.Tables["SalesSummary"];

            // 4) Create your ReportDataSource: 
            //    the first parameter must EXACTLY match the DataSet name inside the RDLC
            var rds = new ReportDataSource("SalesSummaryDataSet", dt);

            // 5) Assign the file path you already resolved:
            rvSales.LocalReport.ReportPath = reportPath;

            // 6) Add and refresh
            rvSales.LocalReport.DataSources.Add(rds);
            rvSales.LocalReport.Refresh();
        }

             public void LoadReport()
        {
            string reportPath = Server.MapPath("~/Admin/report/ReportSalesSummary.rdlc");
            rvSales.LocalReport.DataSources.Clear();

            // 1. Get the data
            var ds = objMethod.DisplaySalesSummary();
            var dt = ds.Tables["SalesSummary"]; // Ensure this matches your DataTable name

            // 2. Match the dataset name EXACTLY to the RDLC
            var rds = new ReportDataSource(
                "SalesSummaryDataSet", // Must match the RDLC dataset name
                dt
            );

            // 3. Set the report path
            rvSales.LocalReport.ReportPath = Server.MapPath(reportPath);

            // 4. Add the data source
            rvSales.LocalReport.DataSources.Add(rds);
            rvSales.LocalReport.Refresh();
        }

        public void LoadReport()
        {
            rvSales.LocalReport.DataSources.Clear();
            // 1st param = RDLC Dataset Name
            // 2nd param = the table name you filled above
            var rds = new ReportDataSource(
                "SalesSummaryByProductDataSet",
                objMethod.DisplaySalesSummary().Tables["SalesSummaryByProductDataTable"]
            );
            rvSales.LocalReport.DataSources.Add(rds);
            rvSales.LocalReport.ReportPath = Server.MapPath("~/Admin/report/ReportSalesSummary.rdlc");
            rvSales.LocalReport.Refresh();
        }
        /*
        public void LoadReport()
        {
            rvSales.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("SalesSummaryByProductDataSet", objMethod.DisplaySalesSummary().Tables["myTable"]);
            rvSales.LocalReport.ReportPath = Server.MapPath("~/Admin/report/ReportSalesSummary.rdlc");
            rvSales.LocalReport.DataSources.Add(rds);
            rvSales.LocalReport.Refresh();
        }

                public void LoadReport()
        {
            try
            {
                rvSales.LocalReport.DataSources.Clear();

                // Get the CORRECT table name from dataset
                var ds = objMethod.DisplaySalesSummary();
                var dt = ds.Tables["SalesSummary"]; // Changed from "myTable" to "SalesSummary"

                // Verify dataset name matches RDLC exactly (check in .rdlc file)
                var rds = new ReportDataSource(
                    "SalesSummaryByProductDataSet", // Must match RDLC DataSet name
                    dt
                );

                // Verify physical path exists
                string reportPath = Server.MapPath("~/Admin/report/ReportSalesSummary.rdlc");
                if (!System.IO.File.Exists(reportPath))
                {
                    throw new FileNotFoundException("Report file missing: " + reportPath);
                }
                rvSales.LocalReport.ReportPath = reportPath;

                rvSales.LocalReport.DataSources.Add(rds);
                rvSales.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                // Handle errors properly
                lblMessage.Text = $"Report Error: {ex.Message}";
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
        }
        */

    }
    
}