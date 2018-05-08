using System;
using System.Windows.Forms;
using V6Controls.Forms;
using V6Tools;

namespace V6ControlManager.FormManager.ToolManager
{
    public partial class FormChuyenMa : V6Form
    {
        public FormChuyenMa()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void radAuto_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GetSource(sender);
            }
            catch (Exception)
            {
                
            }
        }

        private void GetSource(object sender)
        {
            if (sender == radAuto)
            {
                grbNguon.Text = "Mã nguồn. " + ChuyenMaTiengViet.NhanDangMaTiengViet(richTextBox1.Text);
            }
            else
            {
                grbNguon.Text = "Mã nguồn";
            }
        }

        private void btnChuyen_Click(object sender, EventArgs e)
        {
            try
            {
                string from = GetSelectedRad(grbNguon);
                string to = GetSelectedRad(grbDich);
                richTextBox2.Text = ChuyenMaTiengViet.VIETNAM_CONVERT(richTextBox1.Text, from, to);
            }
            catch (Exception)
            {
                
            }
        }

        private string GetSelectedRad(GroupBox groupBox)
        {
            foreach (Control control in groupBox.Controls)
            {
                if (control is RadioButton)
                {
                    if (((RadioButton) control).Checked) return control.Text;
                }
            }

            return "";
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            if(radAuto.Checked) GetSource(radAuto);
        }
    }
}
