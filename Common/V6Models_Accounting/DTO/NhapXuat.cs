using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
    /// <summary>
    /// Cái này có trong view, không có trong table nên entity chưa có!
    /// </summary>
    public class NhapXuat : V6Model
    {
        /// <summary>
        /// Column: ma_nx
        /// Description: 
        /// </summary>
        public string MaNhapXuat { get; set; }
        /// <summary>
        /// Column: TEN_NX
        /// Description: 
        /// </summary>
        public string TenNhapXuat { get; set; }
        /// <summary>
        /// Column: TEN_NX2
        /// Description: 
        /// </summary>
        public string TenNhapXuat2 { get; set; }
        /// <summary>
        /// Column: TK
        /// Description: 
        /// </summary>
        public string TaiKhoan { get; set; }
        /// <summary>
        /// Column: TK_CN
        /// Description: 
        /// </summary>
        public string TaiKhoanCongNo { get; set; }
    }
}
