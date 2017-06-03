using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Customer
{
    public class CustomerListItem : V6Model
    {        
        /// <summary>
        ///     Column: ma_kh
        ///     Description: 
        /// </summary>
        public string MaKhachHang { get; set; }

        /// <summary>
        ///     Column: ten_kh
        ///     Description: 
        /// </summary>
        public string TenKhachHang { get; set; }

        /// <summary>
        ///     Column: ma_so_thue
        ///     Description: 
        /// </summary>
        public string MaSoThue { get; set; }

        /// <summary>
        ///     Column: dia_chi
        ///     Description: 
        /// </summary>
        public string DiaChi { get; set; }

        /// <summary>
        ///     Column: dien_thoai
        ///     Description: 
        /// </summary>
        public string DienThoai { get; set; }

        /// <summary>
        ///     Column: fax
        ///     Description: 
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        ///     Column: e_mail
        ///     Description: 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Column: ghi_chu
        ///     Description: 
        /// </summary>
        public string GhiChu { get; set; }

        /// <summary>
        ///     Column: status
        ///     Description: 
        /// </summary>
        public string TrangThai { get; set; }

        /// <summary>
        ///     Column: kh_yn
        ///     Description: 
        /// </summary>
        public bool LaKhachHang { get; set; }

        /// <summary>
        ///     Column: cc_yn
        ///     Description: 
        /// </summary>
        public bool LaNhaCungCap { get; set; }

        /// <summary>
        ///     Column: nv_yn
        ///     Description: 
        /// </summary>
        public bool LaNhanVien { get; set; }

        /// <summary>
        ///     Column: DT_DD
        ///     Description: 
        /// </summary>
        public string DienThoaiDiDong { get; set; }
    }
}
