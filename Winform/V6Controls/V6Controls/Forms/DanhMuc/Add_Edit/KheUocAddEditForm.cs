using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Controls.Controls;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class KheUocAddEditForm : AddEditControlVirtual
    {
        public KheUocAddEditForm()
        {
            InitializeComponent();
            if (Mode == V6Mode.Add)
            {
                if (V6Login.MadvcsCount == 1)
                {
                    TxtMa_dvcs.Text = V6Login.Madvcs;
                }
            }
            else if (Mode == V6Mode.Edit)
            {

            }
        }

        private void KheUocAddEditForm_Load(object sender, EventArgs e)
        {
            Txtnh_ku1.SetInitFilter("Loai_nh=1");
            Txtnh_ku2.SetInitFilter("Loai_nh=2");
            Txtnh_ku3.SetInitFilter("Loai_nh=3");
        }

        public override void DoBeforeEdit()
        {
            try
            {
                var v = Categories.IsExistOneCode_List("ABKU,ARA00,ARI70", "MA_KU", TxtMa_ku.Text);
                TxtMa_ku.Enabled = !v;
                if (!V6Login.IsAdmin && TxtMa_dvcs.Text.ToUpper() != V6Login.Madvcs.ToUpper())
                {
                    TxtMa_dvcs.Enabled = false;
                }

            }

            catch (Exception ex)
            {
                Logger.WriteToLog("DisableWhenEdit " + ex.Message);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_ku.Text.Trim() == "" || TxtTen_ku.Text.Trim() == "" || TxtMa_dvcs.Text.Trim() == "")
                errors += V6Init.V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Structs.V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_KU",
                    TxtMa_ku.Text.Trim(), DataOld["MA_KU"].ToString());
                if (!b)
                    throw new Exception(V6Init.V6Text.DataExist
                                        + "MA_KU = " + TxtMa_ku.Text.Trim());
            }
            else if (Mode == V6Structs.V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_KU",
                    TxtMa_ku.Text.Trim(), TxtMa_ku.Text.Trim());
                if (!b)
                    throw new Exception(V6Init.V6Text.DataExist
                                        + "MA_KU = " + TxtMa_ku.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        public override void AfterUpdate()
        {
            UpdateAlkuct();
        }

        public override void AfterInsert()
        {
            UpdateAlkuct();
        }

        private void UpdateAlkuct()
        {
            try
            {
                var newID = DataDic["MA_KU"].ToString().Trim();
                
                SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                data.Add("MA_KU", newID);

                // Tuanmh 25/05/2017 loi Null
                if (_keys != null)
                {
                    Categories.Update("ALKUCT", data, _keys);

                }
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".UpdateAlkuct", ex);
            }
        }

        private void btnBoSung_Click(object sender, EventArgs e)
        {
            try
            {
                if (Mode != V6Mode.Edit)
                {
                    this.ShowInfoMessage("Chỉ dùng khi sửa.");
                    return;
                }

                if (DataOld != null && DataOld.ContainsKey("UID") && DataOld.ContainsKey("MA_KU"))
                {
                    var uid_ct = DataOld["UID"].ToString();
                    var ma_ku_old = DataOld["MA_KU"].ToString().Trim();
                    var data = new Dictionary<string, object>();

                    CategoryView dmView = new CategoryView(ItemID, "title", "Alkuct", "uid_ct='" + uid_ct + "'", null, DataOld);
                    if (Mode == V6Mode.View)
                    {
                        dmView.EnableAdd = false;
                        dmView.EnableCopy = false;
                        dmView.EnableDelete = false;
                        dmView.EnableEdit = false;
                    }
                    dmView.ToFullForm(btnBoSung.Text);
                }
                else
                {
                    ShowMainMessage(V6Text.NoData);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + " BoSung_Click " + ex.Message);
            }
        }
    }
}
