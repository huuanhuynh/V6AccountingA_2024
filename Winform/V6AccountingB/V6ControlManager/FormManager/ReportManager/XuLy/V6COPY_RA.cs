using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SevenZip;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class V6COPY_RA : XuLyBase0
    {
        public V6COPY_RA()
        {
            InitializeComponent();
        }

        public V6COPY_RA(string itemId, string program, string reportProcedure, string reportFile, string text)
            : base(itemId, program, reportProcedure, reportFile, text, true)
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                dateNgay_ct1.Value = dateNgay_ct1.Value.AddMonths(-1);

                bool haveD = false, haveC = false;
                var drives = DriveInfo.GetDrives();
                var dir = "C:\\V6Copy";
                foreach (DriveInfo drive_info in drives)
                {
                    if (drive_info.DriveType == DriveType.Fixed && drive_info.Name.StartsWith("C"))
                    {
                        haveC = true;
                    }
                    else if (drive_info.DriveType == DriveType.Fixed && drive_info.Name.StartsWith("D"))
                    {
                        haveD = true;
                        break;
                    }
                }
                if (haveD)
                {
                    dir = "D:\\V6Copy";
                    
                }
                else if(haveC)
                {
                    dir = "C:\\V6Copy";
                }
                if (haveD || haveC)
                {
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    txtFileName.Text = Path.Combine(dir, DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".7z");
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".V6COPY_RA Init: " + ex.Message);
            }
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("Sao chép số liệu ra.");
        }

        protected override void Nhan()
        {
            try
            {
                if (_executing)
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.Executing);
                    return;
                }

                if (txtDanhSachDonVi.Text.Trim() == "")
                {
                    this.ShowInfoMessage("Chưa chọn đơn vị nào!");
                    btnChonDanhSachDonVi.PerformClick();
                    return;
                }
                if (!chkDanhMuc.Checked && !chkDuLieu.Checked && !chkSoDuVaLuyKe.Checked)
                {
                    this.ShowInfoMessage("Chưa chọn loại dữ liệu lưu trữ nào!");
                    return;
                }
                files = new List<string>();
                Control.CheckForIllegalCrossThreadCalls = false;
                Thread tRunAll = new Thread(RunAll);
                tRunAll.IsBackground = true;
                tRunAll.Start();

                Timer timerRunAll = new Timer();
                timerRunAll.Interval = 500;
                timerRunAll.Tick += timerRunAll_Tick;
                _success = false;
                _executing = true;
                timerRunAll.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Nhan: " + ex.Message);
            }
        }
        private void timerRunAll_Tick(object sender, EventArgs e)
        {
            if (_success)
            {
                ((Timer)sender).Stop();
                btnNhan.Image = btnNhanImage;
                try
                {
                    DoAfterExecuteSuccess();
                    V6ControlFormHelper.ShowMainMessage("V6CopyRa Thực hiện xong!\r\n" + _message);
                    _success = false;
                }
                catch (Exception ex)
                {
                    ((Timer)sender).Stop();
                    _success = false;
                    this.ShowErrorMessage(GetType() + ".TimerView" + ex.Message, ex.Source);
                }
            }
            else if (_executing)
            {
                btnNhan.Image = waitingImages.Images[ii];
                ii++;
                if (ii >= waitingImages.Images.Count) ii = 0;
            }
            else
            {
                ((Timer)sender).Stop();
                btnNhan.Image = btnNhanImage;
                this.ShowErrorMessage(_error);
            }
        }

        private string _dir = "";
        private string _tempDir = "";
        private string _saveZipFile = "";
        private string _error = "";
        private void RunAll()
        {
            try
            {
                _message = "";
                _saveZipFile = txtFileName.Text;
                _dir = Path.GetDirectoryName(_saveZipFile);
                _tempDir = _dir + "\\Temp";
                if (Directory.Exists(_tempDir))
                {
                    //Làm sạch thư mục tạm.
                    Directory.Delete(_tempDir, true);
                }
                if (!Directory.Exists(_tempDir)) Directory.CreateDirectory(_tempDir);
                
                if (chkDanhMuc.Checked) ExportDanhMuc();
                if (chkDuLieu.Checked) ExportDuLieu();
                if (chkSoDuVaLuyKe.Checked) ExportSoDuVaLuyKe();
                
                RunV67z();
                //RunSevenZipFolder();
                
                _executing = false;
                _success = true;
            }
            catch (Exception ex)
            {
                _error = _message + " " + ex.Message;
                _success = false;
                _executing = false;
            }
        }


        private void ExportDataSet(DataSet ds, string key)
        {
            var tempDirCurrent = Path.Combine(_tempDir, key);
            if (!Directory.Exists(tempDirCurrent))
            {
                Directory.CreateDirectory(tempDirCurrent);
            }
            var saveFile = "";

            if (ds.Tables.Count > 1)
            {
                var tblList = ds.Tables[0];
                saveFile = Path.Combine(tempDirCurrent, key + ".xls");
                V6Tools.V6Export.ExportData.ToExcel(tblList, saveFile, "");
                files.Add(saveFile);

                foreach (DataRow row in tblList.Rows)
                {
                    //Xuất excel từng bảng dữ liệu
                    var stt = V6Tools.V6Convert.ObjectAndString.ObjectToInt(row["STT"]);
                    //var ma_file = V6Tools.V6Convert.ObjectAndString.ObjectToString(row["MA_FILE"]);
                    var xls_file = V6Tools.V6Convert.ObjectAndString.ObjectToString(row["XLS_FILE"]);
                    var data1 = ds.Tables[stt];
                    saveFile = Path.Combine(tempDirCurrent, xls_file + ".xls");
                    V6Tools.V6Export.ExportData.ToExcel(data1, saveFile, "");
                    files.Add(saveFile);
                }
            }
        }
        private void ExportDanhMuc()
        {
            _message += "\r\nDM";
            var ds = RunProcV6CopyRa("DM");
            _message += " ds.Count: " + ds.Tables.Count;
            ExportDataSet(ds, "DM");
            _message += " CompleteExport ";
        }

        private void ExportDuLieu()
        {
            _message += "\r\nDữ liệu: VC,BC,HB,CT:";
            var types = "VC,BC,HB,CT".Split(',');
            foreach (string type in types)
            {
                _message += "\r\n" + type;
                var ds = RunProcV6CopyRa(type);
                _message += " ds.Count: " + ds.Tables.Count;
                ExportDataSet(ds, type);
                _message += " CompleteExport ";
            }
            //var ds = RunProcV6CopyRa("VC");
            //ExportDataSet(ds, "VC");
            //ds = RunProcV6CopyRa("BC");
            //ExportDataSet(ds, "BC");
            //ds = RunProcV6CopyRa("HB");
            //ExportDataSet(ds, "HB");
            //ds = RunProcV6CopyRa("CT");
            //ExportDataSet(ds, "CT");
        }
        private void ExportSoDuVaLuyKe()
        {
            _message += "\r\nLũy kế: SD,LK:";
            var types = "SD,LK".Split(',');
            foreach (string type in types)
            {
                _message += "\r\n" + type;
                var ds = RunProcV6CopyRa(type);
                _message += " ds.Count: " + ds.Tables.Count;
                ExportDataSet(ds, type);
                _message += " CompleteExport ";
            }
            //var ds = RunProcV6CopyRa("SD");
            //ExportDataSet(ds, "SD");
            //ds = RunProcV6CopyRa("LK");
            //ExportDataSet(ds, "LK");
        }

        private DataSet RunProcV6CopyRa(string type)
        {
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@Type", type), 
                new SqlParameter("@Ngay_ct1", dateNgay_ct1.Value.Date), 
                new SqlParameter("@Ngay_ct2", dateNgay_ct2.Value.Date), 
                new SqlParameter("@Ma_dvcs", txtDanhSachDonVi.Text), 
                new SqlParameter("@Ws_id", V6Options.V6OptionValues["M_WS_ID"]), 
            };
            var ds = V6BusinessHelper.ExecuteProcedure("V6CopyRa", plist);
            return ds;
        }

        private List<string> files; 
        private void RunV67z()
        {
            if (File.Exists(_saveZipFile)) File.Delete(_saveZipFile);
            //V67z.ZipFiles(ExportDataSet(ds1, "VC");, true, files.ToArray());
            V67z.Run7z("a " + _saveZipFile + " " + _tempDir + " -aoa ");
            FileInfo fi = new FileInfo(_saveZipFile);
            var s = 0;
            while (V6FileIO.IsFileLocked(fi))
            {
                s++;
                if (s == 3600) return;
                Thread.Sleep(1000);
            }
            //Xóa file tạm
            Directory.Delete(_tempDir, true);
        }

        private void RunSevenZipFolder()
        {
            if (File.Exists(_saveZipFile)) File.Delete(_saveZipFile);

            Class1.CompressFileLZMA(_tempDir, _saveZipFile);
            //Class1.CompressFolderLZMA(_tempDir, _saveZipFile);
            
            FileInfo fi = new FileInfo(_saveZipFile);
            var s = 0;
            while (V6FileIO.IsFileLocked(fi))
            {
                s++;
                if (s == 3600) return;
                Thread.Sleep(1000);
            }
            //Xóa file tạm
            Directory.Delete(_tempDir, true);
        }

        private void ChonFile()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "7zip|*.7z|Rar|*.rar";
            if (save.ShowDialog(this) == DialogResult.OK)
            {
                txtFileName.Text = save.FileName;
            }
        }

        private void ChonDanhSachDonVi()
        {
            txtDanhSachDonVi.Lookup(LookupMode.Multi);
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            ChonFile();
        }
        
        private void btnChonDanhSachDonVi_Click(object sender, EventArgs e)
        {
            ChonDanhSachDonVi();
        }
        
    }
}
