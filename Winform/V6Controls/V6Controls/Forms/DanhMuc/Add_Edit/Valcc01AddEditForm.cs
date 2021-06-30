using System;
using System.Collections.Generic;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class Valcc01AddEditForm : AddEditControlVirtual
    {
        public Valcc01AddEditForm()
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
                txtMaCC.Enabled = false;
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
                txtMaCC.SetInitFilter("isnull(ngay_pb1,'')=''");
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("DisableControlWhenAdd " + ex.Message, ex.Source);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaCC.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMa.Text;

            if (Mode == V6Mode.Edit)
            {
                //bool b = V6Categories.IsValidOneCode_Full(TableName.ToString(), 0, "MA_BP",
                // txtma_gia.Text.Trim(), DataOld["MA_BP"].ToString());
                //if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMa.Text + "=" + txtMa.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                //bool b = V6Categories.IsValidOneCode_Full(TableName.ToString(), 1, "MA_BP",
                // txtma_gia.Text.Trim(), txtma_gia.Text.Trim());
                //if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMa.Text + "=" + txtMa.Text;
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
                DataOld = txtMaCC.Data.ToDataDictionary();
                if(_keys == null) _keys = new SortedDictionary<string, object>();
                if (DataOld.ContainsKey("UID"))
                {
                    _keys["UID"] = DataOld["UID"];
                }
                Dictionary<string, object> updateData = new Dictionary<string, object>(DataDic);
                if (_aldmConfig != null && _aldmConfig.HaveInfo)
                {
                    if (_aldmConfig.EXTRA_INFOR.ContainsKey("NOUPDATE"))
                    {
                        foreach (string FIELD in ObjectAndString.SplitString(_aldmConfig.EXTRA_INFOR["NOUPDATE"].ToUpper()))
                        {
                            if (updateData.ContainsKey(FIELD)) updateData.Remove(FIELD);
                        }
                    }
                }
                var result = Categories.Update(_MA_DM, updateData, _keys);
                return result>0;
            }
            return false;
        }


    }
}
