using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ASOTH3: FilterBase
    {
        public ASOTH3()
        {
            InitializeComponent();
            F3 = false;
            F5 = true;
            dateNgay_ct1.Value = V6Setting.M_ngay_ct1;
            dateNgay_ct2.Value = V6Setting.M_ngay_ct2;

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            Txtma_ct.VvarTextBox.Text = "SOA,SOF";

            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
            chkGiamTru.Checked = true;
            String1 = txtLoaiBaoCao.Text;
            String2 = txtChiTietTheo.Text;
            SetHideFields("V");
            Txtnh_kh1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_kh2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_kh3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_kh4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_kh5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_kh6.VvarTextBox.SetInitFilter("loai_nh=6");

            Txtnh_vt1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_vt2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_vt3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_vt4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_vt5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_vt6.VvarTextBox.SetInitFilter("loai_nh=6");

            
        }

        public void SetHideFields(string lang)
        {
            _hideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                _hideFields.Add("TAG", "TAG");

            }
            else
            {

            }
            _hideFields.Add("STT_REC", "STT_REC");
            _hideFields.Add("STT_REC0", "STT_REC0");

        }
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@ngay_ct1 char(8),
            //@ngay_ct2 char(8),
            //@advance nvarchar(max),	
            //@pListVoucher nvarchar(max),
            //@KindFilter int
            var result = new List<SqlParameter>();

            V6Setting.M_ngay_ct1 = dateNgay_ct1.Value;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Value;


            result.Add(new SqlParameter("@ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@Loai_bc", (int)txtLoaiBaoCao.Value));
            result.Add(new SqlParameter("@Ct_theo", (int)txtChiTietTheo.Value));
            result.Add(new SqlParameter("@giam_tru", chkGiamTru.Checked ? 1 : 0));



            var and = radAnd.Checked;
            
            var cKey = "";


            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_KHO","MA_DVCS","MA_BP", "MA_KH", "MA_VV", "MA_NX", "MA_VT","MA_NVIEN","MA_BPHT"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
                 "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6"
            }, and);
            var key2 = GetFilterStringByFields(new List<string>()
            {
               "NH_VT1","NH_VT2","NH_VT3","NH_VT4","NH_VT5","NH_VT6", "MA_QG", "MA_NSX", "TK_VT"
            }, and);

            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey += string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey += string.Format("(1=2 OR {0})", key0);
                }
            }
            else
            {
                cKey = "1=1";
            }

            if (!string.IsNullOrEmpty(key1))
            {
                cKey = cKey + string.Format(" and ma_kh in (select ma_kh from alkh where {0} )", key1);
            }

            if (!string.IsNullOrEmpty(key2))
            {
                cKey = cKey + string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key2);
            }
            result.Add(new SqlParameter("@advance", cKey));
            return result;
        }

        private void txtLoaiBaoCao_ChiTietTheo_TextChanged(object sender, System.EventArgs e)
        {
            String1 = txtLoaiBaoCao.Text;
            String2 = txtChiTietTheo.Text;
        }

        private void txtLoaiBaoCao_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.ToString() == txtChiTietTheo.Text)
                {
                    if (txtChiTietTheo.LimitCharacters[0] == e.KeyChar)
                    {
                        txtChiTietTheo.Text = txtChiTietTheo.LimitCharacters[1].ToString();
                        txtChiTietTheo.Value = Convert.ToInt32(txtChiTietTheo.Text);
                    }
                    else
                    {
                        txtChiTietTheo.Text = txtChiTietTheo.LimitCharacters[0].ToString();
                        txtChiTietTheo.Value = Convert.ToInt32(txtChiTietTheo.Text);
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void txtChiTietTheo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.ToString() == txtLoaiBaoCao.Text)
                {
                    if (txtLoaiBaoCao.LimitCharacters[0] == e.KeyChar)
                    {
                        txtLoaiBaoCao.Text = txtLoaiBaoCao.LimitCharacters[1].ToString();
                        txtLoaiBaoCao.Value = Convert.ToInt32(txtLoaiBaoCao.Text);
                    }
                    else
                    {
                        txtLoaiBaoCao.Text = txtLoaiBaoCao.LimitCharacters[0].ToString();
                        txtLoaiBaoCao.Value = Convert.ToInt32(txtLoaiBaoCao.Text);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        
    }
}
