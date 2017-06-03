namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CorpLang")]
    public partial class CorpLang
    {
        [Key]
        [StringLength(1)]
        public string Lan_Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Lan_name { get; set; }

        [Required]
        [StringLength(64)]
        public string Lan_name2 { get; set; }
    }

}
