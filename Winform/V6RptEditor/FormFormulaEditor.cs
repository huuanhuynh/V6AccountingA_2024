using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace V6RptEditor
{
    public partial class FormFormulaEditor : Form
    {
        public FormFormulaEditor()
        {
            InitializeComponent();
        }
        rptFormulaField _ff;
        public FormFormulaEditor(rptFormulaField ff)
        {
            InitializeComponent();
            _ff = ff;
        }

        private void FormFormulaEditor_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = _ff.FormulaText;
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chưa có chức năng kiểm tra cú pháp!");
            _ff.FormulaText = richTextBox1.Text;
            toolStripButtonSave.Enabled = false;
            toolStripButtonSaveAndClose.Enabled = false;
        }

        private void toolStripButtonSaveAndClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chưa có chức năng kiểm tra cú pháp!");
            _ff.FormulaText = richTextBox1.Text;
            toolStripButtonSave.Enabled = false;
            toolStripButtonSaveAndClose.Enabled = false;
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            toolStripButtonSave.Enabled = true;
            toolStripButtonSaveAndClose.Enabled = true;
        }
    }
}
