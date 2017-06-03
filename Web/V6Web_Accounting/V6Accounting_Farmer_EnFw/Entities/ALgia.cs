namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALgia")]
    public partial class ALgia
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_kho { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia02 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt02 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia03 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt03 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia04 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt04 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia05 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt05 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia06 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt06 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia07 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt07 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia08 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt08 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia09 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt09 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia10 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt10 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia11 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt11 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia12 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt12 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [StringLength(8)]
        public string time0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id0 { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }

}
