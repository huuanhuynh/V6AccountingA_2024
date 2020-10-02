using System;
using V6Tools.V6Objects;

namespace V6ThuePost.ViettelV2Objects.GetInvoice
{
    public class GetInvoiceInput : V6JsonObject
    {
        public GetInvoiceInput()
        {
            DateTimeFormat = "s";//2017-12-12T10:14:32
        }
        /// <summary>
        /// 2017-12-12T10:14:32.611+07:00
        /// </summary>
        public DateTime startDate;
        public DateTime endDate;
        /// <summary>
        /// 02GTTT
        /// </summary>
        public string invoiceType;

        public int rowPerPage = 20;
        public int pageNum = 1;
        public string templateCode;
    }
}
