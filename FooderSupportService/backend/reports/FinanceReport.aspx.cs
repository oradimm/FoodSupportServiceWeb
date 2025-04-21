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
    public partial class FinanceReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EmpUserName"] == null)
            {
                Response.Redirect("~/backend/login.aspx");
            }
            else
            {
               
                DateTime meetingDate = Convert.ToDateTime(Session["meetingDate"]);
                string meetingSn = Convert.ToString(Session["meetingSn"]);
                lbl_sn.Text = meetingSn;
                lbl_meeting_date.Text = meetingDate.ToString("dd-MM-yyyy");
               
                GetRequests(meetingSn);
            }
        }
        protected void GetRequests(string sn)
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
                oracleCommand.CommandText = "select * FROM VW_FINANCE_REPORT WHERE COMMITTEE_SN = :C_SN";
                oracleCommand.CommandType = CommandType.Text;
                OracleParameter oracleParameter = new OracleParameter("C_SN", sn);
                oracleCommand.Parameters.Add(oracleParameter);
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

                    TableCell tcWaterAmount = new TableCell();
                    tcWaterAmount.Text = Convert.ToString(dataRow["TOTAL_WATER"]);
                    total_water = total_water + Convert.ToInt32(dataRow["TOTAL_WATER"]);
                    
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
                    tableRow.Cells.Add(tcWaterAmount);
                    tableRow.Cells.Add(tcValue);
                    tbl_myRequests.Rows.Add(tableRow);
                    index++;
                }
                TableRow tableRowTotal = new TableRow();
                TableCell tcTotal = new TableCell() { ColumnSpan = 6, Text = "المجموع" };
                tableRowTotal.Cells.Add(tcTotal);

                TableCell tcTotalWater = new TableCell();
                tcTotalWater.Text = total_water.ToString();

                TableCell tcTotalValue = new TableCell();
                tcTotalValue.Text = total_value.ToString();

                tableRowTotal.Cells.Add(tcTotalWater);
                tableRowTotal.Cells.Add(tcTotalValue);
                tbl_myRequests.Rows.Add(tableRowTotal);
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