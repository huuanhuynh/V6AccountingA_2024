using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.MaterialType
{
    /// <summary>
    /// Loại vật tư
    /// </summary>
    public class MaterialTypeItem : V6Model
    {
        /// <summary>
        /// Column: loai_vt
        /// Description: 
        /// </summary>
        public string Loai_VatTu { get; set; }
        /// <summary>
        /// Column: ten_loai
        /// Description: 
        /// </summary>
        public string Ten_Loai { get; set; }
        /// <summary>
        /// Column: ten_loai2
        /// Description: 
        /// </summary>
        public string Ten_Loai2 { get; set; }
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
        /// Column: date2
        /// Description: 
        /// </summary>
        public DateTime NgaySua { get; set; }
        /// <summary>
        /// Column: time2
        /// Description: 
        /// </summary>
        public string ThoiGianSua { get; set; }
    }
}
