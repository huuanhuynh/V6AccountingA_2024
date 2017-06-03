using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
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
    public partial class CorplanControl2 : V6FormControl
    {
        public CorplanControl2()
        {
            InitializeComponent();
            MyInit();
        }

        public CorplanControl2(string itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            if (V6Setting.Language != "V") langs[1] = V6Setting.Language;
            GenControlsThread();
        }

        private void GenControlsThread()
        {
            _currentRowData = null;
            panel1.Enabled = false;
            RemoveControls();
            GenTitleLabels();

            Thread T = new Thread(LoadData);
            T.IsBackground = true;
            T.Start();
            timer1.Start();
        }

        private const string ID_FIELD = "ID";
        string[] langs = { "V", "E" };
        int lblLangWidth = 180;
        private DataTable _data;
        private IDictionary<string, object> _currentRowData;
        private string _tableName = "Corplan2";
        private int _index;
        private bool _dataloaded;
        private bool _genfinish;
        
        private void LoadData()
        {
            try
            {
                _data = V6BusinessHelper.Select(_tableName, "*", "", "", "D,ID").Data;
                _index = 0;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".GenControls: " + ex.Message, ex.Source);
            }
            _dataloaded = true;
        }

        private void GenOneLine(DataRow row, int index)
        {
            try
            {
                var top = index * 23;

                TextBox txtIDs = new TextBox();
                txtIDs.Top = top;
                txtIDs.Left = lblID.Left;
                txtIDs.Width = lblID.Width;
                txtIDs.ReadOnly = true;
                txtIDs.TabStop = false;
                txtIDs.Text = row[ID_FIELD].ToString().Trim();
                txtIDs.GotFocus += (sender, e) =>
                {
                    _currentRowData = row.ToDataDictionary();
                    CopyCurrentRowIdToClipBoard();
                };
                panel1.Controls.Add(txtIDs);

                //Nút update
                var btnLeft = lblDefault.Right + 5 + langs.Length * (lblLangWidth + 5);
                Button b = new Button();
                b.Left = btnLeft;
                b.Top = top;
                b.UseVisualStyleBackColor = true;
                b.Text = "&Update";
                b.TabStop = true;
                b.Enabled = false;
                b.Click += (sender, args) =>
                {
                    SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                    keys.Add("ID", row[ID_FIELD].ToString());
                    UpdateOneRow(row.ToDataDictionary(), keys, b);
                };
                panel1.Controls.Add(b);

                //Text D
                TextBox default_text = new TextBox();
                
                default_text.Top = top;
                default_text.Left = lblDefault.Left;
                default_text.Width = lblDefault.Width;
                default_text.Text = row["D"].ToString().Trim();
                default_text.GotFocus += (sender, e) =>
                {
                    _currentRowData = row.ToDataDictionary();
                    CopyCurrentRowIdToClipBoard();
                };
                default_text.KeyDown += (sender, args) =>
                {
                    if (args.KeyData == (Keys.Control | Keys.S))
                    {
                        SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                        keys.Add("ID", row[ID_FIELD].ToString());
                        UpdateOneRow(row.ToDataDictionary(), keys, b);
                    }
                };
                default_text.TextChanged += (sender, args) =>
                {
                    row["D"] = default_text.Text;
                    b.Enabled = true;
                };
                panel1.Controls.Add(default_text);
                toolTip1.SetToolTip(default_text, default_text.Text);


                for (int i = 0; i < langs.Length; i++)
                {
                    string lang = langs[i];
                    TextBox txtLang = new TextBox();
                    txtLang.Name = "txtLang" + lang;
                    txtLang.AccessibleName = lang;
                    txtLang.Top = top;
                    txtLang.Left = lblDefault.Right + 5 + i * (lblLangWidth + 5);
                    txtLang.Width = lblLangWidth;
                    txtLang.Text = row[lang].ToString().Trim();
                    txtLang.TextChanged += (sender, args) =>
                    {
                        row[lang] = txtLang.Text;
                        b.Enabled = true;
                    };
                    txtLang.KeyDown += (sender, args) =>
                    {
                        if (args.KeyData == (Keys.Control | Keys.S))
                        {
                            SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                            keys.Add("ID", row[ID_FIELD].ToString());
                            UpdateOneRow(row.ToDataDictionary(), keys, b);
                        }
                    };
                    txtLang.GotFocus += (sender, e) =>
                    {
                        _currentRowData = row.ToDataDictionary();
                        CopyCurrentRowIdToClipBoard();
                    };
                    panel1.Controls.Add(txtLang);
                    txtLang.BringToFront();
                }
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
                lblLang.Left = lblDefault.Right + 5 + i * (lblLangWidth + 5);
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
            panel1.VerticalScroll.Value = 0;
            for (int i = panel1.Controls.Count - 1; i >= 0; i--)
            {
                Control c = panel1.Controls[i];
                if (c.Top > 20) panel1.Controls.RemoveAt(i);
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

        private void DoAdd()
        {
            try
            {
                var SFile = "";
                var id = "";
                if (_currentRowData != null)
                {
                    SFile = _currentRowData["sfile"].ToString().Trim();
                    var sql = "Select Max(Right(Rtrim(ID),4)) as max_num From ["
                              + _tableName + "] Where Sfile='" + SFile + "'";
                    var max_num = SqlConnect.ExecuteScalar(CommandType.Text, sql);

                    id = SFile + ("0000" + (ObjectAndString.ObjectToInt(max_num) + 1)).Right(4);
                }

                SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                data.Add("SFILE", SFile);
                data.Add("ID", id);
                FormAddEdit f = new FormAddEdit(V6TableName.CorpLan2, V6Mode.Add, null, data);
                f.InsertSuccessEvent += dic =>
                {
                    _index++;
                    var top = _index * 23 + panel1.AutoScrollPosition.Y;

                    TextBox txtIDs = new TextBox();
                    txtIDs.Top = top;
                    txtIDs.Left = lblID.Left;
                    txtIDs.Width = lblID.Width;
                    txtIDs.ReadOnly = true;
                    txtIDs.TabStop = false;
                    txtIDs.Text = dic[ID_FIELD].ToString().Trim();
                    panel1.Controls.Add(txtIDs);

                    //TextBox lbl_default_text = new TextBox();
                    //lbl_default_text.AutoSize = true;
                    //lbl_default_text.Top = top;
                    //lbl_default_text.Left = lblDefault.Left;
                    //lbl_default_text.Text = dic["D"].ToString().Trim();
                    //panel1.Controls.Add(lbl_default_text);

                    //Nút update
                    var btnLeft = lblDefault.Right + 5 + langs.Length * (lblLangWidth + 5);
                    Button b = new Button();
                    b.Left = btnLeft;
                    b.Top = top;
                    b.UseVisualStyleBackColor = true;
                    b.Text = "&Update";
                    b.TabStop = true;
                    b.Enabled = false;
                    b.Click += (sender, args) =>
                    {
                        SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                        keys.Add("ID", dic[ID_FIELD].ToString());
                        UpdateOneRow(dic, keys, b);
                    };
                    panel1.Controls.Add(b);


                    //Text D
                    TextBox default_text = new TextBox();

                    default_text.Top = top;
                    default_text.Left = lblDefault.Left;
                    default_text.Width = lblDefault.Width;
                    default_text.Text = dic["D"].ToString().Trim();
                    default_text.GotFocus += (sender, e) =>
                    {
                        _currentRowData = dic;
                        CopyCurrentRowIdToClipBoard();
                    };
                    default_text.KeyDown += (sender, args) =>
                    {
                        if (args.KeyData == (Keys.Control | Keys.S))
                        {
                            SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                            keys.Add("ID", dic[ID_FIELD].ToString());
                            UpdateOneRow(dic, keys, b);
                        }
                    };
                    default_text.TextChanged += (sender, args) =>
                    {
                        dic["D"] = default_text.Text;
                        b.Enabled = true;
                    };
                    panel1.Controls.Add(default_text);
                    toolTip1.SetToolTip(default_text, default_text.Text);

                    for (int i = 0; i < langs.Length; i++)
                    {
                        string lang = langs[i];
                        TextBox txtLang = new TextBox();
                        txtLang.Name = "txtLang" + lang;
                        txtLang.AccessibleName = lang;
                        txtLang.Top = top;
                        txtLang.Left = lblDefault.Right + 5 + i * (lblLangWidth + 5);
                        txtLang.Width = lblLangWidth;
                        txtLang.Text = dic[lang].ToString().Trim();
                        txtLang.GotFocus += (sender, e) =>
                        {
                            _currentRowData = dic;
                            CopyCurrentRowIdToClipBoard();
                        };
                        txtLang.TextChanged += (sender, args) =>
                        {
                            dic[lang] = txtLang.Text;
                            b.Enabled = true;
                        };
                        txtLang.KeyDown += (sender, args) =>
                        {
                            if (args.KeyData == (Keys.Control | Keys.S))
                            {
                                SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                                keys.Add("ID", dic[ID_FIELD].ToString());
                                UpdateOneRow(dic, keys, b);
                            }
                        };
                        panel1.Controls.Add(txtLang);
                    }
                };

                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoAdd: " + ex.Message, ex.Source);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DoAdd();
        }


        private string loading_text = "        LOADING";

        private void Loading()
        {
            for (int i = 1; i < loading_text.Length; i++)
            {
                char c0 = loading_text[i-1];
                char c1 = loading_text[i];
                if (c0 == ' ' && c1 != ' ')
                {
                    loading_text = loading_text.Substring(0, i - 1) + c1 + c0 + loading_text.Substring(i + 1);
                    return;
                }
            }
            loading_text = "        LOADING";
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Loading();
            V6ControlFormHelper.SetStatusText(loading_text);
            if (_genfinish)
            {
                timer1.Stop();
                panel1.Enabled = true;
                V6ControlFormHelper.SetStatusText("Ready.");
            }
            else if (_dataloaded)
            {
                timer1.Interval = 10;
                if (_data == null)
                {
                    timer1.Stop();
                    _genfinish = true;
                    V6ControlFormHelper.SetStatusText("Load error.");
                    return;
                }
                var row = _data.Rows[_index];
                _index++;
                if (_index == _data.Rows.Count) _genfinish = true;
                GenOneLine(row, _index);
            }
        }

    }
}
