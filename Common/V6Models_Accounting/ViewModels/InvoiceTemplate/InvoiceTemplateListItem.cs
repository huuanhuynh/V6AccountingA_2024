using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.InvoiceTemplate
{
    /// <summary>
    /// Mẫu hóa đơn
    /// </summary>
    public class InvoiceTemplateListItem : V6Model
    {
        /// <summary>
        /// Column: ma_mauhd
        /// Description: 
        /// </summary>
        public string MaMauHoaDon { get; set; }
        /// <summary>
        /// Column: ten_mauhd
        /// Description: 
        /// </summary>
        public string TenMauHoaDon { get; set; }
        /// <summary>
        /// Column: ten_mauhd2
        /// Description: 
        /// </summary>
        public string TenMauHoaDon2 { get; set; }
        /// <summary>
        /// Column: loai_mau
        /// Description: 
        /// </summary>
        public string LoaiMau { get; set; }
        /// <summary>
        /// Column: date0
        /// Description: 
        /// </summary>
        public DateTime NgayNhap { get; set; }
        /// <summary>
        /// Column: time0
        /// Description: 
        /// </summary>
        public string GioKhoiTao { get; set; }
        /// <summary>
        /// Column: user_id0
        /// Description: 
        /// </summary>
        public byte NguoiNhap { get; set; }
        /// <summary>
        /// Column: date2
        /// Description: 
        /// </summary>
        public DateTime NgaySua { get; set; }
        /// <summary>
        /// Column: time2
        /// Description: 
        /// </summary>
        public string ThoiGianSua { get; set; }
        /// <summary>
        /// Column: user_id2
        /// Description: 
        /// </summary>
        public byte NguoiSua { get; set; }
       
    }
}
