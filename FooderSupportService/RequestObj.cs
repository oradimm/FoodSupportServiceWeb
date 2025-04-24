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
        public DateTime? SupportStart { set; get; }
        public DateTime? SupportEnd { set; get; }
        public int GrassFooderPerLivestockKg { set; get; }
        public int ProteinFooderPerLivestockKg { set; get; }
        public int TotalGrassFooderKg { set; get; }
        public int TotalProteinFooderKg { set; get; }
        public int TotalGrassFooderBag { set; get; }
        public int TotalProteinFooderBag { set; get; }
        public int GrassBagSizeKg { set; get; }
        public int ProteinBagSizeKg { set; get; }
        public string RequestSource { set; get; }
        public DateTime DeliveryStatus { set; get; }
        public int DeliveredBy { set; get; }
        public DateTime DeliveredDate { set; get; }
        public int RelatedApplicationId { set; get; }

    }
}