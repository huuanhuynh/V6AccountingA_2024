using System;
using System.Data;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonCafe
{
    public partial class KhuSelectForm : V6Form
    {
        public KhuSelectForm()
        {
            InitializeComponent();
        }

        private string ID_FIELD = "MA_KHO";
        private string NAME_FIELD = "TEN_KHO";
        public string SelectedID { get; set; }
        public string SelectedName { get; set; }
        public string NotInList { get; set; }

        private void KhuSelectForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                if (!V6Setting.IsVietnamese) NAME_FIELD = "TEN_KHO2";

                var and_not_in = string.IsNullOrEmpty(NotInList) ? "" : " AND MA_KHO NOT IN (" + NotInList + ")";
                var kho_data = V6BusinessHelper.Select("Alkho", "*",
                    (V6Login.IsAdmin ? "1=1" : " dbo.VFA_Inlist_MEMO(MA_KHO,'" + V6Login.UserRight.RightKho +"')=1")
                        + and_not_in).Data;
                if (kho_data.Rows.Count == 0)
                {
                    this.ShowWarningMessage("NO KHO");
                    return;
                }

                
                listBox1.DisplayMember = NAME_FIELD;
                listBox1.ValueMember = ID_FIELD;
                listBox1.DataSource = kho_data;
                listBox1.DisplayMember = NAME_FIELD;
                listBox1.ValueMember = ID_FIELD;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Load", ex);
            }
            Ready();
        }

        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (listBox1.DataSource != null)
            {
                DataRowView row = listBox1.SelectedItem as DataRowView;
                if (row != null && row.Row.Table.Columns.Contains(ID_FIELD))
                {
                    SelectedID = row[ID_FIELD].ToString().Trim();
                    SelectedName = row[NAME_FIELD].ToString().Trim();
                }
                else
                {
                    SelectedID = null;
                    SelectedName = "";
                }
            }
        }
    }
}
