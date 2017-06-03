using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.CustomerGroup
{
    public class CustomerGroupDetail : V6Model
    {
        /// <summary>
        /// Column: loai_nh
        /// Description: 
        /// </summary>
        public byte LoaiNhom { get; set; }
        /// <summary>
        /// Column: ma_nh
        /// Description: 
        /// </summary>
        public string MaNhom { get; set; }
        /// <summary>
        /// Column: ten_nh
        /// Description: 
        /// </summary>
        public string TenNhom { get; set; }
        /// <summary>
        /// Column: ten_nh2
        /// Description: 
        /// </summary>
        public string TenNhom2 { get; set; }
        /// <summary>
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
        /// <summary>
        /// Column: CHECK_SYNC
        /// Description: 
        /// </summary>
        public string CheckSync { get; set; }
       
    }
}
