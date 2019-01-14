using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using V6Tools;

namespace Tools
{
    public partial class FormAllMain : Form
    {
        public FormAllMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new ToolExportSqlToExcel().Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            new FormHuuanEditText().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FormConvertTable().Show();
        }

        private void btnDBF_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Dùng convert table!");
            //new FormDBF().Show();
        }

        private void btnModelHelp_Click(object sender, EventArgs e)
        {
            new FormModelHelp().Show();
        }

        private void btnUploadFTP_Click(object sender, EventArgs e)
        {
            var open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                UploadDownloadFTP ftp = new UploadDownloadFTP("118.69.183.160", "huuan", UtilityHelper.EnCrypt("_D21C2V62015"));
                ftp.Upload(open.FileName);
                MessageBox.Show("Test Xong");
            }
        }

        private void btnCopyToV6_Click(object sender, EventArgs e)
        {
            var open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                //118.69.183.160
                V6FileIO.CopyToVPN(open.FileName, "", "118.69.183.16", "huuan", UtilityHelper.EnCrypt("_D21C2V62015"));
            }
        }

        private void btnParseDecimal_Click(object sender, EventArgs e)
        {
            try
            {
                numericUpDown1.Value = Decimal.Parse(textBox1.Text, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFilterTextFiles_Click(object sender, EventArgs e)
        {
            try
            {
                new FormFilterTextFiles().Show(this);
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".Filter text files", ex, "");
                MessageBox.Show(ex.Message);
            }
        }
    }
}
