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
    public partial class V6DateTimeFullPickerNull : V6Control
    {
        public DateTime? Value
        {
            get { return GetThisValue(); }
            set { SetThisValue(value); }
        }

        public V6DateTimeFullPicker DateControl { get { return date1; } }

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

        public V6DateTimeFullPickerNull()
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

        /// <summary>
        /// Bật chức năng mang theo giá trị được gán cuối cùng
        /// </summary>
        [DefaultValue(false)]
        [Description("Bật chức năng mang theo giá trị được gán cuối cùng và được dùng bởi 2 hàm SetCarryValues UseCarryValues trong V6ControlFormHelper.")]
        public bool Carry { get; set; }
        /// <summary>
        /// Giá trị đang mang theo - kiểu string
        /// </summary>
        protected DateTime? Carry_Value;
        /// <summary>
        /// Mang theo giá trị hiện tại của control.
        /// </summary>
        public virtual void CarryValue()
        {
            if (Carry)
                Carry_Value = Value;
        }
        /// <summary>
        /// Gán lại giá trị lên control bằng value đã mang theo.
        /// </summary>
        public void UseCarry()
        {
            if (Carry)
                Value = Carry_Value;
        }
    }
}
