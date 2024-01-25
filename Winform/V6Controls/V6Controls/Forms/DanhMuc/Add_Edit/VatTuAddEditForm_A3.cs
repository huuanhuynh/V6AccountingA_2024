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
    public partial class VatTuAddEditForm_A3 : AddEditControlVirtual
    {
        public VatTuAddEditForm_A3()
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
            MyInit2();
        }

        private void MyInit2()
        {
            try
            {
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
                this.WriteExLog(GetType() + ".DoBeforeEdit", ex);
            }
        }

        public override void DoBeforeEdit()
        {
            try
            {
                var v = Categories.IsExistOneCode_List("ABVT,ABLO,ARI70", "Ma_vt", txtMaVT.Text);
                txtMaVT.Enabled = !v;
                txtDVT.Enabled = !v;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeEdit", ex);
            }
        }

        public override void AfterInsert()
        {
            //UpdateAlqddvt();
        }

        public override void AfterUpdate()
        {
            //UpdateAlqddvt();
        }

        public override void AfterSave()
        {
            UpdateAlqddvt();
        }

        private void LoadImageData()
        {
            try
            {
                SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                keys.Add("MA_VT", txtMaVT.Text);
                var data = Categories.Select("Alvtct1", keys).Data;
                if (data != null && data.Rows.Count > 0)
                {
                    var rowData = data.Rows[0].ToDataDictionary();
                    DataOld["PHOTOGRAPH"] = rowData["PHOTOGRAPH"];
                    SetSomeData(new SortedDictionary<string, object>()
                    {
                        {"PHOTOGRAPH", rowData["PHOTOGRAPH"]},
                        {"SIGNATURE", rowData["SIGNATURE"]},
                        {"PDF1", rowData["PDF1"] },
                        {"PDF2", rowData["PDF2"] },
                        {"FILE_NAME1", rowData["FILE_NAME1"] },
                        {"FILE_NAME2", rowData["FILE_NAME2"] },
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
            if (txtMaVT.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMa.Text;
            if (TxtTenVT.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTen.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_VT",
                 txtMaVT.Text.Trim(), DataOld["MA_VT"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMa.Text + "=" + txtMaVT.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_VT",
                 txtMaVT.Text.Trim(), txtMaVT.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMa.Text + "=" + txtMaVT.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void UpdateAlqddvt()
        {
            try
            {
                var ma_vt_new = DataDic["MA_VT"].ToString().Trim();
                var ma_vt_old = Mode == V6Mode.Edit ? DataOld["MA_VT"].ToString().Trim() : ma_vt_new;
                V6BusinessHelper.UpdateAlqddvt(ma_vt_old, ma_vt_new);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".UpdateAlqddvt", ex);
            }
        }

        private void ChonHinh()
        {
            try
            {
                var chooseImage = V6ControlFormHelper.ChooseImage(this);
                if (chooseImage == null) return;

                ptbPHOTOGRAPH.Image = Picture.ResizeDownImage(chooseImage, ptbPHOTOGRAPH.Width, ptbPHOTOGRAPH.Height);

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
                    ShowMainMessage(V6Text.Updated + "PHOTOGRAPH");
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
                    ShowMainMessage(V6Text.Updated + "PHOTOGRAPH");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + " " + GetType() + ".ChonHinh " + ex.Message);
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
                var keys = new SortedDictionary<string, object> { { "MA_VT", txtMaVT.Text } };

                var result = V6BusinessHelper.UpdateTable(V6TableName.Alvtct1.ToString(), data, keys);

                if (result == 1)
                {
                    ShowMainMessage(V6Text.Updated + FIELD);
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
        //        var keys = new SortedDictionary<string, object> { { "MA_VT", txtMaVT.Text } };

        //        var result = V6BusinessHelper.UpdateTable(V6TableName.Alvtct1.ToString(), data, keys);

        //        if (result == 1)
        //        {
        //            ShowMainMessage(V6Text.Updated + FIELD);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.WriteExLog(GetType() + ".ChonPDF " + FIELD, ex);
        //    }
        //}

        private void XoaPDF(string FIELD)
        {
            try
            {
                var data = new SortedDictionary<string, object> { { FIELD, null } };
                var keys = new SortedDictionary<string, object> { { "MA_VT", txtMaVT.Text } };
                var result = V6BusinessHelper.UpdateTable(V6TableName.Alvtct1.ToString(), data, keys);

                if (result == 1)
                {
                    ShowMainMessage(V6Text.Updated + FIELD);
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
                    V6ControlFormHelper.OpenFileBytes((byte[])DataOld[FIELD], "pdf");
                }
                else
                {
                    ShowMainMessage(V6Text.NoData);
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
                    ShowMainMessage(V6Text.NotFound);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XemFile " + fileName, ex);
            }
        }


        private void btnChonPDF_Click(object sender, EventArgs e)
        {
            ChonPDF("PDF1", "PDF files|*.PDF");
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

        private void btnChonFile1_AfterProcess(object sender, Controls.FileButton.Event_Args e)
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
                        var keys = new SortedDictionary<string, object> { { "MA_VT", txtMaVT.Text } };

                        var result = V6BusinessHelper.UpdateTable(V6TableName.Alvtct1.ToString(), data, keys);

                        if (result == 1)
                        {
                            ShowMainMessage(V6Text.Updated + FIELD);
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
                        var keys = new SortedDictionary<string, object> { { "MA_VT", txtMaVT.Text } };
                        var result = V6BusinessHelper.UpdateTable(V6TableName.Alvtct1.ToString(), data, keys);

                        if (result == 1)
                        {
                            ShowMainMessage(V6Text.Updated + FIELD);
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

    }
}
