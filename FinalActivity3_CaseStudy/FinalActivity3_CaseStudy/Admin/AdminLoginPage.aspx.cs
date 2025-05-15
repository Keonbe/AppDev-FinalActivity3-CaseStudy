using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary;


namespace FinalActivity3_CaseStudy.Admin
{
    public partial class AdminLoginPage : System.Web.UI.Page
    {
        ClassMethods classMethod = new ClassMethods();
        AdminMethods adminMethod = new AdminMethods();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the admin is already logged in
                if (Session["AdminEmailAddress"] != null)
                {
                    // If logged in already, Redirect to User Catalog Page
                    Response.AppendHeader("Refresh", "2;url=AdminDashboard.aspx");
                    lblMessage.Text = "Redirecting to Admin Page, You already have logged in.";
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
                Session["AdminEmailAddress"] = classMethod.AdminEmailAddressClass;

                Session["AdminEmailAddress"] = tbUsername.Text;

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