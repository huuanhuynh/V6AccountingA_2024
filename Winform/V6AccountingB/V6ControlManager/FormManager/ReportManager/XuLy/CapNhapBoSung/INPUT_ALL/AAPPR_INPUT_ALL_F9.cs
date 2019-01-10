﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Controls;
using V6Controls.Forms;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AAPPR_INPUT_ALL_F9 : V6Form
    {
        protected DataRow _am;
        /// <summary>
        /// FIELD1:Label1,FIELD2....
        /// </summary>
        protected string _fields;

        public AAPPR_INPUT_ALL_F9()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields">FIELD1:Label1:Type:Options,FIELD2....</param>
        public AAPPR_INPUT_ALL_F9(string fields)
        {
            _fields = fields;
            InitializeComponent();
            MyInit();
        }

        public Dictionary<string, ListValueItem> ListValue { get; set; }
        public class ListValueItem
        {
            public string Field { get; set; }
            public object Value { get; set; }
            public string type { get; set; }
        }

        private void MyInit()
        {
            try
            {
                CreateFormControls();
                V6ControlFormHelper.SetFormDataRow(this, _am);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }


        /// <summary>
        /// FIELD:labelText
        /// </summary>
        Dictionary<string, string> _fieldDic = new Dictionary<string, string>();
        /// <summary>
        /// Danh sách field luôn update dù dữ liệu rỗng.
        /// </summary>
        Dictionary<string, string> _allwayUpdate = new Dictionary<string, string>();
        private void CreateFormControls()
        {
            try
            {
                ListValue = new Dictionary<string, ListValueItem>();
                // Phân tích danh sách Field
                string[] sss = ObjectAndString.SplitString(_fields);

                int top = 5;
                foreach (string s in sss)
                {
                    string[] ss = s.Split(':');
                    if (ss[0].Trim().Length > 0)
                    {
                        string FIELD = ss[0].ToUpper();
                        string label = ss[0];
                        string type = "";
                        List<string> options = new List<string>();
                        if (ss.Length > 1)
                        {
                            label = ss[1];
                        }
                        if (ss.Length > 2)
                        {
                            type = ss[2];
                            for (int i = 3; i < ss.Length; i++)
                            {
                                options.Add(ss[i]);
                            }
                        }
                        
                        _fieldDic.Add(FIELD, label);

                        // Tạo input cùng label
                        //Kiem tra
                        Control c = this.GetControlByAccessibleName(FIELD);
                        if (c != null)
                        {
                            V6ControlFormHelper.SetControlReadOnly(c, false);
                            continue;
                        }

                        Control txt = new TextBox(){Text = "error"};
                        if (type.StartsWith("N"))
                        {
                            int decimal_place = ObjectAndString.ObjectToInt(type.Substring(1));
                            txt = new V6NumberTextBox()
                            {
                                AccessibleName = FIELD,
                                BorderStyle = BorderStyle.FixedSingle,
                                Name = "txt" + FIELD,
                                Top = top,
                                Left = 5,
                                Width = 400,

                                DecimalPlaces = decimal_place,
                            };

                            var num = txt as V6NumberTextBox;
                            num.TextChanged += (sender, args) =>
                            {
                                ListValue[FIELD].Value = num.Value;
                            };
                            ListValue[FIELD] = new ListValueItem()
                            {
                                Field = FIELD,
                                Value = num.Value,
                                type = "N"
                            };
                        }
                        else if(type.StartsWith("D"))
                        {
                            switch (type)
                            {
                                case "D0": // Allow null
                                    txt = new V6DateTimeColor()
                                    {
                                        AccessibleName = FIELD,
                                        BorderStyle = BorderStyle.FixedSingle,
                                        Name = "txt" + FIELD,
                                        Top = top,
                                        Left = 100,
                                        Width = 400,
                                    };
                                    var d0 = txt as V6DateTimeColor;
                                    d0.TextChanged += (sender, args) =>
                                    {
                                        ListValue[FIELD].Value = ObjectAndString.ObjectToString(d0.Value, "yyyyMMdd");
                                    };
                                    ListValue[FIELD] = new ListValueItem()
                                    {
                                        Field = FIELD,
                                        Value = ObjectAndString.ObjectToString(d0.Value, "yyyyMMdd"),
                                        type = "D0"
                                    };
                                    break;
                                case "D1": // Not null
                                    txt = new V6DateTimePicker()
                                    {
                                        AccessibleName = FIELD,
                                        //BorderStyle = BorderStyle.FixedSingle,
                                        Name = "txt" + FIELD,
                                        Top = top,
                                        Left = 100,
                                        Width = 400,
                                    };
                                    var d1 = txt as V6DateTimePicker;
                                    d1.TextChanged += (sender, args) =>
                                    {
                                        ListValue[FIELD].Value = ObjectAndString.ObjectToString(d1.Value, "yyyyMMdd");
                                    };
                                    ListValue[FIELD] = new ListValueItem()
                                    {
                                        Field = FIELD,
                                        Value = ObjectAndString.ObjectToString(d1.Value, "yyyyMMdd"),
                                        type = "D1"
                                    };
                                    break;
                                case "D2": // Not null + time
                                    txt = new V6DateTimeFullPicker()
                                    {
                                        AccessibleName = FIELD,
                                        //BorderStyle = BorderStyle.FixedSingle,
                                        Name = "txt" + FIELD,
                                        Top = top,
                                        Left = 100,
                                        Width = 400,
                                    };
                                    var d2 = txt as V6DateTimeFullPicker;
                                    d2.ValueChanged += (sender, args) =>
                                    {
                                        ListValue[FIELD].Value = ObjectAndString.ObjectToString(d2.Value, "yyyy-MM-dd HH:mm:ss");
                                    };
                                    ListValue[FIELD] = new ListValueItem()
                                    {
                                        Field = FIELD,
                                        Value = ObjectAndString.ObjectToString(d2.Value, "yyyy-MM-dd HH:mm:ss"),
                                        type = "D2"
                                    };
                                    break;
                                case "D3": // null + time
                                    txt = new V6DateTimeFullPickerNull()
                                    {
                                        AccessibleName = FIELD,
                                        BorderStyle = BorderStyle.FixedSingle,
                                        Name = "txt" + FIELD,
                                        Top = top,
                                        Left = 100,
                                        Width = 400,
                                    };
                                    var d3 = txt as V6DateTimeFullPickerNull;
                                    d3.DateControl.ValueChanged += (sender, args) =>
                                    {
                                        ListValue[FIELD].Value = ObjectAndString.ObjectToString(d3.Value, "yyyy-MM-dd HH:mm:ss");
                                    };
                                    ListValue[FIELD] = new ListValueItem()
                                    {
                                        Field = FIELD,
                                        Value = ObjectAndString.ObjectToString(d3.Value, "yyyy-MM-dd HH:mm:ss"),
                                        type = "D3"
                                    };
                                    break;
                                default:
                                    break;
                            }
                            
                        }
                        else
                        {
                            txt = new V6VvarTextBox()
                            {
                                AccessibleName = FIELD,
                                BorderStyle = BorderStyle.FixedSingle,
                                Name = "txt" + FIELD,
                                Top = top,
                                Left = 100,
                                Width = 400,
                            };
                            var vvar = txt as V6VvarTextBox;
                            if(options.Count > 0) vvar.VVar = options[0];
                            if(options.Count > 1) vvar.CheckOnLeave = ObjectAndString.ObjectToBool(options[1]);
                            if(options.Count > 2) vvar.CheckNotEmpty = ObjectAndString.ObjectToBool(options[2]);
                            if(options.Count > 3) vvar.F2 = ObjectAndString.ObjectToBool(options[3]);
                            vvar.TextChanged += (sender, args) =>
                            {
                                ListValue[FIELD].Value = vvar.Text;
                            };
                            ListValue[FIELD] = new ListValueItem()
                            {
                                Field = FIELD,
                                Value = vvar.Text,
                                type = "C"
                            };
                        }

                        V6Label lbl = new V6Label()
                        {
                            Name = "lbl" + FIELD,
                            Text = label,
                            Top = top,
                            Left = 5,
                        };
                        this.Controls.Add(txt);
                        this.Controls.Add(lbl);
                        top += 25;
                        this.Height += 25;
                    }
                }

                foreach (KeyValuePair<string, string> item in _fieldDic)
                {

                }

            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateFormControls", ex);
            }
        }

        
        private void Form_Load(object sender, EventArgs e)
        {
            //SetStatus2Text();
        }
        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnHuy.PerformClick();
            }
            else if (keyData == (Keys.Control | Keys.Enter))
            {
                btnNhan.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected int _oldIndex = -1;
        
    }
}
