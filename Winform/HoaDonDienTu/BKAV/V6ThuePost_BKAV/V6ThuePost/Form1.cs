using System;
using System.Windows.Forms;
using V6ThuePostBkavApi;

namespace V6ThuePost
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string result = Program.POST("", BkavConst._100_CreateNew);
            lblResult.Text = result;
            if (result != null && result.Contains("Dữ liệu tạo hóa đơn không hợp lệ"))
            {
                V6Message.Show("Kết nối ổn!", 500, this);
            }
            else
            {
                V6Message.Show(result, 500, this);
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            string result = Program.POST(richTextBox1.Text, BkavConst._100_CreateNew);
            lblResult.Text = result;
            V6Message.Show(result, 500, this);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            string jsonBody = Program.ReadData(txtDbfFile.Text, "M");
            txtUsername.Text = Program.BkavPartnerGUID;
            txtPassword.Text = Program.BkavPartnerToken;
            richTextBox1.Text = jsonBody;
            btnTest.Enabled = true;
            btnSend.Enabled = true;
        }

    }
}
