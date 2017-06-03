using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.MeasurementUnit
{
    /// <summary>
    /// Đơn vị tính
    /// </summary>
    public class MeasurementUnitListItem : V6Model
    {
        /// <summary>
        /// Column: dvt
        /// Description: 
        /// </summary>
        public string Ma_DonViTinh { get; set; }
        /// <summary>
        /// Column: ten_dvt
        /// Description: 
        /// </summary>
        public string Ten_DonViTinh { get; set; }
        /// <summary>
        /// Column: ten_dvt2
        /// Description: 
        /// </summary>
        public string Ten_DonViTinh2 { get; set; }
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
        /// <summary>
        /// Column: user_id2
        /// Description: 
        /// </summary>
        public byte NguoiSua { get; set; }
    }
}
