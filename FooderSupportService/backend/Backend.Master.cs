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
            if (Session["EmpUserName"] != null)
            {
                lbl_adUserName.Text = Session["EmpUserName"].ToString();
            }
            else
            {
                Response.Redirect("~/backend/login.aspx");
            }
        }

        protected void lb_signOut_Click(object sender, EventArgs e)
        {
            Session["EmpUserName"] = null;
            Response.Redirect("~/backend/login.aspx");
        }

       

        protected void lb_meetings_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/meetings.aspx");
        }
    }
}