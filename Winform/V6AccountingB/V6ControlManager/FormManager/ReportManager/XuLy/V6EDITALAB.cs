using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class V6EDITALAB : XuLyBase0
    {
        public V6EDITALAB()
        {
            InitializeComponent();
        }

        public V6EDITALAB(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                LoadListALIMXLS();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".V6EDITALAB Init: " + ex.Message);
            }
        }

        private DataTable listAl;
        private string _table_name;
        private V6TableStruct _tableStruct;
        

        private DataRow SelectedRow
        {
            get
            {
                if (cboDanhMuc.DataSource != null && cboDanhMuc.SelectedItem is DataRowView && cboDanhMuc.SelectedIndex >= 0)
                {
                    return ((DataRowView)cboDanhMuc.SelectedItem).Row;
                }
                return null;
            }
        }
        /// <summary>
        /// Danh sách các trường được edit.
        /// </summary>
        private string ADV_AL1
        {
            get
            {
                var result = "";
                if (SelectedRow != null)
                {
                    result = SelectedRow["ADV_AL1"].ToString().Trim();
                }
                return result;
            }
        }

        private string EDIT_YN
        {
            get
            {
                var result = "";
                if (SelectedRow != null)
                {
                    result = SelectedRow["EDIT_YN"].ToString().Trim();
                }
                return result;
            }
        }
        
        private string NOEDIT_F
        {
            get
            {
                var result = "";
                if (SelectedRow != null)
                {
                    result = SelectedRow["NOEDIT_F"].ToString().Trim();
                }
                return result;
            }
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(".");
        }

        private void LoadListALIMXLS()
        {
            listAl = V6BusinessHelper.Select("ALIMXLS", "*", "EDIT_YN IN ('1','2')", "", "stt").Data;

            cboDanhMuc.ValueMember = "dbf_im";
            cboDanhMuc.DisplayMember = V6Setting.IsVietnamese ? "Ten" : "Ten2";
            cboDanhMuc.DataSource = listAl;
            cboDanhMuc.ValueMember = "dbf_im";
            cboDanhMuc.DisplayMember = V6Setting.IsVietnamese ? "Ten" : "Ten2";
        }

        private void ResetVar()
        {
            _tableStruct = V6BusinessHelper.GetTableStruct(_table_name);
            
            ResetFilterLine();
            ResetUpdateFields();

        }

        private void ResetFilterLine()
        {
            panelFilter1.Controls.Clear();
            panelFilter1.AddMultiFilterLine(_tableStruct, ADV_AL1);
        }

        private void ResetUpdateFields()
        {
            var noedit_fields = ObjectAndString.SplitString(NOEDIT_F.ToUpper());
            List<string> edit_fields = new List<string>();
            foreach (KeyValuePair<string, V6ColumnStruct> item in _tableStruct)
            {
                var FIELD = item.Value.ColumnName.ToUpper();
                if (!noedit_fields.Contains(FIELD))
                {
                    edit_fields.Add(item.Value.ColumnName);
                }
            }
            cboUpdateFields.DataSource = edit_fields;
        }

        private string GenQuery()
        {
            try
            {
                string sql = "";
                bool and = radAnd.Checked;
                var where = panelFilter1.GetQueryString(_tableStruct, null, and);
                var test_where = "";
                if (where.Length > 0)
                {
                    where = "where (" + where + ")";
                    test_where = where + " and 1=0";
                }
                else
                {
                    where = "where 1=0";
                    test_where = where;
                }

                if (radEditMode.Checked)
                {
                    string SelectedUpdateField = cboUpdateFields.SelectedItem.ToString().ToUpper();
                    var columnStruct = _tableStruct[SelectedUpdateField];
                    var stringValue = SqlGenerator.GenSqlStringValue(txtValue.Text,
                        columnStruct.sql_data_type_string, columnStruct.ColumnDefault, false, columnStruct.MaxLength);
                    
                    sql = string.Format("UPDATE {0} SET [{1}] = {2} {3}",
                        _table_name, SelectedUpdateField, stringValue, where);
                    _test_query = string.Format("UPDATE {0} SET [{1}] = {2} {3}",
                        _table_name, SelectedUpdateField, stringValue, test_where);

                }
                else if (radDeleteMode.Checked)
                {
                    if (EDIT_YN == "2")
                    {
                        sql = string.Format("DELETE {0} {1}", _table_name, where);
                        _test_query = string.Format("DELETE {0} {1}", _table_name, test_where);
                    }
                    else
                    {
                        sql = "not_allowed";
                        _test_query = sql;
                    }
                }
                else
                {
                    sql = "no_mode_selected";
                    _test_query = sql;
                }
                return sql;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".GenQuery", ex);
            }
            return null;
        }

        private string _run_query = "", _test_query = "";
        protected override void Nhan()
        {
            try
            {
                if (_executing)
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.Executing);
                    return;
                }

                _run_query = "";
                _run_query = GenQuery();
                richTextBox1.Text = _run_query;
                
                Control.CheckForIllegalCrossThreadCalls = false;
                Thread tRunAll = new Thread(RunAll);
                tRunAll.IsBackground = true;
                tRunAll.Start();

                Timer timerRunAll = new Timer();
                timerRunAll.Interval = 500;
                timerRunAll.Tick += timerRunAll_Tick;
                _executesuccess = false;
                _executing = true;
                timerRunAll.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Nhan: " + ex.Message);
            }
        }
        private void timerRunAll_Tick(object sender, EventArgs e)
        {
            if (_executesuccess)
            {
                ((Timer)sender).Stop();
                btnNhan.Image = btnNhanImage;
                try
                {
                    DoAfterExecuteSuccess();
                    V6ControlFormHelper.ShowMainMessage(V6Text.Finish + "\r\n" + _message);
                    _executesuccess = false;
                }
                catch (Exception ex)
                {
                    ((Timer)sender).Stop();
                    _executesuccess = false;
                    this.ShowErrorMessage(GetType() + ".TimerView" + ex.Message, ex.Source);
                }
            }
            else if (_executing)
            {
                btnNhan.Image = waitingImages.Images[ii];
                ii++;
                if (ii >= waitingImages.Images.Count) ii = 0;
            }
            else
            {
                ((Timer)sender).Stop();
                btnNhan.Image = btnNhanImage;
                this.ShowErrorMessage(_error);
            }
        }

        private string _error = "";
        private void RunAll()
        {
            try
            {
                _message = "";

                try
                {
                    V6BusinessHelper.ExecuteSqlNoneQuery(_test_query);
                }
                catch (Exception ex)
                {
                    _error = _message + " test query error: " + ex.Message;
                    _executesuccess = false;
                    _executing = false;
                    return;
                }
                
                int result = V6BusinessHelper.ExecuteSqlNoneQuery(_run_query);

                _message = result + "row(s)";
                _executing = false;
                _executesuccess = true;
            }
            catch (Exception ex)
            {
                _error = _message + " query error: " + ex.Message;
                _executesuccess = false;
                _executing = false;
            }
        }

        private void cboDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (V6Setting.IsDesignTime) return;
            try
            {
                _table_name = cboDanhMuc.SelectedValue.ToString().Trim();
                ResetVar();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = GenQuery();
        }


        
    }
}
