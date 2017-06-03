using System;
using System.Data;

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
                        if (row[i] is System.String)
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
                        if(row[i] is System.String)
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
    }
}
