using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class ViTriChiTietAddEditForm : AddEditControlVirtual
    {
        public ViTriChiTietAddEditForm()
        {
            InitializeComponent();
        }
        
        public override void DoBeforeEdit()
        {
            Txtma_vt.ExistRowInTable();
            TxtMa_kho.ExistRowInTable();
            TxtMa_vitri.ExistRowInTable();
        }
        public override void DoBeforeAdd()
        {
            Txtma_vt.ExistRowInTable();
            TxtMa_kho.ExistRowInTable();
            TxtMa_vitri.ExistRowInTable();
        }

        private void ViTriChiTietAddEditForm_Load(object sender, EventArgs e)
        {
            ReloadFlag = true;
        }

        public override SortedDictionary<string, object> GetData()
        {
            var data = base.GetData();
            data["UID_CT"] = ParentData["UID"];
            return data;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_vitri.Text.Trim() == "")
                errors +=  V6Text.CheckInfor +"\r\n";
            if (Txtma_vt.Text.Trim() == "")
                errors += V6Text.CheckInfor + "\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidTwoCode_Full(TableName.ToString(), 0, "MA_VITRI",
                    TxtMa_vitri.Text.Trim(), DataOld["MA_VITRI"].ToString(),
                    "MA_VT", Txtma_vt.Text.Trim(), DataOld["MA_VT"].ToString());
                if (!b) errors += V6Text.EditDenied;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidTwoCode_Full(TableName.ToString(), 1, "MA_VITRI",
                    TxtMa_vitri.Text.Trim(), TxtMa_vitri.Text.Trim(),
                    "MA_VT", Txtma_vt.Text.Trim(), Txtma_vt.Text.Trim());
                if (!b) errors += V6Text.AddDenied;
            }


            if (errors.Length > 0) throw new Exception(errors);
        }
        
        private bool flag = true;
        private void v6TabControl1_Enter(object sender, EventArgs e)
        {
            if (flag)
            {
                Txtma_vt.Focus();
                flag = false;
            }
        }

    }
}
