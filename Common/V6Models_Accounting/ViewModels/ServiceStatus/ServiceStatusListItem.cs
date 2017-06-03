using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.ServiceStatus
{
    /// <summary>
    /// Tình trạng dịch vụ
    /// </summary>
  public  class ServiceStatusListItem :V6Model
    {
        /// <summary>
        /// Column: Tt_vt
        /// Description: 
        /// </summary>
        public string ThongTinVatTu { get; set; }
        /// <summary>
        /// Column: ten_tt
        /// Description: 
        /// </summary>
        public string Ten_tt { get; set; }
        /// <summary>
        /// Column: ten_tt2
        /// Description: 
        /// </summary>
        public string Ten_tt2 { get; set; }
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
