using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.XuLy;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6ThuePostManager;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AAPPR_SOA3_filter : FilterBase
    {
        public override string MAU
        {
            get { return rTienViet.Checked ? "VN" : "FC"; }
            set { rTienViet.Checked = value == "VN"; }
        }

        public AAPPR_SOA3_filter()
        {
            InitializeComponent();
            F3 = true;
            F7 = true;
            F9 = true;
            
            dateNgay_ct1.SetValue(V6Setting.M_SV_DATE);
            dateNgay_ct2.SetValue(V6Setting.M_SV_DATE);

            TxtXtag.Text = "2";
            ctDenSo.Enabled = false;
            chkHoaDonDaIn.Checked = true;
            cboSendType.SelectedIndex = 0;

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
            TxtMa_ct.Text = "SOA";
            TxtMa_ct.Enabled = false;

           
            Txtnh_kh1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_kh2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_kh3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_kh4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_kh5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_kh6.VvarTextBox.SetInitFilter("loai_nh=6");
            lineNH_KH7.VvarTextBox.SetInitFilter("loai_nh=7");
            lineNH_KH8.VvarTextBox.SetInitFilter("loai_nh=8");
            lineNH_KH9.VvarTextBox.SetInitFilter("loai_nh=9");
        }

        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            String1 = (cboSendType.SelectedIndex + 1).ToString();
            String2 = MAU;
            String3 = TxtMa_ct.Text;

            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@ma_ct", TxtMa_ct.Text.Trim()));
            result.Add(new SqlParameter("@ma_td1", String1.Trim()));

            var and = radAnd.Checked;
            
            var cKey = "";
            

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS", "MA_SONB", "MA_MAUHD", "SO_SERI", "MA_BP", "MA_KH", "MA_NX", "MA_XULY", "STATUS_IN"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
                "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6","NH_KH7","NH_KH8","NH_KH9"
            }, and);
            var key2 = GetFilterStringByFields(new List<string>()
            {
               "MA_KHO"
            }, and);
           
            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey = string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey = string.Format("(1=2 OR {0})", key0);
                }
            }
            else
            {
                cKey = "1=1";
            }

            if (!string.IsNullOrEmpty(key1))
            {
                cKey = cKey + string.Format(" and ma_kh in (select ma_kh from alkh where {0})", key1);
            }
            if (!string.IsNullOrEmpty(key2))
            {
                cKey = cKey + string.Format(" AND STT_REC IN (SELECT STT_REC FROM AD81 WHERE NGAY_CT between '{1}' and '{2}' and ma_kho_i in (select ma_kho from alkho where {0}))",
                    key2, dateNgay_ct1.YYYYMMDD, dateNgay_ct2.YYYYMMDD);
            }
            switch (TxtXtag.Text.Trim())
            {
                case "0":
                    cKey = cKey + " and ( Xtag=' ' or Xtag IS NULL )";
                    break;
                case "1":
                    cKey = cKey + " and ( Xtag='1' OR Kieu_post='1' )";
                    break;
                case "2":
                    cKey = cKey + " and ( Xtag='2'  OR Kieu_post='2')";
                    break;
                case "5":
                    cKey = cKey + " and ( Xtag='5'  OR Kieu_post='5')";
                    break;
            }
            if (chkHoaDonDaIn.Checked)
            {
                cKey = cKey + " and [Sl_in] > 0";
            }
            else
            {
                cKey = cKey + " and [Sl_in] = 0";
            }


            // Tu so den so
            var tu_so = ctTuSo.Text.Trim().Replace("'", "");
            var den_so = ctDenSo.Text.Trim().Replace("'", "");
            var invoice = new V6AccountingBusiness.Invoices.V6Invoice81();
            var and_or = " and ";
            var tbL = "";
            //so chung tu
            if (chkLike.Checked)
            {
                if (tu_so != "")
                {
                    cKey += (cKey.Length > 0 ? and_or : "")
                       + string.Format("so_ct like '%{0}'",
                        tu_so + ((tu_so.Contains("_") || tu_so.Contains("%")) ? "" : "%"));
                }
            }
            else
            {
                var dinh_dang = invoice.Alct["DinhDang"].ToString().Trim();
                if (!string.IsNullOrEmpty(dinh_dang))
                {
                    if (tu_so != "") tu_so = (dinh_dang + tu_so).Right(dinh_dang.Length);
                    if (den_so != "") den_so = (dinh_dang + den_so).Right(dinh_dang.Length);
                }
                if (tu_so != "" && den_so == "")
                {
                    cKey += string.Format("{0} LTrim(RTrim({1}so_ct)) = '{2}'",
                        cKey.Length > 0 ? and_or : "",
                        tbL,
                        tu_so);
                }
                else if (tu_so == "" && den_so != "")
                {
                    cKey += string.Format("{0} LTrim(RTrim({1}so_ct)) = '{2}'",
                       cKey.Length > 0 ? and_or : "",
                       tbL,
                       den_so);
                }
                else if (tu_so != "" && den_so != "")
                {
                    cKey += string.Format("{0} (LTrim(RTrim({1}so_ct)) >= '{2}' and LTrim(RTrim({1}so_ct)) <= '{3}')",
                        cKey.Length > 0 ? and_or : "",
                        tbL,
                        tu_so, den_so)
                    ;
                }
            }


            result.Add(new SqlParameter("@advance", cKey));

          
            return result;
        }

        private void chkLike_CheckedChanged(object sender, System.EventArgs e)
        {
            ctDenSo.Enabled = !chkLike.Checked;
        }

        private void btnSuaChiTieu_Click(object sender, System.EventArgs e)
        {
            try
            {
                bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                bool ctrl_is_down = (ModifierKeys & Keys.Control) == Keys.Control;
                if (new ConfirmPasswordV6().ShowDialog(this) != DialogResult.OK) return;
                string tableName = "V6MAPINFO";
                if (shift_is_down) tableName = "V6MAPINFO1";
                string keys = "UID,MA_TD1";//+ma_td1   1:VIETTEL    2:VNPT    3:BKAV
                var data = V6BusinessHelper.Select(tableName, "*", "LOAI = '" + _reportFile + "' and (MA_TD1='" + String1 + "' or ma_td1='0' or ma_td1='') order by GROUPNAME,GC_TD1").Data;
                if (ctrl_is_down) // Xuất Excel
                {
                    if (data.Columns.Contains("UID")) data.Columns.Remove("UID");
                    string fileName = V6ControlFormHelper.ExportExcel_ChooseFile(this, data, tableName + "_" + String1 + cboSendType.Text);

                    if (V6Options.AutoOpenExcel)
                    {
                        V6ControlFormHelper.OpenFileProcess(fileName);
                    }
                    else
                    {
                        this.ShowInfoMessage(V6Text.ExportFinish);
                    }
                }
                else
                {
                    IDictionary<string, object> defaultData = new Dictionary<string, object>();
                    defaultData.Add("LOAI", _reportFile);
                    V6ControlFormHelper.ShowDataEditorForm(this, data, tableName, null, keys, false, false, true, true,
                        defaultData);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "btnSuaChiTieu_Click", ex);
            }
        }

        private void cboSendType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            String1 = (cboSendType.SelectedIndex + 1).ToString();
        }

        private void btnCheckConnection_Click(object sender, System.EventArgs e)
        {
            try
            {
                bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;

                AAPPR_SOA3 parentForm = FindParent<AAPPR_SOA3>() as AAPPR_SOA3;
                if (parentForm == null)
                {
                    this.ShowWarningMessage(V6Text.NotFound + " AAPPR_SOA3 form.");
                    return;
                }
                DataGridView dataGridView = parentForm.dataGridView1;
                var row = dataGridView.CurrentRow;
                if (row == null)
                {
                    this.ShowWarningMessage(V6Text.NoData);
                    return;
                }

                SqlParameter[] plist0 =
                {
                    new SqlParameter("@Loai", _reportFile),
                    new SqlParameter("@MA_TD1", String1),
                    new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                    new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                    new SqlParameter("@Ma_dvcs", row.Cells["MA_DVCS"].Value.ToString()),
                    new SqlParameter("@User_ID", V6Login.UserId),
                    new SqlParameter("@Advance", ""),
                };
                var map_table = V6BusinessHelper.ExecuteProcedure("VPA_GET_V6MAPINFO", plist0).Tables[0];
                
                if (shift_is_down)
                {
                    var site = PostManager.GetConfigBaseLink(map_table);
                    if (string.IsNullOrEmpty(site)) return;
                    System.Diagnostics.Process.Start(site);
                    return;
                }

                var pmparams1 = new PostManagerParams
                {
                    DataSet = map_table.DataSet,
                    Branch = String1,
                    //InvoiceNo = invoiceNo,
                    //Pattern = pattern,
                    //Serial = serial,
                    //strIssueDate = strIssueDate,
                    Mode = "CheckConnection",
                };
                string exception;
                var check = PostManager.PowerCheckConnection(pmparams1, out exception);
                if (string.IsNullOrEmpty(check))
                {
                    this.ShowInfoMessage(V6Text.ConnectionOk);
                }
                else
                {
                    if (!string.IsNullOrEmpty(exception))
                    {
                        this.ShowErrorMessage(exception);
                    }
                    this.ShowErrorMessage(check);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "btnCheckConnection", ex);
            }
        }

        
    }
}
