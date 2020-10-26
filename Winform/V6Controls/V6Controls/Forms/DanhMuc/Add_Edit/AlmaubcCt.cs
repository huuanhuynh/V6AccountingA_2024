using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Init;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class AlmaubcCt : AddEditControlVirtual
    {
        public AlmaubcCt()
        {
            InitializeComponent();
        }

        public override void DoBeforeEdit()
        {
            //txtMaMauBc.Enabled = false;
            //txtkind.Value = txtcach_tinh.Text.Trim() == "" ? 1 : 0;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaSo.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaSo.Text;

            if (txtcach_tinh.Text.Trim() != "" &&
                !V6BusinessHelper.CheckValidFormula(_MA_DM,
                "MAU_BC", "CACH_TINH", "MA_SO", txtMauBc.Text, txtcach_tinh.Text))
            {
                errors += V6Text.Wrong + "\r\n";
            }


            IList<string> keyFields = new []{"MAU_BC", "MA_SO"};
            errors += CheckValid(_MA_DM, keyFields);
            
            if (errors.Length > 0) throw new Exception(errors);
        }
        
        private void txtkind_TextChanged(object sender, EventArgs e)
        {
            if (txtkind.Value == 1 || txtkind.Value == 2)
            {
                txtcach_tinh.Text = "";
                txtcach_tinh.Enabled = false;
                txtTK.Enabled = true;
            }
            else
            {
                txtTK.Text = "";
                txtTK.Enabled = false;
                txtcach_tinh.Enabled = true;
            }
        }

        private void AlmaubcCt_Load(object sender, EventArgs e)
        {
            var switch_text = "";
            if (txtMauBc.Text.Length >= 5) switch_text = txtMauBc.Text.Substring(0, 5);
            switch (switch_text)
            {
                case "GLTCB":
                    groupBoxB.Visible = true;
                    groupBoxB.Left = groupBox1.Left;
                    break;
                case "GLTCA":
                case "GLTCC":
                case "GLTCD":
                    groupBoxC.Visible = true;
                    groupBoxC.Left = groupBox1.Left;
                    break;
                case "GLTCX":
                    groupBoxB.Visible = true;
                    groupBoxC.Visible = true;
                    break;
                default:
                    groupBoxB.Visible = true;
                    groupBoxC.Visible = true;
                    break;
            }
        }
    }
}
