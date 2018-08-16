using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.HeThong.QuanLyHeThong.NgonNgu
{
    public partial class CorplanControl  : V6FormControl
    {
        private Dictionary<string, LineObjects> lineDic = new Dictionary<string, LineObjects>(); 
        public CorplanControl()
        {
            InitializeComponent();
            MyInit();
        }

        public CorplanControl(string itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            if (V6Setting.Language != "V") langs[1] = V6Setting.Language;
            cboSFile.ValueMember = "SFile";
            cboSFile.DisplayMember = "Ten";
            cboSFile.DataSource = V6BusinessHelper.Select(_tableName, "SFile, Max(Ma_ct+'_'+SFile+': '+IsNull([" +
                (V6Setting.IsVietnamese ? "Ten" : "Ten2")
                + "],'')) as Ten", "", "SFile", "SFile").Data;
            cboSFile.ValueMember = "SFile";
            cboSFile.DisplayMember = "Ten";
        }

        private const string ID_FIELD = "ID";
        string[] langs = { "V", "E" };
        int lblLangWidth = 180;
        private string _tableName = "Corplan";
        private int _index;
        private IDictionary<string,object> _currentRowData;
        private void GenControls()
        {
            try
            {
                lineDic = new Dictionary<string, LineObjects>();
                _currentRowData = null;
                panel1.VerticalScroll.Value = 0;
                RemoveControls();
                GenTitleLabels();
                
                var SFile = cboSFile.SelectedValue.ToString().Trim();
                if (SFile != "")
                {
                    var data = V6BusinessHelper.Select(_tableName, "*", "SFile='" + SFile + "'", "", "D,ID").Data;
                    _index = 0;
                    foreach (DataRow row in data.Rows)
                    {
                        _index++;
                        GenOneLine(row.ToDataDictionary(), _index);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".GenControls: " + ex.Message, ex.Source);
            }
        }

        private void GenOneLine(SortedDictionary<string, object> row, int index)
        {
            try
            {
                LineObjects lineO = new LineObjects();
                var top = index * 23 + panel1.AutoScrollPosition.Y;
                var ID = row[ID_FIELD].ToString().Trim().ToUpper();

                V6ColorTextBox txtID = new V6ColorTextBox();
                txtID.Name = "txtID" + ID;
                txtID.AccessibleName = txtID.Name;
                txtID.Top = top;
                txtID.Left = lblID.Left;
                txtID.Width = lblID.Width;
                txtID.ReadOnly = true;
                txtID.TabStop = false;
                txtID.Text = ID;
                txtID.GotFocus += (sender, e) =>
                {
                    _currentRowData = row;//.ToDataDictionary();
                    CopyCurrentRowIdToClipBoard();
                };
                txtID.KeyDown += (sender, e) =>
                {
                    var keyChar = (char) e.KeyValue;
                    if (char.IsLetterOrDigit((char)e.KeyValue))
                    {
                        var rowD = row["D"].ToString().Trim();
                        foreach (KeyValuePair<string, LineObjects> item in lineDic)
                        {
                            TextBox txt_default = item.Value.TxtDefault;
                            TextBox txt_id = item.Value.TxtID;
                            string cString = keyChar.ToString().ToUpper();
                            var stringCompare = string.Compare(txt_default.Text, rowD, StringComparison.InvariantCulture);
                            if ((e.Shift||stringCompare>0) && txt_default.Text.ToUpper().StartsWith(cString))
                            {
                                txt_id.Focus();
                                return;
                            }
                        }
                        //Nếu tìm không thấy chạy lại từ đầu không cần điều kiện
                        foreach (KeyValuePair<string, LineObjects> item in lineDic)
                        {
                            TextBox txt_default = item.Value.TxtDefault;
                            TextBox txt_id = item.Value.TxtID;
                            string cString = keyChar.ToString().ToUpper();
                            //var stringCompare = string.Compare(txt_default.Text, rowD, StringComparison.InvariantCulture);
                            if ((true) && txt_default.Text.ToUpper().StartsWith(cString))
                            {
                                txt_id.Focus();
                                break;
                            }
                        }
                    }
                };
                lineO.TxtID = txtID;
                panel1.Controls.Add(txtID);

                //Nút update
                var btnLeft = lblDefault.Right + 25 + langs.Length * (lblLangWidth + 5);
                Button b = new Button();
                b.Name = "btn" + ID;
                b.Left = btnLeft;
                b.Top = top;
                b.UseVisualStyleBackColor = true;
                b.Text = "&Update";
                b.TabStop = true;
                b.Enabled = false;
                b.Click += (sender, args) =>
                {
                    SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                    keys.Add("ID", ID);
                    UpdateOneRow(row, keys, b);
                };
                lineO.BtnUpdate = b;
                panel1.Controls.Add(b);

                //Text D
                V6ColorTextBox default_text = new V6ColorTextBox();
                default_text.Name = "txtLangD" + ID;
                default_text.AccessibleName = default_text.Name;
                default_text.Top = top;
                default_text.Left = lblDefault.Left;
                default_text.Width = lblDefault.Width;
                default_text.Text = row["D"].ToString().Trim();
                default_text.GotFocus += (sender, e) =>
                {
                    _currentRowData = row;
                    CopyCurrentRowIdToClipBoard();
                };
                default_text.KeyDown += (sender, args) =>
                {
                    if (args.KeyData == (Keys.Control | Keys.S))
                    {
                        SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                        keys.Add("ID", ID);
                        UpdateOneRow(row, keys, b);
                    }
                };
                default_text.TextChanged += (sender, args) =>
                {
                    row["D"] = default_text.Text;
                    b.Enabled = true;
                };
                lineO.TxtDefault = default_text;
                panel1.Controls.Add(default_text);
                toolTipV6FormControl.SetToolTip(default_text, default_text.Text);


                //Input textBox
                for (int i = 0; i < langs.Length; i++)
                {
                    string lang = langs[i];
                    TextBox txtLang = new TextBox();
                    txtLang.Name = "txtLang" + lang + ID;
                    txtLang.AccessibleName = txtLang.Name;
                    txtLang.Top = top;
                    txtLang.Left = lblDefault.Right + 25 + i * (lblLangWidth + 5);
                    txtLang.Width = lblLangWidth;
                    txtLang.Text = row[lang].ToString().Trim();
                    txtLang.TextChanged += (sender, args) =>
                    {
                        row[lang] = txtLang.Text;
                        b.Enabled = true;
                    };
                    txtLang.KeyDown += (sender, args) =>
                    {
                        if(args.KeyData == (Keys.Control | Keys.S))
                        {
                            SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                            keys.Add("ID", row[ID_FIELD].ToString());
                            UpdateOneRow(row, keys, b);
                        }
                    };
                    txtLang.GotFocus += (sender, e) =>
                    {
                        _currentRowData = row;
                        CopyCurrentRowIdToClipBoard();
                    };
                    //lineO.TxtID = txtID;
                    panel1.Controls.Add(txtLang);
                    //txtLang.BringToFront();
                }

                // Check UseV (CHANGE_V)
                V6CheckBox check_ChangeV = new V6CheckBox();

                check_ChangeV.Top = top;
                check_ChangeV.Left = lblDefault.Right + 5;
                check_ChangeV.TabStop = false;
                check_ChangeV.AutoSize = true;
                check_ChangeV.Text = " ";
                check_ChangeV.Checked = ObjectAndString.ObjectToBool(row["CHANGE_V"]);
                check_ChangeV.GotFocus += (sender, e) =>
                {
                    _currentRowData = row;
                    CopyCurrentRowIdToClipBoard();
                };
                check_ChangeV.KeyDown += (sender, args) =>
                {
                    if (args.KeyData == (Keys.Control | Keys.S))
                    {
                        SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                        keys.Add("ID", ID);
                        UpdateOneRow(row, keys, b);
                    }
                };
                check_ChangeV.CheckedChanged += (sender, args) =>
                {
                    row["CHANGE_V"] = check_ChangeV.Checked ? "1" : "0";
                    b.Enabled = true;
                };
                lineO.ChkChangeV = check_ChangeV;
                panel1.Controls.Add(check_ChangeV);
                toolTipV6FormControl.SetToolTip(check_ChangeV, V6Setting.IsVietnamese ? "Dịch tiếng Việt" : "Translate Vietnamese");
                lineDic[ID] = lineO;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".GenOneLine: " + ex.Message);
            }
        }
        
        private void GenTitleLabels()
        {
            for (int i = 0; i < langs.Length; i++)
            {
                string lang = langs[i];
                Label lblLang = new Label();
                lblLang.Name = "lblLang" + lang;
                lblLang.Top = lblID.Top;
                lblLang.Left = lblDefault.Right + 25 + i * (lblLangWidth + 5);
                lblLang.Width = lblLangWidth;
                lblLang.Text = "Text " + lang;

                panel1.Controls.Add(lblLang);
            }
        }

        private void CopyCurrentRowIdToClipBoard()
        {
            try
            {
                if (chkAutoCopyID.Checked && _currentRowData != null)
                {
                    var id = _currentRowData[ID_FIELD].ToString().Trim();
                    Clipboard.SetText(id);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CopyCurrentRowIdToClipBoard", ex);
            }
        }

        private void RemoveControls()
        {
            for (int i = panel1.Controls.Count-1; i >=0; i--)
            {
                Control c = panel1.Controls[i];
                if(c.Top>20) panel1.Controls.RemoveAt(i);
            }
            foreach (Control control in panel1.Controls)
            {
                if (control.Top > 20) panel1.Controls.Remove(control);
            }
        }

        private void UpdateOneRow(SortedDictionary<string, object> dataDic, SortedDictionary<string, object> keys, Button button)
        {
            try
            {
                var result = V6BusinessHelper.Update(_tableName, dataDic, keys);
                if (result > 0)
                {
                    button.Enabled = false;
                    V6ControlFormHelper.ShowMainMessage(V6Text.Updated + dataDic[ID_FIELD]);
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowMainMessage(V6Text.EditFail + " " + ex.Message);
            }
        }


        private void AddOrEdit(V6Mode mode)
        {
            try
            {
                if (mode != V6Mode.Add && mode != V6Mode.Edit)
                {
                    this.ShowWarningMessage("Mode error!");
                    return;
                }
                
                var SFile = cboSFile.SelectedValue.ToString().Trim();
                var CType = "";
                var ID = "";
                if (_currentRowData != null)
                {
                    if (mode == V6Mode.Add)
                    {
                        CType = _currentRowData["CTYPE"].ToString().Trim();
                        var sql = "Select Max(Right(Rtrim(ID),5)) as max_num From Corplan Where SFile='" + SFile
                                  + "' and ctype='" + CType + "'";
                        var max_num = ObjectAndString.ObjectToInt(SqlConnect.ExecuteScalar(CommandType.Text, sql));

                        ID = SFile + CType + "" + ("00000" + (max_num + 1)).Right(5);
                    }
                    else
                    {
                        ID = _currentRowData["ID"].ToString().Trim().ToUpper();
                    }
                }
                 
                SortedDictionary<string, object> ukey = null;
                SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                if (mode == V6Mode.Add)
                {
                    data.Add("SFILE", SFile);
                    data.Add("CTYPE", CType);
                    data.Add("ID", ID);
                    data.AddRange(_currentRowData);
                }
                else
                {
                    if (_currentRowData == null)
                    {
                        this.ShowWarningMessage("_currentData error!");
                        return;
                    }
                    ukey = new SortedDictionary<string, object>();
                    ukey["ID"] = ID;
                    data.AddRange(_currentRowData);
                }

                FormAddEdit f = new FormAddEdit(V6TableName.CorpLan, mode, ukey, data);
                #region ==== f_InsertSuccess ====
                f.InsertSuccessEvent += dic =>
                {
                    try
                    {
                        GenOneLine(dic, ++_index);
                    }
                    catch (Exception ex)
                    {
                        this.ShowErrorException(GetType() + ".DoAdd Success", ex);
                    }
                };
                #endregion ==== f_InsertSuccess ====
                #region ==== f_UpdateSuccess ====

                f.UpdateSuccessEvent += dic =>
                {
                    try
                    {
                        //Update _currentRowData
                        //row data not updated!!!
                        _currentRowData.AddRange(dic, true);

                        //var cID = dic["ID"].ToString().Trim().ToUpper();
                        var someData = new SortedDictionary<string, object>();
                        for (int i = 0; i < langs.Length; i++)
                        {
                            string lang = langs[i];
                            var txtLang_Name = "txtLang" + lang + ID;
                            someData[txtLang_Name.ToUpper()] = dic[lang];
                        }
                        someData["txtID".ToUpper() + ID] = dic["ID"];
                        someData["txtLangD".ToUpper() + ID] = dic["D"];
                        this.SetSomeData(someData);
                        var btn = V6ControlFormHelper.GetControlByName(this, "btn" + ID);
                        if (btn != null)
                        {
                            btn.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.ShowErrorException(GetType() + ".UpdateSuccessEvent", ex);
                    }
                };
                #endregion

                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoAdd: " + ex.Message, ex.Source);
            }
        }

        private void cboSFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenControls();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddOrEdit(V6Mode.Add);
        }
        
        private void btnEdit_Click(object sender, EventArgs e)
        {
            AddOrEdit(V6Mode.Edit);
        }

    }

    internal class LineObjects
    {
        public TextBox TxtDefault { get; set; }
        public TextBox TxtID { get; set; }
        public V6CheckBox ChkChangeV { get; set; }
        public Button BtnUpdate { get; set; }
    }
}
