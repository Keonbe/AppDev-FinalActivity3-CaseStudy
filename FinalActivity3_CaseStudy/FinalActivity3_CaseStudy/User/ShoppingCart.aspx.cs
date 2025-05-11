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

        private void BindCart()
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

            double vat = sub * 0.10;
            string memType = Session["MembershipType"].ToString();
            double discRate = 0;
            if (sub >= 10000)
            {
                switch (memType)
                {
                    case "Platinum": discRate = 0.15; break;
                    case "Gold": discRate = 0.10; break;
                    case "Silver": discRate = 0.05; break;
                }
            }
            double discount = sub * discRate;
            double finalAmount = (sub + vat) - discount;

            lblTotalAmount.Text = sub.ToString("C");
            lblVat.Text = vat.ToString("C");
            lblDiscount.Text = discount.ToString("C");
            lblFinalAmount.Text = finalAmount.ToString("C");
        }

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

        protected void btnContinueShopping_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/ProductCatalog.aspx");
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            int userId = (int)Session["UserID"];

            methods.ProcessCartCheckout(userId);
            BindCart();

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "✅ Your order has been successfully placed!";
        }
    }
}