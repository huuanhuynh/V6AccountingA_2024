using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit.PhanQuyen
{
    public partial class PhanQuyenSoNoiBo : V6Form
    {
        public PhanQuyenSoNoiBo()
        {
            InitializeComponent();
        }
        public PhanQuyenSoNoiBo(V6Mode mode)
        {
            InitializeComponent();
            Mode = mode;
        }

        private V6Mode Mode { get; set; }
        public string Vrights_sonb { get; set; }
        public string Vrights_dvcs { get; set; }
        
        private void PhanQuyenSoNoiBo_Load(object sender, EventArgs e)
        {
            AddItems();
        }
        private DataTable Alsonb;
        

        private void AddItems()
        {
            var where = "";
            {
                where = " dbo.VFA_Inlist_MEMO(ma_dvcs,'" + Vrights_dvcs + "')=1";
                //where = "1=1";
            }

            Alsonb = V6BusinessHelper.Select("Alsonb", "Ma_sonb,Ten_sonb,Ten_sonb2",where, "", "MA_SONB").Data;
            var data1 = Filter();
            foreach (DataRow row in data1.Rows)
            {
                MakeList1(row);
            }
        }

      
        private void MakeList1(DataRow row)
        {
            var text = "";
            if (V6Setting.Language=="V")
                text = row["Ten_sonb"].ToString().Trim();
            else
                text = row["Ten_sonb2"].ToString().Trim();



            var Ma_sonb = row["Ma_sonb"].ToString().Trim();
            TreeListViewItem item = new TreeListViewItem(text);
            item.Name = Ma_sonb;

            treeListView1.Items.Add(item);
            if (Vrights_sonb.Contains(Ma_sonb))
            {
                item.Checked = true;
            }
          
        }


        private DataTable Filter()
        {
            DataView v = new DataView(Alsonb);
            v.Sort = "MA_SONB";
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

            Vrights_sonb = vrights_kho;

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
