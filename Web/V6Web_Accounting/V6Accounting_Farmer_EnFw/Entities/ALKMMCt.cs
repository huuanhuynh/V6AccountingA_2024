namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALKMMCt")]
    public partial class ALKMMCt
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string Ma_km { get; set; }

        [StringLength(16)]
        public string Ma_km0 { get; set; }

        [StringLength(16)]
        public string Ma_kh { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_hl { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_hl2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_sl1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_sl2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien2 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? pt_ck { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string ma_sp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_km { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_km { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(1)]
        public string Status { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(8)]
        public string time0 { get; set; }

        [Key]
        [Column(Order = 8)]
        public byte user_id0 { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(8)]
        public string time2 { get; set; }

        [Key]
        [Column(Order = 11)]
        public byte user_id2 { get; set; }
    }

}
