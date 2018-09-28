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
                    //
                }
            }

            // ReSharper disable once MemberCanBePrivate.Local
            public string FullPath
            {
                // ReSharper disable once UnusedMember.Local
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
            private string _fullPath;
            public string Name;
            public string Text;

            public override string ToString()
            {
                return Name;
            }
        }

        public FormFilterTextFiles()
        {
            InitializeComponent();
        }

        private readonly FolderBrowserDialog f = new FolderBrowserDialog();
        private void GetFolder()
        {
            try
            {
                if(Directory.Exists(txtFolder.Text)) f.SelectedPath = txtFolder.Text;

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

        private void LoadFiles(bool subFolder)
        {
            try
            {
                var files = Directory.GetFiles(txtFolder.Text, txtExt.Text == "" ? "*.*" : "*." + txtExt.Text,
                    subFolder ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

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
            var o = chkCase.Checked ? StringComparison.Ordinal : StringComparison.InvariantCultureIgnoreCase;
            var fileText = myFileInfo.Text;
            if (txt11.Text != "" && (fileText.IndexOf(txt11.Text, o) < 0 || (chkx211.Checked && fileText.IndexOf(txt11.Text, o) == fileText.LastIndexOf(txt11.Text, o)))) return false;
            if (txt12.Text != "" && (fileText.IndexOf(txt12.Text, o) < 0 || (chkx212.Checked && fileText.IndexOf(txt12.Text, o) == fileText.LastIndexOf(txt12.Text, o)))) return false;
            if (txt13.Text != "" && (fileText.IndexOf(txt13.Text, o) < 0 || (chkx213.Checked && fileText.IndexOf(txt13.Text, o) == fileText.LastIndexOf(txt13.Text, o)))) return false;

            if (txt01.Text != "" && (fileText.IndexOf(txt01.Text, o) >= 0)) return false;
            if (txt02.Text != "" && (fileText.IndexOf(txt02.Text, o) >= 0)) return false;
            if (txt03.Text != "" && (fileText.IndexOf(txt03.Text, o) >= 0)) return false;
            
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
            LoadFiles(chkSubFolder.Checked);
        }

        private void txtExt_Leave(object sender, EventArgs e)
        {
            LoadFiles(chkSubFolder.Checked);
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            Filter();
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

        private void txtFolder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && Directory.Exists(txtFolder.Text))
            {
                LoadFiles(chkSubFolder.Checked);
            }
        }
    }

    
}
