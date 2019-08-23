using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using UrielGuy.SyntaxHighlightingTextBox;

//using Tester;


namespace V6Controls.Controls.GridView
{
    public partial class CodeEditorForm : Form
    {
        private string APPPATH = "";
        //string startGroup = "{", endGroup = "}";
        string startGroup = "(", endGroup = ")";

        DataSet ds = new DataSet();

        public string UsingText { get; set; }
        public string ContentText
        {
            get { return txtColorTextBox.Text; }
            set { txtColorTextBox.Text = value; }
        }

        public CodeEditorForm()
        {
            InitializeComponent();
            myInit();                   
        }        

        private void myInit()
        {
            //SyntaxHighlightingTextBox txtColorTextBox = new SyntaxHighlightingTextBox();
            txtColorTextBox.coThayDoi = false;
            txtColorTextBox.Location = new Point(0, 0);
            txtColorTextBox.Dock = DockStyle.Fill;
            txtColorTextBox.Font = new Font("Courier New", 10);
            //txtColorTextBox.Font = new Font("Arial", 10);
            txtColorTextBox.Seperators.Add(' ');
            txtColorTextBox.Seperators.Add('\r');
            txtColorTextBox.Seperators.Add('\t');
            txtColorTextBox.Seperators.Add('\n');
            txtColorTextBox.Seperators.Add(',');
            txtColorTextBox.Seperators.Add('.');
            txtColorTextBox.Seperators.Add(';');
            txtColorTextBox.Seperators.Add('+');
            txtColorTextBox.Seperators.Add('-');
            txtColorTextBox.Seperators.Add('*');
            txtColorTextBox.Seperators.Add('/');

            txtColorTextBox.Seperators.Add('{');
            txtColorTextBox.Seperators.Add('}');
            txtColorTextBox.Seperators.Add('[');
            txtColorTextBox.Seperators.Add(']');
            txtColorTextBox.Seperators.Add('(');
            txtColorTextBox.Seperators.Add(')');
            txtColorTextBox.Seperators.Add('<');
            txtColorTextBox.Seperators.Add('>');
            txtColorTextBox.Seperators.Add('\'');
            txtColorTextBox.Seperators.Add('\"');
            //Controls.Add(shtb);
            //splitContainer2.Panel1.Controls.Add(txtColorTextBox);
            //txtColorTextBox.WordWrap = false;
            txtColorTextBox.ScrollBars = RichTextBoxScrollBars.Both;// & RichTextBoxScrollBars.ForcedVertical;

            //txtColorTextBox.FilterAutoComplete = false;
            txtColorTextBox.tenFile = "SQL";
        }

        private void CodeEditorForm_Load(object sender, EventArgs e)
        {
            try
            {
                APPPATH = Application.StartupPath;// "V6_Helper.getAppPath()";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            NapDanhSachTuKhoa(txtColorTextBox);
        }        

        private void btnRun(object sender, EventArgs e)
        {   
            
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {            
            newFile();
        }
        
        private void save()
        {
            setSttTrangThai("Lưu tài liệu...");
            if (txtColorTextBox.tenFile == "SQL")
            {
                saveAs();
            }
            else
            {
                if (Path.GetExtension(txtColorTextBox.tenFile).ToUpper().Equals(".RTF")
                    || Path.GetExtension(txtColorTextBox.tenFile).ToUpper().Equals(".DOC"))
                {
                    txtColorTextBox.SaveFile(txtColorTextBox.tenFile);
                }
                else
                {
                    saveText(txtColorTextBox.Text, txtColorTextBox.tenFile);
                }
                //txtColorTextBox.SaveFile(txtColorTextBox.tenFile);
                txtColorTextBox.coThayDoi = false;
            }
            setSttTrangThai("Sẵn sàng...");
        }
        private void saveText(string text, string fileName)
        {
            StreamWriter sw = new StreamWriter(fileName);
            try 
	        {	        
        		sw.Write(text);
                sw.Close();
	        }
	        catch 
	        {
                sw.Close();
        		MessageBox.Show("Lưu lỗi!");
	        }
        }

        private void newFile()
        {
            setSttTrangThai("Tạo tài liệu mới...");
            if (kiemTraLuu() != DialogResult.Cancel)
            {
                txtColorTextBox.tenFile = "SQL";
                txtColorTextBox.coThayDoi = false;
                txtColorTextBox.Clear();
                txtColorTextBox.Focus();
            }
            setSttTrangThai("Sẵn sàng...");
        }
        private DialogResult saveAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Select Sql *.sql|*.sql|Văn bản *.rtf|*.rtf|Tập tin text *.txt|*.txt|Tất cả tập tin!|*.*";
            sfd.Title = "Lưu theo tên";
            sfd.FileName = txtColorTextBox.tenFile;
            DialogResult drs = sfd.ShowDialog(this);
            if (drs == DialogResult.OK)
            {
                //Luu theo 2 dinh dang, text va dinh dang rieng
                //Neu phan mo rong cua file luu la ADNT hoặc RTF

                if (Path.GetExtension(sfd.FileName).ToUpper().Equals(".RTF")
                    || Path.GetExtension(sfd.FileName).ToUpper().Equals(".DOC"))
                {
                    txtColorTextBox.SaveFile(sfd.FileName);
                }
                else
                {
                    saveText(txtColorTextBox.Text, sfd.FileName);
                }
                //MessageBox.Show(txtColorTextBox.Text);
                txtColorTextBox.coThayDoi = false;
                txtColorTextBox.tenFile = sfd.FileName;
            }
            return drs;
        }
        private void openFile(string fileName)
        {
            txtColorTextBox.tenFile = fileName;
            //Add listview
            
            if (Path.GetExtension(fileName).ToUpper().Equals(".RTF")
                || Path.GetExtension(fileName).ToUpper().Equals(".DOC"))
            {
                try
                {
                    txtColorTextBox.LoadFile(fileName);
                    txtColorTextBox.coThayDoi = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Mở tập tin lỗi!\n" + ex.Message);
                }
            }
            else
            {
                string a = Path.GetExtension(fileName);                
                try
                {
                    StreamReader sr = new StreamReader(fileName);
                    txtColorTextBox.Text = sr.ReadToEnd();
                    txtColorTextBox.coThayDoi = false;
                    sr.Close();
                }
                catch(Exception ex)
                {
                    //sr.Close();
                    MessageBox.Show("Mở tập tin lỗi!\n"+ ex.Message);
                }
            }
            txtColorTextBox.Focus();
        }

        private DialogResult kiemTraLuu()
        {
            if (txtColorTextBox.coThayDoi)
            {
                DialogResult d = MessageBox.Show("Thay đổi chưa được lưu!\nBạn có muốn lưu không?", "V6SQLTraining", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (d == DialogResult.Yes)
                {
                    return saveAs();
                }
                else
                {
                    return d;
                }
            }
            return DialogResult.Yes;
        }

        private void openFile()
        {
            setSttTrangThai("Mở một tập tin");
            kiemTraLuu();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Select Sql *.sql|*.sql|Văn bản *.rtf, *.doc|*.rtf;*.doc|Tập tin text *.txt|*.txt|Tất cả tập tin!|*.*";
            ofd.Title = "Mở tập tin";
            ofd.DefaultExt = "sql";
            ofd.InitialDirectory = APPPATH;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                openFile(ofd.FileName);                
            }
            setSttTrangThai("Sẵn sàng...");
        }
        private void copyText()
        {
            tsbPaste.Enabled = true;
            try
            {
                Clipboard.SetText(txtColorTextBox.SelectedText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void cutText()
        {
            tsbPaste.Enabled = true;
            try
            {
                Clipboard.SetText(txtColorTextBox.SelectedText);
                txtColorTextBox.SelectedText = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message);
            }
        }
        private void pasteText()
        {
            try
            {
                txtColorTextBox.SelectedText = Clipboard.GetText();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void deleteText()
        {
            try
            {   
                txtColorTextBox.SelectedText = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message);
            }
        }

        private void tsbMouseIn(object sender, EventArgs e)
        {
            //setSttTrangThai(((Object)sender).ToString());
            setSttTrangThai(sender.ToString());
        }
        private void tsbMouseOut(object sender, EventArgs e)
        {
            setSttTrangThai("Sẵn sàng...");
        }

        /// <summary>
        /// Build test.
        /// </summary>
        private void Build()
        {
            try
            {
                var testBuild = V6ControlsHelper.CreateProgram("test_namespace", "test_classname", "random", UsingText, ContentText);
                if (testBuild != null)
                {
                    this.ShowInfoMessage("Build OK");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Build", ex);
            }
        }

        #region ========== Cài đặt phím nóng (hotkey) =============
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                switch (keyData)
                {
                    //case (Keys.Shift | Keys.F5):
                    //    tsbMultiSQL_Click(null, null);
                    //    return true;
                    //case Keys.F5:
                    //    RunSQL();
                    //    return true;
                    case (Keys.Control | Keys.N):
                        newFile();
                        return true;
                    case (Keys.Control | Keys.O):
                        openFile();
                        return true;
                    case (Keys.Control | Keys.S):
                        tsbSave_Click(null, null);
                        return true;
                    case (Keys.Control | Keys.P):
                        tsbPrint_Click(null, null);
                        return true;
                    case (Keys.F6):
                        Build();
                        return true;

                    case (Keys.Control | Keys.C):
                        copyText();
                        return true;
                    case (Keys.Control | Keys.V):
                        pasteText();
                        return true;
                    default:
                        return base.ProcessCmdKey(ref msg, keyData);
                }
            }
            catch
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        #endregion



        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (txtColorTextBox.coThayDoi)
            {
                save();                
            }
        }        

        private void toolStripMởFile_Click(object sender, EventArgs e)
        {
            openFile();
        }
         

        /////////////////////////////////////////
        private void NapDanhSachTuKhoa(SyntaxHighlightingTextBox.SyntaxHighlightingTextBox textBox)
        {            
            //Doc danh sach tu khoa tu file xml va nap vao :D
            string xmlFileName = Application.StartupPath + "\\Setting\\TuKhoa.xml";
            if (!System.IO.File.Exists(xmlFileName))
            {
                MessageBox.Show("Không tìm thấy file " + xmlFileName);
                return;
            }
            XmlTextReader reader = new XmlTextReader(xmlFileName);
            Dictionary<string, string> tukhoa_dictionary = new Dictionary<string, string>();
            string tukhoa = "", tukhoa2 = "";
            try
            {
                
                while (reader.Read())
                {
                    try
                    {
                        switch (reader.Name)
                        {
                            case "TuKhoa":
                                if (reader.GetAttribute("TenTuKhoa") != null)
                                {
                                    tukhoa = reader.GetAttribute("TenTuKhoa");
                                    string mausac = reader.GetAttribute("MauSac");
                                    bool autocomplete = "1" == reader.GetAttribute("AutoComplete");
                                    tukhoa_dictionary.Add(tukhoa ?? "", mausac);
                                    textBox.HighlightDescriptors.Add(new HighlightDescriptor(tukhoa, Color.FromName(mausac), autocomplete));
                                } break;
                            case "TuKhoaDoi":
                            case "TuKhoaKep":
                                if (reader.GetAttribute("TuKhoaBatDau") != null && reader.GetAttribute("TuKhoaKetThuc") != null)
                                {
                                    tukhoa = reader.GetAttribute("TuKhoaBatDau");
                                    tukhoa2 = reader.GetAttribute("TuKhoaKetThuc");
                                    string mausac = reader.GetAttribute("MauSac");
                                    bool autocomplete = "1" == reader.GetAttribute("AutoComplete");
                                    tukhoa_dictionary.Add(tukhoa ?? "", mausac);
                                    //tukhoa_dictionary.Add(tukhoa2 ?? "", mausac);
                                    //textBox.HighlightDescriptors.Add(new HighlightDescriptor(tukhoa1, tukhoa2, ADNT_Helper.MauSac(mausac), autocomplete));
                                    textBox.HighlightDescriptors.Add(new HighlightDescriptor(tukhoa, tukhoa2, autocomplete, Color.FromName(mausac)));
                                } break;
                            case "TuKhoaBatDau":
                                if (reader.GetAttribute("TuKhoaBatDau") != null)
                                {
                                    tukhoa = reader.GetAttribute("TuKhoaBatDau");
                                    string mausac = reader.GetAttribute("MauSac");
                                    bool autocomplete = "1" == reader.GetAttribute("AutoComplete");
                                    tukhoa_dictionary.Add(tukhoa ?? "", mausac);
                                    textBox.HighlightDescriptors.Add(new HighlightDescriptor(tukhoa, autocomplete, Color.FromName(mausac)));
                                } break;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.ShowErrorException("NapDanhSachTuKhoa " + tukhoa, ex);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                reader.Close();
                this.ShowErrorException("NapDanhSachTuKhoa ", ex);
            }
        }

        private void lưuKháchiệnTạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAs();
        }

        private void txtColorTextBox_TextChanged(object sender, EventArgs e)
        {
            txtColorTextBox.coThayDoi = true;
        }
        private string sendTab(int tab)
        {   
            string send = "";
            for (int i = 0; i < tab; i++)
            {
                send += "\t";
            }
            return send;
        }

        private void txtColorTextBox_KeyDown(object sender, KeyEventArgs e)
        {            
            ProcessDialogKey(e.KeyData);
        }   
        private void txtColorTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int tab = txtColorTextBox.demSoTuKhoaMoTruDong(startGroup, endGroup);
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                //int tab = txtColorTextBox.demSoTuKhoaMoTruDong("{","}");                
                SendKeys.Send(sendTab(tab));
                return;
            }
            if (e.KeyChar==endGroup.ToCharArray()[0])
            {
                //chưa làm được
                SendKeys.Send("\n");
                SendKeys.Send(sendTab(tab-1));
                //them dau dong
                return;
            }            
            
        }

        

        private void menuClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CodeEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtColorTextBox.coThayDoi)
            {
                DialogResult d = MessageBox.Show("Thay đổi chưa được lưu!\nBạn có muốn lưu không?", "ADNT", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (d == DialogResult.Yes)
                {
                    save();
                }
                else if (d == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void UpdateNumberRowCol(int dong, int chu)
        {
            sttDong.Text = "Dòng: " + dong;
            sttChu.Text = "Chữ: " + chu;
        }
        private void UpdateNumberRowCol()
        {
            int viTriDauNhay = txtColorTextBox.SelectionStart;
            int dong = txtColorTextBox.GetLineFromCharIndex(viTriDauNhay);
            int chu = viTriDauNhay - txtColorTextBox.GetFirstCharIndexFromLine(dong);
            UpdateNumberRowCol(dong + 1, chu + 1);
        }

        private void setSttTrangThai(string trangThai)
        {
            sttTrangThai.Text = trangThai;
        }

        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void tsbCut_Click(object sender, EventArgs e)
        {
            cutText();
        }

        private void tsbPaste_Click(object sender, EventArgs e)
        {
            pasteText();
        }

        private void tsbCopy_Click(object sender, EventArgs e)
        {
            copyText();
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Chưa");
            PrintDialog prd = new PrintDialog();
            prd.AllowCurrentPage = false;
            prd.AllowSelection = false;
            prd.AllowSomePages = false;            
            if (prd.ShowDialog(this)==DialogResult.OK)
            {
                txtColorTextBox.PrinterName = prd.PrinterSettings.PrinterName;                
                txtColorTextBox.PrintRichTextContents();
            }
        }

        private void CodeEditorForm_ResizeEnd(object sender, EventArgs e)
        {
            sttTrangThai.Width = 500 * sttMain.Width / 800;
            sttDong.Width = 100 * sttMain.Width / 800;
            sttChu.Width = 100 * sttMain.Width / 800;            
        }
        
        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteText();
        }

        private void rớtDòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtColorTextBox.WordWrap = !txtColorTextBox.WordWrap;
            rớtDòngToolStripMenuItem.Checked = txtColorTextBox.WordWrap;
        }

        private void tsbExportToExcel_Click(object sender, EventArgs e)
        {
            
        }

        private void hiệnThanhMenu1ToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void hiệnThanhMenu2ToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            
        }
        
        private void tsbMultiSQL_Click(object sender, EventArgs e)
        {
            
        }

        private void runSQLMultiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbMultiSQL_Click(null, null);
        }

        private void TạoMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newFile();
        }

        private void mởToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void tsbXmlTable_Click(object sender, EventArgs e)
        {
            
        }

        private void txtColorTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateNumberRowCol();
        }

        private void txtColorTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            UpdateNumberRowCol();
        }

        private void txtColorTextBox_Enter(object sender, EventArgs e)
        {
            txtColorTextBox.CallOnTextChange();
        }

        private void buildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Build();
        }
        
    }
}

