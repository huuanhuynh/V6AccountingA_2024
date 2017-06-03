namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALTHAUCT")]
    public partial class ALTHAUCT
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string Ma_THAU { get; set; }

        [StringLength(16)]
        public string Ma_THAU0 { get; set; }

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
        public decimal? sl_km { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_km { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(1)]
        public string Status { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(8)]
        public string time0 { get; set; }

        [Key]
        [Column(Order = 7)]
        public byte user_id0 { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(8)]
        public string time2 { get; set; }

        [Key]
        [Column(Order = 10)]
        public byte user_id2 { get; set; }

        [Key]
        [Column(Order = 11)]
        public string Ghi_chukm { get; set; }

        [Key]
        [Column(Order = 12)]
        public string Ghi_chuck { get; set; }

        [Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal T_SLKM { get; set; }
    }

}
