﻿using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class ABTD2_Form : SoDuAddEditControlVirtual
    {
        public ABTD2_Form()
        {
            InitializeComponent();
            
            txtTk.SetInitFilter("loai_tk=1");
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
                || (txtMaTD2.Text.Trim() == "")
                || (txtTk.Text.Trim() == ""))
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
                          && DataDic.ContainsKey("MA_TD2") && DataOld.ContainsKey("MA_TD2")
                        && DataDic.ContainsKey("NAM") && DataOld.ContainsKey("NAM"))
                    {
                        bool b = V6BusinessHelper.IsValidThreeCode_OneNumeric(_MA_DM, 0,
                            "MA_DVCS", TxtMa_dvcs.Text, DataOld["MA_DVCS"].ToString(),
                            "TK", txtTk.Text, DataOld["TK"].ToString(),
                             "MA_TD2", txtMaTD2.Text, DataOld["MA_TD2"].ToString(),
                            "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                        if (!b)
                            throw new Exception(V6Text.Exist + V6Text.EditDenied);
                    }
                }
                else if (Mode == V6Mode.Add)
                {
                    if (DataDic.ContainsKey("MA_DVCS")
                         && DataDic.ContainsKey("TK")
                         && DataDic.ContainsKey("MA_TD2")
                        && DataDic.ContainsKey("NAM"))
                    {
                        bool b = V6BusinessHelper.IsValidThreeCode_OneNumeric(_MA_DM, 1,
                            "MA_DVCS", TxtMa_dvcs.Text, TxtMa_dvcs.Text,
                            "TK", txtTk.Text, txtTk.Text,
                            "MA_TD2", txtMaTD2.Text, txtMaTD2.Text,
                            "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                        if (!b)
                            throw new Exception(V6Text.Exist + V6Text.AddDenied);
                    }
                }
            }
        }

    }
}
