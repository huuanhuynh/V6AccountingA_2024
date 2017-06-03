namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Altd2
    {
        [Key]
        [StringLength(16)]
        public string ma_td2 { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_td2 { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_td22 { get; set; }

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

        [StringLength(1)]
        public string status { get; set; }

        [Required]
        [StringLength(100)]
        public string CHECK_SYNC { get; set; }

        [StringLength(24)]
        public string GC_TD1 { get; set; }

        [StringLength(24)]
        public string GC_TD2 { get; set; }

        [StringLength(24)]
        public string GC_TD3 { get; set; }
    }

}
