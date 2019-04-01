using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class DonViTinhAddEditForm : AddEditControlVirtual
    {
        public DonViTinhAddEditForm()
        {
            InitializeComponent();
        }
        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ALVT,ARI70", "Dvt", txtDVT.Text);
            txtDVT.Enabled = !v;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtDVT.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblDVT.Text;
            if (txtTenDVT.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenDVT.Text;

            if (Mode == V6Mode.Edit)
            {
                var keys = new List<string>() { "DVT" };
                foreach (string key in keys)
                {
                    if (DataDic.ContainsKey(key) && DataOld.ContainsKey(key))
                    {
                        bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, key,
                            DataDic[key].ToString(), DataOld[key].ToString());
                        if (!b)
                            throw new Exception("Không được thêm mã đã tồn tại: "
                                                            + key + "=" + DataDic[key]);
                    }
                }
            }
            else if (Mode == V6Mode.Add)
            {

                var keys = new SortedDictionary<string, object>();
                keys.Add("DVT", txtDVT.Text.Trim());

                foreach (KeyValuePair<string, object> key in keys)
                {
                    bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, key.Key,
                        DataDic[key.Key].ToString(), "");
                    if (!b) throw new Exception("Không được thêm mã đã tồn tại: "
                        + key.Key + "=" + DataDic[key.Key]);
                }
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
