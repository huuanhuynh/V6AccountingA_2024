namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALnhkh")]
    public partial class ALnhkh
    {
        [Key]
        [Column(Order = 0)]
        public byte loai_nh { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_nh { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(48)]
        public string ten_nh { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(48)]
        public string ten_nh2 { get; set; }

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
        [Column(Order = 7)]
        [StringLength(1)]
        public string status { get; set; }

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

        [Key]
        [Column(Order = 11)]
        [StringLength(100)]
        public string CHECK_SYNC { get; set; }

        [Key]
        [Column(Order = 12)]
        public Guid UID { get; set; }
    }
}
