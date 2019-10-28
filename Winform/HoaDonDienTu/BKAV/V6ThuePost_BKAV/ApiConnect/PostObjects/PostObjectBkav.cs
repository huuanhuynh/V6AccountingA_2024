using System.Collections.Generic;

namespace V6ThuePostBkavApi.PostObjects
{
    public class PostObjectBkav : JsonObjectBkav
    {
        public Dictionary<string, object> Invoice = new Dictionary<string, object>();
        public List<Dictionary<string, object>> ListInvoiceDetailsWS = new List<Dictionary<string, object>>();
        
        //public Dictionary<string, object> buyerInfo = new Dictionary<string, object>();
        //public Dictionary<string, object> sellerInfo = new Dictionary<string, object>();
        //public List<Dictionary<string, object>> extAttribute = new List<Dictionary<string, object>>();
        //public List<Dictionary<string, object>> payments = new List<Dictionary<string, object>>();
        //public Dictionary<string, object> deliveryInfo = new Dictionary<string, object>();
        //public List<Dictionary<string, object>> discountItemInfo = new List<Dictionary<string, object>>();
        //public Dictionary<string, object> summarizeInfo = new Dictionary<string, object>();
        //public List<Dictionary<string, object>> taxBreakdowns = new List<Dictionary<string, object>>();
        
        public List<string> ListInvoiceAttachFileWS = new List<string>();
        /// <summary>
        /// ID theo định dạng ddMMyyyyHHmmss
        /// </summary>
        public string PartnerInvoiceID = "0";
        /// <summary>
        /// ID định dạng tùy ý. Nếu dùng ID này thì PartnerInvoiceID phải = "0"
        /// </summary>
        public string PartnerInvoiceStringID = null;
        
    }
}
