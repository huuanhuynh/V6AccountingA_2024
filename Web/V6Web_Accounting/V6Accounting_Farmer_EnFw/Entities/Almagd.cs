namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Almagd")]
    public partial class Almagd
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string ma_ct_me { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(1)]
        public string ma_gd { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(48)]
        public string ten_gd { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(48)]
        public string ten_gd2 { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string form { get; set; }

        [Key]
        [Column(Order = 6)]
        public byte tk_cn { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(2)]
        public string loai_ct { get; set; }
    }

}
