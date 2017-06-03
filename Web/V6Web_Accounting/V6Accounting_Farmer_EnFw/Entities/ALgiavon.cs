namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALgiavon")]
    public partial class ALgiavon
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string loai { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? giavon { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? giavonnt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }
    }

}
