using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.PaymentMethod
{
    /// <summary>
    /// HÌnh thức thanh toán
    /// </summary>
    public class PaymentMethodListItem : V6Model
    {
        /// <summary>
        /// Column: ma_httt
        /// Description: 
        /// </summary>
        public string MaHinhThucThanhToan { get; set; }
        /// <summary>
        /// Column: ten_httt
        /// Description: 
        /// </summary>
        public string TenHinhThucThanhToan { get; set; }
        /// <summary>
        /// Column: ten_httt2
        /// Description: 
        /// </summary>
        public string TenHinhThucThanhToan2 { get; set; }
        /// <summary>
        /// Column: date0
        /// Description: 
        /// </summary>
        public DateTime NgayKhoiTao { get; set; }
        /// <summary>
        /// Column: time0
        /// Description: 
        /// </summary>
        public string GioKhoiTao { get; set; }
        /// <summary>
        /// Column: user_id0
        /// Description: 
        /// </summary>
        public byte NguoiNhap { get; set; }
        /// <summary>
        /// Column: date2
        /// Description: 
        /// </summary>
        public DateTime NgaySua { get; set; }
        /// <summary>
        /// Column: time2
        /// Description: 
        /// </summary>
        public string ThoiGianSua { get; set; }
        /// <summary>
        /// Column: user_id2
        /// Description: 
        /// </summary>
        public byte NguoiSua { get; set; }
    }
}
