using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using ClassLibrary;

namespace FinalActivity3_CaseStudy.Admin
{
    public partial class AddProduct : System.Web.UI.Page
    {
        AdminMethods classObj = new AdminMethods();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminEmailAddress"] != null) //login session check
                {
                    // User is logged in - enable buttons
                    tbProductID.Enabled = true;
                    tbProductName.Enabled = true;
                    tbPrice.Enabled = true;
                    tbStocks.Enabled = true;
                    btnAddProduct.Visible = true;

                }
                else //Not login
                {
                    tbProductID.Enabled = false;
                    tbProductName.Enabled = false;
                    tbPrice.Enabled = false;
                    tbStocks.Enabled = false;
                    btnAddProduct.Visible = false;
                    lblMessage.Text = "⚠️ Not logged in";
                    Response.AppendHeader("Refresh", "1;url=AdminLoginPage.aspx"); //Delays 2 seconds for lblMessage, Redirects

                }
            }
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            // Clear previous messages
            lblMessage.Text = "";

            // Validate required fields
            if (string.IsNullOrEmpty(tbProductID.Text) ||
                string.IsNullOrEmpty(tbProductName.Text) ||
                string.IsNullOrEmpty(tbPrice.Text) ||
                string.IsNullOrEmpty(tbStocks.Text))
            {
                lblMessage.Text = "Please fill in all required fields.";
                return;
            }

            // Validate numeric formats (PRice and Stocks)
            if (!double.TryParse(tbPrice.Text, out double basePrice))
            {
                lblMessage.Text = "Invalid price format. Please enter a valid decimal number.";
                return;
            }

            if (!int.TryParse(tbStocks.Text, out int stocksAvailable))
            {
                lblMessage.Text = "Invalid stock format. Please enter a whole number.";
                return;
            }

            //double basePrice = tbPrice.Text == "" ? 0 : Convert.ToDouble(tbPrice.Text);
            //int stocksAvailable = tbStocks.Text == "" ? 0 : Convert.ToInt32(tbStocks.Text);
            //classObj.AddNewProducts(tbProductID.Text, tbProductName.Text, basePrice, stocksAvailable);

            try
            {   // Attempt to add product
                classObj.AddNewProducts(
                    tbProductID.Text.Trim(),
                    tbProductName.Text.Trim(),
                    basePrice,
                    stocksAvailable
                );

                 // Success handling
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Product added successfully!";

                // Clear form fields
                tbProductID.Text = "";
                tbProductName.Text = "";
                tbPrice.Text = "";
                tbStocks.Text = "";
            }
            catch (InvalidOperationException ex)
            {
                // Duplicate ProductID
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message;
            }
            catch (Exception ex)
            {
                // Other errors
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
    }
}