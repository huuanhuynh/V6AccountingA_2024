namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Agltc5
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal stt { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal bold { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal in_ck { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(64)]
        public string chi_tieu { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(64)]
        public string chi_tieu2 { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string ma_so { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(48)]
        public string cach_tinh { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(48)]
        public string tk_no { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(48)]
        public string tk_co { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal dau { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_truoc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_nay { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_truocnt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_naynt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ids { get; set; }

        public byte? iscal { get; set; }
    }

}
