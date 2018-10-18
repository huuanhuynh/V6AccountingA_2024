using System;
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

        private DataSet _ds;
        public LookupButtonDataViewForm(DataSet ds)
        {
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
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
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
                }
                else if (keyData == Keys.F4)
                {

                }
                else if (keyData == Keys.F5)
                {

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
