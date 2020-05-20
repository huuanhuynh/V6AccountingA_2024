using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class ThongTinLyLichForm : AddEditControlVirtual
    {
        public ThongTinLyLichForm()
        {
            InitializeComponent();
        }

        public override void DoBeforeEdit()
        {

            if (TXTbirth_date.Text.Length == 10)
            {
                TXTbirth_date0.Value = ObjectAndString.ObjectToDate(TXTbirth_date.Text);
            }

            if (TXTyear_deceased.Text.Length == 10)
            {
                TXTyear_deceased0.Value = ObjectAndString.ObjectToDate(TXTyear_deceased.Text);
            }

            txtRelation.ExistRowInTable();

        }

        public override void SetDataKeys(SortedDictionary<string, object> keyData)
        {
            try
            {
                //base.SetDataKeys(keys);
                var keys = new Dictionary<string, object>();
                keys["STT_REC"] = keyData["STT_REC"];
                keys["STT_REC0"] = keyData["STT_REC0"];

                var data = V6BusinessHelper.Select("Hrappfamily", keys, "*").Data;
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

        private void TXTbirth_date0_Leave(object sender, EventArgs e)
        {

            if (sender == TXTbirth_date0)
            {
                if (TXTbirth_date0.Value != null)
                {
                    TXTbirth_date.Text = TXTbirth_date0.Text;
                }
            }
            if (sender == TXTyear_deceased0)
            {
                if (TXTyear_deceased0.Value != null)
                {
                    TXTyear_deceased.Text = TXTyear_deceased0.Text;
                }
            }

        }

        private void txtRelation_TextChanged(object sender, EventArgs e)
        {
            txtRelation.ExistRowInTable();
        }

    }
}
