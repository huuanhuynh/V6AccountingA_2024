using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class V6IMDATA2TH1_Filter: FilterBase
    {
        public V6IMDATA2TH1_Filter()
        {
            InitializeComponent();
            F3 = false;
            F5 = false;
            F9 = true;

            dateNgay_ct1.SetValue(V6Setting.M_SV_DATE);
            dateNgay_ct2.SetValue(V6Setting.M_SV_DATE);
            dateNgay_ct3.SetValue(V6Setting.M_SV_DATE);

            lineMaDvcs.SetValue(V6Login.Madvcs);
        }
        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            UpdateValues();
            return null;
        }

        public override void UpdateValues()
        {
            String1 = lineMaDvcs.StringValue;
            //String2 = (comboBox1.SelectedItem??"").ToString();
            //String3 = (comboBox2.SelectedItem??"").ToString();
            Date1 = dateNgay_ct1.Value;
            Date2 = dateNgay_ct2.Value;
            Date3 = dateNgay_ct3.Value;
            
            Check1 = chkDeleteData0.Checked;
            Check2 = chkAutoSoCt.Checked;
            Check3 = chkAutoF9.Checked;

            Number1 = numHHFrom.Value;
            Number2 = numHHTo.Value;
            Number3 = numAuto1.Value;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                string file = V6ControlFormHelper.ChooseExcelFile(this);
                if (!string.IsNullOrEmpty(file))
                {
                    //txtFile.Text = file;
                    String1 = file;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, ex.Message));
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Check2 = chkDeleteData0.Checked;
        }

        private void btnSuaChiTieu_Click(object sender, EventArgs e)
        {
            bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
            string tableName = "ALFCOPY2DATA";
            if (shift_is_down) tableName = "ALFCOPY2LIST";
            string keys = "MA_FILE,FILE_SQL";
            var data = V6BusinessHelper.Select(tableName, "*", "MA_FILE = '"+_reportProcedure+"'").Data;
            V6ControlFormHelper.ShowDataEditorForm(this, data, tableName, null, keys, false, false);
        }

        private void btnXemMauExcel_Click(object sender, EventArgs e)
        {
            V6ControlFormHelper.OpenExcelTemplate("ALVT_DATA2.XLS", V6Setting.IMPORT_EXCEL);
            V6ControlFormHelper.OpenExcelTemplate("SOH_DATA2.XLS", V6Setting.IMPORT_EXCEL);
        }

        private void chkAutoSoCt_CheckedChanged(object sender, EventArgs e)
        {
            ObjectDictionary["AUTOSOCT"] = chkAutoSoCt.Checked;
            Check3 = chkAutoSoCt.Checked;
        }

        private void chkAutoF9_CheckedChanged(object sender, EventArgs e)
        {
            ObjectDictionary["AUTOF9"] = chkAutoF9.Checked;
            ObjectDictionary["AUTOF9TIME"] = numAuto1.Value;
        }

        private void numAuto1_ValueChanged(object sender, EventArgs e)
        {
            ObjectDictionary["AUTOF9TIME"] = numAuto1.Value;
        }
    }
}
