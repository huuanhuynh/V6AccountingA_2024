using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HuuanTetris
{
    public partial class HuuanTetrisEnterName : Form
    {
        public HuuanTetrisEnterName()
        {
            InitializeComponent();
        }
        public string inputName = "Không tên";

        private void button1_Click(object sender, EventArgs e)
        {
            inputName = textBox1.Text;
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(null, null);
        }
    }
}
