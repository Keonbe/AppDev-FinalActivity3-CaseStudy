using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary;

namespace FinalActivity3_CaseStudy.User
{
    public partial class OrderHistory : System.Web.UI.Page
    {
        private string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;
                    AttachDbFilename=C:\Users\admin\Documents\c#\appdev\FINALS\CaseStudy\FinalActivity3\FinalActivity3_CaseStudy\FinalActivity3_CaseStudy\App_Data\Sales&InvSystemDB.mdf;
                    Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EmailAddress"] != null) //login session check
                {
                    // User is logged in - enable buttons
                    lblMessage.Text = "Viewing your order history as " + Session["EmailAddress"].ToString();

                }
                else //Not login
                {
                    lblMessage.Text = "⚠️ Not logged in";
                    Response.AppendHeader("Refresh", "1;url=AdminLogin.aspx"); //Delays 2 seconds for lblMessage, Redirects

                }
            }
        }

        private void LoadGrid(string storedProcName)
        {
            storedProcName = "GetOrderHistoryUser"; //Stored Procedure name
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(storedProcName, conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                da.Fill(dt);
            }

            gvOrderHistory.DataSource = dt;
            gvOrderHistory.DataBind();
        }
    }
}