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
            if (string.IsNullOrEmpty(tbProductID.Text) || string.IsNullOrEmpty(tbProductName.Text))
            {
                lblMessage.Text = "Please fill in all fields.";
                return;
            }

            double basePrice = tbPrice.Text == "" ? 0 : Convert.ToDouble(tbPrice.Text);
            int stocksAvailable = tbStocks.Text == "" ? 0 : Convert.ToInt32(tbStocks.Text);
            classObj.AddNewProducts(tbProductID.Text, tbProductName.Text, basePrice, stocksAvailable);

            try
            {
                classObj.AddNewProducts(
                    tbProductID.Text.Trim(),
                    tbProductName.Text.Trim(),
                    basePrice,
                    stocksAvailable
                );

                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Product added successfully!";
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