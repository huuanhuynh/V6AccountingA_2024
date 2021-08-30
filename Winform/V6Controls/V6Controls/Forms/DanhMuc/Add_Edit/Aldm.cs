using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Forms.DanhMuc.Add_Edit.Albc;
using V6Controls.Forms.Editor;
using V6Init;
using V6SqlConnect;
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

        public override void V6CtrlF12Execute()
        {
            txtDmethod.Visible = true;
            lblXML.Visible = true;
            btnEditXml.Visible = true;
            lblExtraInfo.Visible = true;
            txtExtraInfo.Visible = true;
            lblValidChars.Visible = true;
            txtValidChars.Visible = true;
            base.V6CtrlF12Execute();
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
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_DM",
                 TXTMA_DM.Text.Trim(), DataOld["MA_DM"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaDM.Text + "=" + TXTMA_DM.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_DM",
                 TXTMA_DM.Text.Trim(), TXTMA_DM.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaDM.Text + "=" + TXTMA_DM.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void Make_Mau()
        {
            if (!chkINCREASE_YN.Checked) return;

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

        private void btnGRDS_V1_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, AlbcFieldInfo> targetInfoList = GetTargetFieldsInfo(txtGRDS_V1.Text, txtGRDF_V1.Text, txtGRDHV_V1.Text, txtGRDHE_V1.Text, string.Empty);
                Dictionary<string, AlbcFieldInfo> sourceFields = GetSourceFieldsInfo1();
                V6ControlFormHelper.SelectFields(this, sourceFields, targetInfoList, txtGRDS_V1, txtGRDF_V1, txtGRDHV_V1, txtGRDHE_V1);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnGRDS_V1_Click", ex);
            }
        }

        private Dictionary<string, AlbcFieldInfo> GetTargetFieldsInfo(string ssss, string ffff, string vvvv, string eeee, string tttt)
        {
            var targetInfoList = new Dictionary<string, AlbcFieldInfo>();
            var sss = ObjectAndString.SplitString(ssss);
            var fff = ObjectAndString.SplitString(ffff);    //  N0:100;C200;D250...
            var vvv = ObjectAndString.SplitString(vvvv);
            var eee = ObjectAndString.SplitString(eeee);
            var ttt = ObjectAndString.SplitString(tttt);
            for (int i = 0; i < sss.Length; i++)
            {
                string field = sss[i];
                string FIELD = field.Trim().ToUpper();
                string f = fff.Length <= i ? "C100" : fff[i];
                string fts = f.Substring(0, 1);
                string fws = f.Substring(1);
                if (fts == "N")
                {
                    if (f.Length > 1) fts = f.Substring(0, 2);
                    if (f.Length > 2) fws = f.Substring(3);
                    else fws = "100";
                }
                var ft = EnumConvert.FromString<AlbcFieldType>(fts);
                int fw = ObjectAndString.ObjectToInt(fws);
                string fhv = vvv.Length <= i ? CorpLan2.GetFieldHeader(FIELD, "V") : vvv[i];
                string fhe = eee.Length <= i ? CorpLan2.GetFieldHeader(FIELD, "E") : eee[i];
                bool fns = ttt.Length > i && ttt.Contains(FIELD);

                AlbcFieldInfo fi = new AlbcFieldInfo()
                {
                    FieldName = FIELD,
                    FieldType = ft,
                    FieldWidth = fw,
                    FieldHeaderV = fhv,
                    FieldHeaderE = fhe,
                    FieldNoSum = fns,
                };
                targetInfoList[FIELD] = fi;
            }
            return targetInfoList;
        }

        private Dictionary<string, AlbcFieldInfo> GetSourceFieldsInfo1()
        {
            try
            {
                if (V6BusinessHelper.IsExistDatabaseTable(txtTable_name.Text))
                {
                    DataTable data1 = null;
                    if (txtTable_View.Text.Trim() != "") data1 = V6BusinessHelper.Select(txtTable_View.Text, "*", "1=0").Data;
                    else data1 = V6BusinessHelper.Select(txtTable_name.Text, "*", "1=0").Data;
                    return V6ControlFormHelper.GetSourceFieldsInfo(data1);
                }
                else
                {
                    var info = V6BusinessHelper.ExecuteProcedure("V6TOOLS_GET_PROC_INFOR",
                        new SqlParameter("@proc_name", TXTMA_DM.Text)).Tables[0];
                    if (info == null || info.Rows.Count == 0) return new Dictionary<string, AlbcFieldInfo>();

                    List<SqlParameter> plist = new List<SqlParameter>();
                    foreach (DataRow row in info.Rows)
                    {
                        string para_name = row["para_name"].ToString().Trim();
                        string para_type = row["para_type"].ToString().Trim().ToLower();
                        Type para_Type = F.TypeFromData_Type(para_type);
                        if (ObjectAndString.IsDateTimeType(para_Type))
                        {
                            plist.Add(new SqlParameter(para_name, V6Setting.M_SV_DATE));
                        }
                        else if (ObjectAndString.IsNumberType(para_Type))
                        {
                            int int0 = 0;
                            plist.Add(new SqlParameter(para_name, int0));
                        }
                        else
                        {
                            plist.Add(new SqlParameter(para_name, " 1=0 "));
                        }
                    }
                    var data2 = V6BusinessHelper.ExecuteProcedure(TXTMA_DM.Text, plist.ToArray()).Tables[0];
                    return V6ControlFormHelper.GetSourceFieldsInfo(data2);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".GetSourceFieldsInfo1", ex);
            }
            return new Dictionary<string, AlbcFieldInfo>();
        }

        private void btnFsearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable temp = null;
                if (txtTable_View.Text.Trim() != "") temp = V6BusinessHelper.Select(txtTable_View.Text, "*", "1=0").Data;
                else temp = V6BusinessHelper.Select(txtTable_name.Text, "*", "1=0").Data;
                V6ControlFormHelper.SelectFields(this, temp, txtFsearch.Text, txtFsearch);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        

    }
}
