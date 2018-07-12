using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using V6Tools;
using V6Tools.V6Reader;

namespace Tools
{
    public partial class FormFilterTextFiles : Form
    {
        public FormFilterTextFiles()
        {
            InitializeComponent();
        }

        private void GetFolder()
        {
            try
            {
                FolderBrowserDialog f = new FolderBrowserDialog
                    {
                        SelectedPath = txtFolder.Text,
                    };

                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    txtFolder.Text = f.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".GetFolder", ex, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadFiles()
        {
            try
            {
                var files = Directory.GetFiles(txtFolder.Text, txtExt.Text == "" ? "*.*" : "*." + txtExt.Text);
                List<MyFileInfo> listFileInfo = new List<MyFileInfo>();
                foreach (string path in files)
                {
                    var info = new MyFileInfo(path);
                    listFileInfo.Add(info);
                }
                listBox1.DataSource = listFileInfo;
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".LoadFiles", ex, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void Filter()
        {
            try
            {
                listBox2.Items.Clear();
                foreach (object item in listBox1.Items)
                {
                    var myFileInfo = item as MyFileInfo;
                    if (myFileInfo != null)
                    {
                        if (Check(myFileInfo))
                        {
                            listBox2.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".Filter", ex, "");
                MessageBox.Show(ex.Message);
            }
        }

        private bool Check(MyFileInfo myFileInfo)
        {
            if (txt11.Text != "" && !myFileInfo.Text.Contains(txt11.Text)) return false;
            if (txt12.Text != "" && !myFileInfo.Text.Contains(txt12.Text)) return false;
            if (txt13.Text != "" && !myFileInfo.Text.Contains(txt13.Text)) return false;
            
            if (txt01.Text != "" && myFileInfo.Text.Contains(txt01.Text)) return false;
            if (txt02.Text != "" && myFileInfo.Text.Contains(txt02.Text)) return false;
            if (txt03.Text != "" && myFileInfo.Text.Contains(txt03.Text)) return false;
            
            return true;
        }


        private void FormFilterTextFiles_Load(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            GetFolder();
            LoadFiles();
        }

        private void txtExt_Leave(object sender, EventArgs e)
        {
            LoadFiles();
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private class MyFileInfo
        {
            public MyFileInfo(string path)
            {
                FullPath = path;
            }

            /// <summary>
            /// Lấy tất cả thông tin MyFileInfos
            /// </summary>
            private void GetAllInfos()
            {
                try
                {
                    Name = Path.GetFileName(_fullPath);
                    Text = TextFile.ToString(_fullPath);
                }
                catch (Exception)
                {

                }
            }

            public string FullPath
            {
                get
                {
                    return _fullPath;
                }
                set
                {
                    _fullPath = value;
                    GetAllInfos();
                }
            }



            private string _fullPath = null;

            public string Name;
            public string Text;

            public override string ToString()
            {
                return Name;
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBox2.SelectedItem != null)
                {
                    richView.Text = ((MyFileInfo)listBox2.SelectedItem).Text;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".listBox2_SelectedIndexChanged", ex, "");
                MessageBox.Show(ex.Message);
            }
        }
    }
}
