using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class ViTriAddEditForm : AddEditControlVirtual
    {
        public ViTriAddEditForm()
        {
            InitializeComponent();
        }
        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ABLO,ARI70", "Ma_vitri", TxtMa_vitri.Text);
            TxtMa_vitri.Enabled = !v;
            TxtMa_kho.Enabled = !v;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_vitri.Text.Trim() == "")
                errors += "Chưa nhập mã vị trí!\r\n";
            if (TxtTen_vitri.Text.Trim() == "")
                errors += "Chưa nhập tên vị trí!\r\n";

            if (Mode == V6Mode.Edit)
            {
                var keys = new List<string>() { "MA_VITRI" };
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
                keys.Add("MA_VITRI", TxtMa_vitri.Text.Trim());

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
