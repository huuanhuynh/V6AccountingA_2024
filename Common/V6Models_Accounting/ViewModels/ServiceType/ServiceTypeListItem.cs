using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.ServiceType
{
    /// <summary>
    /// Loại dịch vụ
    /// </summary>
    public class ServiceTypeListItem : V6Model
    {
        /// <summary>
        /// Column: ma_loai
        /// Description: 
        /// </summary>
        public string MaLoai { get; set; }
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
        /// Column: ma_td1
        /// Description: 
        /// </summary>
        public string MaTuDo1 { get; set; }
    }
}
