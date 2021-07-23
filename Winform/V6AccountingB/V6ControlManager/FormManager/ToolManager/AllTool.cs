using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using V6ControlManager.FormManager.ReportManager.DXreport;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.Viewer;
using V6Init;
using V6Tools;

namespace V6ControlManager.FormManager.ToolManager
{
    public partial class AllTool : V6FormControl
    {
        public AllTool()
        {
            InitializeComponent();
            MyInit();
        }

        public AllTool(string itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
            MyInit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (new ConfirmPasswordV6().ShowDialog(this) == DialogResult.OK)
            {
                new FormExportSqlTableToFile().Show();
            }
        }

        private void MyInit()
        {
            try
            {
                chkMouseRightTriple.Checked = V6Setting.Triple;
                chkAutoFixInvoiceVvar.Checked = V6Setting.FixInvoiceVvar;
                chkWriteExtraLog.Checked = V6Setting.WriteExtraLog;
                chkAllowAdd.Checked = V6Setting.V6Special_AllowAdd;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
            Ready();
        }

        public override void V6CtrlF12Execute()
        {
            grbTools.Visible = true;
            chkAutoFixInvoiceVvar.Visible = true;
            chkWriteExtraLog.Visible = true;
            chkAllowAdd.Visible = true;
            base.V6CtrlF12Execute();
        }

        private void btnTestInvoice_Click(object sender, EventArgs e)
        {
            if (new ConfirmPasswordV6().ShowDialog(this) == DialogResult.OK)
            {
                new FormTestInvoice().Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FormConvertTable().Show();
        }

        private void btnDBF_Click(object sender, EventArgs e)
        {
            new FormDBF().Show();
        }

        private void btnModelHelp_Click(object sender, EventArgs e)
        {
            new FormModelHelp().Show();
        }

        private void btnHdocument_Click(object sender, EventArgs e)
        {
            try
            {
                //new FormHdocumentEditor().Show();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void btnSearchInRpt_Click(object sender, EventArgs e)
        {
            try
            {
                new FormSearchRpt().Show();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void btnLogView_Click(object sender, EventArgs e)
        {
            new FormLogView().Show();
        }

        private void chkMouseRightTriple_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsReady) return;

            if (chkMouseRightTriple.Checked)
            {
                V6Setting.V6Special = V6Setting.V6Special + "Triple";
            }
            else
            {
                V6Setting.V6Special = V6Setting.V6Special.Replace("Triple", "");
            }
        }

        private void chkAutoFixInvoiceVvar_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsReady) return;

            if (chkAutoFixInvoiceVvar.Checked)
            {
                V6Setting.V6Special = V6Setting.V6Special + "FixInvoiceVvar";
            }
            else
            {
                V6Setting.V6Special = V6Setting.V6Special.Replace("FixInvoiceVvar", "");
            }
        }
        
        private void chkWriteExtraLog_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsReady) return;

            if (chkWriteExtraLog.Checked)
            {
                V6Setting.V6Special = V6Setting.V6Special + "WriteExtraLog";
            }
            else
            {
                V6Setting.V6Special = V6Setting.V6Special.Replace("WriteExtraLog", "");
            }
        }

        private void chkAllowAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsReady) return;

            if (chkAllowAdd.Checked)
            {
                V6Setting.V6Special = V6Setting.V6Special + "AllowAdd";
            }
            else
            {
                V6Setting.V6Special = V6Setting.V6Special.Replace("AllowAdd", "");
            }
        }

        private void btnSQL_Click(object sender, EventArgs e)
        {

        }

        private void btnAmLich_Click(object sender, EventArgs e)
        {
            new FormAmLich().Show(this);
        }
        
        private void btnChuyenMa_Click(object sender, EventArgs e)
        {
            new FormChuyenMa().Show(this);
        }

        private void btnChuyenMaExcel_Click(object sender, EventArgs e)
        {
            new FormConvertExcel().Show(this);
        }

        private void btnTeamViewer_Click(object sender, EventArgs e)
        {
            try
            {
                string file = "TeamViewerQS_en.exe";
                file = Path.GetFullPath(file);
                if (File.Exists(file))
                {
                    Process.Start(file);
                }
                else
                {
                    ShowMainMessage(V6Text.NotExist + " " + file);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void btnAnyDesk_Click(object sender, EventArgs e)
        {
            try
            {
                string file = "AnyDesk.exe";
                file = Path.GetFullPath(file);
                if (File.Exists(file))
                {
                    Process.Start(file);
                }
                else
                {
                    ShowMainMessage(V6Text.NotExist + " " + file);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void btnHDSDDT_Click(object sender, EventArgs e)
        {
            try
            {
                string file = "V6HELP\\HDSD_SHOW.ppsx";
                V6ControlFormHelper.OpenFileProcess(file);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        
        private void btnHDSDDT2_Click(object sender, EventArgs e)
        {
            try
            {
                string file = "V6HELP\\HDSD_SHOW_AUTONEXT.ppsx";
                V6ControlFormHelper.OpenFileProcess(file);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void btnSendToV6_Click(object sender, EventArgs e)
        {
            try
            {
                var file = V6ControlFormHelper.ChooseOpenFile(this, "All file|*.*");
                if (string.IsNullOrEmpty(file)) return;
                var _setting = new H.Setting(Path.Combine(V6Login.StartupPath, "Setting.ini"));
                var VPN_IP = _setting.GetSetting("VPN_IP");
                var VPN_USER = _setting.GetSetting("VPN_USER");
                var VPN_EPASS = _setting.GetSetting("VPN_EPASS");
                var VPN_SUBFOLDER = _setting.GetSetting("VPN_SUBFOLDER");
                V6FileIO.CopyToVPN(file, VPN_SUBFOLDER, VPN_IP, VPN_USER, VPN_EPASS);
            }
            catch (Exception ex)
            {
                this.ShowErrorException("AllTool.SendToV6", ex);
            }
        }

        private void btnQuayRa_Click(object sender, EventArgs e)
        {
            try
            {
                var p = Parent;
                Dispose();
                if (p is Form) p.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Close " + ex.Message);
            }
        }

        private void btnPDF_HTMLview_Click(object sender, EventArgs e)
        {
            try
            {
                string open_file_name = V6ControlFormHelper.ChooseOpenFile(this, "PDF|*.pdf|HTML|*.html");
                string ext = Path.GetExtension(open_file_name).ToLower();
                if (ext == ".pdf")
                {
                    PDF_ViewPrintForm view = new PDF_ViewPrintForm(open_file_name);
                    view.ShowDialog(this);
                }
                else if (ext == ".html")
                {
                    HtmlViewerForm view = new HtmlViewerForm(open_file_name, open_file_name, false);
                    view.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void btnXMLXSLview_Click(object sender, EventArgs e)
        {
            try
            {
                string open_file_name = V6ControlFormHelper.ChooseOpenFile(this, "zip|*.zip");
                XmlXslZipViewerForm view = new XmlXslZipViewerForm(open_file_name, "XML XSL", false);
                view.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void btnXtraEditor_Click(object sender, EventArgs e)
        {
            try
            {
                XtraEditorForm1 form1 = new XtraEditorForm1();
                form1.Show(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

       
    }
}
