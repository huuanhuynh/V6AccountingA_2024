using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Forms.DanhMuc.Add_Edit.PhanQuyen;
using V6Init;
using V6Structs;
using V6Tools;

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
            chknd51.Checked = (1 & (int) txtND51.Value) > 0;
            chkCheckPrint.Checked = (2 & (int) txtND51.Value) > 0;
            _ready_nd51 = true;

            chkRight_YN_CheckedChanged(null, null);
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
                }

                if (errors.Length == 0)
                {
                    bool ok = true;
                    var ReportFileFullOld = @"Reports\"
                                            + DataOld["MAU"] + @"\" + DataOld["LAN"] + @"\" +
                                            DataOld["REPORT"].ToString().Trim() + ".rpt";
                    var ExcelFileFullOld = @"Reports\"
                                            + DataOld["MAU"] + @"\" + DataOld["LAN"] + @"\" +
                                            DataOld["REPORT"].ToString().Trim() + ".xls";

                    var ReportFileFullNew = @"Reports\"
                                            + DataDic["MAU"] + @"\" + DataDic["LAN"] + @"\" +
                                            DataDic["REPORT"].ToString().Trim() + ".rpt";
                    var ExcelFileFullNew = @"Reports\"
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
            if (f3count == 3)
            {
                f3count = 0;
                if (new ConfirmPasswordV6().ShowDialog(this) == DialogResult.OK)
                {
                    ShowTopLeftMessage("V6 Confirm ......OK....");
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

                    //ReadOnly
                    txtMAU.ReadOnly = false;
                    txtLAN.ReadOnly = false;
                    txtIsUser.ReadOnly = false;
                    txtExcel2.ReadOnly = false;
                    txtExcel2View.ReadOnly = false;
                    txtMa_File.ReadOnly = false;
                    txtReportFileNew.ReadOnly = false;
                }
            }
            else
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
                var saveFile = V6ControlFormHelper.ChooseSaveFile(this, "Xml|*.xml");
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
            catch (Exception)
            {
                
            }
        }
    }
}
