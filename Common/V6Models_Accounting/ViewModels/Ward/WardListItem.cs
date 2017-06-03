using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Ward
{
    /// <summary>
    /// Phường xã
    /// </summary>
    public class WardListItem : V6Model
    {
        /// <summary>
        /// Column: ma_phuong
        /// Description: 
        /// </summary>
        public string MaPhuong { get; set; }
        /// <summary>
        /// Column: ten_ph
        /// Description: 
        /// </summary>
        public string TenPhuong { get; set; }
        /// <summary>
        /// Column: ten_ph2
        /// Description: 
        /// </summary>
        public string TenPhuong2 { get; set; }
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
