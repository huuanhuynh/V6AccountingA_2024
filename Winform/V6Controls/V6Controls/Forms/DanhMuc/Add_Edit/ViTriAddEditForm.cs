using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Controls;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class ViTriAddEditForm : AddEditControlVirtual
    {
        public ViTriAddEditForm()
        {
            InitializeComponent();
            MyInit();
        }
        private void MyInit()
        {
            txtNH_VITRI1.SetInitFilter("Loai_nh=1");
            txtNH_VITRI2.SetInitFilter("Loai_nh=2");
            txtNH_VITRI3.SetInitFilter("Loai_nh=3");
            txtNH_VITRI4.SetInitFilter("Loai_nh=4");
            txtNH_VITRI5.SetInitFilter("Loai_nh=5");
            txtNH_VITRI6.SetInitFilter("Loai_nh=6");

            KeyField1 = "MA_VITRI";
        }

        private void ViTriAddEditForm_Load(object sender, EventArgs e)
        {
            InitCTView();
        }

        public override void DoBeforeEdit()
        {
            TxtMa_kho.ExistRowInTable();
            var v = Categories.IsExistOneCode_List("ABVITRI,ABLO,ARI70", "Ma_vitri", TxtMa_vitri.Text);
            TxtMa_vitri.Enabled = !v;
            TxtMa_kho.Enabled = !v;
        }

        public override void DoBeforeAdd()
        {
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_vitri.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaVitri.Text;
            if (TxtTen_vitri.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenVitri.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_VITRI", DataDic["MA_VITRI"].ToString(), DataOld["MA_VITRI"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaVitri.Text + "=" + TxtMa_vitri.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_VITRI", TxtMa_vitri.Text, "");
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaVitri.Text + "=" + TxtMa_vitri.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void txtma_maurgb_TextChanged(object sender, EventArgs e)
        {
            
            
            if(txtma_rgb.Text=="")
            {
                lblTenMau.BackColor =Color.Transparent ;
                Txtten_rgb.Text = "";
            }
            else
            {
                var datarow = txtma_rgb.Data;

                if(datarow !=null)
                {
                    var r = ObjectAndString.ObjectToInt(datarow["R"]);
                    var g = ObjectAndString.ObjectToInt(datarow["G"]);
                    var b = ObjectAndString.ObjectToInt(datarow["B"]);
                    r = Math.Min(r, 255);
                    g = Math.Min(g, 255);
                    b = Math.Min(b, 255);
                    Txtten_rgb.Text = (datarow["TEN_RGB"]??"").ToString();

                    lblTenMau.BackColor = Color.FromArgb(r, g, b);
                }
            }
        }

        private void btnBoSung_Click(object sender, EventArgs e)
        {
            tabChiTiet.Focus();
            v6TabControl1.SelectedTab = tabChiTiet;
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
                    var uid_ct = DataOld["UID"].ToString();

                    dmView = new CategoryView(ItemID, "title", "alvitrict", "uid_ct='" + uid_ct + "'", null, DataOld);
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
                this.ShowErrorMessage(GetType() + " BoSung_Click " + ex.Message);
            }
        }

        public override void AfterUpdate()
        {
            UpdateVitrict();
        }

        public override void AfterInsert()
        {
            UpdateVitrict();
        }

        private void UpdateVitrict()
        {
            try
            {
                var ma_vitri_new = DataDic["MA_VITRI"].ToString().Trim();
                var ma_vitri_old = Mode == V6Mode.Edit ? DataOld["MA_VITRI"].ToString().Trim() : ma_vitri_new;

                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UPDATE_ALVITRICT",
                    new System.Data.SqlClient.SqlParameter("@cMa_vitri_old", ma_vitri_old),
                    new System.Data.SqlClient.SqlParameter("@cMa_vitri_new", ma_vitri_new));

            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".UpdateAlvitriCT", ex);
            }
        }

        
    }
}
