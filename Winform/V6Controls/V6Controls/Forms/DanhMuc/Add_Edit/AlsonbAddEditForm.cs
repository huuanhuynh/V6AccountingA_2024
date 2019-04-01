using System;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;

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
                    txtMaDVCS.Text = V6Login.Madvcs;
                }
            }
        }
        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ARI70,ARA00", "MA_SONB", txtMa_sonb.Text);
            txtMa_sonb.Enabled = !v;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMa_sonb.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMa_sonb.Text;
            if (txtTen_sonb.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTen_sonb.Text;
            if (V6Login.MadvcsTotal > 0 && txtMaDVCS.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaDVCS.Text;
            if (TxtPhandau.Text.Trim() == "" && TxtPhancuoi.Text.Trim() == "" && TxtDinhdang.Text.Trim() == "")
                errors += string.Format("{0} {1}, {2}, {3}", V6Text.Text("CHUANHAP"), lblDauNgu.Text, lblViNgu.Text, lblDinhDangSo.Text);

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_SONB", txtMa_sonb.Text.Trim(), DataOld["MA_SONB"].ToString());
                if (!b) throw new Exception(string.Format("{0} {1} = {2}", V6Text.ExistData, lblMa_sonb.Text, txtMa_sonb.Text));
                
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_SONB", txtMa_sonb.Text.Trim(), txtMa_sonb.Text.Trim());
                if (!b) throw new Exception(string.Format("{0} {1} = {2}", V6Text.ExistData, lblMa_sonb.Text, txtMa_sonb.Text));
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            
        }

        private void btnMa_ctnb_Click(object sender, EventArgs e)
        {
            using (PhanQuyen.DanhSachChungTu chungtu = new PhanQuyen.DanhSachChungTu
            {
                Text = V6Text.Text("DSCT"),
                VLists_ct = TxtMa_ctnb.Text
            })
            {
                if (chungtu.ShowDialog(this) == DialogResult.OK)
                {
                    TxtMa_ctnb.Text = chungtu.VLists_ct;
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
