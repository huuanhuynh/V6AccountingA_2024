using System.Collections.Generic;

namespace V6ThuePost.MInvoiceObject.Request
{
    public class InvoiceData : Dictionary<string, object>
    {
        //public List<DetailObject> details = new List<DetailObject>();
    }

    public class DetailObject
    {
        /// <summary>
        /// TAB00188 Hóa đơn giá trị gia tăng 01GTKT (WIN00187), TAB00192 Hóa đơn bán hàng giá trị trực tiếp 02GTTT(WIN00189)
        /// </summary>
        public string tab_id;

        public List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
    }
}
