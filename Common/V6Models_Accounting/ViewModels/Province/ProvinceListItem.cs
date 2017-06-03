using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Province
{
    /// <summary>
    /// Tỉnh Thành
    /// </summary>
    public class ProvinceListItem : V6Model
    {
        /// <summary>
        /// Column: ma_tinh
        /// Description: 
        /// </summary>
        public string MaTinh { get; set; }
        /// <summary>
        /// Column: ten_tinh
        /// Description: 
        /// </summary>
        public string TenTinh { get; set; }
        /// <summary>
        /// Column: ten_tinh2
        /// Description: 
        /// </summary>
        public string TenTinh2 { get; set; }
    }
}
