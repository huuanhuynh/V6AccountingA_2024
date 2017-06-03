namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Corpuser")]
    public partial class Corpuser
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal user_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string user_name { get; set; }

        [Required]
        [StringLength(8)]
        public string user_pre { get; set; }

        [Required]
        [StringLength(64)]
        public string password { get; set; }

        [Required]
        [StringLength(48)]
        public string comment { get; set; }

        public bool is_Vadmin { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string is_Madmin { get; set; }

        [Column(TypeName = "numeric")]
        public decimal del_yn { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Rmodule { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Inherit_id { get; set; }

        [Required]
        [StringLength(1)]
        public string inherit_ch { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(100)]
        public string CHECK_SYNC { get; set; }

        [Required]
        [StringLength(2)]
        public string LEVEL { get; set; }
    }
}
