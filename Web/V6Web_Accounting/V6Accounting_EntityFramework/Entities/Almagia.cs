namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Almagia")]
    public partial class Almagia
    {
        [Key]
        [StringLength(8)]
        public string ma_gia { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_gia { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_gia2 { get; set; }

        [Required]
        [StringLength(1)]
        public string Loai { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }
    }
}
