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
                    Response.AppendHeader("Refresh", "1;url=/HomePage/newLogin.aspx"); //Delays for lblMessage, Redirects
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

                int userId = Convert.ToInt32(Session["UserID"]);
                DataTable dt = methods.GetCartItems(userId);

                //SRP column if missing
                if (!dt.Columns.Contains("SRP"))
                    dt.Columns.Add("SRP", typeof(double));

                //Fill SRP and compute SubTotal
                double sub = 0;
                foreach (DataRow r in dt.Rows)
                {
                    double srp = ClassComputations.CalculateSRP(Convert.ToDouble(r["Price"]));
                    int qty = Convert.ToInt32(r["Quantity"]);
                    double srpSubtotal = srp * qty;
                    r["SRP"] = srp;
                    sub += srpSubtotal;
                }

                //Bind to GRID
                gvCartItems.DataSource = dt;
                gvCartItems.DataBind();

                // Get membership from session
                string memType = Session["MembershipType"]?.ToString() ?? "";
                double vat = ClassComputations.CalculateVAT(sub);
                double finalAmount = ClassComputations.CalculateFinalAmount(sub, memType);
                double discount = (sub + vat) - finalAmount;

                // Display values
                lblTotalAmount.Text = sub.ToString("C");
                lblVat.Text = vat.ToString("C");
                lblDiscount.Text = discount.ToString("C");
                lblFinalAmount.Text = finalAmount.ToString("C");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "⚠️ Error: " + ex.Message;
            }
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

            // ← NEW: if no rows, bail out
            if (dt == null || dt.Rows.Count == 0)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "⚠️ Your cart is empty. Add some products before checking out.";
                return;
            }

            // 1) Compute subtotal via DataTable helper
            //double sub = Convert.ToDouble(dt.Compute("SUM(SubTotal)", ""));
            double sub = 0;
            foreach (DataRow r in dt.Rows)
            {
                double srp = ClassComputations.CalculateSRP(Convert.ToDouble(r["Price"]));
                int qty = Convert.ToInt32(r["Quantity"]);
                sub += srp * qty;
            }



            // 2) Get membership
            string memType = Session["MembershipType"]?.ToString() ?? "";

            // 3) Compute VAT, discount, and final total using your helpers
            double vat = ClassComputations.CalculateVAT(sub);
            double discount = ClassComputations.CalculateDiscount(sub, memType);
            double finalTotal = ClassComputations.CalculateFinalAmount(sub, memType);

            // 4) Compute discountRate as a fraction of the subtotal
            //    (so e.g. 0.15 for 15%)
            double discountRate = sub > 0 ? (discount / sub) : 0;

            // Pass calculated values to SQL
            // Call SP with all pre-computed values
            methods.ProcessCartCheckout(
                userId: userId,
                subTotal: sub,
                memType: memType,
                discountRate: discountRate,
                finalTotal: finalTotal
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