namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALtknh")]
    public partial class ALtknh
    {
        [Key]
        [StringLength(16)]
        public string tk { get; set; }

        [Required]
        [StringLength(16)]
        public string tknh { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_tknh { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_tknh2 { get; set; }

        [StringLength(128)]
        public string dia_chi { get; set; }

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
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }
    }

}
