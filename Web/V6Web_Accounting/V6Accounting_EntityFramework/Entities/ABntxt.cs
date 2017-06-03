namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ABntxt")]
    public partial class ABntxt
    {
        [Column(TypeName = "numeric")]
        public decimal? nam { get; set; }

        [StringLength(13)]
        public string stt_rec_nt { get; set; }

        [StringLength(5)]
        public string stt_rec0 { get; set; }

        public byte? pn_co_gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ngay { get; set; }

        [StringLength(3)]
        public string ma_ct { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? stt_ctntxt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

        [StringLength(8)]
        public string ma_kho { get; set; }

        [StringLength(16)]
        public string ma_vt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_luong { get; set; }

        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_cp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_cp_nt { get; set; }

        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal ton_kho01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt01 { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal ton_kho02 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du02 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt02 { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal ton_kho03 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du03 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt03 { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal ton_kho04 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du04 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt04 { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal ton_kho05 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du05 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt05 { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal ton_kho06 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du06 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt06 { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal ton_kho07 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du07 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt07 { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ton_kho08 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du08 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt08 { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal ton_kho09 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du09 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt09 { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal ton_kho10 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du10 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt10 { get; set; }

        [Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal ton_kho11 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du11 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt11 { get; set; }

        [Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal ton_kho12 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du12 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt12 { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal ton_kho13 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du13 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt13 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [StringLength(8)]
        public string time0 { get; set; }

        public byte? user_id0 { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(8)]
        public string Ma_Dvcs { get; set; }
    }
}
