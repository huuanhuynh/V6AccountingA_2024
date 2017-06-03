using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.District
{
    /// <summary>
    /// Quận huyện
    /// </summary>
   public class DistrictListItem : V6Model
    {
        /// <summary>
        /// Column: ma_quan
        /// Description: 
        /// </summary>
        public string MaQuan { get; set; }
        /// <summary>
        /// Column: ten_quan
        /// Description: 
        /// </summary>
        public string Ten_quan { get; set; }
        /// <summary>
        /// Column: ten_quan2
        /// Description: 
        /// </summary>
        public string Ten_quan2 { get; set; }
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
