using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary;

namespace FinalActivity3_CaseStudy.User
{
    public partial class ProductPage : System.Web.UI.Page
    {
        private string connStr =
            @"Data Source=(LocalDB)\MSSQLLocalDB;
              AttachDbFilename=|DataDirectory|\SalesInvSystemDB.mdf;
              Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()
        {
            // Call your stored procedure that returns all products
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand("GetAllProducts", conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                da.Fill(dt);
            }

            gvProductTable.DataSource = dt;
            gvProductTable.DataBind();
        }

        protected void gvProductTable_RowCommand(object sender, GridViewCommandEventArgs e) //Grant Approve Loans
        {
            if (e.CommandName == "cmdAddtoCart")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvProductTable.Rows[rowIndex];
                int productID = Convert.ToInt32(gvProductTable.DataKeys[rowIndex].Value);

                ClassMethods classMethods = new ClassMethods();
                classMethods.AddToCartMethod(productID, newStatus);
            }
        }

        protected void btnLogoutAdmin_Click(object sender, EventArgs e)
        {
            Session.Clear();        // Clear session data
            Session.Abandon();      // Ends session
            Response.Redirect("LoginPage.aspx");
        }

    }
}