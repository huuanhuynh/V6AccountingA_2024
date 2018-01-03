using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Controls;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class KhachHangAddEditFrom : AddEditControlVirtual
    {
        public KhachHangAddEditFrom()
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

        private void KhachHangFrom_Load(object sender, System.EventArgs e)
        {
            txtNhomKH1.SetInitFilter("Loai_nh=1");
            txtNhomKH2.SetInitFilter("Loai_nh=2");
            txtNhomKH3.SetInitFilter("Loai_nh=3");
            txtNhomKH4.SetInitFilter("Loai_nh=4");
            txtNhomKH5.SetInitFilter("Loai_nh=5");
            txtNhomKH6.SetInitFilter("Loai_nh=6");

            txtloai_kh.ExistRowInTable();

            txtMaKH.Focus();
        }

        public override void DoBeforeEdit()
        {
            try
            {
                var v = Categories.IsExistOneCode_List("ABKH,ARA00,ARI70", "Ma_kh", txtMaKH.Text);
                txtMaKH.Enabled = !v;

                if (!V6Login.IsAdmin && TxtMa_dvcs.Text.ToUpper() != V6Login.Madvcs.ToUpper())
                {
                    TxtMa_dvcs.Enabled = false;
                }

                LoadImageData();
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("KhachHang DisableWhenEdit " + ex.Message);
            }
        }

        private void LoadImageData()
        {
            SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
            keys.Add("MA_KH", txtMaKH.Text);
            var data = Categories.Select("Alkhct1", keys).Data;
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

        public override void ValidateData()
        {
             
               var errors = "";
            if (txtMaKH.Text.Trim() == "" || txtTenKH.Text.Trim() == "")
                errors += V6Init.V6Text.CheckInfor + " !\r\n";
            if (V6Login.MadvcsTotal > 0 && TxtMa_dvcs.Text.Trim() == "")
                errors += "Chưa nhập đơn vị cơ sở !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0,"MA_KH",
                 txtMaKH.Text.Trim(), DataOld["MA_KH"].ToString());
                if (!b)
                    throw new Exception(V6Init.V6Text.ExistData
                                                    + "MA_KH = " + txtMaKH.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_KH",
                 txtMaKH.Text.Trim(), txtMaKH.Text.Trim());
                if (!b)
                    throw new Exception(V6Init.V6Text.ExistData
                                                    + "MA_KH = " + txtMaKH.Text.Trim());
            }

            if(errors.Length>0) throw new Exception(errors);
        }

        public override void AfterUpdate()
        {
            UpdateAlkhct();
        }

        public override void AfterInsert()
        {
            UpdateAlkhct();
        }

        private void UpdateAlkhct()
        {
            try
            {
                var newID = DataDic["MA_KH"].ToString().Trim();
                //var oldID = Mode == V6Mode.Edit ? DataOld["MA_KH"].ToString().Trim() : newID;
                //var uidKH = DataOld["UID"].ToString();
                //var sql = string.Format("Update ALKHCT set MA_KH='{0}' where UID='{1}'", newID, uidKH);

                SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                data.Add("MA_KH", newID);

                // Tuanmh 25/05/2017 loi Null
                if (_keys != null)
                {
                    Categories.Update("ALKHCT", data, _keys);
                 
                }

                // 19/03/2016 ADD ALKHCT1
                var ma_kh_new = DataDic["MA_KH"].ToString().Trim();
                var ma_kh_old = Mode == V6Mode.Edit ? DataOld["MA_KH"].ToString().Trim() : ma_kh_new;

                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UPDATE_ALKHCT1",
                    new SqlParameter("@cMa_kh_old", ma_kh_old),
                    new SqlParameter("@cMa_kh_new", ma_kh_new));
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".UpdateAlkhct", ex);
            }
        }

        private void btnUpdateHtt_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@cMa_kh", txtMaKH.Text), 
                    new SqlParameter("@nHan_tt", numHantt.Value), 
                };
                var a = V6BusinessHelper.ExecuteProcedureNoneQuery("AARKH_UPDATE_ARS20", plist);

                ShowTopMessage(V6Text.UpdateSuccess);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".UpdateHtt " + ex.Message);
            }
        }

        private void btnBoSung_Click(object sender, EventArgs e)
        {
            try
            {
                var uid_kh = DataOld["UID"].ToString();
                var ma_kh_old = DataOld["MA_KH"].ToString().Trim();
                var data = new Dictionary<string, object>();
                //FormAddEdit editForm = new FormAddEdit(V6TableName.Alkhct,);
                
                CategoryView dmView = new CategoryView(ItemID, "title", "Alkhct", "uid_kh='"+uid_kh+"'", null, DataOld);
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

        private void btnChonhinh_Click(object sender, EventArgs e)
        {
            ChonHinh();
        }
        
        private void btnChonhinhS_Click(object sender, EventArgs e)
        {
            ChonHinhS();
        }

        private void ChonHinh()
        {
            try
            {
                var chooseImage = V6ControlFormHelper.ChooseImage(this);
                if (chooseImage == null) return;

                ptbPHOTOGRAPH.Image = chooseImage;

                var ma_kh_new = txtMaKH.Text.Trim();
                var ma_kh_old = Mode == V6Mode.Edit ? DataOld["MA_KH"].ToString().Trim() : ma_kh_new;
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UPDATE_ALKHCT1",
                    new SqlParameter("@cMa_kh_old", ma_kh_old),
                    new SqlParameter("@cMa_kh_new", ma_kh_new));

                var photo = Picture.ToJpegByteArray(ptbPHOTOGRAPH.Image);
                //var sign = Picture.ToJpegByteArray(pictureBox2.Image);
                var data = new SortedDictionary<string, object> { { "PHOTOGRAPH", photo } };//, {"SIGNATURE", sign}};
                var keys = new SortedDictionary<string, object> { { "MA_KH", txtMaKH.Text } };

                var result = V6BusinessHelper.UpdateTable(V6TableName.Alkhct1.ToString(), data, keys);

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
        
        private void ChonHinhS()
        {
            try
            {
                var chooseImage = V6ControlFormHelper.ChooseImage(this);
                if (chooseImage == null) return;

                pictureBoxS.Image = chooseImage;

                var ma_kh_new = txtMaKH.Text.Trim();
                var ma_kh_old = Mode == V6Mode.Edit ? DataOld["MA_KH"].ToString().Trim() : ma_kh_new;
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UPDATE_ALKHCT1",
                    new SqlParameter("@cMa_kh_old", ma_kh_old),
                    new SqlParameter("@cMa_kh_new", ma_kh_new));

                var photo = Picture.ToJpegByteArray(pictureBoxS.Image);
                //var sign = Picture.ToJpegByteArray(pictureBox2.Image);
                var data = new SortedDictionary<string, object> { { "SIGNATURE", photo } };//, {"SIGNATURE", sign}};
                var keys = new SortedDictionary<string, object> { { "MA_KH", txtMaKH.Text } };

                var result = V6BusinessHelper.UpdateTable(V6TableName.Alkhct1.ToString(), data, keys);

                if (result == 1)
                {
                    ShowTopMessage(V6Text.Updated + "SIGNATURE");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + " " + GetType() + ".ChonHinhS " + ex.Message);
            }
        }
        
        private void XoaHinh()
        {
            try
            {
                ptbPHOTOGRAPH.Image = null;

                var photo = Picture.ToJpegByteArray(ptbPHOTOGRAPH.Image);
                var data = new SortedDictionary<string, object> { { "PHOTOGRAPH", photo } };
                var keys = new SortedDictionary<string, object> { { "MA_KH", txtMaKH.Text } };

                var result = V6BusinessHelper.UpdateTable(V6TableName.Alkhct1.ToString(), data, keys);

                if (result == 1)
                {
                    ShowTopMessage(V6Text.Updated + "PHOTOGRAPH");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + " " + GetType() + ".XoaHinh " + ex.Message);
            }
        }
        
        private void XoaHinhS()
        {
            try
            {
                pictureBoxS.Image = null;

                var photo = Picture.ToJpegByteArray(pictureBoxS.Image);
                var data = new SortedDictionary<string, object> { { "SIGNATURE", photo } };
                var keys = new SortedDictionary<string, object> { { "MA_KH", txtMaKH.Text } };

                var result = V6BusinessHelper.UpdateTable(V6TableName.Alkhct1.ToString(), data, keys);

                if (result == 1)
                {
                    ShowTopMessage(V6Text.Updated + "SIGNATURE");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + " " + GetType() + ".XoaHinhS " + ex.Message);
            }
        }

        private void btnXoahinh_Click(object sender, EventArgs e)
        {
            XoaHinh();
        }
        
        private void btnXoahinhS_Click(object sender, EventArgs e)
        {
            XoaHinhS();
        }

    }
}
