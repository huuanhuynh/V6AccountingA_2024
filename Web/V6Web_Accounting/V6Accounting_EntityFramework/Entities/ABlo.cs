namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ABlo")]
    public partial class ABlo
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_kho { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(8)]
        public string ma_vitri { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string ma_lo { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal ton00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? du00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? du_nt00 { get; set; }

        [StringLength(128)]
        public string dien_giai { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(8)]
        public string time0 { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(8)]
        public string Ma_Dvcs { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }
    }
}
