using System;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class Algia2AddEditForm : AddEditControlVirtual
    {
        public Algia2AddEditForm()
        {
            InitializeComponent();
        }

        private void Algia2AddEditForm_Load(object sender, EventArgs e)
        {
            txtDVT.Upper();
            txtDVT.SetInitFilter("");

            txtDVT.GotFocus += (s, ee) =>
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
            if (txtma_gia.Text.Trim() == "")
                errors += "Chưa nhập mã !\r\n";
            if (txtMaVt.Text.Trim() == "")
                errors += "Chưa nhập vt !\r\n";
            if (txtMaNT.Text.Trim() == "")
                errors += "Chưa nhập mã ngoại tệ !\r\n";

            if (Mode == V6Mode.Edit)
            {
                //bool b = V6Categories.IsValidOneCode_Full(TableName.ToString(), 0, "MA_BP",
                // txtma_gia.Text.Trim(), DataOld["MA_BP"].ToString());
                //if (!b)
                //    throw new Exception("Không được thêm mã đã tồn tại: "
                //                                    + "MA_BP = " + txtma_gia.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                //bool b = V6Categories.IsValidOneCode_Full(TableName.ToString(), 1, "MA_BP",
                // txtma_gia.Text.Trim(), txtma_gia.Text.Trim());
                //if (!b)
                //    throw new Exception("Không được thêm mã đã tồn tại: "
                //                                    + "MA_BP = " + txtma_gia.Text.Trim());
            }

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
