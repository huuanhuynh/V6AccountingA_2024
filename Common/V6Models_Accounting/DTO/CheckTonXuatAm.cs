using System.ComponentModel.DataAnnotations.Schema;

namespace V6Soft.Models.Accounting.DTO
{
    public class CheckTonXuatAm
    {
        [Column("ma_kho")]
        public string MaKho { get; set; }
        [Column("ma_vt")]
        public string MaVatTu { get; set; }
        [Column("du00")]
        public double Du { get; set; }
        [Column("ton00")]
        public double Ton { get; set; }
        [Column("du_nt00")]
        public double DuNt { get; set; }
    }
}
