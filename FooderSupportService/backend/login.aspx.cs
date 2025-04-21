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
            List<string> users = GetAllUsers();
            bool isAuthenticated = Authenticate(txt_user_name.Text, txt_password.Text);
            if (users.Contains(txt_user_name.Text))
            {
                if (isAuthenticated)
                {
                    Session["EmpUserName"] = txt_user_name.Text;
                    Response.Redirect("~/backend/requests.aspx");
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
        protected List<string> GetAllUsers()
        {
            List<string> users = new List<string>();
            DataTable dataTable = new DataTable();
            try
            {
                OracleConnection oracleConnection = new OracleConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["FOODERSS_PRODConnString"].ConnectionString
                };
                OracleCommand oracleCommand = new OracleCommand();
                oracleCommand = oracleConnection.CreateCommand();
                oracleCommand.CommandText = "select USER_NAME from BACK_UESRS";
                oracleCommand.CommandType = CommandType.Text;
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    users.Add(dataRow[0].ToString());
                }

            }
            catch (Exception ex)
            {
                return null;
            }
            return users;
        }
    }
}