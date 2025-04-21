using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FooderSupportService
{
    public partial class Success : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserQid"] != null)
            {
                lbl_date.Text = DateTime.Now.ToString("dd-MM-yyyy");
                lbl_requestRefrance.Text = Request.QueryString[0].ToString();
            }
            else
            {
                Response.Redirect("~/Signin.aspx");
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("/MyRequests.aspx");
        }
    }
}