using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Test
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FormCurrency().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FormTabChungTu().Show();
        }

        private void btnTestVvar_Click(object sender, EventArgs e)
        {
            new FormTestVvar().Show();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void btntestv6f_Click(object sender, EventArgs e)
        {
            new FormTestV6Form().Show();
        }
    }
}
