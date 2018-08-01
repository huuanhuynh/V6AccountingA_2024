using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.XuLy;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGSCTGS01 : FilterBase
    {
        public AGSCTGS01()
        {
            InitializeComponent();

            F3 = true;
            F4 = true;
            F8 = true;
            F9 = true;
            
            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);

            FixButtonText();
            
        }

        private void FixButtonText()
        {
            var nMonth = dateNgay_ct1.Date.Month + 1;
            var nYear = dateNgay_ct2.Date.Year;
            if (nMonth == 13)
            {
                nMonth = 1;
                nYear++;
            }
            btnChuyen.Text = string.Format("Chuyển sang tháng {0} năm {1}", nMonth, nYear);
        }

        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            var result = new List<SqlParameter>();

            result.Add(new SqlParameter("@thang", dateNgay_ct1.Date.Month));
            result.Add(new SqlParameter("@nam", dateNgay_ct2.Date.Year));
            
            return result;
        }
        
        private void Filter_Load(object sender, EventArgs e)
        {

        }

        private void dateNgay_ct1_ValueChanged(object sender, EventArgs e)
        {
            Number1 = dateNgay_ct1.Date.Month;
            FixButtonText();
        }

        private void dateNgay_ct2_ValueChanged(object sender, EventArgs e)
        {
            Number2 = dateNgay_ct2.Date.Year;
            FixButtonText();
        }

        public override void LoadDataFinish(DataSet ds)
        {
            _ds = ds;
        }

        private void btnChuyen_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ds == null) return;

                var nMonth = dateNgay_ct1.Date.Month + 1;
                var nYear = dateNgay_ct2.Date.Year;
                if (nMonth == 13)
                {
                    nMonth = 1;
                    nYear++;
                }
                
                SqlParameter[] plist = {
                    new SqlParameter("@nMonth", nMonth), 
                    new SqlParameter("@nYear", nYear), 
                };
                var count = V6BusinessHelper.SelectCount("Arctgs01", "Thang", "Thang = @nMonth and Nam = @nYear", plist);
                if (count > 0)
                {
                    if (this.ShowConfirmMessage(V6Text.ExistData, V6Text.Confirm)
                        != DialogResult.Yes)
                    {
                        return;
                    }
                }


                plist = new []
                {
                    new SqlParameter("@thang", dateNgay_ct1.Date.Month), 
                    new SqlParameter("@nam", dateNgay_ct2.Date.Year)
                };
                V6BusinessHelper.ExecuteProcedure("AGSCTGS01_COPY", plist);
                var xulybase = V6ControlFormHelper.FindParent<XuLyBase>(this) as XuLyBase;
                if (xulybase != null)
                {
                    xulybase.btnNhan.PerformClick();
                }
                V6ControlFormHelper.ShowMainMessage(V6Text.Finish);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ClickChuyen " + ex.Message);
            }
        }

        private void btnSuaSoCTGS_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ds == null) return;
                
                var plist = new[]
                {
                    new SqlParameter("@thang", dateNgay_ct1.Date.Month), 
                    new SqlParameter("@nam", dateNgay_ct2.Date.Year)
                };
                V6BusinessHelper.ExecuteProcedure("AGSCTGS01_A", plist);
                var xulybase = V6ControlFormHelper.FindParent<XuLyBase>(this) as XuLyBase;
                if (xulybase != null)
                {
                    xulybase.btnNhan.PerformClick();
                }
                V6ControlFormHelper.ShowMainMessage(V6Text.Finish);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".SuaSoCTGS", ex);
            }
        }
    }
}
