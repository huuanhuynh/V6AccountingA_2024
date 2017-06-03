using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using V6Controls;
using V6Forms;


namespace Test
{
    public partial class FormTestV6Form : Form
    {
        public FormTestV6Form()
        {
            InitializeComponent();
        }

        V6Form vf1;
        V6FormInput vf2;


        private void FormTestV6Form_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new V6Forms.V6Form().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new V6FormInput().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new FormThemKH("1", "Data Source=v6soft;Initial Catalog=v6kd11vp;User ID=sa;Password=v6soft","ALKH").Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new FormThemKH("2", "Data Source=v6soft;Initial Catalog=v6kd11vp;User ID=sa;Password=v6soft",
                "ALKH","ma_kh",v6VvarTextBox1.Text) .Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Update username
            
        }
    }
}
