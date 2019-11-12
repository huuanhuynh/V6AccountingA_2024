using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class V6SoftAddEditForm : AddEditControlVirtual
    {
        public V6SoftAddEditForm()
        {
            InitializeComponent();
        }

        private void KhachHangFrom_Load(object sender, EventArgs e)
        {
            txtval.Focus();
        }
        public override void DoBeforeEdit()
        {
            if (DataOld["NAME"].ToString().Trim() == "M_TEN_CTY")
            {
                ShowPictureSelect();
            }
        }
        public override void ValidateData()
        {
            
        }

        private void ShowPictureSelect()
        {
            try
            {
                btnChonhinh.Visible = true;
                btnXoahinh.Visible = true;
                picLOGO.Image = Picture.ToImage(DataOld["LOGO"]);
                picLOGO.Visible = true;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ShowPictureSelect", ex);
            }
        }

        private void ChonHinh()
        {
            try
            {
                var chooseImage = V6ControlFormHelper.ChooseImage(this);
                if (chooseImage == null) return;

                picLOGO.Image = chooseImage;

                var photo = Picture.ToJpegByteArray(picLOGO.Image);
                var data = new SortedDictionary<string, object> { { "LOGO", photo } };
                var keys = new SortedDictionary<string, object> { { "NAME", txtName.Text } };

                var result = V6BusinessHelper.UpdateTable(V6TableName.V6soft.ToString(), data, keys);

                if (result == 1)
                {
                    ShowTopLeftMessage(V6Text.Updated + "LOGO");
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ChonHinh", ex);
            }
        }

        private void XoaHinh()
        {
            try
            {
                picLOGO.Image = null;

                var photo = Picture.ToJpegByteArray(picLOGO.Image);
                var data = new SortedDictionary<string, object> { { "LOGO", photo } };
                var keys = new SortedDictionary<string, object> { { "NAME", txtName.Text } };

                var result = V6BusinessHelper.UpdateTable(V6TableName.V6soft.ToString(), data, keys);

                if (result == 1)
                {
                    ShowTopLeftMessage(V6Text.Updated + "LOGO");
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XoaHinh", ex);
            }
        }

        private void btnChonhinh_Click(object sender, EventArgs e)
        {
            ChonHinh();
        }

        private void btnXoahinh_Click(object sender, EventArgs e)
        {
            XoaHinh();
        }
    }
}
