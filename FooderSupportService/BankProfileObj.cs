using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FooderSupportService
{
    public class BankProfileObj
    {
        public int Id { set; get; }
        public int BankId { set; get; }
        public string BankName { set; get; }
        public string IBAN { set; get; }
        public string IbanFile { set; get; }

    }
}