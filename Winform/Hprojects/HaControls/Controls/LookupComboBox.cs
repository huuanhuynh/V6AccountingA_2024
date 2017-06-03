using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using H_Utility.Converter;
using Timer = System.Windows.Forms.Timer;

namespace H_Controls.Controls
{
    public class LookupComboBox:ComboBox
    {
        [Category("H")]
        public string DisplayField { get; set; }
        /// <summary>
        /// Tên các trường dữ liệu liên quan
        /// </summary>
        [Category("H")]
        public string BrotherFields { get; set; }
        /// <summary>
        /// Tên bảng dữ liệu lookup.
        /// </summary>
        [Category("H")]
        public string TableName { get; set; }

        /// <summary>
        /// Tên trường dữ liệu lookup.
        /// </summary>
        [Category("H")]
        public string FieldName { get; set; }

        private string _initFilter;
        public string InitFilter { get { return _initFilter; } }

        public void SetInitFilter(string filter)
        {
            _initFilter = filter;
        }

        public SortedDictionary<string, object> Data
        {
            get
            {
                if (Datas != null && SelectedIndex >= 0 && Datas.Length > SelectedIndex)
                {
                    return Datas[SelectedIndex];
                }
                return null;
            }
        }

        private SortedDictionary<string, object>[] Datas;
        private DataTable tbl;

        public LookupComboBox()
        {
            KeyDown += ColorTextBox_KeyDown;
            SelectedIndexChanged += LookupComboBox_SelectedIndexChanged;
        }
        
        private void LookupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataSource != null && SelectedValue != null)
            {
                //Setborther
                var control = HControlHelper.GetFormControl(Parent, BrotherFields);
                control.Text = Text;
            }
        }

        internal void StartLoadLookupData()
        {
            try
            {
                if (string.IsNullOrEmpty(TableName) || string.IsNullOrEmpty(FieldName)) return;

                Thread t = new Thread(LoadLookupData);
                t.IsBackground = true;
                loading = true;
                t.Start();
                Timer timer = new Timer();
                timer.Tick += (sender, e) =>
                {
                    if (!loading)
                    {
                        timer.Stop();
                        if (Datas == null)
                        {
                            DropDownStyle = ComboBoxStyle.Simple;
                        }
                        else
                        {
                            DropDownStyle = ComboBoxStyle.DropDownList;
                            DisplayMember = DisplayField;
                            ValueMember = FieldName;
                            DataSource = tbl;
                        }
                        timer.Dispose();
                    }
                };
                timer.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private bool loading;
        private void LoadLookupData()
        {
            try
            {
                Thread.Sleep((Name.Length + 1) * 100);
                var filter = InitFilter;
                if (!string.IsNullOrEmpty(filter)) filter = " where " + filter;
                else filter = "";
                string strCommand = "select top 20 [" + FieldName + "], [" + DisplayField 
                    + "] from [" + TableName + "]" + filter;
                tbl = HaControlSetting.DBA.ExecuteQuery(strCommand).Tables[0];
                Datas = tbl.ToArrayDataDictionary();
            }
            catch (Exception)
            {
                Datas = null;
            }
            loading = false;
        }

        /// <summary>
        /// Gọi sự kiện click
        /// </summary>
        public void PerformClick()
        {
            OnClick(new EventArgs());
        }
        protected virtual void ColorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        
    }
}
