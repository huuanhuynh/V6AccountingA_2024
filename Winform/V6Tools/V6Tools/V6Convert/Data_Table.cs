using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using V6Tools.V6Export;

namespace V6Tools.V6Convert
{
    public class Data_Table
    {
        public static DataTable ToUnicode(DataTable data)
        {
            return ChuyenMaTiengViet(data, "auto", "U");
        }
        public static DataTable ToUnicode(DataTable data, string from)
        {
            return ChuyenMaTiengViet(data, from, "U");
        }

        public static DataTable FromTCVNtoUnicode(DataTable data)
        {
            try
            {
                var t = data.Copy();
                //DataTable result = data.Clone();
                int columnsCount = t.Columns.Count;
                foreach (DataColumn column in t.Columns)
                {
                    column.ColumnName = V6Tools.ChuyenMaTiengViet.TCVNtoUNICODE(column.ColumnName);
                }
                foreach (DataRow row in t.Rows)
                {
                    for (int i = 0; i < columnsCount; i++)
                    {
                        if (row[i] is string)
                        {
                            row[i] = V6Tools.ChuyenMaTiengViet.TCVNtoUNICODE((string)row[i]);
                        }
                    }
                }
                return t;
            }
            catch (Exception ex)
            {
                throw new ConvertException(ex.Message);
            }
        }

        public static DataTable ChuyenMaTiengViet(DataTable data, string from, string to)
        {
            try
            {
                DataTable data_ = data.Copy();//.Clone();
                int columnsCount = data_.Columns.Count;
                foreach (DataColumn column in data_.Columns)
                {
                    column.ColumnName = V6Tools.ChuyenMaTiengViet.VIETNAM_CONVERT(column.ColumnName, from, to);
                }
                foreach (DataRow row in data_.Rows)
                {
                    for (int i = 0; i < columnsCount; i++)
                    {
                        if(row[i] is string)
                        {
                            row[i] = V6Tools.ChuyenMaTiengViet.VIETNAM_CONVERT((string)row[i], from, to);
                        }
                    }
                }
                return data_;
            }
            catch (Exception ex)
            {   
                throw new ConvertException(ex.Message);
            }
        }

        /// <summary>
        /// Đọc text dạng xml thành DataTable. Nếu lỗi trả về null.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DataTable FromXmlString(string text)
        {
            var ds = DataSetFromXml(text);
            if (ds.Tables.Count > 0) return ds.Tables[0];
            return null;
        }
        
        public static DataTable FromXmlFile(string file)
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            DataSet ds = new DataSet();
            ds.ReadXml(fs);
            fs.Close();

            if (ds.Tables.Count > 0) return ds.Tables[0];
            return null;
        }
        
        /// <summary>
        /// Đọc text dạng xml thành DataSet. Nếu lỗi trả về ds rỗng.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DataSet DataSetFromXml(string text)
        {
            var ds = new DataSet();
            try
            {
                ds.ReadXml(new StringReader(text));
            }
            catch (Exception ex)
            {
                Logger.WriteExLog("DataSetFromXml", ex, "");
            }
            return ds;
        }

        public static void ToExcelFile(DataTable data, string fileName)
        {
            ExportData.ToExcel(data, fileName, "");
        }

        public static string ToXml(DataTable data)
        {
            if (data.DataSet != null) return DataSetToXml(data.DataSet);

            DataSet ds = new DataSet("DataSet");
            ds.Tables.Add(data.Copy());
            return DataSetToXml(ds);
        }

        public static void ToXmlFile(DataTable data, string fileName)
        {
            ExportData.ToXmlFile(data, fileName);
        }

        public static string DataSetToXml(DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
            TextWriter tw = new StringWriter(sb);
            ds.WriteXml(tw);
            return sb.ToString();
        }

        /// <summary>
        /// Biến dữ liệu cột thành Dictionary, key trùng sẽ lấy dòng sau cùng.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="keyField"></param>
        /// <param name="dataField"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDataDictionary(DataTable data, string keyField, string dataField)
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

        public static Dictionary<string, string> ToStringDataDictionary(DataTable data, string keyField, string dataField)
        {
            if (!data.Columns.Contains(keyField))
                throw new Exception(string.Format("No keyField [{0}] column.", keyField));
            if (!data.Columns.Contains(dataField))
                throw new Exception(string.Format("No dataField [{0}] column.", dataField));
            var DataDic = new Dictionary<string, string>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                var row = data.Rows[i];
                DataDic[(row[keyField]+"").Trim().ToUpper()] = row[dataField] + "";
            }
            return DataDic;
        }
    }
}
