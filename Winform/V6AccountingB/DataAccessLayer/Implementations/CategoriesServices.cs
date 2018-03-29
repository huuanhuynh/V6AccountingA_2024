﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.Interfaces;
using V6SqlConnect;
using V6Structs;

namespace DataAccessLayer.Implementations
{
    public class CategoriesServices
    {
        ///// <summary>
        ///// Lấy lên danh sách cột ẩn định nghĩa trong bảng V6Lookup, trường v_hide
        ///// </summary>
        ///// <param name="tableName"></param>
        ///// <returns></returns>
        //public SortedDictionary<string, string> GetHideColumns(string tableName)
        //{
        //    var result = new SortedDictionary<string, string>();
        //    tableName = tableName.Replace("'", "");
        //    var data = SqlConnect.Select("V6Lookup", "Top 1 v_hide", "vMa_File='" + tableName + "'", "", "").Data;
        //    if (data != null && data.Rows.Count>0)
        //    {
        //        var hideFields = data.Rows[0][0].ToString();
        //        hideFields = hideFields.Trim().ToUpper();
        //        var sss = hideFields.Split(',');
        //        foreach (string s in sss)
        //        {
        //            var f = s.Trim();
        //            result.Add(f, f);
        //        }
        //        return result;
        //    }
        //    //var hideFields = new LookupService().GetValueByTableName(tableName, "v_hide");
        //    return new SortedDictionary<string, string>();
        //}

        //public V6TableStruct GetTableStruct(string tableName)
        //{
        //    if (string.IsNullOrEmpty(tableName)) throw new Exception("V6ERRORNOTABLE");
        //    var resultStruct = new V6TableStruct(tableName);

        //    try
        //    {
        //        string sql = "Select ORDINAL_POSITION, COLUMN_NAME" +
        //                     ", DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT, CHARACTER_MAXIMUM_LENGTH" +
        //                     ", NUMERIC_PRECISION, NUMERIC_PRECISION_RADIX, NUMERIC_SCALE" +
        //                     " From INFORMATION_SCHEMA.COLUMNS Where TABLE_NAME = '" +
        //                     tableName + "'" +
        //                     " Order by ORDINAL_POSITION";
        //        DataTable columnsStructInfo = SqlConnect.ExecuteDataset(CommandType.Text, sql)
        //            .Tables[0];

        //        foreach (DataRow row in columnsStructInfo.Rows)
        //        {
        //            var columnStruct = new V6ColumnStruct
        //            {
        //                ColumnName = row["COLUMN_NAME"].ToString().Trim(),
        //                AllowNull = "YES" == row["IS_NULLABLE"].ToString(),
        //                ColumnDefault = row["COLUMN_DEFAULT"] == null ? null : row["COLUMN_DEFAULT"].ToString().Trim(),
        //                sql_data_type_string = row["DATA_TYPE"].ToString().Trim()
        //            };
        //            try
        //            {
        //                int num;
        //                string stringLength = row["CHARACTER_MAXIMUM_LENGTH"].ToString();
        //                string numLength = row["NUMERIC_PRECISION"].ToString();
        //                string numDecimal = row["NUMERIC_SCALE"].ToString();

        //                if (stringLength != "")
        //                {
        //                    num = (int)row["CHARACTER_MAXIMUM_LENGTH"];
        //                    columnStruct.MaxLength = num;
        //                }
        //                else if (numLength != "")
        //                {
        //                    //columnStruct.MaxLength = -2;
        //                    num = Int32.Parse(numLength);
        //                    columnStruct.MaxNumLength = num;
        //                    num = Int32.Parse(numDecimal);
        //                    columnStruct.MaxNumDecimal = num;
        //                }

        //            }
        //            catch
        //            {
        //                // ignored
        //            }

        //            resultStruct.Add(columnStruct.ColumnName.ToUpper(), columnStruct);
        //        }
        //        return resultStruct;
        //    }
        //    catch
        //    {
        //        // ignored
        //    }
        //    return resultStruct;
        //}

        //public bool IsValidOneCode_Full(string cInputTable, byte nStatus, string cInputField, string cpInput, string cOldItems)
        //{
        //    SqlParameter[] plist =
        //    {
        //        new SqlParameter("@cInputTable", @cInputTable),
        //        new SqlParameter("@nStatus", @nStatus),
        //        new SqlParameter("@cInputField", @cInputField),
        //        new SqlParameter("@cpInput", @cpInput),
        //        new SqlParameter("@cOldItems", @cOldItems)
        //    };

        //    object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidOneCode_Full", plist);
        //    if (obj != null && Convert.ToInt32(obj) == 1) return true;

        //    return false;
        //}

        //public bool IsExistOneCode_List(string tables, string field, string value)
        //{
        //    SqlParameter[] plist =
        //    {
        //        new SqlParameter("@cInputTable_list", tables),
        //        new SqlParameter("@cInputField", field),
        //        new SqlParameter("@cpInput", value)
        //    };

        //    object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isExistOneCode_List", plist);
        //    if (obj != null && (int)obj == 1) return true;

        //    return false;
        //}

    }
}