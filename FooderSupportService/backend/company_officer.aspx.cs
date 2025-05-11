using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FooderSupportService.backend
{
    public partial class company_officer : System.Web.UI.Page
    {
        BackUserObj backUserObj { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            backUserObj = (BackUserObj)Session["backUserObj"];
            if (!IsPostBack)
            {
                GridDataBind();
            }
            try
            {
                string qid = ASPxGridView1.GetSelectedFieldValues("QID")[0].ToString();
               
            }
            catch
            { }
        }
        private void GridDataBind()
        {
            ASPxGridView1.DataSource = GetAllApprovedRequests();
            ASPxGridView1.DataBind();
        }
        protected DataTable GetAllApprovedRequests()
        {
            DataTable dataTable = new DataTable();
            try
            {
                OracleConnection oracleConnection = new OracleConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["FOODERSS_PRODConnString"].ConnectionString
                };
                OracleCommand oracleCommand = new OracleCommand();
                oracleCommand = oracleConnection.CreateCommand();
                oracleCommand.CommandText = "select * from VW_APPROVED_REQUESTS";
                oracleCommand.CommandType = CommandType.Text;
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
               
            }
            catch (Exception ex)
            {

            }
            return dataTable;
        }
        protected void UpdateRequestStatus()
        {

            if (txt_otp.Text == Session["otp"].ToString())
            {
                List<object> SelectedRequests = ASPxGridView1.GetSelectedFieldValues("REQ_ID", "MOBILE", "REF");
                object[] SelectedRequest = (object[])SelectedRequests[0];
                string reqId = SelectedRequest[0].ToString();
                string mobile = SelectedRequest[1].ToString();
                string reqRef = SelectedRequest[2].ToString();


                DataTable dataTable = new DataTable();
                try
                {
                    OracleConnection oracleConnection = new OracleConnection
                    {
                        ConnectionString = ConfigurationManager.ConnectionStrings["FOODERSS_PRODConnString"].ConnectionString
                    };
                    OracleCommand oracleCommand = new OracleCommand();
                    oracleCommand = oracleConnection.CreateCommand();
                    oracleCommand.CommandText = "update requests set DELIVERY_STATUS=1 ,DELIVERED_DATE=SYSDATE ,DELIVERED_BY='"+backUserObj.UserQid+"' where REQ_ID=" + reqId;
                    oracleCommand.CommandType = CommandType.Text;
                    oracleConnection.Open();
                    int rowsAffected = oracleCommand.ExecuteNonQuery();
                    oracleConnection.Close();
                    UtilityHelper.SendSms(mobile, "تم استلام الكمية المخصصة من الأعلاف و الخاصة بطلب رقم "+"\n"+ reqRef);
                    Response.Redirect("~/backend/company_officer.aspx");
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                lbl_message.Text = "رمز التحقق غير صحيح!";
            }
            
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            UpdateRequestStatus();
            GridDataBind();
        }
        protected void ASPxGridView1_DataBinding(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSource = GetAllApprovedRequests();
        }
        protected void btn_allRequests_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/company_report.aspx");
        }
        protected void btn_underReview_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/company_officer.aspx");
        }
        protected void btn_send_otp_Click(object sender, EventArgs e)
        {
            string otp = CreateOTP(6);
            string EmpUserName = backUserObj.UserQid;

            List<object> SelectedRequests = ASPxGridView1.GetSelectedFieldValues("REQ_ID", "MOBILE", "REQ_DATE", "SUPPORT_END", "QID", "NAME");
            foreach (object[] request in SelectedRequests)
            {
                DateTime requestDate = Convert.ToDateTime(request[2]);
                string reqId = request[0].ToString();
                string requestRef = "FSS" + requestDate.ToString("yy") + requestDate.ToString("MM") + reqId;
                string mobile = request[1].ToString();
                DateTime requestSupportEndDate = Convert.ToDateTime(request[3]);
                string qid = request[4].ToString();
                string name = request[5].ToString();
                string smsText = "رمز التحقق لصرف الأعلاف المركزة و العشبية للرقم الشخصي " +
                    qid +
                    " " +
                    "و المسجل بأسم " + " " +
                    name + " "+
                    "هو"+ "\n"+
                    otp+"\n"+
                    "يرجى مشاركة هذا الرمز مع موظف التسليم لإتمام عملية التسليم."
                    ;
                Session["otp"] = otp;
                UtilityHelper.SendSms(mobile,smsText);
                btn_send_otp.Visible = false;
                ASPxGridView1.Enabled = false;

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

    }
}