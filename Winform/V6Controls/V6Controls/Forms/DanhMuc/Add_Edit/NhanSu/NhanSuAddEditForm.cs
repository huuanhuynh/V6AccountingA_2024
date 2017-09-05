﻿using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Structs;
using V6Controls.Controls;
using V6ControlManager.FormManager.DanhMucManager;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class NhanSuAddEditForm : AddEditControlVirtual
    {
        public NhanSuAddEditForm()
        {
            InitializeComponent();
        }

        public override void DoBeforeEdit()
        {
            try
            {
                btnBoSung.Enabled = true;
                //if (TXTbirth_date.Text.Length == 10)
                //{
                //    TxTbirth_date.Value = ObjectAndString.ObjectToDate(TXTbirth_date.Text);
                //}
                if (txtgender.Text == "1")
                {
                    radNam.Checked = true;
                    radNu.Checked = false;
                }
                else
                {
                    radNam.Checked = false;
                    radNu.Checked = true;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeEdit", ex);
            }
        }

        public override void SetDataKeys(SortedDictionary<string, object> keyData)
        {
            try
            {
                //base.SetDataKeys(keys);
                var keys = new Dictionary<string, object>();
                keys["STT_REC"] = keyData["STT_REC"];
                keys["STT_REC0"] = keyData["STT_REC0"];

                var data = V6BusinessHelper.Select(V6TableName.Hrpersonal, keys, "*").Data;
                if (data != null)
                {
                    if (data.Rows.Count == 1)
                    {
                        SetData(data.Rows[0].ToDataDictionary());
                    }
                    else
                    {
                        throw new Exception(string.Format("{0} key {1} {2} có {3} dòng dữ liệu.",
                            V6TableName.Hrpersonal, keys["STT_REC"], keys["STT_REC0"], data.Rows.Count));
                    }
                }
                else
                {
                    throw new Exception(string.Format("{0} key {1} {2} Select null.",
                            V6TableName.Hrpersonal, keys["STT_REC"], keys["STT_REC0"]));
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetDataKeys", ex);
            }
        }

        public override void DoBeforeAdd()
        {
            try
            {
                btnBoSung.Enabled = false;
                
                txtSttRec.Text = V6BusinessHelper.GetNewLikeSttRec("HR1", "STT_REC", "M");
                //  TxTemp_id.Text = "HR1";
                radNam.Checked = true;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeAdd", ex);
            }
        }
        
        public override void ValidateData()
        {

            var errors = "";
            if (TxTemp_id.Text.Trim() == "")
                errors += "Chưa nhập mã nhân viên!\r\n";


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "EMP_ID",
                 TxTemp_id.Text.Trim(), DataOld["EMP_ID"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "EMP_ID = " + TxTemp_id.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "EMP_ID",
                 TxTemp_id.Text.Trim(), TxTemp_id.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "EMP_ID = " + TxTemp_id.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
        
        private void radNam_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsReady) txtgender.Text = radNam.Checked ? "1" : "0";
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".radNam_CheckedChanged", ex);
            }
        }

        public override void AfterInsert()
        {
            if (!da_click_bo_sung)
            {
                //btnBoSung.PerformClick();
                BoSungTuDong();
            }
        }

        private void BoSungTuDong()
        {
            try
            {
                //Lay du lieu nhan vien //orgunit_id, position_id hrposition
                IDictionary<string, object> key = new SortedDictionary<string, object>();
                key.Add("STT_REC", DataOld["STT_REC"]);
                
                key.Add("TYPE", 0);
                var data = V6BusinessHelper.Select("hrposition", key, "*").Data;
                if (data.Rows.Count > 0)
                {
                    var data_row = data.Rows[0];
                    string orgunit_id = data_row["ORGUNIT_ID"].ToString().Trim();
                    string position_id = data_row["POSITION_ID"].ToString().Trim();

                    var insert_data = new SortedDictionary<string, object>(key);
                    insert_data.Add("STT_REC0", "00001");
                    insert_data.Add("ORGUNIT_ID", orgunit_id);
                    insert_data.Add("POSITION_ID", position_id);
                    V6BusinessHelper.Insert("hrposition", insert_data);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".BoSungTuDong", ex);
            }
        }

        private bool da_click_bo_sung;
        private void btnBoSung_Click(object sender, EventArgs e)
        {
            da_click_bo_sung = true;
            try
            {
                var stt_recNS = Mode == V6Mode.Add? txtSttRec.Text : DataOld["STT_REC"].ToString();

                CategoryView dmView = new CategoryView(ItemID, "title", "HRPOSITION", "STT_REC='" + stt_recNS + "'", null, DataOld);
                if (Mode == V6Mode.View)
                {
                    dmView.EnableAdd = false;
                    dmView.EnableCopy = false;
                    dmView.EnableDelete = false;
                    dmView.EnableEdit = false;
                }
                dmView.ToFullForm(btnBoSung.Text);

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + " BoSung_Click " + ex.Message);
            }
        }
    }
}
