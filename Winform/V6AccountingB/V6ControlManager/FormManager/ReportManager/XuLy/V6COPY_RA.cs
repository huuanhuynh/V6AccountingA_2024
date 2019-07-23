using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using SevenZip;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;
using V6Tools.V6Export;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class V6COPY_RA : XuLyBase0
    {
        private DateTime dateNgay_ct1_Date;
        private DateTime dateNgay_ct2_Date;
        private string txtDanhSachDonVi_Text = null;
        private readonly string m_ws_id = V6Options.GetValue("M_WS_ID");

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
                dateNgay_ct1.SetValue(dateNgay_ct1.Date.AddMonths(-1));
                GetTxtFileName();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".V6COPY_RA Init: " + ex.Message);
            }
        }

        private void GetTxtFileName()
        {
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
            else if (haveC)
            {
                dir = "C:\\V6Copy";
            }

            if (haveD || haveC)
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                txtFileName.Text = Path.Combine(dir, DateTime.Now.ToString("yyyyMMdd_HHmmss") + (radExcel.Checked ? "_excel" : "_xml") + ".7z");
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

                dateNgay_ct1_Date = dateNgay_ct1.Date;
                dateNgay_ct2_Date = dateNgay_ct2.Date;
                txtDanhSachDonVi_Text = txtDanhSachDonVi.Text;

                files = new List<string>();
                CheckForIllegalCrossThreadCalls = false;
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
                CreateGeneralInfoFile();

                RunV67z();
                
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

        private IDictionary<string, object> generalDictionary = null;
        private void CreateGeneralInfoFile()
        {
            try
            {
                DataTable generalInfoData = new DataTable("GeneralInfo");
                generalDictionary = new Dictionary<string, object>();
                string types = "", checking = "";
                if (chkDanhMuc.Checked)
                {
                    types += "DM";
                    checking += "DM";
                }
                if (chkDuLieu.Checked)
                {
                    if (types.Length > 0)
                    {
                        types += ";";
                        checking += ";";
                    }
                    types += "VC,BC,HB,CT";
                    checking += "DL";
                }
                if (chkSoDuVaLuyKe.Checked)
                {
                    if (types.Length > 0)
                    {
                        types += ";";
                        checking += ";";
                    }
                    types += "SD,LK";
                    checking += "SD";
                }
                generalDictionary["TYPE"] = types;
                generalDictionary["MA_DVCS"] = txtDanhSachDonVi_Text;
                generalDictionary["NGAY_CT1"] = dateNgay_ct1_Date;
                generalDictionary["NGAY_CT2"] = dateNgay_ct2_Date;
                generalDictionary["WS_ID"] = m_ws_id;
                generalDictionary["CHECKING"] = checking;
                generalInfoData.AddRow(generalDictionary, true);
                var generalFile = Path.Combine(_tempDir, "GeneralInfo.xml");
                Data_Table.ToXmlFile(generalInfoData, generalFile);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }

        private void ExportDataSet(DataSet ds, string key)
        {
            var tempDirCurrent = Path.Combine(_tempDir, key);
            if (!Directory.Exists(tempDirCurrent))
            {
                Directory.CreateDirectory(tempDirCurrent);
            }
            
            if (ds.Tables.Count > 1)
            {
                var tblList = ds.Tables[0];
                // replace key in tblList
                var tblList_copy = tblList.Copy();
                foreach (DataRow row in tblList_copy.Rows)
                {
                    row["KEY"] = row["KEY"].ToString()
                        .Replace("@dFrom", "'" + ObjectAndString.ObjectToString(dateNgay_ct1_Date, "yyyyMMdd") + "'")
                        .Replace("@dTo", "'" + ObjectAndString.ObjectToString(dateNgay_ct2_Date, "yyyyMMdd") + "'")
                        .Replace("@Ma_dvcsList", "'" + txtDanhSachDonVi_Text + "'")
                        .Replace("@WsId", "'" + m_ws_id + "'")
                        ;
                }

                if (radExcel.Checked)
                {
                    var saveFile = Path.Combine(tempDirCurrent, key + ".xls");
                    ExportData.ToExcel(tblList_copy, saveFile, "");
                    files.Add(saveFile);

                    foreach (DataRow row in tblList.Rows)
                    {
                        //Xuất excel từng bảng dữ liệu
                        var stt = ObjectAndString.ObjectToInt(row["STT"]);
                        //var ma_file = V6Tools.V6Convert.ObjectAndString.ObjectToString(row["MA_FILE"]).Trim();
                        var xls_file = ObjectAndString.ObjectToString(row["XLS_FILE"]).Trim();
                        var data1 = ds.Tables[stt];
                        saveFile = Path.Combine(tempDirCurrent, xls_file + ".xls");
                        ExportData.ToExcel(data1, saveFile, "");
                        files.Add(saveFile);
                    }
                }
                else
                {
                    var saveFile = Path.Combine(tempDirCurrent, key + ".xml");
                    ExportData.ToXmlFile(tblList_copy, saveFile);
                    files.Add(saveFile);

                    foreach (DataRow row in tblList.Rows)
                    {
                        //Xuất xml từng bảng dữ liệu
                        var stt = ObjectAndString.ObjectToInt(row["STT"]);
                        //var ma_file = V6Tools.V6Convert.ObjectAndString.ObjectToString(row["MA_FILE"]).Trim();
                        var xls_file = ObjectAndString.ObjectToString(row["XLS_FILE"]).Trim();
                        var data1 = ds.Tables[stt];
                        saveFile = Path.Combine(tempDirCurrent, xls_file + ".xml");
                        ExportData.ToXmlFile(data1, saveFile);
                        files.Add(saveFile);
                    }
                }
            }
        }

        /// <summary>
        /// Xuất file dữ liệu chứng từ.
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="key"></param>
        private void ExportDataSetDuLieu(DataSet ds, string key)
        {
            var tempDirCurrent = Path.Combine(_tempDir, key);
            if (!Directory.Exists(tempDirCurrent))
            {
                Directory.CreateDirectory(tempDirCurrent);
            }

            if (ds.Tables.Count > 1)
            {
                var tblList = ds.Tables[0];
                // replace key in tblList
                var tblList_copy = tblList.Copy();
                foreach (DataRow row in tblList_copy.Rows)
                {
                    row["KEY"] = row["KEY"].ToString()
                        .Replace("@dFrom", "'" + ObjectAndString.ObjectToString(dateNgay_ct1_Date, "yyyyMMdd") + "'")
                        .Replace("@dTo", "'" + ObjectAndString.ObjectToString(dateNgay_ct2_Date, "yyyyMMdd") + "'")
                        .Replace("@Ma_dvcsList", "'" + txtDanhSachDonVi_Text + "'")
                        .Replace("@WsId", "'" + m_ws_id + "'")
                        ;
                }

                int data1_count = 0;
                if (radExcel.Checked)
                {
                    var saveFile = Path.Combine(tempDirCurrent, key + ".xls");
                    Data_Table.ToExcelFile(tblList_copy, saveFile);
                    files.Add(saveFile);

                    foreach (DataRow row in tblList.Rows)
                    {
                        //Xuất excel từng bảng dữ liệu
                        var stt = ObjectAndString.ObjectToInt(row["STT"]);
                        var ma_file = ObjectAndString.ObjectToString(row["MA_FILE"]).Trim();
                        var ma_file_ct = ObjectAndString.ObjectToString(row["MA_FILE_CT"]).Trim();
                        var ma_file_ct_list = ObjectAndString.SplitString(ma_file_ct);
                        //var xls_file = ObjectAndString.ObjectToString(row["XLS_FILE"]).Trim();
                        var data1 = ds.Tables[++data1_count];
                        saveFile = Path.Combine(tempDirCurrent, ma_file + ".xls");
                        Data_Table.ToExcelFile(data1, saveFile);
                        files.Add(saveFile);
                        foreach (string file_ct in ma_file_ct_list)
                        {
                            data1 = ds.Tables[++data1_count];
                            saveFile = Path.Combine(tempDirCurrent, file_ct + ".xls");
                            Data_Table.ToExcelFile(data1, saveFile);
                            files.Add(saveFile);
                        }
                    }
                }
                else
                {
                    var saveFile = Path.Combine(tempDirCurrent, key + ".xml");
                    Data_Table.ToXmlFile(tblList_copy, saveFile);
                    files.Add(saveFile);

                    foreach (DataRow row in tblList.Rows)
                    {
                        //Xuất xml từng bảng dữ liệu
                        //var stt = ObjectAndString.ObjectToInt(row["STT"]);
                        var ma_file = ObjectAndString.ObjectToString(row["MA_FILE"]).Trim();
                        var ma_file_ct = ObjectAndString.ObjectToString(row["MA_FILE_CT"]).Trim();
                        var ma_file_ct_list = ObjectAndString.SplitString(ma_file_ct);
                        //var xls_file = ObjectAndString.ObjectToString(row["XLS_FILE"]).Trim();
                        var data1 = ds.Tables[++data1_count];
                        saveFile = Path.Combine(tempDirCurrent, ma_file + ".xml");
                        Data_Table.ToXmlFile(data1, saveFile);
                        files.Add(saveFile);
                        foreach (string file_ct in ma_file_ct_list)
                        {
                            data1 = ds.Tables[++data1_count];
                            saveFile = Path.Combine(tempDirCurrent, file_ct + ".xml");
                            Data_Table.ToXmlFile(data1, saveFile);
                            files.Add(saveFile);
                        }
                    }
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
                ExportDataSetDuLieu(ds, type);
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
                new SqlParameter("@Ngay_ct1", dateNgay_ct1_Date),
                new SqlParameter("@Ngay_ct2", dateNgay_ct2_Date),
                new SqlParameter("@Ma_dvcs", txtDanhSachDonVi_Text),
                new SqlParameter("@Ws_id", m_ws_id),
            };
            var ds = V6BusinessHelper.ExecuteProcedure("V6CopyRa", plist);
            return ds;
        }

        private List<string> files; 
        private void RunV67z()
        {
            if (File.Exists(_saveZipFile)) File.Delete(_saveZipFile);
            V67z.Run7z_Zip(_tempDir, _saveZipFile);
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

        private void radExcel_CheckedChanged(object sender, EventArgs e)
        {
            GetTxtFileName();
        }
        
    }
}
