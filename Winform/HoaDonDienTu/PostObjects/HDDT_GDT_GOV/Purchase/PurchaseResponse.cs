using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V6ThuePost.HDDT_GDT_GOV.Purchase
{
    public class PurchaseResponse
    {
        public List<HDinfo> datas { get; set; }
        public int total { get; set; }
        public string state { get; set; }
        public int time { get; set; }

        public string timestamp { get; set; }
        public string path { get; set; }
        public string status { get; set; }
        public string error { get; set; }
        public string message { get; set; }
        public string requestId { get; set; }
    }
}
