using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.SubAccount
{
   public class SubAccountListItem : V6Model
    {
       
        /// <summary>
        /// Column: tk2
        /// Description: 
        /// </summary>
       public string TieuKhoan2 { get; set; }
        /// <summary>
        /// Column: nh_tk2
        /// Description: 
        /// </summary>
       public string nh_TieuKhoan2 { get; set; }
        /// <summary>
        /// Column: ten_tk2
        /// Description: 
        /// </summary>
       public string Ten_TieuKhoan2 { get; set; }
        /// <summary>
        /// Column: ten_tk22
        /// Description: 
        /// </summary>
       public string Ten_TieuKhoan22 { get; set; }
        /// <summary>
        /// Column: date0
        /// Description: 
        /// </summary>
        public DateTime? NgayKhoiTao { get; set; }
        /// <summary>
        /// Column: time0
        /// Description: 
        /// </summary>
        public string GioKhoiTao { get; set; }
        /// <summary>
        /// Column: user_id0
        /// Description: 
        /// </summary>
        public byte? NguoiNhap { get; set; }
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
