using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
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
    public partial class V6COPY_RA_ALL : XuLyBase0
    {
        private DateTime dateNgay_ct1_Date;
        private DateTime dateNgay_ct2_Date;
        private string txtDanhSachDonVi_Text = null;
        private readonly string m_ws_id = V6Options.GetValue("M_WS_ID");

        public V6COPY_RA_ALL()
        {
            InitializeComponent();
        }

        public V6COPY_RA_ALL(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
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
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = "Sao chép số liệu ra.";
            }

            V6ControlFormHelper.SetStatusText2(text, id);
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

                dateNgay_ct1_Date = dateNgay_ct1.Date;
                dateNgay_ct2_Date = dateNgay_ct2.Date;
                txtDanhSachDonVi_Text = txtDanhSachDonVi.Text;

                files = new List<string>();
                CheckForIllegalCrossThreadCalls = false;
                var tRunAll = new Thread(RunAll) {IsBackground = true};
                tRunAll.Start();

                Timer timerRunAll = new Timer {Interval = 500};
                timerRunAll.Tick += timerRunAll_Tick;
                _executesuccess = false;
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
            if (_executesuccess)
            {
                ((Timer)sender).Stop();
                btnNhan.Image = btnNhanImage;
                try
                {
                    DoAfterExecuteSuccess();
                    SetStatusText("V6CopyRa Thực hiện xong!\r\n" + _message);
                    V6ControlFormHelper.ShowMainMessage(V6Text.Finish + " V6CopyRaAll\r\n" + _message);
                    _executesuccess = false;
                }
                catch (Exception ex)
                {
                    ((Timer)sender).Stop();

                    _executesuccess = false;
                    this.ShowErrorMessage(GetType() + ".TimerView" + ex.Message, ex.Source);
                }
            }
            else if (_executing)
            {
                SetStatusText(_message);
                btnNhan.Image = waitingImages.Images[ii++];
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

                ExportAll();
                //if (chkDanhMuc.Checked) ExportDanhMuc();
                //if (chkDuLieu.Checked) ExportDuLieu();
                //if (chkSoDuVaLuyKe.Checked) ExportSoDuVaLuyKe();
                CreateGeneralInfoFile();

                RunV67z();
                _executing = false;
                _executesuccess = true;
            }
            catch (Exception ex)
            {
                _error = _message + " " + ex.Message;
                _executesuccess = false;
                _executing = false;
            }
        }

        private void CreateGeneralInfoFile()
        {
            try
            {
                string m_ws_id = V6Options.GetValue("M_WS_ID");
                DataTable generalInfoData = new DataTable("GeneralInfo");
                IDictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary["TYPE"] = "DM,SD,LK,CT,VC,BC,HB,V6";
                dictionary["MA_DVCS"] = txtDanhSachDonVi_Text;
                dictionary["NGAY_CT1"] = dateNgay_ct1_Date;
                dictionary["NGAY_CT2"] = dateNgay_ct2_Date;
                dictionary["WS_ID"] = m_ws_id;
                dictionary["CHECKING"] = "DM,DL,SD";
                generalInfoData.AddRow(dictionary, true);
                var generalFile = Path.Combine(_tempDir, "GeneralInfo.xml");
                Data_Table.ToXmlFile(generalInfoData, generalFile);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }

        private void ExportAll()
        {
            try
            {
                _message += "DM,SD,LK,CT,VC,BC,HB,V6:";
                var types = "DM,SD,LK,CT,VC,BC,HB,V6".Split(',');
                foreach (string type in types)
                {
                    _message += "\r\n" + type;
                    var ds = RunProcV6CopyAll(type);
                    _message += " ds.Count: " + ds.Tables.Count;
                    ExportDataSet(ds, type);
                    _message += " CompleteExport ";
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _message), ex);
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
                if (radExcel.Checked)
                {
                    var setting = new ExportExcelSetting();
                    setting.saveFile = Path.Combine(tempDirCurrent, key + ".xls");
                    setting.data = tblList;
                    V6Tools.V6Export.ExportData.ToExcel(setting);
                    files.Add(setting.saveFile);

                    foreach (DataRow row in tblList.Rows)
                    {
                        //Xuất excel từng bảng dữ liệu
                        var stt = V6Tools.V6Convert.ObjectAndString.ObjectToInt(row["STT"]);
                        //var ma_file = V6Tools.V6Convert.ObjectAndString.ObjectToString(row["MA_FILE"]).Trim();
                        var xls_file = V6Tools.V6Convert.ObjectAndString.ObjectToString(row["XLS_FILE"]).Trim();
                        var data1 = ds.Tables[stt];
                        setting.saveFile = Path.Combine(tempDirCurrent, xls_file + ".xls");
                        setting.data = data1;
                        V6Tools.V6Export.ExportData.ToExcel(setting);
                        files.Add(setting.saveFile);
                    }
                }
                else
                {
                    var saveFile = Path.Combine(tempDirCurrent, key + ".xml");
                    V6Tools.V6Export.ExportData.ToXmlFile(tblList, saveFile);
                    files.Add(saveFile);

                    foreach (DataRow row in tblList.Rows)
                    {
                        //Xuất xml từng bảng dữ liệu
                        var stt = V6Tools.V6Convert.ObjectAndString.ObjectToInt(row["STT"]);
                        //var ma_file = V6Tools.V6Convert.ObjectAndString.ObjectToString(row["MA_FILE"]).Trim();
                        var xls_file = V6Tools.V6Convert.ObjectAndString.ObjectToString(row["XLS_FILE"]).Trim();
                        var data1 = ds.Tables[stt];
                        saveFile = Path.Combine(tempDirCurrent, xls_file + ".xml");
                        V6Tools.V6Export.ExportData.ToXmlFile(data1, saveFile);
                        files.Add(saveFile);
                    }
                }
            }
        }

        private DataSet RunProcV6CopyAll(string type)
        {
            string m_ws_id = V6Options.GetValue("M_WS_ID");

            SqlParameter[] plist = new[]
            {
                new SqlParameter("@Type", type),
                new SqlParameter("@Ngay_ct1", dateNgay_ct1_Date),
                new SqlParameter("@Ngay_ct2", dateNgay_ct2_Date),
                new SqlParameter("@Ma_dvcs", txtDanhSachDonVi_Text),
                new SqlParameter("@Ws_id", m_ws_id),
            };
            var ds = V6BusinessHelper.ExecuteProcedure("V6CopyRaAll", plist);
            return ds;
        }

        private List<string> files; 
        private void RunV67z()
        {
            if (File.Exists(_saveZipFile)) File.Delete(_saveZipFile);
            V67z.Run7z_Zip(_tempDir, _saveZipFile);
            Directory.Delete(_tempDir, true);
        }

        private void ChonFile()
        {
            var save = new SaveFileDialog {Filter = "7zip|*.7z|Rar|*.rar"};
            save.FileName = txtFileName.Text;
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
