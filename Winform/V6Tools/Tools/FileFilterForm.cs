using H;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using V6Tools;

namespace Tools
{
    public partial class FileFilterForm : Form
    {
        public FileFilterForm()
        {
            InitializeComponent();
        }

        Setting Setting = new Setting("Setting.ini");
        //private readonly FolderBrowserDialog folderBrowser1 = new FolderBrowserDialog();
        //private readonly FolderBrowserDialog folderBrowser2 = new FolderBrowserDialog();
        //private readonly FolderBrowserDialog folderBrowser3 = new FolderBrowserDialog();
        

        private void btnFolder1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(txtFolder1.Text)) openFileDialog1.InitialDirectory = txtFolder1.Text;

                if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    txtFolder1.Text = Path.GetDirectoryName(openFileDialog1.FileName);
                    GetSource1();
                    Setting.SetSetting(txtFolder1.Name, txtFolder1.Text);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".GetFolder1", ex, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFolder2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(txtFolder2.Text)) openFileDialog2.InitialDirectory = txtFolder2.Text;

                if (openFileDialog2.ShowDialog(this) == DialogResult.OK)
                {
                    txtFolder2.Text = Path.GetDirectoryName(openFileDialog2.FileName);
                    GetSource_2();
                    Setting.SetSetting(txtFolder2.Name, txtFolder2.Text);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".GetFolder2", ex, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFolder3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(txtFolder3.Text)) saveFileDialog3.InitialDirectory = txtFolder3.Text;

                if (saveFileDialog3.ShowDialog(this) == DialogResult.OK)
                {
                    txtFolder3.Text = Path.GetDirectoryName(saveFileDialog3.FileName);
                    Setting.SetSetting(txtFolder3.Name, txtFolder3.Text);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".GetFolder3", ex, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                GetSource1();
                Filter();
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".Filter", ex, "");
                MessageBox.Show(ex.Message);
            }
        }

        private List<MyFileInfo> SourceFiles_1 = null;
        private List<MyFileInfo> SourceFiles_2 = null;
        private List<MyFileInfo> FilterFiles = null;
        private List<MyFileInfo> CopyFiles = null;

        private void GetSource1()
        {
            try
            {
                SourceFiles_1 = new List<MyFileInfo>();
                var files = Directory.GetFiles(txtFolder1.Text, "*.*", SearchOption.TopDirectoryOnly);
                foreach (string file in files)
                {
                    MyFileInfo fi = new MyFileInfo(file);
                    //if (fi.LastWriteTime.Date >= dateFrom.Value.Date && fi.LastWriteTime.Date <= dateTo.Value.Date)
                    {
                        SourceFiles_1.Add(fi);
                    }
                }

                listBox1.DataSource = SourceFiles_1;
                
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".GetSource", ex, "");
                MessageBox.Show("GetSource error: " + ex.Message);
            }
        }

        private void GetSource_2()
        {
            try
            {
                SourceFiles_2 = new List<MyFileInfo>();
                var files = Directory.GetFiles(txtFolder2.Text, "*.*", SearchOption.TopDirectoryOnly);
                foreach (string file in files)
                {
                    MyFileInfo fi = new MyFileInfo(file);
                    //if (fi.LastWriteTime.Date >= dateFrom.Value.Date && fi.LastWriteTime.Date <= dateTo.Value.Date)
                    {
                        SourceFiles_2.Add(fi);
                    }
                }

                listBox3.DataSource = SourceFiles_2;

            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".GetSource", ex, "");
                MessageBox.Show("GetSource error: " + ex.Message);
            }
        }

        private void Filter()
        {
            try
            {
                FilterFiles = new List<MyFileInfo>();
                foreach (MyFileInfo file in SourceFiles_1)
                {
                    if (file.fileInfo.LastWriteTime.Date >= dateFrom.Value.Date
                        && file.fileInfo.LastWriteTime.Date <= dateTo.Value.Date
                        && file.fileInfo.Name.ToUpper().Contains(txtFilter.Text.ToUpper()))
                    {
                        FilterFiles.Add(file);
                    }
                }

                listBox2.DataSource = FilterFiles;
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".Filter", ex, "");
                MessageBox.Show("Filter error: " + ex.Message);
            }
        }

        private void Copy_if_newer()
        {
            try
            {
                CopyFiles = new List<MyFileInfo>();
                foreach (MyFileInfo file in SourceFiles_2)
                {
                    
                    if (HaveInFilter_andNewer(file))
                    {
                        string desFileName = Path.Combine(txtFolder3.Text, file.fileInfo.Name);
                        File.Copy(file.fileInfo.FullName, desFileName, true);
                        CopyFiles.Add(file);
                    }
                }

                listBox4.DataSource = CopyFiles;
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".Copy", ex, "");
                MessageBox.Show("Copy error: " + ex.Message);
            }
        }

        private bool HaveInFilter_andNewer(MyFileInfo file)
        {
            try
            {
                foreach (MyFileInfo item in FilterFiles)
                {
                    if (item.fileInfo.Name.ToUpper() == file.fileInfo.Name.ToUpper())
                    {
                        if (file.fileInfo.LastWriteTime > item.fileInfo.LastWriteTime) return true;
                        else return false;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return false;
        }

        private void FileFilterForm_Load(object sender, EventArgs e)
        {
            Setting._autoSave = true;
            foreach (Control control in this.Controls)
            {
                string s = Setting.GetSetting(control.Name);
                if (s != "") control.Text = s;
            }
        }

        public class MyFileInfo
        {
            public FileInfo fileInfo = null;
            public MyFileInfo(string filePath)
            {
                fileInfo = new FileInfo(filePath);
            }
            public override string ToString()
            {
                if (fileInfo == null) return "";
                return fileInfo.Name;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(txtFolder3.Text)
                    && MessageBox.Show("Xóa hết file trong thư mục [" + txtFolder3.Text + "] ?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    System.IO.DirectoryInfo di = new DirectoryInfo(txtFolder3.Text);
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    
                    listBox4.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".Clear", ex, "");
                MessageBox.Show("Clear error: " + ex.Message);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                GetSource_2();
                Copy_if_newer();
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".Copy", ex, "");
                MessageBox.Show(ex.Message);
            }
        }
    }

    
}


