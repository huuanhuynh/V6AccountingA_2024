﻿using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class QuanHuyenAddEditForm : AddEditControlVirtual
    {
        public QuanHuyenAddEditForm()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (TXTma_quan.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMa.Text;
            if (txtten_quan.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTen.Text;


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_QUAN",
                 TXTma_quan.Text.Trim(), DataOld["MA_QUAN"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMa.Text + "=" + TXTma_quan.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_QUAN",
                 TXTma_quan.Text.Trim(), TXTma_quan.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMa.Text + "=" + TXTma_quan.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
