using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FooderSupportService.backend
{
    public partial class Backend : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["backUserObj"] != null)
            {
                BackUserObj userObj = (BackUserObj)Session["backUserObj"];
                lbl_adUserName.Text = userObj.UserQid+", "+userObj.UserRole;
            }
            else
            {
                Response.Redirect("~/backend/login.aspx");
            }
        }

        protected void lb_signOut_Click(object sender, EventArgs e)
        {
            Session["backUserObj"] = null;
            Response.Redirect("~/backend/login.aspx");
        }
    }
}