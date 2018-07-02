using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit.PhanQuyen
{
    public partial class PhanQuyen : V6Form
    {
        public PhanQuyen()
        {
            InitializeComponent();
        }
        public PhanQuyen(V6Mode mode)
        {
            InitializeComponent();
            Mode = mode;
        }
        private V6Mode Mode { get; set; }
        public string Vrights { get; set; }
        public string Vrights_Add { get; set; }
        public string Vrights_Copy { get; set; }
        public string Vrights_Edit { get; set; }
        public string Vrights_Delete { get; set; }
        public string Vrights_View { get; set; }
        public string Vrights_Print { get; set; }

        //private TreeListViewColumnSorter lvwColumnSorter;

        private void PhanQuyen_Load(object sender, EventArgs e)
        {
            AddItemsMenu();
            ready = true;
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
                string select = "v2id,jobid,vbar,vbar2,Basicright";
                select += ",CASE WHEN CODE<>'' THEN (LTRIM(RTRIM(ITEMID))+'/'+LTRIM(RTRIM(code))) ELSE ITEMID END AS ITEMID ";
                string where = "Module_id= '" + V6Options.MODULE_ID + "' And (HIDE_YN<>1 or Basicright=1)";

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
            TreeListViewItem item = new TreeListViewItem(text);
            item.Name = v2ID;
            
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            treeListView1.Items.Add(item);

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
            TreeListViewItem item = new TreeListViewItem(text);
            item.Name = jobID;
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            parent.Items.Add(item);
            
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
            TreeListViewItem item = new TreeListViewItem(text);
            item.Name = itemID;
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            
            parent.Items.Add(item);

            bool is_basic_right = ObjectAndString.ObjectToBool(row["Basicright"]);
            if (Vrights.Contains(itemID) || is_basic_right)
            {                
                item.Checked = true;
                item.Parent.Checked = true;
                //item.Parent.Parent.Checked = true;
                //treeListView1.FakeOnItemCheck(item.Parent, CheckState.Unchecked);
            }

            if (Vrights_Add.Contains(itemID) || is_basic_right)
            {
                item.SubItems[1].Text = yes;
                item.Parent.SubItems[1].Text = yes;
                item.Parent.Parent.SubItems[1].Text = yes;
            }
            if (Vrights_Copy.Contains(itemID) || is_basic_right)
            {
                item.SubItems[2].Text = yes;
                item.Parent.SubItems[2].Text = yes;
                item.Parent.Parent.SubItems[2].Text = yes;
            }
            if (Vrights_Edit.Contains(itemID) || is_basic_right)
            {
                item.SubItems[3].Text = yes;
                item.Parent.SubItems[3].Text = yes;
                item.Parent.Parent.SubItems[3].Text = yes;
            }
            if (Vrights_Delete.Contains(itemID) || is_basic_right)
            {
                item.SubItems[4].Text = yes;
                item.Parent.SubItems[4].Text = yes;
                item.Parent.Parent.SubItems[4].Text = yes;
            }
            if (Vrights_View.Contains(itemID) || is_basic_right)
            {
                item.SubItems[5].Text = yes;
                item.Parent.SubItems[5].Text = yes;
                item.Parent.Parent.SubItems[5].Text = yes;
            }
            if (Vrights_Print.Contains(itemID) || is_basic_right)
            {
                item.SubItems[6].Text = yes;
                item.Parent.SubItems[6].Text = yes;
                item.Parent.Parent.SubItems[6].Text = yes;
            }

            if (last)
            {
                for (int i = 1; i <= 6; i++)
                {
                    SetCheckX(item, i, item.SubItems[i].Text);
                }
            }
        }

        private DataTable GetMenu1(string moduleID)
        {
            DataView v = new DataView(v6Menu);
            v.RowFilter = "Itemid='A0000000'";
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
        
        bool ready = false;
        private void treeListView1_ItemCheckedChanged(TreeListViewItem item, ItemCheckEventArgs e)
        {
            
            if(ready)
            if (e.NewValue == CheckState.Unchecked)
            {
                for (int i = 1; i <= 5; i++)
                {
                    //item.SubItems[i].Text = no;
                    SetCheckX(item, i, no);
                }
            }
        }

        private void treeListView1_MouseClick(object sender, MouseEventArgs e)
        {
            //mouseButton = e.Button;

            var col = treeListView1.GetColumnAt(e.Location);
            var item = treeListView1.GetItemAt(e.Location);

            if (col > 0)
            {
                var text = SwapText(item.SubItems[col].Text);
                SetCheckX(item, col, text);
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
            DialogResult = DialogResult.OK;
        }

        private void GetRights()
        {
            var vrights = "";
            var vrights_add = "";
            var vrights_copy = "";
            var vrights_edit = "";
            var vrights_delete = "";
            var vrights_view = "";
            var vrights_print = "";
            foreach (TreeListViewItem item in treeListView1.Items)
            {
                foreach (TreeListViewItem item2 in item.Items)
                {
                    foreach (TreeListViewItem item3 in item2.Items)
                    {
                        if (item3.Checked)
                        {
                            vrights += "/" + item3.Name;

                            if (item3.SubItems[1].Text == yes)
                                vrights_add += "/" + item3.Name;
                            if (item3.SubItems[2].Text == yes)
                                vrights_copy += "/" + item3.Name;
                            if (item3.SubItems[3].Text == yes)
                                vrights_edit += "/" + item3.Name;
                            if (item3.SubItems[4].Text == yes)
                                vrights_delete += "/" + item3.Name;
                            if (item3.SubItems[5].Text == yes)
                                vrights_view += "/" + item3.Name;
                            if (item3.SubItems[6].Text == yes)
                                vrights_print += "/" + item3.Name;
                        }
                    }
                }
            }
            if (vrights.Length > 1) vrights = vrights.Substring(1);
            if (vrights_add.Length > 1) vrights_add = vrights_add.Substring(1);
            if (vrights_copy.Length > 1) vrights_copy = vrights_copy.Substring(1);
            if (vrights_edit.Length > 1) vrights_edit = vrights_edit.Substring(1);
            if (vrights_delete.Length > 1) vrights_delete = vrights_delete.Substring(1);
            if (vrights_view.Length > 1) vrights_view = vrights_view.Substring(1);
            if (vrights_print.Length > 1) vrights_print = vrights_print.Substring(1);
            Vrights = vrights;
            Vrights_Add = vrights_add;
            Vrights_Copy = vrights_copy;
            Vrights_Edit = vrights_edit;
            Vrights_Delete = vrights_delete;
            Vrights_View = vrights_view;
            Vrights_Print = vrights_print;
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
