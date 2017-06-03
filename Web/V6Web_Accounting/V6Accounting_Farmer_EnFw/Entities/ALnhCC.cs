namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALnhCC")]
    public partial class ALnhCC
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal loai_nh { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_nh { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_nh { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_nh2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }

}
