namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Alcltg")]
    public partial class Alcltg
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string tag { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal stt { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(32)]
        public string ten_bt { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string tk { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(12)]
        public string so_ct { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(13)]
        public string stt_rec01 { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(13)]
        public string stt_rec02 { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(13)]
        public string stt_rec03 { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(13)]
        public string stt_rec04 { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(13)]
        public string stt_rec05 { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(13)]
        public string stt_rec06 { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(13)]
        public string stt_rec07 { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(13)]
        public string stt_rec08 { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(13)]
        public string stt_rec09 { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(13)]
        public string stt_rec10 { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(13)]
        public string stt_rec11 { get; set; }

        [Key]
        [Column(Order = 17)]
        [StringLength(13)]
        public string stt_rec12 { get; set; }

        [Key]
        [Column(Order = 18, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 19)]
        public byte user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }

        [StringLength(12)]
        public string so_ct01 { get; set; }

        [StringLength(12)]
        public string so_ct02 { get; set; }

        [StringLength(12)]
        public string so_ct03 { get; set; }

        [StringLength(12)]
        public string so_ct04 { get; set; }

        [StringLength(12)]
        public string so_ct05 { get; set; }

        [StringLength(12)]
        public string so_ct06 { get; set; }

        [StringLength(12)]
        public string so_ct07 { get; set; }

        [StringLength(12)]
        public string so_ct08 { get; set; }

        [StringLength(12)]
        public string so_ct09 { get; set; }

        [StringLength(12)]
        public string so_ct10 { get; set; }

        [StringLength(12)]
        public string so_ct11 { get; set; }

        [StringLength(12)]
        public string so_ct12 { get; set; }
    }

}
