using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Employee
{
    /// <summary>
    /// Nhân viên 
    /// </summary>
    public class EmployeeListItem : V6Model
    {
        /// <summary>
        /// Column: ma_nvien
        /// Description: 
        /// </summary>
        public string MaNhanVien { get; set; }
        /// <summary>
        /// Column: ten_nvien
        /// Description: 
        /// </summary>
        public string Ten_nvien { get; set; }
        /// <summary>
        /// Column: ten_nvien2
        /// Description: 
        /// </summary>
        public string Ten_nvien2 { get; set; }
        /// <summary>
        /// Column: Loai
        /// Description: 
        /// </summary>
        public string Loai { get; set; }
        /// <summary>
        /// Column: han_tt
        /// Description: 
        /// </summary>
        public decimal? HanThanhToan { get; set; }
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
