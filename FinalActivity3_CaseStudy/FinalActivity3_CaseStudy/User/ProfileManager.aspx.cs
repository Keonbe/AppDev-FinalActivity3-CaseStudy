using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary;

namespace FinalActivity3_CaseStudy.User
{
    public partial class ProfileManager : System.Web.UI.Page
    {
        ClassMethods classMethods = new ClassMethods();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EmailAddress"] != null) //login session check
                {
                    // User is logged in - enable buttons
                    tbCurrentPassword.Enabled = true;
                    tbNewPassword.Enabled = true;
                    tbConfirmNewPassword.Enabled = true;
                    btnUpdatePassword.Enabled = true;

                }
                else //Not login
                {
                    tbCurrentPassword.Enabled = false; //Disable buttons and icons
                    tbNewPassword.Enabled = false;
                    tbConfirmNewPassword.Enabled = false;
                    btnUpdatePassword.Enabled = false;
                    btnUpdatePassword.Visible = false;
                    lblMessage.Text = "⚠️ Not logged in";
                    Response.AppendHeader("Refresh", "1;url=/HomePage/newLogin.aspx");
                }
            }
        }

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            string password = tbNewPassword.Text;
            if (password.Length < 8)     // Password length validation error
            {
                lblPlaceHolder.Text = "Error: Password must be at least 8 characters long.";
                lblPlaceHolder.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string emailAddress = Session["EmailAddress"].ToString();
            string passWord = Session["Password"].ToString();
            classMethods.UpdateProfileManager(emailAddress, passWord, tbNewPassword.Text.Trim());
        }
    }
}