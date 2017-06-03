using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;

namespace V6Controls.Forms.DanhMuc.Add_Edit.PhanQuyen
{
    public partial class PhanQuyenNSD : V6Form
    {
        public PhanQuyenNSD()
        {
            InitializeComponent();
        }

        public string Vrights_user { get; set; }
        
        private void PhanQuyenSoNoiBo_Load(object sender, EventArgs e)
        {
            AddItems();
        }
        private DataTable data;
        

        private void AddItems()
        {
            var where = "1=1";
            data = V6BusinessHelper.Select("V6User", "User_name,User_id,Comment",where).Data;
            var data1 = Filter();
            foreach (DataRow row in data1.Rows)
            {
                MakeList1(row);
            }
        }

      
        private void MakeList1(DataRow row)
        {
            var text = row["Comment"].ToString().Trim();

            var user_id = row["User_id"].ToString().Trim();
            var user_name = row["User_name"].ToString().Trim();
            TreeListViewItem item = new TreeListViewItem(text);
            item.Name = user_id;
            item.SubItems.Add(user_name);

            treeListView1.Items.Add(item);
            if (("/" + Vrights_user + "/").Contains("/" + user_id + "/"))
            {
                item.Checked = true;
            }
        }


        private DataTable Filter()
        {
            DataView v = new DataView(data);
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
            var result_right = "";
            foreach (TreeListViewItem item in treeListView1.Items)
            {
                if (item.Checked)
                {
                    result_right += "/" + item.Name;
                }
            }
            if (result_right.Length > 1) result_right = result_right.Substring(1);

            Vrights_user = result_right;

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
