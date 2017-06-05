using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace V6Controls.Forms.DanhMuc.Add_Edit.Albc
{
    public partial class AlbcExcel2EditorForm : V6Form
    {
        public AlbcExcel2EditorForm()
        {
            InitializeComponent();
        }

        private readonly Control _excel2;
        private DataSet _ds;

        public AlbcExcel2EditorForm(Control excel2)
        {
            InitializeComponent();
            _excel2 = excel2;
            Read();
        }

        private void Read()
        {
            try
            {
                _ds = new DataSet("DataSet");
                _ds.ReadXml(new StringReader(_excel2.Text));
                if (_ds.Tables.Count > 0)
                {
                    var table = _ds.Tables[0];
                    if (!table.Columns.Contains("type"))
                    {
                        table.Columns.Add("type", typeof (string));
                    }
                    if (!table.Columns.Contains("key"))
                    {
                        table.Columns.Add("key", typeof(string));
                    }
                    if (!table.Columns.Contains("content"))
                    {
                        table.Columns.Add("content", typeof(string));
                    }
                    _ds.DataSetName = "Dataset";
                    _ds.Tables[0].TableName = "ExcelConfig";
                    dataGridView1.DataSource = _ds.Tables[0];
                }
            }
            catch
            {
                if (_ds.Tables.Count == 0)
                {
                    _ds.Tables.Add(new DataTable("ExcelConfig"));
                }
                var table = _ds.Tables[0];
                if (!table.Columns.Contains("type"))
                {
                    table.Columns.Add("type", typeof(string));
                }
                if (!table.Columns.Contains("key"))
                {
                    table.Columns.Add("key", typeof(string));
                }
                if (!table.Columns.Contains("content"))
                {
                    table.Columns.Add("content", typeof(string));
                }
                if (table.Rows.Count == 0)
                {
                    var newRow = table.NewRow();
                    newRow["type"] = 0;
                    newRow["key"] = "FirstCell";
                    newRow["content"] = "A4";
                    table.Rows.Add(newRow);
                }
                dataGridView1.DataSource = _ds.Tables[0];
            }
        }

        private void Write()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                TextWriter tw = new StringWriter(sb);
                _ds.WriteXml(tw);
                _excel2.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("Write xml error: " + ex.Message);
            }
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            Write();
            Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
