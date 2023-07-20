using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace V6DBF2EXCELTEMPLATE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = Program.fileSave;
            saveFileDialog1.FileName = Program.fileSave;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.Text = saveFileDialog1.FileName;
                Program.fileSave = richTextBox1.Text;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Program.fileSave = richTextBox1.Text;
                Program.ExportExcelTemplate();
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
