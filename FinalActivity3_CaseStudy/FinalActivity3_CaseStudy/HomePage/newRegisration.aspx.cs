using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace FinalActivity3_CaseStudy.HomePage
{
    public partial class newRegisration : System.Web.UI.Page
    {
        ClassMethods classObj = new ClassMethods();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //lblMessage.Text = ddlMembershipType.SelectedValue; //Debug ddlMembershipType
                //Add "If user registered, message then redirect to login page"
            }
        }

        protected void cvPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // server‐side check for minimum length
            args.IsValid = args.Value?.Length >= 8;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // run all validators
            if (!Page.IsValid)
                return;

            try
            {
                // Trim inputs to avoid accidental spaces
                var name = tbName.Text.Trim();
                var email = tbEmailAddress.Text.Trim();
                var pwd = tbPassword.Text;
                var member = ddlMembershipType.SelectedValue;

                // Save (throws InvalidOperationException on duplicates)
                classObj.SaveRecordRegistration(name, email, pwd, member);

                // Success feedback & clear form
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Registration successful!";
                //Response.Redirect("~/User/ProductCatalog.aspx");

                ClearForm();
            }
            catch (InvalidOperationException dupEx)
            {
                // Duplicate email
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = dupEx.Message;
            }
            catch (Exception ex)
            {
                // Other errors
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Error during registration: " + ex.Message;
            }
        }

        private void ClearForm()
        {
            tbName.Text = "";
            tbEmailAddress.Text = "";
            tbPassword.Text = "";
            tbConfirmPassword.Text = "";
            ddlMembershipType.SelectedIndex = 0;
        }
    }
}