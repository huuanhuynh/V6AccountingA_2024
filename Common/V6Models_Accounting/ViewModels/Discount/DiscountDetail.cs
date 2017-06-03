using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Discount
{
    /// <summary>
    /// Chiết khấu
    /// </summary>
    public class DiscountDetail : V6Model
    {
        /// <summary>
        /// Column: ma_ck
        /// Description: 
        /// </summary>
        public string MaChietKhau { get; set; }
        /// <summary>
        /// Column: ten_ck
        /// Description: 
        /// </summary>
        public string TenChietKhau { get; set; }
        /// <summary>
        /// Column: ten_ck2
        /// Description: 
        /// </summary>
        public string TenChietKhau2 { get; set; }
        /// <summary>
        /// Column: loai_ck
        /// Description: 
        /// </summary>
        public string LoaiChietKhau { get; set; }
        public string Type_ck { get; set; }

        /// <summary>
        /// Column: muc_do
        /// Description: 
        /// </summary>
        public decimal? Muc_DO { get; set; }
        /// <summary>
        /// Column: tien_yn
        /// Description: 
        /// </summary>
        public string ChoPhepSuaTien { get; set; }
        /// <summary>
        /// Column: tienh_yn
        /// Description: 
        /// </summary>
        public string ChoPhepSuaTienH { get; set; }
        /// <summary>
        /// Column: cong_yn
        /// Description: 
        /// </summary>
        public string Cong_YN { get; set; }
        /// <summary>
        /// Column: con_lai_yn
        /// Description: 
        /// </summary>
        public string ConLai_YN { get; set; }
        /// <summary>
        /// Column: ngay_ct1
        /// Description: 
        /// </summary>
        public DateTime? NgayChungTu1 { get; set; }
        /// <summary>
        /// Column: ngay_ct2
        /// Description: 
        /// </summary>
        public DateTime? NgayChungTu2 { get; set; }
        /// <summary>
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
        /// <summary>
        /// Column: ma_td1
        /// Description: 
        /// </summary>
        public string MaTuDo1 { get; set; }
        /// <summary>
        /// Column: ma_td2
        /// Description: 
        /// </summary>
        public string MaTuDo2 { get; set; }
        /// <summary>
        /// Column: ma_td3
        /// Description: 
        /// </summary>
        public string MaTuDo3 { get; set; }
        /// <summary>
        /// Column: ngay_td1
        /// Description: 
        /// </summary>
        public DateTime? NgayTuDo1 { get; set; }
        /// <summary>
        /// Column: ngay_td2
        /// Description: 
        /// </summary>
        public DateTime? NgayTuDo2 { get; set; }
        /// <summary>
        /// Column: ngay_td3
        /// Description: 
        /// </summary>
        public DateTime? NgayTuDo3 { get; set; }
        /// <summary>
        /// Column: sl_td1
        /// Description: 
        /// </summary>
        public decimal? SoLuongTuDo1 { get; set; }
        /// <summary>
        /// Column: sl_td2
        /// Description: 
        /// </summary>
        public decimal? SoLuongTuDo2 { get; set; }
        /// <summary>
        /// Column: sl_td3
        /// Description: 
        /// </summary>
        public decimal? SoLuongTuDo3 { get; set; }
        /// <summary>
        /// Column: gc_td1
        /// Description: 
        /// </summary>
        public string GhiChuTuDo1 { get; set; }
        /// <summary>
        /// Column: gc_td2
        /// Description: 
        /// </summary>
        public string GhiChuTuDo2 { get; set; }
        /// <summary>
        /// Column: gc_td3
        /// Description: 
        /// </summary>
        public string GhiChuTuDo3 { get; set; }
    }
}
