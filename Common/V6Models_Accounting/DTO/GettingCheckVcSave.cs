using System.ComponentModel.DataAnnotations.Schema;

namespace V6Soft.Models.Accounting.DTO
{
   public class GettingCheckVcSave
    {
       [Column("code")]
       public string Code { get; set; }
       [Column("chkso_ct")]
       public string ChksoCT { get; set; }
       [Column("index")]
       public int Index { get; set; }
    }
}
