using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.XuLy;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AAPPR_XULY_ALL_Filter : FilterBase
    {
        public AAPPR_XULY_ALL_Filter()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                F3 = true;
                F4 = true;
                F7 = true;
                F9 = true;

                dateNgay_ct1.SetValue(V6Setting.M_SV_DATE);
                dateNgay_ct2.SetValue(V6Setting.M_SV_DATE);

                //TxtXtag.Text = "2";
                ctDenSo.Enabled = false;
                //chkHoaDonDaIn.Checked = true;

                txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

                if (V6Login.MadvcsCount <= 1)
                {
                    txtMaDvcs.Enabled = false;
                }
                SqlParameter[] plist =
                {
                    new SqlParameter("@Ma_ct", ""),
                    new SqlParameter("@User_id", V6Login.UserId),
                    new SqlParameter("@Advance", "M_MA_VV='1'"),
                };
                var data = V6BusinessHelper.ExecuteProcedure("VPA_GET_ALCTCT_DEFAULT", plist).Tables[0];
                if (data.Rows.Count > 0)
                {
                    txtMaCtProc.Text = data.Rows[0]["MA_CT"].ToString().Trim();
                }
                else
                {
                    txtMaCtProc.Text = "";
                }

                txtMaCtProc.SetInitFilter("M_MA_VV='1'");
                txtMaCtProc.Upper();

                LoadComboboxSource(txtMaCtProc.Text);

                Txtnh_kh1.VvarTextBox.SetInitFilter("loai_nh=1");
                Txtnh_kh2.VvarTextBox.SetInitFilter("loai_nh=2");
                Txtnh_kh3.VvarTextBox.SetInitFilter("loai_nh=3");
                Txtnh_kh4.VvarTextBox.SetInitFilter("loai_nh=4");
                Txtnh_kh5.VvarTextBox.SetInitFilter("loai_nh=5");
                Txtnh_kh6.VvarTextBox.SetInitFilter("loai_nh=6");
                lineNH_KH7.VvarTextBox.SetInitFilter("loai_nh=7");
                lineNH_KH8.VvarTextBox.SetInitFilter("loai_nh=8");
                lineNH_KH9.VvarTextBox.SetInitFilter("loai_nh=9");
                Ready();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        private void AAPPR_XULY_ALL_Filter_Load(object sender, EventArgs e)
        {
            txtMa_ct_V6LostFocus(null);
        }

        private void LoadComboboxSource(string maCt)
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@Ma_ct", txtMaCtProc.Text),
                    new SqlParameter("@User_id", V6Login.UserId),
                    new SqlParameter("@Advance", ""),
                };
                var data = V6BusinessHelper.ExecuteProcedure("VPA_GET_ALXULY", plist).Tables[0];

                cboMa_xuly.ValueMember = "MA_XULY";
                cboMa_xuly.DisplayMember = V6Setting.IsVietnamese ? "Ten_xuly" : "Ten_xuly2";
                //cboMa_xuly.DataSource = V6BusinessHelper.Select("Alxuly", "ma_xuly as MA_XULY1,Ten_xuly,Ten_xuly2",
                //                    "Ma_ct=@mact and Status = '1'", "", "Ma_xuly",
                //                    new SqlParameter("@mact", maCt)).Data;
                cboMa_xuly.DataSource = data;
                cboMa_xuly.ValueMember = "MA_XULY";
                cboMa_xuly.DisplayMember = V6Setting.IsVietnamese ? "Ten_xuly" : "Ten_xuly2";

                var viewXuly = new DataView(data);
                viewXuly.RowFilter = "Ma_ct='"+ maCt+ "' and Status='1' And SL_TD2=1";
                if (viewXuly.Count == 1)
                {
                    string selectValue = viewXuly.ToTable().Rows[0]["MA_XULY"].ToString().Trim();
                    if (!string.IsNullOrEmpty(selectValue)) cboMa_xuly.SelectedValue = selectValue;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadComboboxSource", ex);
            }
        }

        public override string Kieu_post
        {
            get
            {
                return cboMa_xuly.SelectedValue.ToString().Trim();
            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@ma_ct", txtMaCtProc.Text.Trim()));
            result.Add(new SqlParameter("@user_id", V6Login.UserId));

            var and = radAnd.Checked;
            
            var cKey = "";


            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS", "MA_SONB", "MA_BP", "MA_KH", "MA_NX", "MA_TD_PH", "MA_NVIEN", "MA_XULY"
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
            if (!string.IsNullOrEmpty(key2) && txtMaCtProc.Data != null && ObjectAndString.ObjectToInt(txtMaCtProc.Data["CT_NXT"]) != 0)
            {
                string AD = ("" + txtMaCtProc.Data["m_ctdbf"]).Trim();
                if(AD != "")
                cKey = cKey + string.Format(" AND STT_REC IN (SELECT STT_REC FROM {3} WHERE NGAY_CT between '{1}' and '{2}' and ma_kho_i in (select ma_kho from alkho where {0}))",
                    key2, dateNgay_ct1.YYYYMMDD, dateNgay_ct2.YYYYMMDD, AD);
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
            //else
            //{
            //    cKey = cKey + " and [Sl_in] = 0";
            //}

            //if (lineMa_xuly.IsSelected==false)
            //{
            //    cKey = cKey + " and ISNULL(ma_xuly,'')=''";
            //}


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

        public override void LoadDataFinish(DataSet ds)
        {
            base.LoadDataFinish(ds);
            GenButtons(ds.Tables[1]);
            chkView_all.Checked = true;
        }

        private XuLyBase _xulyBase;
        private V6ColorDataGridView _gridView1;
        private V6ColorDataGridView _gridView2;
        private List<Button> bs = new List<Button>(); 
        private void GenButtons(DataTable dataTable)
        {
            try
            {
                RemoveOldButtons();
                _xulyBase = FindParent<XuLyBase>() as XuLyBase;
                if (_xulyBase != null)
                {
                    _gridView1 = _xulyBase.dataGridView1;
                    _gridView2 = _xulyBase.dataGridView2;
                }

                int x_start = groupBox1.Left;
                int y_start = groupBox1.Bottom + 10;
                foreach (DataRow row in dataTable.Rows)
                {
                    Button b = new Button();
                    bs.Add(b);
                    b.UseVisualStyleBackColor = true;
                    b.Text = string.Format("{0} ({1})", row["ten_xuly" + (V6Setting.IsVietnamese?"":"2")], ObjectAndString.ObjectToInt(row["so_luong"]));

                    var file0 = Path.Combine("Pictures\\", row["PICTURE"].ToString().Trim());
                    var file = file0 + ".png";
                    if (File.Exists(file))
                    {
                        b.Image = V6ControlFormHelper.LoadCopyImage(file);
                    }
                    else
                    {
                        file = file0 + ".jpg";
                        if (!File.Exists(file))
                        {
                            file = file0 + ".gif";
                            if (!File.Exists(file)) file = file0 + ".bmp";
                        }
                        if (File.Exists(file)) b.Image = V6ControlFormHelper.LoadCopyImage(file);
                    }
                    b.ImageAlign = ContentAlignment.BottomLeft;
                    //b.TextAlign = ContentAlignment.MiddleLeft;    
                    
                    int y = y_start;
                    y_start += 38;
                    b.Location = new Point(x_start, y);
                    b.Width = groupBox1.Width;
                    b.Height = 38;
                    Controls.Add(b);
                    Height = b.Bottom + 5;
                    b.Tag = row;
                    b.Click += b_Click;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GenButtons", ex);
            }
        }

        private void RemoveOldButtons()
        {
            while (bs.Count>0)
            {
                Controls.Remove(bs[0]);
                bs.RemoveAt(0);
            }
        }

        void b_Click(object sender, EventArgs e)
        {
            try
            {
                chkView_all.Checked = false;
                Button b = (Button) sender;
                DataRow row = (DataRow) b.Tag;

                _gridView1.Filter("ma_xuly", "=", row["ma_xuly"], "value2", false, false);
                _xulyBase.FormatGridViewExtern();
                _xulyBase.UpdateGridView2(_gridView1.CurrentRow);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".b_Click", ex);
            }
        }

        private void txtMa_ct_V6LostFocus(object sender)
        {
            try
            {
                LoadComboboxSource(txtMaCtProc.Text);
                lineMa_xuly.VvarTextBox.SetInitFilter(string.Format("Ma_ct='{0}'", txtMaCtProc.Text.Trim()));
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".txtMa_ct_V6LostFocus", ex);
            }
        }

        private void chkView_all_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (chkView_all.Checked)
                {
                    _gridView1.Filter("ma_ct", "=",txtMaCtProc.Text.Trim(), "value2", false, false);
                    _xulyBase.FormatGridViewExtern();
                    _xulyBase.UpdateGridView2(_gridView1.CurrentRow);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".chkView_all", ex);
            }
        }
        
    }
}
