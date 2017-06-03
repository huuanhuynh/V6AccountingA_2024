namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ACvv")]
    public partial class ACvv
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string tk { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string ma_vv { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal lk_no { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal lk_co { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal lk_no_nt { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal lk_co_nt { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(8)]
        public string time0 { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(1)]
        public string status { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }
}
