using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using V6Tools.V6Convert;
using V6Tools.V6Export;
using V6Tools.V6Reader;

namespace V6Tools.V6Convert
{
    public partial class FormConvertExcel : Form
    {
        public FormConvertExcel()
        {
            InitializeComponent();
        }

        private void FormConvertExcel_Resize(object sender, EventArgs e)
        {
            int w = Width / 2 - 30;
            tabControl1.Width = w;
            tabControl2.Left = Width/2;
            tabControl2.Width = w;
            button2.Left = Width / 2 - button2.Width / 2;
        }

        private void FormConvertExcel_Load(object sender, EventArgs e)
        {

        }
        
        DataSet ds1, ds2;

        private string openFile = "";
        private void LoadExcel()
        {
            try
            {
                OpenFileDialog o = new OpenFileDialog();
                o.Filter = "Excel files|*.xls;*.xlsx";
                if(o.ShowDialog(this) == DialogResult.OK)
                {
                    openFile = o.FileName;
                    var extension = Path.GetExtension(o.FileName);
                    if (extension != null)
                    {
                        string ext = extension.ToLower();
                        if(ext.StartsWith(".xls"))
                        {
                            ds1 = ExcelFile.ToDataSet(o.FileName);
                            ViewDataSetToTabControl(tabControl1, ds1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodBase.GetCurrentMethod().Name + "\n" + ex.Message);
            }
        }

        private void ViewDataSetToTabControl(TabControl tabControl, DataSet ds)
        {
            try
            {
                tabControl.TabPages.Clear();
                if (ds != null)
                {
                    foreach (DataTable table in ds.Tables)
                    {
                        TabPage page = new TabPage(table.TableName);
                        tabControl.TabPages.Add(page);
                        DataGridView grid = new DataGridView();
                        grid.Dock = DockStyle.Fill;
                        page.Controls.Add(grid);
                        grid.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodBase.GetCurrentMethod().Name + "\n" + ex.Message);
            }
        }

        private void ConverExcel()
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

                
                SaveFileDialog d = new SaveFileDialog();
                d.Filter = "Excel files|*.xls;*.xlsx";

                if (d.ShowDialog(this) == DialogResult.OK)
                {
                    string saveFile = d.FileName;
                    ds2 = Excel_File.ChangeCode(openFile, from, to, saveFile);
                    ViewDataSetToTabControl(tabControl2, ds2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetType() + ".ConverTable: " + ex.Message);
            }
        }

        private void SaveTableAs()
        {
            try
            {
                if (ds2 != null)
                {
                    DataGridView currentGridview2 = tabControl2.TabPages[tabControl2.SelectedIndex].Controls[0] as DataGridView;
                    DataTable currentTable2 = currentGridview2.DataSource as DataTable;
                    
                    SaveFileDialog o = new SaveFileDialog();
                    o.Filter = "All|*.*|Excel|*.xls;*.xlsx|Xml|*.xml";
                    if (o.ShowDialog(this) == DialogResult.OK)
                    {
                        string ext = Path.GetExtension(o.FileName).ToLower();
                        bool no = false;
                        if (ext.StartsWith(".xls"))
                        {
                            var setting = new ExportExcelSetting();
                            setting.data = currentTable2;
                            setting.saveFile = o.FileName;
                            setting.title = "";
                            ExportData.ToExcel(setting);
                        }
                        else if (ext == ".dbf")
                        {
                            ExportData.ToDbfFile(currentTable2, o.FileName);
                        }
                        else if (ext == ".txt")
                        {
                            ExportData.ToTextFile(currentTable2, o.FileName);
                        }
                        else if (ext == ".xml")
                        {
                            ExportData.ToXmlFile(currentTable2, o.FileName);
                        }
                        else
                        {
                            no = true;
                            MessageBox.Show("Chưa hỗ trợ " + ext);
                            ExportData.ToTextFile(currentTable2, o.FileName);
                        }
                        if (!no) MessageBox.Show("Xong.");
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu kết quả.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetType() + ".SaveAs: " + ex.Message);
            }
        }

        private void btnLoadExcel_Click(object sender, EventArgs e)
        {
            LoadExcel();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConverExcel();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveTableAs();
        }
    }
}
