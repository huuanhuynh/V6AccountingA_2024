using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit.PhanQuyen
{
    public partial class V6MenuHideYN : V6Form
    {
        public V6MenuHideYN()
        {
            InitializeComponent();
        }
        public V6MenuHideYN(V6Mode mode)
        {
            InitializeComponent();
            Mode = mode;
        }
        private V6Mode Mode { get; set; }
        public string Vrights_Hide_yn { get; set; }
        public string Vrights_Hide_yn_uncheck { get; set; }

        private void PhanQuyen_Load(object sender, EventArgs e)
        {
            AddItemsMenu();
            GetCurrentState();
            ready = true;
        }

        Dictionary<TreeListViewItem, CheckState> _currentCheckStateDic = new Dictionary<TreeListViewItem, CheckState>();
        private void GetCurrentState()
        {
            try
            {
                foreach (TreeListViewItem item in treeListView1.Items)
                {
                    _currentCheckStateDic[item] = item.CheckStatus;
                    AddChild(item);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GetCurrentState", ex);
            }
        }

        private void AddChild(TreeListViewItem titem)
        {
            foreach (TreeListViewItem item in titem.Items)
            {
                _currentCheckStateDic[item] = item.CheckStatus;
                AddChild(item);
            }
        }

        private void UpdateNewState()
        {
            try
            {
                string message = "";
                foreach (KeyValuePair<TreeListViewItem, CheckState> item in _currentCheckStateDic)
                {
                    if (item.Key.CheckStatus != item.Value) // nếu khác ban đầu mới update
                    {
                        // bỏ qua nếu thay đổi trạng thái giữa nữa vời và uncheck.
                        switch (item.Value)
                        {
                            case CheckState.Unchecked:
                                if (item.Key.CheckStatus == CheckState.Indeterminate) continue; // bỏ qua
                                break;
                            case CheckState.Checked:
                                
                                break;
                            case CheckState.Indeterminate:
                                if (item.Key.CheckStatus == CheckState.Unchecked) continue; // bỏ qua
                                break;
                            default:
                                break;
                        }
                        
                        SortedDictionary<string, object> key = new SortedDictionary<string, object>();
                        key.Add("UID", item.Key.Name);
                        key.Add("Basicright", 0);
                        SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                        data.Add("HIDE_YN", (item.Key.CheckStatus == CheckState.Checked) ? "1" : "0");
                        int a = V6BusinessHelper.UpdateSimple(V6TableName.V6menu, data, key);

                        message += "\n" + item.Key.Text + " HIDE_YN=" + data["HIDE_YN"];
                    }
                }

                if (message.Length > 0)
                {
                    message = V6Text.Updated + message;
                    ShowMainMessage(message);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".UpdateNewState", ex);
            }
        }

        private void AddItemsMenu()
        {
            GetV6MenuData();
            var data1 = GetMenu1(V6Options.MODULE_ID);
            foreach (DataRow row in data1.Rows)
            {
                MakeMenu1(row);
            }
        }

        private DataTable v6Menu;
        private void GetV6MenuData()
        {
            try
            {
                string select = "v2id,jobid,STT_BOX,vbar,vbar2,Basicright,Hide_yn,UID";
                select += ",CASE WHEN CODE<>'' THEN (LTRIM(RTRIM(ITEMID))+'/'+LTRIM(RTRIM(code))) ELSE ITEMID END AS ITEMID ";
                string where = "Module_id= '" + V6Options.MODULE_ID + "'";
                //string orderby = "V2ID,JOBID,STT_BOX";
                v6Menu = V6BusinessHelper.Select("v6menu", select, where).Data;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GetV6MenuData", ex);
            }
        }

        private void MakeMenu1(DataRow row)
        {
            var text = row["vbar"].ToString().Trim();
            var v2ID = row["v2ID"].ToString().Trim();
            var uid = row["UID"].ToString().Trim();
            TreeListViewItem item = new TreeListViewItem(text);
            item.Tag = row;
            item.Name = uid;
            
            //item.SubItems.Add("");
            //item.SubItems.Add("");
            //item.SubItems.Add("");
            //item.SubItems.Add("");
            //item.SubItems.Add("");
            //item.SubItems.Add("");
            treeListView1.Items.Add(item);

            bool hide_yn = ObjectAndString.ObjectToBool(row["Hide_yn"]);
            if (hide_yn)
            {
                item.Checked = true;
                //item.Parent.Checked = true;
            }

            var data2 = GetMenu2(V6Options.MODULE_ID, v2ID);
            foreach (DataRow row2 in data2.Rows)
            {
                MakeMenu2(item, row2);
            }
            treeListView1.CheckParentAllCheck(item);
        }

        private void MakeMenu2(TreeListViewItem parent, DataRow row)
        {
            var text = row["vbar"].ToString().Trim();
            var v2ID = row["v2ID"].ToString().Trim();
            var jobID = row["jobID"].ToString().Trim();
            var uid = row["UID"].ToString().Trim();
            TreeListViewItem item = new TreeListViewItem(text);
            item.Tag = row;
            item.Name = uid;

            //item.SubItems.Add("");
            //item.SubItems.Add("");
            //item.SubItems.Add("");
            //item.SubItems.Add("");
            //item.SubItems.Add("");
            //item.SubItems.Add("");
            parent.Items.Add(item);
            
            bool hide_yn = ObjectAndString.ObjectToBool(row["Hide_yn"]);
            if (hide_yn)
            {
                item.Checked = true;
                //item.Parent.Checked = true;
            }

            var data3 = GetMenu3(V6Options.MODULE_ID, v2ID, jobID);
            for (int i = 0; i < data3.Rows.Count; i++ )
            {
                MakeMenu3(item, data3.Rows[i], i+1 == data3.Rows.Count);
            }
        }

        private void MakeMenu3(TreeListViewItem parent, DataRow row, bool last)
        {
            var text = row["vbar"].ToString().Trim();
            //var v2ID = row["v2ID"].ToString().Trim();
            //var jobID = row["jobID"].ToString().Trim();
            var itemID = row["itemID"].ToString().Trim();
            var uid = row["UID"].ToString().Trim();
            TreeListViewItem item = new TreeListViewItem(text);
            item.Tag = row;
            item.Name = uid;

            //item.SubItems.Add("");
            //item.SubItems.Add("");
            //item.SubItems.Add("");
            //item.SubItems.Add("");
            //item.SubItems.Add("");
            //item.SubItems.Add("");
            parent.Items.Add(item);

            //bool is_basic_right = ObjectAndString.ObjectToBool(row["Basicright"]);
            bool hide_yn = ObjectAndString.ObjectToBool(row["Hide_yn"]);
            if (hide_yn) // || Vrights_Hide_yn.Contains(itemID)
            {                
                item.Checked = true;
                //item.Parent.Checked = true;
                //item.Parent.Parent.Checked = true;
                //treeListView1.FakeOnItemCheck(item.Parent, CheckState.Unchecked);
            }

            //if (last)
            //{
            //    for (int i = 1; i <= 6; i++)
            //    {
            //        SetCheckX(item, i, item.SubItems[i].Text);
            //    }
            //}
        }

        private DataTable GetMenu1(string moduleID)
        {
            DataView v = new DataView(v6Menu);
            v.RowFilter = "Itemid='A0000000'";
            v.Sort = "STT_BOX";
            return v.ToTable();

            //var sql = "SELECT v2id,max(vbar) vbar, max(vbar2) vbar2"
            //          + " FROM V6MENU"
            //          + " WHERE Itemid='A0000000'"
            //          + " AND Module_id=@moduleID"//"'A'" //--V6Options
            //          + " AND HIDE_YN<>1"
            //          + " GROUP BY v2id"
            //          + " ORDER BY v2id";

            //SqlParameter[] plist = {
            //    new SqlParameter("@moduleID", moduleID)
            //};
            //return SqlConnect.ExecuteDataset(CommandType.Text, sql, plist).Tables[0];
        }

        private DataTable GetMenu2(string moduleID, string v2ID)
        {
            DataView v = new DataView(v6Menu);
            v.RowFilter = "Itemid='B0000000' AND V2ID = '"+v2ID+"' AND  JOBID<>'B099'";
            v.Sort = "STT_BOX";
            return v.ToTable();

            //var sql = "SELECT v2id,jobid,max(vbar) vbar, max(vbar2) vbar2"
            //          + " FROM V6MENU WHERE Itemid='B0000000'"
            //          + " AND Module_id= @moduleID AND HIDE_YN<>1"
            //          + " AND V2ID = @v2ID"//"'A001'" //--@@@
            //          + " AND  JOBID<>'B099'"
            //          + " GROUP BY V2ID,JOBID";
            //SqlParameter[] plist = {
            //    new SqlParameter("@moduleID", moduleID),
            //    new SqlParameter("@v2ID", v2ID)
            //};
            //return SqlConnect.ExecuteDataset(CommandType.Text, sql, plist).Tables[0];
        }

        private DataTable GetMenu3(string moduleID, string v2ID, string jobID)
        {
            DataView v = new DataView(v6Menu);
            v.RowFilter = "V2ID = '"+v2ID+"'  AND JOBID = '"+jobID+"' AND Itemid NOT IN('A0000000','B0000000')";
            v.Sort = "STT_BOX";
            return v.ToTable();

        }


        private void treeListView1_BeforeCollapse(object sender, TreeListViewCancelEventArgs e)
        {
            if (e.Item.ImageIndex == 2) e.Item.ImageIndex = 1;
        }

        private void treeListView1_BeforeExpand(object sender, TreeListViewCancelEventArgs e)
        {
            if (e.Item.ImageIndex == 1) e.Item.ImageIndex = 2;
        }


        private TreeListViewItem _item;
        private bool _itemCheck;
        private void treeListView1_MouseClick(object sender, MouseEventArgs e)
        {
            //mouseButton = e.Button;

            var col = treeListView1.GetColumnAt(e.Location);
            var item = treeListView1.GetItemAt(e.Location);
            _item = item;
            //this.ShowMessage("MouseClick " + item.Checked);
            if (col > 0)
            {
                var text = SwapText(item.SubItems[col].Text);
                SetCheckX(item, col, text);
            }
        }

        private void treeListView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_item != null)
            {
                //this.ShowMessage("ItemCheck " + _item.Checked);
                _itemCheck = true;
            }
        }

        private void treeListView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            return;
            //Sự iện xảy ra quá nhiều.
            try
            {
                if (_item != null && _itemCheck)
                {
                    //this.ShowMessage("ItemChecked " + _item.Checked);
                    string uid = _item.Name;
                    SortedDictionary<string, object> key = new SortedDictionary<string, object>();
                    key.Add("UID", uid);
                    key.Add("Basicright", 0);
                    SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                    data.Add("HIDE_YN", (_item.CheckStatus == CheckState.Checked) ? 1 : 0);
                    V6BusinessHelper.UpdateSimple(V6TableName.V6menu, data, key);
                }
                _item = null;
                _itemCheck = false;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".treeListView1_ItemChecked", ex);
            }
        }
        
        bool ready = false;
        private void treeListView1_ItemCheckedChanged(TreeListViewItem item, ItemCheckEventArgs e)
        {
            return;
            //Không thấy sự kiện xảy ra nếu thuộc tính CheckBox = Single
            
            if (ready)
            //if (e.NewValue == CheckState.Unchecked)
            {
                string itemID = item.Name;
                SortedDictionary<string, object> key = new SortedDictionary<string, object>();
                key.Add("UID", item.Name);
                key.Add("Basicright", 0);
                SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                data.Add("HIDE_YN", (item.CheckStatus == CheckState.Checked) ? "1" : "0");
                int a = V6BusinessHelper.UpdateSimple(V6TableName.V6menu, data, key);
            }
        }

        

        private void SetCheckX(TreeListViewItem item, int column, string text)
        {
            try
            {
                if (item.CheckStatus== CheckState.Unchecked) text = no;
                item.SubItems[column].Text
                    = item.CheckStatus == CheckState.Indeterminate ? yn : text;

                if (item.Items != null && item.Items.Count > 0)
                {
                    foreach (TreeListViewItem item1 in item.Items)
                    {
                        var text1 = text;
                        //if (!item1.Checked)
                        //{
                        //    text1 = no;
                        //}
                        SetCheckX(item1, column, text1);
                    }
                }

                if (item.Parent != null)
                    CheckParent(item.Parent, column);
            }
            catch (Exception)
            {
                
            }
        }

        private void CheckParent(TreeListViewItem parent, int column)
        {
            var full = true;
            var empty = true;
            foreach (TreeListViewItem item in parent.Items)
            {
                if (item.SubItems[column].Text == yes)
                {
                    empty = false;
                    if (!full) break;
                }
                else if (item.SubItems[column].Text == no)
                {
                    full = false;
                    if (!empty) break;
                }
                else
                {
                    full = false;
                    empty = false;
                    break;
                }
            }

            if (full) parent.SubItems[column].Text = parent.CheckStatus == CheckState.Indeterminate ? yn : yes;
            else if (empty) parent.SubItems[column].Text = no;
            else parent.SubItems[column].Text = yn;

            if (parent.Parent != null)
            {
                CheckParent(parent.Parent, column);
            }
        }

        private string yes = "X";
        private string no = "";
        private string yn = "-";
        

        private string SwapText(string s)
        {
            if (s != no) return no;
            return yes;
        }
        
        private void btnNhan_Click(object sender, EventArgs e)
        {
            if (Mode == V6Mode.View) return;
            GetRights();
            UpdateNewState();
            DialogResult = DialogResult.OK;
        }

        private void GetRights()
        {
            var vrights_hide_yn = "";
            var vrights_hide_yn_uncheck = "";
            
            foreach (TreeListViewItem item1 in treeListView1.Items)
            {
                if (item1.CheckStatus == CheckState.Checked)
                {
                    vrights_hide_yn += "," + item1.Name;
                }
                else
                {
                    vrights_hide_yn_uncheck += "," + item1.Name;
                }

                foreach (TreeListViewItem item2 in item1.Items)
                {
                    if (item2.CheckStatus == CheckState.Checked)
                    {
                        vrights_hide_yn += "," + item2.Name;
                    }
                    else
                    {
                        vrights_hide_yn_uncheck += "," + item2.Name;
                    }

                    foreach (TreeListViewItem item3 in item2.Items)
                    {
                        if (item3.Checked)
                        {
                            vrights_hide_yn += "," + item3.Name;
                        }
                        else
                        {
                            vrights_hide_yn_uncheck += "," + item3.Name;
                        }
                    }
                }
            }
            if (vrights_hide_yn.Length > 1) vrights_hide_yn = vrights_hide_yn.Substring(1);
            if (vrights_hide_yn_uncheck.Length > 1) vrights_hide_yn_uncheck = vrights_hide_yn_uncheck.Substring(1);
            
            Vrights_Hide_yn = vrights_hide_yn;
            Vrights_Hide_yn_uncheck = vrights_hide_yn_uncheck;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void treeListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0) return;

            var column = treeListView1.Columns[e.Column];
            //if (column.Tag == null) column.Tag = yes;

            var text = yes;
            if ((string) column.Tag == yes)
            {
                column.Tag = no;
                text = no;
            }
            else
            {
                column.Tag = yes;
            }


            foreach (TreeListViewItem item in treeListView1.Items)
            {
                SetCheckX(item, e.Column, text);
            }
        }
        
    }
}
