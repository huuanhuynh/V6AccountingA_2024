using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    public class GetCheckVcEntity
    {
        [Column("code")]
        [StringLength(4)]
        public string Code { get; set; }
		
        [Column("chkso_ct")]
        [StringLength(1)]
        public string ChkSoCt { get; set; }
		
        [Column("index")]
        public int Index { get; set; }
    }
}
