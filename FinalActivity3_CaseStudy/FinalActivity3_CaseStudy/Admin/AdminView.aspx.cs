using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace FinalActivity3_CaseStudy.Admin
{
    public partial class TestAdminView : System.Web.UI.Page
    {

        private readonly string connStr = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString; // Remove hardcoded connection string

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGrid(rblViewSelector.SelectedValue); //Load the grid with the default value(rblViewSelector.SelectedValue - View Products)
                if (Session["AdminEmailAddress"] != null) //login session check
                {
                    // User is logged in - enable buttons
                    lblMessage.Text = "Viewing Database Tables as " + Session["AdminEmailAddress"].ToString();

                }
                else //Not login
                {
                    lblMessage.Text = "⚠️ Not logged in";
                    Response.AppendHeader("Refresh", "1;url=AdminLogin.aspx"); //Delays 2 seconds for lblMessage, Redirects

                }
            }
        }

        protected void rblViewSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid(rblViewSelector.SelectedValue); //Each time SelectedIndexChanged, reload the grid with the selected value(rblViewSelector.SelectedValue; Stored Procedure name)
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadGrid(rblViewSelector.SelectedValue);
        }
        private void LoadGrid(string storedProcName)
        {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(storedProcName, conn)) //StoredProc name @ rblViewSelector
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open(); 
                da.Fill(dt);
            }

            gvAdmin.DataSource = dt;
            gvAdmin.DataBind();
        }
    }
}