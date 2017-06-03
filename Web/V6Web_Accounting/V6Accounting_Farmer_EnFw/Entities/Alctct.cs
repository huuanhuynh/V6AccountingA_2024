namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Alctct")]
    public partial class Alctct
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string Module_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(12)]
        public string ma_phan_he { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(3)]
        public string ma_ct_me { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        public byte? user_id_ct { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal so_ct { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal m_trung_so { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(1)]
        public string chuan { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(1)]
        public string loai { get; set; }
    }

}
