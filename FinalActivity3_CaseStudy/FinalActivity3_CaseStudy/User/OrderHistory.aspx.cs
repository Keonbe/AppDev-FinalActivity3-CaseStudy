﻿using System;
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

        private readonly string connStr = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString; // Remove hardcoded connection string 

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
                    Response.AppendHeader("Refresh", "1;url=/HomePage/newLogin.aspx"); //Delays for lblMessage, Redirects
                }

            }
        }
        private void LoadOrderHistory(int userId)
        {
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

        /// <summary>
        /// Fired when the user clicks "View Details" on an order.
        /// </summary>
        protected void gvOrderHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1) Re-load the history so DataKeys are fresh
            int userId = Convert.ToInt32(Session["UserID"]);
            LoadOrderHistory(userId);

            // 2) Now grab the newly selected key
            int transId = (int)gvOrderHistory.SelectedDataKey.Value;
            LoadOrderDetails(transId);
        }

        protected void gvOrderHistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "ViewDetails") return;

            // e.CommandSource is the Button; find its row
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int index = row.RowIndex;

            // DataKeys will be populated because the grid was bound already
            int transId = (int)gvOrderHistory.DataKeys[index].Value;
            LoadOrderDetails(transId);
        }

        private void LoadOrderDetails(int transactionId)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand("GetTransactionDetails", conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TransactionID", transactionId);
                conn.Open();
                da.Fill(dt);
            }

            gvOrderDetails.DataSource = dt;
            gvOrderDetails.DataBind();
        }

    }
}