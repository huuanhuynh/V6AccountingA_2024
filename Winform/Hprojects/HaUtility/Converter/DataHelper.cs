using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using HaUtility.MyTypes;

namespace HaUtility.Converter
{
    public static class DataHelper
    {
        public static int SecondsFrom2016
        {
            get
            {
                DateTime dt = new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
                DateTime dtNow = DateTime.Now;
                TimeSpan result = dtNow.Subtract(dt);
                int seconds = Convert.ToInt32(result.TotalSeconds);
                return seconds;
            }
        }

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

        public static string ToXml(this object o)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(o.GetType());
            serializer.Serialize(stringwriter, o);
            return stringwriter.ToString();
        }

        public static string ToJson<T>(this T o)
        {
            return HJson.ConvertToJson(o);
        }
        
        /// <summary>
        /// Hàm chuyển Model về DataDictionary với key UPPER.
        /// Lưu ý: không dùng cho DataRow.
        /// </summary>
        /// <typeparam name="T">Kiểu object đưa vào. Vd ModelBenhNhan</typeparam>
        /// <param name="entity">Đối tượng đưa vào</param>
        /// <returns></returns>
        public static SortedDictionary<string, object> ToDic<T>(this T entity)
        {
            if (entity is DataRow)
            {
                return (entity as DataRow).ToDataDictionary();
            }
            var dic = new SortedDictionary<string, object>();
            foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    //object value = GetFormValue(container, propertyInfo.Name);
                    var NAME = propertyInfo.Name.ToUpper();
                    object o = propertyInfo.GetValue(entity, null);
                    dic[NAME] = o;
                }
            }
            return dic;
        }

        public static T ToModel<T>(this IDictionary<string, object> dataDic) where T : new()
        {
            var t = new T();
            foreach (PropertyInfo propertyInfo in t.GetType().GetProperties())
            {
                if (propertyInfo.CanWrite)
                {
                    //object value = GetFormValue(container, propertyInfo.Name);
                    object o = "";
                    if (dataDic.ContainsKey(propertyInfo.Name.ToUpper()))
                        o = dataDic[propertyInfo.Name.ToUpper()];
                    var value = PrimitiveTypes.ObjectTo(propertyInfo.PropertyType, o);
                    propertyInfo.SetValue(t, value, null);
                }
            }
            return t;
        }

        #region ==== DataTable ====
        /// <summary>
        /// Chuyển DataTable về ListDic
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<SortedDictionary<string, object>> ToListDataDictionary(this DataTable data)
        {
            return (from DataRow row in data.Rows select row.ToDataDictionary()).ToList();
        }

        public static SortedDictionary<string, object>[] ToArrayDataDictionary(this DataTable data)
        {
            var result = new SortedDictionary<string, object>[data.Rows.Count];
            for (int i = 0; i < data.Rows.Count; i++)
            {
                result[i] = data.Rows[i].ToDataDictionary();
            }
            return result;
        }

        public static SortedDictionary<string, object> ToDataDictionary(this DataRow row)
        {
            var DataDic = new SortedDictionary<string, object>();
            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                DataDic.Add(row.Table.Columns[i].ColumnName.ToUpper(), row[i]);
            }
            return DataDic;
        }

        #endregion dataTable
    }
}
