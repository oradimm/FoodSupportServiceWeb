using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FooderSupportService.backend
{
    public partial class section_head : System.Web.UI.Page
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
            ASPxGridView1.DataSource = GetAllRequestUnderProcess();
            ASPxGridView1.DataBind();
        }
        protected DataTable GetAllRequestUnderProcess()
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
                oracleCommand.CommandText = "select * from VW_UNDER_PROCESS_REQUESTS";
                oracleCommand.CommandType = CommandType.Text;
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
               
            }
            catch (Exception ex)
            {

            }
            return dataTable;
        }
        protected DataTable UpdateRequestApprovalStatus(string reqId, string actionValue, string notes, string requestRef, string mobile)
        {
            string approvalStatus = "0";
            DateTime approvalDate = DateTime.Now;
            DateTime support_start = approvalDate;
            DateTime support_end = support_start.AddDays(31);
            if (actionValue == "2")
            {
                approvalStatus = "2";
                UtilityHelper.SendSms(mobile, "عزيزي المربي تم رفض طلب دعم الأعلاف رقم: " + requestRef + "و ذلك " + notes);
            }
            else if (actionValue == "1")
            {
                approvalStatus = "1";
                UtilityHelper.SendSms(mobile, " عزيزي المربي تم الموافقة طلب خدمة دعم الأعلاف رقم: \n"+ requestRef +"\n"+
                    "يرجى التوجه لإستلام الأعلاف من مركز الصرف في الموقع التالي: " +
                    "\n"+ "https://maps.app.goo.gl/tgHEbcoFUKJAu1UK9" +
                    "\nوذلك قبل تاريخ نهاية الأستحقاق" + support_end.ToString("MM-dd-yyyy"));
            }
            DataTable dataTable = new DataTable();
            try
            {
                OracleConnection oracleConnection = new OracleConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["FOODERSS_PRODConnString"].ConnectionString
                };
                OracleCommand oracleCommand = new OracleCommand();
                oracleCommand = oracleConnection.CreateCommand();
                oracleCommand.CommandText = "update   REQUESTS set  APPROVAL_STATUS=" + approvalStatus + ",APPROVAL_BY='"+backUserObj.UserQid+"',APPROVAL_NOTES='"+notes+ "',APPROVAL_DATE= TO_DATE('" + approvalDate.ToString("MM-dd-yyyy")+ "','MM-DD-YYYY'),SUPPORT_START=TO_DATE('" + support_start.ToString("MM-dd-yyyy") + "','MM-DD-YYYY'),SUPPORT_END= TO_DATE('" + support_end.ToString("MM-dd-yyyy") + "','MM-DD-YYYY') WHERE REQ_ID =" + reqId;
                oracleCommand.CommandType = CommandType.Text;
                oracleConnection.Open();
                int rowsAffected = oracleCommand.ExecuteNonQuery();
                oracleConnection.Close();
            }
            catch (Exception ex)
            {

            }
            return dataTable;
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            string action = radBtnList_Action.SelectedValue;
            string notes = txt_reject_reson.Text;
            string EmpUserName = backUserObj.UserQid;

            List<object> SelectedRequests =  ASPxGridView1.GetSelectedFieldValues("REQ_ID","MOBILE", "REQ_DATE");
            foreach (object[] request in SelectedRequests)
            {
                DateTime requestDate = Convert.ToDateTime(request[2]);
                string reqId = request[0].ToString();
                string requestRef = "FSS" + requestDate.ToString("yy") + requestDate.ToString("MM") + reqId;
                string mobile = request[1].ToString();
                UpdateRequestApprovalStatus(reqId, radBtnList_Action.SelectedValue,txt_reject_reson.Text,requestRef,mobile);
            }
            GridDataBind();
        }
        protected void ASPxGridView1_DataBinding(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSource = GetAllRequestUnderProcess();
        }
        protected void btn_allRequests_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/allRequests.aspx");
        }
        protected void btn_underReview_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/section_head.aspx");
        }
    }
}