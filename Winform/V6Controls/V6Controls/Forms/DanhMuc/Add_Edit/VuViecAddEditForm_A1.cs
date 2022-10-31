using System;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Controls;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class VuViecAddEditForm_A1 : AddEditControlVirtual
    {
        public VuViecAddEditForm_A1()
        {
            InitializeComponent();
        }

        private void VuViecAddEditForm_A1_Load(object sender, EventArgs e)
        {
            txtMaKH.ExistRowInTable();
            TxtNh_vv1.SetInitFilter("Loai_nh=1");
            TxtNh_vv2.SetInitFilter("Loai_nh=2");
            TxtNh_vv3.SetInitFilter("Loai_nh=3");
            InitFormat();
        }

        private void InitFormat()
        {
            try
            {
                txtSL_HD.DecimalPlaces = V6Options.M_IP_SL;
                txtSL_HDPL.DecimalPlaces = V6Options.M_IP_SL;
                txtSL_NK.DecimalPlaces = V6Options.M_IP_SL;

                txtGIA_HD.DecimalPlaces = V6Options.M_IP_GIA;

                txtTien.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                txtTienPL.DecimalPlaces = V6Options.M_IP_TIEN;
                txtTienTT.DecimalPlaces = V6Options.M_IP_TIEN;

                txtThue.DecimalPlaces = V6Options.M_IP_TIEN;
                txtThuePL.DecimalPlaces = V6Options.M_IP_TIEN;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + " InitFormat " + ex.Message);
            }
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
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit) return;
            if (!IsReady) return;

            try
            {
                int m_round = 0;
                if (TxtMa_nt.Text == V6Options.M_MA_NT0)
                {
                    m_round = V6Options.M_ROUND;
                }
                else
                {
                    m_round = V6Options.M_ROUND_NT;
                }

                txtTien.Value = V6BusinessHelper.Vround(txtSL_HD.Value * txtGIA_HD.Value, m_round);
                
                if (TxtMa_nt.Text == V6Options.M_MA_NT0)
                {
                    txtTien_nt.Value = txtTien.Value;
                }
                else
                {
                    txtTien_nt.Value = 0;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + " Tinh_tien " + ex.Message);
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

        public CategoryView dmView = null;
        public override void LoadDetails()
        {
            InitCTView();
        }

        private void InitCTView()
        {
            try
            {   
                if (Mode == V6Mode.Add)
                {
                    tabChiTiet.Enabled = false;
                    return;
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

        private void txtMaVT_V6LostFocus(object sender)
        {
            try
            {
                if (txtMaVT.Data != null)
                {
                    decimal value = ObjectAndString.ObjectToDecimal(txtMaVT.Data["THUE_SUAT"]);
                    txtPT_VAT.Value = value;
                    txtPT_VATPL.Value = value;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + " txtMaVT_V6LostFocus " + ex.Message);
            }
        }

        private void txtMaKH_V6LostFocus(object sender)
        {
            try
            {
                if (txtMaKH.Data != null)
                {
                    txtOngBa.Text = "" + txtMaKH.Data["ONG_BA"];
                    txtChucVu.Text = "" + txtMaKH.Data["TEN_KH4"];
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + " txtMaKH_V6LostFocus " + ex.Message);
            }
        }

        private void txtSL_HD_TextChanged(object sender, EventArgs e)
        {
            Tinh_tien();
        }

        private void txtGIA_HD_TextChanged(object sender, EventArgs e)
        {
            Tinh_tien();
        }

    }
}
