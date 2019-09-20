﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6ReportControls;
using V6Structs;

namespace V6ControlManager.FormManager.SoDuManager
{
    public partial class SoDuFilterForm : V6Form
    {
        public delegate void FilterOkHandle(string query);

        public event FilterOkHandle FilterOkClick;

        protected virtual void CallFilterOkClick()
        {
            FilterOkHandle handler = FilterOkClick;
            if (handler != null) handler(QueryString);
        }

        private string QueryString
        {
            get
            {
                return panel1.QueryString;
            }
        }
        private V6TableStruct _structTable;
        private string[] _fields;

        public SoDuFilterForm()
        {
            InitializeComponent();
        }

        
        public SoDuFilterForm(V6TableStruct structTable, string[] fields)
        {
            InitializeComponent();
            _structTable = structTable;
            _fields = fields;
            MyInit();
        }

        private void MyInit()
        {
           MadeControls();
        }

        private void MadeControls()
        {
            string err = "";
            try
            {
                // Add auto fields
                if (_fields == null || _fields.Length == 0)
                {
                    Text += " (Auto fields)";
                    List<string> list = new List<string>();
                    foreach (KeyValuePair<string, V6ColumnStruct> column_item in _structTable)
                    {
                        string KEY = column_item.Key;
                        V6ColumnStruct column = column_item.Value;
                        if ((KEY.StartsWith("MA") || KEY.StartsWith("TEN")) && column.DataType == typeof(string))
                        {
                            list.Add(KEY);
                            if (list.Count >= 4) break;
                        }
                    }
                    _fields = list.ToArray();
                }
                panel1.AddMultiFilterLine(_structTable, string.Join(",", _fields));
            }
            catch (Exception ex)
            {
                err += "\n" + ex.Message;
            }
            if (err.Length > 0)
            {
                this.ShowErrorMessage(GetType() + ".MadeControls error!" + err);
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Control | Keys.Enter))
                {
                    CallFilterOkClick();
                    return true;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return base.DoHotKey0(keyData);
        }

        private void FilterForm_Load(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            CallFilterOkClick();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
