using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Capital
{
    /// <summary>
    /// Nguồn vốn
    /// </summary>
    public class CapitalListItem : V6Model
    {
        /// <summary>
        /// Column: ma_nv
        /// Description: 
        /// </summary>
        public string MaNguonVon { get; set; }
        /// <summary>
        /// Column: ten_nv
        /// Description: 
        /// </summary>
        public string TenNguonVon { get; set; }
        /// <summary>
        /// Column: ten_nv2
        /// Description: 
        /// </summary>
        public string TenNguonVon2 { get; set; }
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
        public decimal NguoiNhap { get; set; }
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
        public decimal? NguoiSua { get; set; }
    }
}
