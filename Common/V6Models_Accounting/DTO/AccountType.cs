using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
   public class AccountType : V6Model
    {
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
    }
}
