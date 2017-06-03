namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Althue")]
    public partial class Althue
    {
        [Key]
        [StringLength(8)]
        public string ma_thue { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_thue { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_thue2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_thue_co { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_thue_no { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }
    }
}
