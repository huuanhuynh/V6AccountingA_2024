using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Discount
{
    public class DiscountListItem : V6Model
    {
        /// <summary>
        /// Column: ma_ck
        /// Description: 
        /// </summary>
        public string MaChietKhau { get; set; }
        /// <summary>
        /// Column: ten_ck
        /// Description: 
        /// </summary>
        public string TenChietKhau { get; set; }
        /// <summary>
        /// Column: ten_ck2
        /// Description: 
        /// </summary>
        public string TenChietKhau2 { get; set; }
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
        public DateTime? NgaySua { get; set; }
        /// <summary>
        /// Column: time2
        /// Description: 
        /// </summary>
        public string ThoiGianSua { get; set; }
        /// <summary>
        /// Column: user_id2
        /// Description: 
        /// </summary>
        public byte? NguoiSua { get; set; }
       
    }
}
