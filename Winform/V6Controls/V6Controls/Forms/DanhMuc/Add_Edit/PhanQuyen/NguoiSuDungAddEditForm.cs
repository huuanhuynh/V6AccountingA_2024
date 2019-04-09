using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Forms.Editor;
using V6Controls.Forms.Viewer;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit.PhanQuyen
{
    public partial class NguoiSuDungAddEditForm : AddEditControlVirtual
    {
        public NguoiSuDungAddEditForm()
        {
            InitializeComponent();

            MyInit();
        }

        private void MyInit()
        {
            try
            {
                var inheritData = V6BusinessHelper.Select("V6User", "User_ID,User_Name").Data;
                cboInherit.ValueMember = "User_ID";
                cboInherit.DisplayMember = "User_Name";
                cboInherit.DataSource = inheritData;
                cboInherit.ValueMember = "User_ID";
                cboInherit.DisplayMember = "User_Name";


                var levelData = V6BusinessHelper.Select("AlLevel").Data;
                cboLevel.ValueMember = "ma_level";
                cboLevel.DisplayMember = "ten_level";
                cboLevel.DataSource = levelData;
                cboLevel.ValueMember = "ma_level";
                cboLevel.DisplayMember = "ten_level";
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("MyInit: " + ex.Message);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                if (TxtPassword.Text.Trim() != "")
                {
                    var descript = UtilityHelper.DeCrypt(TxtPassword.Text);
                    if (descript.Length >= txtUser_name.Text.Trim().Length)
                    {
                        txtPassword1.Text = descript.Substring(txtUser_name.Text.Trim().Length);
                    }
                }
                txtPassword2.Text = txtPassword1.Text;
                if (TxtCodeUser.Text.Trim() != "")
                {
                    var code = UtilityHelper.DeCrypt(TxtCodeUser.Text);
                    if (!string.IsNullOrEmpty(code))
                    {
                        var isAdmin = code.Right(1) == "1";
                        chkIs_admin.Checked = isAdmin;
                        chkIsVadmin.Checked = isAdmin;
                        if (Mode == V6Mode.Add)
                        {
                            chkIs_admin.Checked = false;
                        }
                    }
                }

                // Only Admin 
                if (Mode != V6Mode.View)
                {
                    var enable = V6Login.IsAdmin;

                    cboInherit.Enabled = enable;
                    cboLevel.Enabled = enable;
                    btnPhanQuyen.Enabled = enable;
                    btnPhanQuyenDvcs.Enabled = enable;
                    btnPhanQuyenKho.Enabled = enable;
                    btnPhanQuyenSnb.Enabled = enable;
                    bntPhanQuyenCtct.Enabled = enable;
                    txtInherit_type.Enabled = enable;

                    chkInherit_ch.Enabled = enable;
                    chkPass_Exp.Enabled = enable;
                    chkIs_admin.Enabled = enable;
                    chkUser_acc.Enabled = enable;
                    chkUser_inv.Enabled = enable;
                    chkUser_sale.Enabled = enable;
                    v6chk_set_vattu_ketoan_banhang.Enabled = enable;
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("Load: " + ex.Message);
            }
        }

        public override void V6F3Execute()
        {
            try
            {
                txtPassword1.PasswordChar = '\0';
                txtXmlInfor.ReadOnly = false;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".V6F3Execute", ex);
            }
        }

        public override void DoBeforeAdd()
        {
            try
            {
                txtModuleID.Text = V6Options.MODULE_ID;
                var max = Convert.ToInt32(SqlConnect.ExecuteScalar(CommandType.Text, "Select Max(User_ID) from V6user"));
                TxtUser_id.Value = max + 1;

                txtInherit_type.Text = "1,2,3,4,5";
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoBeforeAdd " + ex.Message);
            }
        }

        public override void DoBeforeCopy()
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@User_id", V6Login.UserId), 
                    new SqlParameter("@User_id_From", DataOld["USER_ID"]), 
                    new SqlParameter("@User_id_To", (int)TxtUser_id.Value) 
                };
                V6BusinessHelper.ExecuteProcedure("VPA_COPY_ALCTCT", plist);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoBeforeCopy " + ex.Message);
            }
        }

        public override void DoBeforeEdit()
        {
            
        }

        public override void FixFormData()
        {
            TxtPassword.Text = UtilityHelper.EnCrypt(txtUser_name.Text.Trim() + txtPassword1.Text.Trim());
            TxtCodeUser.Text =
                UtilityHelper.EnCrypt(txtUser_name.Text.Trim() + txtPassword1.Text.Trim() +
                                        (chkIs_admin.Checked ? "1" : "0"));

            if (Mode == V6Mode.Add)
            {
                txtPassDate.Value = V6Setting.M_SV_DATE;
            }
            else if (Mode == V6Mode.Edit)
            {
                string old_pass = ObjectAndString.ObjectToString(DataOld["Password".ToUpper()]);
                string new_pass = TxtPassword.Text.Trim();
                if (new_pass != old_pass)
                {
                    txtPassDate.Value = V6Setting.M_SV_DATE;
                }
            }
        }

        public override void SetData(IDictionary<string, object> d)
        {
            base.SetData(d);
            if (Mode == V6Mode.Add)
            {
                chkIs_admin.Checked = false;
            }
        }

        public override void ValidateData()
        {
            if ((Mode == V6Mode.Add || Mode == V6Mode.Edit) && !V6Login.IsAdmin)
            {
                if(V6Login.UserId != ObjectAndString.ObjectToInt(TxtUser_id.Text))
                throw new Exception(GetType() + ".ValidateData " + V6Text.NoRight);
            }

            var errors = "";
            if (txtUser_name.Text.Trim() == "") errors += V6Text.Text("CHUANHAP") + " " + lblUserName.Text;
            if (txtComment.Text.Trim() == "") errors += V6Text.Text("CHUANHAP") + " " + lblComment.Text;
            if (txtPassword1.Text != txtPassword2.Text) errors += V6Text.Wrong + "!\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "USER_NAME",
                 txtUser_name.Text.Trim(), DataOld["USER_NAME"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblUserName.Text + "=" + txtUser_name.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "USER_NAME",
                 txtUser_name.Text.Trim(), txtUser_name.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblUserName.Text + "=" + txtUser_name.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void btnPhanQuyen_Click(object sender, EventArgs e)
        {
            try
            {
                using (PhanQuyen phanQuyen = new PhanQuyen(Mode)
                    {
                        Text =V6Setting.Language=="V"?"Phân quyền.":"Set rights",
                        WindowState = FormWindowState.Maximized,
                        Vrights = txtRights.Text,
                        Vrights_Add = txtRightAdd.Text,
                        Vrights_Copy = txtRightCopy.Text,
                        Vrights_Edit = txtRightEdit.Text,
                        Vrights_Delete = txtRightDelete.Text,
                        Vrights_View = txtRightView.Text,
                        Vrights_Print = txtRightPrint.Text

                    })
                {
                    if (phanQuyen.ShowDialog(this) == DialogResult.OK)
                    {
                        txtRights.Text = phanQuyen.Vrights;
                        txtRightAdd.Text = phanQuyen.Vrights_Add;
                        txtRightCopy.Text = phanQuyen.Vrights_Copy;
                        txtRightEdit.Text = phanQuyen.Vrights_Edit;
                        txtRightDelete.Text = phanQuyen.Vrights_Delete;
                        txtRightView.Text = phanQuyen.Vrights_View;
                        txtRightPrint.Text = phanQuyen.Vrights_Print;
                    }
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("PhanQuyen: " + ex.Message);
            }
        }

      
        private void btnPhanQuyenDvcs_Click(object sender, EventArgs e)
        {
            try
            {
                using (PhanQuyenDvcs phanQuyenDvcs = new PhanQuyenDvcs(Mode)
                    {
                        Text = V6Setting.Language=="V"?"Phân quyền theo đơn vị cơ sở.":"Set agent rights",
                        //WindowState = FormWindowState.Normal,
                        Vrights_dvcs = txtRightDvcs.Text
                    })
                {
                    if (phanQuyenDvcs.ShowDialog(this) == DialogResult.OK)
                    {
                        txtRightDvcs.Text = phanQuyenDvcs.Vrights_dvcs;

                    }
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("PhanQuyenDVCS: " + ex.Message);
            }
                
        }

        private void btnPhanQuyenKho_Click(object sender, EventArgs e)
        {
            try
            {
                using (PhanQuyenKho phanQuyenKho = new PhanQuyenKho(Mode)
                {
                    Text = V6Setting.Language=="V"?"Phân quyền theo kho.":"Set warehouse rights",
                    Vrights_kho = txtRightKho.Text,
                    Vrights_dvcs = txtRightDvcs.Text
                })
                {
                    if (phanQuyenKho.ShowDialog(this) == DialogResult.OK)
                    {
                        txtRightKho.Text = phanQuyenKho.Vrights_kho;

                    }
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("PhanQuyenKho: " + ex.Message);
            }
        }

        private void cboInherit_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cboInherit_Leave(object sender, EventArgs e)
        {
            try
            {
                var where = " User_id=" + Convert.ToString(cboInherit.SelectedValue) + "";

                var inheritRow = V6BusinessHelper.Select("V6User", "*", where).Data.Rows[0];

                txtRights.Text = inheritRow["RIGHTS"].ToString().Trim();
                txtRightAdd.Text = inheritRow["R_ADD"].ToString().Trim();
                txtRightDelete.Text = inheritRow["R_DEL"].ToString().Trim();
                txtRightDvcs.Text = inheritRow["R_DVCS"].ToString().Trim();
                txtRightEdit.Text = inheritRow["R_EDIT"].ToString().Trim();
                txtRightKho.Text = inheritRow["R_KHO"].ToString().Trim();
                txtRightPrint.Text = inheritRow["R_PRINT"].ToString().Trim();
                txtRightView.Text = inheritRow["R_VIEW"].ToString().Trim();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("Inherit_Leave: " + ex.Message);
            }
        }

        private void btnPhanQuyenSnb_Click(object sender, EventArgs e)
        {
            try
            {
                using (PhanQuyenSoNoiBo phnQuyenF = new PhanQuyenSoNoiBo(Mode)
                {
                    Text =V6Setting.Language=="V"?"Phân quyền số nội bộ.":"Set book number code",
                    Vrights_sonb = txtR_sonb.Text,
                    Vrights_dvcs = txtRightDvcs.Text
                })
                {
                    if (phnQuyenF.ShowDialog(this) == DialogResult.OK)
                    {
                        txtR_sonb.Text = phnQuyenF.Vrights_sonb;

                    }
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("PhanQuyenSnb: " + ex.Message);
            }
        }

        private void bntPhanQuyenCtct_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@User_id", V6Login.UserId), 
                    new SqlParameter("@User_id_CT", (int) TxtUser_id.Value) 
                };
                V6BusinessHelper.ExecuteProcedure("VPA_ADD_ALCTCT", plist);
                
                //Hiển thị form chỉnh sửa chứng từ chi tiết cho user.
                //IDictionary<string, object> keys = new SortedDictionary<string, object>();
                //keys.Add("User_id_ct", TxtUser_id.Text);
                //var data = V6BusinessHelper.Select(V6TableName.Alctct, keys, "*").Data;

                var where0 = " WHERE d.User_id_ct='" + TxtUser_id.Text+"'";
                var sql = string.Format("Select e.ten_ct, d.* FROM AlctCt d "
                 + "\n LEFT JOIN alct e ON d.Ma_ct = e.Ma_ct "
                 + "\n {0}"
                 + "\n ORDER BY d.ma_ct",
                 where0);



                var data = SqlConnect.ExecuteDataset(CommandType.Text, sql).Tables[0];

                DataEditorForm editorForm = new DataEditorForm(this, data, "AlctCt", null, "Ma_ct,User_id_ct", V6Text.Edit + " " + V6TableHelper.V6TableCaption("AlctCt", V6Setting.Language), false, false, false, true, null);
                editorForm.DataGridView.EditingPrepare += DataGridView_EditingPrepare;
                editorForm.ShowDialog(this);
                //V6ControlFormHelper.ShowDataEditorForm(this, data, "AlctCt", null, "Ma_ct,User_id_ct", false, false, false, true, null);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "PhanQuyenCtct", ex);
            }
        }

        void DataGridView_EditingPrepare(object sender, V6ColorDataGridView.EditingEventArgs e)
        {
            All_Objects["e"] = e;
            InvokeFormEvent(e.CurrentColumn.DataPropertyName.ToUpper());
            //if (e.CurrentColumn.DataPropertyName.ToUpper() == "R_" && e.V6VvarTextBox != null)
            //{
            //    e.V6VvarTextBox.F2 = true;
            //    e.V6VvarTextBox.SetInitFilter("MA_CT='" + e.CurrentRow.Cells["MA_CT"].Value + "'");
            //}
        }

        private void v6chk_set_vattu_ketoan_banhang_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (v6chk_set_vattu_ketoan_banhang.Checked == false)
                {
                    return;
                }

                var user_inv = "0";
                var user_acc = "0";
                var user_sale = "0";

                if (chkUser_inv.Checked) user_inv = "1";
                if (chkUser_acc.Checked) user_acc = "1";
                if (chkUser_sale.Checked) user_sale = "1";
                   
                SqlParameter[] plist =
                {
                    new SqlParameter("@User_id", V6Login.UserId), 
                    new SqlParameter("@User_id_CT", (int) TxtUser_id.Value), 
                    new SqlParameter("@User_inv", user_inv) ,
                    new SqlParameter("@User_acc", user_acc) ,
                    new SqlParameter("@User_sale", user_sale) 
                    

                };
                V6BusinessHelper.ExecuteProcedure("VPA_UPDATE_ALCTCT", plist);
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "PhanQuyenCtct_Check", ex);
            }
        }

        private void DoEditXml()
        {
            try
            {
                var file_xml = txtUser_name.Text.Trim().ToUpper() + ".xml";
                new XmlEditorForm(txtXmlInfor, file_xml, "Table0", "KEY,VALUE,NOTE,ENCRYPT_YN".Split(',')).ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoEditXml", ex);
            }
        }

        private void btnEditXml_Click(object sender, EventArgs e)
        {
            DoEditXml();
        }

    }
}

