using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class TaiKhoanDauKyAddEditForm : SoDuAddEditControlVirtual
    {
        public TaiKhoanDauKyAddEditForm()
        {
            InitializeComponent();
        }

        public override void DoBeforeAdd()
        {
            TxtNam.Value = V6Setting.M_Nam_bd;
        }

        public override void DoBeforeEdit()
        {
            if (V6Setting.M_Ngay_ky1.Day == 1 && V6Setting.M_Ngay_ky1.Month == 1)
            {
                TxtDu_no1.Enabled = false;
                TxtDu_co1.Enabled = false;
                TxtDu_co_nt1.Enabled = false;
                TxtDu_no_nt1.Enabled = false;
            }
        }

        public override void ValidateData()
        {
            if ((TxtMa_dvcs.Text.Trim() == "") 
                || (TxtTk.Text.Trim() == "")                )
            {
                throw new Exception(V6Text.Text("LACKINFO"));
            }
            else
            {
                // Check data 
                if (Mode == V6Mode.Edit)
                {
                    if (DataDic.ContainsKey("MA_DVCS") && DataOld.ContainsKey("MA_DVCS")
                        && DataDic.ContainsKey("TK") && DataOld.ContainsKey("TK")
                        && DataDic.ContainsKey("NAM") && DataOld.ContainsKey("NAM"))
                    {
                        bool b = V6BusinessHelper.IsValidTwoCode_OneNumeric(TableName.ToString(), 0,
                            "MA_DVCS", TxtMa_dvcs.Text, DataOld["MA_DVCS"].ToString(),
                            "TK", TxtTk.Text, DataOld["TK"].ToString(),
                            "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                        if (!b)
                            throw new Exception(V6Text.Exist + V6Text.EditDenied);

                    }
                }
                else if (Mode == V6Mode.Add)
                {
                    if (DataDic.ContainsKey("MA_DVCS") 
                         && DataDic.ContainsKey("TK") 
                         && DataDic.ContainsKey("NAM"))
                    {
                        bool b = V6BusinessHelper.IsValidTwoCode_OneNumeric(TableName.ToString(), 1,
                            "MA_DVCS", TxtMa_dvcs.Text, TxtMa_dvcs.Text,
                            "TK", TxtTk.Text, TxtTk.Text,
                            "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                        if (!b)
                            throw new Exception(V6Text.Exist + V6Text.AddDenied);

                    }
                }
            }
        }

        private void CapNhapDuDauNam()
        {
            if (V6Setting.M_Ngay_ky1.Day == 1 && V6Setting.M_Ngay_ky1.Month == 1)
            {
                TxtDu_no1.Value = TxtDu_no00.Value;
                TxtDu_co1.Value = TxtDu_co00.Value;
                TxtDu_co_nt1.Value = TxtDu_co_nt00.Value;
                TxtDu_no_nt1.Value = TxtDu_no_nt00.Value;
            }
        }

        private void TxtDu_no00_V6LostFocus(object sender)
        {
            if (TxtDu_no00.Value != 0)
            {
                TxtDu_co00.Value = 0;
            }
            CapNhapDuDauNam();
        }
        private void TxtDu_co00_V6LostFocus(object sender)
        {
            if (TxtDu_co00.Value != 0)
            {
                TxtDu_no00.Value = 0;
            }
            CapNhapDuDauNam();
        }

        private void TxtDu_no_nt00_V6LostFocus(object sender)
        {
            if (TxtDu_no_nt00.Value != 0)
            {
                TxtDu_co_nt00.Value = 0;
            }
            CapNhapDuDauNam();
        }
        private void TxtDu_co_nt00_V6LostFocus(object sender)
        {
            if (TxtDu_co_nt00.Value != 0)
            {
                TxtDu_no_nt00.Value = 0;
            }
            CapNhapDuDauNam();
        }
        private void TxtDu_no1_V6LostFocus(object sender)
        {
            
        }
        private void TxtDu_co1_V6LostFocus(object sender)
        {
            
        }

        private void TxtDu_no_nt1_V6LostFocus(object sender)
        {
            
        }
        private void TxtDu_co_nt1_V6LostFocus(object sender)
        {
            
        }
    }
}
