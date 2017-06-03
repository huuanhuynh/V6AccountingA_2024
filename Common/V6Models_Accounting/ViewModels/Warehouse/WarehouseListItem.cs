using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Warehouse
{
    /// <summary>
    /// Danh mục kho hàng
    /// </summary>
    public class WarehouseListItem : V6Model
    {
        /// <summary>
        /// Column: ma_kho
        /// Description: 
        /// </summary>
        public string MaKho { get; set; }
        /// <summary>
        /// Column: ten_kho
        /// Description: 
        /// </summary>
        public string TenKho { get; set; }
        /// <summary>
        /// Column: ten_kho2
        /// Description: 
        /// </summary>
        public string TenKho2 { get; set; }
        /// <summary>
        /// Column: tk_dl
        /// Description: 
        /// </summary>
        public string TaiKhoanDaiLy { get; set; }
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
