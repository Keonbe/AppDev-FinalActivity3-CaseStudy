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
        private readonly string  connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;
                    AttachDbFilename=C:\Users\admin\Documents\c#\appdev\FINALS\CaseStudy\FinalActivity3\FinalActivity3_CaseStudy\FinalActivity3_CaseStudy\App_Data\Sales&InvSystemDB.mdf;
                    Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                if (Session["EmailAddress"] == null)
                {
                    lblMessage.Text = "⚠️ Not logged in";
                    Response.AppendHeader("Refresh", "1;url=/HomePage/newLogin.aspx"); //Delays for lblMessage, Redirects
                }
            }
        }

        private void BindGrid() //Bind gv, Display value from table using "GetAllProducts" SP. 
        {
            ClassMethods methods = new ClassMethods();
            gvProductTable.DataSource = methods.GetAllProducts();
            gvProductTable.DataBind();
        }

        protected void gvProductTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName != "AddToCart") return;

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvProductTable.Rows[rowIndex];

                var qtyBox = (TextBox)row.FindControl("txtQty"); // Find quantity controls
                var rvQty = (RangeValidator)row.FindControl("rvQty");

                // Server-side validation
                Page.Validate();
                if (!rvQty.IsValid)
                {
                    // Optionally display a message
                    return;
                }

                // Parse values
                string productId = gvProductTable.DataKeys[rowIndex].Value.ToString();
                int quantity = int.Parse(qtyBox.Text);
                int stocks = Convert.ToInt32(gvProductTable.DataKeys[rowIndex].Values["Stocks"]);

                if (quantity < 1 || quantity > stocks)
                    return; // Extra check

                int userId = (int)Session["UserID"];  // Get user ID from session
                new ClassMethods().AddToCartMethod(userId, productId, quantity); // Add to cart via class library

                // Optional: feedback
                lblMessage.Text = "Item added to cart!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

    }
}