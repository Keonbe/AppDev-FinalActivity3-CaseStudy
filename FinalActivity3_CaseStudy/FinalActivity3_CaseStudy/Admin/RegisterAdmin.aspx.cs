using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary;

namespace FinalActivity3_CaseStudy.Admin
{
    public partial class RegisterAdmin : System.Web.UI.Page
    {
        AdminMethods classObj = new AdminMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminEmailAddress"] != null) //login session check
                {
                    // User is logged in - enable buttons
                    tbName.Enabled = true;
                    tbEmailAddress.Enabled = true;
                    tbPassword.Enabled = true;
                    ddlMembershipType.Enabled = true;
                    btnRegister.Enabled = true;
                    lblMessage.Text = "You are logged in as " + Session["AdminEmailAddress"].ToString();

                }
                else //Not login
                {
                    tbName.Enabled = false; //disable buttons
                    tbEmailAddress.Enabled = false;
                    tbPassword.Enabled = false;
                    ddlMembershipType.Enabled = false;
                    btnRegister.Visible = false;
                    lblMessage.Text = "⚠️ Not logged in";
                    Response.AppendHeader("Refresh", "1;url=AdminLogin.aspx");

                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            classObj.SaveRegisrationAdmin(tbName.Text, tbEmailAddress.Text, tbPassword.Text, ddlMembershipType.SelectedValue); //Registers an admin user
            Session["AdminEmailAddress"] = tbName.Text;
            lblMessage.Text = "Registration successful! You can now log in.";
        }
    }
}