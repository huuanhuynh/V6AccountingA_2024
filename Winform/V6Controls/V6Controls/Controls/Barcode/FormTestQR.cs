using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using V6Controls.Forms;
using V6Tools.V6Convert;

namespace V6Controls.Controls.Barcode
{
    public partial class FormTestQR : Form
    {
        public FormTestQR()
        {
            InitializeComponent();
        }

        private void FormTestQR_Load(object sender, EventArgs e)
        {
            var a = qRview1.ByteArrayValue;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Text = "Length: " + textBox1.TextLength;
        }
    }
}
