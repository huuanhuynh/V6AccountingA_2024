using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.DiscountType
{
    /// <summary>
    /// Loại chiết khấu
    /// </summary>
    public class DiscountTypeListItem : V6Model
    {
        /// <summary>
        /// Column: ma_loai
        /// Description: 
        /// </summary>
        public string MaLoai { get; set; }
        /// <summary>
        /// Column: ten_loai
        /// Description: 
        /// </summary>
        public string Ten_Loai { get; set; }
        /// <summary>
        /// Column: ten_loai2
        /// Description: 
        /// </summary>
        public string Ten_Loai2 { get; set; }
    }
}
