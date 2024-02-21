using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit.PhanQuyen
{
    public partial class PhanQuyenDvcs : V6Form
    {
        public PhanQuyenDvcs()
        {
            InitializeComponent();
        }
        public PhanQuyenDvcs(V6Mode mode)
        {
            InitializeComponent();
            Mode = mode;
        }

        private V6Mode Mode { get; set; }
        public string Vrights_dvcs { get; set; }
        private void PhanQuyenDvcs_Load(object sender, EventArgs e)
        {
            AddItems();
        }

        private void AddItems()
        {
            L_Aldvcs = V6BusinessHelper.Select("Aldvcs", "Ma_dvcs,Ten_dvcs,Ten_dvcs2", "1=1", "", "MA_DVCS").Data;
            var data1 = GetDvcs1();
            foreach (DataRow row in data1.Rows)
            {
                MakeList1(row);
            }
        }

        private DataTable L_Aldvcs;
        

        private void MakeList1(DataRow row)
        {
            var text = "";
            if (V6Setting.Language=="V")
                text = row["Ten_dvcs"].ToString().Trim();
            else
                text = row["Ten_dvcs2"].ToString().Trim();
            
                
            
            var Ma_dvcs = row["Ma_dvcs"].ToString().Trim();
            TreeListViewItem item = new TreeListViewItem(text);
            item.Name = Ma_dvcs;
            item.SubItems.Add(Ma_dvcs);

            treeListView1.Items.Add(item);
            if (("/" + Vrights_dvcs + "/").Contains("/" + Ma_dvcs + "/"))
            {
                item.Checked = true;
            }
        }


        private DataTable GetDvcs1()
        {
            DataView v = new DataView(L_Aldvcs);
            v.Sort = "MA_DVCS";
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
            if (Mode == V6Mode.View) return;
            GetRights();
            DialogResult = DialogResult.OK;
        }

        private void GetRights()
        {
            var vrights_dvcs = "";
            foreach (TreeListViewItem item in treeListView1.Items)
            {
                
                if (item.Checked)
                {
                    vrights_dvcs += "/" + item.Name;
                }
            }
            if (vrights_dvcs.Length > 1) vrights_dvcs = vrights_dvcs.Substring(1);

            Vrights_dvcs = vrights_dvcs;
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
