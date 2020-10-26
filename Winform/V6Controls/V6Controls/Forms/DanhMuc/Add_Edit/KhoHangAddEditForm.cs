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
            TxtTk_dl.SetInitFilter("loai_tk=1");
            if (V6Login.MadvcsTotal > 0)
            {
                txtMaDVCS.CheckNotEmpty = true;
                if (V6Login.MadvcsCount == 1)
                {
                    txtMaDVCS.Text = V6Login.Madvcs;
                    txtMaDVCS.Enabled = false;
                }
            }
            else
            {
                txtMaDVCS.Enabled = false;
            }
            
        }

        public override void DoBeforeAdd()
        {
            try
            {
                Chk_khodaily.Checked = TxtTk_dl.Text != "";
                TxtTk_dl.Enabled = (Chk_khodaily.Checked);
                TxtTk_dl.ReadOnly = !(Chk_khodaily.Checked);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeAdd", ex);
            }
        }

        public override void DoBeforeEdit()
        {
            try
            {
                var v = Categories.IsExistOneCode_List("ABVT,ABLO,ARI70,ARS90", "Ma_kho", txtMaKho.Text);
                txtMaKho.Enabled = !v;
                txtMaDVCS.Enabled = !v;
                
                Chk_khodaily.Checked = TxtTk_dl.Text != "";
                TxtTk_dl.Enabled = (Chk_khodaily.Checked);
                TxtTk_dl.ReadOnly = !(Chk_khodaily.Checked);

                LoadImageData();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeEdit", ex);
            }
        }

        private void LoadImageData()
        {
            try
            {
                SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                keys.Add("MA_KHO", txtMaKho.Text);
                var data = Categories.Select("Alkhoct1", keys).Data;
                if (data != null && data.Rows.Count > 0)
                {
                    var rowData = data.Rows[0].ToDataDictionary();
                    SetSomeData(new SortedDictionary<string, object>()
                {
                    {"PHOTOGRAPH", rowData["PHOTOGRAPH"] },
                    {"SIGNATURE", rowData["SIGNATURE"] }
                });
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadImageData", ex);
            }
        }

        
        public override void ValidateData()
        {
            var errors = "";
            if (txtMaKho.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaKho.Text;
            if (txtTenKho.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenKho.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_KHO",
                 txtMaKho.Text.Trim(), DataOld["MA_KHO"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaKho.Text + "=" + txtMaKho.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_KHO",
                 txtMaKho.Text.Trim(), txtMaKho.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaKho.Text + "=" + txtMaKho.Text;
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
                var chooseImage = V6ControlFormHelper.ChooseImage(this);
                if (chooseImage == null) return;

                ptbPHOTOGRAPH.Image = chooseImage;

                var ma_kho_new = txtMaKho.Text.Trim();
                var ma_kho_old = Mode == V6Mode.Edit ? DataOld["MA_KHO"].ToString().Trim() : ma_kho_new;
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UPDATE_ALKHOCT1",
                    new SqlParameter("@cMa_kho_old", ma_kho_old),
                    new SqlParameter("@cMa_kho_new", ma_kho_new));

                var photo = Picture.ToJpegByteArray(ptbPHOTOGRAPH.Image);
                //var sign = Picture.ToJpegByteArray(pictureBox2.Image);
                var data = new SortedDictionary<string, object> { { "PHOTOGRAPH", photo } };//, {"SIGNATURE", sign}};
                var keys = new SortedDictionary<string, object> { { "MA_KHO", txtMaKho.Text } };

                var result = V6BusinessHelper.UpdateTable("Alkhoct1", data, keys);

                if (result == 1)
                {
                    ShowTopLeftMessage(V6Text.Updated + "PHOTOGRAPH");
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
                var keys = new SortedDictionary<string, object> { { "MA_KHO", txtMaKho.Text } };

                var result = V6BusinessHelper.UpdateTable("Alkhoct1", data, keys);

                if (result == 1)
                {
                    ShowTopLeftMessage(V6Text.Updated + "PHOTOGRAPH");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + " " + GetType() + ".ChonHinh " + ex.Message);
            }
        }

        private void Chk_khodaily_CheckedChanged(object sender, EventArgs e)
        {
            TxtTk_dl.Enabled = (Chk_khodaily.Checked);
            TxtTk_dl.ReadOnly = !(Chk_khodaily.Checked);
            if (Chk_khodaily.Checked==false)
            {
                TxtTk_dl.Text = "";
            }



        }
        
    }
}
