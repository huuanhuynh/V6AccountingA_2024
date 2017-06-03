using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Nation
{
    /// <summary>
    /// Quốc gia
    /// </summary>
    public class NationListItem : V6Model
    {
        /// <summary>
        /// Column: ma_qg
        /// Description: 
        /// </summary>
        public string MaQuocGia { get; set; }
        /// <summary>
        /// Column: ten_qg
        /// Description: 
        /// </summary>
        public string TenQuocGia { get; set; }
        /// <summary>
        /// Column: ten_qg2
        /// Description: 
        /// </summary>
        public string TenQuocGia2 { get; set; }
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
    }
}
