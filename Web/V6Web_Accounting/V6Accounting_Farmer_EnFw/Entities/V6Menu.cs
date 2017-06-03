namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V6Menu
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string module_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string v2id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string jobid { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string itemid { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int stt_box { get; set; }

        [StringLength(2)]
        public string hotkey { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string vbar { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string vbar2 { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int hide_yn { get; set; }

        [StringLength(50)]
        public string page { get; set; }

        [StringLength(10)]
        public string ma_ct { get; set; }

        [StringLength(1)]
        public string loai_ct { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int basic_right { get; set; }
    }

}
