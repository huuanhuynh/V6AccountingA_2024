using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using V6SqlConnect;

namespace Tools
{
    public partial class V6DatabaseConfogTestForm : Form
    {
        public V6DatabaseConfogTestForm()
        {
            InitializeComponent();
            richTextBox1.AllowDrop = true;
            richTextBox1.DragEnter += V6DatabaseConfogTestForm_DragEnter;
            richTextBox1.DragDrop += V6DatabaseConfogTestForm_DragDrop;
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Xml|*.xml|All files|*.*";
            if (open.ShowDialog(this) == DialogResult.OK)
            {
                txtFile.Text = open.FileName;
                LoadConfig();
            }
        }

        private void LoadConfig()
        {
            try
            {
                DatabaseConfig.LoadDatabaseConfig("V6Soft", txtFile.Text);
                cboDatabase.DisplayMember = DatabaseConfig.ConfigDataDisplayMember;
                cboDatabase.ValueMember = DatabaseConfig.ConfigDataValueMember;
                cboDatabase.DataSource = DatabaseConfig.ConnectionConfigData;
                cboDatabase.DisplayMember = DatabaseConfig.ConfigDataDisplayMember;
                cboDatabase.ValueMember = DatabaseConfig.ConfigDataValueMember;
                cboDatabase.SelectedIndex = DatabaseConfig.GetConfigDataRunIndex();
                btnCheckConnection.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
        }

        private void btnCheckConnection_Click(object sender, EventArgs e)
        {
            CheckSelectedConfig();
        }

        private void CheckSelectedConfig()
        {
            try
            {
                SqlConnection con = new SqlConnection(DatabaseConfig.ConnectionString);
                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    richTextBox1.AppendText(DatabaseConfig.Database + " Connection open ok.\n");
                }
                con.Close();
                if (con.State == ConnectionState.Closed)
                {
                    richTextBox1.AppendText(DatabaseConfig.Database + " Connection closed ok.\n");
                }
                richTextBox1.AppendText("==============================================\n");
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error: " + ex.Message);
                richTextBox1.AppendText(DatabaseConfig.Database + " Error.\n");
                richTextBox1.AppendText(ex.Message + "\n");
                richTextBox1.AppendText("==============================================\n");
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
            }
        }

        private void V6DatabaseConfogTestForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void V6DatabaseConfogTestForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                txtFile.Text = files[0];
                LoadConfig();
            }
        }

        private void cboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseConfig.SelectedIndex = cboDatabase.SelectedIndex;
        }

    }
}
