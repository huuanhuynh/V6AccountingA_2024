using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class KinhNghiemLamViec : AddEditControlVirtual
    {
        public KinhNghiemLamViec()
        {
            InitializeComponent();
        }

        public override void DoBeforeEdit()
        {
        }

        public override void SetDataKeys(SortedDictionary<string, object> keyData)
        {
            try
            {
                //base.SetDataKeys(keys);
                var keys = new Dictionary<string, object>();
                keys["STT_REC"] = keyData["STT_REC"];
                keys["STT_REC0"] = keyData["STT_REC0"];

                var data = V6BusinessHelper.Select(V6TableName.Hrappfamily, keys, "*").Data;
                if (data != null)
                {
                    if (data.Rows.Count == 1)
                    {
                        SetData(data.Rows[0].ToDataDictionary());
                    }
                    else
                    {
                        throw new Exception(string.Format("{0} key {1} {2} có {3} dòng dữ liệu.",
                            V6TableName.Hrappfamily, keys["STT_REC"], keys["STT_REC0"], data.Rows.Count));
                    }
                }
                else
                {
                    throw new Exception(string.Format("{0} key {1} {2} Select null.",
                            V6TableName.Hrappfamily, keys["STT_REC"], keys["STT_REC0"]));
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + " " +GetType() + ".SetDataKeys " + ex.Message);
            }
        }

    }
}
