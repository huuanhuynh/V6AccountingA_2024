using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Tools.V6Convert;
using V6Tools.V6Export;

namespace V6ControlManager.FormManager.ToolManager
{
    public partial class FormConvertTable : V6Form
    {
        public FormConvertTable()
        {
            InitializeComponent();
        }

        private void FormConvertTable_Resize(object sender, EventArgs e)
        {
            int w = this.Width / 2 - 30;
            dataGridView1.Width = w;
            dataGridView2.Left = this.Width/2;
            dataGridView2.Width = w;
            button2.Left = this.Width / 2 - button2.Width / 2;
        }

        private void FormConvertTable_Load(object sender, EventArgs e)
        {

        }
        DataTable table1, table2;
        private void LoadTable()
        {
            try
            {
                OpenFileDialog o = new OpenFileDialog();
                if(o.ShowDialog(this) == DialogResult.OK)
                {
                    var extension = Path.GetExtension(o.FileName);
                    if (extension != null)
                    {
                        string ext = extension.ToLower();
                        bool no = false;
                        if(ext.StartsWith(".xls"))
                        {
                            table1 = V6Tools.V6Reader.ExcelFile.Sheet1ToDataTable(o.FileName);
                        }
                        else if(ext == ".dbf")
                        {
                            table1 = V6Tools.V6Reader.DbfFile.ToDataTable(o.FileName);
                        }
                        else if(ext == ".txt")
                        {
                            table1 = V6Tools.V6Reader.TextFile.ToTable(o.FileName);
                        }
                        else if(ext == ".csv")
                        {
                            table1 = V6Tools.V6Reader.TextFile.ToTable(o.FileName);
                        }
                        else if (ext == ".xml")
                        {
                            table1 = Data_Table.FromXmlFile(o.FileName);
                        }
                        else
                        {
                            no = true;
                            V6Message.Show("Chưa hỗ trợ " + ext);
                        }
                        if(!no)
                        {
                            dataGridView1.DataSource = table1;
                        }
                    }
                    else
                    {
                        table1 = V6Tools.V6Reader.TextFile.ToTable(o.FileName);
                        dataGridView1.DataSource = table1;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".LoadTable: " + ex.Message);
            }
        }

        private void ConverTable()
        {
            try
            {
                string from = "", to = "U";
                if (radFromABC.Checked) from = "A";
                if (radFromUNI.Checked) from = "U";
                if (radFromVNI.Checked) from = "V";

                if (radToABC.Checked) to = "A";
                if (radToUNI.Checked) to = "U";
                if (radToVNI.Checked) to = "V";

                table2 = V6Tools.V6Convert.Data_Table.ChuyenMaTiengViet(table1, from, to);

                dataGridView2.DataSource = table2;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ConverTable: " + ex.Message);
            }
        }

        private void SaveAs()
        {
            try
            {
                if (table2 != null)
                {
                    SaveFileDialog o = new SaveFileDialog();
                    if (o.ShowDialog(this) == DialogResult.OK)
                    {
                        string ext = Path.GetExtension(o.FileName).ToLower();
                        bool no = false;
                        if (ext.StartsWith(".xls"))
                        {
                            var setting = new ExportExcelSetting();
                            setting.data = table2;
                            setting.saveFile = o.FileName;
                            V6Tools.V6Export.ExportData.ToExcel(setting);
                        }
                        else if (ext == ".dbf")
                        {
                            V6Tools.V6Export.ExportData.ToDbfFile(table2, o.FileName);
                        }
                        else if (ext == ".txt")
                        {
                            V6Tools.V6Export.ExportData.ToTextFile(table2, o.FileName);
                        }
                        else
                        {
                            no = true;
                            V6Message.Show("Chưa hỗ trợ " + ext);
                            V6Tools.V6Export.ExportData.ToTextFile(table2, o.FileName);
                        }
                        if (!no) V6Message.Show("Xong.");
                    }
                }
                else
                {
                    V6Message.Show("Không có dữ liệu kết quả.");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SaveAs: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConverTable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveAs();
        }
    }
}
