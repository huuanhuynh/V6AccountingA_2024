﻿using System;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.SoDuManager.FirstFilter
{
    public partial class AlkmbFilterForm : V6Form
    {
        public string QueryString { get; set; }

        public AlkmbFilterForm()
        {
            InitializeComponent();
            MyInit();
        }
        
        private void MyInit()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".MyInit: " + ex.Message, "AlkmbFilterForm");
            }
        }

        private void CallFilterOkClick()
        {
            try
            {
                var where = string.Format("ngay_hl BETWEEN '{0}' AND '{1}'",
                    v6ColorDateTimePick1.YYYYMMDD,
                    v6ColorDateTimePick2.YYYYMMDD);

                if (txtMaDVCS.Text != "")
                {
                    where += string.Format(" and MA_DVCS like '{0}%'", txtMaDVCS.Text);
                }

                QueryString = where;
                
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".CallFilterOkClick", ex);
            }
        }
        
        public override bool DoHotKey0(Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Control | Keys.Enter))
                {
                    CallFilterOkClick();
                    return true;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return false;
        }

        private void FilterForm_Load(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            CallFilterOkClick();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
