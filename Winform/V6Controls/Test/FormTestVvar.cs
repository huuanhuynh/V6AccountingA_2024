using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test
{
    public partial class FormTestVvar : Form
    {
        public FormTestVvar()
        {
            InitializeComponent();
        }

        private void v6VvarTextBox1_TextChanged(object sender, EventArgs e)
        {
            propertyGrid1.Refresh();
        }

        private void v6VvarTextBox1_Validated(object sender, EventArgs e)
        {
            propertyGrid1.Refresh();
        }

        private void propertyGrid1_Validated(object sender, EventArgs e)
        {
            v6VvarTextBox1.Refresh();
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            v6VvarTextBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            v6VvarTextBox1.LoadAutoCompleteSource();
        }
    }
}
