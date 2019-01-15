
using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class QuocGiaAddEditForm : AddEditControlVirtual
    {
        public QuocGiaAddEditForm()
        {
            InitializeComponent();
        }
        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ALVT", "Ma_qg", TxtMa_qg.Text);
            TxtMa_qg.Enabled = !v;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_qg.Text.Trim() == "" || TxtTen_qg.Text.Trim() == "")
                errors += V6Init.V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Mode.Edit)
            {
                var keys = new List<string>() { "MA_QG" };
                foreach (string key in keys)
                {
                    if (DataDic.ContainsKey(key) && DataOld.ContainsKey(key))
                    {
                        bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, key,
                            DataDic[key].ToString(), DataOld[key].ToString());
                        if (!b)
                            throw new Exception(V6Init.V6Text.ExistData
                                                            + key + "=" + DataDic[key]);
                    }
                }
            }
            else if (Mode == V6Mode.Add)
            {
                var keys = new SortedDictionary<string, object>();
                keys.Add("MA_QG", TxtMa_qg.Text.Trim());

                foreach (KeyValuePair<string, object> key in keys)
                {
                    bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, key.Key,
                        DataDic[key.Key].ToString(), "");
                    if (!b) throw new Exception(V6Init.V6Text.ExistData
                        + key.Key + "=" + DataDic[key.Key]);
                }
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
