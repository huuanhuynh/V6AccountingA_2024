using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
   public class Tax : V6Model
    {
        /// <summary>
        /// Column: ma_thue
        /// Description: 
        /// </summary>
        public string MaThue { get; set; }
        /// <summary>
        /// Column: ten_thue
        /// Description: 
        /// </summary>
        public string Ten_thue { get; set; }
        /// <summary>
        /// Column: ten_thue2
        /// Description: 
        /// </summary>
        public string Ten_thue2 { get; set; }
        /// <summary>
        /// Column: thue_suat
        /// Description: 
        /// </summary>
        public decimal ThueSuat { get; set; }
        /// <summary>
        /// Column: tk_thue_co
        /// Description: 
        /// </summary>
        public string TaiKhoan_thue_co { get; set; }
        /// <summary>
        /// Column: tk_thue_no
        /// Description: 
        /// </summary>
        public string TaiKhoan_thue_no { get; set; }
        /// <summary>
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
    }
}
