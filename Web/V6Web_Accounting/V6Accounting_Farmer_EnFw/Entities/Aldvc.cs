namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Aldvc
    {
        [Key]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_dvcs { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_dvcs2 { get; set; }

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

        [StringLength(128)]
        public string DIA_CHI { get; set; }

        [StringLength(128)]
        public string DIA_CHI2 { get; set; }

        [StringLength(128)]
        public string DIEN_THOAI { get; set; }

        [Required]
        [StringLength(8)]
        public string NH_DVCS1 { get; set; }

        [Required]
        [StringLength(8)]
        public string NH_DVCS2 { get; set; }

        [Required]
        [StringLength(8)]
        public string NH_DVCS3 { get; set; }
    }

}
