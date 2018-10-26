using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit.PhanQuyen
{
    public partial class PhanQuyenKho : V6Form
    {
        public PhanQuyenKho()
        {
            InitializeComponent();
        }
        public PhanQuyenKho(V6Mode mode)
        {
            InitializeComponent();
            Mode = mode;
        }

        private V6Mode Mode { get; set; }
        public string Vrights_kho { get; set; }
        public string Vrights_dvcs { get; set; }

        private void PhanQuyenKho_Load(object sender, EventArgs e)
        {
            AddItems();
        }
        private DataTable L_Alkho;
        

        private void AddItems()
        {
            var where = "";
            {
                where = " dbo.VFA_Inlist_MEMO(ma_dvcs,'"+Vrights_dvcs+"')=1";
                //where = "1=1";
            }

            L_Alkho = V6BusinessHelper.Select("Alkho", "Ma_kho,Ten_kho,Ten_kho2",where).Data;
            var data1 = GetKho1();
            foreach (DataRow row in data1.Rows)
            {
                MakeList1(row);
            }
        }

      
        private void MakeList1(DataRow row)
        {
            var text = "";
            if (V6Setting.Language=="V")
                text = row["Ten_kho"].ToString().Trim();
            else
                text = row["Ten_kho2"].ToString().Trim();



            var Ma_kho = row["Ma_kho"].ToString().Trim();
            TreeListViewItem item = new TreeListViewItem(text);
            item.Name = Ma_kho;

            treeListView1.Items.Add(item);
            if (Vrights_kho.Contains(Ma_kho))
            {
                item.Checked = true;
            }
          
        }


        private DataTable GetKho1()
        {
            DataView v = new DataView(L_Alkho);
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
            var vrights_kho = "";
            foreach (TreeListViewItem item in treeListView1.Items)
            {

                if (item.Checked)
                {
                    vrights_kho += "/" + item.Name;

                }


            }
            if (vrights_kho.Length > 1) vrights_kho = vrights_kho.Substring(1);

            Vrights_kho = vrights_kho;

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
