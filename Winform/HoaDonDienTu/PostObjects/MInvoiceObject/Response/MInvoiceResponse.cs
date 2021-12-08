using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V6ThuePost.MInvoiceObject.Response
{
    public class MInvoiceResponse
    {
        public string windowid;
        public string ok;
        public string Message;
        public string error;

        public Dictionary<string, object> data = new Dictionary<string, object>();
    }

    public class MInvoiceResponse78
    {
        /// <summary>
        /// <para>01	khieu là trường bắt buộc nhập {khieu}</para>
        /// <para>02	Mã đơn vị không tồn tại</para>
        /// <para>03	Không tồn tại ký hiệu</para>
        /// <para>04	tdlap là trường bắt buộc nhập</para>
        /// <para>21	Bạn phải nhập vào số hóa đơn {shdon}</para>
        /// <para>22	Không tồn tại hóa đơn có ký hiệu: ...  số hóa đơn: …</para>
        /// <para>23	Hóa đơn đã ký hoặc đã gửi cơ quan thuế</para>
        /// <para>00	Thành công</para>
        /// </summary>
        public string code;
        public string message;
        //public string error;

        public Dictionary<string, object> data = new Dictionary<string, object>();
    }
}
