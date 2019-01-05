﻿using System;
using System.Windows.Forms;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AAPPR_POH1_F9 : V6Form
    {
        public AAPPR_POH1_F9()
        {
            InitializeComponent();
        }
        
        private void Form_Load(object sender, EventArgs e)
        {
            //SetStatus2Text();
        }
        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnHuy.PerformClick();
            }
            else if (keyData == (Keys.Control | Keys.Enter))
            {
                btnNhan.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected int _oldIndex = -1;
        
    }
}