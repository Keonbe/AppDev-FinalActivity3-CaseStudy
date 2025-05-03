using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary;

namespace FinalActivity3_CaseStudy
{
    public partial class Regisration : System.Web.UI.Page
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

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            classObj.SaveRecordRegisration(tbName.Text, tbEmailAddress.Text, tbPassword.Text, ddlMembershipType.SelectedValue);
        }
    }
}