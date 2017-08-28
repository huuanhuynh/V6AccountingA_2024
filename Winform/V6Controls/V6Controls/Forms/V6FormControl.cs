using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6Tools;

namespace V6Controls.Forms
{
    /// <summary>
    /// Dùng cho loại form chính thực hiện 1 công việc gì đó.
    /// Trong này đã có sẵn hàm DoHotKey thực hiện theo Tag.
    /// Ví dụ Hóa đơn control
    /// </summary>
    public partial class V6FormControl : V6Control
    {
        protected Image btnNhanImage = Properties.Resources.Apply;
        protected ImageList waitingImages { get { return _waitingImages; } }
        protected int ii = 0;
        public string _sttRec { get; set; }
        protected bool _escape = false;
        
        protected string _message = "";
        public bool Data_Loading, _load_data_success;
        

        public V6FormControl()
        {
            InitializeComponent();
            Disposed += (sender, args) =>
            {
                V6ControlFormHelper.RemoveRunningList(_sttRec);
            };
        }

        /// <summary>
        /// Lấy dữ liệu trên form.
        /// </summary>
        /// <returns></returns>
        public virtual SortedDictionary<string, object> GetData()
        {
            return V6ControlFormHelper.GetFormDataDictionary( this );
        }
        /// <summary>
        /// Gán dữ liệu lên form theo AccessibleName (không phân biệt hoa thường).
        /// Dữ liệu không có sẽ gán rỗng.
        /// </summary>
        /// <param name="d"></param>
        public virtual void SetData(IDictionary<string, object> d)
        {
            V6ControlFormHelper.SetFormDataDictionary( this, d );
        }
        
        /// <summary>
        /// Giống SetData. Dùng để override lấy dữ liệu theo khóa.
        /// </summary>
        /// <param name="keyData">Dữ liệu khóa</param>
        public virtual void SetDataKeys(SortedDictionary<string, object> keyData)
        {
            V6ControlFormHelper.SetFormDataDictionary( this, keyData );
        }
        /// <summary>
        /// Gán dữ liệu cho vài control trên form theo AccessibleName nếu có trong data.
        /// </summary>
        /// <param name="d"></param>
        public virtual void SetSomeData(SortedDictionary<string, object> d)
        {
            V6ControlFormHelper.SetSomeDataDictionary( this, d );
        }
        
        public virtual void SetStatus2Text()
        {
            //V6ControlFormHelper.SetStatusText2("");
        }

        /// <summary>
        /// Gán thông tin hướng dẫn khi rê chuột lên.
        /// </summary>
        /// <param name="control">Control trên form</param>
        /// <param name="tip">Dòng chữ hướng dẫn</param>
        protected void SetToolTip(Control control, string tip)
        {
            toolTipV6FormControl.SetToolTip(control, tip);
        }

        protected SortedList<int,int> _rowIndex = new SortedList<int, int>();
        protected SortedList<int, int> _cellIndex = new SortedList<int, int>();
        protected void SaveSelectedCellLocation(DataGridView dataGridView1, int saveIndex = 0)
        {
            try
            {
                if (dataGridView1.CurrentCell != null)
                {
                    _rowIndex[saveIndex] = dataGridView1.CurrentCell.RowIndex;
                    _cellIndex[saveIndex] = dataGridView1.CurrentCell.ColumnIndex;
                }
                else
                {
                    _rowIndex[saveIndex] = 0;
                    _cellIndex[saveIndex] = 0;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SaveSelectedCellLocation " + ex.Message);
            }
        }

        protected void LoadSelectedCellLocation(DataGridView dataGridView1, int saveIndex = 0)
        {
            V6ControlFormHelper.SetGridviewCurrentCellByIndex(dataGridView1, _rowIndex[saveIndex], _cellIndex[saveIndex], this);
        }

        private void V6FormControl_Load(object sender, EventArgs e)
        {
            SetStatus2Text();
            LoadLanguage();
        }

        private void V6UserControl_VisibleChanged(object sender, EventArgs e)
        {
            if(Visible)
                SetStatus2Text();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == Keys.F3)
                {
                    f3count++;
                    V6F3Execute();
                }
                else
                {
                    f3count = 0;
                }

                if (do_hot_key)
                {
                    do_hot_key = false;
                    return base.ProcessCmdKey(ref msg, keyData);
                }

                if(DoHotKey0(keyData)) return true;
            }
            catch
            {
                return false;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected bool do_hot_key;
        /// <summary>
        /// Viết lệnh do_hot_key = true; trước.
        /// Các form kế thừa nếu có phím nóng quy định riêng cần override lại.
        /// Nếu không có hotkey định nghĩa thì gọi lại base.
        /// </summary>
        /// <param name="keyData"></param>
        public override void DoHotKey (Keys keyData)
        {
            try
            {
                //if (keyData == Keys.F3)
                //{
                //    f3count++;
                //    V6F3Execute();
                //}
                //else
                //{
                //    f3count = 0;
                //}

                do_hot_key = true;
                DoHotKey0(keyData);
            }
            catch
            {
                // ignored
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            //if (keyData == Keys.F3)
            //{
            //    f3count++;
            //    V6F3Execute();
            //}
            //else
            //{
            //    f3count = 0;
            //}
            return V6ControlFormHelper.DoKeyCommand(this, keyData);
        }

        protected int f3count;
        public virtual void V6F3Execute()
        {
            
        }

        /// <summary>
        /// Gán dữ liệu mặc định lên form.
        /// </summary>
        /// <param name="loai">1ct 4report</param>
        /// <param name="mact"></param>
        /// <param name="madm"></param>
        /// <param name="itemId"></param>
        /// <param name="adv"></param>
        protected void LoadDefaultData(int loai, string mact, string madm, string itemId, string adv = "")
        {
            try
            {
                var data = GetDefaultData(V6Setting.Language, 4, mact, madm, itemId, adv);
                var data0 = new SortedDictionary<string, object>();
                data0.AddRange(data);
                V6ControlFormHelper.SetFormDataDictionary(this, data0, false);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadDefaultData", ex);
            }
        }

        /// <summary>
        /// Gán Tag được lưu lên form.
        /// </summary>
        /// <param name="loai">1ct 4report</param>
        /// <param name="mact"></param>
        /// <param name="madm"></param>
        /// <param name="adv"></param>
        protected void LoadTag(int loai, string mact, string madm, string adv = "")
        {
            try
            {
                var data = GetTagData(loai, mact, madm, adv);
                V6ControlFormHelper.SetFormTagDictionary(this, data);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadTag", ex);
            }
        }

        protected void LoadReadonly(int loai, string mact, string madm, string adv = "")
        {
            try
            {
                var data = GetReadonlyData(loai, mact, madm, adv);
                V6ControlFormHelper.SetFormTagDictionary(this, data);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadReadonly", ex);
            }
        }

        /// <summary>
        /// Tải dữ liệu và trả về DefaultData.
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="loai">1ct, 4report</param>
        /// <param name="mact"></param>
        /// <param name="madm"></param>
        /// <param name="itemId"></param>
        /// <param name="adv"></param>
        /// <returns></returns>
        private SortedDictionary<string, string> GetDefaultData(string lang, int loai, string mact, string madm, string itemId, string adv = "")
        {
            if (defaultData != null && defaultData.Count > 0) return defaultData;
            if (alinitData == null || alinitData.Rows.Count == 0)
                alinitData = V6BusinessHelper.GetDefaultValueData(loai, mact, madm, itemId, adv);
            var result = new SortedDictionary<string, string>();
            foreach (DataRow row in alinitData.Rows)
            {
                var cell = row["Default" + lang]; if (cell == null) continue;
                var value = cell.ToString().Trim(); if (value == "") continue;

                var name = row["NameVal"].ToString().Trim().ToUpper();
                result[name] = value;
            }
            defaultData = result;
            return result;
        }

        protected DataTable alinitData;
        private SortedDictionary<string, string> defaultData;
        private SortedDictionary<string, string> tagData;
        private SortedDictionary<string, string> readonlyData;
        private SortedDictionary<string, string> visibleData;

        /// <summary>
        /// Tải dữ liệu và trả về TagData.
        /// </summary>
        /// <param name="loai">1ct 4report</param>
        /// <param name="mact"></param>
        /// <param name="madm"></param>
        /// <param name="itemId"></param>
        /// <param name="adv"></param>
        /// <returns></returns>
        private SortedDictionary<string, string> GetTagData(int loai, string mact, string madm, string itemId, string adv = "")
        {
            if (tagData != null && tagData.Count > 0) return tagData;
            if (alinitData == null || alinitData.Rows.Count == 0)
                alinitData = V6BusinessHelper.GetDefaultValueData(loai, mact, madm, itemId, adv);
            var result = new SortedDictionary<string, string>();
            foreach (DataRow row in alinitData.Rows)
            {
                var cell = row["Tag"]; if (cell == null) continue;
                var value = cell.ToString().Trim(); if (value == "") continue;

                var name = row["NameTag"].ToString().Trim().ToUpper();
                result[name] = value;
            }
            tagData = result;
            return result;
        }

        private SortedDictionary<string, string> GetReadonlyData(int loai, string mact, string madm, string itemId, string adv = "")
        {
            if (readonlyData != null && readonlyData.Count > 0) return readonlyData;
            if (alinitData == null || alinitData.Rows.Count == 0)
                alinitData = V6BusinessHelper.GetDefaultValueData(loai, mact, madm, itemId, adv);
            var result = new SortedDictionary<string, string>();
            foreach (DataRow row in alinitData.Rows)
            {
                var cell = row["Readonly"]; if (cell == null) continue;
                var value = cell.ToString().Trim(); if (value == "") continue;

                var name = row["NameTag"].ToString().Trim().ToUpper();
                result[name] = value;
            }
            readonlyData = result;
            return result;
        }

        private SortedDictionary<string, string> GetHideData(int loai, string mact, string madm, string itemId, string adv = "")
        {
            if (visibleData != null && visibleData.Count > 0) return visibleData;
            if (alinitData == null || alinitData.Rows.Count == 0)
                alinitData = V6BusinessHelper.GetDefaultValueData(loai, mact, madm, itemId, adv);
            var result = new SortedDictionary<string, string>();
            foreach (DataRow row in alinitData.Rows)
            {
                var cell = row["Hide"]; if (cell == null) continue;
                var value = cell.ToString().Trim(); if (value == "") continue;

                var name = row["NameTag"].ToString().Trim().ToUpper();
                result[name] = value;
            }
            visibleData = result;
            return result;
        }
    }
}
