using System;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class TonKhoDauKyAddEditForm : SoDuAddEditControlVirtual
    {
        public TonKhoDauKyAddEditForm()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                TxtMa_vt.SetInitFilter("Gia_ton<>3");

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
            try
            {
                TxtMa_vt.SetInitFilter("");
                var data = TxtMa_vt.Data;
                if(data != null && data.Table.Columns.Contains("Gia_ton"))
                {
                    var gia_ton = ObjectAndString.ObjectToInt(data["Gia_ton"]);
                    if (gia_ton == 3)
                    {
                        V6ControlFormHelper.SetFormControlsReadOnly(this, true);
                    }
                }
                TxtMa_vt.SetInitFilter("Gia_ton<>3");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeEdit", ex);
            }
        }

        public override void ValidateData()
        {
            if ((TxtMa_kho.Text.Trim() == "") 
                || (TxtMa_vt.Text.Trim() == "")                )
            {
                throw new Exception(V6Text.Text("LACKINFO"));
            }
            else
            {
                // Check data 
                if (Mode == V6Mode.Edit)
                {
                    bool b = V6BusinessHelper.IsValidTwoCode_OneNumeric(TableName.ToString(), 0,
                        "MA_KHO",TxtMa_kho.Text.Trim(), DataOld["MA_KHO"].ToString(),
                        "MA_VT", TxtMa_vt.Text.Trim(), DataOld["MA_VT"].ToString(),
                        "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                    if (!b)
                        throw new Exception(V6Text.Exist + V6Text.EditDenied);

                }
                else if (Mode == V6Mode.Add)
                {
                    bool b = V6BusinessHelper.IsValidTwoCode_OneNumeric(TableName.ToString(), 1,
                        "MA_KHO", TxtMa_kho.Text.Trim(), TxtMa_kho.Text.Trim(),
                        "MA_VT", TxtMa_vt.Text.Trim(), TxtMa_vt.Text,
                        "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                    if (!b)
                        throw new Exception(V6Text.Exist + V6Text.AddDenied);

                }


            }
        }
    }
}
