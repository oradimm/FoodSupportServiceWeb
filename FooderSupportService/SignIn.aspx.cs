using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FooderSupportService.IdentityManagmentService;
using FooderSupportService.MobileVerification;
using FooderSupportService.SMSWebService;

namespace FooderSupportService
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //txt_qid.Text = "28840000698";
            //txt_mobile.Text = "74000797";  
        }
        protected void btn_send_otp_Click(object sender, EventArgs e)
        {
            btn_send_otp.Visible = false;
            btn_sign_in.Visible = true;
            txt_qid.Enabled = false;
            txt_mobile.Enabled = false;
            txt_otp.Enabled = true;
            if (CheckQidMobile(txt_qid.Text, txt_mobile.Text))
            {
                string otp = CreateOTP(4);
                //txt_otp.Text = otp;
                Session["otp"] = otp;
                string smsBody = "رمز التحقق هو: "+otp;
                UtilityHelper.SendSms(txt_mobile.Text, smsBody);
                //UtilityHelper.SendSms(txt_mobile.Text, smsBody);
            }
            else
            {
                btn_send_otp.Visible = true;
                btn_sign_in.Visible = false;
                txt_qid.Enabled = true;
                txt_mobile.Enabled = true;
                txt_otp.Enabled = false;
                lbl_error_msg.Text = "تعذر التحقق من البيانات, يرجى المحاولة مرة أخرى";
            }
        }
        protected void btn_sign_in_Click(object sender, EventArgs e)
        {
            if (txt_otp.Text == Session["otp"].ToString())
            {
                Session["UserQid"] = txt_qid.Text;
                Session["UserMobile"] = txt_mobile.Text;
                Session["UserName"] = GetUserNameByQID(txt_qid.Text);
                int userId = UpdateUserProfile(txt_qid.Text, Session["UserName"].ToString(), txt_mobile.Text);

                ////test
                //Session["UserQid"] = "28840000698";
                //Session["UserMobile"] = "74000797";
                //Session["UserName"] = GetUserNameByQID("28840000698");
                //int userId = UpdateUserProfile("28840000698", Session["UserName"].ToString(), "74000797");

                if (userId != -1)
                {
                    DateTime lastActivity = UtilityHelper.GetLastActivityForUser(userId);
                    //if (lastActivity.AddMinutes(10) < DateTime.Now)
                    if (lastActivity.AddMinutes(0) < DateTime.Now)
                    {
                        Session["UserId"] = userId;
                        UtilityHelper.UpdateLastActivityForUser(userId);
                        Response.Redirect("~/MyRequests.aspx");
                    }
                    else
                    {
                        lbl_error_msg.Text = "لا يمكن استخدام في الوقت الحالي, يرجى المحاولة بعد 10 دقائق";
                    }
                }
                else
                {
                    lbl_error_msg.Text = "حدث خطأ فني, يرجى التواصل مع خدمة العملاء على 184";
                }
            }
            else
            {
                lbl_error_msg.Text = "رمز التحقق غير صحيح, يرجى المحاولة مرة اخرى";
            }
        }
        public int UpdateUserProfile(string UserQID, string UserName, string UserMobile)
        {
            int userId = -1;
            try
            {
                DataTable dataTable = new DataTable();
                OracleConnection oracleConnection = new OracleConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["FOODERSS_PRODConnString"].ConnectionString
                };
                OracleCommand oracleCommand = new OracleCommand();
                oracleCommand = oracleConnection.CreateCommand();
                oracleCommand.CommandText = "select * from USER_PROFILE where QID=:PQID";
                oracleCommand.CommandType = CommandType.Text;
                OracleParameter oracleParameter = new OracleParameter("PQID", UserQID);
                oracleCommand.Parameters.Add(oracleParameter);
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                   userId = Convert.ToInt32(dataTable.Rows[0]["ID"]);
                   UpdateUserMobile(UserQID, UserMobile);
                }
                else
                {
                   
                    OracleCommand oracleCommandInsertUser = new OracleCommand();
                    oracleCommandInsertUser = oracleConnection.CreateCommand();
                    oracleCommandInsertUser.CommandText = "INSERT INTO USER_PROFILE (QID, NAME, MOBILE) VALUES (:PQID, :PNAME, :PMOBILE) RETURNING id INTO :insertedId";
                    oracleCommandInsertUser.CommandType = CommandType.Text;
                    OracleParameter oracleParameterQID = new OracleParameter("PQID", UserQID);
                    OracleParameter oracleParameterName = new OracleParameter("PNAME", UserName);
                    OracleParameter oracleParameterMobile = new OracleParameter("PMOBILE", UserMobile);
                    oracleCommandInsertUser.Parameters.Add(oracleParameterQID);
                    oracleCommandInsertUser.Parameters.Add(oracleParameterName);
                    oracleCommandInsertUser.Parameters.Add(oracleParameterMobile);
                    oracleCommandInsertUser.Parameters.Add("insertedId", OracleDbType.Int32).Direction = System.Data.ParameterDirection.Output;
                    oracleConnection.Open();
                    oracleCommandInsertUser.ExecuteNonQuery();

                    object id = oracleCommandInsertUser.Parameters["insertedId"].Value;
                    userId = int.Parse (id.ToString());
                    oracleConnection.Close();
                }
                return userId;
            }
            catch (Exception ex)
            {
                return userId;
            }
        }
        public void UpdateUserMobile(string UserQID, string NewMobile)
        {
            try
            {
                DataTable dataTable = new DataTable();
                OracleConnection oracleConnection = new OracleConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["FOODERSS_PRODConnString"].ConnectionString
                };
                OracleCommand oracleCommand = new OracleCommand();
                oracleCommand = oracleConnection.CreateCommand();
                oracleCommand.CommandText = "update USER_PROFILE set MOBILE="+NewMobile+"WHERE QID="+UserQID;
                oracleCommand.CommandType = CommandType.Text;
                oracleConnection.Open();
                oracleCommand.ExecuteNonQuery();
                oracleConnection.Close();

            }
            catch (Exception ex)
            {
                
            }
        }
        public String GetUserNameByQID(String qid)
        {
            try
            {
                IdentityManagmentServiceClient identityManagmentServiceClient = new IdentityManagmentServiceClient();
                String returnValue = null;
                returnValue = identityManagmentServiceClient.retrieveCitizenById(qid).nameA;
                if (returnValue == null)
                {
                    return "";
                }
                else
                {
                    return returnValue;
                }
            }
            catch
            {
                return null;
            }
        }
        public Boolean CheckQidMobile (String qid, String mobile)
        {
            try
            {
                VerificationCustomerMobile VCM = new VerificationCustomerMobile();
                VerifyCustomerMobileRequest Verify_Request = new VerifyCustomerMobileRequest();
                Verify_Request.QID = qid;
                Verify_Request.mobileNo = mobile;
                VCM.verifyCustomerMobileRequest = Verify_Request;
                CustomerVerificationMobileClient D = new CustomerVerificationMobileClient();
                VerificationCustomerMobileResponse Verify_Responce = new VerificationCustomerMobileResponse();
                Verify_Responce = D.verifyCustomerMobile(VCM);
                String inw = Verify_Responce.result.statusMessage; //the message that is in responce
                if (Verify_Responce.result.status == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }
        public string CreateOTP(int length)
        {
            try
            {
                const string valid = "1234567890";
                StringBuilder res = new StringBuilder();
                Random rnd = new Random();
                while (0 < length--)
                {
                    res.Append(valid[rnd.Next(valid.Length)]);
                }
                return res.ToString();
            }
            catch
            {
                return "";
            }
        }  
        protected void txt_qid_TextChanged(object sender, EventArgs e)
        {
        }
    }
}