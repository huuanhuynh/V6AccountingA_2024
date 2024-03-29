﻿using System;
using System.Collections.Generic;
using System.Data;
using V6SqlConnect;
using V6Structs;

namespace V6Init
{
    public static class V6Soft
    {

        /// <summary>
        /// Lấy lên tất cả _value
        /// </summary>
        private static void LoadValue()
        {
            V6SelectResult selectResult = SqlConnect.Select("V6Soft", "name,type,val,attribute");

            if (selectResult.Data != null)
            {
                _value = new SortedDictionary<string, string>();
                _type = new SortedDictionary<string, string>();

                foreach (DataRow row in selectResult.Data.Rows)
                {
                    var att = Convert.ToInt16(row["attribute"]) == 1;
                    var value = row["val"].ToString().Trim();
                    _value[row["name"].ToString().Trim()]= att?V6Tools.UtilityHelper.DeCrypt(value):value;
                    _type[row["name"].ToString().Trim()]= row["type"].ToString().Trim();
                }
            }

        }
        private static SortedDictionary<string, string> _value;
        private static SortedDictionary<string, string> _type;
        /// <summary>
        /// [UPPER_FIELD]
        /// </summary>
        public static SortedDictionary<string, string> V6SoftValue
        {
            get
            {
                if(_value == null || _value.Count==0)
                    LoadValue();
                return _value;
            }
        }
        public static SortedDictionary<string, string> V6SoftType
        {
            get
            {
                if (_type == null || _type.Count == 0)
                    LoadValue();
                return _type;
            }
        }

        public static string TEST_PROPERTY
        {
            get
            {
                return V6SoftValue == null ? "PROPERTY" : V6SoftValue["PROPERTY"];
            }
        }
        
    }

}
