namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALkc")]
    public partial class ALkc
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
        [StringLength(64)]
        public string ten_bt { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string tk_no { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string tk_co { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal loai_kc { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(12)]
        public string so_ct { get; set; }

        [Key]
        [Column(Order = 8)]
        public byte kc_vv_yn { get; set; }

        [Key]
        [Column(Order = 9)]
        public byte kc_td_yn { get; set; }

        [Key]
        [Column(Order = 10)]
        public byte kc_bpht_yn { get; set; }

        [Key]
        [Column(Order = 11)]
        public byte kc_sp_yn { get; set; }

        [Key]
        [Column(Order = 12)]
        public byte kc_hd_yn { get; set; }

        [Key]
        [Column(Order = 13)]
        public byte kc_ku_yn { get; set; }

        [Key]
        [Column(Order = 14)]
        public byte kc_phi_yn { get; set; }

        [Key]
        [Column(Order = 15)]
        public byte kc_td2_yn { get; set; }

        [Key]
        [Column(Order = 16)]
        public byte kc_td3_yn { get; set; }

        [Key]
        [Column(Order = 17)]
        public string group_kc { get; set; }

        [Key]
        [Column(Order = 18)]
        [StringLength(13)]
        public string stt_rec01 { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(13)]
        public string stt_rec02 { get; set; }

        [Key]
        [Column(Order = 20)]
        [StringLength(13)]
        public string stt_rec03 { get; set; }

        [Key]
        [Column(Order = 21)]
        [StringLength(13)]
        public string stt_rec04 { get; set; }

        [Key]
        [Column(Order = 22)]
        [StringLength(13)]
        public string stt_rec05 { get; set; }

        [Key]
        [Column(Order = 23)]
        [StringLength(13)]
        public string stt_rec06 { get; set; }

        [Key]
        [Column(Order = 24)]
        [StringLength(13)]
        public string stt_rec07 { get; set; }

        [Key]
        [Column(Order = 25)]
        [StringLength(13)]
        public string stt_rec08 { get; set; }

        [Key]
        [Column(Order = 26)]
        [StringLength(13)]
        public string stt_rec09 { get; set; }

        [Key]
        [Column(Order = 27)]
        [StringLength(13)]
        public string stt_rec10 { get; set; }

        [Key]
        [Column(Order = 28)]
        [StringLength(13)]
        public string stt_rec11 { get; set; }

        [Key]
        [Column(Order = 29)]
        [StringLength(13)]
        public string stt_rec12 { get; set; }

        [Key]
        [Column(Order = 30, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 31)]
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

        [Key]
        [Column(Order = 32)]
        [StringLength(1)]
        public string AUTO { get; set; }
    }

}
