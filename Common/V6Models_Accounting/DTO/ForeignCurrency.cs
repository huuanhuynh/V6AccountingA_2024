using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
    public class ForeignCurrency : V6Model
    {
        /// <summary>
        /// Column: ma_nt
        /// Description: 
        /// </summary>
        public string MaNgoaiTe { get; set; }
        /// <summary>
        /// Column: ten_nt
        /// Description: 
        /// </summary>
        public string TenNgoaiTe { get; set; }
        /// <summary>
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
        /// <summary>
        /// Column: ten_nt2
        /// Description: 
        /// </summary>
        public string TenNgoaiTe2 { get; set; }
        /// <summary>
        /// Column: tk_pscl_no
        /// Description: 
        /// </summary>
        public string TaiKhoanPhatSinhCL_No { get; set; }
        /// <summary>
        /// Column: tk_pscl_co
        /// Description: 
        /// </summary>
        public string TaiKhoanPhatSinhCL_Co { get; set; }
        /// <summary>
        /// Column: tk_dgcl_no
        /// Description: 
        /// </summary>
        public string TaiKhoanDGCL_No { get; set; }
        /// <summary>
        /// Column: tk_dgcl_co
        /// Description: 
        /// </summary>
        public string TaiKhoanDGCL_Co { get; set; }
    }
}
