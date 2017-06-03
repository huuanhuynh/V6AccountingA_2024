using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.EquipmentChangedReason
{
    /// <summary>
    /// Lý do tăng giảm công cụ
    /// </summary>
    public class EquipmentChangedReasonListItem : V6Model
    {
        /// <summary>
        /// Column: ma_tg_cc
        /// Description: 
        /// </summary>
        public string Ma_TGCungCap { get; set; }
        /// <summary>
        /// Column: ten_tg_cc
        /// Description: 
        /// </summary>
        public string Ten_tg_cc { get; set; }
        /// <summary>
        /// Column: ten_tg_cc2
        /// Description: 
        /// </summary>
        public string Ten_tg_cc2 { get; set; }
        /// <summary>
        /// Column: loai_tg_cc
        /// Description: 
        /// </summary>
        public string Loai_tg_cc { get; set; }
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
