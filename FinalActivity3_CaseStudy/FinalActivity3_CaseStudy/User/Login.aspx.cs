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

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            // Check user login
            if (classMethod.CheckLogin(tbEmailAddress.Text, tbPassword.Text))
            {
                // Login successful - set session variables
                Session["EmailAddress"] = classMethod.EmailAddressClass; //IDK IF THIS NEED TO BE INVERTED DELARATION
                Session["Password"] = classMethod.PasswordClass;

                //Session["EmailAddress"] = tbUsername.Text;
                Session["Password"] = tbPassword.Text;

                // Redirect to User Catalog Page
                Response.Redirect("ProductPage.aspx");
                return;
            }
            else
            {
                // Login failed - show error message
                lblMessage.Text = "Invalid Username or Password";
                return;
            }
        }
    }
}