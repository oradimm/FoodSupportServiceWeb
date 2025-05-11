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
    public partial class allRequests : System.Web.UI.Page
    {
        BackUserObj backUserObj { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            backUserObj = (BackUserObj)Session["backUserObj"];
            if ((backUserObj == null) || (backUserObj.UserRole != "officer"))
            {
                Response.Redirect("~/backend/login.aspx");
            }
            if (!IsPostBack)
            {
                GridBindData();
            }
        }
        private void GridBindData()
        {
            ASPxGridView1.DataSource = GetAllRequest();
            ASPxGridView1.DataBind();
        }
        protected DataTable GetAllRequest()
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
                oracleCommand.CommandText = "select * from VW_ALL_REQUESTS";
                oracleCommand.CommandType = CommandType.Text;
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                oracleDataAdapter.Fill(dataTable);
               
            }
            catch (Exception ex)
            {

            }
            return dataTable;
        }
        
        protected void btn_underReview_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/officer.aspx");
        }
        protected void ASPxGridView1_DataBinding(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSource = GetAllRequest();
        }
        protected void btn_allRequests_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/backend/allRequests.aspx");
        }
        protected void btn_view_iban_Init(object sender, EventArgs e)
        {
            ASPxButton btn_edit = (ASPxButton)sender;
            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)btn_edit.NamingContainer;
            object image = ASPxGridView1.GetRowValues(container.VisibleIndex, "IBAN_FILE").ToString();
            btn_edit.ClientSideEvents.Click = "function(s, e) {OnViewDocBtnClick('" + image + "');}";
        }

        protected void btn_view_water_cert_Init(object sender, EventArgs e)
        {
            ASPxButton btn_edit = (ASPxButton)sender;
            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)btn_edit.NamingContainer;
            object image = ASPxGridView1.GetRowValues(container.VisibleIndex, "CERT_FILE").ToString();
            btn_edit.ClientSideEvents.Click = "function(s, e) {OnViewDocBtnClick('" + image + "');}";
        }

       
      

        protected void btn_export_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsxToResponse();
        }
    }
}