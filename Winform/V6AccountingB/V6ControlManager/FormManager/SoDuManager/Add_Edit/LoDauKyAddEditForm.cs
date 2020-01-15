using System;
using System.Reflection;
using V6AccountingBusiness;
using V6Controls;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class LoDauKyAddEditForm : SoDuAddEditControlVirtual
    {
        public LoDauKyAddEditForm()
        {
            InitializeComponent();

            MyInit();
        }

        private void MyInit()
        {
            try
            {
                TxtMa_kho.SetInitFilter("LO_YN=1");
                TxtMa_vt.SetInitFilter("LO_YN=1");
                TxtMa_vitri.Enabled = false;

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
            if (Mode == V6Mode.Add)
            {
                TxtNam.Value = V6Setting.M_Nam_bd;
            }
        }

        public override void DoBeforeEdit()
        {
                    

        }

        public override void ValidateData()
        {
            if ((TxtMa_kho.Text.Trim() == "")
                || (TxtMa_vt.Text.Trim() == "")
                || (TxtMa_lo.Text.Trim() == ""))
            {
                throw new Exception(V6Text.Text("LACKINFO"));
            }
            else
            {
                AldmConfig config = ConfigManager.GetAldmConfig(TableName.ToString());
                string errors = "";
                if (config != null && config.HaveInfo && !string.IsNullOrEmpty(config.KEY))
                {
                    var key_list = ObjectAndString.SplitString(config.KEY);
                    errors += CheckValid(TableName.ToString(), key_list);
                }
                if (errors.Length > 0) throw new Exception(errors);
                //// Check data 
                //if (Mode == V6Mode.Edit)
                //{
                //    bool b = V6BusinessHelper.IsValidThreeCode_OneNumeric(TableName.ToString(), 0,
                //            "MA_KHO", TxtMa_kho.Text.Trim(), DataOld["MA_KHO"].ToString(),
                //            "MA_VT", TxtMa_vt.Text.Trim(), DataOld["MA_VT"].ToString(),
                //            "MA_LO", TxtMa_lo.Text.Trim(), DataOld["MA_LO"].ToString(),
                //            "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));
                //    if (!b)
                //        throw new Exception(V6Text.Exist + V6Text.EditDenied);
                //}
                //else if (Mode == V6Mode.Add)
                //{
                //    bool b = V6BusinessHelper.IsValidThreeCode_OneNumeric(TableName.ToString(), 1,
                //            "MA_KHO", TxtMa_kho.Text.Trim(), TxtMa_kho.Text.Trim(),
                //            "MA_VT", TxtMa_vt.Text.Trim(), TxtMa_vt.Text.Trim(),
                //            "MA_LO", TxtMa_lo.Text.Trim(), TxtMa_lo.Text.Trim(),
                //           "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));
                //    if (!b)
                //        throw new Exception(V6Text.Exist + V6Text.AddDenied);
                //}
            }
        }

        private void TxtMa_vt_V6LostFocus(object sender)
        {
            try
            {
                var data = TxtMa_vt.Data;

                TxtMa_vitri.Enabled = ObjectAndString.ObjectToInt(data["VITRI_YN"]) == 1;
                TxtMa_lo.SetInitFilter("ma_vt='" + TxtMa_vt.Text.Trim() + "'");
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1}.{2} {3}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, ex.Message));
            }
        }

        private void TxtMa_lo_V6LostFocus(object sender)
        {
            var data = TxtMa_lo.Data;
            TxtHsd.Value = ObjectAndString.ObjectToDate(data["NGAY_HHSD"]);
        }

        private void TxtMa_vt_TextChanged(object sender, EventArgs e)
        {
            TxtMa_lo.SetInitFilter("Ma_vt='"+TxtMa_vt.Text+"'");
        }
    }
}
