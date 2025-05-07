using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalActivity3_CaseStudy.Admin
{
    public partial class TestAdminView : System.Web.UI.Page
    {
        private string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;
                    AttachDbFilename=C:\Users\admin\Documents\c#\appdev\FINALS\CaseStudy\FinalActivity3\FinalActivity3_CaseStudy\FinalActivity3_CaseStudy\App_Data\Sales&InvSystemDB.mdf;
                    Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadGrid(rblViewSelector.SelectedValue);
        }

        protected void rblViewSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid(rblViewSelector.SelectedValue);
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadGrid(rblViewSelector.SelectedValue);
        }
        private void LoadGrid(string storedProcName)
        {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(storedProcName, conn))
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