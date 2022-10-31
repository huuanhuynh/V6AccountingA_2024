using System;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Controls;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class VuViecAddEditForm : AddEditControlVirtual
    {
        public VuViecAddEditForm()
        {
            InitializeComponent();
        }

        private void VuViecAddEditForm_Load(object sender, EventArgs e)
        {
            TxtMa_kh.ExistRowInTable();
            TxtNh_vv1.SetInitFilter("Loai_nh=1");
            TxtNh_vv2.SetInitFilter("Loai_nh=2");
            TxtNh_vv3.SetInitFilter("Loai_nh=3");
        }

        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ABVV,ARA00,ARI70", "MA_VV", TxtMa_vv.Text);
            TxtMa_vv.Enabled = !v;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_vv.Text.Trim() == "" || TxtTen_vv.Text.Trim() == "")
                errors += V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_VV",
                    TxtMa_vv.Text.Trim(), DataOld["MA_VV"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_VV",
                    TxtMa_vv.Text.Trim(), TxtMa_vv.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void Tinh_tien()
        {
            if (TxtMa_nt.Text == V6Options.M_MA_NT0)
            {
                TxtTien.Value = TxtTien_nt.Value;
            }
        }

        private void TxtMa_nt_V6LostFocus(object sender)
        {
            Tinh_tien();
        }

        private void TxtTien_nt_V6LostFocus(object sender)
        {
            Tinh_tien();
        }

        public CategoryView dmView;
        public override void LoadDetails()
        {
            InitCTView();
        }
        private void InitCTView()
        {
            try
            {
                dmView = new CategoryView();
                if (Mode == V6Mode.Add)
                {
                    tabChiTiet.Enabled = false;
                }
                if (Mode == V6Mode.Edit || Mode == V6Mode.View)
                {
                    var uid1 = DataOld["UID"].ToString();
                    dmView = new CategoryView(ItemID, "title", "Alvvct", "uid_ct='" + uid1 + "'", null, DataOld);
                    dmView._MA_DM_P = _MA_DM;
                    if (Mode == V6Mode.View)
                    {
                        dmView.EnableAdd = false;
                        dmView.EnableCopy = false;
                        dmView.EnableDelete = false;
                        dmView.EnableEdit = false;
                    }
                }
                dmView.btnBack.Enabled = false;
                dmView.btnBack.Visible = false;
                dmView.Dock = DockStyle.Fill;
                tabChiTiet.Font = new System.Drawing.Font(tabChiTiet.Font.FontFamily, 8.25f);
                tabChiTiet.Controls.Add(dmView);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + " InitCTView " + ex.Message);
            }
        }

        private void v6TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Mode != V6Mode.Add || Mode != V6Mode.Edit) return;
            if (v6TabControl1.SelectedTab == tabChiTiet)
            {
                if (dmView != null) dmView.SetParentData(GetData());
            }
        }
    }
}
