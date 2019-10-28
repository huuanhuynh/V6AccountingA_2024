using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bai_Tap_Thep__Povision
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void xửLýHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImportAndPublishInv frm = new frmImportAndPublishInv();
            frm.ShowDialog();
        }

        private void loadDownloadHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLoadHoaDon frm = new frmLoadHoaDon();
            frm.ShowDialog();
        }

        private void convertDSKháchHàngSangXMLZIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConvertXMLtoExcel frm = new frmConvertXMLtoExcel();
            frm.ShowDialog();
        }
    }
}
