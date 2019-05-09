using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6Controls.Controls
{
    public class FileButton : Button
    {
        public FileButton()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FileButton
            // 
            this.Image = global::V6Controls.Properties.Resources.unknow16;
            this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Click += new System.EventHandler(this.FileButton_Click);
            this.ResumeLayout(false);

        }

        public event EventHandler<Event_Args> FileNameChanged;
        protected virtual void OnFileNameChanged(Event_Args e)
        {
            ChangeViewText();
            ChangeIcon();
            var handler = FileNameChanged;
            if (handler != null) handler(this, e);
        }

        
        [DefaultValue(null)]
        public string FileName { get { return _fileName; }
            set
            {
                Event_Args e = new Event_Args();
                e.Mode = FileButtonMode.ChangeFileName;
                e.Sender = this;
                e.OldFileName = _fileName;
                e.NewFileName = value;
                _fileName = value;
                OnFileNameChanged(e);
            } }
        private string _fileName = null;

        public bool ReadOnly { get; set; }

        private void ChangeViewText()
        {
            if (string.IsNullOrEmpty(_fileName))
            {
                Text = "...";
            }
            else
            {
                Text = _fileName;
            }
        }

        private void ChangeIcon()
        {
            var ext = Path.GetExtension(_fileName)??"";
            switch (ext.ToLower())
            {
                case ".doc":
                case ".docx":
                    Image = Properties.Resources.word16;
                    break;
                case ".xls":
                case ".xlsx":
                    Image = Properties.Resources.Excel16;
                    break;
                case ".ppt":
                case ".pps":
                    Image = Properties.Resources.ppt16;
                    break;
                case ".pptx":
                case ".ppsx":
                    Image = Properties.Resources.pptx16;
                    break;
                case ".jpg":
                    Image = Properties.Resources.jpg16;
                    break;
                case ".png":
                    Image = Properties.Resources.png16;
                    break;
                case ".gif":
                case ".bmp":
                    Image = Properties.Resources.image16;
                    break;
                case ".pdf":
                    Image = Properties.Resources.pdf16;
                    break;
                default:
                    Image = Properties.Resources.unknow16;
                    break;
            }
        }

        public enum FileButtonMode
        {
            ChooseFile,
            Clear,
            OpenFile,
            ChangeFileName,
            None
        }
        public class Event_Args : EventArgs
        {
            public Event_Args()
            {
                Mode = FileButtonMode.None;
            }
            public FileButtonMode Mode { get; set; }
            public FileButton Sender { get; set; }
            public string OpenFile { get; set; }
            public string OldFileName { get; set; }
            public string NewFileName { get; set; }
        }
        public event EventHandler<Event_Args> AfterProcess;
        protected virtual void OnAfterProcess(Event_Args e)
        {
            if (ReadOnly) return;
            var handler = AfterProcess;
            if (handler != null) handler(this, e);
        }

        private void FileButton_Click(object sender, EventArgs e)
        {
            Event_Args e1 = new Event_Args();
            e1.Sender = this;
            bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
            if (shift_is_down && !string.IsNullOrEmpty(_fileName))
            {
                ClearFileName();
                e1.Mode = FileButtonMode.Clear;
            }
            else if (!string.IsNullOrEmpty(_fileName))
            {
                e1.OpenFile = OpenFile();
                e1.Mode = FileButtonMode.OpenFile;
            }
            else
            {
                ChooseFileName();
                e1.Mode = FileButtonMode.ChooseFile;
            }
            
            OnAfterProcess(e1);
        }

        private string OpenFile()
        {
            try
            {
                if (string.IsNullOrEmpty(_fileName)) return null;

                var _setting = new H.Setting(Path.Combine(V6Login.StartupPath, "Setting.ini"));
                V6IOInfo info = new V6IOInfo()
                {
                    FileName = _fileName,
                    FTP_IP = _setting.GetSetting("FTP_IP"),
                    FTP_USER = _setting.GetSetting("FTP_USER"),
                    FTP_EPASS = _setting.GetSetting("FTP_EPASS"),
                    FTP_SUBFOLDER = _setting.GetSetting("FTP_V6DOCSFOLDER"),
                    LOCAL_FOLDER = V6Setting.V6SoftLocalAppData_Directory,
                };
                if (V6FileIO.CopyFromVPN(info))
                {
                    string tempFile = Path.Combine(V6Setting.V6SoftLocalAppData_Directory, _fileName);
                    Process.Start(tempFile);
                    return tempFile;
                }
                else
                {
                    V6ControlFormHelper.ShowWarningMessage(V6Text.NotFound + _fileName);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XemFile " + _fileName, ex);
            }
            return null;
        }

        public void ClearFileName()
        {
            if (ReadOnly) return;
            FileName = null;
        }

        public void ChooseFileName()
        {
            if (ReadOnly) return;
            try
            {
                var filePath = V6ControlFormHelper.ChooseOpenFile(this, "All files|*.*");
                if (filePath == null) return;

                var _setting = new H.Setting(Path.Combine(V6Login.StartupPath, "Setting.ini"));
                var info = new V6IOInfo()
                {
                    FileName = filePath,
                    FTP_IP = _setting.GetSetting("FTP_IP"),
                    FTP_USER = _setting.GetSetting("FTP_USER"),
                    FTP_EPASS = _setting.GetSetting("FTP_EPASS"),
                    FTP_SUBFOLDER = _setting.GetSetting("FTP_V6DOCSFOLDER"), // + "/ChildFolder"
                };
                V6FileIO.CopyToVPN(info);
                FileName = Path.GetFileName(filePath); //  // + "/ChildFolder" + fileName
                //txtFileName.Text = Path.GetFileName(filePath);
                //var data = new SortedDictionary<string, object> { { FIELD, txtFileName1.Text } };
                //var keys = new SortedDictionary<string, object> { { "MA_KH", txtMaKH.Text } };
                //var result = V6BusinessHelper.UpdateTable(V6TableName.Alkhct1.ToString(), data, keys);
                //if (result == 1)
                //{
                //    ShowTopLeftMessage(V6Text.Updated + FIELD);
                //}
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ChooseFileName", ex);
            }
        }

        
    }
}
