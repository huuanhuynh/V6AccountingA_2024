namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V6rights
    {
        [Key]
        [Column(Order = 0)]
        public byte user_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string Sfield { get; set; }

        [Required]
        [StringLength(50)]
        public string D { get; set; }

        [Required]
        [StringLength(50)]
        public string V { get; set; }

        [Required]
        [StringLength(50)]
        public string E { get; set; }

        [StringLength(1)]
        public string Rread { get; set; }

        [StringLength(1)]
        public string Rhide { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(1)]
        public string MD { get; set; }

        [StringLength(10)]
        public string sfile { get; set; }

        [StringLength(1)]
        public string loai { get; set; }
    }

}
