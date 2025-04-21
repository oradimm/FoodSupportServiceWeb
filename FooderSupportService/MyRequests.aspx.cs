using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FooderSupportService
{
    public partial class MyRequests : System.Web.UI.Page
    {
        protected static string UserQid { get; set; }
        protected static string UserId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["UserQid"] != null)
            {               
                UserQid = Session["UserQid"].ToString();
                UserId = Session["UserId"].ToString();
                UtilityHelper.UpdateLastActivityForUser(int.Parse(UserId));
                bool hasError = true;
                List <RequestObj> requestObjs = GetAllRequestsForUser(UserId, out hasError);
                BindMyRequestsTable(requestObjs,hasError);
                
            }
            else
            {
                Response.Redirect("~/Signin.aspx");
            }
        }
        protected List<RequestObj> GetAllRequestsForUser(string UserId, out bool hasError)
        {
            
            List<RequestObj> requestObjs = new List<RequestObj>();
            try
            {
                DataTable dataTable = new DataTable();
                OracleConnection oracleConnection = new OracleConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["FOODERSS_PRODConnString"].ConnectionString
                };
                OracleCommand oracleCommand = new OracleCommand();
                oracleCommand = oracleConnection.CreateCommand();
                oracleCommand.CommandText = "select * from WSS_REQUEST where USER_PROFILE=:UPROFILE order by REQ_DATE DESC";
                oracleCommand.CommandType = CommandType.Text;
                OracleParameter oracleParameter = new OracleParameter("UPROFILE", UserId);
                oracleCommand.Parameters.Add(oracleParameter);
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    RequestObj requestObj = new RequestObj()
                    {
                        ReqId = Convert.ToInt32(dataRow["REQ_ID"]),
                        ReqDate = Convert.ToDateTime(dataRow["REQ_DATE"]),
                        UserProfile = Convert.ToInt32(dataRow["USER_PROFILE"]),
                        //BankProfile = Convert.ToInt32(dataRow["BANK_PROFILE"]),
                        //WaterCertProfile = Convert.ToInt32(dataRow["WATER_CERT_PROFILE"]),
                        //Camels = Convert.ToInt32(dataRow["CAMELS"]),
                        //Cows = Convert.ToInt32(dataRow["COWS"]),
                        //Sheeps = Convert.ToInt32(dataRow["SHEEPS"]),
                        //Goats = Convert.ToInt32(dataRow["GOATS"]),
                        //Horses = Convert.ToInt32(dataRow["HORSES"]),
                        //Gazals = Convert.ToInt32(dataRow["GAZALS"]),
                        //CamelsWaterAmt = Convert.ToInt32(dataRow["CAMELS_WATER_AMT"]),
                        //CowsWaterAmt = Convert.ToInt32(dataRow["COWS_WATER_AMT"]),
                        //SheepsWaterAmt = Convert.ToInt32(dataRow["SHEEPS_WATER_AMT"]),
                        //GoatsWaterAmt = Convert.ToInt32(dataRow["GOATS_WATER_AMT"]),
                        //HorsesWaterAmt = Convert.ToInt32(dataRow["HORSES_WATER_AMT"]),
                        //GazalsWaterAmt = Convert.ToInt32(dataRow["GAZALS_WATER_AMT"]),
                        //WaterPrice = Convert.ToDouble(dataRow["WATER_PRICE"]),
                        ReviewStatus = Convert.ToInt32(dataRow["REVIEW_STATUS"]),
                        ReviewedDate = Convert.ToDateTime(dataRow["REVIEWED_DATE"]),
                        ReviewedBy = Convert.ToString(dataRow["REVIEWED_BY"]),
                        ReviewNotes = Convert.ToString(dataRow["REVIEW_NOTES"]) +" " + Convert.ToString(dataRow["AUDITING_NOTES"]),
                        ApprovalStatus = Convert.ToInt32(dataRow["APPROVAL_STATUS"]),
                        ApprovalBy = Convert.ToString(dataRow["APPROVAL_BY"]),
                        ApprovalNotes = Convert.ToString(dataRow["APPROVAL_NOTES"]),
                        SupportStart = Convert.ToDateTime(dataRow["SUPPORT_START"]),
                        SupportEnd = Convert.ToDateTime(dataRow["SUPPORT_END"])
                    };
                    if (dataRow["APPROVAL_DATE"] != DBNull.Value)
                    {
                        requestObj.ApprovalDate = Convert.ToDateTime(dataRow["APPROVAL_DATE"]);
                    }
                    else
                    {
                        requestObj.ApprovalDate = DateTime.MinValue;
                    }
                    requestObjs.Add(requestObj);
                }
                hasError = false;
            }
            catch (Exception ex)
            {
                requestObjs.Clear();
                hasError = true;
            }
           
            return requestObjs;
        }
        protected RequestObj GetRequestByReqId(string ReqId)
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
                oracleCommand.CommandText = "select * from WSS_REQUEST where REQ_ID=:REQ";
                oracleCommand.CommandType = CommandType.Text;
                OracleParameter oracleParameter = new OracleParameter("REQ", ReqId);
                oracleCommand.Parameters.Add(oracleParameter);
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow dataRow = dataTable.Rows[0];
                    RequestObj requestObj = new RequestObj()
                    {
                        ReqId = Convert.ToInt32(dataRow["REQ_ID"]),
                        ReqDate = Convert.ToDateTime(dataRow["REQ_DATE"]),
                        UserProfile = Convert.ToInt32(dataRow["USER_PROFILE"]),
                        //BankProfile = Convert.ToInt32(dataRow["BANK_PROFILE"]),
                        //WaterCertProfile = Convert.ToInt32(dataRow["WATER_CERT_PROFILE"]),
                        //Camels = Convert.ToInt32(dataRow["CAMELS"]),
                        //Cows = Convert.ToInt32(dataRow["COWS"]),
                        //Sheeps = Convert.ToInt32(dataRow["SHEEPS"]),
                        //Goats = Convert.ToInt32(dataRow["GOATS"]),
                        //Horses = Convert.ToInt32(dataRow["HORSES"]),
                        //Gazals = Convert.ToInt32(dataRow["GAZALS"]),
                        //CamelsWaterAmt = Convert.ToInt32(dataRow["CAMELS_WATER_AMT"]),
                        //CowsWaterAmt = Convert.ToInt32(dataRow["COWS_WATER_AMT"]),
                        //SheepsWaterAmt = Convert.ToInt32(dataRow["SHEEPS_WATER_AMT"]),
                        //GoatsWaterAmt = Convert.ToInt32(dataRow["GOATS_WATER_AMT"]),
                        //HorsesWaterAmt = Convert.ToInt32(dataRow["HORSES_WATER_AMT"]),
                        //GazalsWaterAmt = Convert.ToInt32(dataRow["GAZALS_WATER_AMT"]),
                        //WaterPrice = Convert.ToDouble(dataRow["WATER_PRICE"]),
                        ReviewStatus = Convert.ToInt32(dataRow["REVIEW_STATUS"]),
                        ReviewedDate = Convert.ToDateTime(dataRow["REVIEWED_DATE"]),
                        ReviewedBy = Convert.ToString(dataRow["REVIEWED_BY"]),
                        ReviewNotes = Convert.ToString(dataRow["REVIEW_NOTES"]),
                        ApprovalStatus = Convert.ToInt32(dataRow["APPROVAL_STATUS"]),
                        ApprovalDate = Convert.ToDateTime(dataRow["APPROVAL_DATE"]),
                        ApprovalBy = Convert.ToString(dataRow["APPROVAL_BY"]),
                        ApprovalNotes = Convert.ToString(dataRow["APPROVAL_NOTES"]),
                        SupportStart = Convert.ToDateTime(dataRow["SUPPORT_START"]),
                        SupportEnd = Convert.ToDateTime(dataRow["SUPPORT_END"])
                    };
                    return requestObj;
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
        protected void BindMyRequestsTable(List<RequestObj> requestObjs, bool hasError)
        {
            bool CanApply = true;
           
            if (requestObjs.Count > 0)
            {
                foreach (RequestObj requestObj in requestObjs)
                {
                    if (requestObj.ApprovalStatus == 0)
                    {
                        lbl_error_msg.Text = "* لديك طلب سابق تحت الإجراء, لا يمكن تقديم طلب جديد في الوقت الحالي";
                        btn_new_request.Enabled = false;
                        errorBox.Visible = true;
                        CanApply = false;
                    }
                    if (requestObj.ApprovalStatus == 1 && requestObj.SupportEnd > DateTime.Today)
                    {
                        lbl_error_msg.Text = "*لديك بالفعل طلب موافق عليه, يمكنك تقديم طلب جدبد بتاريخ" + ":"+requestObj.SupportEnd.AddDays(1).ToString("dd-MM-yyyy");
                        btn_new_request.Enabled = false;
                        errorBox.Visible = true;
                        CanApply = false;
                    }
                    TableRow tableRow = new TableRow();
                    TableCell tcRequestRef = new TableCell();
                    string requestRef = "WSS" + requestObj.ReqDate.ToString("yy") + requestObj.ReqDate.ToString("MM") + requestObj.ReqId.ToString();
                    LinkButton lbtn_open_request = new LinkButton();
                    lbtn_open_request.ID = requestRef;
                    lbtn_open_request.Attributes.Add("ReqId", requestObj.ReqId.ToString());
                    lbtn_open_request.Click += lbtn_open_request_Click;
                    lbtn_open_request.Text = requestRef;
                    tcRequestRef.Controls.Add(lbtn_open_request);
                    tableRow.Cells.Add(tcRequestRef);

                    TableCell tcRequestDate = new TableCell();
                    tcRequestDate.Text = requestObj.ReqDate.ToString("dd-MM-yyyy");
                    tableRow.Cells.Add(tcRequestDate);

                    TableCell tcStartDate = new TableCell();
                    if (requestObj.SupportStart != DateTime.MinValue)
                    {
                        tcStartDate.Text = requestObj.SupportStart.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        tcStartDate.Text = "---";
                    }
                    tableRow.Cells.Add(tcStartDate);

                    TableCell tcEndDate = new TableCell();
                    if (requestObj.SupportEnd != DateTime.MinValue)
                    {
                        tcEndDate.Text = requestObj.SupportEnd.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        tcEndDate.Text = "---";
                    }
                    tableRow.Cells.Add(tcEndDate);

                    TableCell tcBankName = new TableCell();
                    //tcBankName.Text = GetBankProfileForRequest(requestObj.BankProfile).BankName;
                    tableRow.Cells.Add(tcBankName);

                    TableCell tcIban = new TableCell();
                    //tcIban.Text = GetBankProfileForRequest(requestObj.BankProfile).IBAN;
                    tableRow.Cells.Add(tcIban);

                    TableCell tcStatus = new TableCell();
                    tcStatus.Text = GetRequestStatus(requestObj.ReviewStatus, requestObj.ApprovalStatus);
                    tableRow.Cells.Add(tcStatus);

                    TableCell tcNotes = new TableCell();
                    tcNotes.Text = requestObj.ReviewNotes + "<br/>" + requestObj.ApprovalNotes;
                    tableRow.Cells.Add(tcNotes);

                    tbl_myRequests.Rows.Add(tableRow);
                }
            }
            else
            {
                TableRow etr = new TableRow();
                TableCell etc = new TableCell();
                etc.Text = "لا يوجد لديك طلبات سابقة, يرجى اختيار -طلب جديد-";
                etc.ColumnSpan = 8;
                etr.Cells.Add(etc);
                tbl_myRequests.Rows.Add(etr);
                btn_new_request.Enabled = true;
                CanApply = true;
            }
            if (hasError)
            {
                lbl_error_msg.Text = "* حدث خطأ فني, يرجى التواصل مع خدمة العملاء على 184";
                btn_new_request.Enabled = false;
                errorBox.Visible = true;
                CanApply = false;
            }
            if (CanApply)
            {
                Session["CanApply"] = "True";
            }
            else
            {
                Session["CanApply"] = "False";
            }
        }
        protected BankProfileObj GetBankProfileForRequest(int BankProfileId)
        {
            BankProfileObj bankProfileObj = new BankProfileObj();
            try
            {
                DataTable dataTable = new DataTable();
                OracleConnection oracleConnection = new OracleConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["FOODERSS_PRODConnString"].ConnectionString
                };
                OracleCommand oracleCommand = new OracleCommand();
                oracleCommand = oracleConnection.CreateCommand();
                oracleCommand.CommandText = "select * from USER_BANK_INFO where ID=:UPROFILE";
                oracleCommand.CommandType = CommandType.Text;
                OracleParameter oracleParameter = new OracleParameter("UPROFILE", BankProfileId);
                oracleCommand.Parameters.Add(oracleParameter);
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow dataRow = dataTable.Rows[0];
                    bankProfileObj.Id = Convert.ToInt32(dataRow["ID"]);
                    bankProfileObj.IBAN = Convert.ToString(dataRow["IBAN1"]) + Convert.ToString(dataRow["IBAN2"]) + Convert.ToString(dataRow["IBAN3"]) + Convert.ToString(dataRow["IBAN4"]) + Convert.ToString(dataRow["IBAN5"]) + Convert.ToString(dataRow["IBAN6"]) + Convert.ToString(dataRow["IBAN7"]) + Convert.ToString(dataRow["IBAN8"]);
                    bankProfileObj.BankId = Convert.ToInt32(dataRow["BANK_ID"]);
                    switch (dataRow["BANK_ID"].ToString())
                    {
                        case "1":
                            bankProfileObj.BankName  = "بنك قطر الوطني";
                            break;
                        case "2":
                            bankProfileObj.BankName  = "مصرف قطر الإسلامي";
                            break;
                        case "3":
                            bankProfileObj.BankName  = "بنك قطر الدولي الإسلامي";
                            break;
                        case "4":
                            bankProfileObj.BankName  = "مصرف الريان";
                            break;
                        case "5":
                            bankProfileObj.BankName  = "البنك التجاري القطري";
                            break;
                        case "6":
                            bankProfileObj.BankName  = "بنك الدوحه";
                            break;
                        case "7":
                            bankProfileObj.BankName  = "بنك دخان";
                            break;
                        case "8":
                            bankProfileObj.BankName  = "البنك الأهلي";
                            break;
                        case "9":
                            bankProfileObj.BankName  = "البنك البريطاني";
                            break;
                        default:
                            bankProfileObj.BankName  = "";
                            break;
                    }

                }

            }
            catch (Exception ex)
            {

            }
            return bankProfileObj;
        }
        protected void btn_new_request_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Apply.aspx");
        }
        protected void lbtn_open_request_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            linkButton.ForeColor = Color.DarkRed;
            string reqId = ((LinkButton)sender).Attributes["ReqId"];
            calculations_panel.Visible = true;
            RequestObj requestObj = GetRequestByReqId(reqId);
            BindWaterCalculations(requestObj);
        }
        protected string GetRequestStatus(int ReviewStatus, int ApprovalStatus)
        {
            string reviewText = "";
            if (ApprovalStatus == 0)
            {
                if (ReviewStatus == 0)
                {
                    reviewText = "تحت المراجعة";
                }
                else if (ReviewStatus == 1)
                {
                    reviewText = "تحت الإجراء";
                }
                else if (ReviewStatus == 2)
                {
                    reviewText = "مرفوض";
                }
            }
            else if (ApprovalStatus == 1)
            {
                reviewText = "موافق عليه";
            }
            else if (ApprovalStatus == 2)
            {
                reviewText = "مرفوض";
            }
            return reviewText;
        }
        private void BindWaterCalculations(RequestObj requestObj)
        {
            //double waterGallonPrice = 0;
            //int camelWaterConsumption = 0;
            //int cowWaterConsumption = 0;
            //int goatlWaterConsumption = 0;
            //int sheeplWaterConsumption = 0;
            //int horseWaterConsumption = 0;
            //int gazalWaterConsumption = 0;
            //TableCell tcCamels_cont = new TableCell() { Text = requestObj.Camels.ToString() };
            //TableCell tcCows_cont = new TableCell() { Text = requestObj.Cows.ToString() };
            //TableCell tcGoats_cont = new TableCell() { Text = requestObj.Goats.ToString() };
            //TableCell tcSheeps_cont = new TableCell() { Text = requestObj.Sheeps.ToString() };
            //TableCell tcHorss_cont = new TableCell() { Text = requestObj.Horses.ToString() };
            //TableCell tcGazals_cont = new TableCell() { Text = requestObj.Gazals.ToString() };
            //tbl_calculations_total_animals_cell.Text = (requestObj.Camels + requestObj.Cows + requestObj.Goats + requestObj.Sheeps + requestObj.Horses + requestObj.Gazals).ToString();
            //tbl_calculations_Camels_row.Cells.Add(tcCamels_cont);
            //tbl_calculations_Cows_row.Cells.Add(tcCows_cont);
            //tbl_calculations_Goats_row.Cells.Add(tcGoats_cont);
            //tbl_calculations_Sheeps_row.Cells.Add(tcSheeps_cont);
            //tbl_calculations_Horses_row.Cells.Add(tcHorss_cont);
            //tbl_calculations_Gazals_row.Cells.Add(tcGazals_cont);
            //TableCell tc_camelWaterConsumption = new TableCell() { Text = (requestObj.Camels * camelWaterConsumption).ToString() };
            //TableCell tc_cowWaterConsumption = new TableCell() { Text = (requestObj.Cows * cowWaterConsumption).ToString() };
            //TableCell tc_goatlWaterConsumption = new TableCell() { Text = (requestObj.Goats * goatlWaterConsumption).ToString() };
            //TableCell tc_sheeplWaterConsumption = new TableCell() { Text = (requestObj.Sheeps * sheeplWaterConsumption).ToString() };
            //TableCell tc_horseWaterConsumption = new TableCell() { Text = (requestObj.Horses * horseWaterConsumption).ToString() };
            //TableCell tc_gazalWaterConsumption = new TableCell() { Text = (requestObj.Gazals * gazalWaterConsumption).ToString() };
            //tbl_calculations_total_water_cell.Text = (requestObj.Camels * camelWaterConsumption + requestObj.Cows * cowWaterConsumption + requestObj.Goats * goatlWaterConsumption + requestObj.Sheeps * sheeplWaterConsumption + requestObj.Horses * horseWaterConsumption + requestObj.Gazals * gazalWaterConsumption).ToString();
            //tbl_calculations_Camels_row.Cells.Add(tc_camelWaterConsumption);
            //tbl_calculations_Cows_row.Cells.Add(tc_cowWaterConsumption);
            //tbl_calculations_Goats_row.Cells.Add(tc_goatlWaterConsumption);
            //tbl_calculations_Sheeps_row.Cells.Add(tc_sheeplWaterConsumption);
            //tbl_calculations_Horses_row.Cells.Add(tc_horseWaterConsumption);
            //tbl_calculations_Gazals_row.Cells.Add(tc_gazalWaterConsumption);
            //TableCell tc_camelWaterConsumptionMonthlyValue = new TableCell() { Text = (requestObj.Camels * camelWaterConsumption * waterGallonPrice / 12).ToString() };
            //TableCell tc_cowWaterConsumptionMonthlyValue = new TableCell() { Text = (requestObj.Cows * cowWaterConsumption * waterGallonPrice / 12).ToString() };
            //TableCell tc_goatlWaterConsumptionMonthlyValue = new TableCell() { Text = (requestObj.Goats * goatlWaterConsumption * waterGallonPrice / 12).ToString() };
            //TableCell tc_sheeplWaterConsumptionMonthlyValue = new TableCell() { Text = (requestObj.Sheeps * sheeplWaterConsumption * waterGallonPrice / 12).ToString() };
            //TableCell tc_horseWaterConsumptionMonthlyValue = new TableCell() { Text = (requestObj.Horses * horseWaterConsumption * waterGallonPrice / 12).ToString() };
            //TableCell tc_gazalWaterConsumptionMonthlyValue = new TableCell() { Text = (requestObj.Gazals * gazalWaterConsumption * waterGallonPrice / 12).ToString() };
            //tbl_calculations_total_monthly_value_cell.Text = (requestObj.Camels * camelWaterConsumption * waterGallonPrice / 12 + requestObj.Cows * cowWaterConsumption * waterGallonPrice / 12 + requestObj.Goats * goatlWaterConsumption * waterGallonPrice / 12 + requestObj.Sheeps * sheeplWaterConsumption * waterGallonPrice / 12 + requestObj.Horses * horseWaterConsumption * waterGallonPrice / 12 + requestObj.Gazals * gazalWaterConsumption * waterGallonPrice / 12).ToString();
            //tbl_calculations_Camels_row.Cells.Add(tc_camelWaterConsumptionMonthlyValue);
            //tbl_calculations_Cows_row.Cells.Add(tc_cowWaterConsumptionMonthlyValue);
            //tbl_calculations_Goats_row.Cells.Add(tc_goatlWaterConsumptionMonthlyValue);
            //tbl_calculations_Sheeps_row.Cells.Add(tc_sheeplWaterConsumptionMonthlyValue);
            //tbl_calculations_Horses_row.Cells.Add(tc_horseWaterConsumptionMonthlyValue);
            //tbl_calculations_Gazals_row.Cells.Add(tc_gazalWaterConsumptionMonthlyValue);
            //TableCell tc_camelWaterConsumptionYearlyValue = new TableCell() { Text = (requestObj.Camels * camelWaterConsumption * waterGallonPrice).ToString() };
            //TableCell tc_cowWaterConsumptionYearlyValue = new TableCell() { Text = (requestObj.Cows * cowWaterConsumption * waterGallonPrice).ToString() };
            //TableCell tc_goatlWaterConsumptionYearlylyValue = new TableCell() { Text = (requestObj.Goats * goatlWaterConsumption * waterGallonPrice).ToString() };
            //TableCell tc_sheeplWaterConsumptionYearlylyValue = new TableCell() { Text = (requestObj.Sheeps * sheeplWaterConsumption * waterGallonPrice).ToString() };
            //TableCell tc_horseWaterConsumptionYearlyValue = new TableCell() { Text = (requestObj.Horses * horseWaterConsumption * waterGallonPrice).ToString() };
            //TableCell tc_gazalWaterConsumptionYearlyyValue = new TableCell() { Text = (requestObj.Gazals * gazalWaterConsumption * waterGallonPrice).ToString() };
            //tbl_calculations_total_yearly_value_cell.Text = (requestObj.Camels * camelWaterConsumption * waterGallonPrice + requestObj.Cows * cowWaterConsumption * waterGallonPrice + requestObj.Goats * goatlWaterConsumption * waterGallonPrice + requestObj.Sheeps * sheeplWaterConsumption * waterGallonPrice + requestObj.Horses * horseWaterConsumption * waterGallonPrice + requestObj.Gazals * gazalWaterConsumption * waterGallonPrice).ToString();
            //tbl_calculations_Camels_row.Cells.Add(tc_camelWaterConsumptionYearlyValue);
            //tbl_calculations_Cows_row.Cells.Add(tc_cowWaterConsumptionYearlyValue);
            //tbl_calculations_Goats_row.Cells.Add(tc_goatlWaterConsumptionYearlylyValue);
            //tbl_calculations_Sheeps_row.Cells.Add(tc_sheeplWaterConsumptionYearlylyValue);
            //tbl_calculations_Horses_row.Cells.Add(tc_horseWaterConsumptionYearlyValue);
            //tbl_calculations_Gazals_row.Cells.Add(tc_gazalWaterConsumptionYearlyyValue);
        }
    }
}