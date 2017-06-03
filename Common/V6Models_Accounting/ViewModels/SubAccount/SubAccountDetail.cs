using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.SubAccount
{
    /// <summary>
    /// Tiểu khoản
    /// </summary>
    public class SubAccountDetail : V6Model
    {
        /// <summary>
        /// Column: type
        /// Description: 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// Column: tk2
        /// Description: 
        /// </summary>
        public string TieuKhoan2 { get; set; }
        /// <summary>
        /// Column: nh_tk2
        /// Description: 
        /// </summary>
        public string NhomTieuKhoan2 { get; set; }
        /// <summary>
        /// Column: ten_tk2
        /// Description: 
        /// </summary>
        public string TenTieuKhoan2 { get; set; }
        /// <summary>
        /// Column: ten_tk22
        /// Description: 
        /// </summary>
        public string TenTieuKhoan22 { get; set; }
        /// <summary>
        /// Column: dau
        /// Description: 
        /// </summary>
        public string Dau { get; set; }
        /// <summary>
        /// Column: loai
        /// Description: 
        /// </summary>
        public byte Loai { get; set; }
        /// <summary>
        /// Column: bac
        /// Description: 
        /// </summary>
        public byte Bac { get; set; }
    }
}
