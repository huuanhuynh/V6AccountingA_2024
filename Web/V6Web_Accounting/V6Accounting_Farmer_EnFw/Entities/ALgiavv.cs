namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALgiavv")]
    public partial class ALgiavv
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(16)]
        public string ma_vv { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "smalldatetime")]
        public DateTime ngay_ban { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal gia_nt2 { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal gia2 { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string time0 { get; set; }

        [Key]
        [Column(Order = 6)]
        public byte user_id0 { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(8)]
        public string time2 { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(8)]
        public string nh_vt2 { get; set; }

        [Key]
        [Column(Order = 10)]
        public byte user_id2 { get; set; }
    }

}
