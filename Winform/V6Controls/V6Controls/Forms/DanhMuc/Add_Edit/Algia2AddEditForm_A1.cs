using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class Algia2AddEditForm_A1 : AddEditControlVirtual
    {
        public Algia2AddEditForm_A1()
        {
            InitializeComponent();
        }

        private void Algia2AddEditForm_A1_Load(object sender, EventArgs e)
        {
            txtDVT.Upper();
            txtDVT.SetInitFilter("");

            txtDVT.Enter += (s, ee) =>
            {
                txtDVT.SetInitFilter("ma_vt='" + txtMaVt.Text.Trim() + "'");
            };
        }

        public override void DoBeforeEdit()
        {
            
            try
            {
                var data = txtMaVt.Data;

                txtDVT.Text = data["dvt"].ToString().Trim();
                txtDVT.SetInitFilter("ma_vt='" + txtMaVt.Text + "'");
                txtDVT.Text = txtDVT.Text;

                if (data.Table.Columns.Contains("Nhieu_DVT"))
                {
                    var nhieuDvt = data["Nhieu_DVT"].ToString().Trim();
                    if (nhieuDvt == "1")
                    {
                        txtDVT.Tag = null;
                        txtDVT.ReadOnly = false;
                    }
                    else
                    {
                        txtDVT.Tag = "readonly";
                        txtDVT.ReadOnly = true;
                        txtDVT.Focus();
                    }
                }
                else
                {
                    txtDVT.ExistRowInTable(txtDVT.Text);
                    txtDVT.Tag = "readonly";
                    txtDVT.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeEdit", ex);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaGia.Text.Trim() == "") errors += V6Text.Text("CHUANHAP") + " " + lblMaGia.Text;
            if (txtMaVt.Text.Trim() == "") errors += V6Text.Text("CHUANHAP") + " " + lblMaVT.Text;
            if (txtMaNT.Text.Trim() == "") errors += V6Text.Text("CHUANHAP") + " " + lblMaNT.Text;

            string[] key_list = {"MA_VT","MA_NT", "MA_GIA", "NGAY_BAN", "MA_KHO"};
            errors += CheckValid(TableName.ToString(), key_list);

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void txtMaVt_V6LostFocus(object sender)
        {
            try
            {
                var data = txtMaVt.Data;

                txtDVT.Text = data["dvt"].ToString().Trim();
                txtDVT.SetInitFilter("ma_vt='" + txtMaVt.Text + "'");
                txtDVT.Text = txtDVT.Text;

                if (data.Table.Columns.Contains("Nhieu_DVT"))
                {
                    var nhieuDvt = data["Nhieu_DVT"].ToString().Trim();
                    if (nhieuDvt == "1")
                    {
                        txtDVT.Tag = null;
                        txtDVT.ReadOnly = false;
                    }
                    else
                    {
                        txtDVT.Tag = "readonly";
                        txtDVT.ReadOnly = true;
                        txtDVT.Focus();
                    }
                }
                else
                {
                    txtDVT.ExistRowInTable(txtDVT.Text);
                    txtDVT.Tag = "readonly";
                    txtDVT.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("txtMaVt_V6LostFocus " + ex.Message, ex.Source);
            }
        }
    }
}
