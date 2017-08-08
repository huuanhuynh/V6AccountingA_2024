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
            if (new ConfirmPasswordV6().ShowDialog(this) == DialogResult.OK)
            {
                new FormExportSqlTableToFile().Show();
            }
        }

        private void MyInit()
        {
            try
            {
                chkMouseRightTriple.Checked = V6Setting.Triple;
                chkAutoFixInvoiceVvar.Checked = V6Setting.Fixinvoicevvar;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
            Ready();
        }

        private void btnTestInvoice_Click(object sender, EventArgs e)
        {
            if (new ConfirmPasswordV6().ShowDialog(this) == DialogResult.OK)
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
            new FormDBF().Show();
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

        private void chkAutoFixInvoiceVvar_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsReady) return;

            if (chkAutoFixInvoiceVvar.Checked)
            {
                V6Setting.V6Special = V6Setting.V6Special + "Fixinvoicevvar";
            }
            else
            {
                V6Setting.V6Special = V6Setting.V6Special.Replace("Fixinvoicevvar", "");
            }
        }

        private void btnSQL_Click(object sender, EventArgs e)
        {

        }

       
    }
}
