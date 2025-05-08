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

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            classObj.SaveRegisrationAdmin(tbName.Text, tbEmailAddress.Text, tbPassword.Text, ddlMembershipType.SelectedValue);
            Session["AdminEmailAddress"] = tbName.Text;
            lblMessage.Text = "Registration successful! You can now log in.";
        }
    }
}