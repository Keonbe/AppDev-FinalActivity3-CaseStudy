using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary;
using System.Configuration;

namespace FinalActivity3_CaseStudy.User
{
    public partial class OrderHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EmailAddress"] != null) //login session check
                {
                    // User is logged in - enable buttons
                    lblMessage.Text = "Viewing your order history as " + Session["EmailAddress"].ToString();

                    // Load orders for this user
                    if (Session["UserID"] != null)
                    {
                        int userId = Convert.ToInt32(Session["UserID"]);
                        LoadOrderHistory(userId);
                    }
                    else
                    {
                        lblMessage.Text += "<br>Unable to load order history - User ID not found.";
                    }
                }
                else //Not login
                {
                    lblMessage.Text = "⚠️ Not logged in";
                    Response.AppendHeader("Refresh", "1;url=Login.aspx"); //Delays for lblMessage, Redirects
                }

            }
        }
        private void LoadOrderHistory(int userId)
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;
                    AttachDbFilename=C:\Users\admin\Documents\c#\appdev\FINALS\CaseStudy\FinalActivity3\FinalActivity3_CaseStudy\FinalActivity3_CaseStudy\App_Data\Sales&InvSystemDB.mdf;
                    Integrated Security=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("GetOrderHistoryUser", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        conn.Open();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            gvOrderHistory.CssClass = "grid-view";
                            gvOrderHistory.DataSource = dt;
                            gvOrderHistory.DataBind();
                        }
                        else
                        {
                            lblMessage.Text += "<br>No transaction history found for your account.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error and show a user-friendly message
                lblMessage.Text = "An error occurred while retrieving your transaction history: " + ex.Message;
                System.Diagnostics.Debug.WriteLine("Error in LoadOrderHistoryUsingStoredProcedure: " + ex.Message);
            }
        }

    }
}