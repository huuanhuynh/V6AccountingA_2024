using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
  public class PriceCode : V6Model
    {
        /// <summary>
        /// Column: ma_gia
        /// Description: 
        /// </summary>
        public string MaGia { get; set; }
        /// <summary>
        /// Column: ten_gia
        /// Description: 
        /// </summary>
        public string Ten_Gia { get; set; }
        /// <summary>
        /// Column: ten_gia2
        /// Description: 
        /// </summary>
        public string Ten_Gia2 { get; set; }
        /// <summary>
        /// Column: Loai
        /// Description: 
        /// </summary>
        public string Loai { get; set; }
        /// <summary>
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
    }
}
