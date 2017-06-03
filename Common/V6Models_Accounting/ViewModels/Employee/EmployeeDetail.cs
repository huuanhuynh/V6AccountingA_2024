using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Employee
{
    /// <summary>
    /// Nhân viên bán hàng
    /// </summary>
    public class EmployeeDetail : V6Model
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
        /// Column: ghi_chu
        /// Description: 
        /// </summary>
        public string GhiChu { get; set; }
        /// <summary>
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
        /// <summary>
        /// Column: sl_td1
        /// Description: 
        /// </summary>
        public decimal? SoLuongTuDo1 { get; set; }
        /// <summary>
        /// Column: sl_td2
        /// Description: 
        /// </summary>
        public decimal? SoLuongTuDo2 { get; set; }
        /// <summary>
        /// Column: sl_td3
        /// Description: 
        /// </summary>
        public decimal? SoLuongTuDo3 { get; set; }
        /// <summary>
        /// Column: ngay_td1
        /// Description: 
        /// </summary>
        public DateTime? NgayTuDo1 { get; set; }
        /// <summary>
        /// Column: ngay_td2
        /// Description: 
        /// </summary>
        public DateTime? NgayTuDo2 { get; set; }
        /// <summary>
        /// Column: ngay_td3
        /// Description: 
        /// </summary>
        public DateTime? NgayTuDo3 { get; set; }
        /// <summary>
        /// Column: gc_td1
        /// Description: 
        /// </summary>
        public string GhiChuTuDo1 { get; set; }
        /// <summary>
        /// Column: gc_td2
        /// Description: 
        /// </summary>
        public string GhiChuTuDo2 { get; set; }
        /// <summary>
        /// Column: gc_td3
        /// Description: 
        /// </summary>
        public string GhiChuTuDo3 { get; set; }
    }
}
