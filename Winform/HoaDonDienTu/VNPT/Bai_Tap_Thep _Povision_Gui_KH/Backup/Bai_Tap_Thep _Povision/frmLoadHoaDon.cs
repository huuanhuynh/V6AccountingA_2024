using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using ICSharpCode;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
//using System.Threading.Tasks;
using System.Xml;
using System.Threading;
using System.Data.Common;
using System.Web;
using System.Text.RegularExpressions;
using System.IO.Compression;
using Bai_Tap_Thep__Povision.PublishServices;
using Bai_Tap_Thep__Povision.BusinessServices;
using Bai_Tap_Thep__Povision.PortalServices;

namespace Bai_Tap_Thep__Povision
{
    public partial class frmLoadHoaDon : Form
    {
        public frmLoadHoaDon()
        {
            InitializeComponent();
        }
        string stringshow = "";

        private string writePDF(string pdf, string folder, string invNo)
        {
            string str = DateTime.Now.ToString("ddMMyyHHmmSS");
            string str2 = invNo + "_" + str + ".pdf";
            stringshow = str2;
            byte[] buffer = Convert.FromBase64String(pdf);
            FileStream stream1 = new FileStream(folder + @"\" + str2, FileMode.Create, FileAccess.ReadWrite);
            stream1.Write(buffer, 0, buffer.Length);
            stream1.Close();
            string[] textArray1 = new string[] { "OK: ", folder, @"\", str2, " \n" };
            return string.Concat(textArray1);
        }

        private void btnTaiHoaDon_Click(object sender, EventArgs e)
        {         
            PortalService bs = new PortalService();
            string rs = bs.downloadInvPDFFkeyNoPay(txtFKey.Text, txtUserName.Text, txtUserPass.Text);
            txtKetQua.Text = rs.Trim();
            if (rs.Trim().Length > 50)
            {
                writePDF(rs.Trim(), txtThuMuc.Text, "Thien");
                MessageBox.Show("File PDF đã được xuất ra tại : " + txtThuMuc.Text + @"\" + stringshow.Trim());
               axAcroPDF1.src = txtThuMuc.Text + @"\" + stringshow.Trim();
            } 
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.txtThuMuc.Text = dialog.SelectedPath;
                }
            }
        }

        private void frmLoadHoaDon_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PortalService bs = new PortalService();
            string rs = bs.downloadInvFkeyNoPay(txtFKey.Text, txtUserName.Text, txtUserPass.Text);

            string line = rs;

            string strStartupPath = Application.StartupPath;
            string strXmlDataFilePath = txtThuMuc.Text + "einv" + ".xml";
            System.IO.StreamWriter file = new System.IO.StreamWriter(strXmlDataFilePath);
            file.WriteLine(line);
            file.Close();
            

            MessageBox.Show(rs);

        }

      
    }
}

