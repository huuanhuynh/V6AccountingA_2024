using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
    /// <summary>
    /// Phân bổ
    /// </summary>
    public class PhanBo : V6Model
    {
        /// <summary>
        /// Column: nam
        /// Description: 
        /// </summary>
        public decimal Nam { get; set; }
        /// <summary>
        /// Column: tag
        /// Description: 
        /// </summary>
        public string TAG { get; set; }
        /// <summary>
        /// Column: stt
        /// Description: 
        /// </summary>
        public decimal STT { get; set; }
        /// <summary>
        /// Column: stt_rec
        /// Description: 
        /// </summary>
        public string STT_REC { get; set; }
        /// <summary>
        /// Column: ten_bt
        /// Description: 
        /// </summary>
        public string Ten_BT { get; set; }
        /// <summary>
        /// Column: tk
        /// Description: 
        /// </summary>
        public string TaiKhoan { get; set; }
        /// <summary>
        /// Column: loai_pb
        /// Description: 
        /// </summary>
        public byte LoaiPhanBo { get; set; }
        /// <summary>
        /// Column: ps_no_co
        /// Description: 
        /// </summary>
        public byte PhatSinhNoCo { get; set; }
        /// <summary>
        /// Column: so_ct
        /// Description: 
        /// </summary>
        public string SoChungTu { get; set; }
        /// <summary>
        /// Column: tien_nt
        /// Description: 
        /// </summary>
        public decimal TienNgoaiTe { get; set; }
        /// <summary>
        /// Column: ma_nt
        /// Description: 
        /// </summary>
        public string MaNgoaiTe { get; set; }
        /// <summary>
        /// Column: ty_gia
        /// Description: 
        /// </summary>
        public decimal ty_Gia { get; set; }
        /// <summary>
        /// Column: tien
        /// Description: 
        /// </summary>
        public decimal Tien { get; set; }
        /// <summary>
        /// Column: ma_dvcs
        /// Description: 
        /// </summary>
        public string MaDonVi { get; set; }
        /// <summary>
        /// Column: stt_rec01
        /// Description: 
        /// </summary>
        public string STT_REC01 { get; set; }
        /// <summary>
        /// Column: stt_rec02
        /// Description: 
        /// </summary>
        public string STT_REC02 { get; set; }
        /// <summary>
        /// Column: stt_rec03
        /// Description: 
        /// </summary>
        public string STT_REC03 { get; set; }
        /// <summary>
        /// Column: stt_rec04
        /// Description: 
        /// </summary>
        public string STT_REC04 { get; set; }
        /// <summary>
        /// Column: stt_rec05
        /// Description: 
        /// </summary>
        public string STT_REC05 { get; set; }
        /// <summary>
        /// Column: stt_rec06
        /// Description: 
        /// </summary>
        public string STT_REC06 { get; set; }
        /// <summary>
        /// Column: stt_rec07
        /// Description: 
        /// </summary>
        public string STT_REC07 { get; set; }
        /// <summary>
        /// Column: stt_rec08
        /// Description: 
        /// </summary>
        public string STT_REC08 { get; set; }
        /// <summary>
        /// Column: stt_rec09
        /// Description: 
        /// </summary>
        public string STT_REC09 { get; set; }
        /// <summary>
        /// Column: stt_rec10
        /// Description: 
        /// </summary>
        public string STT_REC10 { get; set; }
        /// <summary>
        /// Column: stt_rec11
        /// Description: 
        /// </summary>
        public string STT_REC11 { get; set; }
        /// <summary>
        /// Column: stt_rec12
        /// Description: 
        /// </summary>
        public string STT_REC12 { get; set; }
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
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
        /// <summary>
        /// Column: date2
        /// Description: 
        /// </summary>
        public DateTime? NgaySua { get; set; }
        /// <summary>
        /// Column: time2
        /// Description: 
        /// </summary>
        public string ThoiGianSua { get; set; }
        /// <summary>
        /// Column: user_id2
        /// Description: 
        /// </summary>
        public byte? NguoiSua { get; set; }
        /// <summary>
        /// Column: so_ct01
        /// Description: 
        /// </summary>
        public string SoChungTu01 { get; set; }
        /// <summary>
        /// Column: so_ct02
        /// Description: 
        /// </summary>
        public string SoChungTu02 { get; set; }
        /// <summary>
        /// Column: so_ct03
        /// Description: 
        /// </summary>
        public string SoChungTu03 { get; set; }
        /// <summary>
        /// Column: so_ct04
        /// Description: 
        /// </summary>
        public string SoChungTu04 { get; set; }
        /// <summary>
        /// Column: so_ct05
        /// Description: 
        /// </summary>
        public string SoChungTu05 { get; set; }
        /// <summary>
        /// Column: so_ct06
        /// Description: 
        /// </summary>
        public string SoChungTu06 { get; set; }
        /// <summary>
        /// Column: so_ct07
        /// Description: 
        /// </summary>
        public string SoChungTu07 { get; set; }
        /// <summary>
        /// Column: so_ct08
        /// Description: 
        /// </summary>
        public string SoChungTu08 { get; set; }
        /// <summary>
        /// Column: so_ct09
        /// Description: 
        /// </summary>
        public string SoChungTu09 { get; set; }
        /// <summary>
        /// Column: so_ct10
        /// Description: 
        /// </summary>
        public string SoChungTu10 { get; set; }
        /// <summary>
        /// Column: so_ct11
        /// Description: 
        /// </summary>
        public string SoChungTu11 { get; set; }
        /// <summary>
        /// Column: so_ct12
        /// Description: 
        /// </summary>
        public string SoChungTu12 { get; set; }
        /// <summary>
        /// Column: loai
        /// Description: 
        /// </summary>
        public string Loai { get; set; }
        /// <summary>
        /// Column: Ten_loai
        /// Description: 
        /// </summary>
        public string Ten_Loai { get; set; }
        /// <summary>
        /// Column: MA_BPHTPH
        /// Description: 
        /// </summary>
        public string MaBoPhanHachToanph { get; set; }
        /// <summary>
        /// Column: AUTO
        /// Description: 
        /// </summary>
        public string Auto { get; set; }
        /// <summary>
        /// Column: LOAI_PBCP
        /// Description: 
        /// </summary>
        public string LoaiPhanBocp { get; set; }
    }
}
