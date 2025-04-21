using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FooderSupportService
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lb_signOut_Click(object sender, EventArgs e)
        {
            Session["UserQid"] = null;
            Session["UserMobile"] = null;
            Session["UserName"] = null;
            Response.Redirect("~/SignIn.aspx");
        }
    }
}