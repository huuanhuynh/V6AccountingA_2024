﻿using System.Data.SqlClient;
using V6AccountingBusiness;
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

        }

        public override void DoBeforeEdit()
        {
            txtMaCong.ExistRowInTable();
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaCong.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_CONG",
                 txtMaCong.Text.Trim(), DataOld["MA_CONG"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_CONG = " + txtMaCong.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_CONG",
                 txtMaCong.Text.Trim(), txtMaCong.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_CONG = " + txtMaCong.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

     
    }
}
