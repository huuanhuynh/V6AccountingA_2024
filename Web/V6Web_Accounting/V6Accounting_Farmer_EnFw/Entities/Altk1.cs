namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Altk1
    {
        [Key]
        [StringLength(16)]
        public string tk { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_tk { get; set; }

        [Required]
        [StringLength(64)]
        public string ds_tk { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_tk2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }
    }

}
