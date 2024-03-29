﻿using V6AccountingBusiness;
using V6Structs;
using System;
using V6Init;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class KhaiBaoNgayNghiLe : AddEditControlVirtual
    {
        public KhaiBaoNgayNghiLe()
        {
            InitializeComponent();
            MyInit();
        }

        public void MyInit()
        {
          //  txtMaCong.Enabled = false;
        }

        public override void DoBeforeAdd()
        {
            txtMaCong.ExistRowInTable();
        }

        public override void DoBeforeEdit()
        {
            txtMaCong.ExistRowInTable();
        }
        
        public override void ValidateData()
        {
            var errors = "";
            if (txtMaCong.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaCong.Text;
            if (txtNgay.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblNgay.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_OneDate(_MA_DM, 0, "MA_CONG","NGAY",
                 txtMaCong.Text.Trim(), txtNgay.YYYYMMDD, DataOld["MA_CONG"].ToString(), DataOld["NGAY"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaCong.Text + "," + lblNgay.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_OneDate(_MA_DM, 0, "MA_CONG", "NGAY",
                  txtMaCong.Text.Trim(), txtNgay.YYYYMMDD, DataOld["MA_CONG"].ToString(), DataOld["NGAY"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaCong.Text + "," + lblNgay.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void txtMaCong_MouseLeave(object sender, EventArgs e)
        {
            if (txtMaCong.Text == "")
            {
                txtten_kh.Text = "";
            }
        }
    }
}
