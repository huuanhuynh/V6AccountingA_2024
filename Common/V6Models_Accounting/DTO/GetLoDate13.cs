using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace V6Soft.Models.Accounting.DTO
{
   public class GetLoDate13
   {
       [Column("Ma_kho")]
       public string MaKho { get; set; }
       [Column("Ma_vt")]
       public string MaVt { get; set; }
       [Column("Ma_vitri")]
       public string MaViTri { get; set; }
       [Column("Ma_lo")]
       public string MaLo { get; set; }
       [Column("Hsd")]
       public DateTime HanSuDung { get; set; }
       [Column("Dvt")]
       public string DonViTinh { get; set; }
       [Column("Tk_dl")]
       public string TkDl { get; set; }
       [Column("Stt_ntxt")]
       public short SttNtXt { get; set; }
       [Column("Ten_vt")]
       public string TenVatTu { get; set; }
       [Column("Ten_vt2")]
       public string TenVatTu2 { get; set; }
       [Column("Nh_vt1")]
       public string NhaVatTu1 { get; set; }
       [Column("Nh_vt2")]
       public string NhaVatTu2 { get; set; }
       [Column("Nh_vt3")]
       public string NhaVatTu3 { get; set; }
       [Column("Ton_dau")]
       public float TonDau { get; set; }
       [Column("Du_dau")]
       public float DuDau { get; set; }
       [Column("Du_dau_nt")]
       public float DuDauNt { get; set; }
    }
}
