using System;
using System.Collections.Generic;
using System.ComponentModel;
using V6Tools.V6Objects;

namespace V6ThuePost.MInvoiceObject.Request
{
    public class MInvoicePostObject : V6JsonObject
    {
        public MInvoicePostObject()
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

    //public class MInvoicePostObject78 : V6JsonObject
    //{
    //    public MInvoicePostObject()
    //    {
    //        DateTimeFormat = "yyyy-MM-dd";
    //    }

    //    /// <summary>
    //    /// 1 : Mới, 2 : Sửa.
    //    /// </summary>
    //    public string editmode;

    //    /// <summary>
    //    /// &lt;Inv>...&lt;/Inv>[...]
    //    /// </summary>
    //    public List<InvoiceData> data;
    //}

    public class MInvoicePostObject_View
    {
        private MInvoicePostObject _obj;
        public MInvoicePostObject_View(MInvoicePostObject obj)
        {
            _obj = obj;
        }

        [Description("Ngày chứng từ.")]
        [Category("GeneralInvoiceInfo")]
        public DateTime inv_invoiceIssuedDate {
            get
            {
                return (DateTime)_obj.data[0]["inv_invoiceIssuedDate"];
            }
            set
            {
                _obj.data[0]["inv_invoiceIssuedDate"] = value;
            }
        }

        [Description("Số sê-ri.")]
        [Category("GeneralInvoiceInfo")]
        public string inv_invoiceSeries
        {
            get
            {
                return (string)_obj.data[0]["inv_invoiceSeries"];
            }
            set
            {
                _obj.data[0]["inv_invoiceSeries"] = value;
            }
        }
    }
}
