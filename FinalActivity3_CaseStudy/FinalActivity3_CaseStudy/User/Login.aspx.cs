using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary;

namespace FinalActivity3_CaseStudy
{
    public partial class Login : System.Web.UI.Page
    {
        ClassMethods classMethod = new ClassMethods();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                // Check if the user is already logged in
                if (Session["EmailAddress"] != null)
                {
                    // If logged in already, Redirect to User Catalog Page
                    Response.AppendHeader("Refresh", "2;url=ProductCatalog.aspx"); //Delays 2 seconds for lblMessage, Redirects
                    lblMessage.Text = "Redirecting to Product Page, You already have logged in.";
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string email = tbEmailAddress.Text.Trim();
            string pwd = tbPassword.Text.Trim();

            if (classMethod.CheckLogin(email, pwd, out int userId))
            {
                // Login successful – store both Email and UserID
                Session["EmailAddress"] = email;
                Session["UserID"] = userId;

                Response.Redirect("ProductCatalog.aspx");
                return;
            }
            else
            {
                lblMessage.Text = "Invalid Username or Password";
                return;
            }
        }

    }
}