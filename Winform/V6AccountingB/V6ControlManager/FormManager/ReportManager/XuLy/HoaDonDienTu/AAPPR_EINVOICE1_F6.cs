using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6ThuePostManager;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AAPPR_EINVOICE1_F6 : V6Form
    {
        public AAPPR_EINVOICE1_F6()
        {
            InitializeComponent();
            MyInit();
        }

        /// <summary>
        /// Dòng đang đứng.
        /// </summary>
        public DataGridViewRow SelectedGridViewRow { get { return xuLyBase1.dataGridView1.CurrentRow; } }
        public IDictionary<string, object> am_OLD;
        public string branch { get; set; }

        private void MyInit()
        {
            try
            {
                xuLyBase1.m_itemId = ItemID;
                xuLyBase1._program = "AAPPR_EINVOICE1";
                xuLyBase1._reportProcedure = "AAPPR_EINVOICE1F6";
                xuLyBase1._reportFile = "AAPPR_EINVOICE1F6";
                xuLyBase1._reportCaption = "AAPPR_EINVOICE1F6";
                xuLyBase1._reportCaption2 = "AAPPR_EINVOICE1F6";

                xuLyBase1._reportFileF5 = "";
                xuLyBase1._reportTitleF5 = "";
                xuLyBase1._reportTitle2F5 = "";
                xuLyBase1.ViewDetail = false;

                xuLyBase1.MyInitBase();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Init", ex);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            
        }
        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnHuy.PerformClick();
            }
            else if (keyData == (Keys.Control | Keys.Enter))
            {
                btnNhan.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected int _oldIndex = -1;
        
        private void btnTestViewXml_Click(object sender, EventArgs e)
        {
            try
            {
                if (xuLyBase1.dataGridView1.DataSource == null || xuLyBase1.dataGridView1.CurrentRow == null)
                {
                    return;
                }

                bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;

                var row = xuLyBase1.dataGridView1.CurrentRow;

                //string mode = row.Cells["Kieu_in"].Value.ToString();
                string soct = row.Cells["So_ct"].Value.ToString().Trim();
                string dir = row.Cells["Dir_in"].Value.ToString().Trim();
                string file = row.Cells["File_in"].Value.ToString().Trim();
                string fkey_hd = row.Cells["fkey_hd"].Value.ToString().Trim();
                string inputPattern = "";
                // nhập mẫu để lấy thông tin Metadata Viettel
                StringInput inputForm = new StringInput("Nhập mẫu hóa đơn. vd 1/001", "1/001");
                if (shift_is_down && inputForm.ShowDialog(this) == DialogResult.OK)
                {
                    inputPattern = inputForm.InputString;
                }

                SqlParameter[] plist =
                        {
                            new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                            new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                            new SqlParameter("@HoaDonMau","0"),
                            new SqlParameter("@isInvoice","1"),
                            new SqlParameter("@ReportFile",""),
                            new SqlParameter("@MA_TD1", xuLyBase1.FilterControl.String1),
                            new SqlParameter("@UserID", V6Login.UserId)
                        };

                DataSet ds = V6BusinessHelper.ExecuteProcedure(xuLyBase1._reportFile + "F9", plist);
                //DataTable data0 = ds.Tables[0];
                string result = "";//, error = "", sohoadon = "", id = "";
                var paras = new PostManagerParams
                {
                    AM_data = row.ToDataDictionary(),
                    DataSet = ds,
                    Mode = "TestViewE_T1" + (shift_is_down ? "_Shift" : ""),
                    Pattern = inputPattern,
                    Branch = branch,
                    Dir = dir,
                    FileName = file,
                    Key_Down = "TestViewE_T1" + (shift_is_down ? "_Shift" : ""),
                    RptFileFull = xuLyBase1.ReportFileFull,
                    Fkey_hd = fkey_hd,
                    Form = this,
                };
                result = PostManager.PowerPost(paras);
                Clipboard.SetText(result);
                //this.ShowMessage(result);
                AAPPR_SOA2_ViewXml viewer = new AAPPR_SOA2_ViewXml(result);
                viewer.MoreInfos = paras.Result.MoreInfos;
                viewer.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnTestViewXml_Click", ex);
            }
        }
    }
}
