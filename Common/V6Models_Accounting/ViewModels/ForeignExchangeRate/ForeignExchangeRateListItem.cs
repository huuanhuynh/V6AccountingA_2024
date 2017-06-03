using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.ForeignExchangeRate
{
    /// <summary>
    /// Tỷ giá ngoại tệ list item
    /// </summary>
   public class ForeignExchangeRateListItem : V6Model
    {
        /// <summary>
        /// Column: ma_nt
        /// Description: 
        /// </summary>
        public string MaNgoaiTe { get; set; }
        /// <summary>
        /// Column: ngay_ct
        /// Description: 
        /// </summary>
        public DateTime NgayChungTu { get; set; }
        /// <summary>
        /// Column: ty_gia
        /// Description: 
        /// </summary>
        public decimal ty_Gia { get; set; }
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
