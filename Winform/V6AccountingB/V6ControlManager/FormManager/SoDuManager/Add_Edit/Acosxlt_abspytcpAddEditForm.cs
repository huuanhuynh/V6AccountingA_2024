using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class Acosxlt_abspytcpAddEditForm : SoDuAddEditControlVirtual
    {
        public Acosxlt_abspytcpAddEditForm()
        {
            InitializeComponent();
            TxtMa_sp.SetInitFilter("Loai_vt='55'");
        }

        public override void DoBeforeAdd()
        {
            TxtNam.Value = V6Setting.YearFilter;
            Txtthang.Value = V6Setting.MonthFilter;
        }

        public override void DoBeforeEdit()
        {

            int decimal0 = V6Options.M_ROUND;
            int decimalnt = V6Options.M_ROUND_NT;
            
            txttien_dd.DecimalPlaces = decimal0;
            txttien_dd_nt.DecimalPlaces = decimalnt;

        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_bpht.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if ((txtma_ytcp.Text.Trim() == "")
                || (TxtMa_sp.Text.Trim() == ""))
            {
                throw new Exception("Chưa nhập đủ thông tin!");
            }
            else
            {
                // Check data 
                if (Mode == V6Mode.Edit)
                {

                    bool b = V6BusinessHelper.IsValidTwoCode_OneNumeric(TableName.ToString(), 0,
                        "MA_YTCP", txtma_ytcp.Text.Trim(), DataOld["MA_YTCP"].ToString(),
                        "MA_SP", TxtMa_sp.Text.Trim(), DataOld["MA_SP"].ToString(),
                        "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                    if (!b)
                        throw new Exception("Không được thêm mã đã tồn tại: ");

                }
                else if (Mode == V6Mode.Add)
                {

                    bool b = V6BusinessHelper.IsValidTwoCode_OneNumeric(TableName.ToString(), 1,
                        "MA_YTCP", txtma_ytcp.Text.Trim(), txtma_ytcp.Text.Trim(),
                        "MA_SP", TxtMa_sp.Text.Trim(), TxtMa_sp.Text,
                        "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                    if (!b)
                        throw new Exception("Không được thêm mã đã tồn tại: ");

                }

                if (errors.Length > 0) throw new Exception(errors);
            }
        }

    }
}
