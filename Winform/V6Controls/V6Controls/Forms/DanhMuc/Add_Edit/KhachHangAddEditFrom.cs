using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
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
                    txtMaDVCS.Text = V6Login.Madvcs;
                }
            }
            else if (Mode == V6Mode.Edit)
            {
                
            }
        }

        private void KhachHangFrom_Load(object sender, EventArgs e)
        {
            InitCTView();
            txtNhomKH1.SetInitFilter("Loai_nh=1");
            txtNhomKH2.SetInitFilter("Loai_nh=2");
            txtNhomKH3.SetInitFilter("Loai_nh=3");
            txtNhomKH4.SetInitFilter("Loai_nh=4");
            txtNhomKH5.SetInitFilter("Loai_nh=5");
            txtNhomKH6.SetInitFilter("Loai_nh=6");
            txtNhomKH7.SetInitFilter("Loai_nh=7");
            txtNhomKH8.SetInitFilter("Loai_nh=8");
            txtNhomKH9.SetInitFilter("Loai_nh=9");

            txtloai_kh.ExistRowInTable();

            txtMaKH.Focus();
        }

        public override void DoBeforeEdit()
        {
            try
            {
                var v = Categories.IsExistOneCode_List("ABKH,ARA00,ARI70", "Ma_kh", txtMaKH.Text);
                txtMaKH.Enabled = !v;

                if (!V6Login.IsAdmin && txtMaDVCS.Text.ToUpper() != V6Login.Madvcs.ToUpper())
                {
                    txtMaDVCS.Enabled = false;
                }

                LoadImageData();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeEdit", ex);
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
                    {"SIGNATURE", rowData["SIGNATURE"] },
                    {"PDF1", rowData["PDF1"] },
                    {"PDF2", rowData["PDF2"] },
                    {"FILE_NAME1", rowData["FILE_NAME1"] },
                    {"FILE_NAME2", rowData["FILE_NAME2"] },
                });
            }
        }

        public override void ValidateData()
        {
            var errors = "";

            if (txtMaKH.Text.Trim() == "" || txtTenKH.Text.Trim() == "")
                errors += V6Text.CheckInfor + " !\r\n";
            if (V6Login.MadvcsTotal > 0 && txtMaDVCS.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaDVCS.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM.ToString(), 0,"MA_KH", txtMaKH.Text.Trim(), DataOld["MA_KH"].ToString());
                if (!b) throw new Exception(string.Format("{0} {1} = {2}", V6Text.DataExist, lblMaKH.Text, txtMaKH.Text));
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM.ToString(), 1, "MA_KH", txtMaKH.Text.Trim(), txtMaKH.Text.Trim());
                if (!b) throw new Exception(string.Format("{0} {1} = {2}", V6Text.DataExist, lblMaKH.Text, txtMaKH.Text));
            }

            if (txtMaSoThueVAT.Text.Trim().Length >= 10 && V6Options.M_QLY_MA_SO_THUE.StartsWith("1"))
            {
                if (!V6BusinessHelper.CheckMST(txtMaSoThueVAT.Text.Trim()))
                {
                    errors += V6Text.CheckData + " " + txtMaSoThueVAT.AccessibleName;
                }
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

                ShowTopLeftMessage(V6Text.UpdateSuccess);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".UpdateHtt", ex);
            }
        }

        private void InitCTView()
        {
            try
            {
                CategoryView dmView = new CategoryView();
                if (Mode == V6Mode.Add)
                {
                    tabChiTiet.Enabled = false;
                }
                if (Mode == V6Mode.Edit || Mode == V6Mode.View)
                {
                    var uid_kh = DataOld["UID"].ToString();
                    var ma_kh_old = DataOld["MA_KH"].ToString().Trim();
                    var data = new Dictionary<string, object>();
                    dmView = new CategoryView(ItemID, "title", "Alkhct", "uid_kh='" + uid_kh + "'", null, DataOld);
                    if (Mode == V6Mode.View)
                    {
                        dmView.EnableAdd = false;
                        dmView.EnableCopy = false;
                        dmView.EnableDelete = false;
                        dmView.EnableEdit = false;
                    }
                }

                dmView.btnBack.Enabled = false;
                dmView.btnBack.Visible = false;
                dmView.Dock = DockStyle.Fill;
                tabChiTiet.Controls.Add(dmView);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".InitCTView", ex);
            }
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
                    ShowTopLeftMessage(V6Text.Updated + "PHOTOGRAPH");
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ChonHinh", ex);
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
                    ShowTopLeftMessage(V6Text.Updated + "SIGNATURE");
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ChonHinhS", ex);
            }
        }

        private void ChonPDF(string FIELD, string fileFilter)
        {
            try
            {
                var filePath = V6ControlFormHelper.ChooseOpenFile(this, fileFilter);
                if (filePath == null) return;

                //var photo = ;
                byte[] fileBytes = File.ReadAllBytes(filePath);
                //DataOld là chỗ chứa tạm.
                if (DataOld == null) DataOld = new SortedDictionary<string, object>();
                DataOld[FIELD] = fileBytes;
                //var sign = Picture.ToJpegByteArray(pictureBox2.Image);
                var data = new SortedDictionary<string, object> { { FIELD, fileBytes } };
                var keys = new SortedDictionary<string, object> { { "MA_KH", txtMaKH.Text } };

                var result = V6BusinessHelper.UpdateTable(V6TableName.Alkhct1.ToString(), data, keys);

                if (result == 1)
                {
                    ShowTopLeftMessage(V6Text.Updated + "PHOTOGRAPH");
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ChonPDF " + FIELD, ex);
            }
        }
        
        //private void ChonFile(string fileFilter, string FIELD, TextBox txtFileName)
        //{
        //    try
        //    {
        //        var filePath = V6ControlFormHelper.ChooseOpenFile(this, fileFilter);
        //        if (filePath == null) return;

        //        var _setting = new H.Setting(Path.Combine(V6Login.StartupPath, "Setting.ini"));
        //        var info = new V6IOInfo()
        //        {
        //            FileName = filePath,
        //            FTP_IP = _setting.GetSetting("FTP_IP"),
        //            FTP_USER = _setting.GetSetting("FTP_USER"),
        //            FTP_EPASS = _setting.GetSetting("FTP_EPASS"),
        //            FTP_SUBFOLDER = _setting.GetSetting("FTP_V6DOCSFOLDER"),
        //        };
        //        V6FileIO.CopyToVPN(info);

        //        txtFileName.Text = Path.GetFileName(filePath);
        //        var data = new SortedDictionary<string, object> { { FIELD, txtFileName1.Text } };
        //        var keys = new SortedDictionary<string, object> { { "MA_KH", txtMaKH.Text } };

        //        var result = V6BusinessHelper.UpdateTable(V6TableName.Alkhct1.ToString(), data, keys);

        //        if (result == 1)
        //        {
        //            ShowTopLeftMessage(V6Text.Updated + FIELD);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.WriteExLog(GetType() + ".ChonPDF " + FIELD, ex);
        //    }
        //}
        
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
                    ShowTopLeftMessage(V6Text.Updated + "PHOTOGRAPH");
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
                    ShowTopLeftMessage(V6Text.Updated + "SIGNATURE");
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XoaHinhS", ex);
            }
        }
        
        private void XoaPDF(string FIELD)
        {
            try
            {
                var data = new SortedDictionary<string, object> { { FIELD, null } };
                var keys = new SortedDictionary<string, object> { { "MA_KH", txtMaKH.Text } };
                var result = V6BusinessHelper.UpdateTable(V6TableName.Alkhct1.ToString(), data, keys);

                if (result == 1)
                {
                    ShowTopLeftMessage(V6Text.Updated + FIELD);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XoaPDF " + FIELD, ex);
            }
        }

        private void XemPDF(string FIELD)
        {
            try
            {
                //DataOld là chỗ chứa tạm.
                if (DataOld != null && DataOld.ContainsKey(FIELD))
                {
                    V6ControlFormHelper.OpenFileBytes((byte[]) DataOld[FIELD], "pdf");
                }
                else
                {
                    ShowTopLeftMessage(V6Text.NoData);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XemPDF", ex);
            }
        }
        
        private void XemFile(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName)) return;

                var _setting = new H.Setting(Path.Combine(V6Login.StartupPath, "Setting.ini"));
                V6IOInfo info = new V6IOInfo()
                {
                    FileName = fileName,
                    FTP_IP = _setting.GetSetting("FTP_IP"),
                    FTP_USER = _setting.GetSetting("FTP_USER"),
                    FTP_EPASS = _setting.GetSetting("FTP_EPASS"),
                    FTP_SUBFOLDER = _setting.GetSetting("FTP_V6DOCSFOLDER"),
                    LOCAL_FOLDER = V6Setting.V6SoftLocalAppData_Directory,
                };
                if (V6FileIO.CopyFromVPN(info))
                {
                    string tempFile = Path.Combine(V6Setting.V6SoftLocalAppData_Directory, fileName);
                    Process.Start(tempFile);
                }
                else
                {
                    ShowTopLeftMessage(V6Text.NotFound);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XemFile " + fileName, ex);
            }
        }
        
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                linkLabel1.LinkVisited = true;
                if (txtHomePage.Text.Trim() != "") Process.Start(txtHomePage.Text);
            }
            catch (Exception)
            {
                //
            }
        }

        private void btnBoSung_Click(object sender, EventArgs e)
        {
            tabChiTiet.Focus();
            v6TabControl1.SelectedTab = tabChiTiet;
        }

        private void btnChonhinh_Click(object sender, EventArgs e)
        {
            ChonHinh();
        }

        private void btnChonhinhS_Click(object sender, EventArgs e)
        {
            ChonHinhS();
        }
        
        private void btnXoahinh_Click(object sender, EventArgs e)
        {
            XoaHinh();
        }

        private void btnXoahinhS_Click(object sender, EventArgs e)
        {
            XoaHinhS();
        }


        private void btnChonPDF_Click(object sender, EventArgs e)
        {
            ChonPDF( "PDF1", "PDF files|*.PDF");
        }

        private void btnChonPDF2_Click(object sender, EventArgs e)
        {
            ChonPDF("PDF2", "PDF files|*.PDF");
        }
        
        private void btnXoaPDF_Click(object sender, EventArgs e)
        {
            XoaPDF("PDF1");
        }

        private void btnXoaPDF2_Click(object sender, EventArgs e)
        {
            XoaPDF("PDF2");
        }

        private void btnXemPDF_Click(object sender, EventArgs e)
        {
            XemPDF("PDF1");
        }

        private void btnXemPDF2_Click(object sender, EventArgs e)
        {
            XemPDF("PDF2");
        }
        
        private void btnChonFile0_AfterProcess(object sender, FileButton.Event_Args e)
        {
            try
            {
                string FIELD = e.Sender.AccessibleName.Trim().ToUpper();
                if (e.Mode == FileButton.FileButtonMode.ChooseFile)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(e.Sender.FileName)) return;
                        
                        
                        var data = new SortedDictionary<string, object> { { FIELD, e.Sender.FileName } };
                        var keys = new SortedDictionary<string, object> { { "MA_KH", txtMaKH.Text } };

                        var result = V6BusinessHelper.UpdateTable(V6TableName.Alkhct1.ToString(), data, keys);

                        if (result == 1)
                        {
                            ShowTopLeftMessage(V6Text.Updated + FIELD);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ChooseFile: " + ex.Message);
                    }
                }
                else if (e.Mode == FileButton.FileButtonMode.Clear)
                {
                    try
                    {
                        var data = new SortedDictionary<string, object> { { FIELD, null } };
                        var keys = new SortedDictionary<string, object> { { "MA_KH", txtMaKH.Text } };
                        var result = V6BusinessHelper.UpdateTable(V6TableName.Alkhct1.ToString(), data, keys);

                        if (result == 1)
                        {
                            ShowTopLeftMessage(V6Text.Updated + FIELD);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Clear:" + FIELD + ex.Message);
                    }
                }
                else if (e.Mode == FileButton.FileButtonMode.OpenFile)
                {
                    string openFile = e.OpenFile;
                    //File.Delete(openFile);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnChonFile_AfterProcess", ex);
            }
        }

        private void btnChonFile0_FileNameChanged(object sender, FileButton.Event_Args e)
        {
            toolTipV6FormControl.SetToolTip(e.Sender, e.NewFileName);
        }

    }
}
