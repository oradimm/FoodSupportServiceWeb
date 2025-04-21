using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FooderSupportService
{
    public class RequestObj
    {
        public int ReqId { set; get; }
        public DateTime ReqDate { set; get; }
        public int UserProfile { set; get; }
        public int Sheeps { set; get; }
        public int ReviewStatus { set; get; }
        public DateTime ReviewedDate { set; get; }
        public string ReviewedBy { set; get; }
        public string ReviewNotes { set; get; }
        public int ApprovalStatus { set; get; }
        public DateTime ApprovalDate { set; get; }
        public string ApprovalBy { set; get; }
        public string ApprovalNotes { set; get; }
        public DateTime SupportStart { set; get; }
        public DateTime SupportEnd { set; get; }
    }
}