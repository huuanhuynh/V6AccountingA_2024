using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class AbhhvtAddEditForm : SoDuAddEditControlVirtual
    {
        public AbhhvtAddEditForm()
        {
            InitializeComponent();
         //   TxtMa_sp.SetInitFilter("Loai_vt='55'");
        }

       

        public override void DoBeforeEdit()
        {
            int decimal0 = V6Options.M_ROUND;
            int decimalnt = V6Options.M_ROUND_NT;
            TxtMa_vt.ExistRowInTable();
        }
        public override void DoBeforeAdd()
        {
            TxtNam.Value = V6Setting.YearFilter;
            Txtthang.Value = V6Setting.MonthFilter;
            TxtMa_vt.ExistRowInTable();
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_vt.Text.Trim() == "" || txtloai_hh.Text.Trim() == "")
                errors += V6Text.CheckInfor+"!\r\n";
            if (errors.Length > 0)
                throw new Exception(errors);
            else
            {
                // Check data 
                if (Mode == V6Mode.Edit)
                {

                    bool b = V6BusinessHelper.IsValidTwoCode_TwoNumeric(TableName.ToString(), 0,
                        "MA_VT", TxtMa_vt.Text.Trim(), DataOld["MA_VT"].ToString(),
                        "LOAI_HH", txtloai_hh.Text.Trim(), DataOld["LOAI_HH"].ToString(),
                        "THANG", Convert.ToInt32(Txtthang.Value), Convert.ToInt32(Txtthang.Value),
                        "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                    if (!b)
                        throw new Exception(V6Text.EditDenied);

                }
                else if (Mode == V6Mode.Add)
                {

                    bool b = V6BusinessHelper.IsValidTwoCode_TwoNumeric(TableName.ToString(), 1,
                        "MA_VT", TxtMa_vt.Text.Trim(), TxtMa_vt.Text.Trim(),
                        "LOAI_HH", txtloai_hh.Text.Trim(), txtloai_hh.Text,
                        "THANG", Convert.ToInt32(Txtthang.Value), Convert.ToInt32(Txtthang.Value),
                        "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                    if (!b)
                        throw new Exception(V6Text.AddDenied);

                }

               
            }
        }

        private void txtloai_hh_TextChanged(object sender, EventArgs e)
        {
            switch (txtloai_hh.Text)
            {
                case "1":
                    txtGhiChu.Text = "Nhập";
                    break;
                case "2":
                    txtGhiChu.Text = "Xuất";
                    break;
                case "3":
                    txtGhiChu.Text = "Tồn đầu cuối";
                    break;
            }
        }
    }
    }

