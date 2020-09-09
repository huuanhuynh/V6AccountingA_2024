using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

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

            txtDVT.Enter += (s, ee) =>
            {
                txtDVT.SetInitFilter("ma_vt='" + txtMaVt.Text.Trim() + "'");
            };
            MyInit2();
        }

        private void MyInit2()
        {
            try
            {
                txtMaGia.ExistRowInTable();
                txtMaKH.ExistRowInTable();
                txtMaVt.ExistRowInTable();
                txtDVT.ExistRowInTable();
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

            if (errors.Length > 0) throw new Exception(errors);

            var KEY1 = "MA_VT";
            var KEY2 = "MA_NT";
            var KEY3 = "MA_GIA";
            var KEY4 = "NGAY_BAN";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidThreeCode_OneDate("ALGIA2", 0,
                    KEY1, DataDic[KEY1].ToString(), ObjectAndString.ObjectToString(DataOld[KEY1]),
                    KEY2, DataDic[KEY2].ToString(), ObjectAndString.ObjectToString(DataOld[KEY2]),
                    KEY3, DataDic[KEY3].ToString(), ObjectAndString.ObjectToString(DataOld[KEY3]),
                    KEY4, ObjectAndString.ObjectToString(DataDic[KEY4], "yyyyMMdd"), ObjectAndString.ObjectToString(DataOld[KEY4], "yyyyMMdd"));
                if (!b)
                    throw new Exception(string.Format(V6Text.EditDenied + " {0},{1},{2},{3} = {4},{5},{6},{7}",
                        KEY1, KEY2, KEY3, KEY4, DataDic[KEY1], DataDic[KEY2], DataDic[KEY3], DataDic[KEY4]));
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidThreeCode_OneDate("ALGIA2", 1,
                    KEY1, DataDic[KEY1].ToString(), ObjectAndString.ObjectToString(DataDic[KEY1]),
                    KEY2, DataDic[KEY2].ToString(), ObjectAndString.ObjectToString(DataDic[KEY2]),
                    KEY3, DataDic[KEY3].ToString(), ObjectAndString.ObjectToString(DataDic[KEY3]),
                    KEY4, ObjectAndString.ObjectToString(DataDic[KEY4], "yyyyMMdd"), ObjectAndString.ObjectToString(DataDic[KEY4], "yyyyMMdd"));
                if (!b)
                    throw new Exception(string.Format(V6Text.AddDenied + " {0},{1},{2},{3} = {4},{5},{6},{7}",
                        KEY1, KEY2, KEY3, KEY4, DataDic[KEY1], DataDic[KEY2], DataDic[KEY3], DataDic[KEY4]));
            }

            
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
