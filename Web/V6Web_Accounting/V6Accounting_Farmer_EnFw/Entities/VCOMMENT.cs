namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VCOMMENT")]
    public partial class VCOMMENT
    {
        [StringLength(13)]
        public string stt_rec { get; set; }

        [StringLength(3)]
        public string ma_ct { get; set; }

        [StringLength(5)]
        public string KHOA { get; set; }

        [StringLength(2)]
        public string Level { get; set; }

        [StringLength(64)]
        public string Level_name { get; set; }

        [StringLength(64)]
        public string ten_user { get; set; }

        [StringLength(1)]
        public string Lock { get; set; }

        [StringLength(200)]
        public string comment1 { get; set; }

        [StringLength(200)]
        public string comment2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [StringLength(8)]
        public string time0 { get; set; }

        public byte? user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }

        [Key]
        [Column(Order = 0, TypeName = "smalldatetime")]
        public DateTime NGAY_CT { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(12)]
        public string SO_CT { get; set; }

        [Key]
        [Column(Order = 2)]
        public string DIEN_GIAI { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string MA_DVCS { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(8)]
        public string MA_KHO { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(1)]
        public string STATUS { get; set; }
    }

}
