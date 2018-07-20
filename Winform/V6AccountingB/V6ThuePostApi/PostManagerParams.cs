using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace V6ThuePostManager
{
    public class PostManagerParams
    {
        public DataSet DataSet { get; set; }
        public string Mode { get; set; }
        /// <summary>
        /// 1:Viettel   2:Vnpt  3:Bkav
        /// </summary>
        public string Branch { get; set; }

        public string Dir { get; set; }
        public string FileName { get; set; }
        public string RptFileFull { get; set; }
        public string Fkey_hd { get; set; }
    }
}
