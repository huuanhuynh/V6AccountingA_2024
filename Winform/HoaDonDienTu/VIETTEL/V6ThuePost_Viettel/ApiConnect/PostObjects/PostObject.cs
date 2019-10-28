using System.Collections.Generic;

namespace V6ThuePostViettelApi.PostObjects
{
    public class PostObject : JsonObject
    {
        public Dictionary<string, object> generalInvoiceInfo = new Dictionary<string, object>();
        public Dictionary<string, object> buyerInfo = new Dictionary<string, object>();
        public Dictionary<string, object> sellerInfo = new Dictionary<string, object>();
        public List<Dictionary<string, object>> extAttribute = new List<Dictionary<string, object>>();

        public List<Dictionary<string, object>> payments = new List<Dictionary<string, object>>();
        //{
            //new Dictionary<string, object>()
            //{
            //    {"paymentMethodName", "TM"},
            //}
        //};

        public Dictionary<string, object> deliveryInfo = new Dictionary<string, object>();
        public List<Dictionary<string, object>> itemInfo = new List<Dictionary<string, object>>();
        public List<Dictionary<string, object>> discountItemInfo = new List<Dictionary<string, object>>();
        public Dictionary<string, object> summarizeInfo = new Dictionary<string, object>();
        public List<Dictionary<string, object>> taxBreakdowns = new List<Dictionary<string, object>>();

    }
}
