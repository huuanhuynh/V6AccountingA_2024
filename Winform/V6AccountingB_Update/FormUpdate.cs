using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using V6Tools;

namespace V6AccountingB_Update
{
    public partial class FormUpdate : Form
    {
        public string ftp_folder;
        public string update_available_file;
        public FormUpdate()
        {
            InitializeComponent();
        }

        private void FormUpdate_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Stop for debug.");
            UpdateThread();
        }

        private void UpdateThread()
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 500;
            timer.Tick += timer_Tick;
            flagDoUpdateFinish = false;
            flagDoUpdateSuccess = false;

            new Thread(DoUpdate)
            {
                IsBackground = true
            }
            .Start();

            timer.Start();
        }

        int _percent = 0;
        bool flagDoUpdateFinish = false;
        bool flagDoUpdateSuccess = false;
        string status_text = "";
        private void DoUpdate()
        {
            try
            {
                var _setting = new H.Setting(Path.Combine(Program.StartupFolder, "Setting.ini"));
                // Download update.7z
                V6IOInfo info2 = new V6IOInfo()
                {
                    FileName = "update.7z",
                    FTP_IP = _setting.GetSetting("FTP_IP"),
                    FTP_USER = _setting.GetSetting("FTP_USER"),
                    FTP_EPASS = _setting.GetSetting("FTP_EPASS"),
                    FTP_SUBFOLDER = ftp_folder,
                    LOCAL_FOLDER = Program.StartupFolder// Program.V6SoftLocalAppData_Directory,
                };
                status_text += "\r\nDownload update file.";
                _percent = 5;
                bool copy = V6FileIO.CopyFromVPN(info2);
                if (copy) // && File.Exist
                {
                    _percent = 40;
                    status_text += "\r\nDownload update file ok.";
                    // Giải nén
                    status_text += "\r\nExtracting...";
                    string update_zipfile = Path.Combine(Program.StartupFolder, info2.FileName);
                    //V67z.Unzip(update_zipfile, Application.StartupPath);// code không chạy.
                    V67z.Run7z_Unzip(update_zipfile, Path.GetDirectoryName(update_zipfile), true);
                    _percent = 50;
                    // Thực hiện update (copy and replace). // log
                    int count = 0;
                    var update_available = File.ReadAllLines(update_available_file);
                    List<string> updated_line_list = new List<string>();
                    string[] updated_lines = { };
                    string updated_txt_filename = Path.Combine(Program.StartupFolder, "updated.txt");
                    if (File.Exists(updated_txt_filename)) updated_lines = File.ReadAllLines(updated_txt_filename);
                    var updated_dic = new SortedDictionary<string, string>();
                    foreach (string line in updated_lines)
                    {
                        if (line.StartsWith(";") || line.Trim() == "") continue;
                        var ss = line.Split(':');
                        if (ss.Length == 2)
                        {
                            updated_dic[ss[0]] = ss[1];
                        }
                    }

                    string subfolder = "";
                    foreach (string line in update_available)
                    {
                        try
                        {
                            if (line.StartsWith(";")) goto cont;
                            if (!line.Contains(":"))
                            {
                                subfolder = line + "\\";
                                goto cont;
                            }
                            var ss = line.Split(':');
                            string from_file = Path.Combine(info2.LOCAL_FOLDER, subfolder + ss[0]);
                            string to_file = Path.Combine(Program.StartupFolder, ss[0]);
                            if (File.Exists(to_file))
                            {
                                File.Delete(to_file);
                            }
                            File.Copy(from_file, to_file);
                            //updated_files.Add(line);
                            updated_dic[ss[0]] = ss[1];
                            status_text += "\r\nUpdated file: " + to_file;
                        }
                        catch(Exception ex)
                        {
                            Logger.WriteExLog("DoUpdate_File.Copy", ex, null, "V6UpdateLog");
                        }

                    cont:
                        count++;
                        _percent = 50 + count * 50 / update_available.Length;
                    }

                    foreach (KeyValuePair<string, string> item in updated_dic)
                    {
                        updated_line_list.Add(item.Key + ":" + item.Value);
                    }

                    File.WriteAllLines("updated.txt", updated_line_list.ToArray());
                    _percent = 100;
                    flagDoUpdateSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog("DoUpdate_File.Copy", ex, null, "V6UpdateLog");
            }
            flagDoUpdateFinish = true;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (true)
            {
                progressBar1.Value = _percent;
                string newText = status_text;
                status_text = status_text.Substring(newText.Length);
                richTextBox1.AppendText(newText);
                this.Text = "V6AccountingB_Update " + _percent + "%";
            }
            if (flagDoUpdateFinish)
            {
                ((System.Windows.Forms.Timer)sender).Stop();
                if (flagDoUpdateSuccess)
                {
                    richTextBox1.AppendText("\r\nUpdate finish.");
                }
                else
                {
                    richTextBox1.AppendText("\r\nUpdate end fail.");
                }



                ((System.Windows.Forms.Timer)sender).Dispose();
            }
        }


    }
}
