using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using V6Controls;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (V6Controls.V6ConfirmDialogs.Show("Câu thông báo của bạn!") == DialogResult.OK)
                V6Controls.V6ConfirmDialogs.Show("bạn đã bấm nhận", "V6Soft");
            else
                V6Controls.V6ConfirmDialogs.Show("bạn không bấm nhận", "V6Soft");
        }

        private void btnYesNo_Click(object sender, EventArgs e)
        {
            DialogResult d = V6Controls.V6ConfirmDialogs.Show("Câu thông báo của bạn! Dài dài thêm tí nữa coi làm sao! :D", "Tiêu đề", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
                V6Controls.V6ConfirmDialogs.Show("Bạn đã bấm Có");
            else if (d == DialogResult.No)
            {
                V6Controls.V6ConfirmDialogs.Show("Bạn đã bấm Không");
            }
            else
                V6Controls.V6ConfirmDialogs.Show("bạn không bấm nút nào");
        }

        private void btnYesNoCancel_Click(object sender, EventArgs e)
        {
            V6ConfirmDialogs.Show("message ádf gádf gs dg sèg sdfg sdfg sètgsẻ sẻ sẻ sếtrfg sdf gsè gsdfg sdrfg sdf !", "Tiêu đề", MessageBoxButtons.YesNoCancel);
        }

        private void btnOkCancel_Click(object sender, EventArgs e)
        {
            V6ConfirmDialogs.Show("Câu thông báo của bạn!"
                                + "\nDòng thứ 2!"
                                + "\nDòng thứ 3!", "Tiêu đề", MessageBoxButtons.OKCancel);
        }

        private void btnError_Click(object sender, EventArgs e)
        {
            V6ConfirmDialogs.Show("message", "title", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            V6ConfirmDialogs.Test();
        }

        private void v6HiddenButton1_Click(object sender, EventArgs e)
        {
            V6ConfirmDialogs.Show("Bạn vừa bấm nút V6HiddentButton!", "V6Soft", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //contextMenuStrip1.Parent.Dispose();
                
                //customTabControl1.TabPages.Remove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            addTable();
            addTabControl();
            
        }
        private void addTabControl()
        {
            //CustomTabControl customTabControl1 = new CustomTabControl();
            //
        }
        private void addTable()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("Cot 1");
            tb.Columns.Add("Cot 2");
            tb.Columns.Add("Cot 3");
            tb.Columns.Add("Cot 4");
            DataRow r;
            for (int i = 0; i < 10; i++)
            {
                r = tb.NewRow();
                r[0] = "Dong " + (i + 1) + " cot 1";
                r[1] = "Dong " + (i + 1) + " cot 2";
                r[2] = "Dong " + (i + 1) + " cot 3";
                r[3] = "Dong " + (i + 1) + " cot 4";
                tb.Rows.Add(r);
            }
            v6ColorDataGridView1.DataSource = tb;
        }

        private void btnCreateTab_Click(object sender, EventArgs e)
        {
            V6TabControlLib.V6TabPage tpe = new V6TabControlLib.V6TabPage();
            tpe.Text = "Chứng từ " + (V6TabControl1.TabPages.Count + 1);
            V6TabControl1.TabPages.Add(tpe);
        }

        private void customTabControl1_TabClosing(object sender, TabControlCancelEventArgs e)
        {            
            DialogResult d = V6ConfirmDialogs.Show("Đóng tab?", "V6", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormTabChungTu ftct = new FormTabChungTu();
            ftct.Show();
        }

        
    }
}
