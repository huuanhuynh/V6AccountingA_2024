namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALpost")]
    public partial class ALpost
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_post { get; set; }

        [Column("default", TypeName = "numeric")]
        public decimal? _default { get; set; }

        [StringLength(48)]
        public string ten_post { get; set; }

        [StringLength(48)]
        public string ten_post2 { get; set; }

        [StringLength(48)]
        public string ten_act { get; set; }

        [StringLength(48)]
        public string ten_act2 { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ARI70 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ARA00 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date { get; set; }

        [StringLength(8)]
        public string time { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [StringLength(8)]
        public string time0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id0 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? dupdate { get; set; }

        [StringLength(8)]
        public string ma_phan_he { get; set; }

        [StringLength(12)]
        public string Itemid { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? stt_in { get; set; }

        [StringLength(900)]
        public string search { get; set; }
    }

}
