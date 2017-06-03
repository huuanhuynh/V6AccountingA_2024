using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.ForeignExchangeRate
{
    public class ForeignExchangeRateDetail : V6Model
    {
        /// <summary>
        /// Column: ma_nt
        /// Description: 
        /// </summary>
        public string MaNgoaiTe { get; set; }
        /// <summary>
        /// Column: ngay_ct
        /// Description: 
        /// </summary>
        public DateTime NgayChungTu { get; set; }
        /// <summary>
        /// Column: ty_gia
        /// Description: 
        /// </summary>
        public decimal ty_Gia { get; set; }
        /// <summary>
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
    }
}
