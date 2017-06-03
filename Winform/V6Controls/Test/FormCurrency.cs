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
    public partial class FormCurrency : Form
    {
        public FormCurrency()
        {
            InitializeComponent();
        }

        private void v6CurrencyTextBox1_Validated(object sender, EventArgs e)
        {
            propertyGrid1.Refresh();
        }

        private void propertyGrid1_Validated(object sender, EventArgs e)
        {
            v6CurrencyTextBox1.Refresh();
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            v6CurrencyTextBox1.Refresh();
        }

        private void v6CurrencyTextBox1_TextChanged(object sender, EventArgs e)
        {
            propertyGrid1.Refresh();
        }
    }
}
