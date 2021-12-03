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

namespace V6AccountingB_Update
{
    public partial class FormCreateUpdate : Form
    {
        public FormCreateUpdate()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnMakeUpdateFile_Click(object sender, EventArgs e)
        {
            try
            {
                var files = Directory.GetFiles(Program.StartupFolder, "*.*", SearchOption.AllDirectories);

                //var files1 = Directory.GetFiles(Program.StartupFolder, "*.exe");
                //var files2 = Directory.GetFiles(Program.StartupFolder, "*.dll");
                
                List<string> files3 = new List<string>();
                var update_txt_files = new List<string>();
                // dòng đầu tiên ghi tên thư mục trong file update.7z
                update_txt_files.Add(Path.GetFileName(Program.StartupFolder) + (chkAutoUpdate.Checked ? ";1" : ""));

                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    if (fi.Name.ToUpper() == "V6ACCOUNTINGB_UPDATE.EXE") continue;
                    if (fi.Name.ToUpper() == "V6TOOL_ALONE.DLL") continue;
                    if (fi.Name.ToUpper() == "UPDATE.7Z" || fi.Name.ToLower() == "update.txt") continue;
                    string line = string.Format("{0}:{1}", file.Substring(Program.StartupFolder.Length+1), fi.CreationTime.ToString("yyyyMMdd_HHmmss"));
                    update_txt_files.Add(line);
                    files3.Add(file);
                }

                
                File.WriteAllLines("update.txt", update_txt_files.ToArray());
                //V67z.ZipFiles("update.7z", files3.ToArray());
                V67z.Run7z_Zip(Program.StartupFolder, "update.7z", true);

                MessageBox.Show("Create Update finish.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void buttonTestUnzip7z_Click(object sender, EventArgs e)
        {
            try
            {
                //V67z.Unzip("update.7z");
                V67z.Run7z_Unzip("update.7z", Application.StartupPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
    }
}
