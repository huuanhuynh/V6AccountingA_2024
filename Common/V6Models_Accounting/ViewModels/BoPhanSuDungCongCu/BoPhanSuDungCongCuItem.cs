using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.BoPhanSuDungCongCu
{
   public class BoPhanSuDungCongCuItem : V6Model
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
