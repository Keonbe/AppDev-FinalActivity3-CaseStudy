using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalActivity3_CaseStudy.User
{
    public partial class UserLogout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is already logged in
                if (Session["EmailAddress"] == null)
                {
                    // If not logged in, Redirect and Display message
                    lblMessage.Text = "⚠️ Not logged in - Redirecting to Login Page";
                    btnLogout.Visible = false; // If not login, Hide the logout button
                    Response.AppendHeader("Refresh", "1;url=/HomePage/newLogin.aspx");
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();        // Clear session data
            Session.Abandon();      // Ends session
            lblMessage.Text = "Account Successfully Logged Out. Redirecting to Home Page...";
            Response.AppendHeader("Refresh", "2;url=/HomePage/LandingPage.aspx"); //Redirect to homepage.aspx
            // Use "/webform.aspx" or "~/folder/webform.aspx" for specific webform @ admin or user
        }
    }
}