using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Tax
{
    /// <summary>
    /// thuế suất
    /// </summary>
   public class TaxListItem : V6Model
    {
        /// <summary>
        /// Column: ma_thue
        /// Description: 
        /// </summary>
        public string MaThue { get; set; }
        /// <summary>
        /// Column: ten_thue
        /// Description: 
        /// </summary>
        public string Ten_thue { get; set; }
        /// <summary>
        /// Column: ten_thue2
        /// Description: 
        /// </summary>
        public string Ten_thue2 { get; set; }
        /// <summary>
        /// Column: thue_suat
        /// Description: 
        /// </summary>
        public decimal ThueSuat { get; set; }
        /// <summary>
        /// Column: tk_thue_co
        /// Description: 
        /// </summary>
        public string TaiKhoan_thue_co { get; set; }
        /// <summary>
        /// Column: tk_thue_no
        /// Description: 
        /// </summary>
        public string TaiKhoan_thue_no { get; set; }
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
