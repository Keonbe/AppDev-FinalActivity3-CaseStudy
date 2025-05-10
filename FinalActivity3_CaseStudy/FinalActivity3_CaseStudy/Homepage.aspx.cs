using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalActivity3_CaseStudy
{
    public partial class Homepage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // any initialization logic
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/Login.aspx"); //Redirects to User Login
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/Regisration.aspx"); //Redirects to User Regisration
        }
    }
}