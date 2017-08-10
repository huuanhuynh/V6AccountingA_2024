﻿using System;
using System.Collections.Generic;
using System.Data;
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
                var sss = define.Split(';');
                foreach (string ss in sss)
                {
                    if (ss.Contains(":"))
                    {
                        var split = ss.Split(':');
                        var key = split[0];
                        var value = split[1];
                        SetDefine(key, value);
                    }
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
                case "BFIELD":
                    BField = value;
                    break;
                case "CONTROLTYPE":
                    ControlType = value;
                    break;
                case "DECIMALS":
                    int.TryParse(value, out Decimals);
                    break;
                case "DESCRIPTIONE":
                    DescriptionE = value;
                    break;
                case "DESCRIPTIONV":
                    DescriptionV = value;
                    break;
                case "ENABLED":
                    Enabled = value == "1";
                    break;
                case "EVENT":
                    Event = value;
                    break;
                case "F2":
                    F2 = value == "1";
                    break;
                case "FIELD":
                    Field = value;
                    break;
                case "FIELD2":
                    Field2 = value;
                    break;
                case "FILTERSTART":
                    FilterStart = value == "1";
                    break;
                case "FNAME":
                    Fname = value;
                    break;
                case "FPARENT":
                    Fparent = value;
                    break;
                case "ACCESSIBLENAME":
                    AccessibleName = value;
                    break;
                case "ACCESSIBLENAME2":
                    AccessibleName2 = value;
                    break;
                case "DEFAULTVALUE":
                    DefaultValue = value;
                    break;
                case "INITFILTER":
                    InitFilter = value;
                    break;
                case "LIMITCHAR":
                case "LIMITCHARS":
                    LimitChars = value;
                    break;
                case "LOAI_KEY":
                    Loai_key = value;
                    break;
                case "NAME":
                    Name = value;
                    break;
                case "MA_DM":
                    MA_DM = value;
                    break;
                case "NOTEMPTY":
                    NotEmpty = value == "1";
                    break;
                case "OVERRIDE":
                    Override = value == "1";
                    break;
                case "NOOVERRIDE":
                    NoOverride = value == "1";
                    break;
                case "OPER":
                    Oper = value;
                    break;
                case "PTYPE":
                    Ptype = value;
                    break;
                case "SQLTYPE":
                    sqltype = value.ToLower();
                    break;
                case "STATUS":
                    Status = value == "1";
                    break;
                case "TEXTV":
                    TextV = value;
                    break;
                case "TEXTE":
                    TextE = value;
                    break;
                case "TOUPPER":
                    ToUpper = value == "1";
                    break;
                case "TYPE":
                    if (!string.IsNullOrEmpty(value)) Type = value;
                    break;
                case "VALUE":
                    Value = value;
                    break;
                case "VISIBLE":
                    Visible = value == "1";
                    break;
                case "VVAR":
                    Vvar = value;
                    break;
                case "WIDTH":
                    Width = value;
                    break;
                case "MAXLENGTH":
                    int.TryParse(value, out MaxLength);
                    break;

                default:
                    break;
            }
        }

        public string ControlType { get; set; }

        /// <summary>
        /// Trường để lấy data trong vvar_data dùng cho RptExtraParameters khi Ptype = FILTER_BROTHER.
        /// </summary>
        public string Fname { get; set; }
        /// <summary>
        /// TABLE2, PARENT, FILTER, FILTER_BROTHER, Dùng trong định nghĩa ds ExtraParameterInfo.
        /// </summary>
        public string Ptype { get; set; }
        /// <summary>
        /// Giá trị, tùy ý sử dụng.
        /// </summary>
        public string Value { get; set; }

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
        public string Vvar = "";

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
        public bool Visible = true;
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
        /// <summary>
        /// Trường lọc số liệu khi F5 (trường trong parent data).
        /// </summary>
        public string Fparent { get; set; }

        public string Width { get; set; }
        /// <summary>
        /// Brother Field
        /// </summary>
        public string BField { get; set; }

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
    }
}
