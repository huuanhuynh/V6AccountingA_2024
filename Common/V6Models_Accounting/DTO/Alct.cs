using System;
using System.ComponentModel.DataAnnotations;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
  public  class AlCt : V6Model
    {
        public string ModuleID { get; set; }
        public string MaPhanHe { get; set; }
      [Key]
        public string MaCT { get; set; }
        public string TenCT { get; set; }
        public string TenCT2 { get; set; }
        public string MaCTMe { get; set; }
        public decimal so_ct { get; set; }
        public string MMaNk { get; set; }
        public string MMaGd { get; set; }
        public byte? MMaTd { get; set; }
        public string MaNt { get; set; }
        public string TieuDeCT { get; set; }
        public string TieuDe2 { get; set; }
        public decimal SoLien { get; set; }
        public string MaCTIn { get; set; }
        public string Form { get; set; }
        public byte STTCTNkc { get; set; }
        public byte STTCtntxt { get; set; }
        public byte? CTNxt { get; set; }
        public string MPhdbf { get; set; }
        public string MCtdbf { get; set; }
        public string MSTATUS { get; set; }
        public byte PostType { get; set; }
        public byte MSlCT0 { get; set; }
        public decimal MTrungSo { get; set; }
        public byte MLocNsd { get; set; }
        public byte MOngBa { get; set; }
        public byte MNgayCT { get; set; }
        public string Procedur { get; set; }
        public byte? STT { get; set; }
        public string MMaTd2 { get; set; }
        public string MMaTd3 { get; set; }
        public string MNgayTd1 { get; set; }
        public string MSlTd1 { get; set; }
        public string MSlTd2 { get; set; }
        public string MSlTd3 { get; set; }
        public string MGcTd1 { get; set; }
        public string MGcTd2 { get; set; }
        public string MGcTd3 { get; set; }
        public byte? Post2 { get; set; }
        public byte? Post3 { get; set; }
        public string MNgayTd2 { get; set; }
        public string MNgayTd3 { get; set; }
        public string DkCtgs { get; set; }
        public byte? KhYn { get; set; }
        public byte? CCYn { get; set; }
        public byte? NvYn { get; set; }
        public string MaCTOld { get; set; }
        public string MPhOld { get; set; }
        public byte? MBpBh { get; set; }
        public byte? MMaNvien { get; set; }
        public byte? MMaVv { get; set; }
        public string MMaHd { get; set; }
        public string MMaKu { get; set; }
        public string MMaPhi { get; set; }
        public string MMaVitri { get; set; }
        public string MMaLo { get; set; }
        public string MMaBpht { get; set; }
        public string MMaSp { get; set; }
        public string MKPost { get; set; }
        public string TkNo { get; set; }
        public string TkCo { get; set; }
        public string MMaLnx { get; set; }
        public string MHSD { get; set; }
        public byte MMaSonb { get; set; }
        public byte MSxoaNsd { get; set; }
        public string SizeCT { get; set; }
        public decimal ThemIn { get; set; }
        public string Phandau { get; set; }
        public string Phancuoi { get; set; }
        public string Dinhdang { get; set; }
        public string MMaKmb { get; set; }
        public string MMaKmm { get; set; }
        public string MSoLsx { get; set; }
        public string MMaKho2 { get; set; }
        public string F6BARCODE { get; set; }
        public string AdvAm { get; set; }
        public string AdvAd { get; set; }
    }
}
