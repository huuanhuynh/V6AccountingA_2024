using System;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ToolManager
{
    public partial class AllTool : V6FormControl
    {
        public AllTool()
        {
            InitializeComponent();
            MyInit();
        }

        public AllTool(string itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
            MyInit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FormExportSqlTableToFile().Show();
        }

        private void MyInit()
        {
            try
            {
                chkMouseRightTriple.Checked = V6Setting.V6Special.Contains("Triple");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
            Ready();
        }

        private void btnTestInvoice_Click(object sender, EventArgs e)
        {
            if (new ConfirmPasswordV6().ShowDialog() == DialogResult.OK)
            {
                new FormTestInvoice().Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FormConvertTable().Show();
        }

        private void btnDBF_Click(object sender, EventArgs e)
        {
            V6Message.Show("Dùng convert table!");
            //new FormDBF().Show();
        }

        private void btnModelHelp_Click(object sender, EventArgs e)
        {
            new FormModelHelp().Show();
        }

        private void btnHdocument_Click(object sender, EventArgs e)
        {
            try
            {
                new FormHdocumentEditor().Show();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void btnSearchInRpt_Click(object sender, EventArgs e)
        {
            try
            {
                new FormSearchRpt().Show();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void btnLogView_Click(object sender, EventArgs e)
        {
            new FormLogView().Show();
        }

        private void chkMouseRightTriple_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsReady) return;

            if (chkMouseRightTriple.Checked)
            {
                V6Setting.V6Special = V6Setting.V6Special + "Triple";
            }
            else
            {
                V6Setting.V6Special = V6Setting.V6Special.Replace("Triple", "");
            }
        }
    }
}
