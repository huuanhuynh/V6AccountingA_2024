namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALTHAU")]
    public partial class ALTHAU
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(16)]
        public string Ma_THAU { get; set; }

        [Required]
        [StringLength(48)]
        public string Ten_THAU { get; set; }

        [Required]
        [StringLength(48)]
        public string Ten_THAU2 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_hl { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_hl2 { get; set; }

        [StringLength(16)]
        public string Ma_THAU0 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string Ma_kh { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_sl1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_sl2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(128)]
        public string ghi_chu { get; set; }

        [Required]
        [StringLength(1)]
        public string Status { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }
    }
}
