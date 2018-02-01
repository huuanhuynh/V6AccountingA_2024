﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using V6Tools.V6Convert;

namespace V6Tools
{
    public static class ExtensionMethod
    {
        /// <summary>
        /// Cắt chuỗi lấy phần bên trái.
        /// </summary>
        /// <param name="str">Chuỗi nguồn</param>
        /// <param name="length">Độ dài cần lấy</param>
        /// <returns>Phần chuỗi bên trái với độ dài cần lấy, hoặc ngắn hơn nếu nguồn không đủ dài.</returns>
        public static string Left(this string str, int length)
        {
            return length >= str.Length ? str : str.Substring(0, length);
        }
        /// <summary>
        /// Cắt chuỗi lấy phần bên phải
        /// </summary>
        /// <param name="str">Chuỗi nguồn</param>
        /// <param name="length">Độ dài cần lấy</param>
        /// <returns>Phần chuỗi bên phải với độ dài cần lấy, hoặc ngắn hơn nếu nguồn không đủ dài.</returns>
        public static string Right(this string str, int length)
        {
            return length >= str.Length ? str : str.Substring(str.Length - length, length);
        }
        
        /// <summary>
        /// Hàm mở rộng. Add source to target
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="source"></param>
        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (source == null)
                throw new ArgumentNullException("source");
            foreach (var element in source)
            {
                try
                {
                    target.Add(element);
                }
                catch
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// Add source to target
        /// </summary>
        /// <param name="target">Đích</param>
        /// <param name="source">Nguồn</param>
        /// <param name="overwrite">Lấy dữ liệu mới nếu trùng khóa</param>
        public static void AddRange(this IDictionary<string, object> target, IDictionary<string, object> source, bool overwrite = false)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (source == null)
                throw new ArgumentNullException("source");
            if (overwrite)
            {
                foreach (KeyValuePair<string, object> item in source)
                {
                    target[item.Key] = item.Value;
                }
            }
            else foreach (var element in source)
            {
                if (!target.ContainsKey(element.Key))
                {
                    target.Add(element.Key, element.Value);
                }
            }
        }

        /// <summary>
        /// Add source to target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <param name="overwrite"></param>
        public static void AddRange(this IDictionary<string, object> target, IDictionary<string, string> source, bool overwrite = false)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (source == null)
                throw new ArgumentNullException("source");
            foreach (var element in source)
            {
                if (target.ContainsKey(element.Key))
                {
                    if (overwrite)
                    {
                        target[element.Key] = element.Value;
                    }
                }
                else
                {
                    target.Add(element.Key, element.Value);
                }
            }
        }

        /// <summary>
        /// Add source to target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <param name="overwrite"></param>
        public static void AddRange(this IDictionary<string, string> target, IDictionary<string, object> source, bool overwrite = false)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (source == null)
                throw new ArgumentNullException("source");
            foreach (var element in source)
            {
                if (target.ContainsKey(element.Key))
                {
                    if (overwrite)
                    {
                        target[element.Key] = element.Value.ToString();
                    }
                }
                else
                {
                    target.Add(element.Key, ObjectAndString.ObjectToString(element.Value));
                }
            }
        }

        #region ==== DataTable ====

        public static DataRow AddRow(this DataTable table, IDictionary<string, object> data, bool autoAddColumns = false)
        {
            if (autoAddColumns)
            {
                foreach (string key in data.Keys)
                {
                    if (!table.Columns.Contains(key))
                    {
                        table.Columns.Add(key);
                    }
                }
            }

            var newRow = table.NewRow();
            foreach (DataColumn column in table.Columns)
            {
                var KEY = column.ColumnName.ToUpper();
                object value = ObjectAndString.ObjectTo(column.DataType,
                    data.ContainsKey(KEY) ? data[KEY] : "") ?? DBNull.Value;
                newRow[KEY] = value;
            }
            table.Rows.Add(newRow);
            return newRow;
        }

        /// <summary>
        /// Biếnt thành Dic, key trùng sẽ lấy dòng sau cùng.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="keyField"></param>
        /// <param name="dataField"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDataDictionary(this DataTable data, string keyField, string dataField)
        {
            if (!data.Columns.Contains(keyField))
                throw new Exception(string.Format("No keyField [{0}] column.", keyField));
            if (!data.Columns.Contains(dataField))
                throw new Exception(string.Format("No dataField [{0}] column.", dataField));
            var DataDic = new Dictionary<string, object>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                var row = data.Rows[i];
                DataDic[row[keyField].ToString().Trim().ToUpper()] = row[dataField];
            }
            return DataDic;
        }

        /// <summary>
        /// Biếnt thành Dic, key trùng sẽ lấy dòng sau cùng.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="keyField"></param>
        /// <param name="dataField"></param>
        /// <returns></returns>
        public static SortedDictionary<string, object> ToDataSortedDictionary(this DataTable data, string keyField, string dataField)
        {
            if (!data.Columns.Contains(keyField))
                throw new Exception(string.Format("No keyField [{0}] column.", keyField));
            if (!data.Columns.Contains(dataField))
                throw new Exception(string.Format("No dataField [{0}] column.", dataField));
            var DataDic = new SortedDictionary<string, object>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                var row = data.Rows[i];
                DataDic[row[keyField].ToString().Trim().ToUpper()] = row[dataField];
            }
            return DataDic;
        }

        /// <summary>
        /// Chuyển DataTable về ListDic
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<SortedDictionary<string, object>> ToListDataDictionary(this DataTable data)
        {
            return (from DataRow row in data.Rows select row.ToDataDictionary()).ToList();
        }

        public static SortedDictionary<string, DataRow> ToRowDictionary(this DataTable data, string keyField)
        {
            if (!data.Columns.Contains(keyField))
                throw new Exception(string.Format("No keyField [{0}] column.", keyField));
            var DataDic = new SortedDictionary<string, DataRow>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                var row = data.Rows[i];
                DataDic[row[keyField].ToString().Trim().ToUpper()] = row;
            }
            return DataDic;
        }

        /// <summary>
        /// Lấy ra bảng dữ liệu sau khi lọc
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filterString"></param>
        /// <returns></returns>
        public static DataTable Filter(this DataTable data, string filterString)
        {
            DataView dv = new DataView(data);
            dv.RowFilter = filterString;

            DataTable newData = dv.ToTable();
            return newData;
        }

        public static List<SortedDictionary<string, object>> ToListDataDictionary(this List<DataRow> data)
        {
            return (from DataRow row in data select row.ToDataDictionary()).ToList();
        }

        /// <summary>
        /// Chuyển DataTable về ListDic
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sttRec"></param>
        /// <returns></returns>
        public static List<SortedDictionary<string, object>> ToListDataDictionary(this DataTable data, string sttRec)
        {
            return (from DataRow row in data.Rows select row.ToDataDictionary(sttRec)).ToList();
        }

        public static List<SortedDictionary<string, object>> ToListDataDictionary(this List<DataRow> data, string sttRec)
        {
            return (from DataRow row in data select row.ToDataDictionary(sttRec)).ToList();
        }

        public static SortedDictionary<string, object> ToDataDictionary(this DataRow row)
        {
            var DataDic = new SortedDictionary<string, object>();
            if (row == null) return DataDic;
            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                DataDic.Add(row.Table.Columns[i].ColumnName.ToUpper(), row[i]);
            }
            return DataDic;
        }

        public static SortedDictionary<string, object> ToDataDictionary(this DataRow row, string sttRec)
        {
            var DataDic = new SortedDictionary<string, object>();
            if (row == null) return DataDic;
            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                DataDic.Add(row.Table.Columns[i].ColumnName.ToUpper(), row[i]);
            }
            DataDic["STT_REC"] = sttRec;
            return DataDic;
        }
        #endregion dataTable

        /// <summary>
        /// So sánh 2 đối tượng theo các thuộc tính và giá trị.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool ReflectiveEquals(this object first, object second)
        {
            if (first == null && second == null)
            {
                return true;
            }
            if (first == null || second == null)
            {
                return false;
            }
            Type firstType = first.GetType();
            if (second.GetType() != firstType)
            {
                return false; // Or throw an exception
            }
            // This will only use public properties. Is that enough?
            foreach (PropertyInfo propertyInfo in firstType.GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    object firstValue = propertyInfo.GetValue(first, null);
                    object secondValue = propertyInfo.GetValue(second, null);
                    if (!Equals(firstValue, secondValue))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}