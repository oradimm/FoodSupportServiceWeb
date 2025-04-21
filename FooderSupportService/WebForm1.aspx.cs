using System;
using System.IO;
using System.Net;
using System.Web;


namespace FooderSupportService
{
    public partial class WebForm1 : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            DownloadFile("{D0F9648A-73C0-4D19-94B8-EA0BCCCCE2C0}");

        }

        private void DownloadFile(string FileId)
        {
            string fileUrl = "https://dotnet.mme.gov.qa/AttachmentsDownloadManager/GetAttachmentProd?DocID="+FileId;
            string contentType;
            byte[] imageData = null;
            string fileType = "";
            using (var wc = new System.Net.WebClient())
            {
                imageData = wc.DownloadData(fileUrl);
                contentType = wc.ResponseHeaders["Content-Type"];
            }
            if (contentType == "image/jpeg")
            {
                fileType = ".jpg";
            }
            else if (contentType == "application/pdf")
            {
                fileType = ".pdf";
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + "attachment"+fileType);
            Response.BinaryWrite(imageData);
            Response.Flush();
            Response.End();
        }
    }
}