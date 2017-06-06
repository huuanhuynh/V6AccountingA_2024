using System;
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
                case "ENABLED":
                    Enabled = value == "1";
                    break;
                case "F2":
                    F2 = value == "1";
                    break;
                case "FIELD":
                    Field = value;
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
                case "NOTEMPTY":
                    NotEmpty = value == "1";
                    break;
                case "OPER":
                    Oper = value;
                    break;
                case "PTYPE":
                    Ptype = value;
                    break;
                case "SQLTYPE":
                    sql_type = value;
                    break;
                case "TEXTV":
                    TextV = value;
                    break;
                case "TEXTE":
                    TextE = value;
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

                default:
                    break;
            }
        }

        public string Fname { get; set; }

        public string Ptype { get; set; }

        public string Value { get; set; }

        public string Name { get; set; }

        public string InitFilter { get; set; }

        public bool F2 { get; set; }

        public bool NotEmpty { get; set; }

        public string Field { get; set; }
        public string AccessibleName { get; set; }
        public string Oper = "";
        public string Loai_key = "10";
        public string Type = "T";
        public string Vvar = "";
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
        public string sql_type { get; set; }

        public SqlDbType SqlDbType
        {
            get
            {
                return F.ToSqlDbType(sql_type);
            }
        }

        public Type DataType
        {
            get
            {
                return F.TypeFromData_Type(sql_type);
            }
        }

        public string DefaultValue { get; set; }
        /// <summary>
        /// Key là LimitChars hoặc LimitChar không phân biệt hoa thường.
        /// </summary>
        public string LimitChars { get; set; }

        public string Fparent { get; set; }
    }
}
