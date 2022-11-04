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

        private const string TABLE_NAME = "V6ONLINES";
        private DataTable data;
        private DataView view;
        private void OnlineManager_Load(object sender, EventArgs e)
        {
            LoadData("");
        }

        public override void LoadData(string code)
        {
            try
            {
                data = new DataTable(TABLE_NAME);
                data = V6Login.GetV6onlineTable();
                data.TableName = TABLE_NAME;
                view = new DataView(data);
                view.RowFilter = "Server_yn<>1 or Server_yn is null";
                dataGridView1.DataSource = view;
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
                //var column = dataGridView1.Columns["Name"];
                //if (column != null) column.Width = 300;
                //dataGridView1.ShowColumns("NAME", "ALLOW", "SERI");
                dataGridView1.Format("NAME,ALLOW,SERI", "C300,N0:40", V6Setting.IsVietnamese ? "Tên máy,Cho phép,Seri" : "Name,Allow,Seri");
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

        public override void SetStatus2Text()
        {
            string id = "ST2OnlineManager";
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = "Online manager: DoubleClick cột Allow để thay đổi.";
            }

            V6ControlFormHelper.SetStatusText2(text, id);
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
                var COLUMN_NAME = dataGridView1.Columns[e.ColumnIndex].DataPropertyName.ToUpper();
                
                if (COLUMN_NAME == "ALLOW")
                {
                    SortedDictionary<string, object> dataDic;
                    if (e.Button == MouseButtons.Right && dataGridView1.Rows.Count > 0)
                    {
                        //Cập nhập allow tất cả các dòng giống với ô đang bấm.
                        var count = 0;
                        var allow = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim();
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            var allow2 = row.Cells["ALLOW"].ToString().Trim();
                            if(allow == allow2) continue;

                            var NAME = row.Cells["NAME"].Value.ToString().Trim();
                            var checkCode = SqlConnect.GetServerDateTime().ToString("yyyyMMddHH:mm:ss");
                            checkCode = checkCode.Replace(" ", "");
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
                            if (V6BusinessHelper.Update(TABLE_NAME, dataDic, keys) > 0)
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
                        var row = dataGridView1.Rows[e.RowIndex];
                        var allow = row.Cells[e.ColumnIndex].Value.ToString().Trim() == "1" ? "0" : "1";
                        var NAME = row.Cells["NAME"].Value.ToString().Trim();
                        var checkCode = SqlConnect.GetServerDateTime().ToString("yyyyMMddHH:mm:ss");
                        checkCode = checkCode.Replace(" ", "");
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
                        if (V6BusinessHelper.Update(TABLE_NAME, dataDic, keys) > 0)
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
            LoadData("");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                DoDelete();
            }
        }

        private void DoDelete()
        {
            try
            {
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                
                if (row != null)
                {
                    //var data = row.ToDataDictionary();
                    var keys = new SortedDictionary<string, object> { { "UID", row.Cells["UID"].Value } };
                    if (this.ShowConfirmMessage(V6Text.DeleteConfirm + " " + row.Cells["NAME"].Value, V6Text.DeleteConfirm)
                        == DialogResult.Yes)
                    {
                        int t = V6BusinessHelper.Delete(TABLE_NAME, keys);
                        if (t > 0)
                        {
                            dataGridView1.SaveSelectedCellLocation();
                            LoadData("");
                            dataGridView1.LoadSelectedCellLocation();
                            V6ControlFormHelper.ShowMainMessage(V6Text.Deleted);
                        }
                        else
                        {
                            V6ControlFormHelper.ShowMessage(V6Text.DeleteFail);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoDelete", ex);
            }
        }
    }
}
