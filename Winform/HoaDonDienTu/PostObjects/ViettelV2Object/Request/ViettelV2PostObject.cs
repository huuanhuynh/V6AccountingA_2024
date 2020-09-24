using System.Collections.Generic;
using V6Tools.V6Objects;

namespace V6ThuePost.ViettelV2Object.Request
{
    public class ViettelV2PostObject : V6JsonObject
    {
        public ViettelV2PostObject()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }

        /// <summary>
        /// WIN00187 : Hóa đơn giá trị gia tăng 01GTKT, WIN00189 : Hóa đơn bán hàng giá trị trực tiếp 02GTTT
        /// </summary>
        public string windowid;
        /// <summary>
        /// 1 : Mới, 2 : Sửa.
        /// </summary>
        public string editmode;

        /// <summary>
        /// &lt;Inv>...&lt;/Inv>[...]
        /// </summary>
        public List<InvoiceData> data;
    }
}
