using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FooderSupportService
{
    public class OwnershipObj
    {
        public int OWNERSHIP_ID { set; get; }
        public int Sheeps { set; get; }
        public DateTime REGISTERATION_DATE { set; get; }
        public string TYPE { set; get; }
        public int TYPE_NUMBER { set; get; }
        public string LOCATION { set; get; }
        public string BLOCK_REASON { set; get; }
        public bool IS_BLOCKED { set; get; }
        public int LIC_NUMBER { set; get; }
        public DateTime FARM_EXP_DATE { set; get; }
        public int NUMBER_OF_WELLS { set; get; }
        public bool CanApply { set; get; }
        public List<string> notes { set; get; }
    }
}