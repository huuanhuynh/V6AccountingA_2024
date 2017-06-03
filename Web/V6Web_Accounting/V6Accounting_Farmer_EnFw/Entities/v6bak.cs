namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v6bak
    {
        [Key]
        [Column(Order = 0, TypeName = "smalldatetime")]
        public DateTime ngay_bk { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal max_file { get; set; }

        [Key]
        [Column(Order = 2)]
        public string file_name { get; set; }

        [StringLength(128)]
        public string file_zip { get; set; }
    }

}
