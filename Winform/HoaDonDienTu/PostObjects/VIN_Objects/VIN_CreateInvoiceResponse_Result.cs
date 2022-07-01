using System.Collections.Generic;
namespace V6ThuePost.VIN_Objects
{
    public class VIN_CreateInvoiceResponse_Result
    {
        public string maketqua { get; set; }
        public string motaketqua { get; set; }
        public string magiaodich { get; set; }
        public string ma_hoadon { get; set; }
        public string mauso { get; set; }
        public string kyhieu { get; set; }
        public string sohoadon { get; set; }
        public string trangthai { get; set; }
        public string ngayky { get; set; }
        /// <summary>
        /// loaihoadon: 1- hóa đơn gốc, 2-hóa đơn bị thay thế, 3-hóa đơn thay thế, 4-hóa đơn bị điều chỉnh, 5-hóa đơn điều chỉnh, 6-hóa đơn bị xóa bỏ, 7-hóa đơn xóa bỏ
        /// </summary>
        public string loaihoadon { get; set; }
        public string chuyendoi { get; set; }
        public string base64xml { get; set; }
        public Dictionary<string, object> datahd { get; set; }



        public string base64pdf { get; set; }
    }
}
