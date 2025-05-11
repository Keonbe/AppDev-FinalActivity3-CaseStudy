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
    public partial class ShoppingCart : System.Web.UI.Page
    {

        private readonly string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;
                    AttachDbFilename=C:\Users\admin\Documents\c#\appdev\FINALS\CaseStudy\FinalActivity3\FinalActivity3_CaseStudy\FinalActivity3_CaseStudy\App_Data\Sales&InvSystemDB.mdf;
                    Integrated Security=True";
        ClassMethods methods = new ClassMethods();
        ClassComputations ClassComputations = new ClassComputations();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EmailAddress"] != null)
                {
                    BindCart();
                }
                else //Not login
                {
                    lblMessage.Text = "⚠️ Not logged in";
                    Response.AppendHeader("Refresh", "1;url=Login.aspx"); //Delays for lblMessage, Redirects
                }
            }
        }

        private void BindCart() //Bind table
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    lblMessage.Text = "⚠️ No items in cart.";
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "⚠️ Error: " + ex.Message;
                return;
            }


            // Get the user ID from the session
            int userId = Convert.ToInt32(Session["UserID"]);
            DataTable dt = methods.GetCartItems(userId);

            gvCartItems.DataSource = dt;
            gvCartItems.DataBind();

            // Compute totals
            double sub = 0;
            foreach (DataRow r in dt.Rows)
                sub += Convert.ToDouble(r["SubTotal"]);

            // Use helper to compute VAT
            double vat = ClassComputations.CalculateVAT(sub); // Use helper to compute VAT
            string memType = Session["MembershipType"].ToString(); //MembershipType
            double discRate = 0; //Discount Rate 
            int memIndex = memType == "Platinum" ? 0 
                          : memType == "Gold" ? 1
                          : 2; //MembershipType DiscountRate
            // Use helper to compute final amount (includes VAT & discount)
            double finalAmount = ClassComputations.CalculateFinalAmount(sub, memIndex);

            // Derive discount back out if you need to display it
            double discount = (sub + vat) - finalAmount;

            lblTotalAmount.Text = sub.ToString("C"); //TotalAmount
            lblVat.Text = vat.ToString("C"); //VAT
            lblDiscount.Text = discount.ToString("C"); //Discount
            lblFinalAmount.Text = finalAmount.ToString("C"); //TotalAmount
        }

        //Remove Item
        protected void gvCartItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveItem")
            {
                int cartId = Convert.ToInt32(e.CommandArgument);
                int userId = (int)Session["UserID"];
                methods.RemoveCartItem(userId, cartId);
                BindCart();
            }
        }

        //Checkout Btn = Transaction
        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            int userId = (int)Session["UserID"];
            DataTable dt = methods.GetCartItems(userId);

            // Compute subtotal via DataTable helper
            double sub = Convert.ToDouble(dt.Compute("SUM(SubTotal)", ""));

            // Compute VAT & final using helpers
            double vat = ClassComputations.CalculateVAT(sub);
            string memType = Session["MembershipType"].ToString();
            int memIndex = memType == "Platinum" ? 0
                          : memType == "Gold" ? 1
                          : 2;
            double finalTotal = ClassComputations.CalculateFinalAmount(sub, memIndex);

            // Pass calculated values to SQL
            // Call SP with all pre-computed values
            methods.ProcessCartCheckout(
                userId: userId,
                subTotal: (double)sub,
                memType: memType,
                discountRate: (double)((sub + vat - finalTotal) / sub),
                finalTotal: (double)finalTotal
            );

            BindCart();
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "✅ Order placed successfully!";
        }

        //Continue Shopping Btn = Redirect to ProductCatalog
        protected void btnContinueShopping_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/ProductCatalog.aspx");
        }
    }
}