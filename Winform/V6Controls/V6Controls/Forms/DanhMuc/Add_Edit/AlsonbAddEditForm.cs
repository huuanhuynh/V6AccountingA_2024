using System;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class AlsonbAddEditForm : AddEditControlVirtual
    {
        public AlsonbAddEditForm()
        {
            InitializeComponent();
            if (Mode == V6Mode.Add)
            {
                if (V6Login.MadvcsCount == 1)
                {
                    TxtMa_dvcs.Text = V6Login.Madvcs;
                }
            }
        }
        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ARI70,ARA00", "MA_SONB", TxtMa_sonb.Text);
            TxtMa_sonb.Enabled = !v;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_sonb.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (TxtTen_sonb.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";
            if (V6Login.MadvcsTotal > 0 && TxtMa_dvcs.Text.Trim() == "")
                errors += "Chưa nhập đơn vị cơ sở !\r\n";
            if (TxtPhandau.Text.Trim() == "" && TxtPhancuoi.Text.Trim() == "" && TxtDinhdang.Text.Trim() == "")
                errors += "Chưa có định dạng, đầu ngữ, vị ngữ !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_SONB",
                 TxtMa_sonb.Text.Trim(), DataOld["MA_SONB"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_SONB = " + TxtMa_sonb.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_SONB",
                 TxtMa_sonb.Text.Trim(), TxtMa_sonb.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_SONB = " + TxtMa_sonb.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            
        }

        private void btnMa_ctnb_Click(object sender, EventArgs e)
        {
            {
                using (PhanQuyen.DanhSachChungTu chungtu    = new PhanQuyen.DanhSachChungTu
                {
                    Text = "Danh sách chứng từ.",
                    VLists_ct = TxtMa_ctnb.Text

                })
                {
                    if (chungtu.ShowDialog(this) == DialogResult.OK)
                    {
                        TxtMa_ctnb.Text = chungtu.VLists_ct;

                    }
                }
            }

        }

        private void TxtPhandau_TextChanged(object sender, EventArgs e)
        {
            Make_Mau();
        }

        private void TxtPhancuoi_TextChanged(object sender, EventArgs e)
        {
            Make_Mau();
        }

        private void TxtDinhdang_TextChanged(object sender, EventArgs e)
        {
            Make_Mau();
        }
        private void TxtSo_ct_TextChanged(object sender, EventArgs e)
        {
            Make_Mau();
        }
        private void Make_Mau()
        {
            var result = "";
            var result_mau = "";
            var _so_ct = Convert.ToString((int)TxtSo_ct.Value);

            if (TxtPhandau.Text.Trim() != "")
            {
                result = TxtPhandau.Text.Trim();
                result_mau = TxtPhandau.Text.Trim();
            }
            if (TxtDinhdang.Text.Trim() != "")
            {
                result += "{0:" + TxtDinhdang.Text.Trim() + "}";
                result_mau += (TxtDinhdang.Text.Trim() + _so_ct).Right(TxtDinhdang.Text.Trim().Length);
            }
            else
            {
                result += "{0}";
                if (TxtSo_ct.Value > 0)
                {
                    result_mau += _so_ct;
                }
                else
                {
                    result_mau += "1";
                }
            }


            if (TxtPhancuoi.Text.Trim() != "")
            {
                result += TxtPhancuoi.Text.Trim();
                result_mau += TxtPhancuoi.Text.Trim();
            }

            
            TxtTransform.Text = result;
            TxtMau.Text = result_mau;
            if (TxtMau.Text.Trim().Length > V6Setting.M_Size_ct)
            {
                TxtPhancuoi.Text = "";
                TxtPhandau.Text = "";
                TxtDinhdang.Text = "";
                this.ShowWarningMessage("Quá giới hạn số chứng từ !");

            }


        }
    }
}
