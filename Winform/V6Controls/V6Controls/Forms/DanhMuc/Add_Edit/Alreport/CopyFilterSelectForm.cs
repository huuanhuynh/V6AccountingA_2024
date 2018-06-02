using System;
using System.Collections.Generic;
using System.Data;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit.Alreport
{
    public partial class CopyFilterSelectForm : V6Form
    {
        public CopyFilterSelectForm()
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

                var select_data = V6BusinessHelper.Select("Alreport", "('['+Rtrim(MA_BC)+']  '+" + NAME_FIELD + ") as MA_TEN, *", " MA_BC NOT IN (" + NotInList + ")").Data;
                if (select_data.Rows.Count == 0)
                {
                    this.ShowWarningMessage("NO DATA");
                    return;
                }

                listBox1.DisplayMember = "MA_TEN";
                listBox1.ValueMember = ID_FIELD;
                listBox1.DataSource = select_data;
                listBox1.DisplayMember = "MA_TEN";
                listBox1.ValueMember = ID_FIELD;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Load", ex);
            }
            Ready();
        }

        private void LoadGridView(string ma_bc)
        {
            try
            {
                var keys = new SortedDictionary<string, object>
                    {
                        {"MA_BC", ma_bc}
                    };
                var alreport1_data = V6BusinessHelper.Select("Alreport1", keys, "*").Data;

                dataGridView1.DataSource = alreport1_data;
                dataGridView1.SelectAllRow();
                //var add_count = 0;
                //foreach (DataRow row in alreport1_data.Rows)
                //{
                //    var data = row.ToDataDictionary();
                //    data["MA_BC"] = TXTMA_BC.Text.Trim();
                //    data["UID_CT"] = DataOld["UID"];
                //    if (Categories.Insert(V6TableName.Alreport1, data))
                //    {
                //        add_count++;
                //    };
                //}
                //ShowMainMessage(string.Format("Đã thêm {0} chi tiết.", add_count));
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadGridView", ex);
            }
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
                LoadGridView(SelectedID);
            }
        }
    }
}
