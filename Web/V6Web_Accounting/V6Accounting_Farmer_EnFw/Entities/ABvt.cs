namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ABvt")]
    public partial class ABvt
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
        [StringLength(16)]
        public string ma_vt { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal ton00 { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal du00 { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal du_nt00 { get; set; }

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

        [Key]
        [Column(Order = 9)]
        [StringLength(1)]
        public string status { get; set; }

        [Key]
        [Column(Order = 10, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(8)]
        public string time2 { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal user_id2 { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(8)]
        public string Ma_Dvcs { get; set; }
    }

}
