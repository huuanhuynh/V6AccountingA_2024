namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ALgia2
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(16)]
        public string ma_vt { get; set; }

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
        [Column(Order = 4)]
        [StringLength(3)]
        public string ma_nt { get; set; }

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

        [StringLength(8)]
        public string ma_gia { get; set; }

        [StringLength(10)]
        public string Dvt { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(16)]
        public string Ma_kh { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(1)]
        public string STATUS { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NGAY_HHL { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(100)]
        public string CHECK_SYNC { get; set; }
    }
}
