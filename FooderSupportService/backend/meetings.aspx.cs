using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using FooderSupportService.AttachmentsWSProd;

namespace FooderSupportService.backend
{
    public partial class meetings : System.Web.UI.Page
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
            ASPxGridView1.DataSource = GetAllMeetings();
            ASPxGridView1.DataBind();
        }
        protected DataTable GetAllMeetings()
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
                oracleCommand.CommandText = "select * from COMM_MEETING order by meeting_date DESC";
                oracleCommand.CommandType = CommandType.Text;
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
               
            }
            catch (Exception ex)
            {

            }
            return dataTable;
        }
        protected void ASPxGridView1_DataBinding(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSource = GetAllMeetings();
        }
        protected void btn_underReview_Click(object sender, EventArgs e)
        {
            //pcLogin.op
        }
        protected void pcLogin_Load(object sender, EventArgs e)
        {
            txt_sn_y.Text = "CM" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM");
           
        }
        protected void btOK_Click(object sender, EventArgs e)
        {
            string SN = txt_sn_y.Text + txt_sn.Text;
            DateTime meetingDate = Convert.ToDateTime(de_meeting_date.Value);
            DateTime reportDateFrom = Convert.ToDateTime(de_report_date_from.Value);
            DateTime reportDateTo = Convert.ToDateTime(de_report_date_to.Value);
            string notes = txt_notes.Text;
            string created_by = Session["EmpUserName"].ToString();
            DateTime createdDate = DateTime.Now;
            string ip = GetIPAddress();
            SaveNewMeeting(SN,meetingDate,reportDateFrom, reportDateTo, notes,created_by,createdDate,ip);
            GridBindData();
            
        }
        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        public void SaveNewMeeting(string SN, DateTime meetingDate, DateTime reportDateFrom, DateTime reportDateTo, string notes, string created_by, DateTime createdDate, string ip)
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
                oracleCommand.CommandText = @"insert into COMM_MEETING(CREATED_BY ,NOTES ,MEETING_DATE,REPORT_DATE_FROM,REPORT_DATE_TO,CREATED_DATE,UPDATED_IP,SN) 
                                            values (:p_CREATED_BY,:p_NOTES,:p_MEETING_DATE,:p_REPORT_DATE_FROM,:p_REPORT_DATE_TO,:p_CREATED_DATE,:p_UPDATED_IP,:p_SN)";
                oracleCommand.CommandType = CommandType.Text;

                OracleParameter p_CREATED_BY = new OracleParameter("p_CREATED_BY", OracleDbType.Varchar2);
                p_CREATED_BY.Direction = ParameterDirection.Input;
                p_CREATED_BY.Value = created_by;
                oracleCommand.Parameters.Add(p_CREATED_BY);

                
                OracleParameter p_NOTES = new OracleParameter("p_NOTES", OracleDbType.Varchar2);
                p_NOTES.Direction = ParameterDirection.Input;
                p_NOTES.Value = notes;
                oracleCommand.Parameters.Add(p_NOTES);

                OracleParameter p_MEETING_DATE = new OracleParameter("p_MEETING_DATE", OracleDbType.Date);
                p_MEETING_DATE.Direction = ParameterDirection.Input;
                p_MEETING_DATE.Value = meetingDate;
                oracleCommand.Parameters.Add(p_MEETING_DATE);

                OracleParameter p_REPORT_DATE_FROM = new OracleParameter("p_REPORT_DATE_FROM", OracleDbType.Date);
                p_REPORT_DATE_FROM.Direction = ParameterDirection.Input;
                p_REPORT_DATE_FROM.Value = reportDateFrom;
                oracleCommand.Parameters.Add(p_REPORT_DATE_FROM);

                OracleParameter p_REPORT_DATE_TO = new OracleParameter("p_REPORT_DATE_TO", OracleDbType.Date);
                p_REPORT_DATE_TO.Direction = ParameterDirection.Input;
                p_REPORT_DATE_TO.Value = reportDateTo;
                oracleCommand.Parameters.Add(p_REPORT_DATE_TO);

                OracleParameter p_CREATED_DATE = new OracleParameter("p_CREATED_DATE", OracleDbType.Date);
                p_CREATED_DATE.Direction = ParameterDirection.Input;
                p_CREATED_DATE.Value = DateTime.Now;
                oracleCommand.Parameters.Add(p_CREATED_DATE);


                OracleParameter p_UPDATED_IP = new OracleParameter("p_UPDATED_IP", OracleDbType.Varchar2);
                p_UPDATED_IP.Direction = ParameterDirection.Input;
                p_UPDATED_IP.Value = ip;
                oracleCommand.Parameters.Add(p_UPDATED_IP);

                OracleParameter p_SN = new OracleParameter("p_SN", OracleDbType.Varchar2);
                p_SN.Direction = ParameterDirection.Input;
                p_SN.Value = SN;
                oracleCommand.Parameters.Add(p_SN);



                oracleConnection.Open();
                oracleCommand.ExecuteNonQuery();
                oracleConnection.Close();
               // lbl_message.Text = "تم الحفظ بنجاح";
            }
            catch (Exception ex)
            {
                //lbl_message.Text = "جدث خطأ فني, يرجى المحاولة في وقت اخر";
            }
        }
        protected void btn_upload_attachment_Init(object sender, EventArgs e)
        {
            ASPxButton btn_upload_attachment = (ASPxButton)sender;
            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)btn_upload_attachment.NamingContainer;
            string id = ASPxGridView1.GetRowValues(container.VisibleIndex, "ID").ToString();
            string ATTACHMENT = ASPxGridView1.GetRowValues(container.VisibleIndex, "ATTACHMENT").ToString();
            btn_upload_attachment.ClientSideEvents.Click = "function(s, e) {OnAttachBtnClick('"+ id + "',`" + ATTACHMENT + "`);}";
        }
        protected void btn_edit_Init(object sender, EventArgs e)
        {
            ASPxButton btn_edit = (ASPxButton)sender;
            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)btn_edit.NamingContainer;
            object id = ASPxGridView1.GetRowValues(container.VisibleIndex, "ID").ToString();
            object notes = ASPxGridView1.GetRowValues(container.VisibleIndex, "NOTES").ToString();
            object meetingDate = ASPxGridView1.GetRowValues(container.VisibleIndex, "MEETING_DATE");
            btn_edit.ClientSideEvents.Click = "function(s, e) {OnEditBtnClick('" + id + "',`" + notes + "`,`" + meetingDate + "`);}";
        }
        protected void ASPxGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }
        protected void btn_edit_save_Click(object sender, EventArgs e)
        {
            string meetingId = hf_meeting_id_edit.Value;
            DateTime meetingDate = Convert.ToDateTime(de_edit_meeting_date.Value);
            string notes = txt_edit_notes.Text;
            string updated_by = Session["EmpUserName"].ToString();
            string ip = GetIPAddress();
            UpdateMeeting(meetingId, meetingDate, notes, updated_by, ip);
            GridBindData();
        }
        protected void btn_attachment_save_Click(object sender, EventArgs e)
        {
            string meetingId = hf_meeting_id_attachment.Value;
            hf_uploaded_file_id.Value = AttachFile(fu_attachment.FileBytes, fu_attachment.PostedFile.ContentType);
            SaveFile(meetingId, hf_uploaded_file_id.Value);
        }
        public void UpdateMeeting(string meetingId, DateTime meetingDate, string notes, string updated_by, string ip)
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
                oracleCommand.CommandText = @"update COMM_MEETING set NOTES='" + notes + "' ,MEETING_DATE=TO_DATE('" + meetingDate.ToString("yyyy-MM-dd") + "', 'YYYY-MM-DD'),UPDATED_BY='" + updated_by + "',UPDATED_DATE = SYSDATE, UPDATED_IP='" + ip + "' where id="+meetingId;
                oracleCommand.CommandType = CommandType.Text;
                oracleConnection.Open();
                oracleCommand.ExecuteNonQuery();
                oracleConnection.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void SaveFile(string meetingId,string file_id)
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
                oracleCommand.CommandText = @"update COMM_MEETING set ATTACHMENT='" + file_id + "' where id=" + meetingId;
                oracleCommand.CommandType = CommandType.Text;
                oracleConnection.Open();
                oracleCommand.ExecuteNonQuery();
                oracleConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void btn_show_file_Click(object sender, EventArgs e)
        {
            OpenPopUpPage(hf_uploaded_file_id.Value);
        }
        private void OpenPopUpPage(string url)
        {
            if (!(string.IsNullOrEmpty(url)))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + "https://dotnet.mme.gov.qa/AttachmentsDownloadManager/GetAttachmentProd?DocID=" + url + "');", true);
            }
            else
            {
                //lbl_message.Text = "الملف غير مرفق";
            }

        }
        public static string AttachFile(byte[] UploadDocContentBytes, string mimeType)
        {
            string fileId;
            AttachmentsServiceProdSoapClient AttServiceProd = new AttachmentsServiceProdSoapClient();
            AttachedDocUploadResult uploadResultProd = AttServiceProd.UploadConfirmedAttachmentFile("ds.jpg", mimeType, "DocPrintService", UploadDocContentBytes);
            var v = UploadDocContentBytes.ToString();
            bool isSuccess = uploadResultProd.boolFinalResult;
            string createdDocID = uploadResultProd.newCreatedDocID;
            if (isSuccess)
            {
                fileId = createdDocID;
            }
            else
            {
                fileId = string.Empty;
            }
            return fileId;
        }
        protected void btn_attachment_cancel_Click(object sender, EventArgs e)
        {
            hf_uploaded_file_id.Value = null;
        }
        protected void btn_meeting_report_Click(object sender, EventArgs e)
        {
            ASPxButton button = (ASPxButton)sender;
            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)button.NamingContainer;
            Session["sDate"] = ASPxGridView1.GetRowValues(container.VisibleIndex, "REPORT_DATE_FROM").ToString(); 
            Session["eDate"] = ASPxGridView1.GetRowValues(container.VisibleIndex, "REPORT_DATE_TO").ToString(); 
            Session["meetingDate"] = ASPxGridView1.GetRowValues(container.VisibleIndex, "MEETING_DATE");
            Session["meetingSn"] = ASPxGridView1.GetRowValues(container.VisibleIndex, "SN");
            Session["notes"] = ASPxGridView1.GetRowValues(container.VisibleIndex, "NOTES");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/backend/reports/CommiteeReport.aspx');", true);
        }
        protected void btn_committee_report_anix_Click(object sender, EventArgs e)
        {
            ASPxButton button = (ASPxButton)sender;
            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)button.NamingContainer;
            Session["sDate"] = ASPxGridView1.GetRowValues(container.VisibleIndex, "REPORT_DATE_FROM").ToString(); 
            Session["eDate"] = ASPxGridView1.GetRowValues(container.VisibleIndex, "REPORT_DATE_TO").ToString();
            Session["meetingSn"] = ASPxGridView1.GetRowValues(container.VisibleIndex, "SN");
            Session["meetingDate"] = ASPxGridView1.GetRowValues(container.VisibleIndex, "MEETING_DATE");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/backend/reports/CommitteeReportAnix.aspx');", true);
        }
        protected void btn_finance_report_Click(object sender, EventArgs e)
        {
            ASPxButton button = (ASPxButton)sender;
            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)button.NamingContainer;
            Session["sDate"] = ASPxGridView1.GetRowValues(container.VisibleIndex, "REPORT_DATE_FROM").ToString(); ;
            Session["eDate"] = ASPxGridView1.GetRowValues(container.VisibleIndex, "REPORT_DATE_TO").ToString(); ;
            Session["meetingDate"] = ASPxGridView1.GetRowValues(container.VisibleIndex, "MEETING_DATE");
            Session["meetingSn"] = ASPxGridView1.GetRowValues(container.VisibleIndex, "SN");
            Session["notes"] = ASPxGridView1.GetRowValues(container.VisibleIndex, "NOTES");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/backend/reports/FinanceReport.aspx');", true);
        }
        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/requests.aspx");
        }
        protected void btn_show_file_Init(object sender, EventArgs e)
        {
            ASPxButton btn_show_file = (ASPxButton)sender;
            btn_show_file.ClientSideEvents.Click = "function(s, e) {ShowFile();}";
        }
    }
}