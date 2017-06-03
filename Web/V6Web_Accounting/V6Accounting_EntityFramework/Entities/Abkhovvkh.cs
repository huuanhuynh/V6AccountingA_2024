namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Abkhovvkh")]
    public partial class Abkhovvkh
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string tk { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_kh { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string ma_vv { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no_nt00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co_nt00 { get; set; }

        [StringLength(128)]
        public string dien_giai { get; set; }

        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ty_gia { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }
    }
}
