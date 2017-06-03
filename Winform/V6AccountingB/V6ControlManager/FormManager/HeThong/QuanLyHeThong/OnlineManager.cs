using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools;

namespace V6ControlManager.FormManager.HeThong.QuanLyHeThong
{
    public partial class OnlineManager : V6FormControl
    {
        public OnlineManager()
        {
            InitializeComponent();
        }
        public OnlineManager(string itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
        }

        private DataTable data;
        private DataView view;
        private void OnlineManager_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            try
            {
                data = new DataTable("V6ONLINES");
                data = V6Login.GetV6onlineTable();
                data.TableName = "V6ONLINES";
                view = new DataView(data);
                view.RowFilter = "Server_yn<>1";
                gridView1.DataSource = view;
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".LoadData error: " + ex.Message);
            }
        }

        private void FormatDataGridView()
        {
            try
            {
                var column = gridView1.Columns["Name"];
                if (column != null) column.Width = 300;
                gridView1.ShowColumns("NAME", "ALLOW", "SERI");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatDataGridView", ex);
            }
        }

        void SaveData()
        {
            try
            {
                //data.WriteXml("V6ONLINES.xtb", true);

                V6ControlFormHelper.ShowMainMessage(V6Text.EditSuccess);
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowMainMessage(V6Text.EditFail + ex.Message);
            }
        }

        private void v6FormButton2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void v6FormButton1_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        
        private void v6ColorDataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                var COLUMN_NAME = gridView1.Columns[e.ColumnIndex].DataPropertyName.ToUpper();
                
                if (COLUMN_NAME == "ALLOW")
                {
                    SortedDictionary<string, object> dataDic;
                    if (e.Button == MouseButtons.Right && gridView1.Rows.Count > 0)
                    {
                        //Cập nhập allow tất cả các dòng giống với ô đang bấm.
                        var count = 0;
                        var allow = gridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim();
                        foreach (DataGridViewRow row in gridView1.Rows)
                        {
                            var allow2 = row.Cells["ALLOW"].ToString().Trim();
                            if(allow == allow2) continue;

                            var NAME = row.Cells["NAME"].Value.ToString().Trim();
                            //var checkCode = row.Cells["CHECKCODE"].Value;
                            var checkCode = SqlConnect.GetServerDateTime().ToString("yyyyMMddHH:mm:ss");
                            dataDic = new SortedDictionary<string, object>
                            {
                                {"ALLOW", allow},
                                {"CHECKCODE", checkCode},
                                {"CODE_NAME", UtilityHelper.EnCrypt(NAME + allow + checkCode)},
                            };
                            var keys = new SortedDictionary<string, object>
                            {
                                {"UID", row.Cells["UID"].Value}
                            };
                            if (V6BusinessHelper.Update("V6ONLINES", dataDic, keys) > 0)
                            {
                                count++;
                                row.Cells["ALLOW"].Value = allow;
                            }
                        }
                        V6ControlFormHelper.ShowMainMessage(count + " " + V6Text.Updated);
                    }
                    else
                    {
                        //Đổi allow cho ô đang bấm.
                        var row = gridView1.Rows[e.RowIndex];
                        var allow = row.Cells[e.ColumnIndex].Value.ToString().Trim() == "1" ? "0" : "1";
                        var NAME = row.Cells["NAME"].Value.ToString().Trim();
                        //var checkCode = row.Cells["CHECKCODE"].Value;
                        var checkCode = SqlConnect.GetServerDateTime().ToString("yyyyMMddHH:mm:ss");
                        dataDic = new SortedDictionary<string, object>
                        {
                            {"ALLOW", allow},
                            {"CHECKCODE", checkCode},
                            {"CODE_NAME", UtilityHelper.EnCrypt(NAME + allow + checkCode)},
                        };
                        var keys = new SortedDictionary<string, object>
                        {
                            {"UID", row.Cells["UID"].Value}
                        };
                        if (V6BusinessHelper.Update("V6ONLINES", dataDic, keys) > 0)
                        {
                            row.Cells["ALLOW"].Value = allow;
                            row.Cells["CHECKCODE"].Value = checkCode;
                            V6ControlFormHelper.ShowMainMessage(V6Text.Updated);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CellMouseClick", ex);
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
