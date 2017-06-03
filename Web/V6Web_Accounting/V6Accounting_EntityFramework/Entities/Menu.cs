namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public Menu()
        {
            Menu1 = new HashSet<Menu>();
        }

        [Key]
        public int OID { get; set; }

        [Required]
        [StringLength(100)]
        public string Label { get; set; }

        [StringLength(30)]
        public string Icon { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(200)]
        public string Route { get; set; }

        public short Position { get; set; }

        public int? ParentOID { get; set; }

        [StringLength(200)]
        public string Metadata { get; set; }

        public virtual ICollection<Menu> Menu1 { get; set; }

        public virtual Menu Menu2 { get; set; }
    }
}
