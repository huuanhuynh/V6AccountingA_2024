﻿using V6AccountingBusiness;
using System;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class DanhMucChiTietCaLamViecForm : AddEditControlVirtual
    {
        public DanhMucChiTietCaLamViecForm()
        {
            InitializeComponent();
            MyInit();
        }

        public void MyInit()
        {
       //     txtMaNhCa.Enabled = false;
        }

        public override void DoBeforeAdd()
        {
        }
        public override void DoBeforeEdit()
        {
            txtManhCa.ExistRowInTable();
            txtMaCong.ExistRowInTable();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtManhCa.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (txtMaCong.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (txtTenCa.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_NHCA",
                 txtManhCa.Text.Trim(), DataOld["MA_NHCA"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_NHCA = " + txtManhCa.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_NHCA",
                 txtManhCa.Text.Trim(), txtManhCa.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_NHCA = " + txtManhCa.Text.Trim());
            }


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_CA",
                 txtMa_ca.Text.Trim(), DataOld["MA_CA"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_CA = " + txtMa_ca.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_CA",
                 txtMa_ca.Text.Trim(), txtMa_ca.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_CA = " + txtMa_ca.Text.Trim());
            }
            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
