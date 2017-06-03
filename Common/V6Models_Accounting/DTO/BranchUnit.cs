using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
    public class BranchUnit : V6Model
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
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
        /// <summary>
        /// Column: DIA_CHI
        /// Description: 
        /// </summary>
        public string DiaChi { get; set; }
        /// <summary>
        /// Column: DIA_CHI2
        /// Description: 
        /// </summary>
        public string DiaChi2 { get; set; }
        /// <summary>
        /// Column: DIEN_THOAI
        /// Description: 
        /// </summary>
        public string DienThoai { get; set; }
        /// <summary>
        /// Column: NH_DVCS1
        /// Description: 
        /// </summary>
        public string Nhom_DVCS1 { get; set; }
        /// <summary>
        /// Column: NH_DVCS2
        /// Description: 
        /// </summary>
        public string Nhom_DVCS2 { get; set; }
        /// <summary>
        /// Column: NH_DVCS3
        /// Description: 
        /// </summary>
        public string Nhom_DVCS3 { get; set; }
    }
}
