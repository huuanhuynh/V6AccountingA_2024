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
        private readonly string _file_xml;

        public AlbcExcel2EditorForm(Control excel2, string file_xml)
        {
            InitializeComponent();
            _excel2 = excel2;
            _file_xml = file_xml;
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
                V6ControlFormHelper.ShowErrorException(GetType() + ".Write", ex);
            }
        }

        private void WriteToFile()
        {
            try
            {
                //var saveFile = V6ControlFormHelper.ChooseSaveFile("Xml|*.xml");
                var saveFile = new SaveFileDialog
                {
                    Filter = "XML files (*.Xml)|*.xml",
                    Title = "Xuất XML.",
                    FileName = _file_xml
                };
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(saveFile.FileName)) return;
                    FileStream fs = new FileStream(saveFile.FileName, FileMode.Create);
                    _ds.WriteXml(fs);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException(GetType() + ".WriteToFile", ex);
            }
        }

        private void LoadFromFile()
        {
            try
            {
                var openFile = V6ControlFormHelper.ChooseOpenFile("Xml|*.xml");
                if (string.IsNullOrEmpty(openFile)) return;
                FileStream fs = new FileStream(openFile, FileMode.Open);
                _ds.Clear();
                _ds.ReadXml(fs);
                fs.Close();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException(GetType() + ".LoadFromFile", ex);
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

        private void btnXuatXml_Click(object sender, EventArgs e)
        {
            WriteToFile();
        }

        private void btnNhapXml_Click(object sender, EventArgs e)
        {
            LoadFromFile();
        }

    }
}
