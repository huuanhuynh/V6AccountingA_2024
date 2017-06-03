using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
   public class AlPost 
    {
        [Key]
        public string MaCT { get; set; }
        [Key]
        public string KieuPost { get; set; }

        public string MaPost { get; set; }


        public decimal? Default { get; set; }

        public string TenPost { get; set; }

        public string TenPost2 { get; set; }

        public string TenAct { get; set; }

        public string TenAct2 { get; set; }

        public string STATUS { get; set; }


        public decimal? ARI70 { get; set; }


        public decimal? ARA00 { get; set; }


        public DateTime? Date { get; set; }

        public string Time { get; set; }


        public string MaTd1 { get; set; }

        public string MaTd2 { get; set; }

        public string MaTd3 { get; set; }


        public DateTime? NgayTd1 { get; set; }


        public DateTime? NgayTd2 { get; set; }


        public DateTime? NgayTd3 { get; set; }


        public decimal? SlTd1 { get; set; }


        public decimal? SlTd2 { get; set; }



        public decimal? SlTd3 { get; set; }


        public string GcTd1 { get; set; }



        public string GcTd2 { get; set; }

        public string GcTd3 { get; set; }


        public DateTime? Dupdate { get; set; }

        public string MaPhanHe { get; set; }

        public string Itemid { get; set; }


        public decimal? STTIn { get; set; }

        public string Search { get; set; }
       //public string ma_ct { get; set; }
       //public string ma_dm { get; set; }
       //public string caption { get; set; }
       //public string caption2 { get; set; }
       //public int loai { get; set; }
       //public string ftype { get; set; }
       //public int forder { get; set; }
       //public int width { get; set; }
       //public int carry { get; set; }
       //public byte fstatus { get; set; }
       //public byte checkvvar { get; set; }
       //public byte notempty { get; set; }
       //public int fdecimal { get; set; }
       //public byte visible { get; set; }
       //public string fvvar { get; set; }
       //public string fcolumn { get; set; }
       //public string lstatus { get; set; }
       //public byte fstatus2 { get; set; }
    }
}
