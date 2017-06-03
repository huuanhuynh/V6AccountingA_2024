namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALpb")]
    public partial class ALpb
    {
        [Column(TypeName = "numeric")]
        public decimal nam { get; set; }

        [Required]
        [StringLength(1)]
        public string tag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal stt { get; set; }

        [Key]
        [StringLength(13)]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(32)]
        public string ten_bt { get; set; }

        [Required]
        [StringLength(16)]
        public string tk { get; set; }

        public byte loai_pb { get; set; }

        public byte ps_no_co { get; set; }

        [Required]
        [StringLength(12)]
        public string so_ct { get; set; }

        [Column(TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        [Required]
        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal tien { get; set; }

        [Required]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Required]
        [StringLength(13)]
        public string stt_rec01 { get; set; }

        [Required]
        [StringLength(13)]
        public string stt_rec02 { get; set; }

        [Required]
        [StringLength(13)]
        public string stt_rec03 { get; set; }

        [Required]
        [StringLength(13)]
        public string stt_rec04 { get; set; }

        [Required]
        [StringLength(13)]
        public string stt_rec05 { get; set; }

        [Required]
        [StringLength(13)]
        public string stt_rec06 { get; set; }

        [Required]
        [StringLength(13)]
        public string stt_rec07 { get; set; }

        [Required]
        [StringLength(13)]
        public string stt_rec08 { get; set; }

        [Required]
        [StringLength(13)]
        public string stt_rec09 { get; set; }

        [Required]
        [StringLength(13)]
        public string stt_rec10 { get; set; }

        [Required]
        [StringLength(13)]
        public string stt_rec11 { get; set; }

        [Required]
        [StringLength(13)]
        public string stt_rec12 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

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

        [Required]
        [StringLength(1)]
        public string loai { get; set; }

        [Required]
        [StringLength(40)]
        public string Ten_loai { get; set; }

        [Required]
        [StringLength(8)]
        public string MA_BPHTPH { get; set; }

        [Required]
        [StringLength(1)]
        public string AUTO { get; set; }

        [Required]
        [StringLength(2)]
        public string LOAI_PBCP { get; set; }
    }

}
