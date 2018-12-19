using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;

namespace V6Controls.Forms.DanhMuc.Add_Edit.PhanQuyen
{
    public partial class DanhSachChungTu : V6Form
    {
        public DanhSachChungTu()
        {
            InitializeComponent();
        }

        public string VLists_ct { get; set; }

        private void PhanQuyenKho_Load(object sender, EventArgs e)
        {
            AddItems();
        }
        private DataTable L_Alct;
        
        private void AddItems()
        {
            var where = "POST2=1";
            L_Alct = V6BusinessHelper.Select("ALCT", "Ma_ct,Ten_ct,Ten_ct2", where, "", "MA_CT").Data;
            var data1 = GetAlct1();
            foreach (DataRow row in data1.Rows)
            {
                MakeList1(row);
            }
        }
      
        private void MakeList1(DataRow row)
        {
            var text = V6Setting.Language=="V" ? row["Ten_ct"].ToString().Trim() : row["Ten_ct2"].ToString().Trim();
            var Ma_ct = row["Ma_ct"].ToString().Trim();
            TreeListViewItem item = new TreeListViewItem(text);
            item.Name = Ma_ct;

            treeListView1.Items.Add(item);
            if (VLists_ct.Contains(Ma_ct))
            {
                item.Checked = true;
            }
        }

        private DataTable GetAlct1()
        {
            DataView v = new DataView(L_Alct);
            v.Sort = "MA_CT";
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


        private void treeListView1_BeforeLabelEdit(object sender, TreeListViewBeforeLabelEditEventArgs e)
        {

        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            GetRights();
            DialogResult = DialogResult.OK;
        }

        private void GetRights()
        {
            var Lists_ct = "";
            foreach (TreeListViewItem item in treeListView1.Items)
            {
                if (item.Checked)
                {
                    Lists_ct += "," + item.Name;
                }
            }
            if (Lists_ct.Length > 1) Lists_ct = Lists_ct.Substring(1);
            
            VLists_ct = Lists_ct;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            treeListView1.SelectAll();
        }

        private void btnUnSelect_Click(object sender, EventArgs e)
        {
            treeListView1.SelectAll(false);
        }

        
        
    }
}
