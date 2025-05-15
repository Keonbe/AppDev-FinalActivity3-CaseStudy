using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace FinalActivity3_CaseStudy.Admin
{
    public partial class AdminLogout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is already logged in
                if (Session["AdminEmailAddress"] == null)
                {
                    // If not logged in, Redirect and Display message
                    lblMessage.Text = "⚠️ Not logged in - Redirecting to Login Page";
                    btnLogout.Visible = false; // If not login, Hide the logout button
                    Response.AppendHeader("Refresh", "2;url=AdminLoginPage.aspx");
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();        // Clear session data
            Session.Abandon();      // Ends session
            lblMessage.Text = "Account Successfully Logged Out. Redirecting to Home Page...";
            Response.AppendHeader("Refresh", "2;url=/HomePage/LandingPage.aspx"); 
        }
    }
}