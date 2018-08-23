using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6Controls.Forms
{
    public partial class ToChucTreeSelectForm : V6Form
    {
        public ToChucTreeSelectForm()
        {
            InitializeComponent();
            MyInit();
        }

        public ToChucTreeSelectForm(string selectedIDs)
        {
            _selectedIDList = selectedIDs;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                toChucTreeListView1.ID_Field = "NODE";
                toChucTreeListView1.ParentIdField = "PARENT";
                toChucTreeListView1.IsBold_Field = "ISBOLD";
                toChucTreeListView1.ImageIndex_Field = "picture";
                toChucTreeListView1.Text_Field = "NAME";
                toChucTreeListView1.Sort_Field = "FSORT";

                LoadData();
                AldmConfig config = V6ControlsHelper.GetAldmConfigByTableName(_tableName);

                string showFields = config.GRDS_V1;
                string headerString = V6Setting.IsVietnamese ? config.GRDHV_V1 : config.GRDHE_V1;
                string formatStrings = config.GRDF_V1;
                toChucTreeListView1.SetData(_data, showFields, headerString, formatStrings);
                //SelectAll();
                SetSelectedIDs();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GetSelectedData", ex);
            }
        }

        private void SetSelectedIDs()
        {
            try
            {
                var arr = ObjectAndString.SplitString(_selectedIDList);
                if (arr.Length > 0)
                {
                    foreach (TreeListViewItem item in toChucTreeListView1.Items)
                    {
                        SetSelectedIDsRecusive(item, arr);
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetSelectedIDs", ex);
            }
        }

        private void SetSelectedIDsRecusive(TreeListViewItem item, IList<string> arr)
        {
            if (item.Items.Count == 0)
            {
                var itemData = toChucTreeListView1.GetItemData(item);
                if (arr.Contains(itemData[_selectID_field.ToUpper()].ToString().Trim()))
                    item.Checked = true;
                return;
            }

            foreach (TreeListViewItem item0 in item.Items)
            {
                SetSelectedIDsRecusive(item0, arr);
            }
        }

        private void LoadData()
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@Advance", ""),
                new SqlParameter("@User_id", V6Login.UserId),
            };
            _data = V6AccountingBusiness.V6BusinessHelper.ExecuteProcedure("VPH_GET_ORG_TREEVIEW", plist).Tables[0];
        }

        private DataTable _data;
        private string _tableName = "HRORGVIEW1";
        private string _selectID_field = "CODE";
        public event DataSelectHandler AcceptDataSelect;
        public string CheckFields = null;
        string _selectedIDList = "";
        private List<IDictionary<string, object>> listData = new List<IDictionary<string, object>>();
        protected virtual void OnAcceptData(DataTable table)
        {
            var handler = AcceptDataSelect;
            if (handler != null)
            {
                GetSelectedData();
                handler(_selectedIDList, listData);
            }
        }

        private void GetSelectedData()
        {
            try
            {
                _selectedIDList = "";
                listData = new List<IDictionary<string, object>>();
                foreach (TreeListViewItem item in toChucTreeListView1.Items)
                {
                    GetSelectedDataRecusive(item);
                    //toChucTreeListView1.ItemsCount
                    ////Chỉ lấy node con cuối cùng.
                    //if (item.Checked && item.Items.Count == 0)
                    //{
                    //    idList += "," + item.SubItems[_selectID];
                    //    listData.Add(toChucTreeListView1.GetItemData(item));
                    //}
                }
                if (_selectedIDList.Length > 1) _selectedIDList = _selectedIDList.Substring(1);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".GetSelectedData", ex);
            }
        }

        private void GetSelectedDataRecusive(TreeListViewItem item)
        {
            if (item.Items.Count == 0)
            {
                if (item.Checked)
                {
                    var data = toChucTreeListView1.GetItemData(item);
                    var selectedValue = data[_selectID_field].ToString().Trim();
                    if(selectedValue != "") _selectedIDList += "," + selectedValue;
                    listData.Add(data);
                }
                return;
            }

            foreach (TreeListViewItem item0 in item.Items)
            {
                GetSelectedDataRecusive(item0);
            }
        }

        private void Nhan()
        {
            if (_data == null)
            {
                this.ShowMessage(V6Text.NoData);
                return;
            }
            Close();
            OnAcceptData(_data);
        }

        private void Huy()
        {
            Close();
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            Nhan();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Huy();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            toChucTreeListView1.SelectAll(true);
        }

        private void btnUnSelect_Click(object sender, EventArgs e)
        {
            toChucTreeListView1.SelectAll(false);
        }
        
    }
}
