namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.ComponentModel.DataAnnotations;
    using global::System.ComponentModel.DataAnnotations.Schema;
    using global::System.Data.Entity.Spatial;

    public partial class ModelDefinition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "xml")]
        [Required]
        public string DefinitionXml { get; set; }

        [Column(TypeName = "xml")]
        public string MappingXml { get; set; }
    }
}
