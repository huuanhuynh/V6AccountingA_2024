using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class AbvvkhAddEditForm : SoDuAddEditControlVirtual
    {
        public AbvvkhAddEditForm()
        {
            InitializeComponent();
            
            TxtTk.SetInitFilter("loai_tk=1 and tk_cn=1");
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
            if ((TxtMa_dvcs.Text.Trim() == "")
                || (txtma_vv.Text.Trim() == "")
                || (TxtMa_KH.Text.Trim() == "")
                || (TxtTk.Text.Trim() == ""))
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
                         && DataDic.ContainsKey("MA_KH") && DataOld.ContainsKey("MA_KH")
                         && DataDic.ContainsKey("MA_VV") && DataOld.ContainsKey("MA_VV")
                        && DataDic.ContainsKey("NAM") && DataOld.ContainsKey("NAM"))
                    {
                        bool b = V6BusinessHelper.IsValidFourCode_OneNumeric(_MA_DM, 0,
                            "MA_DVCS", TxtMa_dvcs.Text, DataOld["MA_DVCS"].ToString(),
                            "TK", TxtTk.Text, DataOld["TK"].ToString(),
                            "MA_KH", TxtMa_KH.Text, DataOld["MA_KH"].ToString(),
                            "MA_VV", txtma_vv.Text, DataOld["MA_VV"].ToString(),
                            "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));
                        
                        if (!b)
                            throw new Exception(V6Text.Exist + V6Text.EditDenied);
                    }
                }
                else if (Mode == V6Mode.Add)
                {
                    if (DataDic.ContainsKey("MA_DVCS") 
                         && DataDic.ContainsKey("TK") 
                        && DataDic.ContainsKey("NAM") )
                    {
                        bool b = V6BusinessHelper.IsValidFourCode_OneNumeric(_MA_DM, 1,
                            "MA_DVCS", TxtMa_dvcs.Text, TxtMa_dvcs.Text,
                            "TK", TxtTk.Text, TxtTk.Text,
                            "MA_KH", TxtMa_KH.Text, TxtMa_KH.Text,
                            "MA_VV", txtma_vv.Text, txtma_vv.Text,
                            "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                        if (!b)
                            throw new Exception(V6Text.Exist + V6Text.AddDenied);
                    }
                }
            }
        }
        
    }
}
