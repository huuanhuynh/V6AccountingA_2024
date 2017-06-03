namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Alnt")]
    public partial class Alnt
    {
        [Key]
        [StringLength(3)]
        public string ma_nt { get; set; }

        [Required]
        [StringLength(16)]
        public string ten_nt { get; set; }

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

        [Required]
        [StringLength(16)]
        public string ten_nt2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_pscl_no { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_pscl_co { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_dgcl_no { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_dgcl_co { get; set; }
    }
}
