using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FooderSupportService.backend
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_sign_in_Click(object sender, EventArgs e)
        {
            BackUserObj user = GetUserData(txt_user_name.Text);
            if (user != null)
            {
                bool isAuthenticated = Authenticate(user.UserQid, txt_password.Text);
                if (isAuthenticated)
                {
                    Session["backUserObj"] = user;
                    switch (user.UserRole)
                    {
                        case "officer": Response.Redirect("~/backend/officer.aspx");
                            break;
                        case "sectionHead":
                            Response.Redirect("~/backend/section_head.aspx");
                            break;
                        case "companyOfficer":
                            Response.Redirect("~/backend/company_officer.aspx");
                            break;
                        default: lbl_error_msg.Text = "لا يوجد لديك الصلاحيات الازمة للدخول";
                            break;
                    }
                }
                else
                {
                    lbl_error_msg.Text = "اسم المستخدم او كلمة المرور غير صحيح";
                }
            }
            else
            {
                lbl_error_msg.Text = "لا يوجد لديك الصلاحيات الازمة للدخول";
            }
          
        }
        public static bool Authenticate(string userName, string password)
        {
            bool isAuthenticated = false;
            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://mmaa.gov.qa", userName, password);
                object nativeObject = entry.NativeObject;
                isAuthenticated = true;
            }
            catch (DirectoryServicesCOMException ex) { }
            return isAuthenticated;
        }
        protected BackUserObj GetUserData(string userQid)
        {
            BackUserObj user = new BackUserObj();
            DataTable dataTable = new DataTable();
            try
            {
                OracleConnection oracleConnection = new OracleConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["FOODERSS_PRODConnString"].ConnectionString
                };
                OracleCommand oracleCommand = new OracleCommand();
                oracleCommand = oracleConnection.CreateCommand();
                oracleCommand.CommandText = "select ID,USER_QID,USER_ROLE from BACK_USER WHERE IS_ACTIVE=1 and USER_QID='"+ userQid + "'";
                oracleCommand.CommandType = CommandType.Text;
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    user.Id = Convert.ToInt32(dataTable.Rows[0]["ID"]);
                    user.UserQid = Convert.ToString(dataTable.Rows[0]["USER_QID"]);
                    user.UserRole = Convert.ToString(dataTable.Rows[0]["USER_ROLE"]);
                    return user;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}