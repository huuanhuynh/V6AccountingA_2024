using System;
using System.Data.SqlClient;
using System.Reflection;
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

        private void AlsonbAddEditForm_Load(object sender, EventArgs e)
        {
            Make_Mau();
        }

        public override void AfterInsert()
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@cMa_sonb_old", DataOld["MA_SONB"]),
                    new SqlParameter("@cMa_sonb_new", DataDic["MA_SONB"]),
                    new SqlParameter("@user_id", V6Login.UserId),
                };
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UPDATE_RIGHTS_ALSONB", plist);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
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
            if (lblWarning.Visible)
                errors += lblWarning.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_SONB", txtMa_sonb.Text.Trim(), DataOld["MA_SONB"].ToString());
                if (!b) throw new Exception(string.Format("{0} {1} = {2}", V6Text.DataExist, lblMa_sonb.Text, txtMa_sonb.Text));
                
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_SONB", txtMa_sonb.Text.Trim(), txtMa_sonb.Text.Trim());
                if (!b) throw new Exception(string.Format("{0} {1} = {2}", V6Text.DataExist, lblMa_sonb.Text, txtMa_sonb.Text));
            }

            if (errors.Length > 0) throw new Exception(errors);
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
            if (!IsReady) return;

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
            int openindex = result_mau.IndexOf("<", StringComparison.Ordinal);
            int closeindex = result_mau.IndexOf(">", StringComparison.Ordinal);
            int formatlength = closeindex - openindex - 1;
            if (formatlength > 0)
            {
                if (txtTypeAuto.Value == 1 || txtTypeAuto.Value == 2)
                {
                    //int openindex = result_mau.IndexOf("<", StringComparison.Ordinal);
                    //int closeindex = result_mau.IndexOf(">", StringComparison.Ordinal);
                    //int formatlength = closeindex - openindex - 1;
                    //if (formatlength > 0)
                    {
                        string dateformat = result_mau.Substring(openindex + 1, formatlength);
                        string mask = "<" + dateformat + ">";
                        dateformat = dateformat.Replace("D", "d");
                        dateformat = dateformat.Replace("m", "M");
                        dateformat = dateformat.Replace("Y", "y");

                        result_mau = result_mau.Replace(mask, DateTime.Now.ToString(dateformat));
                    }
                }
                else
                {
                    // giữ nguyên <>
                }
            }
            else
            {
                if (txtTypeAuto.Value == 1 || txtTypeAuto.Value == 2)
                {
                    // xóa <>
                    result_mau = result_mau.Replace("<", "");
                    result_mau = result_mau.Replace(">", "");
                }
                else
                {
                    // giữ nguyên <>
                }
            }

            
            
            TxtMau.Text = result_mau;
            if (TxtMau.Text.Trim().Length > V6Setting.M_Size_ct)
            {
                //TxtPhancuoi.Text = "";
                //TxtPhandau.Text = "";
                //TxtDinhdang.Text = "";
                lblWarning.Text = V6Setting.IsVietnamese ? "Quá giới hạn độ dài !" : "Over length limit !";
                lblWarning.Visible = true;
                //this.ShowWarningMessage("Quá giới hạn số chứng từ !");
            }
            else
            {
                lblWarning.Visible = false;
            }
        }
        
    }
}
