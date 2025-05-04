using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary;

namespace FinalActivity3_CaseStudy
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        ClassMethods classMethod = new ClassMethods();
        AdminMethods adminMethod = new AdminMethods();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is already logged in
                if (Session["EmailAddress"] != null)
                {
                    // If logged in already, Redirect to User Catalog Page
                    Response.AppendHeader("Refresh", "2;url=AdminDashboard.aspx"); 
                    lblMessage.Text = "Redirecting to Product Page, You already have logged in.";
                }
            }
        }

        protected void btnAdminLogin_Click(object sender, EventArgs e)
        {
            string email = tbUsername.Text;
            string passwd = tbPassword.Text;

            // Check user login
            if (adminMethod.CheckAdmin(email, passwd))
            {
                // Login successful - set session variables
                Session["IsAdmin"] = "0";
                Session["EmailAddress"] = classMethod.AdminEmailAddressClass;
                Session["Password"] = classMethod.AdminPasswordClass;

                Session["EmailAddress"] = tbUsername.Text;
                Session["Password"] = tbPassword.Text;

                Response.Redirect("AdminDashboard.aspx");
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