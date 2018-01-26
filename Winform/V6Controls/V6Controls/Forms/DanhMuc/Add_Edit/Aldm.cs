using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Forms.DanhMuc.Add_Edit.Albc;
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
            if (f3count == 3)
            {
                f3count = 0;
                if (new ConfirmPasswordV6().ShowDialog(this) == DialogResult.OK)
                {
                    ShowTopMessage("V6 Confirm ......OK....");
                    
                    txtDmethod.Visible = true;
                    lblXML.Visible = true;
                    btnEditXml.Visible = true;
                }
            }
            else
            {
                
            }
        }
        
        public override void ValidateData()
        {
            var errors = "";
            if (TXTMA_DM.Text.Trim() == "")
                errors += V6Text.CheckInfor+"\r\n";
            if (TXTTEN_DM.Text.Trim() == "")
                errors += V6Text.CheckInfor + "\r\n";

            

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_DM",
                 TXTMA_DM.Text.Trim(), DataOld["MA_DM"].ToString());
                if (!b)
                    throw new Exception(V6Text.EditDenied + " MA_DM = " + TXTMA_DM.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_DM",
                 TXTMA_DM.Text.Trim(), TXTMA_DM.Text.Trim());
                if (!b)
                    throw new Exception(V6Text.AddDenied + " MA_DM = " + TXTMA_DM.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void Make_Mau()
        {
            var result = "";
            var result_mau = "";
            var _so_ct = Convert.ToString((int) TxtSTT13.Value);

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
                if (TxtSTT13.Value > 0)
                {
                    result_mau += _so_ct;
                }
                else
                {
                    result_mau += "1";
                }
            }

            TxtTransform.Text = result;
            TxtMau.Text = result_mau;
          
            if (TxtMau.Text.Trim().Length > maxlen_ma)
            {
                txtEXPR1.Text = "";
                txtFORM.Text = "000";
                this.ShowWarningMessage(V6Text.Toolong);
            }

        }

        private void TxtSTT13_TextChanged(object sender, EventArgs e)
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

    }
}
