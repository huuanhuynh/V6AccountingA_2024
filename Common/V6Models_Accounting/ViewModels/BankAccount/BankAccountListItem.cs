using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.BankAccount
{
    /// <summary>
    /// Tài khoản ngân hàng
    /// </summary>
    public class BankAccountListItem : V6Model
    {
        /// <summary>
        /// Column: tk
        /// Description: 
        /// </summary>
        public string TaiKhoan { get; set; }
        /// <summary>
        /// Column: tknh
        /// Description: 
        /// </summary>
        public string TaiKhoanNganHang { get; set; }
        /// <summary>
        /// Column: ten_tknh
        /// Description: 
        /// </summary>
        public string TenTaiKhoanNganHang { get; set; }
        /// <summary>
        /// Column: ten_tknh2
        /// Description: 
        /// </summary>
        public string TenTaiKhoanNganHang2 { get; set; }
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
