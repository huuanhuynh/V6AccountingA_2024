using System.ComponentModel.DataAnnotations;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
    public class V6Option : V6Model
    {
        /// <summary>
        /// Column: ma_phan_he
        /// Description: 
        /// </summary>
        
        public string MaPhanHe { get; set; }
        /// <summary>
        /// Column: stt
        /// Description: 
        /// </summary>
        [Key]
        public string STT { get; set; }
        /// <summary>
        /// Column: attribute
        /// Description: 
        /// </summary>
        public byte Attribute { get; set; }
        /// <summary>
        /// Column: name
        /// Description: 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Column: type
        /// Description: 
        /// </summary>
        public string Loai { get; set; }
        /// <summary>
        /// Column: descript
        /// Description: 
        /// </summary>
        public string Descript { get; set; }
        /// <summary>
        /// Column: descript2
        /// Description: 
        /// </summary>
        public string Descript2 { get; set; }
        /// <summary>
        /// Column: val
        /// Description: 
        /// </summary>
        public string Val { get; set; }
        /// <summary>
        /// Column: defaul
        /// Description: 
        /// </summary>
        public string Defaul { get; set; }
        /// <summary>
        /// Column: formattype
        /// Description: 
        /// </summary>
        public string FormatLoai { get; set; }
        /// <summary>
        /// Column: inputmask
        /// Description: 
        /// </summary>
        public string Inputmask { get; set; }

     
    }
}
