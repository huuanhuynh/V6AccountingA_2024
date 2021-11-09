using System.Collections.Generic;
using V6Tools.V6Objects;

namespace V6ThuePost.ViettelV2Objects
{
    public class PostObjectViettelV2 : V6JsonObject
    {
        public Dictionary<string, object> generalInvoiceInfo = new Dictionary<string, object>();
        public Dictionary<string, object> buyerInfo = new Dictionary<string, object>();
        public Dictionary<string, object> sellerInfo = new Dictionary<string, object>();
        public List<Dictionary<string, object>> extAttribute = new List<Dictionary<string, object>>();
        public List<Dictionary<string, object>> payments = new List<Dictionary<string, object>>();
        public Dictionary<string, object> deliveryInfo = new Dictionary<string, object>();
        public List<Dictionary<string, object>> itemInfo = new List<Dictionary<string, object>>();
        public List<Dictionary<string, object>> metadata = new List<Dictionary<string, object>>();
        public List<Dictionary<string, object>> meterReading = new List<Dictionary<string, object>>();
        public List<Dictionary<string, object>> discountItemInfo = new List<Dictionary<string, object>>();
        public Dictionary<string, object> summarizeInfo = new Dictionary<string, object>();
        public List<Dictionary<string, object>> taxBreakdowns = new List<Dictionary<string, object>>();

    }
}
