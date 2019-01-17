using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using H;
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

            public string FoundLine { get; set; }

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
        Setting Setting = new Setting("Setting.ini");
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

            bool useHead = false, useTail = false;
            string headText = "", tailText = "";
            var searchText = txt11.Text;
            if (searchText.StartsWith("[A]"))
            {
                useHead = true;
                searchText = searchText.Substring(3);
            }
            if (searchText.EndsWith("[B]"))
            {
                useTail = true;
                searchText = searchText.Substring(0, searchText.Length - 3);
            }

            if (searchText != "" &&
                (fileText.IndexOf(searchText, o) < 0 ||
                 (chkx211.Checked && fileText.IndexOf(searchText, o) == fileText.LastIndexOf(searchText, o))))
            {
                return false;
            }
            else
            {
                int searchTextIndex0 = 0;
                while (searchTextIndex0 > -1)
                {
                    searchTextIndex0 = fileText.IndexOf(searchText, searchTextIndex0 + searchText.Length, o);

                    // Lấy head
                    if (useHead)
                    {
                        headText = "";
                        int searchTextIndexA = searchTextIndex0 - 1;
                        while (searchTextIndexA > 0 && fileText[searchTextIndexA] != '\n')
                        {
                            headText = fileText[searchTextIndexA] + headText;
                            searchTextIndexA--;
                        }
                        headText = headText.Trim();
                    }

                    if (useTail)
                    {
                        tailText = "";
                        int searchTextIndexB = searchTextIndex0 + searchText.Length;
                        while (fileText[searchTextIndexB] != '\n')
                        {
                            tailText = tailText + fileText[searchTextIndexB];
                            searchTextIndexB++;
                        }
                        tailText = tailText.Trim();
                    }

                    //Check headText and tailText
                    if (useHead && txtAContains.Text != "")
                    {
                        if (!headText.Contains(txtAContains.Text)) continue;
                    }
                    if (useTail && txtBContains.Text != "")
                    {
                        if (!tailText.Contains(txtBContains.Text)) continue;
                    }

                    try
                    {
                        // Lấy dòng
                        int endLineIndex = fileText.IndexOf("\n", searchTextIndex0, o);
                        string line = headText +
                                      fileText.Substring(searchTextIndex0, endLineIndex - searchTextIndex0 - 1);
                        myFileInfo.FoundLine = line;
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (txt12.Text != "" &&
                        (fileText.IndexOf(txt12.Text, o) < 0 ||
                         (chkx212.Checked && fileText.IndexOf(txt12.Text, o) == fileText.LastIndexOf(txt12.Text, o))))
                        continue;
                    if (txt13.Text != "" &&
                        (fileText.IndexOf(txt13.Text, o) < 0 ||
                         (chkx213.Checked && fileText.IndexOf(txt13.Text, o) == fileText.LastIndexOf(txt13.Text, o))))
                        continue;

                    searchText = txt01.Text;
                    if (useHead) searchText = searchText.Replace("[A]", headText);
                    if (useTail) searchText = searchText.Replace("[B]", tailText);

                    // Kiểm tra không chứa, nếu có chứa là sai.
                    if (txt01.Text != "" && (fileText.IndexOf(searchText, o) >= 0)) continue;
                    if (txt02.Text != "" && (fileText.IndexOf(txt02.Text, o) >= 0)) continue;
                    if (txt03.Text != "" && (fileText.IndexOf(txt03.Text, o) >= 0)) continue;
                    //myFileInfo.FoundInfos = fileText.IndexOf()
                    // All pass;
                    return true;
                }
                return false;
            }
        }

        private void FormFilterTextFiles_Load(object sender, EventArgs e)
        {
            try
            {
                Setting._autoSave = true;
                txtExt.Leave += txtExt_Leave;
                foreach (Control control in this.Controls)
                {
                    string s = Setting.GetSetting(control.Name);
                    if (s != "") control.Text = s;
                }
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
                    MyFileInfo fi = (MyFileInfo) listBox2.SelectedItem;
                    richFoundInfos.Text = fi.FoundLine;
                    richView.Text = fi.Text;
                    lblFilePath.Text = fi.FullPath;
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

        private void txtFolder_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtFolder_Leave(object sender, EventArgs e)
        {
            Setting.SetSetting(((Control)sender).Name, ((Control)sender).Text);
        }

        private void FormFilterTextFiles_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

    }

    
}
