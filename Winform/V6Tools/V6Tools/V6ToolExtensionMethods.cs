using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using V6Tools.V6Convert;

namespace V6Tools
{
    public static class V6ToolExtensionMethods
    {
        /// <summary>
        /// Xác định xem một danh sách chuỗi có chuỗi nào bắt đầu bằng một chuỗi được chỉ định hay không.
        /// </summary>
        public static bool ContainsEndsWith(this IList<string> source, string value)
        {
            foreach (string s in source)
            {
                if (s.EndsWith(value)) return true;
            }
            return false;
        }
        
        public static bool ContainsStartsWith(this IList<string> source, string value)
        {
            foreach (string s in source)
            {
                if (s.StartsWith(value)) return true;
            }
            return false;
        }

        /// <summary>
        /// Cắt chuỗi lấy phần bên trái. Nếu không đủ trả về đúng chuỗi đó.
        /// </summary>
        /// <param name="str">Chuỗi nguồn</param>
        /// <param name="length">Độ dài cần lấy</param>
        /// <returns>Phần chuỗi bên trái với độ dài cần lấy, hoặc ngắn hơn nếu nguồn không đủ dài.</returns>
        public static string Left(this string str, int length)
        {
            return length >= str.Length ? str : str.Substring(0, length);
        }
        /// <summary>
        /// Lấy phần chuỗi ở giữa. Nếu không có trả về rỗng.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Mid(this string str, int start, int length)
        {
            return length + start >= str.Length ? str.Substring(start) : str.Substring(start, length);
        }
        /// <summary>
        /// Cắt chuỗi lấy phần bên phải. Nếu không đủ trả về đúng chuỗi đó.
        /// </summary>
        /// <param name="str">Chuỗi nguồn</param>
        /// <param name="length">Độ dài cần lấy</param>
        /// <returns>Phần chuỗi bên phải với độ dài cần lấy, hoặc ngắn hơn nếu nguồn không đủ dài.</returns>
        public static string Right(this string str, int length)
        {
            return length >= str.Length ? str : str.Substring(str.Length - length, length);
        }

        /// <summary>
        /// Thêm đoạn endText vào string, nếu có rồi thì thôi.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="endText"></param>
        /// <returns></returns>
        public static string WithEnd(this string s, string endText)
        {
            if (!s.EndsWith(endText))
            {
                s += " " + endText;
            }
            return s;
        }

        /// <summary>
        /// Bỏ đoạn endText khỏi phần cuối của string, bỏ luôn khoảng trắng thừa.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="endText"></param>
        /// <returns></returns>
        public static string RemoveEnd(this string s, string endText)
        {
            if (s.EndsWith(endText))
            {
                s = s.Substring(0, s.Length - endText.Length);
                s = s.TrimEnd();
            }
            return s;
        }
        
        /// <summary>
        /// Hàm mở rộng. Add source to target
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="source"></param>
        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            if (target == null || source == null)
                return;

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
            if (target == null || source == null)
                return;

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
            if (target == null || source == null)
                return;

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
            if (target == null || source == null)
                return;

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

        /// <summary>
        /// Hàm tạo ra một Object mới là Dictionary hoặc SortedDictionary với các Key đã được UPPER.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToUpperKeys(this IDictionary<string, object> target)
        {
            IDictionary<string, object> result;
            if (target is SortedDictionary<string, object>) result = new SortedDictionary<string, object>();
            else result = new Dictionary<string, object>();

            foreach (var element in target)
            {
                result[element.Key.ToUpper()] = element.Value;
            }
            return result;
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
                        if (data[key] != null && data[key] != DBNull.Value)// && data[key].GetType() == typeof(byte[]))
                        {
                            //table.Columns.Add(key, typeof(byte[]));
                            table.Columns.Add(key, data[key].GetType());
                        }
                        else
                        {
                            table.Columns.Add(key);
                        }
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
        /// Thêm một dòng dữ liệu từ 1 dòng dữ liệu của bảng khác.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="data"></param>
        /// <param name="autoAddColumns"></param>
        /// <returns></returns>
        public static DataRow AddRow(this DataTable table, DataRow data, bool autoAddColumns = false)
        {
            DataTable sourceTable = data.Table;
            if (autoAddColumns)
            {
                foreach (DataColumn column in sourceTable.Columns)
                {
                    if (!table.Columns.Contains(column.ColumnName))
                    {
                        table.Columns.Add(column.ColumnName, column.DataType);
                    }
                }
            }

            var newRow = table.NewRow();
            foreach (DataColumn column in table.Columns)
            {
                var KEY = column.ColumnName.ToUpper();
                object value = ObjectAndString.ObjectTo(column.DataType,
                    sourceTable.Columns.Contains(KEY) ? data[KEY] : "") ?? DBNull.Value;
                if (KEY == "UID" || column.DataType == typeof(Guid))
                {
                    if (sourceTable.Columns.Contains("UID")) value = data[KEY].ToString();
                    else value = DBNull.Value;
                }
                
                newRow[KEY] = value;
            }
            table.Rows.Add(newRow);
            return newRow;
        }
        /// <summary>
        /// Thêm nhiều dòng dữ liệu từ bảng khác.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="sourceTable"></param>
        /// <param name="autoAddColumns"></param>
        /// <returns></returns>
        public static void AddRowByTable(this DataTable table, DataTable sourceTable, bool autoAddColumns = false)
        {
            if (autoAddColumns)
            {
                foreach (DataColumn column in sourceTable.Columns)
                {
                    if (!table.Columns.Contains(column.ColumnName))
                    {
                        table.Columns.Add(column.ColumnName, column.DataType);
                    }
                }
            }

            foreach (DataRow row in sourceTable.Rows)
            {
                table.AddRow(row, autoAddColumns);
            }
        }

        /// <summary>
        /// Biến thành Dic, key trùng sẽ lấy dòng sau cùng.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="keyField"></param>
        /// <param name="dataField"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDataDictionary(this DataTable data, string keyField, string dataField)
        {
            return Data_Table.ToDataDictionary(data, keyField, dataField);
        }
        public static Dictionary<string, string> ToStringDataDictionary(this DataTable data, string keyField, string dataField)
        {
            return Data_Table.ToStringDataDictionary(data, keyField, dataField);
        }

        /// <summary>
        /// Biến thành Dic, key trùng sẽ lấy dòng sau cùng.
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
        /// <para>Chuyển một dòng thành một Model(Class) với các trường tương ứng.</para>
        /// <para>(Các trường không khớp sẽ bị bỏ qua).</para>
        /// </summary>
        /// <typeparam name="T">Kiểu Model</typeparam>
        /// <param name="row">Dòng dữ liệu</param>
        /// <returns>Model (T)</returns>
        public static T ToModel<T>(this DataRow row) where T : new()
        {
            var p_array = typeof(T).GetProperties();
            var a_array = typeof(T).GetFields();
                        
            var t = new T();
            foreach (PropertyInfo propertyInfo in p_array)
            {
                string field = propertyInfo.Name;
                if (propertyInfo.CanWrite)
                {
                    object o = "";
                    if (row.Table.Columns.Contains(field)) o = row[field];

                    var value = ObjectAndString.ObjectTo(propertyInfo.PropertyType, o);
                    propertyInfo.SetValue(t, value, null);
                }
            }

            foreach (FieldInfo fieldInfo in a_array)
            {
                try
                {
                    string FIELD = fieldInfo.Name.ToUpper();
                    if (fieldInfo.IsPublic)
                    {
                        object o = "";
                        if (row.Table.Columns.Contains(FIELD)) o = row[FIELD];

                        var value = ObjectAndString.ObjectTo(fieldInfo.FieldType, o);
                        fieldInfo.SetValue(t, value);
                    }
                }
                catch
                {

                }
            }

            return t;
        }

        /// <summary>
        /// Như ToModel.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static T ToClass<T>(this IDictionary<string, object> dic) where T : new()
        {
            return dic.ToModel<T>();
        }

        /// <summary>
        /// Như ToModel.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static T ToClass<T>(this IDictionary<string, string> dic) where T : new()
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            foreach (KeyValuePair<string, string> item in dic)
            {
                d[item.Key] = item.Value;
            }
            return d.ToModel<T>();
        }

        /// <summary>
        /// <para>Chuyển một dòng thành một Model(Class) với các trường tương ứng.</para>
        /// <para>(Các trường không khớp sẽ bị bỏ qua).</para>
        /// </summary>
        /// <typeparam name="T">Kiểu Model</typeparam>
        /// <param name="dic">Dòng dữ liệu</param>
        /// <returns>Model (T)</returns>
        public static T ToModel<T>(this IDictionary<string, object> dic) where T : new()
        {
            dic = dic.ToUpperKeys();
            var p_array = typeof(T).GetProperties();
            var a_array = typeof(T).GetFields();

            var t = new T();
            foreach (PropertyInfo propertyInfo in p_array)
            {
                string FIELD = propertyInfo.Name.ToUpper();
                if (propertyInfo.CanWrite)
                {
                    object o = "";
                    if (dic.ContainsKey(FIELD)) o = dic[FIELD];

                    var value = ObjectAndString.ObjectTo(propertyInfo.PropertyType, o);
                    propertyInfo.SetValue(t, value, null);
                }
            }

            foreach (FieldInfo fieldInfo in a_array)
            {
                try
                {
                    string FIELD = fieldInfo.Name.ToUpper();
                    if (fieldInfo.IsPublic)
                    {
                        object o = "";
                        if (dic.ContainsKey(FIELD)) o = dic[FIELD];

                        var value = ObjectAndString.ObjectTo(fieldInfo.FieldType, o);
                        fieldInfo.SetValue(t, value);
                    }
                }
                catch 
                {
                    
                }
            }

            return t;
        }

        /// <summary>
        /// <para>Chuyển một bảng nhiều dòng thành danh sách các Model với các trường tương ứng.</para>
        /// <para>(Các trường không khớp sẽ bị bỏ qua).</para>
        /// </summary>
        /// <typeparam name="T">Kiểu Model</typeparam>
        /// <param name="data">Bảng dữ liệu</param>
        /// <returns>Danh sách các Model (T)</returns>
        public static List<T> ToListModel<T>(this DataTable data) where T : new()
        {
            List<T> result = new List<T>();
            var p_array = typeof(T).GetProperties();
            foreach (DataRow row in data.Rows)
            {
                var t = new T();
                foreach (PropertyInfo propertyInfo in p_array)
                {
                    string field = propertyInfo.Name;
                    if (propertyInfo.CanWrite)
                    {
                        object o = "";
                        if (data.Columns.Contains(field)) o = row[field];

                        var value = ObjectAndString.ObjectTo(propertyInfo.PropertyType, o);
                        if (value != DBNull.Value || propertyInfo.PropertyType != typeof(DateTime))
                        {
                            propertyInfo.SetValue(t, value, null);
                        }
                        else
                        {

                        }
                    }
                }
                result.Add(t);
            }
            return result;
        }
        
        /// <summary>
        /// Chuyển DataTable về ListDic
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<IDictionary<string, object>> ToListDataDictionary(this DataTable data)
        {
            return (from DataRow row in data.Rows select row.ToDataDictionary()).ToList();
        }

        public static IDictionary<string, DataRow> ToRowDictionary(this DataTable data, string keyField)
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

        public static List<IDictionary<string, object>> ToListDataDictionary(this List<DataRow> data)
        {
            return (from DataRow row in data select row.ToDataDictionary()).ToList();
        }

        /// <summary>
        /// Chuyển DataTable về ListDic
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sttRec">Gán thêm vào dữ liệu. (không phải filter)</param>
        /// <returns></returns>
        public static List<IDictionary<string, object>> ToListDataDictionary(this DataTable data, string sttRec)
        {
            return (from DataRow row in data.Rows select row.ToDataDictionary(sttRec)).ToList();
        }

        public static List<IDictionary<string, object>> ToListDataDictionaryLower(this DataTable data)
        {
            return (from DataRow row in data.Rows select row.ToDataDictionaryLower()).ToList();
        }

        /// <summary>
        /// Chuyển DataTable thành danh sách Dic
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sttRec">Gán thêm vào dữ liệu. (không phải filter)</param>
        /// <returns></returns>
        public static List<IDictionary<string, object>> ToListDataDictionary(this List<DataRow> data, string sttRec)
        {
            return (from DataRow row in data select row.ToDataDictionary(sttRec)).ToList();
        }

        public static List<IDictionary<string, object>> ToListDataDictionaryLower(this List<DataRow> data)
        {
            return (from DataRow row in data select row.ToDataDictionaryLower()).ToList();
        }

        public static IDictionary<string, object> DataRowToDataDictionary(DataRow row)
        {
            var DataDic = new SortedDictionary<string, object>();
            if (row == null) return DataDic;
            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                DataDic.Add(row.Table.Columns[i].ColumnName.ToUpper(), row[i]);
            }
            return DataDic;
        }

        public static IDictionary<string, object> DataRowToDataDictionaryLower(DataRow row)
        {
            var DataDic = new SortedDictionary<string, object>();
            if (row == null) return DataDic;
            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                DataDic.Add(row.Table.Columns[i].ColumnName.ToLower(), row[i]);
            }
            return DataDic;
        }

        public static IDictionary<string, object> DataRowToDataDictionaryUpper(DataRow row)
        {
            var DataDic = new SortedDictionary<string, object>();
            if (row == null) return DataDic;
            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                DataDic.Add(row.Table.Columns[i].ColumnName.ToUpper(), row[i]);
            }
            return DataDic;
        }

        public static IDictionary<string, object> DataGridViewRowToDataDictionary(DataGridViewRow row)
        {
            if (row == null) return null;
            var DataDic = new SortedDictionary<string, object>();
            for (int i = 0; i < row.DataGridView.Columns.Count; i++)
            {
                DataDic.Add(row.DataGridView.Columns[i].DataPropertyName.ToUpper(), row.Cells[i].Value);
            }
            return DataDic;
        }
        public static IDictionary<string, object> ToDataDictionary(this DataRow row)
        {
            return DataRowToDataDictionary(row);
        }
        public static IDictionary<string, object> ToDataDictionaryLower(this DataRow row)
        {
            return DataRowToDataDictionaryLower(row);
        }

        public static IDictionary<string, object> ToDataDictionaryUpper(this DataRow row)
        {
            return DataRowToDataDictionaryUpper(row);
        }

        public static IDictionary<string, object> ToDataDictionary(this DataGridViewRow row)
        {
            return DataGridViewRowToDataDictionary(row);
        }

        /// <summary>
        /// Chuyển DataTable thành danh sách Dic
        /// </summary>
        /// <param name="row"></param>
        /// <param name="sttRec">Gán thêm vào dữ liệu. (không phải filter)</param>
        /// <returns></returns>
        public static IDictionary<string, object> DataRowToDataDictionary(DataRow row, string sttRec)
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
        /// <summary>
        /// Chuyển DataTable thành danh sách Dic
        /// </summary>
        /// <param name="row"></param>
        /// <param name="sttRec">Gán thêm vào dữ liệu. (không phải filter)</param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDataDictionary(this DataRow row, string sttRec)
        {
            return DataRowToDataDictionary(row, sttRec);
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
