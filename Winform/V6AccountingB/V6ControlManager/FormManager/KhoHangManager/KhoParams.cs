using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace V6ControlManager.FormManager.KhoHangManager
{
    public class KhoParams
    {
        public string MA_KHO;
        public string Program { get; set; }
        public int CellWidth { get; set; }
        public int CellHeight { get; set; }
        public DataTable Data { get; set; }
        public string ItemId { get; set; }
        public string ReportProcedure { get; set; }
        public string ReportFile { get; set; }
        //public string CodeForm { get; set; }
        public bool ViewLable2 { get; set; }
        public bool RunTimer { get; set; }
    }
}
