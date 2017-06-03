using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
   public class ForeignExchangeRate : V6Model
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
