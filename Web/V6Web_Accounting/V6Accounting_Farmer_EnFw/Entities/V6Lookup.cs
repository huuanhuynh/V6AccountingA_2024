namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V6Lookup
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(16)]
        public string vVar { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string vMa_file { get; set; }

        [Required]
        [StringLength(16)]
        public string vOrder { get; set; }

        [Required]
        [StringLength(16)]
        public string vValue { get; set; }

        [Required]
        [StringLength(254)]
        public string vLfScatter { get; set; }

        [Required]
        [StringLength(128)]
        public string vWidths { get; set; }

        [Required]
        [StringLength(254)]
        public string vFields { get; set; }

        [Required]
        [StringLength(254)]
        public string eFields { get; set; }

        [Required]
        [StringLength(254)]
        public string vHeaders { get; set; }

        [Required]
        [StringLength(254)]
        public string eHeaders { get; set; }

        [Required]
        [StringLength(1)]
        public string vUpdate { get; set; }

        [Required]
        [StringLength(254)]
        public string vTitle { get; set; }

        [Required]
        [StringLength(254)]
        public string eTitle { get; set; }

        [StringLength(254)]
        public string VTitlenew { get; set; }

        [StringLength(254)]
        public string ETitlenew { get; set; }

        public byte Large_yn { get; set; }

        [StringLength(100)]
        public string v1Title { get; set; }

        [StringLength(100)]
        public string e1Title { get; set; }

        [StringLength(128)]
        public string V_Search { get; set; }
    }

}
