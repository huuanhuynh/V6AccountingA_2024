using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
    public class NhomCongCu : V6Model
    {
        /// <summary>
        /// Column: loai_nh
        /// Description: 
        /// </summary>
        public decimal LoaiNhom { get; set; }
        /// <summary>
        /// Column: ma_nh
        /// Description: 
        /// </summary>
        public string MaNhom { get; set; }
        /// <summary>
        /// Column: ten_nh
        /// Description: 
        /// </summary>
        public string TenNhom { get; set; }
        /// <summary>
        /// Column: ten_nh2
        /// Description: 
        /// </summary>
        public string TenNhom2 { get; set; }
        /// <summary>
        /// Column: date0
        /// Description: 
        /// </summary>
        public DateTime NgayNhap { get; set; }
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
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
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
