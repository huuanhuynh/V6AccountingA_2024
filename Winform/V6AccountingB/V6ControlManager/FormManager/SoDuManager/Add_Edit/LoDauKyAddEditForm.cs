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

            TxtMa_kho.SetInitFilter("LO_YN=1");
            TxtMa_vt.SetInitFilter("LO_YN=1");

            TxtMa_vitri.Enabled = false;
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
                throw new Exception("Chưa nhập đủ thông tin!");
            }
           else
            {
                // Check data 
                if (Mode == V6Mode.Edit)
                {
                    bool b = V6BusinessHelper.IsValidThreeCode_OneNumeric(TableName.ToString(), 0,
                            "MA_KHO", TxtMa_kho.Text.Trim(), DataOld["MA_KHO"].ToString(),
                            "MA_VT", TxtMa_vt.Text.Trim(), DataOld["MA_VT"].ToString(),
                            "MA_LO",TxtMa_lo.Text.Trim(), DataOld["MA_LO"].ToString(),
                            "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                    if (!b)
                        throw new Exception("Không được thêm mã đã tồn tại: ");
                }
                else if (Mode == V6Mode.Add)
                {
                    bool b = V6BusinessHelper.IsValidThreeCode_OneNumeric(TableName.ToString(), 1,
                            "MA_KHO", TxtMa_kho.Text.Trim(), TxtMa_kho.Text.Trim(),
                            "MA_VT", TxtMa_vt.Text.Trim(), TxtMa_vt.Text.Trim(),
                            "MA_LO", TxtMa_lo.Text.Trim(), TxtMa_lo.Text.Trim(),
                           "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                    if (!b)
                        throw new Exception("Không được thêm mã đã tồn tại: ");

                }
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
