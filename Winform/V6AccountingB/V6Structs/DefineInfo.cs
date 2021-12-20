using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace V6Structs
{
    public class DefineInfo
    {
        /// <summary>
        /// Lấy thông tin định nghĩa :;
        /// </summary>
        /// <param name="define">field:ngay_ct1;textv:Từ ngày;textE:From;where_field:ngay_ct;type:D;sqltype:smalldatetime;loai_key:10;oper:and;sqltype:smalldatetime;limitchar:ABCabc123;defaultValue:m_ngay_ct1</param>
        public DefineInfo(string define)
        {
            GetDefineInfo(define);
        }

        private void GetDefineInfo(string define)
        {
            try
            {
                // define = key:value;key:2value2;key3:va;lue;key4:value4
                
                var sss = define.Split(';');
                string LAST_KEY = null;
                string lastValue = null;
                foreach (string s in sss)
                {
                    var ss = s.Split(':');
                    if (ss.Length > 1)
                    {
                        LAST_KEY = ss[0].Trim().ToUpper();
                        lastValue = s.Substring(LAST_KEY.Length + 1);
                        SetDefine(LAST_KEY, lastValue);
                    }
                    else
                    {
                        if (LAST_KEY != null)
                        {
                            lastValue += ";" + s;
                            SetDefine(LAST_KEY, lastValue);
                        }
                    }
                    //if (s_item.Contains(":"))
                    //{
                    //    //var split = ss.Split(':');
                    //    int index = s_item.IndexOf(':');
                    //    var key = s_item.Substring(0, index);// split[0];
                    //    var value = s_item.Substring(index + 1);// split[1];
                    //    SetDefine(key, value);
                    //}
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        private void SetDefine(string key, string value)
        {
            switch (key.ToUpper())
            {
                case "DECIMALS":
                    int.TryParse(value, out Decimals);
                    break;
                case "SQLTYPE":
                    sqltype = value.ToLower();
                    break;
                case "TYPE":
                    if (!string.IsNullOrEmpty(value)) Type = value;
                    break;
                case "MAXLENGTH":
                    int.TryParse(value, out MaxLength);
                    break;

                default: // Mặc định gán cho string Property.
                    foreach (PropertyInfo propertyInfo in GetType().GetProperties())
                    {
                        if (propertyInfo.Name.ToUpper() == key.ToUpper() && propertyInfo.CanWrite)
                        {
                            if (propertyInfo.PropertyType == typeof(string))
                            {
                                propertyInfo.SetValue(this, value, null);
                            }
                            else if (propertyInfo.PropertyType == typeof(int))
                            {
                                propertyInfo.SetValue(this, int.Parse(value), null);
                            }
                            else if (propertyInfo.PropertyType == typeof(decimal))
                            {
                                propertyInfo.SetValue(this, decimal.Parse(value), null);
                            }
                            else if (propertyInfo.PropertyType == typeof(DateTime))
                            {
                                propertyInfo.SetValue(this, DateTime.ParseExact(value, "dd/MM/yyyy", null), null);
                            }
                            else if (propertyInfo.PropertyType == typeof(bool))
                            {
                                propertyInfo.SetValue(this, value == "1", null);
                            }
                            break;
                        }
                    }

                    foreach (FieldInfo propertyInfo in GetType().GetFields())
                    {
                        if (propertyInfo.Name.ToUpper() == key.ToUpper() && propertyInfo.IsPublic)
                        {
                            if (propertyInfo.FieldType == typeof(string))
                            {
                                propertyInfo.SetValue(this, value);
                            }
                            else if (propertyInfo.FieldType == typeof(int))
                            {
                                propertyInfo.SetValue(this, int.Parse(value));
                            }
                            else if (propertyInfo.FieldType == typeof(decimal))
                            {
                                propertyInfo.SetValue(this, decimal.Parse(value));
                            }
                            else if (propertyInfo.FieldType == typeof(DateTime))
                            {
                                propertyInfo.SetValue(this, DateTime.ParseExact(value, "dd/MM/yyyy", null));
                            }
                            else if (propertyInfo.FieldType == typeof(bool))
                            {
                                propertyInfo.SetValue(this, value == "1");
                            }
                            break;
                        }
                    }
                    break;
            }
        }

        

        /// <summary>
        /// BUTTON LOOKUPTEXTBOX LABEL CHECKBOX TEXTBOX RICHTEXTBOX
        /// </summary>
        public string ControlType { get; set; }

        /// <summary>
        /// Trường để lấy data trong vvar_data dùng cho RptExtraParameters khi Ptype = FILTER_BROTHER.
        /// </summary>
        public string Fname { get; set; }
        public bool MultiLine { get; set; }
        /// <summary>
        /// TABLE2, PARENT, FILTER, FILTER_BROTHER, Dùng trong định nghĩa ds ExtraParameterInfo.
        /// </summary>
        public string Ptype { get; set; }
        /// <summary>
        /// Giá trị, tùy ý sử dụng.
        /// </summary>
        public string Value { get; set; }
        public string VName { get; set; }
        public string VName2 { get; set; }

        public string MA_DM { get; set; }
        public string Name { get; set; }

        public string InitFilter { get; set; }

        public bool F2 { get; set; }

        /// <summary>
        /// Bỏ qua giá trị rỗng trong GetRptParametersD, Dữ liệu mới phải khác rỗng trong SetControlValue config.
        /// </summary>
        public bool NotEmpty { get; set; }
        /// <summary>
        /// Trường dữ liệu
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// Trường text hiển thị
        /// </summary>
        public string Field2 { get; set; }
        /// <summary>
        /// Tên khóa dữ liệu khi lấy dữ liệu trên form
        /// </summary>
        public string AccessibleName { get; set; }
        /// <summary>
        /// Tên khóa dữ liệu ShowText khi lấy dữ liệu trên form của V6LookupTextBox
        /// </summary>
        public string AccessibleName2 { get; set; }
        public string Oper = "";
        /// <summary>
        /// A1:FilterAdvance
        /// </summary>
        public string Loai_key = "10";
        public string Type = "T";
        public string Vvar { get; set; }

        public string TextLang(bool isVN)
        {
            return isVN ? TextV : TextE;
        }
        public string TextV;
        public string TextE;
        /// <summary>
        /// Tên parameter sql
        /// </summary>
        public string Key1;
        public string Key2;
        public string Key3;
        public string Key4;
        public bool Visible;
        public string ShowName = "";
        public bool Enabled = true;

        /// <summary>
        /// lowercase
        /// </summary>
        public string sqltype { get; set; }
        /// <summary>
        /// Kiểu dữ liệu sql lấy từ sql_type.
        /// </summary>
        public SqlDbType SqlDbType
        {
            get
            {
                return F.ToSqlDbType(sqltype);
            }
        }
        /// <summary>
        /// Kiểu dữ liệu lấy từ sql_type.
        /// </summary>
        public Type DataType
        {
            get
            {
                return F.TypeFromData_Type(sqltype);
            }
        }
        /// <summary>
        /// Số chữ số lẽ sau dấu thập phân.
        /// </summary>
        public int Decimals = 0;
        public int MaxLength = 0;
        public string DefaultValue { get; set; }
        /// <summary>
        /// Key là LimitChars hoặc LimitChar không phân biệt hoa thường.
        /// </summary>
        public string LimitChars { get; set; }
        public string LimitChars0 { get; set; }
        public bool UseChangeText { get; set; }
        public bool UseLimitChars0 { get; set; }
        /// <summary>
        /// Trường lọc số liệu khi F5 (trường trong parent data).
        /// </summary>
        public string Fparent { get; set; }

        public string Width { get; set; }
        /// <summary>
        /// Brother Field
        /// </summary>
        public string BField { get; set; }
        public string BField2 { get; set; }
        /// <summary>
        /// Neightbor Field
        /// </summary>
        public string NField { get; set; }
        /// <summary>
        /// Tên event
        /// </summary>
        public string Event { get; set; }

        public string DescriptionE { get; set; }
        public string DescriptionV { get; set; }
        /// <summary>
        /// Bật tắt tính năng lọc chỉ bắt đầu. Mặc định false sẽ lọc like '%abc%'.
        /// </summary>
        public bool FilterStart { get; set; }

        /// <summary>
        /// Tên bảng chính.
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// Trường mã chính.
        /// </summary>
        public string FieldMa { get; set; }
        /// <summary>
        /// Trường dữ liệu bảng chính.
        /// </summary>
        public string FieldValue { get; set; }
        /// <summary>
        /// Trường hiển thị bảng chính.
        /// </summary>
        public string FieldDisplay { get; set; }
        /// <summary>
        /// Trường sắp xếp bảng chính.
        /// </summary>
        public string FieldOrder { get; set; }
        /// <summary>
        /// Tên bảng chi tiết.
        /// </summary>
        public string TableNameCt { get; set; }
        /// <summary>
        /// Trường dữ liệu bảng chi tiết.
        /// </summary>
        public string FieldValueCt { get; set; }
        /// <summary>
        /// Trường hiển thị bảng chi tiết.
        /// </summary>
        public string FieldDisplayCt { get; set; }
        /// <summary>
        /// Trường sắp xếp bảng chi tiết.
        /// </summary>
        public string FieldOrderCt { get; set; }

        /// <summary>
        /// Bật tắt TemplateSetting. false sẽ bỏ qua hết các xử lý của trường hợp đó.
        /// </summary>
        public bool Status = false;
        /// <summary>
        /// Luôn ghi đè.
        /// </summary>
        public bool Override = true;
        /// <summary>
        /// Nếu đã có không ghi đè.
        /// </summary>
        public bool NoOverride = false;

        public string DescriptionLang(bool isVN)
        {
            return isVN ? DescriptionV : DescriptionE;
        }

        public bool ToUpper;
        // LookupButton info
        public string R_DataType;
        public string R_Ma_ct;

        public string M_DataType;
        public string M_Value;
        public string M_Vvar;
        public string M_Stt_Rec;
        public string M_Ma_ct;
        public string M_Type;
    }
}
