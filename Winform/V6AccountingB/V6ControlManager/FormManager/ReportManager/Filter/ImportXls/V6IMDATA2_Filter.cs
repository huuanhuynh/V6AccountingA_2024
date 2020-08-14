﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class V6IMDATA2_Filter: FilterBase
    {
        public V6IMDATA2_Filter()
        {
            InitializeComponent();
            F3 = false;
            F5 = false;
            F9 = true;

            dateNgay_ct1.SetValue(V6Setting.M_SV_DATE.AddDays(-7));
            dateNgay_ct2.SetValue(V6Setting.M_SV_DATE);
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
            String1 = txtFile.Text;
            String2 = (comboBox1.SelectedItem??"").ToString();
            String3 = (comboBox2.SelectedItem??"").ToString();
            Date1 = dateNgay_ct1.Date;
            Date2 = dateNgay_ct2.Date;
            Check1 = chkChuyenMa.Checked;
            Check2 = chkDeleteData0.Checked;
            Check3 = chkAutoSoCt.Checked;
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
            Check1 = chkChuyenMa.Checked;
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
            Check2 = chkDeleteData0.Checked;
        }

        private void btnSuaChiTieu_Click(object sender, EventArgs e)
        {
            string tableName = "ALIM2XLS";
            string keys = "MA_CT";
            var data = V6BusinessHelper.Select(tableName, "*", "MA_CT = 'SOA'").Data;
            V6ControlFormHelper.ShowDataEditorForm(this, data, tableName, null, keys, false, false);
        }

        private void btnXemMauExcel_Click(object sender, EventArgs e)
        {
            V6ControlFormHelper.OpenExcelTemplate("ALVT_DATA2.XLS", "IMPORT_EXCEL");
            V6ControlFormHelper.OpenExcelTemplate("SOA_DATA2.XLS", "IMPORT_EXCEL");
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
