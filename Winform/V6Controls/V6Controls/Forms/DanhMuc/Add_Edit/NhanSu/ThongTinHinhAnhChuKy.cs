using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class ThongTinHinhAnhChuKy : AddEditControlVirtual
    {
        public ThongTinHinhAnhChuKy()
        {
            InitializeComponent();
        }

        public override void DoBeforeEdit()
        {

           

        }

        private void ChonHinh()
        {
            try
            {
                var chooseImage = V6ControlFormHelper.ChooseImage();
                if (chooseImage == null) return;

                pictureBox1.Image = chooseImage;

                var photo = Picture.ToJpegByteArray(pictureBox1.Image);
                //var sign = Picture.ToJpegByteArray(pictureBox2.Image);
                var data = new SortedDictionary<string, object> {{"PHOTOGRAPH", photo}};//, {"SIGNATURE", sign}};
                var keys = new SortedDictionary<string, object> {{"STT_REC", txtSttRec.Text}};

                var result = V6BusinessHelper.UpdateTable(V6TableName.Hrimages.ToString(), data, keys);

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
        
        private void ChonChuKy()
        {
            try
            {
                var chooseImage = V6ControlFormHelper.ChooseImage();
                if (chooseImage == null) return;

                pictureBox2.Image = chooseImage;

                var sign = Picture.ToJpegByteArray(pictureBox2.Image);
                var data = new SortedDictionary<string, object> { {"SIGNATURE", sign}};
                var keys = new SortedDictionary<string, object> { { "STT_REC", txtSttRec.Text } };

                var result = V6BusinessHelper.UpdateTable(V6TableName.Hrimages.ToString(), data, keys);

                if (result == 1)
                {
                    ShowTopMessage(V6Text.Updated + "SIGNATURE");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + " " + GetType() + ".ChonChuKy " + ex.Message);
            }
        }

        
        private void XoaHinh()
        {
            try
            {
                pictureBox1.Image = null;

                var photo = Picture.ToJpegByteArray(pictureBox1.Image);
                var data = new SortedDictionary<string, object> { { "PHOTOGRAPH", photo } };
                var keys = new SortedDictionary<string, object> { { "STT_REC", txtSttRec.Text } };

                var result = V6BusinessHelper.UpdateTable(V6TableName.Hrimages.ToString(), data, keys);

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

        private void XoaChuKy()
        {
            try
            {
                pictureBox2.Image = null;

                var sign = Picture.ToJpegByteArray(pictureBox2.Image);
                var data = new SortedDictionary<string, object> { { "SIGNATURE", sign } };
                var keys = new SortedDictionary<string, object> { { "STT_REC", txtSttRec.Text } };

                var result = V6BusinessHelper.UpdateTable(V6TableName.Hrimages.ToString(), data, keys);

                if (result == 1)
                {
                    ShowTopMessage(V6Text.Updated + "SIGNATURE");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + " " + GetType() + ".XoaChuKy " + ex.Message);
            }
        }

        private void btnChonhinh_Click(object sender, System.EventArgs e)
        {
            ChonHinh();
        }

        private void btnChonchuky_Click(object sender, EventArgs e)
        {
            ChonChuKy();
        }

        private void btnXoahinh_Click(object sender, EventArgs e)
        {
            XoaHinh();
        }

        private void btnXoachuky_Click(object sender, EventArgs e)
        {
            XoaChuKy();
        }
    }
}
