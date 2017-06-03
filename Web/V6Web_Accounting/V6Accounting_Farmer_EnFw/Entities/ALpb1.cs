namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ALpb1
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string tk { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string tk_he_so { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_vv { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal heso01 { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal heso02 { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal heso03 { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal heso04 { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal heso05 { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal heso06 { get; set; }

        [Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal heso07 { get; set; }

        [Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal heso08 { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal heso09 { get; set; }

        [Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal heso10 { get; set; }

        [Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal heso11 { get; set; }

        [Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal heso12 { get; set; }

        [Key]
        [Column(Order = 16, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 17)]
        [StringLength(8)]
        public string time0 { get; set; }

        [Key]
        [Column(Order = 18)]
        public byte user_id0 { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }

        [Key]
        [Column(Order = 20)]
        [StringLength(16)]
        public string ma_td { get; set; }

        [Key]
        [Column(Order = 21)]
        [StringLength(8)]
        public string ma_bpht { get; set; }

        [Key]
        [Column(Order = 22)]
        [StringLength(16)]
        public string ma_sp { get; set; }

        [Key]
        [Column(Order = 23)]
        [StringLength(16)]
        public string ma_hd { get; set; }

        [Key]
        [Column(Order = 24)]
        [StringLength(16)]
        public string ma_ku { get; set; }

        [Key]
        [Column(Order = 25)]
        [StringLength(16)]
        public string ma_phi { get; set; }

        [Key]
        [Column(Order = 26)]
        [StringLength(16)]
        public string ma_td2 { get; set; }

        [Key]
        [Column(Order = 27)]
        [StringLength(16)]
        public string ma_td3 { get; set; }
    }

}
