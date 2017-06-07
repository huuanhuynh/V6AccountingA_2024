using System;
using System.Data;
using V6AccountingBusiness;
using V6Init;

namespace V6Controls.Forms.DanhMuc.Add_Edit.Alreport
{
    public partial class CopyFromSelectForm : V6Form
    {
        public CopyFromSelectForm()
        {
            InitializeComponent();
        }

        private string ID_FIELD = "MA_BC";
        private string NAME_FIELD = "TEN";
        public string SelectedID { get; set; }
        public string SelectedName { get; set; }
        public string NotInList { get; set; }

        private void CopyFromSelectForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                if (!V6Setting.IsVietnamese) NAME_FIELD = "TEN2";

                var select_data = V6BusinessHelper.Select("Alreport", "*", " MA_BC NOT IN (" + NotInList + ")").Data;
                if (select_data.Rows.Count == 0)
                {
                    this.ShowWarningMessage("NO DATA");
                    return;
                }

                listBox1.DisplayMember = NAME_FIELD;
                listBox1.ValueMember = ID_FIELD;
                listBox1.DataSource = select_data;
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
