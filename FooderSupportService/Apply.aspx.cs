using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FooderSupportService
{
    public partial class Apply : System.Web.UI.Page
    {
       
        protected  string UserQid { get; set; }
        protected  string UserMobile { get; set; }
        protected  string UserName { get; set; }
        protected  string UserId { get; set; }
        protected  RequestObj currentRequest { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CanApply"] = "True";
            Session["UserQid"] = "28840000698";
            Session["UserMobile"] = "74000797";
            Session["UserName"] = "عمرا راضي تجريبي";
            Session["UserId"] = "-5";
            if (Session["CanApply"] == "True")
            {
                if (Session["UserQid"] != null)
                {
                    
                    currentRequest = new RequestObj();
                    UserQid = Session["UserQid"].ToString();
                    //UserQid ="26563400704"; ms hamda user
                    txt_qid.Text = UserQid;
                    UserMobile = Session["UserMobile"].ToString();
                    txt_mobile.Text = UserMobile;
                    UserName = Session["UserName"].ToString();
                    txt_name.Text = UserName;
                    txt_applied_Date.Text = DateTime.Today.ToString("dd-MM-yyyy");
                    UserId = Session["UserId"].ToString();
                    UtilityHelper.UpdateLastActivityForUser(int.Parse(UserId));
                    GetOwnershipsData();
                    txt_total_sheeps.Text = currentRequest.Sheeps.ToString();
                    currentRequest.UserProfile = int.Parse(UserId);
                    currentRequest.ReqDate = DateTime.Now;
                    currentRequest.ReviewStatus = 0;
                    currentRequest.ApprovalStatus = 0;
                    currentRequest.SupportStart = GetRequestStartDate(int.Parse(UserId));
                    currentRequest.SupportEnd = DateTime.MinValue;

                }
                else
                {
                    Response.Redirect("~/Signin.aspx");
                }
            }
            else
            {
                Response.Redirect("~/MyRequests.aspx");
            }
        }
        private DateTime GetRequestStartDate(int UserId)
        {
            DateTime date = DateTime.Today;
            try
            {
                DataTable dataTable = new DataTable();
                OracleConnection oracleConnection = new OracleConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["FOODERSS_PRODConnString"].ConnectionString
                };
                OracleCommand oracleCommand = new OracleCommand();
                oracleCommand = oracleConnection.CreateCommand();
                oracleCommand.CommandText = "SELECT (SUPPORT_END + 1) NEW_REQUEST_DAY FROM WSS_REQUEST WHERE USER_PROFILE=:USER_ID AND APPROVAL_STATUS=1 AND SUPPORT_END=(SELECT MAX(SUPPORT_END) FROM WSS_REQUEST WHERE USER_PROFILE=:USER_ID AND APPROVAL_STATUS=1)";
                oracleCommand.CommandType = CommandType.Text;
                OracleParameter oracleParameter = new OracleParameter("USER_ID", UserId);
                oracleCommand.Parameters.Add(oracleParameter);
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    date = Convert.ToDateTime(dataTable.Rows[0]["NEW_REQUEST_DAY"].ToString());
                }
            }
            catch (Exception ex)
            {
            }
            return DateTime.MinValue;
        }
        private void GetOwnershipsData()
        {
            List<OwnershipObj> ownershipObjs = new List<OwnershipObj>();
            RequestObj requestObj = new RequestObj();
            ownershipObjs = GetUserOwnerships(UserQid);
            foreach (OwnershipObj ownership in ownershipObjs)
            {
                OwnershipObj curOwnership = ownership;
                bool izbaInfoHasError = false;
                if (!(curOwnership.TYPE_NUMBER == 2))
                {
                    IzbaObj izbaInfo = GetIzbaInfo(UserQid, curOwnership.LIC_NUMBER, out izbaInfoHasError);
                    if (!izbaInfoHasError)
                    {
                        if (izbaInfo != null)
                        {
                            DateTime IzbaEndDate = new DateTime();
                            IzbaEndDate = Convert.ToDateTime(DateTime.ParseExact(izbaInfo.CONTRACTEND, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
                            DateTime ExtandedIzbaEndDate = IzbaEndDate.AddMonths(0);
                            if (ExtandedIzbaEndDate < DateTime.Today)
                            {
                                curOwnership.CanApply = false;
                                curOwnership.notes.Add("عقد العزبة منتهي بتاريخ: " + ExtandedIzbaEndDate.ToString("dd-MM-yyyy") + " يرجى تجديد عقد العزبة");
                            }
                            
                            
                        }
                        else
                        {
                            curOwnership.CanApply = false;
                            curOwnership.notes.Add("تعذر الحصول على بيانات العزبة يرجى مراجعة ادارة الثروة الحيوانية");
                        }
                    }
                    else
                    {
                        curOwnership.CanApply = false;
                        curOwnership.notes.Add("لايمكن اختيار هذه العزبة بسبب خلل فني, يرجى المحاولة لاحقا");
                    }
                }
                TableCell tableCell_Sheeps = new TableCell();
                TableCell tableCell_FarmExpDate = new TableCell();
                if (curOwnership.TYPE_NUMBER == 2)
                {
                    bool farmCardDataAvilable;
                    bool farmDataAvilable;
                    curOwnership = GetFarmData(ownership, UserQid, out farmCardDataAvilable, out farmDataAvilable);
                    tableCell_Sheeps.Text = curOwnership.Sheeps.ToString();
                    tableCell_FarmExpDate.Text = curOwnership.FARM_EXP_DATE.ToString("dd-MM-yyyy");
                    if (!farmCardDataAvilable)
                    {
                        curOwnership.CanApply = false;
                        curOwnership.notes.Add("فشل التحقق من بيانات بطاقة المزرعة, يرجى مراجعة ادارة الثروة الحيوانية");
                    }
                    if (!farmDataAvilable)
                    {
                        curOwnership.CanApply = false;
                        curOwnership.notes.Add("فشل التحقق من بيانات المزرعة, يرجى مراجعة ادارة الثروة الحيوانية");
                    }
                   
                    
                }
                else
                {
                    tableCell_Sheeps.Text = curOwnership.Sheeps.ToString();
                    tableCell_FarmExpDate.Text = "---";
                }
                TableRow tableRow = new TableRow();

                TableCell tableCell_OwnershipId = new TableCell();
                TableCell tableCell_OwnershipType = new TableCell();
                TableCell tableCell_Location = new TableCell();
                TableCell tableCell_LastNumbring = new TableCell();
                TableCell tableCell_Notes = new TableCell();


                tableCell_OwnershipId.Text = curOwnership.OWNERSHIP_ID.ToString();
                tableCell_OwnershipType.Text = curOwnership.TYPE;
                tableCell_Location.Text = curOwnership.LOCATION;
                tableCell_LastNumbring.Text = curOwnership.REGISTERATION_DATE.ToString("dd-MM-yyyy");
                string notes = "<ul class='ul-error'>";
                if (curOwnership.notes.Count > 0)
                {
                    foreach (string note in curOwnership.notes)
                    {
                        notes = notes + "<li>" + note + "</li>";
                    }
                    notes = notes + "</ul>";
                }
                else
                {
                    notes = "---";
                }
                tableCell_Notes.Text = notes;
                tableRow.Cells.Add(tableCell_OwnershipId);
                tableRow.Cells.Add(tableCell_OwnershipType);
                tableRow.Cells.Add(tableCell_Location);
                tableRow.Cells.Add(tableCell_Sheeps);
                tableRow.Cells.Add(tableCell_FarmExpDate);
                tableRow.Cells.Add(tableCell_LastNumbring);
                if (curOwnership.CanApply)
                {
                    requestObj.Sheeps = requestObj.Sheeps + curOwnership.Sheeps;
                    currentRequest.Sheeps = requestObj.Sheeps;
                    tbl_valid_ownerships.Rows.Add(tableRow);
                }
                else
                {
                    tableRow.Cells.Add(tableCell_Notes);
                    tbl_not_valid_ownerships.Rows.Add(tableRow);
                }
            }
            if (tbl_valid_ownerships.Rows.Count < 2)
            {
                TableRow etr = new TableRow();
                TableCell etc = new TableCell();
                etc.Text = "لا يوجد لديك مواقع مستوفية لشروط الخدمة";
                etc.ColumnSpan = 6;
                etr.Cells.Add(etc);
                tbl_valid_ownerships.Rows.Add(etr);
                btn_save.Visible = false;
            }
            else
            {
                btn_save.Visible = true;
            }
            if (tbl_not_valid_ownerships.Rows.Count < 1)
            {
                TableRow etr = new TableRow();
                TableCell etc = new TableCell();
                etc.Text = "لا يوجد لديك مواقع غير مستوفية لشروط الخدمة";
                etc.ColumnSpan = 7;
                etr.Cells.Add(etc);
                tbl_not_valid_ownerships.Rows.Add(etr);
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
                        Sheeps = Convert.ToInt32(dataRow["Sheeps"]),
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
                    if (ownershipObj.REGISTERATION_DATE.AddYears(1) < DateTime.Today)
                    {
                        ownershipObj.CanApply = false;
                        ownershipObj.notes.Add("لقد مر اكثر من سنة على تاريخ اخر حصر يرجى طلب موعد ترقيم");
                    }
                   

                    ownershipObjs.Add(ownershipObj);
                }
            }
            catch (Exception ex)
            {

            }
            return ownershipObjs;
        }
        private static OwnershipObj GetFarmData(OwnershipObj farm, string UserQid, out bool farmCardDataAvilable, out bool farmDataAvilable)
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
                    farmCardDataAvilable = true;
                    if (farm.FARM_EXP_DATE < DateTime.Today)
                    {
                        farm.CanApply = false;
                        farm.notes.Add("صلاحية المزرعة منتهية");
                    }
                }
                else
                {
                    farmCardDataAvilable = false;
                }
            }
            catch (Exception ex)
            {
                farmCardDataAvilable = false;
            }
            try
            {
                DataTable dataTable = new DataTable();
                OracleConnection oracleConnection = new OracleConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["AGRI_ROConnString"].ConnectionString
                };
                OracleCommand oracleCommand = new OracleCommand();
                oracleCommand = oracleConnection.CreateCommand();
                oracleCommand.CommandText = "select  ENTITY_VALUE from ESERV.AG_FARMS_V F INNER JOIN ESERV.AG_FARMS_HOLDERS_V FH ON F.UUID = FH.P_UUID WHERE FARM_NUMBER=:FARM_NO";
                oracleCommand.CommandType = CommandType.Text;
                OracleParameter oracleParameter = new OracleParameter("FARM_NO", farm.LIC_NUMBER.ToString());
                oracleCommand.Parameters.Add(oracleParameter);
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
                
                if (dataTable.Rows.Count > 0)
                {
                    farmDataAvilable = true;
                    List<string> listOfOwners = new List<string>();
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        listOfOwners.Add(dr["ENTITY_VALUE"].ToString());
                    }
                    if (!listOfOwners.Contains(UserQid))
                    {
                        farm.CanApply = false;
                        farm.notes.Add("لايوجد للمستخدم سجل حيازة مزرعة يرجى مراجعة ادارة الشؤون الزراعية");
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
        public static IzbaObj GetIzbaInfo(string QID, int RequestNumber, out bool hasError)
        {
            if (QID == "28840000698" && RequestNumber == 235)
            {
                hasError = false;
                return new IzbaObj()
                {
                    COMPSEQ = "235",
                    COMPID = 0,
                    OWNERID = "28840000698",
                    OWNERGSM = "74000797",
                    CONTRACTSTART = "9/28/2024 12:00:00 AM",
                    CONTRACTEND = "9/28/2025 12:00:00 AM",
                    OWNERNAME = "عمر راضي حسني ذباح الجمل",
                    IZBA_TYPE = "مجمعات",
                    IZBA_STATUS = "نشطة",
                    LANDS_TOTAL = "1",
                    IZBA_SERIAL = "M/05/02/235",
                    IZBAID = "5826",
                    IZBA_DESC = "مجمع الخور2"
                };
            }
            else
            {
                try
                {
                    string token = CreateNewToken();
                    var client = new RestClient("https://animalcert.mme.gov.qa/IZAB_WEBSERVICE_ANIMALRES/api/data/GetIzbadata");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Authorization", $"Bearer {token}");
                    request.AddHeader("Content-Type", "application/json");
                    //var body = @"{""QID"": ""25663400962"" ,""REQUEST_NUMBER"": ""343""}";
                    RequestbodyModel requestbody = new RequestbodyModel() { QID = QID, REQUEST_NUMBER = RequestNumber };
                    request.AddParameter("application/json", JsonConvert.SerializeObject(requestbody), ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    List<IzbaObj> izbaList = JsonConvert.DeserializeObject<List<IzbaObj>>(response.Content);
                    hasError = false;
                    if (izbaList.Count > 0)
                    {
                        return izbaList[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    hasError = true;
                    return null;
                }
            }
        }
        public static string CreateNewToken()
        {
            var client = new RestClient("https://animalcert.mme.gov.qa/IZAB_WEBSERVICE_ANIMALRES/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("Username", "IZAB");
            request.AddParameter("Password", "IZAB@2022!");
            request.AddParameter("grant_type", "password");
            IRestResponse response = client.Execute(request);
            TokenModel token = JsonConvert.DeserializeObject<TokenModel>(response.Content);
            return token.access_token;
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (cb_agree.Checked)
            {
                SaveRequest();
            }
            else
            {

                lbl_error_msg.Text = "يرجى أختيار (اقر بكامل العلم بصحة جميع المعلومات المقدمة في الطلب)";
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
                        ReviewNotes = Convert.ToString(dataRow["REVIEW_NOTES"]) + " " + Convert.ToString(dataRow["AUDITING_NOTES"]),
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
        public void SaveRequest()
        {
            bool hasError = true;
            List<RequestObj> requestObjs = GetAllRequestsForUser(UserId, out hasError);
            bool CanApply = true;

            if (requestObjs.Count > 0)
            {
                foreach (RequestObj requestObj in requestObjs)
                {
                    if (requestObj.ApprovalStatus == 0)
                    {

                        CanApply = false;
                    }
                    if (requestObj.ApprovalStatus == 1 && requestObj.SupportEnd > DateTime.Today)
                    {

                        CanApply = false;
                    }
                }
            }
            if (CanApply)
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
                    oracleCommand.CommandText = @"insert into WSS_REQUEST( SHEEPS,COWS_WATER_AMT
,WATER_CERT_PROFILE,APPROVAL_STATUS,WATER_PRICE,REVIEW_NOTES,REQ_DATE,GOATS_WATER_AMT,GOATS,USER_PROFILE,COWS,GAZALS_WATER_AMT,REVIEW_STATUS,APPROVAL_BY,
APPROVAL_DATE,APPROVAL_NOTES,HORSES_WATER_AMT,SUPPORT_END,HORSES,CAMELS_WATER_AMT,GAZALS,CAMELS,BANK_PROFILE,SHEEPS_WATER_AMT,SUPPORT_START,REVIEWED_DATE,REVIEWED_BY) 
values (:p_SHEEPS,:p_COWS_WATER_AMT,:p_WATER_CERT_PROFILE,:p_APPROVAL_STATUS,:p_WATER_PRICE,:p_REVIEW_NOTES,:p_REQ_DATE,:p_GOATS_WATER_AMT,:p_GOATS,:p_USER_PROFILE
,:p_COWS,:p_GAZALS_WATER_AMT,:p_REVIEW_STATUS,:p_APPROVAL_BY,:p_APPROVAL_DATE,:p_APPROVAL_NOTES,:p_HORSES_WATER_AMT,:p_SUPPORT_END,:p_HORSES
,:p_CAMELS_WATER_AMT,:p_GAZALS,:p_CAMELS,:p_BANK_PROFILE,:p_SHEEPS_WATER_AMT,:p_SUPPORT_START,:p_REVIEWED_DATE,:p_REVIEWED_BY) RETURNING REQ_ID INTO :insertedId";

                    oracleCommand.CommandType = CommandType.Text;
                    OracleParameter p_SHEEPS = new OracleParameter("p_SHEEPS", OracleDbType.Int32);
                    p_SHEEPS.Direction = ParameterDirection.Input;
                    p_SHEEPS.Value = currentRequest.Sheeps;
                    oracleCommand.Parameters.Add(p_SHEEPS);

                    OracleParameter p_COWS_WATER_AMT = new OracleParameter("p_COWS_WATER_AMT", OracleDbType.Int32);
                    p_COWS_WATER_AMT.Direction = ParameterDirection.Input;
                   // p_COWS_WATER_AMT.Value = currentRequest.CowsWaterAmt;
                    oracleCommand.Parameters.Add(p_COWS_WATER_AMT);

                    OracleParameter p_WATER_CERTP_ROFILE = new OracleParameter("p_WATER_CERT_PROFILE", OracleDbType.Int32);
                    p_WATER_CERTP_ROFILE.Direction = ParameterDirection.Input;
                   // p_WATER_CERTP_ROFILE.Value = currentRequest.WaterCertProfile;
                    oracleCommand.Parameters.Add(p_WATER_CERTP_ROFILE);

                    OracleParameter p_APPROVAL_STATUS = new OracleParameter("p_APPROVAL_STATUS", OracleDbType.Int32);
                    p_APPROVAL_STATUS.Direction = ParameterDirection.Input;
                    p_APPROVAL_STATUS.Value = currentRequest.ApprovalStatus;
                    oracleCommand.Parameters.Add(p_APPROVAL_STATUS);

                    OracleParameter p_WATER_PRICE = new OracleParameter("p_WATER_PRICE", OracleDbType.Decimal);
                    p_WATER_PRICE.Direction = ParameterDirection.Input;
                    //p_WATER_PRICE.Value = currentRequest.WaterPrice;
                    oracleCommand.Parameters.Add(p_WATER_PRICE);

                    OracleParameter p_REVIEW_NOTES = new OracleParameter("p_REVIEW_NOTES", OracleDbType.Varchar2);
                    p_REVIEW_NOTES.Direction = ParameterDirection.Input;
                    p_REVIEW_NOTES.Value = currentRequest.ReviewNotes;
                    oracleCommand.Parameters.Add(p_REVIEW_NOTES);

                    OracleParameter p_REQ_DATE = new OracleParameter("p_REQ_DATE", OracleDbType.Date);
                    p_REQ_DATE.Direction = ParameterDirection.Input;
                    p_REQ_DATE.Value = currentRequest.ReqDate;
                    oracleCommand.Parameters.Add(p_REQ_DATE);

                    OracleParameter p_GOATS_WATER_AMT = new OracleParameter("p_GOATS_WATER_AMT", OracleDbType.Int32);
                    p_GOATS_WATER_AMT.Direction = ParameterDirection.Input;
                    //p_GOATS_WATER_AMT.Value = currentRequest.GoatsWaterAmt;
                    oracleCommand.Parameters.Add(p_GOATS_WATER_AMT);

                    OracleParameter p_GOATS = new OracleParameter("p_GOATS", OracleDbType.Int32);
                    p_GOATS.Direction = ParameterDirection.Input;
                   // p_GOATS.Value = currentRequest.Goats;
                    oracleCommand.Parameters.Add(p_GOATS);

                    OracleParameter p_USER_PROFILE = new OracleParameter("p_USER_PROFILE", OracleDbType.Int32);
                    p_USER_PROFILE.Direction = ParameterDirection.Input;
                    p_USER_PROFILE.Value = currentRequest.UserProfile;
                    oracleCommand.Parameters.Add(p_USER_PROFILE);

                    OracleParameter p_COWS = new OracleParameter("p_COWS", OracleDbType.Int32);
                    p_COWS.Direction = ParameterDirection.Input;
                   // p_COWS.Value = currentRequest.Cows;
                    oracleCommand.Parameters.Add(p_COWS);

                    OracleParameter p_GAZALS_WATER_AMT = new OracleParameter("p_GAZALS_WATER_AMT", OracleDbType.Int32);
                    p_GAZALS_WATER_AMT.Direction = ParameterDirection.Input;
                    //p_GAZALS_WATER_AMT.Value = currentRequest.GazalsWaterAmt;
                    oracleCommand.Parameters.Add(p_GAZALS_WATER_AMT);

                    OracleParameter p_REVIEW_STATUS = new OracleParameter("p_REVIEW_STATUS", OracleDbType.Int32);
                    p_REVIEW_STATUS.Direction = ParameterDirection.Input;
                    p_REVIEW_STATUS.Value = currentRequest.ReviewStatus;
                    oracleCommand.Parameters.Add(p_REVIEW_STATUS);

                    OracleParameter p_APPROVAL_BY = new OracleParameter("p_APPROVAL_BY", OracleDbType.Varchar2);
                    p_APPROVAL_BY.Direction = ParameterDirection.Input;
                    p_APPROVAL_BY.Value = currentRequest.ApprovalBy;
                    oracleCommand.Parameters.Add(p_APPROVAL_BY);

                    OracleParameter p_APPROVAL_DATE = new OracleParameter("p_APPROVAL_DATE", OracleDbType.Date);
                    p_APPROVAL_DATE.Direction = ParameterDirection.Input;
                    p_APPROVAL_DATE.Value = currentRequest.ApprovalDate;
                    oracleCommand.Parameters.Add(p_APPROVAL_DATE);

                    OracleParameter p_APPROVAL_NOTES = new OracleParameter("p_APPROVAL_NOTES", OracleDbType.Varchar2);
                    p_APPROVAL_NOTES.Direction = ParameterDirection.Input;
                    p_APPROVAL_NOTES.Value = currentRequest.ApprovalNotes;
                    oracleCommand.Parameters.Add(p_APPROVAL_NOTES);

                    OracleParameter p_HORSES_WATER_AMT = new OracleParameter("p_HORSES_WATER_AMT", OracleDbType.Varchar2);
                    p_HORSES_WATER_AMT.Direction = ParameterDirection.Input;
                    //p_HORSES_WATER_AMT.Value = currentRequest.HorsesWaterAmt;
                    oracleCommand.Parameters.Add(p_HORSES_WATER_AMT);

                    OracleParameter p_SUPPORT_END = new OracleParameter("p_SUPPORT_END", OracleDbType.Date);
                    p_SUPPORT_END.Direction = ParameterDirection.Input;
                    p_SUPPORT_END.Value = currentRequest.SupportEnd;
                    oracleCommand.Parameters.Add(p_SUPPORT_END);

                    OracleParameter p_HORSES = new OracleParameter("p_HORSES", OracleDbType.Int32);
                    p_HORSES.Direction = ParameterDirection.Input;
                   // p_HORSES.Value = currentRequest.Horses;
                    oracleCommand.Parameters.Add(p_HORSES);

                    OracleParameter p_CAMELS_WATER_AMT = new OracleParameter("p_CAMELS_WATER_AMT", OracleDbType.Int32);
                    p_CAMELS_WATER_AMT.Direction = ParameterDirection.Input;
                    //p_CAMELS_WATER_AMT.Value = currentRequest.CamelsWaterAmt;
                    oracleCommand.Parameters.Add(p_CAMELS_WATER_AMT);

                    OracleParameter p_GAZALS = new OracleParameter("p_GAZALS", OracleDbType.Int32);
                    p_GAZALS.Direction = ParameterDirection.Input;
                   // p_GAZALS.Value = currentRequest.Gazals;
                    oracleCommand.Parameters.Add(p_GAZALS);

                    OracleParameter p_CAMELS = new OracleParameter("p_CAMELS", OracleDbType.Int32);
                    p_CAMELS.Direction = ParameterDirection.Input;
                  //  p_CAMELS.Value = currentRequest.Camels;
                    oracleCommand.Parameters.Add(p_CAMELS);

                    OracleParameter p_BANK_PROFILE = new OracleParameter("p_BANK_PROFILE", OracleDbType.Int32);
                    p_BANK_PROFILE.Direction = ParameterDirection.Input;
                  //  p_BANK_PROFILE.Value = currentRequest.BankProfile;
                    oracleCommand.Parameters.Add(p_BANK_PROFILE);

                    OracleParameter p_SHEEPS_WATER_AMT = new OracleParameter("p_SHEEPS_WATER_AMT", OracleDbType.Int32);
                    p_SHEEPS_WATER_AMT.Direction = ParameterDirection.Input;
                   // p_SHEEPS_WATER_AMT.Value = currentRequest.SheepsWaterAmt;
                    oracleCommand.Parameters.Add(p_SHEEPS_WATER_AMT);

                    OracleParameter p_SUPPORT_START = new OracleParameter("p_SUPPORT_START", OracleDbType.Date);
                    p_SUPPORT_START.Direction = ParameterDirection.Input;
                    p_SUPPORT_START.Value = currentRequest.SupportStart;
                    oracleCommand.Parameters.Add(p_SUPPORT_START);

                    OracleParameter p_REVIEWED_DATE = new OracleParameter("p_REVIEWED_DATE", OracleDbType.Date);
                    p_REVIEWED_DATE.Direction = ParameterDirection.Input;
                    p_REVIEWED_DATE.Value = currentRequest.ReviewedDate;
                    oracleCommand.Parameters.Add(p_REVIEWED_DATE);

                    OracleParameter p_REVIEWED_BY = new OracleParameter("p_REVIEWED_BY", OracleDbType.NVarchar2);
                    p_REVIEWED_BY.Direction = ParameterDirection.Input;
                    p_REVIEWED_BY.Value = currentRequest.ReviewedBy;
                    oracleCommand.Parameters.Add(p_REVIEWED_BY);

                    oracleCommand.Parameters.Add("insertedId", OracleDbType.Int32).Direction = System.Data.ParameterDirection.Output;

                    oracleConnection.Open();
                    oracleCommand.ExecuteNonQuery();
                    object id = oracleCommand.Parameters["insertedId"].Value;
                    int recordId = int.Parse(id.ToString());
                    oracleConnection.Close();

                    string requestRef = "WSS" + DateTime.Today.ToString("yy") + DateTime.Today.ToString("MM") + recordId.ToString();
                    UtilityHelper.SendSms(UserMobile, "تم تقديم طلب خدمة دعم المياه بنجاح, رقم الطلب" + ":" + requestRef + " حالة الطلب تحت المراجعة و يمكن متابعة الطلب من خلال موقع الوزارة او تطبيق عون");
                    Response.Redirect("~/Success.aspx?req=" + requestRef);

                }
                catch (Exception ex)
                {

                }
            }
        }
        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Myrequests.aspx");
        }
        protected void cb_agree_CheckedChanged(object sender, EventArgs e)
        {
            lbl_error_msg.Text = "";
        }
    }
}