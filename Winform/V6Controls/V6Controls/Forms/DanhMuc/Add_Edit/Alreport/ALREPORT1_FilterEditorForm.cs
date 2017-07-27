using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6Structs;
namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class ALREPORT1_FilterEditorForm : V6Form
    {
        public ALREPORT1_FilterEditorForm()
        {
            InitializeComponent();
        }

        private string _define;
        private DefineInfo Info;
        public string FILTER_DEFINE { get; private set; }
        public ALREPORT1_FilterEditorForm(string define)
        {
            _define = define;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                Info = new DefineInfo(_define);
                txtControlType.Text = Info.ControlType;
                txtAccessibleName.Text = Info.AccessibleName;
                txtField.Text = Info.Field;
                txtTextV.Text = Info.TextV;
                txtTextE.Text = Info.TextE;
                txtSqlType.Text = Info.sql_type;
                txtOper.Text = Info.Oper;
                txtDefaultValue.Text = Info.DefaultValue;
                txtNotEmpty.Text = Info.NotEmpty ? "1" : "0";
                txtEnabled.Text = Info.Enabled ? "1" : "0";
                txtVisible.Text = Info.Visible ? "1" : "0";
                txtF2.Text = Info.F2 ? "1" : "0";
                txtLimitChars.Text = Info.LimitChars;
                txtVvar.Text = Info.Vvar;
                txtInitFilter.Text = Info.InitFilter;
                txtFparent.Text = Info.Fparent;
                txtWidth.Text = Info.Width;
                txtLoaiKey.Text = Info.Loai_key;
                txtBField.Text = Info.BField;
                txtDecimals.Text = Info.Decimals.ToString();
                txtmaxlength.Text = Info.MaxLength.ToString();
                txtDescriptionV.Text = Info.DescriptionV;
                txtDescriptionE.Text = Info.DescriptionE;
                txtFilterStart.Text = Info.FilterStart ? "1" : "";
                txtToUpper.Text = Info.ToUpper ? "1" : "";
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        private void From_Load(object sender, System.EventArgs e)
        {
           // txtval.Focus();
        }

        private void Nhan()
        {
            try
            {
                var data = GetData();
                var result = "";
                foreach (KeyValuePair<string, object> item in data)
                {
                    var value = item.Value.ToString().Trim();
                    if (value != "")
                    {
                        result += string.Format(";{0}:{1}", item.Key, value);
                    }
                }
                if (result.Length > 0) result = result.Substring(1);
                FILTER_DEFINE = result;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Nhan", ex);
                DialogResult = DialogResult.Abort;
            }
            DialogResult = DialogResult.OK;
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            Nhan();
        }

        
    }
}
