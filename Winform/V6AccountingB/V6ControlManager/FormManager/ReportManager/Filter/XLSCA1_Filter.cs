using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class XLSCA1_Filter: FilterBase
    {
        public XLSCA1_Filter()
        {
            InitializeComponent();
            F3 = false;
            F5 = false;
            F9 = true;
            
        }
        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            return null;
        }

        public override void UpdateValues()
        {
            String1 = txtFile.Text;
            String2 = (comboBox1.SelectedItem??"").ToString();
            String3 = (comboBox2.SelectedItem??"").ToString();
            Check1 = checkBox1.Checked;
            Check2 = checkBox2.Checked;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                string file = V6ControlFormHelper.ChooseExcelFile(this);
                if (!string.IsNullOrEmpty(file))
                {
                    txtFile.Text = file;
                    String1 = file;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, ex.Message));
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Check1 = checkBox1.Checked;
            if (Check1)
            {
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String2 = comboBox1.SelectedItem.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            String3 = comboBox2.SelectedItem.ToString();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Check2 = checkBox2.Checked;
        }

        private void btnSuaChiTieu_Click(object sender, EventArgs e)
        {
            string tableName = "ALIM2XLS";
            string keys = "MA_CT";
            var data = V6BusinessHelper.Select(tableName, "*", "MA_CT = 'CA1'").Data;
            V6ControlFormHelper.ShowDataEditorForm(data, tableName, null, keys, true, false);
        }
    }
}
