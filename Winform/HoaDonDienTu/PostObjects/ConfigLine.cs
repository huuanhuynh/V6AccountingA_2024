using System.Data;
using V6Tools;
using V6Tools.V6Convert;
namespace V6ThuePost
{
    public class ConfigLine
    {
        public string MA_TD2;
        public string MA_TD3;
        public decimal SL_TD1;
        public decimal SL_TD2;
        public decimal SL_TD3;

        /// <summary>
        /// Trường dữ liệu post
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// Giá trị mặc định
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Trường dữ liệu tương ứng của V6
        /// </summary>
        public string FieldV6 { get; set; }
        /// <summary>
        /// <para>Field -> lấy từ dữ liệu theo field.</para>
        /// <para>Field:Date -> Date là kiểu dữ liệu để xử lý.</para>
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Kiểu dữ liệu: Bool,Int,Long,Decimal,String,...
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// Cờ không tạo object (khi đúng điều kiện)
        /// </summary>
        public bool NoGen { get; set; }
        /// <summary>
        /// V6Remark=3+So_luong1=0 (chưa dùng)
        /// </summary>
        public string NoGenCondition { get; set; }

        public object GetValue(DataRow row)
        {
            object fieldValue = Value;
            DataTable table = row.Table;
            //if (string.IsNullOrEmpty(config.Type))
            //{
            //    return fieldValue;
            //}

            string configTYPE = null, configType_Format = null, configDATATYPE = null;
            if (!string.IsNullOrEmpty(Type))
            {
                string[] ss = Type.Split(':');
                configTYPE = ss[0].ToUpper();
                if (ss.Length > 1) configType_Format = ss[1].ToUpper();
            }
            if (string.IsNullOrEmpty(configDATATYPE))
            {
                configDATATYPE = (DataType ?? "");
            }

            if (configTYPE == "ENCRYPT")
            {
                return UtilityHelper.DeCrypt(fieldValue.ToString());
            }

            if (configTYPE == "FIELD" && !string.IsNullOrEmpty(FieldV6))
            {
                // FieldV6 sẽ có dạng thông thường là (Field) hoặc dạng ghép là (Field1 + Field2) hoặc (Field1 + "abc" + field2)
                if (table.Columns.Contains(FieldV6))
                {
                    fieldValue = row[FieldV6];
                    if (table.Columns[FieldV6].DataType == typeof(string))
                    {
                        //Trim
                        fieldValue = fieldValue.ToString().Trim();
                    }
                }
                else
                {
                    decimal giatribt;
                    if (Number.GiaTriBieuThucTry(FieldV6, row.ToDataDictionary(), out giatribt))
                    {
                        fieldValue = giatribt;
                    }
                    else
                    {
                        var fields = ObjectAndString.SplitStringBy(FieldV6.Replace("\\+", "~plus~"), '+');

                        string fieldValueString = "";

                        foreach (string s in fields)
                        {
                            string field = s.Trim();
                            if (table.Columns.Contains(field))
                            {
                                fieldValueString += ObjectAndString.ObjectToString(row[field]).Trim();
                            }
                            else
                            {
                                //if (field.StartsWith("\"") && field.EndsWith("\""))
                                //{
                                //    field = field.Substring(1, field.Length - 2);
                                //}
                                fieldValueString += field;
                            }
                        }
                        // Chốt.
                        fieldValue = fieldValueString.Replace("~plus~", "+");
                    }// end else giatribieuthuc
                }
                //// FieldV6 sẽ có dạng thông thường là (Field) hoặc dạng ghép là (Field1 + Field2) hoặc (Field1 + "abc" + field2)
                //if (table.Columns.Contains(config.FieldV6))
                //{
                //    fieldValue = row[config.FieldV6];
                //    if (table.Columns[config.FieldV6].DataType == typeof(string))
                //    {
                //        //Trim
                //        fieldValue = fieldValue.ToString().Trim();
                //    }
                //}
                //else
                //{
                //    var fields = ObjectAndString.SplitStringBy(config.FieldV6.Replace("\\+", "~plus~"), '+');
                //    fieldValue = null;
                //    string fieldValueString = null;
                //    decimal fieldValueNumber = 0m;
                //    bool still_number = true;
                //    foreach (string s in fields)
                //    {
                //        string field = s.Trim();
                //        if (table.Columns.Contains(field))
                //        {
                //            fieldValueString += ObjectAndString.ObjectToString(row[field]).Trim();
                //            if (still_number && ObjectAndString.IsNumberType(table.Columns[field].DataType))
                //            {
                //                fieldValueNumber += ObjectAndString.ObjectToDecimal(row[field]);
                //            }
                //            else
                //            {
                //                still_number = false;
                //            }
                //        }
                //        else
                //        {
                //            if (still_number)
                //            {
                //                fieldValueString += field;
                //                decimal tempNumber;
                //                if (Decimal.TryParse(field, out tempNumber))
                //                {
                //                    fieldValueNumber += tempNumber;
                //                }
                //                else
                //                {
                //                    still_number = false;
                //                }
                //            }
                //            else
                //            {
                //                if (field.StartsWith("\"") && field.EndsWith("\""))
                //                {
                //                    field = field.Substring(1, field.Length - 2);
                //                }
                //                fieldValueString += field;
                //            }
                //        }
                //    }
                //    // Chốt.
                //    if (still_number) fieldValue = fieldValueNumber;
                //    else fieldValue = fieldValueString.Replace("~plus~", "+");
                //}
            }

            if (!string.IsNullOrEmpty(configDATATYPE))
            {
                string DATATYPE = configDATATYPE.ToUpper();
                switch (DATATYPE)
                {
                    case "BOOL":
                        if (fieldValue is bool)
                        {
                            return fieldValue;
                        }
                        else
                        {
                            return fieldValue != null &&
                                (fieldValue.ToString() == "1" ||
                                    fieldValue.ToString().ToLower() == "true" ||
                                    fieldValue.ToString().ToLower() == "yes");
                        }
                    case "DATE":
                    case "DATETIME":
                        return ObjectAndString.ObjectToDate(fieldValue, Format);
                        break;
                    case "N2C":
                        return DocSo.MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", "VND");
                    case "N2CE":
                        return DocSo.MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "E", "VND");
                    case "N2CMANT":
                        return DocSo.MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", row["MA_NT"].ToString().Trim());
                    case "N2CMANTE":
                        return DocSo.MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "E", row["MA_NT"].ToString().Trim());
                    case "N2C0VNDE":
                        {
                            string ma_nt = row["MA_NT"].ToString().Trim().ToUpper();
                            if (ma_nt != "VND")
                            {
                                return DocSo.MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "E", row["MA_NT"].ToString().Trim());
                            }
                            else
                            {
                                return "";
                            }
                        }
                    case "DECIMAL":
                    case "MONEY":
                        return ObjectAndString.ObjectToDecimal(fieldValue);
                    case "INT":
                        return ObjectAndString.ObjectToInt(fieldValue);
                    case "INT64":
                    case "LONG":
                        return ObjectAndString.ObjectToInt64(fieldValue);
                    case "UPPER": // Chỉ dùng ở exe gọi bằng Foxpro.
                        return (fieldValue + "").ToUpper();
                    default:
                        if (configDATATYPE.StartsWith("Date:") || configDATATYPE.StartsWith("date:"))
                        {
                            string date_format = configDATATYPE.Substring(5);
                            fieldValue = ObjectAndString.ObjectToString(ObjectAndString.ObjectToDate(fieldValue), date_format);
                        }
                        else if (!string.IsNullOrEmpty(Format))
                        {
                            fieldValue = ObjectAndString.ObjectToString(fieldValue, Format);
                        }
                        return fieldValue;
                }
            }
            else
            {
                return fieldValue;
            }
        }
    }
}