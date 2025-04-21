using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using FooderSupportService.SMSWebService;

namespace FooderSupportService
{
    public static class UtilityHelper
    {
        public static Boolean SendSms(String Mobile, String SMS_Text)
        {
            try
            {
                Send_normal_SMS_ooredooRequest SMS_request = new Send_normal_SMS_ooredooRequest();
                Send_normal_SMS_ooredooRequestBody SMS_Body = new Send_normal_SMS_ooredooRequestBody();
                SMS_Body.QID = "EX_An-aR@21";
                SMS_Body.App_type = "sms-s";
                SMS_Body.mobile_no = Mobile;
                SMS_Body.Sent_by = "FooderSupportService";
                SMS_Body.Txt_sms = SMS_Text;
                SMS_request.Body = SMS_Body;
                CommunicateSoapClient c = new CommunicateSoapClient();
                String stat = c.Send_normal_SMS_ooredoo("EX_An-aR@21", "sms-s", Mobile, "Animal Certificate", SMS_Text);
                //LOG SMS
                StackTrace stackTrace = new StackTrace(); //used to get the metod and class used to call this method
                if (SMS_Text.Contains("OTP") == true)
                { //if it is an OTP message then dont store the OTP in the DB
                  // LOG_SMS("APP_SMS", Mobile, "OTP", "HIDDEN OTP", stat, stackTrace.GetFrame(1).GetMethod().DeclaringType.Name, stackTrace.GetFrame(1).GetMethod().Name);
                }
                else
                {
                    // LOG_SMS("APP_SMS", Mobile, "APP_SMS", SMS_Text, stat, stackTrace.GetFrame(1).GetMethod().DeclaringType.Name, stackTrace.GetFrame(1).GetMethod().Name);
                }

                return true;
            }
            catch
            {
                //LOG_ERROR(ex.ToString(), "");
                return false;
            }

        }
        public static void UpdateLastActivityForUser(int UserId)
        {
            try
            {
                OracleConnection oracleConnection = new OracleConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["FOODERSS_PRODConnString"].ConnectionString
                };
                OracleCommand oracleCommand = new OracleCommand();
                oracleCommand = oracleConnection.CreateCommand();
                oracleCommand.CommandText = "UPDATE USER_PROFILE SET LAST_ACTIVITY=SYSDATE WHERE ID="+ UserId;
                oracleCommand.CommandType = CommandType.Text;
                oracleConnection.Open();
                oracleCommand.ExecuteNonQuery();
                oracleConnection.Close();
            }
            catch
            { }
        }
        public static DateTime GetLastActivityForUser(int UserId)
        {
            DateTime LastActivity = DateTime.MinValue;
            try
            {
                DataTable dataTable = new DataTable();
                OracleConnection oracleConnection = new OracleConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["FOODERSS_PRODConnString"].ConnectionString
                };
                OracleCommand oracleCommand = new OracleCommand();
                oracleCommand = oracleConnection.CreateCommand();
                oracleCommand.CommandText = "SELECT LAST_ACTIVITY FROM USER_PROFILE WHERE ID=" + UserId;
                oracleCommand.CommandType = CommandType.Text;
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    LastActivity = Convert.ToDateTime(dataTable.Rows[0]["LAST_ACTIVITY"]);  
                }
            }
            catch
            { }
            return LastActivity;
        }

    }
}