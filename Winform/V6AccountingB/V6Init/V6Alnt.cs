using System.Collections.Generic;
using System.Data;
using V6SqlConnect;
using V6Structs;

namespace V6Init
{
    public static class V6Alnt
    {

        /// <summary>
        /// Lấy lên tất cả _value
        /// </summary>
        private static void LoadValue()
        {
            try
            {
                V6SelectResult selectResult = SqlConnect.Select("Alnt", "*", "");

                if (selectResult.Data != null)
                {
                    _value = new SortedDictionary<string, DataRow>();

                    foreach (DataRow row in selectResult.Data.Rows)
                    {
                        var mant = row["Ma_nt"].ToString().Trim();
                        _value.Add(mant, row);
                    }
                }
            }
            catch
            {
                _value = null;
            }

        }
        private static SortedDictionary<string, DataRow> _value;
        
        /// <summary>
        /// vd: V6AlntValue["VND"]["Field"]
        /// </summary>
        public static SortedDictionary<string, DataRow> V6AlntValue
        {
            get
            {
                if(_value == null || _value.Count==0)
                    LoadValue();
                return _value;
            }
        }

        public static string begin1(string mant)
        {
            return (V6AlntValue[mant]["begin1"] ?? "").ToString().Trim();
        }
        public static string begin2(string mant)
        {
            return (V6AlntValue[mant]["begin2"] ?? "").ToString().Trim();
        }

        public static string end1(string mant)
        {
            return (V6AlntValue[mant]["end1"] ?? "").ToString().Trim();
        }
        public static string end2(string mant)
        {
            return (V6AlntValue[mant]["end2"] ?? "").ToString().Trim();
        }
        public static string only1(string mant)
        {
            return (V6AlntValue[mant]["only1"] ?? "").ToString().Trim();
        }
        public static string only2(string mant)
        {
            return (V6AlntValue[mant]["only2"] ?? "").ToString().Trim();
        }
        public static string point1(string mant)
        {
            return (V6AlntValue[mant]["point1"] ?? "").ToString().Trim();
        }
        public static string point2(string mant)
        {
            return (V6AlntValue[mant]["point2"] ?? "").ToString().Trim();
        }
        public static string endpoint1(string mant)
        {
            return (V6AlntValue[mant]["endpoint1"] ?? "").ToString().Trim();
        }
        public static string endpoint2(string mant)
        {
            return (V6AlntValue[mant]["endpoint2"] ?? "").ToString().Trim();
        }
        //(V6Alnt.V6AlntValue[MA_NT]["end1"]??"").ToString().Trim(),
        //                (V6Alnt.V6AlntValue[MA_NT][""]??"").ToString().Trim(),
        //                (V6Alnt.V6AlntValue[MA_NT][""]??"").ToString().Trim(),
        //                (V6Alnt.V6AlntValue[MA_NT][""]??"").ToString().Trim()));

        //public string this[int index]
        //{
        //    get { return _storrage[index]; }
        //    set { _storrage[index] = value; }
        //}
    }

}
