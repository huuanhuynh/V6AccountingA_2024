using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.LoaiNhapXuat
{
    public class LoaiNhapXuatItem : V6Model
    {
        /// <summary>
        /// Column: ma_lnx
        /// Description: 
        /// </summary>
        public string MaLoaiNhapXuat { get; set; }
        /// <summary>
        /// Column: ten_loai
        /// Description: 
        /// </summary>
        public string ten_Loai { get; set; }
        /// <summary>
        /// Column: ten_loai2
        /// Description: 
        /// </summary>
        public string ten_Loai2 { get; set; }
        /// <summary>
        /// Column: loai
        /// Description: 
        /// </summary>
        public string Loai { get; set; }
        /// <summary>
        /// Column: date0
        /// Description: 
        /// </summary>
        public DateTime NgayKhoiTao { get; set; }
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
