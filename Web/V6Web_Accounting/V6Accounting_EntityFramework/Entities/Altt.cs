namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Altt")]
    public partial class Altt
    {
        [Key]
        [StringLength(10)]
        public string ma_dm { get; set; }

        [StringLength(128)]
        public string m_ma_td1 { get; set; }

        [StringLength(128)]
        public string m_ma_td2 { get; set; }

        [StringLength(128)]
        public string m_ma_td3 { get; set; }

        [StringLength(128)]
        public string m_ngay_td1 { get; set; }

        [StringLength(128)]
        public string m_ngay_td2 { get; set; }

        [StringLength(128)]
        public string m_ngay_td3 { get; set; }

        [StringLength(128)]
        public string m_sl_td1 { get; set; }

        [StringLength(128)]
        public string m_sl_td2 { get; set; }

        [StringLength(128)]
        public string m_sl_td3 { get; set; }

        [StringLength(128)]
        public string m_gc_td1 { get; set; }

        [StringLength(128)]
        public string m_gc_td2 { get; set; }

        [StringLength(128)]
        public string m_gc_td3 { get; set; }
    }
}
