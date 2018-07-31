﻿using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Controls.Controls;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class HopDongAddEditForm : AddEditControlVirtual
    {
        public HopDongAddEditForm()
        {
            InitializeComponent();
        }

        public override void DoBeforeEdit()
        {
            try
            {
                var v = Categories.IsExistOneCode_List("ARA00,ARI70", "MA_HD", Txtma_hd.Text);
                Txtma_hd.Enabled = !v;

                
            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("HopDongAddEditForm DisableWhenEdit " + ex.Message);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (Txtma_hd.Text.Trim() == "" || txtTen_hd.Text.Trim() == "")
                errors += V6Init.V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Structs.V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_HD",
                    Txtma_hd.Text.Trim(), DataOld["MA_HD"].ToString());
                if (!b)
                    throw new Exception(V6Init.V6Text.ExistData
                                        + "MA_HD = " + Txtma_hd.Text.Trim());
            }
            else if (Mode == V6Structs.V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_HD",
                    Txtma_hd.Text.Trim(), Txtma_hd.Text.Trim());
                if (!b)
                    throw new Exception(V6Init.V6Text.ExistData
                                        + "MA_HD = " + Txtma_hd.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnBoSung_Click(object sender, EventArgs e)
        {
            try
            {
                var uid1 = DataOld["UID"].ToString();
                
                CategoryView dmView = new CategoryView(ItemID, "title", "Alhdct", "uid_ct='" + uid1 + "'", null, DataOld);
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
