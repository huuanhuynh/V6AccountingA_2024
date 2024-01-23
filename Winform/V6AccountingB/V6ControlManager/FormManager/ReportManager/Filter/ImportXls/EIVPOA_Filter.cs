using Svg;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6ThuePostViettelV2Api;
using V6Tools;
using System.Data;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;
using SharpCompress.Readers;
using System.Xml.Serialization;
using V6ThuePost.HDDT_GDT_GOV;
using V6Tools.V6Convert;
using Newtonsoft.Json;
using V6ThuePost;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class EIVPOA_Filter: FilterBase
    {
        public EIVPOA_Filter()
        {
            InitializeComponent();
            F3 = false;
            F5 = false;
            F9 = true;
            
        }
        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            return null;
        }

        public override void UpdateValues()
        {
            //String1 = txtFile.Text;
            //String2 = comboBox1.SelectedItem.ToString();
            //String3 = comboBox2.SelectedItem.ToString();
            Check1 = chkContinueDownload.Checked;
            Check2 = chkDeleteOldExcel.Checked;
        }

        HoadondientuAPI api = new HoadondientuAPI("https://hoadondientu.gdt.gov.vn:30000");
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                var capcha = api.GetLoginCapcha();
                string xml = capcha.content;
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(xml);
                if (xml.Length > 100)
                {
                    var svgdoc = SvgDocument.Open(xmldoc);
                    svgImage.Image = svgdoc.Draw();

                    txtUserName.Enabled = true;
                    txtPassword.Enabled = true;
                    txtCaptcha.Enabled = true;
                    btnLogin.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, ex.Message));
            }
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var login = api.Login(txtUserName.Text, txtPassword.Text, txtCaptcha.Text, api.captcha.key);
                if (string.IsNullOrEmpty(login.message) && !string.IsNullOrEmpty(login.token))
                {
                    txtToken.Text = login.token;
                    txtUserName.Enabled = false;
                    txtPassword.Enabled = false;
                    txtCaptcha.Enabled = false;
                    btnLogin.Enabled = false;
                }
                else
                {
                    this.ShowInfoMessage(login.message);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public int per_cent = 0, total = 10, ok_count = 0, fail_count = 0, err_count = 0;
        DataTable full_table = null;
        List<Dictionary<string, object>> Continue_download_list = null;

        public ALIM2XLS_CONFIG ALIM2XLS_Config { get; internal set; }

        /// <summary>
        /// Tải lại những hóa đơn tải lỗi trước đó.
        /// </summary>
        /// <param name="err_string"></param>
        /// <returns></returns>
        public bool ContinueDownload(out string err_string)
        {
            err_string = "";
            try
            {
                if (chkContinueDownload.Checked && fail_count + err_count > 0 && Continue_download_list != null)
                {
                    var list = new List<Dictionary<string, object>>(Continue_download_list);
                    Continue_download_list = new List<Dictionary<string, object>>();
                    fail_count = 0; err_count = 0;
                    foreach (var crowexcelData in list)
                    {
                        string STT = crowexcelData["STT"].ToString();
                        string nbmst = txtUserName.Text;// crowexcelData["MST người bán/MST người xuất hàng".ToUpper()].ToString();
                        string khhdon = crowexcelData["Ký hiệu hóa đơn".ToUpper()].ToString();
                        string shdon = crowexcelData["Số hóa đơn".ToUpper()].ToString();
                        string khmshdon = crowexcelData["Ký hiệu mẫu số".ToUpper()].ToString();
                        string saveFileZip = Path.Combine(V6Setting.V6SoftLocalAppData_Directory, "invoice_" + shdon + ".zip");
                        string one = api.DownloadInvoiceXml_zip(nbmst, khhdon, shdon, khmshdon, saveFileZip);
                        if (string.IsNullOrEmpty(one) && File.Exists(saveFileZip))
                        {
                            try
                            {
                                // Đọc trong file zip, load invoice.xml
                                var xdoc = new XmlDocument();
                                using (Stream stream = File.OpenRead(saveFileZip))
                                {
                                    var reader = ReaderFactory.Open(stream);
                                    while (reader.MoveToNextEntry())
                                    {
                                        if (!reader.Entry.IsDirectory)
                                        {
                                            if (reader.Entry.Key == "invoice.xml")
                                            {
                                                xdoc.Load(reader.OpenEntryStream());
                                                break;
                                            }
                                        }
                                    }
                                }

                                var hoa_don = new HDon(xdoc);

                                var ttchung_data = GetDataXml(hoa_don.DLHDon.TTChung);

                                var dlhdon_data = new Dictionary<string, object>();
                                dlhdon_data["DLHDON_ID"] = hoa_don.DLHDon.Id;
                                if (hoa_don.DLHDon.TTKhac != null)
                                    foreach (var item in hoa_don.DLHDon.TTKhac.TTin)
                                    {
                                        dlhdon_data["DLHDON_" + ChuyenMaTiengViet.ToUnSign(ObjectAndString.TrimSpecial(item.TTruong, " "))] = item.DLieu;
                                    }

                                var nban_data = GetDataXml(hoa_don.DLHDon.NDHDon.NBan);

                                var nmua_data = GetDataXml(hoa_don.DLHDon.NDHDon.NMua);
                                var ttoan_data = GetDataXml(hoa_don.DLHDon.NDHDon.TToan);

                                foreach (var item in hoa_don.DLHDon.NDHDon.DSHHDVu.HHDVu)
                                {
                                    var crow_data = GetDataXml(item);

                                    var new_row = new Dictionary<string, object>();
                                    new_row["_STT_"] = STT;
                                    new_row.AddRange(ttchung_data);
                                    new_row.AddRange(dlhdon_data);
                                    new_row.AddRange(nban_data, "NBAN_");
                                    new_row.AddRange(nmua_data, "NMUA_");
                                    new_row.AddRange(crow_data);

                                    //list_full_data.Add(new_row);

                                    full_table.AddRow(new_row, true);
                                }

                                ok_count++;
                            }
                            catch (Exception ex)
                            {
                                Continue_download_list.Add(new Dictionary<string, object>(crowexcelData));
                                err_count++;
                                err_string += "\n" + ex.Message;
                            }
                        }
                        else
                        {
                            Continue_download_list.Add(new Dictionary<string, object>(crowexcelData));
                            fail_count++;
                            err_string += "\nKhông tải được hd " + string.Format("{0}_{1}_{2}", khhdon, khmshdon, shdon);
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                err_string += "\n" + ex.Message;
                return true;
            }
            return true;
        }


        internal DataTable DownloadDataDetail(DataGridView dataGridView1, out string err_string)
        {   
            err_string = "";

            try
            {
                if (ContinueDownload(out err_string))
                {
                    return full_table;
                }

                full_table = new DataTable("full_table");

                if (ALIM2XLS_Config != null && ALIM2XLS_Config.HaveInfo
                    && ALIM2XLS_Config.EXTRA_INFOR != null && ALIM2XLS_Config.EXTRA_INFOR.ContainsKey("CQT_DAYS"))
                {
                    txtCQT_DAYS.Text = ALIM2XLS_Config.EXTRA_INFOR["CQT_DAYS"];
                }
                // Check CQT_DAYS
                if (chkCQT_DAYS.Checked)
                {
                    var num = ObjectAndString.ObjectToInt(txtCQT_DAYS.Text);
                    if (num < 1) num = 1;
                    if ((dateNgay_ct2.Value.Date - dateNgay_ct1.Value.Date).Days > num)
                    {
                        dateNgay_ct2.Value = dateNgay_ct1.Value.AddDays(num);
                    }
                }
                // Tải danh sách
                string excelFile = Path.Combine(V6Setting.V6SoftLocalAppData_Directory, "Invoice_"
                    + dateNgay_ct1.Value.ToString("yyyyMMdd") + "_"
                    + dateNgay_ct2.Value.ToString("yyyyMMdd") + ".xlsx");
                string s = api.DownloadInvoiceList_Excel(dateNgay_ct1.Value.Date, dateNgay_ct2.Value.Date, excelFile);

                if (string.IsNullOrEmpty(s))
                {
                    var excelData = ReadExcel(excelFile);
                    total = excelData.Rows.Count;
                    ok_count = 0; fail_count = 0; err_count = 0;

                    // Duyệt data, tải chi tiết.
                    Continue_download_list = new List<Dictionary<string, object>>();
                    foreach (DataRow crow_Excel in excelData.Rows)
                    {
                        var crowexcelData = crow_Excel.ToDataDictionary();
                        string STT = crowexcelData["STT"].ToString();
                        string nbmst = txtUserName.Text;// crowexcelData["MST người bán/MST người xuất hàng".ToUpper()].ToString();
                        string khhdon = crowexcelData["Ký hiệu hóa đơn".ToUpper()].ToString();
                        string shdon = crowexcelData["Số hóa đơn".ToUpper()].ToString();
                        string khmshdon = crowexcelData["Ký hiệu mẫu số".ToUpper()].ToString();
                        string saveFileZip = Path.Combine(V6Setting.V6SoftLocalAppData_Directory, "invoice_" + shdon + ".zip");
                        string one = api.DownloadInvoiceXml_zip(nbmst, khhdon, shdon, khmshdon, saveFileZip);
                        if (string.IsNullOrEmpty(one) && File.Exists(saveFileZip))
                        {
                            try
                            {
                                // Đọc trong file zip, load invoice.xml
                                var xdoc = new XmlDocument();
                                using (Stream stream = File.OpenRead(saveFileZip))
                                {
                                    var reader = ReaderFactory.Open(stream);
                                    while (reader.MoveToNextEntry())
                                    {
                                        if (!reader.Entry.IsDirectory)
                                        {
                                            if (reader.Entry.Key == "invoice.xml")
                                            {
                                                xdoc.Load(reader.OpenEntryStream());
                                                break;
                                            }
                                        }
                                    }
                                }

                                var hoa_don = new HDon(xdoc);

                                var ttchung_data = GetDataXml(hoa_don.DLHDon.TTChung);

                                var dlhdon_data = new Dictionary<string, object>();
                                dlhdon_data["DLHDON_ID"] = hoa_don.DLHDon.Id;
                                if (hoa_don.DLHDon.TTKhac != null)
                                    foreach (var item in hoa_don.DLHDon.TTKhac.TTin)
                                    {
                                        dlhdon_data["DLHDON_" + ChuyenMaTiengViet.ToUnSign(ObjectAndString.TrimSpecial(item.TTruong, " "))] = item.DLieu;
                                    }

                                var nban_data = GetDataXml(hoa_don.DLHDon.NDHDon.NBan);

                                var nmua_data = GetDataXml(hoa_don.DLHDon.NDHDon.NMua);
                                var ttoan_data = GetDataXml(hoa_don.DLHDon.NDHDon.TToan);

                                foreach (var item in hoa_don.DLHDon.NDHDon.DSHHDVu.HHDVu)
                                {
                                    var crow_data = GetDataXml(item);

                                    var new_row = new Dictionary<string, object>();
                                    new_row["_STT_"] = STT;
                                    new_row.AddRange(ttchung_data);
                                    new_row.AddRange(dlhdon_data);
                                    new_row.AddRange(nban_data, "NBAN_");
                                    new_row.AddRange(nmua_data, "NMUA_");
                                    new_row.AddRange(crow_data);

                                    //list_full_data.Add(new_row);

                                    full_table.AddRow(new_row, true);
                                }

                                ok_count++;
                            }
                            catch (Exception ex)
                            {
                                Continue_download_list.Add(new Dictionary<string, object>(crowexcelData));
                                err_count++;
                                err_string += "\n" + ex.Message;
                            }
                        }
                        else
                        {
                            Continue_download_list.Add(new Dictionary<string, object>(crowexcelData));
                            fail_count++;
                            err_string += "\nKhông tải được hd " + string.Format("{0}_{1}_{2}", khhdon, khmshdon, shdon);
                        }
                    }
                    
                }
                else
                {
                    err_string += "Không lấy được danh sách. Hãy thử lại!";
                    this.ShowErrorMessage(s);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(ex);
            }
            return full_table;
        }

        internal DataTable DownloadDataDetail_Purchase(DataGridView dataGridView1, out string err_string)
        {   
            err_string = "";

            try
            {
                if (ContinueDownload(out err_string))
                {
                    return full_table;
                }

                full_table = new DataTable("full_table");
                // Check CQT_DAYS
                if (chkCQT_DAYS.Checked)
                {
                    var num = ObjectAndString.ObjectToInt(txtCQT_DAYS.Text);
                    if (num < 1) num = 1;
                    if ((dateNgay_ct2.Value.Date - dateNgay_ct1.Value.Date).Days > num)
                    {
                        dateNgay_ct2.Value = dateNgay_ct1.Value.AddDays(num);
                    }
                }
                // Tải danh sách
                string excelFile = Path.Combine(V6Setting.V6SoftLocalAppData_Directory, "Purchase_"
                    + dateNgay_ct1.Value.ToString("yyyyMMdd") + "_"
                    + dateNgay_ct2.Value.ToString("yyyyMMdd") + ".xlsx");
                string s = api.DownloadPurchaseInvoiceList_Excel(dateNgay_ct1.Value.Date, dateNgay_ct2.Value.Date, excelFile);

                if (string.IsNullOrEmpty(s))
                {
                    var excelData = ReadExcel(excelFile);
                    total = excelData.Rows.Count;
                    ok_count = 0; fail_count = 0; err_count = 0;
                    
                    // Duyệt data, tải chi tiết.
                    
                    foreach (DataRow crow_Excel in excelData.Rows)
                    {   
                        var crowexcelData = crow_Excel.ToDataDictionary();
                        string STT = crowexcelData["STT"].ToString();
                        string nbmst = crowexcelData["MST người bán/MST người xuất hàng".ToUpper()].ToString();
                        string khhdon = crowexcelData["Ký hiệu hóa đơn".ToUpper()].ToString();
                        string shdon = crowexcelData["Số hóa đơn".ToUpper()].ToString();
                        string khmshdon = crowexcelData["Ký hiệu mẫu số".ToUpper()].ToString();
                        string saveFileZip = Path.Combine(V6Setting.V6SoftLocalAppData_Directory, "invoice_" +shdon+ ".zip");
                        string one = api.DownloadInvoiceXml_zip(nbmst, khhdon, shdon, khmshdon, saveFileZip);
                        if (string.IsNullOrEmpty(one) && File.Exists(saveFileZip))
                        {
                            try
                            {
                                // giải nén và đọc file zip.
                                //var a = SharpCompress.Archives.Zip.ZipArchive.Open(saveFileZip);
                                //var directoryPath = Path.Combine(V6Setting.V6SoftLocalAppData_Directory, "invoice_" + shdon);
                                //if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

                                //ExtractionOptions op = new ExtractionOptions()
                                //{
                                //    ExtractFullPath = true,
                                //    Overwrite = true
                                //};

                                var xdoc = new XmlDocument();

                                using (Stream stream = File.OpenRead(saveFileZip))
                                {
                                    var reader = ReaderFactory.Open(stream);

                                    while (reader.MoveToNextEntry())
                                    {
                                        if (!reader.Entry.IsDirectory)
                                        {
                                            // ghi 1 file trong gói zip vào thư mục
                                            //reader.WriteEntryToDirectory(directoryPath, op);
                                            if (reader.Entry.Key == "invoice.xml")
                                            {

                                                xdoc.Load(reader.OpenEntryStream());
                                                break;
                                            }
                                        }
                                    }
                                }


                                var hoa_don = new HDon(xdoc);

                                var ttchung_data = GetDataXml(hoa_don.DLHDon.TTChung);
                                
                                var dlhdon_data = new Dictionary<string, object>();
                                dlhdon_data["DLHDON_ID"] = hoa_don.DLHDon.Id;
                                if (hoa_don.DLHDon.TTKhac != null)
                                    foreach (var item in hoa_don.DLHDon.TTKhac.TTin)
                                    {
                                        dlhdon_data["DLHDON_" + ChuyenMaTiengViet.ToUnSign(ObjectAndString.TrimSpecial(item.TTruong, " "))] = item.DLieu;
                                    }

                                var nban_data = GetDataXml(hoa_don.DLHDon.NDHDon.NBan);
                                
                                var nmua_data = GetDataXml(hoa_don.DLHDon.NDHDon.NMua);
                                var ttoan_data = GetDataXml(hoa_don.DLHDon.NDHDon.TToan);
                                
                                foreach (var item in hoa_don.DLHDon.NDHDon.DSHHDVu.HHDVu)
                                {   
                                    var crow_data = GetDataXml(item);
                                    
                                    var new_row = new Dictionary<string, object>();
                                    new_row["_STT_"] = STT;
                                    new_row.AddRange(ttchung_data);
                                    new_row.AddRange(dlhdon_data);
                                    new_row.AddRange(nban_data, "NBAN_");
                                    new_row.AddRange(nmua_data, "NMUA_");
                                    new_row.AddRange(crow_data);

                                    //list_full_data.Add(new_row);

                                    full_table.AddRow(new_row, true);
                                }

                                ok_count++;
                            }
                            catch(Exception ex)
                            {
                                err_count++;
                                err_string += "\n" + ex.Message;
                            }
                        }
                        else
                        {
                            fail_count++;
                            err_string += "\nKhông tải được hd " + string.Format("{0}_{1}_{2}", khhdon, khmshdon, shdon);
                        }
                    }

                    // made full_table
                    //foreach (var row in list_full_data)
                    //{
                    //    foreach (KeyValuePair<string, object> cell in row)
                    //    {

                    //    }
                    //}

                    //dataGridView1.DataSource = full_table; // đã chuyển ra _Control timer_tick
                }
                else
                {
                    err_string += "Không lấy được danh sách. Hãy thử lại!";
                    this.ShowErrorMessage(s);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(ex);
            }
            return full_table;
        }

        
        public Dictionary<string, object> GetDataXml(AutoXml x)
        {
            var data = new Dictionary<string, object>(ObjectAndString.ObjectToDictionary(x).ToUpperKeys());
            if (x == null) return data;

            if (data.ContainsKey("TTKHAC")) data.Remove("TTKHAC");
            var xtype = x.GetType();
            var pinfo = xtype.GetProperty(nameof(TTKhac));
            if (pinfo != null)
            {
                var ttkhac = pinfo.GetValue(x, null) as TTKhac;
                if (ttkhac != null)
                {
                    foreach (var item in ttkhac.TTin)
                    {
                        object item_value = item.DLieu;
                        if (item.KDLieu == "numeric" || item.KDLieu == "numberic" || item.KDLieu == "double" || item.KDLieu == "decimal" || item.KDLieu == "Decimal")
                        {
                            item_value = ObjectAndString.StringToDecimal(item.DLieu);
                        }
                        else if (item.KDLieu == "string" || item.KDLieu == "String")
                        {
                            DoNothing();
                        }
                        else if (item.KDLieu.ToUpper() == "dateTime".ToUpper())
                        {
                            item_value = ObjectAndString.StringToDate(item_value.ToString());
                        }
                        else
                        {
                            DoNothing();
                        }

                        data[xtype.Name.ToUpper() + "_" + ChuyenMaTiengViet.ToUnSign(ObjectAndString.TrimSpecial(item.TTruong, " "))] = item_value;
                    }
                }
            }
                
            return data;
        }

        public DataTable ReadExcel(string excelFilePath)
        {
            IWorkbook workbook;
            using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read))
            {
                //workbook = new HSSFWorkbook(stream);
                workbook = new XSSFWorkbook(stream); // XSSFWorkbook for XLSX
            }

            var sheet = workbook.GetSheetAt(0); // zero-based index of your target sheet
            var dataTable = new DataTable(sheet.SheetName);

            // write the header row
            
            int rowcount = sheet.PhysicalNumberOfRows;

            var headerRow = sheet.GetRow(5);
            int colcount = headerRow.PhysicalNumberOfCells;
            var headerRow2 = sheet.GetRow(6);
            if (headerRow2 == null)
            {
                headerRow2 = headerRow;
            }
            for (int i = 0; i < headerRow.Cells.Count; i++)
            {
                var columnHeader = headerRow.Cells[i].ToString();
                var cell2 = headerRow2.Cells[i];

                switch (cell2.CellType)
                {
                    case CellType.Unknown:
                        dataTable.Columns.Add(columnHeader, typeof(string));
                        break;
                    case CellType.Numeric:
                        dataTable.Columns.Add(columnHeader, typeof(decimal));
                        break;
                    case CellType.String:
                        dataTable.Columns.Add(columnHeader, typeof(string));
                        break;
                    case CellType.Formula:
                        dataTable.Columns.Add(columnHeader, typeof(string));
                        break;
                    case CellType.Blank:
                        dataTable.Columns.Add(columnHeader, typeof(string));
                        break;
                    case CellType.Boolean:
                        dataTable.Columns.Add(columnHeader, typeof(bool));
                        break;
                    case CellType.Error:
                        dataTable.Columns.Add(columnHeader, typeof(string));
                        break;
                    default:
                        dataTable.Columns.Add(columnHeader, typeof(string));
                        break;
                }
            }

            // write the rest
            for (int r = 6; r <= sheet.LastRowNum; r++)
            {
                var sheetRow = sheet.GetRow(r);
                Dictionary<string, object> rowdata = new Dictionary<string, object>();
                for (int i = 0; i < colcount; i++)
                {
                    var cell = sheetRow.Cells[i];
                    string key = headerRow.Cells[i].ToString().ToUpper();
                    object cell_value = null;
                    switch (cell.CellType)
                    {
                        case CellType.Unknown:
                            cell_value = cell.StringCellValue;
                            break;
                        case CellType.Numeric:
                            cell_value = cell.NumericCellValue;
                            break;
                        case CellType.String:
                            cell_value = cell.StringCellValue;
                            break;
                        case CellType.Formula:
                            cell_value = cell.StringCellValue;
                            break;
                        case CellType.Blank:
                            cell_value = cell.StringCellValue;
                            break;
                        case CellType.Boolean:
                            cell_value = cell.BooleanCellValue;
                            break;
                        case CellType.Error:
                            cell_value = cell.StringCellValue;
                            break;
                        default:
                            cell_value = cell.StringCellValue;
                            break;
                    }
                    rowdata[key] = cell_value;
                }

                dataTable.AddRow(rowdata);
            }
            return dataTable;
        }

        internal void ProgressBar1Update()
        {
            try
            {
                per_cent = (ok_count + fail_count + err_count) * 100 / total;
                lblTOTAL.Text = "Tổng: " + total;
                lblOK.Text = "Tải ok: " + ok_count;
                lblFAIL.Text = "Tải lỗi: " + fail_count;
                lblERR.Text = "Đọc Lỗi: " + err_count;
                progressBar1.Value = per_cent;
            }
            catch (Exception ex)
            {

            }
        }
        

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Check2 = chkDeleteOldExcel.Checked;
        }

        private void btnSuaChiTieu_Click(object sender, EventArgs e)
        {
            string tableName = "ALIM2XLS";
            string keys = "MA_CT";
            var data = V6BusinessHelper.Select(tableName, "*", "MA_CT = '"+ ALIM2XLS_Config.MA_CT+ "'").Data;
            V6ControlFormHelper.ShowDataEditorForm(this, data, tableName, null, keys, false, false);
        }

        private void btnXemMauExcel_Click(object sender, EventArgs e)
        {
            V6ControlFormHelper.OpenExcelTemplate("POA_ALL.XLS", V6Setting.IMPORT_EXCEL);
        }

        private void chkAutoSoCt_CheckedChanged(object sender, EventArgs e)
        {
            ObjectDictionary["AUTOSOCT"] = chkAutoSoCt.Checked;
            Check3 = chkAutoSoCt.Checked;
        }

        private void txtCaptcha_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }


        private void EIVPOA_Filter_Load(object sender, EventArgs e)
        {
            txtUserName.Text = "3700360123";
            txtPassword.Text = "123456aA$";
            btnCaptcha.PerformClick();
        }
        
        
    }
}
