using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Department
{
    /// <summary>
    /// Bộ phận
    /// </summary>
    public class DepartmentDetail : V6Model
    {
        /// <summary>
        /// Column: ma_bp
        /// Description: 
        /// </summary>
        public string MaBoPhan { get; set; }
        /// <summary>
        /// Column: ten_bp
        /// Description: 
        /// </summary>
        public string TenBoPhan { get; set; }
        /// <summary>
        /// Column: ten_bp2
        /// Description: 
        /// </summary>
        public string TenBoPhan2 { get; set; }
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
        public DateTime NgayTuDo1 { get; set; }
        /// <summary>
        /// Column: ngay_td2
        /// Description: 
        /// </summary>
        public DateTime NgayTuDo2 { get; set; }
        /// <summary>
        /// Column: ngay_td3
        /// Description: 
        /// </summary>
        public DateTime NgayTuDo3 { get; set; }
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
        /// <summary>
        /// Column: CHECK_SYNC
        /// Description: 
        /// </summary>
        public string CheckSync { get; set; }
    //    public Guid UID { get; set; }
    }
}
