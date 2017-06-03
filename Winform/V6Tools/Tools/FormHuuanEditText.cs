using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tools
{
    public partial class FormHuuanEditText : Form
    {
        public FormHuuanEditText()
        {
            InitializeComponent();
        }

        List<string> _file, _file_result;
        Dictionary<string,string> _replace1, _replace2;
        private void OpenFile(string file)
        {
            _file = new List<string>();
            try
            {
                if (File.Exists(file))
                {
                    FileStream fs = new FileStream(file, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    while (!sr.EndOfStream)
                    {
                        _file.Add(sr.ReadLine().Trim()
                            .Replace(",","").Replace("[","").Replace("]","")
                            );
                    }
                    sr.Close();
                    txtResult.Clear();
                    txtFields.Lines = _file.ToArray();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void OpenReplace(string file, ref Dictionary<string, string> dic)
        {
            dic = new Dictionary<string, string>();
            try
            {
                if (File.Exists(file))
                {
                    FileStream fs = new FileStream(file, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    string s = "";
                    while (!sr.EndOfStream)
                    {
                        s = sr.ReadLine();
                        if (!s.StartsWith(";"))
                        {
                            if (s.Contains('='))
                            {
                                string[] ss = s.Split('=');
                                dic.Add(ss[0], ss[1]);
                            }
                        }
                    }

                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormHuuanEditText_Load(object sender, EventArgs e)
        {
            try
            {
                OpenFile(txtFile.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        OpenFileDialog o = new OpenFileDialog();
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                if(o.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtFile.Text = o.FileName;
                    OpenFile(txtFile.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOpenReplace1_Click(object sender, EventArgs e)
        {
            try
            {
                if (o.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtReplace1.Text = o.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOpenReplace2_Click(object sender, EventArgs e)
        {
            try
            {
                if (o.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtReplace2.Text = o.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                _file = txtFields.Lines.ToList();
                OpenReplace(txtReplace1.Text, ref _replace1);
                OpenReplace(txtReplace2.Text, ref _replace2);

                _file_result = new List<string>();
                int count = 0;
                foreach (string field0 in _file)
                {
                    string field = field0.Trim().Replace(",", "").Replace("[", "").Replace("]", "");
                    string line_result = txtEditText.Text.Trim();

                    if (count <= 3)
                    {
                        string field_lower = field.ToLower();
                        if (field_lower.StartsWith("ma_"))
                        {
                            line_result = line_result.Replace("{LineReplace2}", "Code");
                        }
                        if (field_lower.StartsWith("ten_") && field_lower.EndsWith("2"))
                        {
                            line_result = line_result.Replace("{LineReplace1}", "OtherName").Replace("{LineReplace2}", "OtherName");
                        }
                        if (field_lower.StartsWith("ten_"))
                        {
                            line_result = line_result.Replace("{LineReplace1}", "Name").Replace("{LineReplace2}", "Name");
                        }
                    }
                    if (count >= 4 && count <= 5)
                    {
                        string field_lower = field.ToLower();
                        if (field_lower.StartsWith("ten_") && field_lower.EndsWith("2"))
                        {
                            line_result = line_result.Replace("{LineReplace1}", "OtherName").Replace("{LineReplace2}", "OtherName");
                        }
                    }

                    line_result = line_result
                        .Replace("{Line}", field)
                        .Replace("{LineReplace1}", _replace1.ContainsKey(field.ToLower())? _replace1[field.ToLower()] : field.ToUpper())
                        .Replace("{LineReplace2}", _replace2.ContainsKey(field.ToLower()) ? _replace2[field.ToLower()] : field.ToUpper())
                        .Replace("{LineToUpper}", field.ToUpper())
                        .Replace("{LineToUpper1}", field.Length > 0 ? ("" + field[0].ToString().ToUpper() + field.Substring(1)) : "")
                        .Replace("{LineToLower}", field.ToLower())
                        ;
                    //Xử lý thêm ma_ = Code, ten_ = Name, ten2...
                    

                    _file_result.Add(line_result); count++;
                }
                txtResult.Lines = _file_result.ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
}
