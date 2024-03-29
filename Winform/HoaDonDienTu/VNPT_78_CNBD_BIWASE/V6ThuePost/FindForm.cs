﻿using System;
using System.Windows.Forms;

namespace V6Controls.Forms
{
    public partial class FindForm : Form
    {
        public FindForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Tìm lần đầu khi vừa thay đổi nội dung tìm kiếm.
        /// </summary>
        private bool _first = true;

        public delegate void FindFormHandler(string text, bool up, bool first);

        public event FindFormHandler Find;
        protected virtual void OnFind()
        {
            var handler = Find;
            if (handler != null) handler(txtFindText.Text, radUp.Checked, _first);
            _first = false;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtFindText.Text != string.Empty)
            {
                OnFind();
            }
            else
            {
                //txtFindText.Alert();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtFindText_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            btnFind.PerformClick();
        }

        private void txtFindText_TextChanged(object sender, EventArgs e)
        {
            _first = true;
        }

        
    }
}
