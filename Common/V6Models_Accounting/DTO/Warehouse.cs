using System;

using V6Soft.Models.Core;


namespace V6Soft.Models.Accounting.DTO
{
    public class Warehouse : V6Model
    {
        /// <summary>
        /// Column: ma_kho
        /// Description: Mã kho.
        /// </summary>
        public string MaKho { get; set; }

        /// <summary>
        /// Column: ten_kho
        /// Description: Tên kho.
        /// </summary>
        public string TenKho { get; set; }

        /// <summary>
        /// Column: ten_kho2
        /// Description: Tên kho bằng tiếng anh.
        /// </summary>
        public string TenKho2 { get; set; }

        /// <summary>
        /// Column: tk_dl
        /// Description: Tài khoản đại lý.
        /// </summary>
        public string TaiKhoanDaiLy { get; set; }

        /// <summary>
        /// Column: stt_ntxt
        /// Description: số thứ tự
        /// </summary>
        public byte  Stt { get; set; }

        /// <summary>
        /// Column: thu_kho
        /// Description: Tên thủ kho.
        /// </summary>
        public string TenThuKho { get; set; }

        /// <summary>
        /// Column: dia_chi
        /// Description: Địa chỉ của kho.
        /// </summary>
        public string DiaChi { get; set; }

        /// <summary>
        /// Column: fax
        /// Description: Số fax của kho.
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// Column: email
        /// Description: Địa chỉ email để liên lạc bên kho.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Column: ma_vc
        /// Description: Mã vận chuyển.
        /// </summary>
        public string MaVanChuyen { get; set; }

        /// <summary>
        /// Column: ma_dvcs
        /// Description: Mã đơn vị cơ sở.
        /// </summary>
        public string MaDonVi { get; set; }

        /// <summary>
        /// Column: status
        /// Description: Trạng thái.
        /// </summary>
        public string TrangThai { get; set; }

        /// <summary>
        /// Column: date_yn
        /// Description: Có theo dõi hạn sử dụng.
        /// </summary>
        public bool CoTheoDoiHanSuDung { get; set; }

        /// <summary>
        /// Column: lo_yn
        /// Description: Có theo dõi lô.
        /// </summary>
        public bool CoTheoDoiLoHang { get; set; }

        /// <summary>
        /// Column: NH_DVCS1
        /// </summary>
        public string NhomDonViCoSo { get; set; }
    }
}
