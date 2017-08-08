using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class XAGLAUTOSO_CT: FilterBase
    {
        public XAGLAUTOSO_CT()
        {
            InitializeComponent();
            
            filterMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

            if (V6Login.MadvcsCount <= 1)
            {
                filterMaDvcs.Enabled = false;
            }
            
        }

        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            int so_ct2 = 0;
            var result = new List<SqlParameter>();
            //ngay_ct1, ngay_ct2, ma_sonb, so_ct1, so_ct2, dsct, transform, condition
            result.Add(new SqlParameter("@ngay_ct1", v6ColorDateTimePick1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ngay_ct2", v6ColorDateTimePick2.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ma_sonb", txtMaSoNB.Text.Trim()));
            result.Add(new SqlParameter("@so_ct1", TxtSo_ct.Value));
            result.Add(new SqlParameter("@so_ct2", so_ct2));
            result.Add(new SqlParameter("@dsct", TxtMa_ctnb.Text));
            result.Add(new SqlParameter("@transform", TxtTransform.Text));
            var and = radAnd.Checked;
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS",
            }, and);
            result.Add(new SqlParameter("@condition", key0));
            return result;
        }


        private void btnMa_ctnb_Click(object sender, EventArgs e)
        {
            using (V6Controls.Forms.DanhMuc.Add_Edit.PhanQuyen.DanhSachChungTu chungtu = new V6Controls.Forms.DanhMuc.Add_Edit.PhanQuyen.DanhSachChungTu
            {Text = "Danh sách chứng từ.", VLists_ct = TxtMa_ctnb.Text})
            {
                if (chungtu.ShowDialog(this) == DialogResult.OK)
                {
                    TxtMa_ctnb.Text = chungtu.VLists_ct;
                }
            }
        }

        private void txtMaSoNB_V6LostFocus(object sender)
        {
            //Gán tt từ alnb
            try
            {
                var data = V6BusinessHelper.Select("ALSONB", "*",
                    "MA_SONB='" + txtMaSoNB.Text.Trim().Replace("'", "''") + "'").Data;
                if (data.Rows.Count > 0)
                {
                    var row = data.Rows[0];
                    TxtMa_ctnb.Text = row["Ma_ctnb"].ToString().Trim();
                    TxtPhandau.Text = row["PHANDAU"].ToString().Trim();
                    TxtPhancuoi.Text = row["PHANCUOI"].ToString().Trim();
                    TxtDinhdang.Text = row["DINHDANG"].ToString().Trim();
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void Make_Mau()
        {
            var result = "";
            var result_mau = "";
            var _so_ct = Convert.ToString((int)TxtSo_ct.Value);

            if (TxtPhandau.Text.Trim() != "")
            {
                result = TxtPhandau.Text.Trim();
                result_mau = TxtPhandau.Text.Trim();
            }
            if (TxtDinhdang.Text.Trim() != "")
            {
                result += "{" + TxtDinhdang.Text.Trim() + "}";
                result_mau += (TxtDinhdang.Text.Trim() + _so_ct).Right(TxtDinhdang.Text.Trim().Length);
            }
            else
            {
                result += "{0}";
                if (TxtSo_ct.Value > 0)
                {
                    result_mau += _so_ct;
                }
                else
                {
                    result_mau += "1";
                }
            }


            if (TxtPhancuoi.Text.Trim() != "")
            {
                result += TxtPhancuoi.Text.Trim();
                result_mau += TxtPhancuoi.Text.Trim();
            }


            TxtTransform.Text = result;
            TxtMau.Text = result_mau;
            if (TxtMau.Text.Trim().Length > V6Setting.M_Size_ct)
            {
                TxtPhancuoi.Text = "";
                TxtPhandau.Text = "";
                TxtDinhdang.Text = "";
                this.ShowWarningMessage("Quá giới hạn số chứng từ !");

            }
        }

        private void TxtPhandau_TextChanged(object sender, EventArgs e)
        {
            Make_Mau();
        }

        private void XAGLAUTOSO_CT_Load(object sender, EventArgs e)
        {

        }

        
    }
}
