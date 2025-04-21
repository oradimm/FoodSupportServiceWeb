using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FooderSupportService
{
  
    public class IzbaObj
    {
        public string COMPSEQ { set; get; }
        public int COMPID { set; get; }
        public string OWNERID { set; get; }
        public string OWNERGSM { set; get; }
        public string CONTRACTSTART { set; get; }
        public string CONTRACTEND { set; get; }
        public string OWNERNAME { set; get; }
        public string IZBA_TYPE { set; get; }
        public string IZBA_STATUS { set; get; }
        public string LANDS_TOTAL { set; get; }
        public string IZBA_SERIAL { set; get; }
        public string IZBA_DESC { set; get; }
        public string IZBAID { set; get; }
    }
    class RequestbodyModel
    {
        public string QID { get; set; }
        public int REQUEST_NUMBER { get; set; }
    }
    class TokenModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public long expires_in { get; set; }

    }
}