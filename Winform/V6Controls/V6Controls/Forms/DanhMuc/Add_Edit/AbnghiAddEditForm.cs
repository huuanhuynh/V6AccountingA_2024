using System;
using V6Init;
using V6Structs;
using V6Tools;
using V6AccountingBusiness;
using V6Tools.V6Convert;

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

        private void From_Load(object sender, System.EventArgs e)
        {
            txtMaKH.ExistRowInTable();
            TxtMa_dvcs.ExistRowInTable();
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
                errors +=V6Text.CheckInfor+ "\r\n";
            if (V6Login.MadvcsTotal > 0 && TxtMa_dvcs.Text.Trim() == "")
                errors += V6Text.CheckInfor + "\r\n";
            
            AldmConfig config = ConfigManager.GetAldmConfig(TableName.ToString());
            if (config != null && config.HaveInfo && !string.IsNullOrEmpty(config.KEY))
            {
                var key_list = ObjectAndString.SplitString(config.KEY);
                errors += CheckValid(TableName.ToString(), key_list);
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
