﻿using System;
using System.Collections.Generic;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class Valts01AddEditForm : AddEditControlVirtual
    {
        public Valts01AddEditForm()
        {
            InitializeComponent();
        }

        private void Algia2AddEditForm_Load(object sender, EventArgs e)
        {
           
           
        }

        public override void DoBeforeEdit()
        {
            
            try
            {
                //F3
                //txtMaTS.SetInitFilter("");
                txtMaTS.Enabled = false;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeEdit", ex);
            }
        }

        public override void DoBeforeAdd()
        {
            try
            {
                //F4
                txtMaTS.SetInitFilter("isnull(ngay_kh1,'')=''");
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("DisableControlWhenAdd " + ex.Message, ex.Source);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaTS.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMa.Text;

            if (Mode == V6Mode.Edit)
            {
                //bool b = V6Categories.IsValidOneCode_Full(TableName.ToString(), 0, "MA_BP",
                // txtma_gia.Text.Trim(), DataOld["MA_BP"].ToString());
                //if (!b)
                //    throw new Exception("Không được thêm mã đã tồn tại: "
                //                                    + "MA_BP = " + txtma_gia.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                //bool b = V6Categories.IsValidOneCode_Full(TableName.ToString(), 1, "MA_BP",
                // txtma_gia.Text.Trim(), txtma_gia.Text.Trim());
                //if (!b)
                //    throw new Exception("Không được thêm mã đã tồn tại: "
                //                                    + "MA_BP = " + txtma_gia.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        public override bool InsertNew()
        {
            if (V6Login.GetDataMode == GetDataMode.Local)
            {
                DataDic = GetData();
                ValidateData();
                //Lấy thêm UID từ DataEditNếu có.
                DataOld = txtMaTS.Data.ToDataDictionary();
                if(_keys == null) _keys = new SortedDictionary<string, object>();
                if (DataOld.ContainsKey("UID"))
                {
                    _keys["UID"] = DataOld["UID"];
                }
                var result = Categories.Update(TableName, DataDic, _keys);
                return result>0;
            }
            return false;
        }


    }
}
