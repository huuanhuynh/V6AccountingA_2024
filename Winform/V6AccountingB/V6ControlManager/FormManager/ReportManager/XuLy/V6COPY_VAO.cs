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
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class V6COPY_VAO : XuLyBase0
    {
        public V6COPY_VAO()
        {
            InitializeComponent();
        }

        public V6COPY_VAO(string itemId, string program, string reportProcedure, string reportFile, string text)
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
                this.ShowErrorMessage(GetType() + ".V6COPY_VAO Init: " + ex.Message);
            }
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("Sao chép số liệu vào.");
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
                //files = new List<string>();
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
                    V6ControlFormHelper.ShowMainMessage("V6CopyVao Thực hiện xong!\r\n" + _message);
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
                //_saveZipFile = txtFileName.Text;
                //_dir = Path.GetDirectoryName(_saveZipFile);
                //_tempDir = _dir + "\\Temp";
                
                //if (!Directory.Exists(_tempDir)) Directory.CreateDirectory(_tempDir);

                if (chkDanhMuc.Checked) ImportDanhMuc();
                if (chkDuLieu.Checked) ExportDuLieu();
                if (chkSoDuVaLuyKe.Checked) ExportSoDuVaLuyKe();
                
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


        private void ImportDanhMuc()
        {
            _message += "\r\nDM";
            var ds = RunProcV6CopyRa("DM");
            _message += " ds.Count: " + ds.Tables.Count;
            ImportDataDanhMuc(ds, "DM");
            _message += " Complete Import Danh mục ";
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
                ImportDataDuLieu(ds, type);
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
                //ImportDataSoDuLuyKe(ds, type);
                _message += " CompleteExport ";
            }
        }

        /// <summary>
        /// Nhập dữ liệu
        /// </summary>
        /// <param name="config">Cấu hình lấy từ hàm V6CopyVao</param>
        /// <param name="key">Thư mục</param>
        private void ImportDataDanhMuc(DataSet config, string key)
        {
            var tempDirCurrent = Path.Combine(_tempDir, key);
            if (!Directory.Exists(tempDirCurrent))
            {
                Directory.CreateDirectory(tempDirCurrent);
            }
            
            if (config.Tables.Count > 1)
            {
                var tblList = config.Tables[0];

                if (radExcel.Checked)
                {
                    ShowMainMessage(V6Text.NotSupported);
                }
                else
                {
                    var saveFile = Path.Combine(tempDirCurrent, key + ".xml");
                    if (File.Exists(saveFile))
                    {
                        var saveList = Data_Table.FromXml(saveFile);
                        var saveListDic = saveList.ToDataDictionary("MA_FILE", "XLS_FILE");
                        // so sánh với tblList, nếu cùng có mới thực hiện
                        foreach (DataRow row0 in tblList.Rows)
                        {
                            // Xuất xml từng bảng dữ liệu
                            var stt = ObjectAndString.ObjectToInt(row0["STT"]);
                            var MA_FILE = ObjectAndString.ObjectToString(row0["MA_FILE"]).Trim().ToUpper();
                            var table_name = MA_FILE;
                            var xls_file = ObjectAndString.ObjectToString(row0["XLS_FILE"]).Trim();
                            var dele_type = ObjectAndString.ObjectToString(row0["dele_type"]).Trim();
                            var fields = ObjectAndString.ObjectToString(row0["fields"]).Trim();
                            //var data1 = config.Tables[stt];
                            var data1File = Path.Combine(tempDirCurrent, xls_file + ".xml");

                            // kiểm tra trong saveList
                            if (!saveListDic.ContainsKey(MA_FILE)) continue;
                            if (!File.Exists(data1File)) continue;
                            
                            

                            var data1 = Data_Table.FromXml(data1File);
                            // insert data to tableName
                            //dele_type "0" nếu tồn tại không insert, (khóa fields)
                            //          "1" xóa rồi insert  (mặc định)
                            //          "2" update nếu tồn tại, insert nếu chưa có.
                            V6Categories ca = new V6Categories();
                            foreach (DataRow row1 in data1.Rows)
                            {
                                bool exists = false;
                                string filter = null;
                                // check tồn tại
                                IDictionary<string, object> check_data = new SortedDictionary<string, object>();
                                var field_list = ObjectAndString.SplitString(fields);
                                foreach (string field in field_list)
                                {
                                    check_data[field] = row1[field];
                                }
                                exists = V6BusinessHelper.CheckDataExist(table_name, check_data, filter);

                                if (dele_type == "2")   //update nếu tồn tại, insert nếu chưa có.
                                {
                                    if (exists)
                                    {
                                        ca.Update(table_name, row1.ToDataDictionary(), check_data);
                                    }
                                    else
                                    {
                                        ca.Insert(table_name, row1.ToDataDictionary());
                                    }
                                }
                                else if (dele_type == "0") //"0" nếu tồn tại không insert, (khóa fields)
                                {
                                    if (!exists)
                                    {
                                        ca.Insert(table_name, row1.ToDataDictionary());
                                    }
                                }
                                else // dele_type == 1 // xóa nếu tồn tại rồi insert
                                {
                                    if (exists)
                                    {
                                        ca.Delete(table_name, check_data);
                                    }
                                    ca.Insert(table_name, row1.ToDataDictionary());
                                }
                            }

                            //InsertTable(data, ma_file);

                        }
                    }//end if exists save file
                }
            }
        }

        /// <summary>
        /// Nhập dữ liệu
        /// </summary>
        /// <param name="config">Cấu hình lấy từ hàm V6CopyVao</param>
        /// <param name="key">Thư mục</param>
        private void ImportDataDuLieu(DataSet config, string key)
        {
            var tempDirCurrent = Path.Combine(_tempDir, key);
            if (!Directory.Exists(tempDirCurrent))
            {
                Directory.CreateDirectory(tempDirCurrent);
            }

            if (config.Tables.Count > 1)
            {
                var tblList = config.Tables[0];

                if (radExcel.Checked)
                {
                    ShowMainMessage(V6Text.NotSupported);
                }
                else
                {
                    var saveFile = Path.Combine(tempDirCurrent, key + ".xml");
                    if (File.Exists(saveFile))
                    {
                        var saveList = Data_Table.FromXml(saveFile);
                        var saveListDic = saveList.ToDataDictionary("MA_FILE", "XLS_FILE");
                        // so sánh với tblList, nếu cùng có mới thực hiện
                        foreach (DataRow row0 in tblList.Rows)
                        {
                            // Xuất xml từng bảng dữ liệu
                            var stt = ObjectAndString.ObjectToInt(row0["STT"]);
                            var MA_FILE = ObjectAndString.ObjectToString(row0["MA_FILE"]).Trim().ToUpper();
                            var table_name = MA_FILE;
                            var xls_file = ObjectAndString.ObjectToString(row0["XLS_FILE"]).Trim();
                            var dele_type = ObjectAndString.ObjectToString(row0["dele_type"]).Trim();
                            var fields = ObjectAndString.ObjectToString(row0["fields"]).Trim();
                            //var data1 = config.Tables[stt];
                            var data1File = Path.Combine(tempDirCurrent, xls_file + ".xml");

                            // kiểm tra trong saveList
                            if (!saveListDic.ContainsKey(MA_FILE)) continue;
                            if (!File.Exists(data1File)) continue;



                            var data1 = Data_Table.FromXml(data1File);
                            // insert data to tableName
                            //dele_type "0" nếu tồn tại không insert, (khóa fields)
                            //          "1" xóa rồi insert  (mặc định)
                            //          "2" update nếu tồn tại, insert nếu chưa có.
                            V6InvoiceBase ca = new V6Invoice81();
                            foreach (DataRow row1 in data1.Rows)
                            {
                                bool exists = false;
                                string filter = null;
                                // check tồn tại
                                string stt_rec = null;
                                IDictionary<string, object> check_data = new SortedDictionary<string, object>();
                                IDictionary<string, object> am = new SortedDictionary<string, object>();
                                List<IDictionary<string, object>> adList = new List<IDictionary<string, object>>();
                                List<IDictionary<string, object>> ad3List = new List<IDictionary<string, object>>();
                                var field_list = ObjectAndString.SplitString(fields);
                                foreach (string field in field_list)
                                {
                                    check_data[field] = row1[field];
                                }
                                exists = V6BusinessHelper.CheckDataExist(table_name, check_data, filter);

                                if (dele_type == "2")   //update nếu tồn tại, insert nếu chưa có.
                                {
                                    if (exists)
                                    {
                                        ca.UpdateInvoice(am, adList, ad3List, check_data);
                                    }
                                    else
                                    {
                                        ca.InsertInvoice(am, adList, ad3List);
                                    }
                                }
                                else if (dele_type == "0") //"0" nếu tồn tại không insert, (khóa fields)
                                {
                                    if (!exists)
                                    {
                                        ca.InsertInvoice(am, adList, ad3List);
                                    }
                                }
                                else // dele_type == 1 // xóa nếu tồn tại rồi insert
                                {
                                    if (exists)
                                    {
                                        ca.DeleteInvoice(stt_rec);
                                    }
                                    ca.InsertInvoice(am, adList, ad3List);
                                }
                            }


                        }
                    }//end if exists save file
                }
            }
        }

        private int InsertTable(DataTable data, string tableName)
        {
            int count = 0;
            try
            {
                V6Categories ca = new V6Categories();
                foreach (DataRow row in data.Rows)
                {
                    try
                    {
                        ca.Insert(tableName, row.ToDataDictionary());
                        count++;
                    }
                    catch (Exception ex1)
                    {

                    }
                }
            }
            catch (Exception ex0)
            {

            }

            return count;
        }
        
        private DataSet RunProcV6CopyRa(string type)
        {
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@Type", type),
                new SqlParameter("@Ngay_ct1", dateNgay_ct1.Date),
                new SqlParameter("@Ngay_ct2", dateNgay_ct2.Date),
                new SqlParameter("@Ma_dvcs", txtDanhSachDonVi.Text),
                new SqlParameter("@Ws_id", V6Options.GetValue("M_WS_ID")),
            };
            var ds = V6BusinessHelper.ExecuteProcedure("V6CopyVao", plist);
            return ds;
        }

        private List<string> files0;
        
        private void ChonFile()
        {
            string file = V6ControlFormHelper.ChooseOpenFile(this, "7zip|*.7z|Rar|*.rar");
            if (!string.IsNullOrEmpty(file))
            {
                txtFileName.Text = file;
                LoadFileInfo(file);
            }
        }

        private void LoadFileInfo(string file)
        {
            try
            {
                V67z.Run7z_Unzip(file);
                _saveZipFile = file;
                _dir = Path.GetDirectoryName(file);
                _tempDir = _dir + "\\Temp";
                var generalFile = Path.Combine(_tempDir, "GeneralInfo.xml");
                DataTable generalData = Data_Table.FromXmlFile(generalFile);
                if (generalData != null && generalData.Rows.Count > 0)
                {
                    var row = generalData.Rows[0];
                    dateNgay_ct1.Value = ObjectAndString.ObjectToFullDateTime(row["NGAY_CT1"]);
                    dateNgay_ct2.Value = ObjectAndString.ObjectToFullDateTime(row["NGAY_CT2"]);
                    txtDanhSachDonVi.Text = row["MA_DVCS"].ToString().Trim();
                    string checking = row["CHECKING"].ToString().Trim();
                    chkDanhMuc.Checked = checking.Contains("DM");
                    chkDuLieu.Checked = checking.Contains("DL");
                    chkSoDuVaLuyKe.Checked = checking.Contains("SD");
                }
                DoNothing();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, file), ex);
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
