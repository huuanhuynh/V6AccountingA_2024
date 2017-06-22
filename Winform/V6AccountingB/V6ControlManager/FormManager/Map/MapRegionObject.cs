using System.Data;
using System.Drawing;

namespace V6ControlManager.FormManager.Map
{
    public class MapRegionObject
    {
        public string ID = "";
        public Point[] Polygon = null;
        public string ColorType = "00";
        public string Path = "";
        public DataRow RowData = null;
    }
}
