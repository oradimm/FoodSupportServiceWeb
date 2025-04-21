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
    public partial class process : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridBindData();
            }
        }
        private void GridBindData()
        {
            ASPxGridView1.DataSource = GetAllRequestUnderReview();
            ASPxGridView1.DataBind();
        }
        protected DataTable GetAllRequestUnderReview()
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
        protected DataTable UpdateRequestReviewStatus(string reqId,string actionValue,string user,string notes,DateTime commiteeMeetingDate, string committeeMeetingSn, string requestRef,string mobile)
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
                oracleCommand.CommandText = "update   WSS_REQUEST set APPROVAL_STATUS=" + actionValue+ ", APPROVAL_DATE=SYSDATE, APPROVAL_BY='" + user+ "', APPROVAL_NOTES='" + notes+ "',COMMITTEE_DATE=TO_DATE('"+commiteeMeetingDate.ToString("yyyy-MM-dd")+ "', 'YYYY-MM-DD'),SUPPORT_START=TO_DATE('" + commiteeMeetingDate.ToString("yyyy-MM-dd") + "', 'YYYY-MM-DD'),SUPPORT_END=TO_DATE('" + commiteeMeetingDate.AddYears(1).ToString("yyyy-MM-dd") + "', 'YYYY-MM-DD'),COMMITTEE_SN='" + committeeMeetingSn+"' WHERE REQ_ID = " + reqId;
                oracleCommand.CommandType = CommandType.Text;
                oracleConnection.Open();
                int rowsAffected = oracleCommand.ExecuteNonQuery();
                oracleConnection.Close();
                if(actionValue=="1")
                {
                    UtilityHelper.SendSms(mobile, "تمت الموافقة على طلب دعم المياه رقم: " + requestRef + "و يمكن متابعة طلبكم على موقع وزارة البلدية او تطبيق عون");
                }
                else
                if (actionValue == "2")
                {
                    UtilityHelper.SendSms(mobile, "تم رفض طلب دعم المياه رقم: " + requestRef + "و ذلك لـ" + notes);
                }
            }
            catch (Exception ex)
            {

            }
            return dataTable;
        }
        protected void ASPxGridView1_SelectionChanged(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            var RequIdData = grid.GetSelectedFieldValues("REQ_ID");
            var MobileData = grid.GetSelectedFieldValues("MOBILE");
            var RequestRefData = grid.GetSelectedFieldValues("REF");
            Session["selectedRequestId"] = RequIdData[0].ToString();
            Session["Mobile"] = MobileData[0].ToString();
            Session["selectedRequestRef"] = RequestRefData[0].ToString();
            ASPxDateEdit_committee_meeting.Value = DateTime.Today;
            FillCommitteeNumberPrefix(Convert.ToDateTime(ASPxDateEdit_committee_meeting.Value));
            panel_action.Visible = true;
        }
        protected void radBtnList_Action_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radBtnList_Action.SelectedValue == "2")
            {
                panel_reject_reason.Visible = true;
                panel_committee_details.Visible = false;
            }
            else
            {
                panel_reject_reason.Visible = false;
                panel_committee_details.Visible = true;
            }
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (radBtnList_Action.SelectedValue == "1")
            {
                UpdateRequestReviewStatus(Session["selectedRequestId"].ToString(), "1", Session["EmpUserName"].ToString(), "",Convert.ToDateTime(ASPxDateEdit_committee_meeting.Value),txt_committee_year_month.Text+txt_committee_sn.Text, Session["selectedRequestRef"].ToString(), Session["Mobile"].ToString());
               
            }
            else if(radBtnList_Action.SelectedValue == "2")
            {
                UpdateRequestReviewStatus(Session["selectedRequestId"].ToString(), "2", Session["EmpUserName"].ToString(), txt_reject_reson.Text,DateTime.MinValue, txt_committee_year_month.Text + txt_committee_sn.Text, Session["selectedRequestRef"].ToString(), Session["Mobile"].ToString());
            }
            GridBindData();
            panel_action.Visible = false;
        }
        protected void btn_underReview_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/requests.aspx");
        }
        protected void btn_underProcess_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/process.aspx");
        }
        protected void ASPxDateEdit_committee_meeting_ValueChanged(object sender, EventArgs e)
        {
            FillCommitteeNumberPrefix(Convert.ToDateTime(ASPxDateEdit_committee_meeting.Value));
        }
        private void FillCommitteeNumberPrefix(DateTime date)
        {
            txt_committee_year_month.Text = "CM" + date.ToString("yy") + date.ToString("MM");
        }
        protected void ASPxGridView1_DataBinding(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSource = GetAllRequestUnderReview();
        }

        protected void btn_allRequests_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/allRequests.aspx");
        }

        protected void btn_underAuditing_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/auditingRequests.aspx");
        }
    }
}