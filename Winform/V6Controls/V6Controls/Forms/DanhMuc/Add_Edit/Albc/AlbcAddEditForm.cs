using System;
using System.Collections.Generic;
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
            Ready();
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
                    if (V6BusinessHelper.CheckDataEsist("Albc", data))
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
            new AlbcExcel2EditorForm(txtExcel2).ShowDialog();
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
            if (pt.ShowDialog() == DialogResult.OK)
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
                    if (phnQuyenF.ShowDialog() == DialogResult.OK)
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
            new AlbcExcel2EditorForm(txtExcel2View).ShowDialog();
        }

        public override void V6F3Execute()
        {
            if (f3count == 2)
            {
                f3count = 0;
                if (new ConfirmPasswordV6().ShowDialog() == DialogResult.OK)
                {
                    ShowTopMessage("V6 Confirm ......OK....");
                    _firstAddF3 = true;
                    //Visible
                    txtMAU.Visible = true;
                    txtLAN.Visible = true;
                    txtIsUser.Visible = true;
                    txtExcel2.Visible = true;
                    txtExcel2View.Visible = true;

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
    }
}
