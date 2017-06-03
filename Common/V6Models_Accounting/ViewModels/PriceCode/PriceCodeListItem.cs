using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.PriceCode
{
    /// <summary>
    /// Danh mục mã giá
    /// </summary>
  public class PriceCodeListItem : V6Model
    {
        /// <summary>
        /// Column: ma_gia
        /// Description: 
        /// </summary>
        public string MaGia { get; set; }
        /// <summary>
        /// Column: ten_gia
        /// Description: 
        /// </summary>
        public string Ten_Gia { get; set; }
        /// <summary>
        /// Column: Loai
        /// Description: 
        /// </summary>
        public string Loai { get; set; }
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
        /// <summary>
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
    }
}
