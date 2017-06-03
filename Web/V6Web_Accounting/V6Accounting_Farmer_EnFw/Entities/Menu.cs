namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.ComponentModel.DataAnnotations;
    using global::System.ComponentModel.DataAnnotations.Schema;
    using global::System.Data.Entity.Spatial;

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
