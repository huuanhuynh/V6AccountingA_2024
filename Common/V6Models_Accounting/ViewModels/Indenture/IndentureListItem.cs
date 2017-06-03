using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Indenture
{
    /// <summary>
    /// Khê ước
    /// </summary>
    public class IndentureListItem : V6Model
    {
        /// <summary>
        /// Column: ma_ku
        /// Description: 
        /// </summary>
        public string MaKheUoc { get; set; }
        /// <summary>
        /// Column: ten_ku
        /// Description: 
        /// </summary>
        public string TenKheUoc { get; set; }
        /// <summary>
        /// Column: ten_ku2
        /// Description: 
        /// </summary>
        public string TenKheUoc2 { get; set; }
        /// <summary>
        /// Column: nh_ku1
        /// Description: 
        /// </summary>
        public string NhomKheUoc1 { get; set; }
        /// <summary>
        /// Column: nh_ku2
        /// Description: 
        /// </summary>
        public string NhomKheUoc2 { get; set; }
        /// <summary>
        /// Column: nh_ku3
        /// Description: 
        /// </summary>
        public string NhomKheUoc3 { get; set; }
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
        public byte NguoiNhap { get; set; }
        /// <summary>
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
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
