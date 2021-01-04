using System.Windows.Forms;
using V6Controls.Controls.RicherTextBox;
using V6Init;

namespace V6Controls.Forms
{
    public partial class RichTextEditorForm : V6Form
    {
        public bool HaveChanged = true;

        public RichTextEditorForm()
        {
            InitializeComponent();

            richerTextBox1.HideToolstripItemsByGroup(
                RicherTextBoxToolStripGroups.Alignment |
                RicherTextBoxToolStripGroups.BoldUnderlineItalic,
                false);

            richerTextBox1.HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.Alignment, true);
            richerTextBox1.HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.BoldUnderlineItalic, true);

            richerTextBox1.ToggleBold();
            richerTextBox1.SetFontSize(8.25f);
            richerTextBox1.LANGUAGE = V6Login.SelectedLanguage;
        }
    }
}
