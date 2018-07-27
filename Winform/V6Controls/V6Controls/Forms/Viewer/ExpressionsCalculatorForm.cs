using System;
using System.Globalization;
using System.Windows.Forms;
using V6Tools.V6Convert;

namespace V6Controls.Forms.Viewer
{
    public partial class ExpressionsCalculatorForm : V6Form
    {
        public ExpressionsCalculatorForm()
        {
            InitializeComponent();
        }

        private void txtBieuThuc_TextChanged(object sender, EventArgs e)
        {
            Tinh();
        }

        private void Tinh()
        {
            try
            {
                decimal value = Number.GiaTriBieuThuc(txtBieuThuc.Text, null);
                int decimals = value.ToString(CultureInfo.InvariantCulture).Split('.').Length > 1
                  ? value.ToString(CultureInfo.InvariantCulture).Split('.')[1].Length
                  : 0;
                txtKetQua.Text = ObjectAndString.NumberToString(value, decimals, ",", " ");
            }
            catch (Exception ex)
            {
                txtKetQua.Text = ex.Message;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtKetQua.Text);
        }

        private void txtBieuThuc_SelectionChanged(object sender, EventArgs e)
        {
            ViewSelection();
        }

        private void ViewSelection()
        {
            try
            {
                string status = "Length=0";
                if (txtBieuThuc.TextLength > 0)
                {
                    status = "Length=" + txtBieuThuc.TextLength;
                    status += " SelectionStart=" + txtBieuThuc.SelectionStart;
                    status += " SelectionLength=" + txtBieuThuc.SelectionLength;
                }

                lblSelectionStatus.Text = status;
            }
            catch (Exception)
            {
                //
            }
        }
    }
}
