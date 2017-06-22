using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class KhoHangAddEditForm : AddEditControlVirtual
    {
        public KhoHangAddEditForm()
        {
            InitializeComponent();
            if (V6Login.MadvcsTotal > 0)
            {
                TxtMa_dvcs.CheckNotEmpty = true;
                if (V6Login.MadvcsCount == 1)
                {
                    TxtMa_dvcs.Text = V6Login.Madvcs;
                    TxtMa_dvcs.Enabled = false;
                }
            
            }
            else
            {
                TxtMa_dvcs.Enabled = false;
            }
        

        }
        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ABVT,ABLO,ARI70,ARS90", "Ma_kho", TxtMa_kho.Text);
            TxtMa_kho.Enabled = !v;
            TxtMa_dvcs.Enabled = !v;
        }

        private void TxtKho_dl_V6LostFocus(object sender)
        {

            if (TxtKho_dl.Value == 1)
            {
                TxtTk_dl.Enabled = true;
                TxtTk_dl.ReadOnly = false;
            }
            else
            {
                TxtTk_dl.ReadOnly = true;
                TxtTk_dl.Enabled = false;
                TxtTk_dl.Text = "";
            }


        }
        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_kho.Text.Trim() == "")
                errors += "Chưa nhập mã !\r\n";
            if (TxtTen_kho.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_KHO",
                 TxtMa_kho.Text.Trim(), DataOld["MA_KHO"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: " + "MA_KHO = " + TxtMa_kho.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_KHO",
                 TxtMa_kho.Text.Trim(), TxtMa_kho.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_KHO = " + TxtMa_kho.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void btnChonhinh_Click(object sender, EventArgs e)
        {
            ChonHinh();
        }
        private void ChonHinh()
        {
            try
            {
                var chooseImage = V6ControlFormHelper.ChooseImage();
                if (chooseImage == null) return;

                ptbPHOTOGRAPH.Image = chooseImage;

                var ma_kho_new = TxtMa_kho.Text.Trim();
                var ma_kho_old = Mode == V6Mode.Edit ? DataOld["MA_KHO"].ToString().Trim() : ma_kho_new;
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UPDATE_ALKHOCT1",
                    new SqlParameter("@cMa_kho_old", ma_kho_old),
                    new SqlParameter("@cMa_kho_new", ma_kho_new));

                var photo = Picture.ToJpegByteArray(ptbPHOTOGRAPH.Image);
                //var sign = Picture.ToJpegByteArray(pictureBox2.Image);
                var data = new SortedDictionary<string, object> { { "PHOTOGRAPH", photo } };//, {"SIGNATURE", sign}};
                var keys = new SortedDictionary<string, object> { { "MA_KHO", TxtMa_kho.Text } };

                var result = V6BusinessHelper.UpdateTable("Alkhoct1", data, keys);

                if (result == 1)
                {
                    ShowTopMessage(V6Text.Updated + "PHOTOGRAPH");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + " " + GetType() + ".ChonHinh " + ex.Message);
            }
        }

        private void btnXoahinh_Click(object sender, EventArgs e)
        {
            XoaHinh();
        }

        private void XoaHinh()
        {
            try
            {
                ptbPHOTOGRAPH.Image = null;

                var photo = Picture.ToJpegByteArray(ptbPHOTOGRAPH.Image);
                var data = new SortedDictionary<string, object> { { "PHOTOGRAPH", photo } };
                var keys = new SortedDictionary<string, object> { { "MA_KHO", TxtMa_kho.Text } };

                var result = V6BusinessHelper.UpdateTable("Alkhoct1", data, keys);

                if (result == 1)
                {
                    ShowTopMessage(V6Text.Updated + "PHOTOGRAPH");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + " " + GetType() + ".ChonHinh " + ex.Message);
            }
        }
        
    }
}
