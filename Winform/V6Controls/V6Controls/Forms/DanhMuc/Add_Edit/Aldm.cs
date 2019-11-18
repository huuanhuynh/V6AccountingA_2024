using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Forms.Editor;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class Aldm : AddEditControlVirtual
    {
        private int maxlen_ma;

        public Aldm()
        {
            InitializeComponent();
            MyInit1();
        }

        private void MyInit1()
        {
            try
            {
                LoadColorNameList();
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("AlbcAddEdit Init" + ex.Message, Application.ProductName);
            }
        }

        private void LoadColorNameList()
        {
            //List<string> theList = Enum.GetValues(typeof(KnownColor)).Cast<string>().ToList();
            List<string> colorList = new List<string>();
            foreach (object value in Enum.GetValues(typeof(KnownColor)))
            {
                colorList.Add(value.ToString());
            }
            cboColorList.DataSource = colorList;
            cboColorList.SelectedIndex = -1;
        }

        private void From_Load(object sender, EventArgs e)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@ctable", TXTMA_DM.Text.Trim()),
                new SqlParameter("@cField", txtvalue.Text.Trim()),
            };
            maxlen_ma = ObjectAndString.ObjectToInt(V6BusinessHelper.ExecuteFunctionScalar("VFV_iFsize", plist));
            if (maxlen_ma == 0)
            {
                maxlen_ma = 16;
            }
            Make_Mau();
        }

        public override void DoBeforeAdd()
        {
            
        }

        public override void DoBeforeEdit()
        {
            
        }

        public override void V6F3Execute()
        {
            txtDmethod.Visible = true;
            lblXML.Visible = true;
            btnEditXml.Visible = true;
            base.V6F3Execute();
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TXTMA_DM.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaDM.Text;
            if (TXTTEN_DM.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenDM.Text;
            
            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_DM",
                 TXTMA_DM.Text.Trim(), DataOld["MA_DM"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaDM.Text + "=" + TXTMA_DM.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_DM",
                 TXTMA_DM.Text.Trim(), TXTMA_DM.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaDM.Text + "=" + TXTMA_DM.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void Make_Mau()
        {
            var result = "";
            var result_mau = "";
            var _so_ct = Convert.ToString((int) txtSTT13.Value);

            if (txtEXPR1.Text.Trim() != "")
            {
                result = txtEXPR1.Text.Trim();
                result_mau = txtEXPR1.Text.Trim();
            }
            if (txtFORM.Text.Trim() != "")
            {
                result += "{0:" + txtFORM.Text.Trim() + "}";
                result_mau += (txtFORM.Text.Trim() + _so_ct).Right(txtFORM.Text.Trim().Length);
            }
            else
            {
                result += "{0}";
                if (txtSTT13.Value > 0)
                {
                    result_mau += _so_ct;
                }
                else
                {
                    result_mau += "1";
                }
            }

            TxtTransform.Text = result;
            txtMau.Text = result_mau;

            int mau_length = txtMau.Text.Trim().Length;
            if (mau_length > maxlen_ma)
            {
                txtEXPR1.Text = "";
                txtFORM.Text = "000";
                this.ShowWarningMessage(string.Format("{0} txtMau({1}) > max({2})", V6Text.Toolong, mau_length, maxlen_ma));
                txtMau.Focus();
            }
        }

        private void TxtSTT13_FORM_EXPR1_TextChanged(object sender, EventArgs e)
        {
            if(IsReady) Make_Mau();
        }

        private void DoEditXml()
        {
            try
            {
                var file_xml = TXTMA_DM.Text.Trim().ToUpper() + ".xml";
                new XmlEditorForm(txtDmethod, file_xml, "Table0", "event,using,method,content".Split(',')).ShowDialog(this);
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

        private void cboColorList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboColorList.SelectedIndex == -1)
            {
                lblTenMau.BackColor = Color.Transparent;
            }
            else
            {
                lblTenMau.BackColor = Color.FromName(cboColorList.Text);
            }
        }

    }
}
