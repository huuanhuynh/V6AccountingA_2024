using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tools
{
    public partial class FormDBF : Form
    {
        public FormDBF()
        {
            InitializeComponent();
        }

        private void FormDBF_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                OpenDBF(files[0]);
            }
        }

        private void OpenDBF(string p)
        {
            dataGridView1.DataSource = V6Tools.ParseDBF.ReadDBF(p);
        }


        private void FormDBF_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
    }
}
