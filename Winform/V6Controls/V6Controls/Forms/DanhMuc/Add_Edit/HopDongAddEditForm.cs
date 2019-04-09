using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Controls;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class HopDongAddEditForm : AddEditControlVirtual
    {
        public HopDongAddEditForm()
        {
            InitializeComponent();
        }

        private void HopDongAddEditForm_Load(object sender, EventArgs e)
        {
            InitCTView();
            txtNH_HD1.SetInitFilter("Loai_nh=1");
            txtNH_HD2.SetInitFilter("Loai_nh=2");
            txtNH_HD3.SetInitFilter("Loai_nh=3");
            txtLoaiHD.Focus();
        }

        public override void DoBeforeEdit()
        {
            try
            {
                var v = Categories.IsExistOneCode_List("ARA00,ARI70", "MA_HD", txtMaHD.Text);
                txtMaHD.Enabled = !v;
            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("HopDongAddEditForm DisableWhenEdit " + ex.Message);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaHD.Text.Trim() == "" || txtTenHD.Text.Trim() == "")
                errors += V6Init.V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_HD",
                    txtMaHD.Text.Trim(), DataOld["MA_HD"].ToString());
                if (!b)
                    throw new Exception(V6Init.V6Text.DataExist
                                        + "MA_HD = " + txtMaHD.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_HD",
                    txtMaHD.Text.Trim(), txtMaHD.Text.Trim());
                if (!b)
                    throw new Exception(V6Init.V6Text.DataExist
                                        + "MA_HD = " + txtMaHD.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        public override void AfterSave()
        {
            UpdateAlhdct();
        }

        private void UpdateAlhdct()
        {
            try
            {
                var ma_new = DataDic["MA_HD"].ToString().Trim();
                var ma_old = Mode == V6Mode.Edit ? DataOld["MA_HD"].ToString().Trim() : ma_new;

                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UPDATE_ALHDCT",
                    new SqlParameter("@cMa_hd_old", ma_old),
                    new SqlParameter("@cMa_hd_new", ma_new));
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".UpdateAlqddvt", ex);
            }
        }
        
        private void InitCTView()
        {
            try
            {
                CategoryView dmView = new CategoryView();
                if (Mode == V6Mode.Add)
                {
                    tabChiTiet.Enabled = false;
                }
                if (Mode == V6Mode.Edit || Mode == V6Mode.View)
                {
                    var uid1 = DataOld["UID"].ToString();
                    dmView = new CategoryView(ItemID, "title", "Alhdct", "uid_ct='" + uid1 + "'", null, DataOld);
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
                tabChiTiet.Controls.Add(dmView);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + " InitCTView " + ex.Message);
            }
        }
        
    }
}
