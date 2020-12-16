using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6Controls.Forms;
using V6Init;

namespace V6Controls.Controls
{
    public partial class LookupButtonDataViewForm : V6Form
    {
        public LookupButtonDataViewForm()
        {
            InitializeComponent();
        }

        private LookupButton _lookupButton;
        private DataSet _ds;

        public event DataSelectHandler AcceptSelectedtData;
        public bool Shift { get; set; }

        protected virtual void OnAccepSelectedtData(string idlist, List<IDictionary<string, object>> datalist)
        {
            var handler = AcceptSelectedtData;
            if (handler != null) handler(idlist, datalist);
        }

        public LookupButtonDataViewForm(LookupButton lookupButton, DataSet ds)
        {
            _lookupButton = lookupButton;
            _ds = ds;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                dataGridView1.DataSource = _ds.Tables[0];
                FormatGridView();
                if (_lookupButton.M_Type == "4")
                {
                    dataGridView1.EnableSelect();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }


        public event LookupButtonEventHandler LookupButtonF3Event;
        protected virtual void OnLookupButtonEvent(object sender, LookupEventArgs e)
        {
            var handler = LookupButtonF3Event;
            if (handler != null) handler(sender, e);
        }

        

        public override bool DoHotKey0(Keys keyData)
        {
            try
            {
                if (keyData == Keys.Escape)
                {
                    Close();
                }
                else if (keyData == Keys.F3)
                {
                    //var hoaDonForm = ChungTuF3.GetChungTuControl(maCt, Name, sttRec);
                    var row = dataGridView1.CurrentRow;
                    if (row != null)
                    {
                        OnLookupButtonEvent(this, new LookupEventArgs
                        {
                            MaCt = row.Cells["ma_ct"].Value.ToString().Trim(),
                            Stt_rec = row.Cells["Stt_rec"].Value.ToString().Trim(),
                        });
                    }
                }
                else if (keyData == Keys.F4)
                {

                }
                else if (keyData == Keys.F5)
                {

                }
                else if (keyData == Keys.Enter)
                {
                    if (!Shift) return true;
                    if (_lookupButton.M_Type == "4")
                    {
                        // Gọi sự kiện Accept Select.
                        var selectedData = dataGridView1.GetSelectedData();
                        if (selectedData.Count == 0 && dataGridView1.RowCount > 0)
                        {
                            selectedData.Add(V6Tools.V6ToolExtensionMethods.DataGridViewRowToDataDictionary(dataGridView1.CurrentRow));
                        }
                        string selectedValues = "";
                        V6lookupConfig config = V6Lookup.GetV6lookupConfig(_lookupButton.R_Vvar);
                        foreach (IDictionary<string, object> item in selectedData)
                        {
                            selectedValues += "," + item[config.vValue].ToString().Trim();
                        }

                        if (selectedValues.Length > 0) selectedValues = selectedValues.Substring(1);

                        _lookupButton.ReferenceControl.Text = selectedValues;

                        OnAccepSelectedtData(selectedValues, selectedData);
                        Close();
                    }
                }

                return base.DoHotKey0(keyData);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void FormatGridView()
        {
            string showFields = "";
            string formatStrings = "";
            string headerString = "";
            if (_ds.Tables.Count > 1 && _ds.Tables[1].Rows.Count > 0)
            {
                var data = _ds.Tables[1];
                if (data.Columns.Contains("GRDS_V1")) showFields = data.Rows[0]["GRDS_V1"].ToString();
                if (data.Columns.Contains("GRDF_V1")) formatStrings = data.Rows[0]["GRDF_V1"].ToString();
                var f = V6Setting.IsVietnamese ? "GRDHV_V1" : "GRDHE_V1";
                if (data.Columns.Contains(f)) headerString = data.Rows[0][f].ToString();
            }
            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, showFields, formatStrings, headerString);
        }

        
    }

    
}
