namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALstt")]
    public partial class ALstt
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal stt_rec { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "smalldatetime")]
        public DateTime ngay_dn { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ks { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal nam_bd { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_lt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ky1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ks_ky { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_Dk { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_Ck { get; set; }
    }

}
