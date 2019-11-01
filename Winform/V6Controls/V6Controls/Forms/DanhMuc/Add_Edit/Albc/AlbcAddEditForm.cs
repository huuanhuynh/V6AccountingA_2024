using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Forms.DanhMuc.Add_Edit.PhanQuyen;
using V6Controls.Forms.Editor;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit.Albc
{
    public partial class AlbcAddEditForm : AddEditControlVirtual
    {
        public AlbcAddEditForm()
        {
            InitializeComponent();
            MyInit1();
        }

        private void MyInit1()
        {
            try
            {
                LoadColorNameList();
                EnablePhanQuyen();
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("AlbcAddEdit Init" + ex.Message, Application.ProductName);
            }
        }

        private bool _ready_nd51 = false;
        private void AlbcAddEditForm_Load(object sender, EventArgs e)
        {
            try
            {
                chknd51.Checked = (1 & (int)txtND51.Value) > 0;
                chkCheckPrint.Checked = (2 & (int)txtND51.Value) > 0;
                _ready_nd51 = true;

                chkRight_YN_CheckedChanged(null, null);
                EnableFieldsSelector();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".AlbcAddEditForm_Load", ex);
            }
        }

        private void EnableFieldsSelector()
        {
            DataGridView dataGridView1 = V6ControlFormHelper.GetControlByName(_grandFatherControl, "dataGridView1") as DataGridView;
            if (dataGridView1 == null)
            {
                ShowTopLeftMessage("Không tìm thấy dataGridView1!");
                return;
            }
            DataTable data1 = dataGridView1.DataSource as DataTable;

            if (data1 != null)
            {
                btnEXCEL1.Enabled = true;
                btnGRDS_V1.Enabled = true;
                btnNoSum1.Enabled = true;
                
                DataGridView dataGridView2 = V6ControlFormHelper.GetControlByName(_grandFatherControl, "dataGridView2") as DataGridView;
                if (dataGridView2 == null)
                {
                    ShowTopLeftMessage("Không tìm thấy dataGridView2!");
                    return;
                }
                DataTable data2 = dataGridView2.DataSource as DataTable;
                DataView data2v = dataGridView2.DataSource as DataView;
                if (data2 != null || data2v != null)
                {
                    btnGRDS_V2.Enabled = true;
                    btnNoSum2.Enabled = true;
                }
            }
        }

        private void EnablePhanQuyen()
        {
            if (V6Login.IsAdmin)
            {
                chkInherit_ch.Enabled = true;
                btnPhanQuyenUser.Enabled = true;
                chkRight_YN.Enabled = true;
            }
        }

        private void LoadColorNameList()
        {
            //List<string> theList = Enum.GetValues(typeof(KnownColor)).Cast<string>().ToList();
            List<string> colorList = new List<string>();
            foreach (object value in Enum.GetValues(typeof(KnownColor)))
            {
                colorList.Add(value.ToString());
            }
            cboColorList.DataSource = colorList;
            cboColorList.SelectedIndex = -1;
        }

        public override void DoBeforeEdit()
        {
            txtReportFileEnd.ReadOnly = true;
            var reportFile = DataOld["REPORT"].ToString().Trim();
            var ma_file = DataOld["MA_FILE"].ToString().Trim();
            if (reportFile.StartsWith(ma_file))
            {
                txtReportFileEnd.Text = reportFile.Substring(ma_file.Length);
            }
        }

        public override void DoBeforeCopy()
        {
            txtMa_File.ReadOnly = true;
            chkStatus.Checked = true;
        }

        public override void DoBeforeAdd()
        {
            txtMa_File.ReadOnly = false;
            chkStatus.Checked = true;
        }

        private bool _firstAddF3 = false;
        public override void ValidateData()
        {
            var errors = "";
            
            if (Mode == V6Mode.Add)
            {
                bool firstAdd = false;
                if (_firstAddF3 || (DataOld.ContainsKey("FirstAdd") && DataOld["FirstAdd"].ToString() == "1"))
                {
                    firstAdd = true;
                    DataDic["ISUSER"] = 0;
                    txtIsUser.Text = "0";
                }
                else
                {
                    DataDic["ISUSER"] = 1;
                    txtIsUser.Text = "1";
                }

                if (!firstAdd && txtReportFileEnd.Text.Trim() == "")
                {
                    errors += "Cần nhập tên file!\n";
                }

                if (!firstAdd && DataOld["REPORT"].ToString().Trim().ToUpper() == ReportFileNew.Trim().ToUpper())
                {
                    errors += "Tên FILE REPORT mới phải khác tên cũ!\n";
                    txtReportFileEnd.Focus();
                }
                else
                {
                    //Kiểm tra tồn tại
                    var data = new SortedDictionary<string, object>
                    {
                        {"MA_FILE", txtMa_File.Text.Trim()},
                        {"MAU", txtMAU.Text},
                        {"LAN", txtLAN.Text},
                        {"REPORT", txtReportFileNew.Text}
                    };
                    if (V6BusinessHelper.CheckDataExist("Albc", data))
                    {
                        errors += "Tên FILE REPORT đã tồn tại. Vui lòng đổi thông tin khác!\n";
                    }
                    //Kiểm tra reportFile trùng mã file
                    if (txtReportFileNew.Text.ToUpper() != txtMa_File.Text.ToUpper())
                    {
                        var key = new SortedDictionary<string, object>
                        {
                            {"MA_FILE", txtReportFileNew.Text}
                        };
                        if (V6BusinessHelper.CheckDataExist("Albc", key))
                        {
                            errors += "Tên FILE REPORT trùng mã file của report khác. Vui lòng đổi thông tin khác!\n";
                        }
                    }

                }

                if (errors.Length == 0)
                {
                    bool ok = true;
                    string old_dir = "";
                    if (DataOld.ContainsKey("RPT_DIR") && DataOld["RPT_DIR"].ToString().Trim() != "")
                    {
                        old_dir = DataOld["RPT_DIR"].ToString().Trim() + @"\";
                    }

                    var ReportFileFullOld = @"Reports\" + old_dir
                                            + DataOld["MAU"] + @"\" + DataOld["LAN"] + @"\" +
                                            DataOld["REPORT"].ToString().Trim() + ".rpt";
                    var ExcelFileFullOld = @"Reports\" + old_dir
                                            + DataOld["MAU"] + @"\" + DataOld["LAN"] + @"\" +
                                            DataOld["REPORT"].ToString().Trim() + ".xls";

                    string new_dir = "";
                    if (DataDic.ContainsKey("RPT_DIR") && DataDic["RPT_DIR"].ToString().Trim() != "")
                    {
                        new_dir = DataDic["RPT_DIR"].ToString().Trim() + @"\";
                    }

                    var ReportFileFullNew = @"Reports\" + new_dir
                                            + DataDic["MAU"] + @"\" + DataDic["LAN"] + @"\" +
                                            DataDic["REPORT"].ToString().Trim() + ".rpt";
                    var ExcelFileFullNew = @"Reports\" + new_dir
                                            + DataDic["MAU"] + @"\" + DataDic["LAN"] + @"\" +
                                            DataDic["REPORT"].ToString().Trim() + ".xls";
                    
                    if (ReportFileFullNew != ReportFileFullOld)
                    {
                        if (File.Exists(ReportFileFullNew))
                        {
                            if (this.ShowConfirmMessage("Có muốn chép đè mẫu cũ?") != DialogResult.Yes)
                                ok = false;
                        }

                        if (ok && errors.Length == 0)
                        {
                            try
                            {
                                File.Copy(ReportFileFullOld, ReportFileFullNew.ToUpper(), true);
                                if (File.Exists(ExcelFileFullOld))
                                    File.Copy(ExcelFileFullOld, ExcelFileFullNew.ToUpper(), true);
                            }
                            catch (Exception ex)
                            {
                                this.WriteExLog(GetType() + ".ValidateData", ex);
                            }
                        }
                    }
                }
            }
            else
            {
                
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                V6ControlFormHelper.ShowHelp("ALBCADDEDIT", V6Text.Help, this);
                return true;
            }
            return base.DoHotKey0(keyData);
        }

        public string ReportFileNew
        {
            get { return txtMa_File.Text + txtReportFileEnd.Text.Trim().ToUpper(); }
        }
        
        private void txtReportFileEnd_TextChanged(object sender, EventArgs e)
        {
            if (IsReady)
            txtReportFileNew.Text = ReportFileNew;
        }

        private void txtMa_File_TextChanged(object sender, EventArgs e)
        {
            if (IsReady)
            txtReportFileNew.Text = ReportFileNew;
        }

        private void btnExcel2_Click(object sender, EventArgs e)
        {
            var file_xml = txtMa_File.Text.Trim().ToUpper() + ".xml";

            new XmlEditorForm(txtExcel2, file_xml, "ExcelConfig", null).ShowDialog(this);
        }

        private void btnExcelTemplate_Click(object sender, EventArgs e)
        {
            var EXCEL_FILE = @"Reports\"
                + txtMAU.Text.Trim() + @"\" + txtLAN.Text.Trim() + @"\" +
                txtReportFileNew.Text.Trim() + ".xls";
            V6ControlFormHelper.RunProcess(EXCEL_FILE);
        }

        private void chkPrinterYn_CheckedChanged(object sender, EventArgs e)
        {
            if (IsReady)
            {
                btnPrinterSelect.Enabled = chkPrinterYn.Checked;
            }
        }

        private void btnPrinterSelect_Click(object sender, EventArgs e)
        {
            PrintDialog pt = new PrintDialog();
            if (pt.ShowDialog(this) == DialogResult.OK)
            {
                txtPrinterDef.Text = pt.PrinterSettings.PrinterName;
            }
        }
        private void btnPhanQuyenUser_Click(object sender, EventArgs e)
        {
            try
            {
                using (PhanQuyenNSD phnQuyenF = new PhanQuyenNSD
                {
                    Text = "Phân quyền người sử dụng.",
                    Vrights_user = txtRightUser.Text
                })
                {
                    if (phnQuyenF.ShowDialog(this) == DialogResult.OK)
                    {
                        txtRightUser.Text = phnQuyenF.Vrights_user;
                    }
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("PhanQuyenUser: " + ex.Message);
            }
        }

        private void chkRight_YN_CheckedChanged(object sender, EventArgs e)
        {
            if (IsReady)
            {
                btnPhanQuyenUser.Enabled = chkRight_YN.Checked;
                chkInherit_ch.Enabled = chkRight_YN.Checked;
            }
        }
        
        private void cboColorList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboColorList.SelectedIndex == -1)
            {
                lblTenMau.BackColor = Color.Transparent;
            }
            else
            {
                lblTenMau.BackColor = Color.FromName(cboColorList.Text);
            }
        }

        private void btnExcelTemplateView_Click(object sender, EventArgs e)
        {
            var EXCEL_FILE = @"Reports\"
                + txtMAU.Text.Trim() + @"\" + txtLAN.Text.Trim() + @"\" +
                txtReportFileNew.Text.Trim() + "_view.xls";
            V6ControlFormHelper.RunProcess(EXCEL_FILE);
        }

        private void btnExcel2View_Click(object sender, EventArgs e)
        {
            var file_xml2 = txtMa_File.Text.Trim().ToUpper()+"_VIEW" + ".xml";
            new XmlEditorForm(txtExcel2View, file_xml2, "ExcelConfig", null).ShowDialog(this);
        }

        public override void V6F3Execute()
        {
            _firstAddF3 = true;
            //Visible
            txtMAU.Visible = true;
            txtLAN.Visible = true;
            txtIsUser.Visible = true;
            txtExcel2.Visible = true;
            txtExcel2View.Visible = true;
            lblXml.Visible = true;
            txtDmethod.Visible = true;
            btnEditXml.Visible = true;
            lblThongTinThem.Visible = true;
            txtExtraInfo.Visible = true;
            txtRPT_DIR.Visible = true;
            txtRPT_DIR.Enabled = true;
            txtRPT_DIR.ReadOnly = false;
            lblRPT_DIR.Visible = true;

            //ReadOnly
            txtMAU.ReadOnly = false;
            txtLAN.ReadOnly = false;
            txtIsUser.ReadOnly = false;
            txtExcel2.ReadOnly = false;
            txtExcel2View.ReadOnly = false;
            txtMa_File.ReadOnly = false;
            txtReportFileNew.ReadOnly = false;
        }

        public override void V6F3ExecuteUndo()
        {
            //Visible
            txtMAU.Visible = false;
            txtLAN.Visible = false;
            txtIsUser.Visible = false;
            txtExcel2.Visible = false;
            txtExcel2View.Visible = false;

            //ReadOnly
            txtMAU.ReadOnly = true;
            txtLAN.ReadOnly = true;
            txtIsUser.ReadOnly = true;
            txtExcel2.ReadOnly = true;
            txtExcel2View.ReadOnly = true;
            txtMa_File.ReadOnly = true;
            txtReportFileNew.ReadOnly = true;
        }

        private void btnEditXml_Click(object sender, EventArgs e)
        {
            DoEditXml();
        }

        private void DoEditXml()
        {
            try
            {
                var file_xml = txtMa_File.Text.Trim().ToUpper() + ".xml";
                new XmlEditorForm(txtDmethod, file_xml, "Table0", "event,using,method,content".Split(',')).ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoEditXml", ex);
            }
        }


        private void DoExportXml()
        {
            try
            {
                var saveFile = V6ControlFormHelper.ChooseSaveFile(this, "Xml|*.xml", txtReportFileNew.Text + ".xml");
                if (string.IsNullOrEmpty(saveFile)) return;

                DataSet exportDataSet = new DataSet("ALBCEXCELFORMAT");
                DataTable dataTable = new DataTable("ExcelConfig");
                IDictionary<string, object> data = V6ControlFormHelper.GetFormDataDictionary(tabPage1);
                //Bỏ qua một số dữ liệu
                if (data.ContainsKey("EXTRA_PARA")) data.Remove("EXTRA_PARA");
                if (data.ContainsKey("MMETHOD")) data.Remove("MMETHOD");
                dataTable.AddRow(data, true);
                exportDataSet.Tables.Add(dataTable.Copy());
                
                FileStream fs = new FileStream(saveFile, FileMode.Create);
                exportDataSet.WriteXml(fs);
                fs.Close();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoExportXml", ex);
            }
        }

        private void DoImportXml()
        {
            try
            {
                var openFile = V6ControlFormHelper.ChooseOpenFile(this, "Xml|*.xml");
                if (string.IsNullOrEmpty(openFile)) return;
                FileStream fs = new FileStream(openFile, FileMode.Open);
                DataSet exportDataSet = new DataSet("ALBCEXCELFORMAT");
                exportDataSet.ReadXml(fs);
                fs.Close();
                if (exportDataSet.Tables.Count > 0 && exportDataSet.Tables[0].Rows.Count > 0)
                {
                    var data = exportDataSet.Tables[0].Rows[0].ToDataDictionary();
                    //Bỏ qua một số dữ liệu
                    if (data.ContainsKey("EXTRA_PARA")) data.Remove("EXTRA_PARA");
                    if (data.ContainsKey("MMETHOD")) data.Remove("MMETHOD");
                    //Gán lên form.
                    V6ControlFormHelper.SetSomeDataDictionary(tabPage1, data);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoImportXml", ex);
            }
        }

        private void btnXuatXML_Click(object sender, EventArgs e)
        {
            DoExportXml();
        }

        private void btnNhapXML_Click(object sender, EventArgs e)
        {
            DoImportXml();
        }

        private void chknd51_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (_ready_nd51)
                {
                    string binary = chknd51.Checked ? "1" : "0";
                    binary = (chkCheckPrint.Checked ? "1" : "0") + binary;
                    int nd51 = Convert.ToInt32(binary, 2);
                    txtND51.Value = nd51;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".chknd51_CheckedChanged", ex);
            }
        }

        private void btnEXCEL1_Click(object sender, EventArgs e)
        {
            try
            {
                FieldSelectorForm form = new FieldSelectorForm();
                List<AlbcFieldInfo> targetInfoList = new List<AlbcFieldInfo>();
                var fff = ObjectAndString.SplitString(txtExcel1.Text);
                foreach (string field in fff)
                {
                    string FIELD = field.Trim().ToUpper();
                    AlbcFieldInfo fi = new AlbcFieldInfo()
                    {
                        FieldName = FIELD
                    };
                    targetInfoList.Add(fi);
                }
                List<AlbcFieldInfo> sourceFields = GetSourceFieldsInfo1();
                form.AddSourceFieldList(sourceFields);
                form.AddTargetFieldList(targetInfoList);

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    txtExcel1.Text = form.GetFieldsString();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnEXCEL1_Click", ex);
            }
        }

        /// <summary>
        /// Lấy thông tin đích 1
        /// </summary>
        /// <returns></returns>
        private List<AlbcFieldInfo> GetTargetFieldsInfo(string ssss, string ffff, string vvvv, string eeee, string tttt)
        {
            var targetInfoList = new List<AlbcFieldInfo>();
            var sss = ObjectAndString.SplitString(ssss);
            var fff = ObjectAndString.SplitString(ffff);    //  N0:100;C200;D250...
            var vvv = ObjectAndString.SplitString(vvvv);
            var eee = ObjectAndString.SplitString(eeee);
            var ttt = ObjectAndString.SplitString(tttt);
            for (int i = 0; i < sss.Length; i++)
            {
                string field = sss[i];
                string FIELD = field.Trim().ToUpper();
                string f = fff.Length <= i ? "C100" : fff[i];
                string fts = f.Substring(0, 1);
                string fws = f.Substring(1);
                if (fts == "N")
                {
                    if (f.Length > 1) fts = f.Substring(0, 2);
                    if (f.Length > 2) fws = f.Substring(3);
                    else fws = "100";
                }
                var ft = EnumConvert.FromString<AlbcFieldType>(fts);
                int fw = ObjectAndString.ObjectToInt(fws);
                string fhv = vvv.Length <= i ? CorpLan2.GetFieldHeader(FIELD, "V") : vvv[i];
                string fhe = eee.Length <= i ? CorpLan2.GetFieldHeader(FIELD, "E") : eee[i];
                bool fns = ttt.Length > i && ttt.Contains(FIELD);

                AlbcFieldInfo fi = new AlbcFieldInfo()
                {
                    FieldName = FIELD,
                    FieldType = ft,
                    FieldWidth = fw,
                    FieldHeaderV = fhv,
                    FieldHeaderE = fhe,
                    FieldNoSum = fns,
                };
                targetInfoList.Add(fi);
            }
            return targetInfoList;
        }

        private List<AlbcFieldInfo> GetSourceFieldsInfo1()
        {
            return GetSourceFieldsInfo("dataGridView1");
        }
        private List<AlbcFieldInfo> GetSourceFieldsInfo2()
        {
            return GetSourceFieldsInfo("dataGridView2");
        }

        /// <summary>
        /// Lấy thông tin nguồn.
        /// </summary>
        /// <returns></returns>
        private List<AlbcFieldInfo> GetSourceFieldsInfo(string dataGridViewName)
        {
            List<AlbcFieldInfo> result = new List<AlbcFieldInfo>();
            try
            {
                DataGridView dataGridView1 = V6ControlFormHelper.GetControlByName(_grandFatherControl, dataGridViewName) as DataGridView;
                if (dataGridView1 == null)
                {
                    ShowTopLeftMessage("Không tìm thấy " + dataGridViewName);
                    return result;
                }
                DataTable data1 = dataGridView1.DataSource as DataTable;
                if (data1 == null && dataGridView1.DataSource is DataView)
                {
                    data1 = ((DataView)dataGridView1.DataSource).Table;
                }
                
                if (data1 != null)
                {
                    foreach (DataColumn column in data1.Columns)
                    {
                        AlbcFieldInfo fi = new AlbcFieldInfo();
                        result.Add(fi);
                        fi.FieldName = column.ColumnName.ToUpper();
                        if (ObjectAndString.IsNumberType(column.DataType)) fi.FieldType = AlbcFieldType.N0;
                        else if (column.DataType == typeof (DateTime)) fi.FieldType = AlbcFieldType.D;
                        else fi.FieldType = AlbcFieldType.C;

                        var gColumn = dataGridView1.Columns[fi.FieldName];
                        if (gColumn != null)
                        {
                            fi.FieldWidth = gColumn.Width;
                        }
                        else
                        {
                            fi.FieldWidth = 100;
                        }

                        fi.FieldHeaderV = CorpLan2.GetFieldHeader(fi.FieldName, "V");
                        fi.FieldHeaderE = CorpLan2.GetFieldHeader(fi.FieldName, "E");
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".GetSourceFieldsInfo", ex);
            }
            return result;
        }

        

        private void btnGRDS_V1_Click(object sender, EventArgs e)
        {
            try
            {
                FieldSelectorForm form = new FieldSelectorForm();
                List<AlbcFieldInfo> targetInfoList = GetTargetFieldsInfo(txtShowFields1.Text, txtFormats1.Text, txtHeaderV1.Text, txtHeaderE1.Text, txtNoSum1.Text);
                List<AlbcFieldInfo> sourceFields = GetSourceFieldsInfo1();
                form.AddSourceFieldList(sourceFields);
                form.AddTargetFieldList(targetInfoList);

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    txtShowFields1.Text = form.GetFieldsString();
                    txtFormats1.Text = form.GetFormatsString();
                    txtHeaderV1.Text = form.GetCaptionsStringV();
                    txtHeaderE1.Text = form.GetCaptionsStringE();
                    //txtNoSum1.Text = form.GetNoSumFieldsString();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnGRDS_V1_Click", ex);
            }
        }

        private void btnGRDS_V2_Click(object sender, EventArgs e)
        {
            try
            {
                FieldSelectorForm form = new FieldSelectorForm();
                List<AlbcFieldInfo> targetInfoList = GetTargetFieldsInfo(txtShowFields2.Text, txtFormats2.Text, txtHeaderV2.Text, txtHeaderE2.Text, txtNoSum2.Text);
                List<AlbcFieldInfo> sourceFields = GetSourceFieldsInfo2();
                form.AddSourceFieldList(sourceFields);
                form.AddTargetFieldList(targetInfoList);

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    txtShowFields2.Text = form.GetFieldsString();
                    txtFormats2.Text = form.GetFormatsString();
                    txtHeaderV2.Text = form.GetCaptionsStringV();
                    txtHeaderE2.Text = form.GetCaptionsStringE();
                    //txtNoSum2.Text = form.GetNoSumFieldsString();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnGRDS_V2_Click", ex);
            }
        }

        private void ChooseNoSumColumns(TextBox txtNoSum)
        {
            try
            {
                FieldSelectorForm form = new FieldSelectorForm();
                List<AlbcFieldInfo> targetInfoList = new List<AlbcFieldInfo>();
                var fff = ObjectAndString.SplitString(txtNoSum.Text);
                foreach (string field in fff)
                {
                    string FIELD = field.Trim().ToUpper();
                    AlbcFieldInfo fi = new AlbcFieldInfo()
                    {
                        FieldName = FIELD
                    };
                    targetInfoList.Add(fi);
                }
                List<AlbcFieldInfo> sourceFields0 = txtNoSum == txtNoSum1 ? GetSourceFieldsInfo1() : GetSourceFieldsInfo2();
                var sourceFields = new List<AlbcFieldInfo>();
                foreach (AlbcFieldInfo info in sourceFields0)
                {
                    if (info.FieldType >= AlbcFieldType.N0 && info.FieldType <= AlbcFieldType.N5) sourceFields.Add(info);
                }
                form.AddSourceFieldList(sourceFields);
                form.AddTargetFieldList(targetInfoList);

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    txtNoSum.Text = form.GetFieldsString();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnNoSum1_Click", ex);
            }
        }

        private void btnNoSum1_Click(object sender, EventArgs e)
        {
            ChooseNoSumColumns(txtNoSum1);
        }

        private void btnNoSum2_Click(object sender, EventArgs e)
        {
            ChooseNoSumColumns(txtNoSum2);
        }
    }
}
