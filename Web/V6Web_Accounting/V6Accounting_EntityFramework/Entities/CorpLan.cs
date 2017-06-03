namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CorpLan")]
    public partial class CorpLan
    {
        [Required]
        [StringLength(50)]
        public string SFile { get; set; }

        [Required]
        [StringLength(20)]
        public string SField { get; set; }

        [StringLength(50)]
        public string Id { get; set; }

        [Required]
        [StringLength(1)]
        public string ctype { get; set; }

        [Required]
        [StringLength(200)]
        public string Sname { get; set; }

        [Required]
        [StringLength(254)]
        public string D { get; set; }

        [Required]
        [StringLength(254)]
        public string V { get; set; }

        [Required]
        [StringLength(254)]
        public string E { get; set; }

        [Required]
        [StringLength(254)]
        public string F { get; set; }

        [Required]
        [StringLength(254)]
        public string C { get; set; }

        [Required]
        [StringLength(254)]
        public string R { get; set; }

        [Required]
        [StringLength(254)]
        public string J { get; set; }

        [Required]
        [StringLength(254)]
        public string K { get; set; }

        [StringLength(3)]
        public string Ma_ct { get; set; }

        [StringLength(1)]
        public string M_or_D { get; set; }
    }
}
