using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Capital
{
    public class CapitalDetail : V6Model
    {
        /// <summary>
        /// Column: ma_nv
        /// Description: 
        /// </summary>
        public string MaNguonVon { get; set; }
        /// <summary>
        /// Column: ten_nv
        /// Description: 
        /// </summary>
        public string TenNguonVon { get; set; }
        /// <summary>
        /// Column: ten_nv2
        /// Description: 
        /// </summary>
        public string TenNguonVon2 { get; set; }
        /// <summary>
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
    }
}
