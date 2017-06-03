using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.ShippingMethod
{
    /// <summary>
    /// Hình thức vận chuyển
    /// </summary>
    public class ShippingMethodListItem : V6Model
    {
        /// <summary>
        /// Column: ma_htvc
        /// Description: 
        /// </summary>
        public string MaHinhThucVanChuyen { get; set; }
        /// <summary>
        /// Column: ten_htvc
        /// Description: 
        /// </summary>
        public string TenHinhThucVanChuyen { get; set; }
        /// <summary>
        /// Column: ten_htvc2
        /// Description: 
        /// </summary>
        public string TenHinhThucVanChuyen2 { get; set; }
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
        public DateTime? NgaySua { get; set; }
        /// <summary>
        /// Column: time2
        /// Description: 
        /// </summary>
        public string ThoiGianSua { get; set; }
        /// <summary>
        /// Column: user_id2
        /// Description: 
        /// </summary>
        public byte? NguoiSua { get; set; }
    }
}
