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
    public partial class auditingRequests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridDataBind();
            }
            try
            {
                string qid = ASPxGridView1.GetSelectedFieldValues("QID")[0].ToString();
                GetOwnershipsData(qid);
            }
            catch
            { }
        }
        private void GridDataBind()
        {
            lbl_count.Text = GetAllRequestUnderAuditingCount();
            ASPxGridView1.DataSource = GetAllRequestUnderAuditing();
            ASPxGridView1.DataBind();
        }
        protected DataTable GetAllRequestUnderAuditing()
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
                oracleCommand.CommandText = "select * from VW_UNDER_AUDIT_REQUESTS WHERE REVIEWED_BY <> '"+ Session["EmpUserName"].ToString() + "'";
                oracleCommand.CommandType = CommandType.Text;
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
               
            }
            catch (Exception ex)
            {

            }
            return dataTable;
        }
        protected DataTable UpdateRequestReviewStatus(string reqId,string actionValue,string user,string notes,string requestRef, string mobile)
        {
            string approvalStatus = "0";
            if (actionValue == "2")
            {
                approvalStatus = "2";
                UtilityHelper.SendSms(mobile, "عزيزي المربي تم رفض طلب دعم المياه رقم: " + requestRef + "و ذلك " + notes);
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
                oracleCommand.CommandText = "update   WSS_REQUEST set AUDIT_STATUS=" + actionValue+ ", AUDITED_DATE=SYSDATE, AUDITED_BY='" + user+ "', AUDITING_NOTES='" + notes+ "', APPROVAL_STATUS="+ approvalStatus + " WHERE REQ_ID = " + reqId;
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
        protected void ASPxGridView1_SelectionChanged(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            var RequIdData = grid.GetSelectedFieldValues("REQ_ID");
            var MobileData = grid.GetSelectedFieldValues("MOBILE");
            
            var BankProfileData = grid.GetSelectedFieldValues("BANK_PROFILE");
            var WaterCertData = grid.GetSelectedFieldValues("WATER_CERT_PROFILE");
            
            var RequestRefData = grid.GetSelectedFieldValues("REF");
            Session["selectedRequestId"] = RequIdData[0].ToString(); 
            Session["selectedRequestRef"] = RequestRefData[0].ToString();
            Session["Mobile"] = MobileData[0].ToString();
            
            Session["BankProfileId"] = BankProfileData[0].ToString();
            
            Session["WaterCertId"] = WaterCertData[0].ToString();
            
            BindBankInfo(Session["BankProfileId"].ToString());
            BindWaterCertInfo(Session["WaterCertId"].ToString());
            string qid = grid.GetSelectedFieldValues("QID")[0].ToString();
            GetOwnershipsData(qid);
            panel_action.Visible = true;
        }
        protected void radBtnList_Action_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radBtnList_Action.SelectedValue == "2")
            {
                panel_reject_reason.Visible = true;
            }
            else
            {
                panel_reject_reason.Visible = false;
            }
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (radBtnList_Action.SelectedValue == "1")
            {
                UpdateRequestReviewStatus(Session["selectedRequestId"].ToString(), "1", Session["EmpUserName"].ToString(), "","","");
            }
            else if(radBtnList_Action.SelectedValue == "2")
            {
                UpdateRequestReviewStatus(Session["selectedRequestId"].ToString(), "2", Session["EmpUserName"].ToString(), txt_reject_reson.Text, Session["selectedRequestRef"].ToString(), Session["Mobile"].ToString());
            }
            GridDataBind();
            panel_action.Visible = false;
        }
        public void BindBankInfo(string bankProfileId)
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
                oracleCommand.CommandText = "select * from USER_BANK_INFO where ID=:profileId";
                oracleCommand.CommandType = CommandType.Text;
                OracleParameter oracleParameter = new OracleParameter("profileId", bankProfileId);
                oracleCommand.Parameters.Add(oracleParameter);
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    txt_iban1.Text = dataTable.Rows[0]["IBAN1"].ToString();
                    txt_iban2.Text = dataTable.Rows[0]["IBAN2"].ToString();
                    txt_iban3.Text = dataTable.Rows[0]["IBAN3"].ToString();
                    txt_iban4.Text = dataTable.Rows[0]["IBAN4"].ToString();
                    txt_iban5.Text = dataTable.Rows[0]["IBAN5"].ToString();
                    txt_iban6.Text = dataTable.Rows[0]["IBAN6"].ToString();
                    txt_iban7.Text = dataTable.Rows[0]["IBAN7"].ToString();
                    txt_iban8.Text = dataTable.Rows[0]["IBAN8"].ToString();
                    lb_view_iban_cert.Attributes.Add("fileId", dataTable.Rows[0]["IBAN_FILE"].ToString());
                    lb_view_iban_cert.Visible = true;

                }
                else
                {
                    txt_iban1.Text = "";
                    txt_iban2.Text = "";
                    txt_iban3.Text = "";
                    txt_iban4.Text = "";
                    txt_iban5.Text = "";
                    txt_iban6.Text = "";
                    txt_iban7.Text = "";
                    txt_iban8.Text = "";
                    lb_view_iban_cert.Attributes.Add("fileId", "");
                    lb_view_iban_cert.Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void BindWaterCertInfo(string waterCertId)
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
                oracleCommand.CommandText = "select * from USER_WATER_CERT where ID=:WATER_CERT_ID";
                oracleCommand.CommandType = CommandType.Text;
                OracleParameter oracleParameter = new OracleParameter("WATER_CERT_ID", waterCertId);
                oracleCommand.Parameters.Add(oracleParameter);
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    lb_view_water_cert.Attributes.Add("fileId", dataTable.Rows[0]["CERT_FILE"].ToString());
                    lb_view_water_cert.Visible = true;
                }
                else
                {
                    lb_view_water_cert.Attributes.Add("fileId", "");
                    lb_view_water_cert.Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void lb_view_iban_cert_Click(object sender, EventArgs e)
        {
            string url = lb_view_iban_cert.Attributes["fileId"];
            OpenPopUpPage(url);
        }
        private void OpenPopUpPage(string url)
        {
            if (!(string.IsNullOrEmpty(url)))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + "https://dotnet.mme.gov.qa/AttachmentsDownloadManager/GetAttachmentProd?DocID=" + url + "');", true);
            }
            else
            {
                lbl_message.Text = "الملف غير مرفق";
            }

        }
        protected void lb_view_water_cert_Click(object sender, EventArgs e)
        {
            string url = lb_view_water_cert.Attributes["fileId"];
            OpenPopUpPage(url);
        }
        protected void lb_save_iban_Click(object sender, EventArgs e)
        {
            //SaveBankInfo(Session["BankProfileId"].ToString(), Session["EmpUserName"].ToString(), txt_iban1.Text, txt_iban2.Text, txt_iban3.Text, txt_iban4.Text, txt_iban5.Text, txt_iban6.Text, txt_iban7.Text, txt_iban8.Text);
        }
        public void SaveBankInfo(string Id, string user, string IBAN1, string IBAN2, string IBAN3, string IBAN4, string IBAN5, string IBAN6, string IBAN7, string IBAN8)
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
                oracleCommand.CommandText = @"UPDATE  USER_BANK_INFO SET IBAN1='" + IBAN1 + "',IBAN2='" + IBAN2 + "',IBAN3='" + IBAN3 + "',IBAN4='" + IBAN4 + "',IBAN5='" + IBAN5 + "',IBAN6='" + IBAN6 + "',IBAN7='" + IBAN7 + "',IBAN8='" + IBAN8 + "',UPDATED_BY='"+user+"',UPDATED_DATE=SYSDATE WHERE ID=" + Id;
                oracleCommand.CommandType = CommandType.Text;
                oracleConnection.Open();
                oracleCommand.ExecuteNonQuery();
                oracleConnection.Close();

                lbl_message.Text = "تم الحفظ بنجاح";
            }
            catch (Exception ex)
            {
                lbl_message.Text = "جدث خطأ فني, يرجى المحاولة في وقت اخر";
            }
        }
        protected void btn_underReview_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/requests.aspx");
        }
        protected void btn_underProcess_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/process.aspx");
        }
        protected void ASPxGridView1_DataBinding(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSource = GetAllRequestUnderAuditing();
        }
        protected void btn_allRequests_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/allRequests.aspx");
        }
        protected void GetOwnershipsData(string UserQid)
        {
            tbl_ownerships.Rows.Clear();

            TableCell tableCell0 = new TableCell { Text = "رقم الاستمارة", BorderWidth = 1 };
            TableCell tableCell1 = new TableCell { Text = "نوع الترخيص", BorderWidth = 1 };
            TableCell tableCell2 = new TableCell { Text = "الموقع", BorderWidth = 1 };
            TableCell tableCell3 = new TableCell { Text = "عدد الابار", BorderWidth = 1 };
            TableCell tableCell4 = new TableCell { Text = "صلاحية المزرعة", BorderWidth = 1 };
            TableCell tableCell5 = new TableCell { Text = "تاريخ اخر حصر", BorderWidth = 1 };
            TableCell tableCell6 = new TableCell { Text = "ابل", BorderWidth = 1 };
            TableCell tableCell7 = new TableCell { Text = "ابقار", BorderWidth = 1 };
            TableCell tableCell8 = new TableCell { Text = "ضان", BorderWidth = 1 };
            TableCell tableCell9 = new TableCell { Text = "ماعز", BorderWidth = 1 };
            TableCell tableCell10 = new TableCell { Text = "خيول", BorderWidth = 1 };
            TableCell tableCell11 = new TableCell { Text = "غزلان", BorderWidth = 1 };

            TableRow trHeader = new TableRow();
            trHeader.Cells.Add(tableCell0);
            trHeader.Cells.Add(tableCell1);
            trHeader.Cells.Add(tableCell2);
            trHeader.Cells.Add(tableCell3);
            trHeader.Cells.Add(tableCell4);
            trHeader.Cells.Add(tableCell5);
            trHeader.Cells.Add(tableCell6);
            trHeader.Cells.Add(tableCell7);
            trHeader.Cells.Add(tableCell8);
            trHeader.Cells.Add(tableCell9);
            trHeader.Cells.Add(tableCell10);
            trHeader.Cells.Add(tableCell11);

            tbl_ownerships.Rows.Add(trHeader);
            List<OwnershipObj> ownershipObjs = new List<OwnershipObj>();
           
            ownershipObjs = GetUserOwnerships(UserQid);
            foreach (OwnershipObj ownership in ownershipObjs)
            {
                OwnershipObj curOwnership = ownership;
                TableCell tableCell_Wells = new TableCell();
                TableCell tableCell_FarmExpDate = new TableCell();
                if (curOwnership.TYPE_NUMBER == 2)
                {
                    bool farmDataAvilable;
                    curOwnership = GetFarmData(ownership, out farmDataAvilable);
                    tableCell_Wells.Text = curOwnership.NUMBER_OF_WELLS.ToString();
                    tableCell_FarmExpDate.Text = curOwnership.FARM_EXP_DATE.ToString("dd-MM-yyyy");
                    if (!farmDataAvilable)
                    {
                        curOwnership.CanApply = false;
                        curOwnership.notes.Add("فشل التحقق من بيانات المزرعة, يرجى مراجعة ادارة الثروة الحيوانية");
                    }  
                }
                else
                {
                    tableCell_Wells.Text = "---";
                    tableCell_FarmExpDate.Text = "---";
                }
                TableRow tableRow = new TableRow();

                TableCell tableCell_OwnershipId = new TableCell();
                TableCell tableCell_OwnershipType = new TableCell();
                TableCell tableCell_Location = new TableCell();
                TableCell tableCell_LastNumbring = new TableCell();
                TableCell tableCell_Notes = new TableCell();
                //TableCell tcCamels_cont = new TableCell() { Text = curOwnership.Camels.ToString() };
                //TableCell tcCows_cont = new TableCell() { Text = curOwnership.Cows.ToString() };
                //TableCell tcGoats_cont = new TableCell() { Text = curOwnership.Goats.ToString() };
                TableCell tcSheeps_cont = new TableCell() { Text = curOwnership.Sheeps.ToString() };
                //TableCell tcHorss_cont = new TableCell() { Text = curOwnership.Horses.ToString() };
                //TableCell tcGazals_cont = new TableCell() { Text = curOwnership.GAZAL.ToString() };

                tableCell_OwnershipId.Text = curOwnership.OWNERSHIP_ID.ToString();
                tableCell_OwnershipType.Text = curOwnership.TYPE;
                tableCell_Location.Text = curOwnership.LOCATION;
                tableCell_LastNumbring.Text = curOwnership.REGISTERATION_DATE.ToString("dd-MM-yyyy");

                tableRow.Cells.Add(tableCell_OwnershipId);
                tableRow.Cells.Add(tableCell_OwnershipType);
                tableRow.Cells.Add(tableCell_Location);
                tableRow.Cells.Add(tableCell_Wells);
                tableRow.Cells.Add(tableCell_FarmExpDate);
                tableRow.Cells.Add(tableCell_LastNumbring);
                //tableRow.Cells.Add(tcCamels_cont);
                //tableRow.Cells.Add(tcCows_cont);
                tableRow.Cells.Add(tcSheeps_cont);
                //tableRow.Cells.Add(tcGoats_cont);
                //tableRow.Cells.Add(tcHorss_cont);
                //tableRow.Cells.Add(tcGazals_cont);
                tbl_ownerships.Rows.Add(tableRow);
            }
        }
        private static List<OwnershipObj> GetUserOwnerships(string UserQid)
        {
            List<OwnershipObj> ownershipObjs = new List<OwnershipObj>();
            try
            {
                DataTable dataTable = new DataTable();
                OracleConnection oracleConnection = new OracleConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["NWA_SYSConnString"].ConnectionString
                };
                OracleCommand oracleCommand = new OracleCommand();
                oracleCommand = oracleConnection.CreateCommand();
                oracleCommand.CommandText = "select * from WATER_SUPPORT_VW where CITIZEN_ID=:QID";
                oracleCommand.CommandType = CommandType.Text;
                OracleParameter oracleParameter = new OracleParameter("QID", UserQid);
                oracleCommand.Parameters.Add(oracleParameter);
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    OwnershipObj ownershipObj = new OwnershipObj()
                    {
                        OWNERSHIP_ID = Convert.ToInt32(dataRow["OWNERSHIP_ID_FULLSERIAL"]),
                        //Camels = Convert.ToInt32(dataRow["Camels"]),
                        //Cows = Convert.ToInt32(dataRow["Cows"]),
                        Sheeps = Convert.ToInt32(dataRow["Sheeps"]),
                        //Goats = Convert.ToInt32(dataRow["Goats"]),
                        //GAZAL = Convert.ToInt32(dataRow["GAZAL"]),
                        //Horses = Convert.ToInt32(dataRow["Horses"]),
                        REGISTERATION_DATE = Convert.ToDateTime(dataRow["REGISTERATION_DATE"]),
                        TYPE = Convert.ToString(dataRow["TYPE"]),
                        TYPE_NUMBER = Convert.ToInt32(dataRow["IZBA_TYPE"]),
                        LIC_NUMBER = Convert.ToInt32(dataRow["LIC_NUMBER"]),
                        LOCATION = Convert.ToString(dataRow["LOCATION"]),
                        BLOCK_REASON = Convert.ToString(dataRow["REASON"]),
                        IS_BLOCKED = Convert.ToBoolean(dataRow["BLOCKSTATUS"]),
                        notes = new List<string>(),
                        CanApply = true
                    };
                    if (ownershipObj.REGISTERATION_DATE.AddYears(2) < DateTime.Today)
                    {
                        ownershipObj.CanApply = false;
                        ownershipObj.notes.Add("لقد مر اكثر من سنتين على تاريخ اخر حصر يرجى طلب موعد ترقيم");
                    }
                    ownershipObjs.Add(ownershipObj);
                }
            }
            catch (Exception ex)
            {

            }
            return ownershipObjs;
        }      
        private static OwnershipObj GetFarmData(OwnershipObj farm, out bool farmDataAvilable)
        {

            try
            {
                DataTable dataTable = new DataTable();
                OracleConnection oracleConnection = new OracleConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["AGRI_ROConnString"].ConnectionString
                };
                OracleCommand oracleCommand = new OracleCommand();
                oracleCommand = oracleConnection.CreateCommand();
                oracleCommand.CommandText = "select  EXPIRY_DATE ,WELLS from ESERV.AG_FARMS_V WHERE FARM_NUMBER = :FARM_NO";
                oracleCommand.CommandType = CommandType.Text;
                OracleParameter oracleParameter = new OracleParameter("FARM_NO", farm.LIC_NUMBER.ToString());
                oracleCommand.Parameters.Add(oracleParameter);
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow dataRow = dataTable.Rows[0];
                    farm.NUMBER_OF_WELLS = Convert.ToInt32(dataRow["WELLS"]);
                    farm.FARM_EXP_DATE = Convert.ToDateTime(dataRow["EXPIRY_DATE"]);
                    farmDataAvilable = true;
                    if (farm.FARM_EXP_DATE < DateTime.Today)
                    {
                        farm.CanApply = false;
                        farm.notes.Add("صلاحية المزرعة منتهية");
                    }
                }
                else
                {
                    farmDataAvilable = false;
                }
            }
            catch (Exception ex)
            {
                farmDataAvilable = false;
            }
            return farm;
        }
        protected void btn_underAuditing_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/auditingRequests.aspx");
        }
        protected string GetAllRequestUnderAuditingCount()
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
                oracleCommand.CommandText = "select count(*) from VW_UNDER_AUDIT_REQUESTS";
                oracleCommand.CommandType = CommandType.Text;
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);

            }
            catch (Exception ex)
            {

            }
            return dataTable.Rows[0][0].ToString();
        }
    }
}