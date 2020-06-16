using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AAPPR_XULY_ALL_F4 : V6FormControl
    {
        #region Biến toàn cục

        private string _tableName_AM;
        protected DataRow _am;
        protected string _text;
        /// <summary>
        /// FIELD1:Label1,FIELD2....
        /// </summary>
        protected string _fields;
        //protected string _reportFileF5, _reportTitleF5, _reportTitle2F5;
        public event HandleResultData UpdateSuccessEvent;

        protected DataSet _ds;
        protected DataTable _tbl, _tbl2;
        //private V6TableStruct _tStruct;
        /// <summary>
        /// Dùng cho procedure chính (program?)
        /// </summary>
        protected List<SqlParameter> _pList;

        public bool ViewDetail { get; set; }
        
        
        #endregion 

        #region ==== Properties ====

        public string So_ctx
        {
            get { return txtSoCtXuat.Text; }
            set { txtSoCtXuat.Text = value; }
        }
        #endregion properties
        public AAPPR_XULY_ALL_F4()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stt_rec"></param>
        /// <param name="am"></param>
        /// <param name="tableName_AM">Tên bảng dữ liệu sẽ được cập nhập (update).</param>
        /// <param name="fields">FIELD1:Label1:vvar:checkonleave:allwayupdate,FIELD2</param>
        public AAPPR_XULY_ALL_F4(string stt_rec, DataRow am, string tableName_AM, string fields = null)
        {
            _sttRec = stt_rec;
            _am = am;
            _tableName_AM = tableName_AM;
            _fields = fields;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                CreateFormControls();
                V6ControlFormHelper.SetFormDataRow(this, _am);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }


        /// <summary>
        /// FIELD:labelText
        /// </summary>
        public Dictionary<string, string> _fieldDic = new Dictionary<string, string>();
        /// <summary>
        /// Danh sách field luôn update dù dữ liệu rỗng.
        /// </summary>
        public Dictionary<string, string> _allwayUpdate = new Dictionary<string, string>();
        private void CreateFormControls()
        {
            try
            {
                // Phân tích danh sách Field
                string[] sss = ObjectAndString.SplitString(_fields);

                int top = txtDienGiai.Top;
                foreach (string s in sss)
                {
                    string[] ss = s.Split(':');
                    if (ss[0].Trim().Length > 0)
                    {
                        string field = ss[0];
                        string label = ss[0];
                        string vVar = "";
                        bool checkOnLeave = false;
                        if (ss.Length > 1)
                        {
                            label = ss[1];
                        }
                        if (ss.Length > 2)
                        {
                            vVar = ss[2];
                        }
                        if (ss.Length > 3)
                        {
                            checkOnLeave = "1" == ss[3];
                        }
                        if (ss.Length > 4)
                        {
                            if ("1" == ss[4]) _allwayUpdate.Add(field, label);
                        }
                        _fieldDic.Add(field, label);

                        // Tạo input cùng label
                        //Kiem tra
                        Control c = this.GetControlByAccessibleName(field);
                        if (c != null)
                        {
                            V6ControlFormHelper.SetControlReadOnly(c, false);
                            continue;
                        }

                        top += 25;
                        V6VvarTextBox txt = new V6VvarTextBox()
                        {
                            AccessibleName = field,
                            BorderStyle = BorderStyle.FixedSingle,
                            Name = "txt" + field,
                            Top = top,
                            Left = txtDienGiai.Left,
                            Width = txtDienGiai.Width,
                            VVar = vVar,
                            F2 = !string.IsNullOrEmpty(vVar),
                            CheckOnLeave = checkOnLeave,
                        };
                        V6Label lbl = new V6Label()
                        {
                            Name = "lbl" + field,
                            Text = label,
                            Top = top,
                            Left = lblDienGiai.Left,
                        };
                        this.Controls.Add(txt);
                        this.Controls.Add(lbl);
                        this.Height += 25;
                    }
                }

                foreach (KeyValuePair<string, string> item in _fieldDic)
                {

                }

            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateFormControls", ex);
            }
        }
        
        private void FormBaoCaoHangTonTheoKho_Load(object sender, EventArgs e)
        {
            //SetStatus2Text();
        }

        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            try
            {
                var am = new SortedDictionary<string, object>();
                var form_data = GetData();
                foreach (KeyValuePair<string, string> item in _fieldDic)
                {
                    string FIELD = item.Key.ToUpper();
                    if (form_data.ContainsKey(FIELD) && (form_data[FIELD].ToString().Length > 0 || _allwayUpdate.ContainsKey(FIELD)))
                    {
                        am[FIELD] = form_data[FIELD];
                    }
                }
                SortedDictionary<string, object> keys
                    = new SortedDictionary<string, object> {{"Stt_rec", _sttRec}};

                var result = V6BusinessHelper.UpdateSimple(_tableName_AM, am, keys);
                if (result == 1)
                {
                    OnUpdateSuccessEvent(am);
                    Dispose();
                }
                else if (result == -2)
                {
                    this.ShowWarningMessage(string.Format("{0} {1}.", V6Text.UpdateFail, V6Text.CheckInfor));
                }
                else
                {
                    this.ShowWarningMessage(string.Format("{0} result({1}).", V6Text.UpdateFail, result));
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Update error:\n" + ex.Message);
            }
        }
        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnHuy.PerformClick();
            }
            else if (keyData == (Keys.Control | Keys.Enter))
            {
                btnNhan.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected int _oldIndex = -1;

        protected virtual void OnUpdateSuccessEvent(IDictionary<string, object> datadic)
        {
            var handler = UpdateSuccessEvent;
            if (handler != null) handler(datadic);
        }
    }
}
