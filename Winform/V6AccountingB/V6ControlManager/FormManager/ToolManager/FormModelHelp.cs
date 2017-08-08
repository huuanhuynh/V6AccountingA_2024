using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ToolManager
{
    public partial class FormModelHelp : V6Form
    {
        public FormModelHelp()
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
                this.ShowErrorMessage(ex.Message);
            }
        }
        private void OpenReplace(string file, ref Dictionary<string, string> dic)
        {
            dic = new Dictionary<string, string>();
            FileStream fs = new FileStream(file, FileMode.Open);
            try
            {
                if (File.Exists(file))
                {
                    
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
                fs.Close();
                this.ShowErrorMessage(ex.Message);
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
                this.ShowErrorMessage(ex.Message);
            }
        }

        OpenFileDialog o = new OpenFileDialog();
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                if(o.ShowDialog(this) == DialogResult.OK)
                {
                    txtFile.Text = o.FileName;
                    OpenFile(txtFile.Text);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void btnOpenReplace1_Click(object sender, EventArgs e)
        {
            try
            {
                if (o.ShowDialog(this) == DialogResult.OK)
                {
                    txtReplace1.Text = o.FileName;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void btnOpenReplace2_Click(object sender, EventArgs e)
        {
            try
            {
                if (o.ShowDialog(this) == DialogResult.OK)
                {
                    //txtReplace2.Text = o.FileName;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private string GetFieldName(string s)
        {
            Regex r = new Regex(@" ([A-Za-z0-9_]+) \{");
            MatchCollection mc = r.Matches(s);
            if (mc.Count >= 1) return mc[0].Groups[1].Value;
            else return "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                _file = txtFields.Lines.ToList();
                OpenReplace(txtReplace1.Text, ref _replace1);
                //OpenReplace(txtReplace2.Text, ref _replace2);

                _file_result = new List<string>();
                int count = 0;
                foreach (string line in _file)
                {
                    string line_text = line.Trim();
                    if (line_text.StartsWith("[") || line_text == "") continue;

                    _file_result.Add("/// <summary>");
                    _file_result.Add("/// Column: " + GetFieldName(line_text));
                    _file_result.Add("/// Description: ");
                    _file_result.Add("/// </summary>");

                    line_text = line_text.ToLower();

                    foreach (string item in _replace1.Keys)
                    {
                        line_text = line_text.Replace(item, _replace1[item]);
                    }
                    
                    _file_result.Add(line_text); count++;
                }

                txtResult.Lines = _file_result.ToArray();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void btnUpdate2_Click(object sender, EventArgs e)
        {
            try
            {
                _file = txtFields.Lines.ToList();
                OpenReplace(txtReplace1.Text, ref _replace1);
                //OpenReplace(txtReplace2.Text, ref _replace2);

                _file_result = new List<string>();
                int count = 0;
                foreach (string line in _file)
                {
                    string line_text = line.Trim();
                    if (line_text.StartsWith("[") || line_text == "") continue;

                    string field = GetFieldName(line_text.Trim());

                    if (_replace1.ContainsKey(field.ToLower()))
                    {
                        _file_result.Add(string.Format("{0} = x.{1},", _replace1[field.ToLower()], field));
                    }
                    else
                    {
                        _file_result.Add(string.Format("{0} = x.{1},", "Nooô", field));
                    }

                    count++;
                }

                txtResult.Lines = _file_result.ToArray();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void btnUpdate3_Click(object sender, EventArgs e)
        {
            try
            {
                _file = txtFields.Lines.ToList();
                //OpenReplace(txtReplace1.Text, ref _replace1);
                //OpenReplace(txtReplace2.Text, ref _replace2);

                _file_result = new List<string>();
                int count = 0;
                foreach (string line in _file)
                {
                    string line_text = line.Trim();
                    
                    string line_result = "public DbSet<"+line_text+"> DM"+line_text+" { get; set; }";
                    _file_result.Add(line_result);

                    count++;
                }

                txtResult.Lines = _file_result.ToArray();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = DateTime.ParseExact(textBox1.Text, "d/M/yyyy", null).ToString("MM/dd/yyyy");
        }
    }
}
