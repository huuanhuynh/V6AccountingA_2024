using System.Collections.Generic;
using V6Init;
using V6Tools.V6Convert;

namespace V6SyncLibrary2021
{
    public class Sync2THConfig : Config
    {
        /// <summary>
        /// Thư mục gốc exe
        /// </summary>
        public string __dir;

        public Sync2THConfig(string dir, IDictionary<string, object> data)
            : base(data)
        {
            __dir = dir;
        }

        public Sync2THConfig()
        {
        }

        public int stt { get { return GetInt("stt"); } }
        public string Database { get { return GetString("Database"); } }

        public string options { get { return GetString("options"); } }
        public string mdbWhere { get { return GetString("mdbWhere"); } }
        public string sqlWhere { get { return GetString("sqlWhere"); } }
        public string mdbCode { get { return GetString("mdbCode"); } }
        public string sqlCode { get { return GetString("sqlCode"); } }
        public string server { get { return GetString("server"); } }
        public string user { get { return GetString("user"); } }
        public string pass { get { return GetString("pass"); } }
        public string Type { get { return GetString("Type"); } }


        public string strlistdvcs { get { return GetString("strlistdvcs"); } }
        public string strdvcs { get { return GetString("strdvcs"); } }
        public string strlistkho { get { return GetString("strlistkho"); } }
        public string strlistws_id { get { return GetString("strlistws_id"); } }
        public string strws_id_kho { get { return GetString("strws_id_kho"); } }
        public int date_num { get { return GetInt("date_num"); } }
        public string TableName1 { get { return GetString("TableName1"); } }

        public int HHFrom { get { return GetInt("HHFrom"); } }
        public int HHTo { get { return GetInt("HHTo"); } }
        /// <summary>
        /// Các trường cần đổi dữ liệu.
        /// </summary>
        public string[] fields { get { return ObjectAndString.SplitString(GetString("fields")); } }
        public string NGAY_CT1 { get { return GetString("NGAY_CT1"); } }
        public string NGAY_CT2 { get { return GetString("NGAY_CT2"); } }
    }
}
