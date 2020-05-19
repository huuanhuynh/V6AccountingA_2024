using System;
using V6AccountingBusiness;
using V6Controls;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class AbvitriAddEditForm: SoDuAddEditControlVirtual
    {
        public AbvitriAddEditForm()
        {
            InitializeComponent();

            MyInit();
        }

        private void MyInit()
        {
            try
            {
                TxtMa_vt.SetInitFilter("VITRI_YN=1");

                if (V6Options.GetValueNull("M_QLY_TON_QD") == "1")
                {
                    lblTon00qd.Visible = true;
                    txtTon00qd.Visible = true;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "INIT", ex);
            }
        }

        public override void DoBeforeAdd()
        {
            TxtNam.Value = V6Setting.M_Nam_bd;
        }

        public override void DoBeforeEdit()
        {
                    

        }

        public override void ValidateData()
        {
            if ((TxtMa_kho.Text.Trim() == "") 
                || (TxtMa_vt.Text.Trim() == "")
                || (TxtMa_vitri.Text.Trim() == ""))
            {
                throw new Exception(V6Text.Text("LACKINFO"));
            }
           else
            {
                // Check data 
                if (Mode == V6Mode.Edit)
                {
                    bool b = V6BusinessHelper.IsValidThreeCode_OneNumeric(_MA_DM.ToString(), 0,
                            "MA_KHO", TxtMa_kho.Text.Trim(), DataOld["MA_KHO"].ToString(),
                            "MA_VT", TxtMa_vt.Text.Trim(), DataOld["MA_VT"].ToString(),
                            "MA_VITRI", TxtMa_vitri.Text.Trim(), DataOld["MA_VITRI"].ToString(),
                            "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                    if (!b)
                        throw new Exception(V6Text.Exist + V6Text.EditDenied);
                }
                else if (Mode == V6Mode.Add)
                {
                    bool b = V6BusinessHelper.IsValidThreeCode_OneNumeric(_MA_DM.ToString(), 1,
                            "MA_KHO", TxtMa_kho.Text.Trim(), TxtMa_kho.Text.Trim(),
                            "MA_VT", TxtMa_vt.Text.Trim(), TxtMa_vt.Text.Trim(),
                            "MA_VITRI", TxtMa_vitri.Text.Trim(), TxtMa_vitri.Text.Trim(),
                           "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                    if (!b)
                        throw new Exception(V6Text.Exist + V6Text.AddDenied);
                }


            }

        }

        private void TxtMa_vt_V6LostFocus(object sender)
        {
            var data = TxtMa_vt.Data;

            //if (Convert.ToInt16(data["VITRI_YN"]) == 1)
            //    TxtMa_vitri.Enabled = true;
            //else
            //    TxtMa_vitri.Enabled = false;


            //TxtMa_vitri.SetInitFilter("ma_vt='" + TxtMa_vt.Text.Trim() + "'");

        }

        private void TxtMa_vt_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
