﻿using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Account
{
    /// <summary>
    /// Cái này có view không có table
    /// Tài khoản
    /// </summary>
    public class AccountListItem : V6Model
    {
        /// <summary>
        /// Column: tk
        /// Description: 
        /// </summary>
        public string Tai_Khoan { get; set; }
        /// <summary>
        /// Column: ten_tk
        /// Description: 
        /// </summary>
        public string Ten_TaiKhoan { get; set; }
        /// <summary>
        /// Column: ten_tk2
        /// Description: 
        /// </summary>
        public string Ten_TaiKhoan2 { get; set; }
        /// <summary>
        /// Column: ten_ngan
        /// Description: 
        /// </summary>
        public string Ten_ngan { get; set; }
        /// <summary>
        /// Column: ten_ngan2
        /// Description: 
        /// </summary>
        public string Ten_ngan2 { get; set; }
        /// <summary>
        /// Column: ma_nt
        /// Description: 
        /// </summary>
        public string MaNgoaiTe { get; set; }
        /// <summary>
        /// Column: loai_tk
        /// Description: 
        /// </summary>
        public byte Loai_TaiKhoan { get; set; }
        /// <summary>
        /// Column: tk_me
        /// Description: 
        /// </summary>
        public string TaiKhoan_me { get; set; }
        /// <summary>
        /// Column: bac_tk
        /// Description: 
        /// </summary>
        public byte bac_TaiKhoan { get; set; }
        /// <summary>
        /// Column: tk_sc
        /// Description: 
        /// </summary>
        public byte TaiKhoan_sc { get; set; }
        /// <summary>
        /// Column: tk_cn
        /// Description: 
        /// </summary>
        public byte TaiKhoanCongNo { get; set; }
        /// <summary>
        /// Column: nh_tk0
        /// Description: 
        /// </summary>
        public string nh_TaiKhoan0 { get; set; }
        /// <summary>
        /// Column: nh_tk2
        /// Description: 
        /// </summary>
        public string nh_TaiKhoan2 { get; set; }
        /// <summary>
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
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
