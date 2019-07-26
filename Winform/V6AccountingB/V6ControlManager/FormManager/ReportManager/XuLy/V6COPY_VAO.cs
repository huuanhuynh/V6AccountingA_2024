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
        private DateTime dateNgay_ct1_Date;
        private DateTime dateNgay_ct2_Date;
        private string txtDanhSachDonVi_Text = null;
        private readonly string m_ws_id = V6Options.GetValue("M_WS_ID");

        public V6COPY_VAO()
        {
            InitializeComponent();
        }

        public V6COPY_VAO(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
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

                
                if (txtFileName.Text.Trim() == "")
                {
                    this.ShowInfoMessage("Chưa chọn gói dữ liệu nào!");
                    btnSaveAs.PerformClick();
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
                    V6ControlFormHelper.ShowMainMessage(V6Text.Finish + " V6CopyVao\r\n" + _message);
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
                SetStatusText(_message);
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
                
                if (chkDanhMuc.Checked) ImportDanhMuc();
                if (chkDuLieu.Checked) ImportDuLieu();
                if (chkSoDuVaLuyKe.Checked) ImportSoDuVaLuyKe();
                
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

        private void ImportDuLieu()
        {
            _message += "\r\nDữ liệu: VC,BC,HB,CT:";
            var types = "VC,BC,HB,CT".Split(',');
            foreach (string type in types)
            {
                _message += "\r\n" + type;
                var ds = RunProcV6CopyRa(type);
                _message += " ds.Count: " + ds.Tables.Count;
                ImportDataDuLieu(ds, type);
                _message += " Complete import dulieu ";
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

        private void ImportSoDuVaLuyKe()
        {
            _message += "\r\nLũy kế: SD,LK:";
            var types = "SD,LK".Split(',');
            foreach (string type in types)
            {
                _message += "\r\n" + type;
                var ds = RunProcV6CopyRa(type);
                _message += " ds.Count: " + ds.Tables.Count;
                ImportDataSoDuLuyKe(ds, type);
                _message += " Complete import soduluyke ";
            }
        }

        /// <summary>
        /// Danh mục
        /// </summary>
        V6Categories ca = new V6Categories();
        /// <summary>
        /// Nhập dữ liệu
        /// </summary>
        /// <param name="config">Cấu hình lấy từ hàm V6CopyVao</param>
        /// <param name="key">Thư mục</param>
        private void ImportDataDanhMuc(DataSet config, string key)
        {
            string propress_mess = null;
            List<string> list_ma_file = new List<string>();
            try
            {
                var tempDirCurrent = Path.Combine(_tempDir, key);
                if (!Directory.Exists(tempDirCurrent))
                {
                    Directory.CreateDirectory(tempDirCurrent);
                }

                if (config.Tables.Count > 0)
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
                            var saveList = Data_Table.FromXmlFile(saveFile);
                            var saveListDic = saveList.ToDataDictionary("MA_FILE", "XLS_FILE");
                            // so sánh với tblList, nếu cùng có mới thực hiện
                            foreach (DataRow row0 in tblList.Rows)
                            {
                                // Đọc xml từng bảng dữ liệu
                                var stt = ObjectAndString.ObjectToInt(row0["STT"]);
                                var MA_FILE = ObjectAndString.ObjectToString(row0["MA_FILE"]).Trim().ToUpper();
                                _message = MA_FILE;
                                list_ma_file.Add(MA_FILE);
                                if (MA_FILE == "ALVT")
                                {
                                    //DoNothing();
                                }
                                else
                                {
                                    //;ALBP;ALBPCC;ALBPHT;ALBPTS;ALCLTG;ALDVCS;ALGIA;ALGIA2;ALKC;ALKH;ALKHO;ALKU;ALLO;ALNHCC;ALNHKH;ALNHPHI;ALNHTHUE;ALNHTK;ALNHTS;ALNHVT;ALNHVV;ALNV;ALPB;ALPB1;ALPHI;ALQDDVT;ALSONB;ALTD;ALTD2;ALTD3;ALTGCC;ALTGNT;ALTGTS;ALTHUE;ALTK;ALTK0;"
                                    //continue; // bỏ qua hết, chỉ chạy phần if.
                                }
                                
                                var table_name = MA_FILE;
                                var xls_file = ObjectAndString.ObjectToString(row0["XLS_FILE"]).Trim();
                                var dele_type = ObjectAndString.ObjectToString(row0["dele_type"]).Trim();
                                var fields = ObjectAndString.ObjectToString(row0["fields"]).Trim();
                                //var data1 = config.Tables[stt];
                                var data1File = Path.Combine(tempDirCurrent, xls_file + ".xml");

                                // kiểm tra trong saveList
                                if (!saveListDic.ContainsKey(MA_FILE)) continue;
                                if (!File.Exists(data1File)) continue;



                                var data1 = Data_Table.FromXmlFile(data1File);
                                if (data1 == null) continue;
                                // insert data to tableName
                                //dele_type "0" nếu tồn tại không insert, (khóa fields)
                                //          "1" xóa rồi insert  (mặc định)
                                //          "2" update nếu tồn tại, insert nếu chưa có.

                                foreach (DataRow row1 in data1.Rows)
                                {
                                    bool exists = false;
                                    string filter = null;
                                    // check tồn tại
                                    IDictionary<string, object> check_data = new SortedDictionary<string, object>();
                                    var field_list = ObjectAndString.SplitString(fields);
                                    foreach (string field in field_list)
                                    {
                                        check_data[field.ToUpper()] = row1[field];
                                    }
                                    _message = MA_FILE + " " + check_data["MA_VT"];
                                    exists = V6BusinessHelper.CheckDataExist(table_name, check_data, filter);

                                    if (dele_type == "2") //update nếu tồn tại, insert nếu chưa có.
                                    {
                                        if (exists)
                                        {
                                            ca.UpdateSimple(table_name, row1.ToDataDictionary(), check_data);
                                        }
                                        else
                                        {
                                            ca.InsertSimple(table_name, row1.ToDataDictionary());
                                        }
                                    }
                                    else if (dele_type == "0") //"0" nếu tồn tại không insert, (khóa fields)
                                    {
                                        if (!exists)
                                        {
                                            ca.InsertSimple(table_name, row1.ToDataDictionary());
                                        }
                                    }
                                    else // dele_type == 1 // xóa nếu tồn tại rồi insert
                                    {
                                        if (exists)
                                        {
                                            ca.Delete(table_name, check_data);
                                        }
                                        ca.InsertSimple(table_name, row1.ToDataDictionary());
                                    }
                                }


                            }
                        } //end if exists save file
                    }
                }
            }
            catch (Exception ex)
            {
                propress_mess = string.Format("MA_FILE:{0}", ObjectAndString.ObjectToString(list_ma_file));
                this.ShowErrorException(GetType() + ".ImportDataDanhMuc\r\n" + propress_mess, ex);
            }
        }

        /// <summary>
        /// Nhập dữ liệu
        /// </summary>
        /// <param name="config">Cấu hình lấy từ hàm V6CopyVao</param>
        /// <param name="key">Thư mục</param>
        private void ImportDataDuLieu(DataSet config, string key)
        {
            string propress_mess = null;
            List<string> list_ma_file = new List<string>();
            try
            {
                var tempDirCurrent = Path.Combine(_tempDir, key);
                if (!Directory.Exists(tempDirCurrent))
                {
                    Directory.CreateDirectory(tempDirCurrent);
                }

                if (config.Tables.Count > 0)
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
                            var saveList = Data_Table.FromXmlFile(saveFile);
                            var saveListDic = saveList.ToDataDictionary("MA_FILE", "XML_FILE");
                            // so sánh với tblList, nếu cùng có mới thực hiện
                            foreach (DataRow row0 in tblList.Rows)
                            {
                                // Đọc xml từng bảng dữ liệu
                                var stt = ObjectAndString.ObjectToInt(row0["STT"]);
                                var MA_CT = row0["MA_CT"].ToString().Trim();
                                var MA_FILE = ObjectAndString.ObjectToString(row0["MA_FILE"]).Trim().ToUpper();
                                _message = MA_FILE;
                                list_ma_file.Add(MA_FILE);
#if DEBUG
                                if (MA_FILE == "AM81")
                                {
                                    DoNothing();
                                }
                                else
                                {
                                    continue; // bỏ qua hết, chỉ chạy phần if.
                                }
#endif
                                var MA_FILE_CT = ObjectAndString.ObjectToString(row0["MA_FILE_CT"]).Trim().ToUpper();
                                    // AD81,AD81GT - tên các bảng chi tiết, thuế...
                                var table_name_AM = MA_FILE;
                                var table_ad_list = ObjectAndString.SplitString(MA_FILE_CT);
                                var ref_code = ObjectAndString.ObjectToString(row0["REF_CODE"]).Trim(); // STT_REC
                                var xml_file = ObjectAndString.ObjectToString(row0["XML_FILE"]).Trim();
                                var dele_type = ObjectAndString.ObjectToString(row0["dele_type"]).Trim();
                                //var fields = ObjectAndString.ObjectToString(row0["fields"]).Trim();
                                //var data1 = config.Tables[stt];
                                var data1File = Path.Combine(tempDirCurrent, xml_file + ".xml");

                                // kiểm tra trong saveList
                                if (!saveListDic.ContainsKey(MA_FILE)) continue;
                                if (!File.Exists(data1File)) continue;
                                if (!V6BusinessHelper.IsExistDatabaseTable(table_name_AM))
                                {
                                    _message = V6Text.NotExist + table_name_AM;
                                }


                                var data1 = Data_Table.FromXmlFile(data1File);
                                if (data1 == null) continue;

                                string table_name_AD1 = null, table_name_AD2 = null, table_name_AD3 = null;
                                DataTable data_AD1 = null, data_AD2 = null, data_AD3 = null;
                                if (table_ad_list.Length > 0)
                                {
                                    table_name_AD1 = table_ad_list[0].Trim();
                                    var data_AD1File = Path.Combine(tempDirCurrent, table_name_AD1 + ".xml");
                                    data_AD1 = Data_Table.FromXmlFile(data_AD1File);
                                }                                
                                if (table_ad_list.Length > 1)
                                {
                                    table_name_AD2 = table_ad_list[1].Trim();
                                    var data_AD2File = Path.Combine(tempDirCurrent, table_name_AD2 + ".xml");
                                    data_AD2 = Data_Table.FromXmlFile(data_AD2File);
                                }
                                if (table_ad_list.Length > 2)
                                {
                                    table_name_AD3 = table_ad_list[2].Trim();
                                    var data_AD3File = Path.Combine(tempDirCurrent, table_name_AD3 + ".xml");
                                    data_AD3 = Data_Table.FromXmlFile(data_AD3File);
                                }

                                // insert data to tableName
                                //dele_type "0" nếu tồn tại không insert, (khóa fields)
                                //          "1" xóa rồi insert  (mặc định)
                                //          "2" update nếu tồn tại, insert nếu chưa có.
                                //V6InvoiceBase ca = new V6Invoice81();
                                //ca = V6InvoiceBase.GetInvoice(MA_CT);
                                foreach (DataRow row1 in data1.Rows)
                                {
                                    bool exists = false;
                                    string filter = null;
                                    // check tồn tại
                                    string stt_rec = row1[ref_code].ToString().Trim();
                                    _message = MA_FILE + " " + stt_rec;
                                    IDictionary<string, object> check_data = new SortedDictionary<string, object>();
                                    check_data[ref_code] = stt_rec;
                                    IDictionary<string, object> am = row1.ToDataDictionary();
                                    List<IDictionary<string, object>> ad1List = new List<IDictionary<string, object>>();
                                    List<IDictionary<string, object>> ad2List = new List<IDictionary<string, object>>();
                                    List<IDictionary<string, object>> ad3List = new List<IDictionary<string, object>>();

                                    var data_AD1_view = new DataView(data_AD1);
                                    data_AD1_view.RowFilter = "STT_REC='" + stt_rec + "'";
                                    ad1List = data_AD1_view.ToTable().ToListDataDictionary();
                                    if (data_AD2 != null)
                                    {
                                        var data_AD2_view = new DataView(data_AD2);
                                        data_AD2_view.RowFilter = "STT_REC='" + stt_rec + "'";
                                        ad2List = data_AD2_view.ToTable().ToListDataDictionary();
                                    }
                                    if (data_AD3 != null)
                                    {
                                        var data_AD3_view = new DataView(data_AD3);
                                        data_AD3_view.RowFilter = "STT_REC='" + stt_rec + "'";
                                        ad3List = data_AD3_view.ToTable().ToListDataDictionary();
                                    }

                                    exists = ca.IsExistData(table_name_AM, check_data, filter);

                                    if (dele_type == "0") //"0" nếu tồn tại không insert
                                    {
                                        if (!exists)
                                        {
                                            ca.InsertSimple(table_name_AM, am);
                                            if (table_name_AD1 != null)
                                                foreach (IDictionary<string, object> dictionary in ad1List)
                                                {
                                                    ca.InsertSimple(table_name_AD1, dictionary);
                                                }
                                            if (table_name_AD2 != null)
                                                foreach (IDictionary<string, object> dictionary in ad2List)
                                                {
                                                    ca.InsertSimple(table_name_AD2, dictionary);
                                                }
                                            if (table_name_AD3 != null)
                                                foreach (IDictionary<string, object> dictionary in ad3List)
                                                {
                                                    ca.InsertSimple(table_name_AD3, dictionary);
                                                }
                                        }
                                    }
                                    else // dele_type == 1 // xóa nếu tồn tại rồi insert
                                    {
                                        if (exists)
                                        {
                                            //Xóa AD1
                                            if (table_name_AD1 != null) ca.Delete(table_name_AD1, check_data);
                                            //Xóa AD2
                                            if (table_name_AD2 != null) ca.Delete(table_name_AD2, check_data);
                                            //Xóa AD3
                                            if (table_name_AD3 != null) ca.Delete(table_name_AD3, check_data);
                                            //Xóa AM
                                            ca.Delete(table_name_AM, check_data);

                                        }
                                        ca.InsertSimple(table_name_AM, am);
                                        if (table_name_AD1 != null)
                                            foreach (IDictionary<string, object> dictionary in ad1List)
                                            {
                                                ca.InsertSimple(table_name_AD1, dictionary);
                                            }
                                        if (table_name_AD2 != null)
                                            foreach (IDictionary<string, object> dictionary in ad2List)
                                            {
                                                ca.InsertSimple(table_name_AD2, dictionary);
                                            }
                                        if (table_name_AD3 != null)
                                            foreach (IDictionary<string, object> dictionary in ad3List)
                                            {
                                                ca.InsertSimple(table_name_AD3, dictionary);
                                            }
                                    }
                                }

                            } //end list
                        } //end if exists save file
                    }
                }
            }
            catch (Exception ex)
            {
                propress_mess = string.Format("MA_FILE:{0}", ObjectAndString.ObjectToString(list_ma_file));
                this.ShowErrorException(GetType() + ".ImportDataDuLieu\r\n" + propress_mess, ex);
            }
        }

        private void ImportDataSoDuLuyKe(DataSet config, string key)
        {
            string propress_mess = null;
            List<string> list_ma_file = new List<string>();
            try
            {
                var tempDirCurrent = Path.Combine(_tempDir, key);
                if (!Directory.Exists(tempDirCurrent))
                {
                    Directory.CreateDirectory(tempDirCurrent);
                }

                if (config.Tables.Count > 0)
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
                            var saveList = Data_Table.FromXmlFile(saveFile);
                            var saveListDic = saveList.ToDataDictionary("MA_FILE", "XML_FILE");
                            // so sánh với tblList, nếu cùng có mới thực hiện
                            foreach (DataRow row0 in tblList.Rows)
                            {
                                // Đọc xml từng bảng dữ liệu
                                var stt = ObjectAndString.ObjectToInt(row0["STT"]);
                                var MA_CT = row0["MA_CT"].ToString().Trim();
                                var MA_FILE = ObjectAndString.ObjectToString(row0["MA_FILE"]).Trim().ToUpper();
                                list_ma_file.Add(MA_FILE);
                                var MA_FILE_CT = ObjectAndString.ObjectToString(row0["MA_FILE_CT"]).Trim().ToUpper();
                                    // AD81,AD81GT - tên các bảng chi tiết, thuế...
                                var table_name_AM = MA_FILE;
                                var table_ad_list = ObjectAndString.SplitString(MA_FILE_CT);
                                var ref_code = ObjectAndString.ObjectToString(row0["REF_CODE"]).Trim(); // STT_REC
                                var xml_file = ObjectAndString.ObjectToString(row0["XML_FILE"]).Trim();
                                var dele_type = ObjectAndString.ObjectToString(row0["dele_type"]).Trim();
                                //var fields = ObjectAndString.ObjectToString(row0["fields"]).Trim();
                                //var data1 = config.Tables[stt];
                                var data1File = Path.Combine(tempDirCurrent, xml_file + ".xml");

                                // kiểm tra trong saveList
                                if (!saveListDic.ContainsKey(MA_FILE)) continue;
                                if (!File.Exists(data1File)) continue;
                                if (!V6BusinessHelper.IsExistDatabaseTable(table_name_AM))
                                {
                                    _message = V6Text.NotExist + table_name_AM;
                                }


                                var data1 = Data_Table.FromXmlFile(data1File);
                                if (data1 == null) continue;

                                string table_name_AD1 = null, table_name_AD2 = null, table_name_AD3 = null;
                                DataTable data_AD1 = null, data_AD2 = null, data_AD3 = null;
                                if (table_ad_list.Length > 0)
                                {
                                    table_name_AD1 = table_ad_list[0].Trim();
                                    if (!V6BusinessHelper.IsExistDatabaseTable(table_name_AD1))
                                    {
                                        _message = V6Text.NotExist + table_name_AD1;
                                    }
                                    var data_AD1File = Path.Combine(tempDirCurrent, table_name_AD1 + ".xml");
                                    data_AD1 = Data_Table.FromXmlFile(data_AD1File);
                                }
                                if (table_ad_list.Length > 1)
                                {
                                    table_name_AD2 = table_ad_list[1].Trim();
                                    if (!V6BusinessHelper.IsExistDatabaseTable(table_name_AD2))
                                    {
                                        _message = V6Text.NotExist + table_name_AD2;
                                    }
                                    var data_AD2File = Path.Combine(tempDirCurrent, table_name_AD2 + ".xml");
                                    data_AD2 = Data_Table.FromXmlFile(data_AD2File);
                                }
                                if (table_ad_list.Length > 2)
                                {
                                    table_name_AD3 = table_ad_list[2].Trim();
                                    if (!V6BusinessHelper.IsExistDatabaseTable(table_name_AD3))
                                    {
                                        _message = V6Text.NotExist + table_name_AD3;
                                    }
                                    var data_AD3File = Path.Combine(tempDirCurrent, table_name_AD3 + ".xml");
                                    data_AD3 = Data_Table.FromXmlFile(data_AD3File);
                                }

                                // insert data to tableName
                                //dele_type "0" nếu tồn tại không insert, (khóa fields)
                                //          "1" xóa rồi insert  (mặc định)
                                //          "2" update nếu tồn tại, insert nếu chưa có.
                                //V6InvoiceBase ca = new V6Invoice81();
                                //ca = V6InvoiceBase.GetInvoice(MA_CT);
                                foreach (DataRow row1 in data1.Rows)
                                {
                                    bool exists = false;
                                    string filter = null;
                                    // check tồn tại
                                    string stt_rec = row1[ref_code].ToString().Trim();
                                    IDictionary<string, object> check_data = new SortedDictionary<string, object>();
                                    check_data[ref_code] = stt_rec;
                                    IDictionary<string, object> am = new SortedDictionary<string, object>();
                                    List<IDictionary<string, object>> ad1List = new List<IDictionary<string, object>>();
                                    List<IDictionary<string, object>> ad2List = new List<IDictionary<string, object>>();
                                    List<IDictionary<string, object>> ad3List = new List<IDictionary<string, object>>();

                                    if (data_AD2 != null)
                                    {
                                        var data_AD1_view = new DataView(data_AD1);
                                        data_AD1_view.RowFilter = "STT_REC='" + stt_rec + "'";
                                        ad1List = data_AD1_view.ToTable().ToListDataDictionary();
                                    }
                                    if (data_AD2 != null)
                                    {
                                        var data_AD2_view = new DataView(data_AD2);
                                        data_AD2_view.RowFilter = "STT_REC='" + stt_rec + "'";
                                        ad2List = data_AD2_view.ToTable().ToListDataDictionary();
                                    }
                                    if (data_AD3 != null)
                                    {
                                        var data_AD3_view = new DataView(data_AD3);
                                        data_AD3_view.RowFilter = "STT_REC='" + stt_rec + "'";
                                        ad3List = data_AD3_view.ToTable().ToListDataDictionary();
                                    }

                                    exists = ca.IsExistData(table_name_AM, check_data, filter);

                                    if (dele_type == "0") //"0" nếu tồn tại không insert
                                    {
                                        if (!exists)
                                        {
                                            ca.InsertSimple(table_name_AM, am);
                                            if (table_name_AD1 != null)
                                                foreach (IDictionary<string, object> dictionary in ad1List)
                                                {
                                                    ca.InsertSimple(table_name_AD1, dictionary);
                                                }
                                            if (table_name_AD2 != null)
                                                foreach (IDictionary<string, object> dictionary in ad2List)
                                                {
                                                    ca.InsertSimple(table_name_AD2, dictionary);
                                                }
                                            if (table_name_AD3 != null)
                                                foreach (IDictionary<string, object> dictionary in ad3List)
                                                {
                                                    ca.InsertSimple(table_name_AD3, dictionary);
                                                }
                                        }
                                    }
                                    else // dele_type == 1 // xóa nếu tồn tại rồi insert
                                    {
                                        if (exists)
                                        {
                                            //Xóa AD1
                                            if (table_ad_list.Length > 0) ca.Delete(table_name_AD1, check_data);
                                            //Xóa AD2
                                            if (table_ad_list.Length > 1) ca.Delete(table_name_AD2, check_data);
                                            //Xóa AD3
                                            if (table_ad_list.Length > 2) ca.Delete(table_name_AD3, check_data);
                                            //Xóa AM
                                            ca.Delete(table_name_AM, check_data);

                                        }
                                        ca.InsertSimple(table_name_AM, am);
                                        if (table_name_AD1 != null)
                                            foreach (IDictionary<string, object> dictionary in ad1List)
                                            {
                                                ca.InsertSimple(table_name_AD1, dictionary);
                                            }
                                        if (table_name_AD2 != null)
                                            foreach (IDictionary<string, object> dictionary in ad2List)
                                            {
                                                ca.InsertSimple(table_name_AD2, dictionary);
                                            }
                                        if (table_name_AD3 != null)
                                            foreach (IDictionary<string, object> dictionary in ad3List)
                                            {
                                                ca.InsertSimple(table_name_AD3, dictionary);
                                            }
                                    }
                                }

                            } //end list
                        } //end if exists save file
                    }
                }
            }
            catch (Exception ex)
            {
                propress_mess = string.Format("MA_FILE:{0}", ObjectAndString.ObjectToString(list_ma_file));
                this.ShowErrorException(GetType() + ".ImportDataSoDuLuyKe\r\n" + propress_mess, ex);
            }
        }
        
        private DataSet RunProcV6CopyRa(string type)
        {
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@Type", type),
                new SqlParameter("@Ngay_ct1", dateNgay_ct1_Date),
                new SqlParameter("@Ngay_ct2", dateNgay_ct2_Date),
                new SqlParameter("@Ma_dvcs", txtDanhSachDonVi_Text),
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
                //txtFileName.Text = file;
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
                txtFileName.Text = file;
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
