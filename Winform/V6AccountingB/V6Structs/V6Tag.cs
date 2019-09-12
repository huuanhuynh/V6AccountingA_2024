using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace V6Structs
{
    /// <summary>
    /// Thông tin định nghĩa trong Tag
    /// </summary>
    public class V6Tag
    {
        /// <summary>
        /// Lấy thông tin định nghĩa từ :; tagString.
        /// </summary>
        /// <param name="tag">readonly;invisible;...</param>
        public V6Tag(object tag)
        {
            GetV6Tag(tag+"");
        }

        private void GetV6Tag(string tagString)
        {
            try
            {
                var sss = tagString.Split(';');
                foreach (string ss in sss)
                {
                    if (ss.Contains(":"))
                    {
                        //var split = ss.Split(':');
                        int index = ss.IndexOf(':');
                        var key = ss.Substring(0, index);// split[0];
                        var value = ss.Substring(index + 1);// split[1];
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
                case "DECIMALS":
                    int.TryParse(value, out Decimals);
                    break;
                //case "SQLTYPE":
                //    sqltype = value.ToLower();
                //    break;
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
        /// BUTTON LOOKUPTEXTBOX LABEL CHECKBOX
        /// </summary>
        public string ControlType { get; set; }
        public string Format { get; set; }

        public string Value { get; set; }
        
        public string Name { get; set; }

        public string InitFilter { get; set; }

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
        
        public string Type = "T";
        
        public bool Enabled = true;

        /// <summary>
        /// Số chữ số lẽ sau dấu thập phân.
        /// </summary>
        public int Decimals = 0;
        public string DecimalSymbol = ",";
        public string ThousandSymbol = " ";
        public int MaxLength = 0;
        
        public string DescriptionE { get; set; }
        public string DescriptionV { get; set; }
        
        /// <summary>
        /// Tên bảng chính.
        /// </summary>
        public string TableName { get; set; }
        
        public string DescriptionLang(bool isVN)
        {
            return isVN ? DescriptionV : DescriptionE;
        }

        public bool ToUpper;
        
    }
}
