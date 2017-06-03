﻿using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class CorpLanAddEditForm : AddEditControlVirtual
    {
        public CorpLanAddEditForm()
        {
            InitializeComponent();
            MyInit1();
        }

        private void MyInit1()
        {
            try
            {
                if (V6Setting.Language != "V")
                {
                    lblTextE.Text = "Text " + V6Setting.Language;
                    txtTextE.AccessibleName = V6Setting.Language;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(Name + " Init1 " + ex.Message, Application.ProductName);
            }
        }

        private void Algia2AddEditForm_Load(object sender, EventArgs e)
        {
            if (TableName != V6TableName.CorpLan)
            {
                txtCtype.Visible = false;
                lblCtype.Visible = false;
                
                txtTen.Visible = false;
                txtTen2.Visible = false;

                lblTen.Visible = false;
                lblTen2.Visible = false;

                txtSname.Visible = false;
                label9.Visible = false;
            }
        }

        public override void DoBeforeEdit()
        {
            try
            {
                num = ObjectAndString.ObjectToInt(txtID.Text.Right(5));
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("DisableControlWhenEdit " + ex.Message, ex.Source);
            }
        }

        //private bool must_have_name;
        public override void ValidateData()
        {
            var errors = "";
            if (txtID.Text.Trim() == "")
                errors += "Chưa nhập mã !\r\n";
            if (txtTextD.Text.Trim() == "")
                errors += "Chưa nhập mặc định !\r\n";
            //if (txtTextV.Text.Trim() == "")
            //    errors += "Chưa nhập TextV !\r\n";
            //if (txtTextE.Text.Trim() == "")
            //    errors += "Chưa nhập TextE !\r\n";

            //if (Mode == V6Mode.Add && txtTen.Text.Trim() == "")
            //    errors += "Chưa nhập Tên !\r\n";
            //if (Mode == V6Mode.Add && txtTen2.Text.Trim() == "")
            //    errors += "Chưa nhập Tên 2 !\r\n";

            if (Mode == V6Mode.Edit)
            {
               
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "ID",
                 txtID.Text.Trim(), txtID.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "ID = " + txtID.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private int num;
        private void FixID()
        {
            try
            {
                if (!IsReady) return;

                var CType = txtCtype.Text.Trim();
                var SFile = txtSfile.Text.Trim();
                
                if (Mode == V6Mode.Add)
                {
                    var sql = "Select Max(Right(Rtrim(ID),5)) as max_num From Corplan Where SFile='" + SFile
                              + "' and ctype='" + CType + "'";
                    var max_num = ObjectAndString.ObjectToInt(SqlConnect.ExecuteScalar(CommandType.Text, sql));
                    num = max_num + 1;
                }
                var id = SFile + CType + "" + ("00000" + num).Right(5);
                txtID.Text = id;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".txtStype_TextChanged", ex);
            }
        }

        private void txtCtype_TextChanged(object sender, EventArgs e)
        {
            FixID();
        }

        private void txtSfile_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F3)
                {
                    txtSfile.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".txtSfile_KeyDown", ex);
            }
        }

        private void txtSfile_TextChanged(object sender, EventArgs e)
        {
            FixID();
        }
    }
}
