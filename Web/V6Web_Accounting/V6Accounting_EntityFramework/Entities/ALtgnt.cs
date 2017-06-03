namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALtgnt")]
    public partial class ALtgnt
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string ma_nt { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(8)]
        public string time0 { get; set; }

        [Key]
        [Column(Order = 5)]
        public byte user_id0 { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }
    }
}
