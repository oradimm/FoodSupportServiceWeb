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
                oracleCommand.CommandText = "select * from REQUESTs where USER_PROFILE=:UPROFILE order by REQ_DATE DESC";
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
                        Sheeps = Convert.ToInt32(dataRow["SHEEPS"]),
                        ReviewNotes = Convert.ToString(dataRow["REVIEW_NOTES"]),
                        ApprovalStatus = Convert.ToInt32(dataRow["APPROVAL_STATUS"]),
                    };
                    if (dataRow["TOTAL_GRASS_F_BAG"] != DBNull.Value)
                    {
                        requestObj.TotalGrassFooderBag = Convert.ToInt32(dataRow["TOTAL_GRASS_F_BAG"]);
                    }
                    else
                    {
                        requestObj.TotalGrassFooderBag = -1;
                    }
                    if (dataRow["TOTAL_PROTEIN_F_BAG"] != DBNull.Value)
                    {
                        requestObj.TotalProteinFooderBag = Convert.ToInt32(dataRow["TOTAL_PROTEIN_F_BAG"]);
                    }
                    else
                    {
                        requestObj.TotalProteinFooderBag = -1;
                    }
                    if (dataRow["APPROVAL_DATE"] != DBNull.Value)
                    {
                        requestObj.ApprovalDate = Convert.ToDateTime(dataRow["APPROVAL_DATE"]);
                    }
                    else
                    {
                        requestObj.ApprovalDate = DateTime.MinValue;
                    }
                    if (dataRow["SUPPORT_START"] != DBNull.Value)
                    {
                        requestObj.SupportStart = Convert.ToDateTime(dataRow["SUPPORT_START"]);
                    }
                    else
                    {
                        requestObj.SupportStart = DateTime.MinValue;
                    }
                    if (dataRow["SUPPORT_END"] != DBNull.Value)
                    {
                        requestObj.SupportEnd = Convert.ToDateTime(dataRow["SUPPORT_END"]);
                    }
                    else
                    {
                        requestObj.SupportEnd = DateTime.MinValue;
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
                        lbl_error_msg.Text = "*لديك بالفعل طلب موافق عليه, يمكنك تقديم طلب جدبد بتاريخ" + ":"+requestObj.SupportEnd.Value.AddDays(1).ToString("dd-MM-yyyy");
                        btn_new_request.Enabled = false;
                        errorBox.Visible = true;
                        CanApply = false;
                    }
                    TableRow tableRow = new TableRow();
                    TableCell tcRequestRef = new TableCell();
                    string requestRef = "FSS" + requestObj.ReqDate.ToString("yy") + requestObj.ReqDate.ToString("MM") + requestObj.ReqId.ToString();
                    LinkButton lbtn_open_request = new LinkButton();
                    lbtn_open_request.ID = requestRef;
                    lbtn_open_request.Attributes.Add("ReqId", requestObj.ReqId.ToString());
                  
                    lbtn_open_request.Text = requestRef;
                    tcRequestRef.Controls.Add(lbtn_open_request);
                    tableRow.Cells.Add(tcRequestRef);

                    TableCell tcRequestDate = new TableCell();
                    tcRequestDate.Text = requestObj.ReqDate.ToString("dd-MM-yyyy");
                    tableRow.Cells.Add(tcRequestDate);

                    

                    TableCell tcSheeps = new TableCell();
                    tcSheeps.Text = requestObj.Sheeps.ToString();
                    tableRow.Cells.Add(tcSheeps);


                    TableCell tcTotalProteinFooderBag = new TableCell();
                    if (requestObj.TotalProteinFooderBag != -1)
                    {
                        tcTotalProteinFooderBag.Text = requestObj.TotalProteinFooderBag.ToString();
                    }
                    else
                    {
                        tcTotalProteinFooderBag.Text = "---";
                    }
                    tableRow.Cells.Add(tcTotalProteinFooderBag);

                    TableCell tcTotalGrassFooderBag = new TableCell();
                    if (requestObj.TotalGrassFooderBag != -1)
                    {
                        tcTotalGrassFooderBag.Text = requestObj.TotalGrassFooderBag.ToString(); 
                    }
                    else
                    {
                        tcTotalGrassFooderBag.Text = "---";
                    }
                    tableRow.Cells.Add(tcTotalGrassFooderBag);


                    TableCell tcStartDate = new TableCell();
                    if (requestObj.SupportStart != DateTime.MinValue)
                    {
                        tcStartDate.Text = requestObj.SupportStart.Value.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        tcStartDate.Text = "---";
                    }
                    tableRow.Cells.Add(tcStartDate);

                    TableCell tcEndDate = new TableCell();
                    if (requestObj.SupportEnd != DateTime.MinValue)
                    {
                        tcEndDate.Text = requestObj.SupportEnd.Value.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        tcEndDate.Text = "---";
                    }
                    tableRow.Cells.Add(tcEndDate);

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
        protected void btn_new_request_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Apply.aspx");
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
    }
}