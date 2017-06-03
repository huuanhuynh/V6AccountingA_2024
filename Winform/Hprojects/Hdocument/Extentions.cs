using System.Collections.Generic;
using System.Data;

namespace H_document
{
    public static class Extentions
    {
        public static SortedDictionary<string, string> ToDictionary(this DataRow row)
        {
            var result = new SortedDictionary<string, string>();
            if (row == null) return result;
            foreach (DataColumn column in row.Table.Columns)
            {
                result.Add(column.ColumnName.ToUpper(), row[column.ColumnName].ToString());
            }
            return result;
        } 
    }
}
