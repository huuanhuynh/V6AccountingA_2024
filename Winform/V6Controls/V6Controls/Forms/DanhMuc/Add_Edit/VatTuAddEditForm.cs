using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class VatTuAddEditForm : AddEditControlVirtual
    {
        public VatTuAddEditForm()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            txtNhomVT1.SetInitFilter("Loai_nh=1");
            txtNhomVT2.SetInitFilter("Loai_nh=2");
            txtNhomVT3.SetInitFilter("Loai_nh=3");
            txtNhomVT4.SetInitFilter("Loai_nh=4");
            txtNhomVT5.SetInitFilter("Loai_nh=5");
            txtNhomVT6.SetInitFilter("Loai_nh=6");
            txtNhomVT7.SetInitFilter("Loai_nh=7");
            txtNhomVT8.SetInitFilter("Loai_nh=8");

            KeyField1 = "MA_VT";
        }

        private void VatTuFrom_Load(object sender, EventArgs e)
        {
            txtMaVT.Focus();
        }

        public override void DoBeforeEdit()
        {
            try
            {
                var v = Categories.IsExistOneCode_List("ABVT,ABLO,ARI70", "Ma_vt", txtMaVT.Text);
                txtMaVT.Enabled = !v;
                txtDVT.Enabled = !v;

                txttk_vt.ExistRowInTable();
                txttk_dt.ExistRowInTable();
                txttk_gv.ExistRowInTable();
                txttk_tl.ExistRowInTable();
                txttk_ck.ExistRowInTable();
                txttk_spdd.ExistRowInTable();
                txttk_cl_vt.ExistRowInTable();
                txttk_cp.ExistRowInTable();

                txtpma_nvien.ExistRowInTable();
                txtlma_nvien.ExistRowInTable();

                txtpma_khc.ExistRowInTable();
                txtpma_khp.ExistRowInTable();
                txtpma_khl.ExistRowInTable();

                //Get ALVTCT1->PHOTOGRAPH
                LoadImageData();

            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + " " + GetType() + ".DisableControlWhenEdit " + ex.Message, Application.ProductName);
            }
        }

        public override void AfterInsert()
        {
            UpdateAlqddvt();
        }

        public override void AfterUpdate()
        {
            UpdateAlqddvt();
        }

        private void LoadImageData()
        {
            SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
            keys.Add("MA_VT", txtMaVT.Text);
            var data = Categories.Select("Alvtct1", keys).Data;
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
            if (txtMaVT.Text.Trim() == "")
                errors += "Chưa nhập mã vật tư!\r\n";
            if (TxtTenVT.Text.Trim() == "")
                errors += "Chưa nhập tên vật tư!\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_VT",
                 txtMaVT.Text.Trim(), DataOld["MA_VT"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_VT = " + txtMaVT.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_VT",
                 txtMaVT.Text.Trim(), txtMaVT.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_VT = " + txtMaVT.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void UpdateAlqddvt()
        {
            try
            {
                var ma_vt_new = DataDic["MA_VT"].ToString().Trim();
                var ma_vt_old = Mode == V6Mode.Edit ? DataOld["MA_VT"].ToString().Trim() : ma_vt_new;

                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UPDATE_ALQDDVT",
                    new SqlParameter("@cMa_vt_old", ma_vt_old),
                    new SqlParameter("@cMa_vt_new", ma_vt_new));
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".UpdateAlqddvt", ex);
            }
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

                var ma_vt_new = txtMaVT.Text.Trim();
                var ma_vt_old = Mode == V6Mode.Edit ? DataOld["MA_VT"].ToString().Trim() : ma_vt_new;
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UPDATE_ALVTCT1",
                    new SqlParameter("@cMa_vt_old", ma_vt_old),
                    new SqlParameter("@cMa_vt_new", ma_vt_new));

                var photo = Picture.ToJpegByteArray(ptbPHOTOGRAPH.Image);
                //var sign = Picture.ToJpegByteArray(pictureBox2.Image);
                var data = new SortedDictionary<string, object> { { "PHOTOGRAPH", photo } };//, {"SIGNATURE", sign}};
                var keys = new SortedDictionary<string, object> { { "MA_VT", txtMaVT.Text } };

                var result = V6BusinessHelper.UpdateTable(V6TableName.Alvtct1.ToString(), data, keys);

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
        
        private void XoaHinh()
        {
            try
            {
                ptbPHOTOGRAPH.Image = null;

                var photo = Picture.ToJpegByteArray(ptbPHOTOGRAPH.Image);
                var data = new SortedDictionary<string, object> { { "PHOTOGRAPH", photo } };
                var keys = new SortedDictionary<string, object> { { "MA_VT", txtMaVT.Text } };

                var result = V6BusinessHelper.UpdateTable(V6TableName.Alvtct1.ToString(), data, keys);

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
    }
}
