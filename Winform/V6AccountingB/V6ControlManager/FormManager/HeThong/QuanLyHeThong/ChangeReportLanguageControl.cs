using System;
using System.Data;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.HeThong.QuanLyHeThong
{
    public partial class ChangeReportLanguageControl : V6Control
    {
        public ChangeReportLanguageControl()
        {
            InitializeComponent();
        }

        private void ChangeReportLanguageControl_Load(object sender, EventArgs e)
        {
            LoadReportLanguage();
        }

        private void LoadReportLanguage()
        {
            try
            {
                if (V6Setting.ReportLanguage == "V")
                {
                    rTiengViet.Checked = true;
                }
                else if (V6Setting.ReportLanguage == "E")
                {
                    rEnglish.Checked = true;
                }
                else if (V6Setting.ReportLanguage == "B")
                {
                    rBothLang.Checked = true;
                }
                else // C
                {
                    rCurrent.Checked = true;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadReportLanguage", ex);
            }
        }

        private void ChangeReportLanguage()
        {
            try
            {
                if (rTiengViet.Checked) V6Setting.ReportLanguage = "V";
                else if (rEnglish.Checked) V6Setting.ReportLanguage = "E";
                else if (rBothLang.Checked) V6Setting.ReportLanguage = "B";
                else if (rCurrent.Checked) V6Setting.ReportLanguage = V6Setting.Language;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ChangeLoginDVCS", ex);
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Dispose();
                return true;
            }
            return base.DoHotKey0(keyData);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            ChangeReportLanguage();
            Dispose();
        }
    }
}
