using System;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AAPPR_SOC_F10 : V6FormControl
    {
        public event EventHandler OkEvent;
        
        public string So_ctx
        {
            get { return txtSoCtXuat.Text; }
            set { txtSoCtXuat.Text = value; }
        }
        
        public AAPPR_SOC_F10()
        {
            InitializeComponent();
        }
        
        private void Form_Load(object sender, EventArgs e)
        {
            
        }
        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            
            try
            {
                var value = txtSoCtXuat.Text;
                Dispose();
                OnOkEvent(value);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Error:\n" + ex.Message);
            }
        }
        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            Dispose();
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

        protected virtual void OnOkEvent(object value)
        {
            var handler = OkEvent;
            if (handler != null) handler(value, new EventArgs());
        }
    }
}
