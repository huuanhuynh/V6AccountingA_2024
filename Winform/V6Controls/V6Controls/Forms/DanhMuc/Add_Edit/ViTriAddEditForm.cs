using System;
using System.Collections.Generic;
using System.Drawing;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

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
            var v = Categories.IsExistOneCode_List("ABVITRI,ABLO,ARI70", "Ma_vitri", TxtMa_vitri.Text);
            TxtMa_vitri.Enabled = !v;
            TxtMa_kho.Enabled = !v;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_vitri.Text.Trim() == "")
                errors += V6Text.CheckInfor+"!\r\n";
            if (TxtTen_vitri.Text.Trim() == "")
                errors += V6Text.CheckInfor + "!\r\n";

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
                            throw new Exception(V6Text.EditDenied+ key + "=" + DataDic[key]);
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
                    if (!b) throw new Exception(V6Text.AddDenied+ key.Key + "=" + DataDic[key.Key]);
                }
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void txtma_maurgb_TextChanged(object sender, EventArgs e)
        {
            
            
            if(txtma_rgb.Text=="")
            {
                lblTenMau.BackColor =Color.Transparent ;
                Txtten_rgb.Text = "";
            }
            else
            {
                var datarow = txtma_rgb.Data;

                if(datarow !=null)
                {
                    var r = ObjectAndString.ObjectToInt(datarow["R"]);
                    var g = ObjectAndString.ObjectToInt(datarow["G"]);
                    var b = ObjectAndString.ObjectToInt(datarow["B"]);
                    r = Math.Min(r, 255);
                    g = Math.Min(g, 255);
                    b = Math.Min(b, 255);
                    Txtten_rgb.Text = (datarow["TEN_RGB"]??"").ToString();

                    lblTenMau.BackColor = Color.FromArgb(r, g, b);
                }
            }
        }
    }
}
