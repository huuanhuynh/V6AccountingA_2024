using System;
using System.Windows.Forms;

namespace V6Controls
{
    public partial class FormChangeVvar : Form
    {
        public FormChangeVvar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(char)Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
            }
        }        
    }
}
