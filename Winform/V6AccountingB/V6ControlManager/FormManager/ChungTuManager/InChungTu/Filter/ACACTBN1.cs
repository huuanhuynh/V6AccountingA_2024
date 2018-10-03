using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Tools;

namespace V6ControlManager.FormManager.ChungTuManager.InChungTu.Filter
{
    public partial class ACACTBN1 : InChungTuFilterBase
    {
        public ACACTBN1()
        {
            InitializeComponent();

            F3 = false;
            F5 = false;

            SetFieldValueEvent += ACACTBN1_SetFieldValueEvent;
            SetHideFields("V");
        }

        void ACACTBN1_SetFieldValueEvent(string sttrec)
        {
            TxtStt_rec.Text = sttrec;
            LoadFilterControlData(sttrec);
        }

        public void SetHideFields(string lang)
        {
            _hideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                _hideFields.Add("TAG", "TAG");
                _hideFields.Add("STT_REC", "STT_REC");
                _hideFields.Add("STT_REC0", "STT_REC0");
                _hideFields.Add("STT_REC_TT", "STT_REC_TT");
                _hideFields.Add("MA_TT", "MA_TT");
                _hideFields.Add("MA_GD", "MA_GD");

                _hideFields.Add("T_TT_NT0", "T_TT_NT0");
                _hideFields.Add("T_TT_NT", "T_TT_NT");
                _hideFields.Add("DA_TT_NT", "DA_TT_NT");
                _hideFields.Add("CON_PT_NT", "CON_PT_NT");
                _hideFields.Add("T_TIEN_NT2", "T_TIEN_NT2");
                _hideFields.Add("T_THUE_NT", "T_THUE_NT");
            }
            else
            {
                _hideFields.Add("TAG", "TAG");
                _hideFields.Add("STT_REC", "STT_REC");
                _hideFields.Add("STT_REC0", "STT_REC0");
                _hideFields.Add("STT_REC_TT", "STT_REC_TT");
                _hideFields.Add("MA_TT", "MA_TT");
                _hideFields.Add("MA_GD", "MA_GD");
            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("SO_TKNH1", soTkTaiNH1.Text);
            RptExtraParameters.Add("TEN_NH1", tenNH1.Text);
            RptExtraParameters.Add("TINH_TP1", tinhTP1.Text);
            RptExtraParameters.Add("TEN_DV_NHANTIEN", tenDVnhanTien.Text);
            RptExtraParameters.Add("SO_TKNH2", soTKtaiNH2.Text);
            RptExtraParameters.Add("TEN_NH2", tenNH2.Text);
            RptExtraParameters.Add("TINH_TP2", tinhTP2.Text);
            RptExtraParameters.Add("NOIDUNG_TT", noiDungTT.Text);
            RptExtraParameters.Add("MAU_RIENG", mauRieng.Text);

            //@stt_rec; varchar(50),
            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@stt_rec", TxtStt_rec.Text.Trim()));
            return result;
        }

        private void LoadFilterControlData(string sttRec)
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@Stt_rec", sttRec),
                };
                var data_table = V6BusinessHelper.ExecuteProcedure("ACACTBN1_GETINFOR", plist).Tables[0];
                if (data_table.Rows.Count == 0) return;
                var data = data_table.Rows[0].ToDataDictionary();
                SetData(data);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".LoadFilterControlData: " + ex.Message);
            }
        }

        private void maKH_TextChanged(object sender, EventArgs e)
        {
            chonNganHang.SetInitFilter(maKH.Text.Trim() == "" ? "" : string.Format("MA_KH='{0}'", maKH.Text));
        }

        private void chonNganHang_V6LostFocus(object sender)
        {
            try
            {
                var data = chonNganHang.Data;
                if (data != null)
                {
                    soTKtaiNH2.Text = data["TK_NH"].ToString().Trim();
                    tenNH2.Text = data["NGAN_HANG"].ToString().Trim();
                    tinhTP2.Text = data["TINH_THANH"].ToString().Trim();

                    ClickNhan();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".chonNganHang_V6LostFocus", ex);
            }
        }

        private void ClickNhan()
        {
            try
            {
                var p = V6ControlFormHelper.FindParent<InChungTuViewBase>(this) as InChungTuViewBase;
                if (p != null)
                {
                    p.btnNhan.PerformClick();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ClickNhan", ex);
            }
        }
    }
}
