using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Shipment
{
    /// <summary>
    /// Vận chuyển
    /// </summary>
    public class ShipmentListItem : V6Model
    {
        /// <summary>
        /// Column: ma_vc
        /// Description: 
        /// </summary>
        public string MaVanChuyen { get; set; }
        /// <summary>
        /// Column: ten_vc
        /// Description: 
        /// </summary>
        public string TenVanChuyen { get; set; }
        /// <summary>
        /// Column: ten_vc2
        /// Description: 
        /// </summary>
        public string TenVanChuyen2 { get; set; }
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
