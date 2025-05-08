using System;
using System.Collections.Generic;
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
        }
    }
}