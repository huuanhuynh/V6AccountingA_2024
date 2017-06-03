using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.BankAccount
{
    public class BankAccountDetail : V6Model
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
        /// Column: dia_chi
        /// Description: 
        /// </summary>
        public string DiaChi { get; set; }
        /// <summary>
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
    }
}
