using System;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ToolManager
{
    public partial class FormSearchRpt : V6Form
    {
        FolderBrowserDialog f = new FolderBrowserDialog();

        public FormSearchRpt()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void LoadFiles()
        {
            var fileNames = Directory.GetFiles(txtFolder.Text, "*.rpt");
            listBox1.DataSource = fileNames;
        }

        private void buttonFolder_Click(object sender, EventArgs e)
        {
            f.SelectedPath = txtFolder.Text;
            f.Description = "Chọn một thư mục cần nén!";
            if (f.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = f.SelectedPath;
                LoadFiles();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var errors = "";
            try
            {
                if (textBox1.Text == "")
                {
                    return;
                }
                
                var search_value = textBox1.Text.ToLower();
                foreach (object item in listBox1.Items)
                {
                    try
                    {
                        string fileName = item.ToString();
                        if (!fileName.ToLower().EndsWith(".rpt")) continue;
                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(fileName);

                        foreach (ReportObject robject in rpt.ReportDefinition.ReportObjects)
                        {
                            if (robject is TextObject)
                            {
                                var textobject = robject as TextObject;
                                if (textobject.Text.ToLower().Contains(search_value))
                                {
                                    listBox2.Items.Add(fileName);
                                    richTextBox1.AppendText("=> " + fileName + " Text:\r\n" + textobject.Text + "\r\n");
                                }
                            }
                        }

                        for (int i = 0; i < rpt.DataDefinition.FormulaFields.Count; i++)
                        {
                            var fobj = rpt.DataDefinition.FormulaFields[i];
                            var oName = fobj.Name;
                            var fName = fobj.FormulaName;
                            var fValue = fobj.Text;
                            if (fValue != null && fValue.ToLower().Contains(search_value))
                            {
                                listBox2.Items.Add(fileName);
                                richTextBox1.AppendText(string.Format("=> {0} Formular {1}:\r\n{2}\r\n", fileName, fName, fValue));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errors += ex.Message + "\n";
                    }
                }

                if (errors.Length > 0)
                {
                    richTextBox1.AppendText("\r\nERRORS:\r\n");
                    richTextBox1.AppendText(errors);
                }

                this.ShowMessage(V6Text.Finish);
            }
            catch (Exception exception)
            {
                this.ShowErrorMessage(exception.Message);
            }
        }

        private void txtFolder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Directory.Exists(txtFolder.Text))
                {
                    LoadFiles();
                }
            }
        }

       
    }
}
