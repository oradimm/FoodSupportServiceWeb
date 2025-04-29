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
    public partial class officer : System.Web.UI.Page
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
                GetOwnershipsData(qid);
            }
            catch
            { }
        }
        private void GridDataBind()
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
                oracleCommand.CommandText = "select * from VW_UNDER_REVIEW_REQUESTS";
                oracleCommand.CommandType = CommandType.Text;
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
               
            }
            catch (Exception ex)
            {

            }
            return dataTable;
        }
        protected DataTable UpdateRequestReviewStatus(string reqId, string actionValue, string user, string notes, string requestRef, string mobile, int GrassBagSizeKg, int ProteinBagSizeKg, int GrassFooderPerLivestockKg, int ProteinFooderPerLivestockKg, int TotalGrassFooderKg, int TotalProteinFooderKg, decimal TotalGrassFooderBag, decimal TotalProteinFooderBag)
        {
            string approvalStatus = "0";
            if (actionValue == "2")
            {
                approvalStatus = "2";
                UtilityHelper.SendSms(mobile, "عزيزي المربي تم رفض طلب دعم الأعلاف رقم: " + requestRef + "و ذلك " + notes);
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
                oracleCommand.CommandText = "update   REQUESTS set GRASS_F_PER_LIVESTOCK_KG=" + GrassFooderPerLivestockKg + ",PROTEIN_F_PER_LIVESTOCK_KG=" + ProteinFooderPerLivestockKg +
                    ",TOTAL_GRASS_F_KG=" + TotalGrassFooderKg + ",TOTAL_PROTEIN_F_KG=" + TotalProteinFooderKg + ",TOTAL_GRASS_F_BAG=" + TotalGrassFooderBag +
                    ",TOTAL_PROTEIN_F_BAG=" + TotalProteinFooderBag + ",GRASS_BAG_SIZE_KG=" + GrassBagSizeKg + ",PROTEIN_BAG_SIZE_KG=" + ProteinBagSizeKg +
                    " ,REVIEW_STATUS=" + actionValue + ", REVIEWED_DATE=SYSDATE, REVIEWED_BY='" + user + "', REVIEW_NOTES='" + notes + "', APPROVAL_STATUS=" + approvalStatus + " WHERE REQ_ID = " + reqId;
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
            var SheepsData = grid.GetSelectedFieldValues("SHEEPS");
            var RequestRefData = grid.GetSelectedFieldValues("REF");
            Session["selectedRequestId"] = RequIdData[0].ToString(); 
            Session["selectedRequestRef"] = RequestRefData[0].ToString();
            Session["selectedRequestMobile"] = MobileData[0].ToString();
            Session["selectedRequestSheeps"] = SheepsData[0].ToString();
            string qid = grid.GetSelectedFieldValues("QID")[0].ToString();
            GetOwnershipsData(qid);
            panel_action.Visible = true;
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            int selectedRequestId = Convert.ToInt32(Session["selectedRequestId"]);
            string EmpUserName = backUserObj.UserQid;
            string selectedRequestRef = Session["selectedRequestRef"].ToString();
            string electedRequestMobile = Session["selectedRequestMobile"].ToString();
            if (radBtnList_Action.SelectedValue == "1")
            {
                
                UpdateRequestReviewStatus(selectedRequestId.ToString(),"1", EmpUserName, txt_reject_reson.Text, selectedRequestRef, electedRequestMobile
                    , 30,50,Convert.ToInt32(rad_lst_fooders_qty.SelectedValue)
                    , Convert.ToInt32(rad_lst_fooders_qty.SelectedValue)
                    , Convert.ToInt32(lbl_TotalGrassFooderKg.Text)
                    , Convert.ToInt32(lbl_TotalProteinFooderKg.Text)
                    , Convert.ToInt32(lbl_TotalGrassFooderBag.Text)
                    , Convert.ToInt32(lbl_TotalProteinFooderBag.Text));
            }
            else if(radBtnList_Action.SelectedValue == "2")
            {
                UpdateRequestReviewStatus(selectedRequestId.ToString(), "2", EmpUserName, txt_reject_reson.Text, selectedRequestRef, electedRequestMobile, 0, 0, 0, 0, 0, 0, 0, 0);
            }
            GridDataBind();
            panel_action.Visible = false;
        }
        protected void ASPxGridView1_DataBinding(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSource = GetAllRequestUnderReview();
        }
        protected void GetOwnershipsData(string UserQid)
        {
            tbl_ownerships.Rows.Clear();

            TableCell tableCell0 = new TableCell { Text = "رقم الاستمارة", BorderWidth = 1 };
            TableCell tableCell1 = new TableCell { Text = "نوع الترخيص", BorderWidth = 1 };
            TableCell tableCell2= new TableCell { Text = "الموقع", BorderWidth = 1 };
            TableCell tableCell3 = new TableCell { Text = "عدد الابار", BorderWidth = 1 };
            TableCell tableCell4 = new TableCell { Text = "صلاحية المزرعة", BorderWidth = 1 };
            TableCell tableCell5 = new TableCell { Text = "تاريخ اخر حصر", BorderWidth = 1 };
            TableCell tableCell6 = new TableCell { Text = "ضأن", BorderWidth = 1 };
            

            TableRow trHeader = new TableRow();
            trHeader.Cells.Add(tableCell0);
            trHeader.Cells.Add(tableCell1);
            trHeader.Cells.Add(tableCell2);
            trHeader.Cells.Add(tableCell3);
            trHeader.Cells.Add(tableCell4);
            trHeader.Cells.Add(tableCell5);
            trHeader.Cells.Add(tableCell6);
           

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
                TableCell tcSheeps_cont = new TableCell() { Text = curOwnership.Sheeps.ToString() };

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
                tableRow.Cells.Add(tcSheeps_cont);
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
                       // Cows = Convert.ToInt32(dataRow["Cows"]),
                        Sheeps = Convert.ToInt32(dataRow["Sheeps"]),
                       // Goats = Convert.ToInt32(dataRow["Goats"]),
                       // GAZAL = Convert.ToInt32(dataRow["GAZAL"]),
                       // Horses = Convert.ToInt32(dataRow["Horses"]),
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
                throw ex;
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
        protected void rad_lst_fooders_qty_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TotalRequestSheeps = Convert.ToInt32(Session["selectedRequestSheeps"]);
            int GrassBagSizeKg = 30;
            int ProteinBagSizeKg = 50;
            int GrassFooderPerLivestockKg = int.Parse(rad_lst_fooders_qty.SelectedValue);
            int ProteinFooderPerLivestockKg = int.Parse(rad_lst_fooders_qty.SelectedValue);
            int TotalGrassFooderKg = TotalRequestSheeps * GrassFooderPerLivestockKg;
            int TotalProteinFooderKg = TotalRequestSheeps * ProteinFooderPerLivestockKg;
            decimal TotalGrassFooderBag =Math.Round(Convert.ToDecimal(TotalGrassFooderKg) / Convert.ToDecimal(GrassBagSizeKg));
            decimal TotalProteinFooderBag = Math.Round(Convert.ToDecimal(TotalProteinFooderKg)/ Convert.ToDecimal(ProteinBagSizeKg));
            lbl_TotalGrassFooderKg.Text = TotalGrassFooderKg.ToString();
            lbl_TotalProteinFooderKg.Text = TotalProteinFooderKg.ToString();
            lbl_TotalGrassFooderBag.Text = TotalGrassFooderBag.ToString();
            lbl_TotalProteinFooderBag.Text = TotalProteinFooderBag.ToString();
        }
        protected void btn_allRequests_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/allRequests.aspx");
        }
        protected void btn_underReview_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/officer.aspx");
        }
    }
}