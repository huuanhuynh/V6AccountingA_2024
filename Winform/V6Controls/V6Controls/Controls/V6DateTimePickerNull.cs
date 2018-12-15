using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using V6Controls.Forms;

namespace V6Controls.Controls
{
    public partial class V6DateTimePickerNull : V6Control
    {
        public DateTime? Value
        {
            get { return GetThisValue(); }
            set { SetThisValue(value); }
        }

        private void SetThisValue(DateTime? value)
        {
            if (value == null)
            {
                chkUSE.Checked = false;
            }
            else
            {
                chkUSE.Checked = true;
                date1.SetValue(value.Value);
            }
        }

        private DateTime? GetThisValue()
        {
            return chkUSE.Checked ? date1.Value : (DateTime?) null;
        }

        public V6DateTimePickerNull()
        {
            InitializeComponent();
        }

        private void date1_ValueChanged(object sender, EventArgs e)
        {
            chkUSE.Checked = true;
        }

        private void chkUSE_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUSE.Checked)
            {
                date1.Value = DateTime.Now;
                ShowDate();
            }
            else
            {
                HideDate();
            }
        }

        private void ShowDate()
        {
            date1.Visible = true;
        }

        private void HideDate()
        {
            date1.Visible = false;
        }
        
    }
}
