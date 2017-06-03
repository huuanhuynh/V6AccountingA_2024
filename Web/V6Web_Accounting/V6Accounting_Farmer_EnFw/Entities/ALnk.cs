namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALnk")]
    public partial class ALnk
    {
        [Required]
        [StringLength(3)]
        public string Ma_ct { get; set; }

        [Key]
        [StringLength(3)]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_nk { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_nk2 { get; set; }

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
