namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Altk2
    {
        [StringLength(1)]
        public string type { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(16)]
        public string tk2 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string nh_tk2 { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(48)]
        public string ten_tk2 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(48)]
        public string ten_tk22 { get; set; }

        [StringLength(1)]
        public string dau { get; set; }

        [Key]
        [Column(Order = 4)]
        public byte loai { get; set; }

        [Key]
        [Column(Order = 5)]
        public byte bac { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [StringLength(8)]
        public string time0 { get; set; }

        public byte? user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }
    }

}
