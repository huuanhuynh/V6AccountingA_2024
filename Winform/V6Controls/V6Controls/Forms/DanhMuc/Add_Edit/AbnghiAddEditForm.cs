using System;
using V6Init;
using V6Structs;
using V6Tools;
using V6AccountingBusiness;
namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class AbnghiAddEditForm : AddEditControlVirtual
    {
        public AbnghiAddEditForm()
        {
            InitializeComponent();
            if (Mode == V6Mode.Add)
            {
                if (V6Login.MadvcsCount == 1)
                {
                    TxtMa_dvcs.Text = V6Login.Madvcs;
                }
            }
            else if (Mode == V6Mode.Edit)
            {
                
            }
        }

        private void KhachHangFrom_Load(object sender, System.EventArgs e)
        {
            txtMaKH.ExistRowInTable();
            txtMaKH.Focus();
        }

        public override void DoBeforeEdit()
        {
            try
            {
                //var v = Categories.IsExistOneCode_List("ABKH,ARA00,ARI70", "Ma_kh", txtMaKH.Text);
                //txtMaKH.Enabled = !v;

                //if (!V6Login.IsAdmin && TxtMa_dvcs.Text.ToUpper() != V6Login.Madvcs.ToUpper())
                //{
                //    TxtMa_dvcs.Enabled = false;
                //}
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + " " + GetType() + ".DisableWhenEdit " + ex.Message);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaKH.Text.Trim() == "")
                errors += "Chưa nhập mã khách hàng!\r\n";
            if (V6Login.MadvcsTotal > 0 && TxtMa_dvcs.Text.Trim() == "")
                errors += "Chưa nhập đơn vị cơ sở !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_KH",
                 txtMaKH.Text.Trim(), DataOld["MA_KH"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_KH = " + txtMaKH.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_KH",
                 txtMaKH.Text.Trim(), txtMaKH.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_KH = " + txtMaKH.Text.Trim());
            }

            if(errors.Length>0) throw new Exception(errors);
        }
        
        private void txtThang1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox)sender;
                if (txt.Value < 1) txt.Value = 0;
                if (txt.Value > 12) txt.Value = 12;
            }
            catch (Exception)
            {
                
            }
        }


    }
}
