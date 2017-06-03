namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V6option
    {
        [Required]
        [StringLength(24)]
        public string ma_phan_he { get; set; }

        [Key]
        [StringLength(3)]
        public string stt { get; set; }

        public byte attribute { get; set; }

        [Required]
        [StringLength(24)]
        public string name { get; set; }

        [Required]
        [StringLength(1)]
        public string type { get; set; }

        [Required]
        [StringLength(64)]
        public string descript { get; set; }

        [Required]
        [StringLength(64)]
        public string descript2 { get; set; }

        [Required]
        [StringLength(72)]
        public string val { get; set; }

        [Required]
        [StringLength(72)]
        public string defaul { get; set; }

        [Required]
        [StringLength(2)]
        public string formattype { get; set; }

        [Required]
        [StringLength(48)]
        public string inputmask { get; set; }
    }
}
