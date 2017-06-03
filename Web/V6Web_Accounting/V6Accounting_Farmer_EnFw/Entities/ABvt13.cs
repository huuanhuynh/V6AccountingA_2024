namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ABvt13
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string ma_kho { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal ton13 { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal du13 { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal du_nt13 { get; set; }

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
        [Column(Order = 8)]
        [StringLength(1)]
        public string status { get; set; }

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
