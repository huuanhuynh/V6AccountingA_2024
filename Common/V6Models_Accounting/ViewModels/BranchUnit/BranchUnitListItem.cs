using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.DonViCoSo
{
    public class BranchUnitListItem : V6Model
    {
        /// <summary>
        /// Column: ma_dvcs
        /// Description: 
        /// </summary>
        public string MaDonVi { get; set; }
        /// <summary>
        /// Column: ten_dvcs
        /// Description: 
        /// </summary>
        public string TenDonVi { get; set; }
        /// <summary>
        /// Column: ten_dvcs2
        /// Description: 
        /// </summary>
        public string TenDonVi2 { get; set; }
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
