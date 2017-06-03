using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V6Soft.Models.Accounting.DTO
{
   public class GetGiaBan
    {
       [Column("gia_ban_nt")]
       public int GIABanNt { get; set; }
       [Column("gia_nt2")]
       public int GIANt2 { get; set; }
    }
}
