using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6Init;
using V6SqlConnect;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class V6BACKUP1 : FilterBase
    {
        public V6BACKUP1()
        {
            InitializeComponent();

            DataTable t = SqlConnect.Select
                ("ALBAK", "MAX(NGAY_BK) AS NGAY_BK", "1=1").Data;
            if (t.Rows.Count == 1)
            {
                if (t.Rows[0]["NGAY_BK"] != DBNull.Value)
                    txtngay_bk.Value = (DateTime?) t.Rows[0]["NGAY_BK"];

                else
                {
                    txtngay_bk.Value = DateTime.Now;
                }
            }
            else
            {
                txtngay_bk.Value = DateTime.Now;
            }

            Check1 = v6CheckBox1.Checked;
            Date1 = (DateTime)txtngay_bk.Value;
            String1ValueChanged += V6BACKUP1_String1ValueChanged;
            Ready();
        }

        void V6BACKUP1_String1ValueChanged(string oldvalue, string newvalue)
        {
            txtFileName.Text = newvalue;
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            return new List<SqlParameter> {new SqlParameter("@nUserID", V6Login.UserId)};
        }

        private void v6CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (IsReady)
            {
                Check1 = v6CheckBox1.Checked;
            }
        }

        private void v6CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (IsReady)
            {
                Check2 = v6CheckBox2.Checked;
            }
        }

        public override void Call1(object s = null)
        {
            v6CheckBox2.Visible = true;
        }

        public override void Call2(object s = null)
        {
            v6CheckBox2.Visible = false;
        }
    }
}
