namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AKhungCK")]
    public partial class AKhungCK
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string khung_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_hl { get; set; }

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
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }

        [Required]
        [StringLength(16)]
        public string Ma_kh { get; set; }
    }
}
