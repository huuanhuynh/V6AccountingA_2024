namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ALnhtk0
    {
        [Key]
        [StringLength(4)]
        public string ma_nh { get; set; }

        [Required]
        [StringLength(96)]
        public string ten_nh { get; set; }

        [Required]
        [StringLength(96)]
        public string ten_nh2 { get; set; }
    }

}
