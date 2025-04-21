using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FooderSupportService.backend.reports
{
    public partial class CommitteeReportAnix : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EmpUserName"] == null)
            {
                Response.Redirect("~/backend/login.aspx");
            }
            else
            {
                DateTime sDate = Convert.ToDateTime(Session["sDate"]);
                DateTime eDate = Convert.ToDateTime(Session["eDate"]);
                string meetingSn = Convert.ToString(Session["meetingSn"]);
                lbl_from.Text = sDate.ToString("dd-MM-yyyy");
                lbl_to.Text = eDate.ToString("dd-MM-yyyy");
                lbl_sn.Text = meetingSn;
                GetRequests(sDate, eDate);
            }
        }
        protected void GetRequests(DateTime ReviwedDateFrom, DateTime ReviwedDateTo)
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
                oracleCommand.CommandText = "select * FROM VW_COMMITTEE_REPORT WHERE AUDITED_DATE between TO_DATE('" + ReviwedDateFrom.ToString("yyyy-MM-dd HH:mm:ss") + "', 'YYYY-MM-DD HH24:MI:SS') and TO_DATE('" + ReviwedDateTo.ToString("yyyy-MM-dd") + " 23:59:59', 'YYYY-MM-DD HH24:MI:SS')"; oracleCommand.CommandType = CommandType.Text;
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
                int index = 1;
                decimal total_water = 0;
                decimal total_value = 0;
                foreach (DataRow dataRow in dataTable.Rows)
                {

                    TableCell tcSN = new TableCell();
                    tcSN.Text = Convert.ToString(index);

                    TableCell tcRef = new TableCell();
                    tcRef.Text = Convert.ToString(dataRow["REF"]);

                    TableCell tcName = new TableCell();
                    tcName.Text = Convert.ToString(dataRow["NAME"]);

                    TableCell tcQid = new TableCell();
                    tcQid.Text = Convert.ToString(dataRow["QID"]);

                    TableCell tcBank = new TableCell();
                    tcBank.Text = GetBankName(Convert.ToString(dataRow["BANK_ID"]));

                    TableCell tcIBAN = new TableCell();
                    tcIBAN.Text = Convert.ToString(dataRow["IBAN"]);

                    TableCell tcCamels = new TableCell();
                    tcCamels.Text = Convert.ToString(dataRow["CAMELS"]);

                    TableCell tcCows = new TableCell();
                    tcCows.Text = Convert.ToString(dataRow["COWS"]);

                    TableCell tcGoats = new TableCell();
                    tcGoats.Text = Convert.ToString(dataRow["GOATS"]);

                    TableCell tcSheeps = new TableCell();
                    tcSheeps.Text = Convert.ToString(dataRow["SHEEPS"]);


                    TableCell tcHorses = new TableCell();
                    tcHorses.Text = Convert.ToString(dataRow["HORSES"]);

                    TableCell tcGazals = new TableCell();
                    tcGazals.Text = Convert.ToString(dataRow["GAZALS"]);



                    TableCell tcWaterAmount = new TableCell();
                    tcWaterAmount.Text = Convert.ToString(dataRow["TOTAL_WATER"]);

                    total_water = total_water + Convert.ToDecimal(dataRow["TOTAL_WATER"]);
                   

                    TableCell tcValue = new TableCell();
                    tcValue.Text = Convert.ToString(dataRow["TOTAL_SUPPORT"]);
                    total_value = total_value + Convert.ToDecimal(dataRow["TOTAL_SUPPORT"]);

                    TableRow tableRow = new TableRow();
                    tableRow.Cells.Add(tcSN);
                    tableRow.Cells.Add(tcRef);
                    tableRow.Cells.Add(tcName);
                    tableRow.Cells.Add(tcQid);
                    tableRow.Cells.Add(tcBank);
                    tableRow.Cells.Add(tcIBAN);
                    tableRow.Cells.Add(tcCamels);
                    tableRow.Cells.Add(tcCows);
                    tableRow.Cells.Add(tcSheeps);
                    tableRow.Cells.Add(tcGoats);
                    tableRow.Cells.Add(tcHorses);
                    tableRow.Cells.Add(tcGazals);
                    tableRow.Cells.Add(tcWaterAmount);
                    tableRow.Cells.Add(tcValue);

                    tbl_requests.Rows.Add(tableRow);
                    index++;
                }
                TableRow tableRowTotal = new TableRow();
                TableCell tcTotal = new TableCell() { ColumnSpan = 12, Text="المجموع" };
                tableRowTotal.Cells.Add(tcTotal);
               
                TableCell tcTotalWater = new TableCell();
                tcTotalWater.Text = total_water.ToString();

                TableCell tcTotalValue = new TableCell();
                tcTotalValue.Text = total_value.ToString();

                tableRowTotal.Cells.Add(tcTotalWater);
                tableRowTotal.Cells.Add(tcTotalValue);


                tbl_requests.Rows.Add(tableRowTotal);
            }
            catch (Exception ex)
            {

            }
        }
        protected string GetBankName(string id)
        {
            switch (id)
            {
                case "1":
                    return "بنك قطر الوطني";
                    break;
                case "2":
                    return "مصرف قطر الإسلامي";
                    break;
                case "3":
                    return "بنك قطر الدولي الإسلامي";
                    break;
                case "4":
                    return "مصرف الريان";
                    break;
                case "5":
                    return "البنك التجاري القطري";
                    break;
                case "6":
                    return "بنك الدوحه";
                    break;
                case "7":
                    return "بنك دخان";
                    break;
                case "8":
                    return "البنك الأهلي";
                    break;
                case "9":
                    return "البنك البريطاني";
                    break;
                default:
                    return "";
                    break;
            }
        }
       
    }
}