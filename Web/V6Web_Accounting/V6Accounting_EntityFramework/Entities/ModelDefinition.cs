namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
