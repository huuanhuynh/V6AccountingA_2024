using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using V6Tools;

namespace V6Controls.Forms.DanhMuc.Add_Edit.Albc
{
    public partial class XmlEditorForm : V6Form
    {
        public XmlEditorForm()
        {
            InitializeComponent();
        }

        private readonly Control Text_control;
        private DataSet _ds;
        private string _tableName = "Table0";
        private readonly string _file_xml;
        private string[] _columns;

        /// <summary>
        /// Khởi tạo form chỉnh sửa xml.
        /// </summary>
        /// <param name="textControl">Control nhận nội dung sau khi chỉnh sửa.</param>
        /// <param name="file_xml">Tên file xuất nhập</param>
        /// <param name="tableName">Tên bảng dữ liệu</param>
        /// <param name="columns">Các cột cần phải có, null = type,key,content</param>
        public XmlEditorForm(Control textControl, string file_xml, string tableName, string[] columns)
        {
            InitializeComponent();
            Text_control = textControl;
            _file_xml = file_xml;
            _tableName = tableName;
            _columns = columns ?? new []{"type","key","content"};
            
            Read();
        }

        private void Read()
        {
            try
            {
                _ds = new DataSet("DataSet");
                _ds.ReadXml(new StringReader(Text_control.Text));
                if (_ds.Tables.Count > 0)
                {
                    var table = _ds.Tables[0];
                    foreach (string column in _columns)
                    {
                        if (!table.Columns.Contains(column))
                        {
                            table.Columns.Add(column, typeof(string));
                        }
                    }
                    
                    _ds.DataSetName = "Dataset";
                    _ds.Tables[0].TableName = _tableName;
                    dataGridView1.DataSource = _ds.Tables[0];
                }
            }
            catch
            {
                if (_ds.Tables.Count == 0)
                {
                    _ds.Tables.Add(new DataTable(_tableName));
                }
                var table = _ds.Tables[0];
                foreach (string column in _columns)
                {
                    if (!table.Columns.Contains(column))
                    {
                        table.Columns.Add(column, typeof(string));
                    }
                }

                if (table.Rows.Count == 0)
                {
                    var newRow = table.NewRow();
                    newRow[0] = "NewRow";
                    //newRow[1] = "firstCell_using";
                    //newRow[2] = "A4_method";
                    table.Rows.Add(newRow);
                }
                dataGridView1.DataSource = _ds.Tables[0];
            }
        }

        private DataSet CloneWithEncrype_yn(DataSet ds)
        {
            var dsc = ds.Clone();
            foreach (DataTable table in dsc.Tables)
            {
                if (table.Columns.Contains("Value") && table.Columns.Contains("Encrype_yn"))
                {
                    foreach (DataRow row in table.Rows)
                    {

                        string yn = row["Encrype_yn"].ToString().Trim();
                        if (yn == "1")
                        {
                            string value = row["Value"].ToString();
                            if (value != "" && UtilityHelper.DeCrypt(value.Trim()) == "")
                            {
                                row["Value"] = UtilityHelper.EnCrypt(value);
                            }
                        }
                    }
                }
            }
            return dsc;
        }

        private void Write()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                TextWriter tw = new StringWriter(sb);
                CloneWithEncrype_yn(_ds).WriteXml(tw);
                Text_control.Text = sb.ToString();
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
                if (saveFile.ShowDialog(this) == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(saveFile.FileName)) return;
                    FileStream fs = new FileStream(saveFile.FileName, FileMode.Create);
                    CloneWithEncrype_yn(_ds).WriteXml(fs);
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
                var openFile = V6ControlFormHelper.ChooseOpenFile(this, "Xml|*.xml");
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
