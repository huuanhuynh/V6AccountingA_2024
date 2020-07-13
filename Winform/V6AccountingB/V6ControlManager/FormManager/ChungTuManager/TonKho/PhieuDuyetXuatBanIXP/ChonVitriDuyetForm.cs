using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Controls;
using V6Controls.Forms;
using V6Init;
using V6ReportControls;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuDuyetXuatBanIXP
{
    public partial class ChonVitriDuyetForm : V6Form
    {
        private string _madm = "IXP_AINCDVITRI3ALL";
        private AldmConfig _aldmConfig = null;
        public ChonVitriDuyetForm()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            TxtVttonkho.Text = "1";
            txtKieuIn.Text = "*";
            chkSoluong.Checked = true;
            _aldmConfig = ConfigManager.GetAldmConfig(_madm);
            gridViewSummary1.NoSumColumns = "";
            gridViewSummary1.SumCondition = new Condition()
            {
                FIELD = "LOAI",
                OPER = "=",
                VALUE = "9"
            };
            LoadDefaultData(4, "", _madm, "", "");
        }

        public DataSet _ds;
        public DataTable _data;
        public event ChonAcceptSelectDataList AcceptSelectEvent;
        public string CheckFields = null;
        private List<SqlParameter> plist;

        protected virtual void OnAcceptSelectEvent(List<IDictionary<string, object>> selecteddatalist, ChonEventArgs e)
        {
            var handler = AcceptSelectEvent;
            if (handler != null) handler(selecteddatalist, e);
        }

        private void Nhan()
        {
            try
            {
                if (_data == null)
                {
                    this.ShowMessage(V6Text.NoData);
                    return;
                }
                //var data = dataGridView1.GetSelectedData();
                var data = GetSelectedData();
                if (data.Count > 0)
                {
                    ChonEventArgs e = new ChonEventArgs()
                    {
                        Loai_ct = null
                    };
                    OnAcceptSelectEvent(data, e);
                    Close();
                }
                else
                {
                    this.ShowWarningMessage(V6Setting.IsVietnamese
                        ? "Chưa chọn dòng nào!\nDùng phím cách(space) hoặc Ctrl+A để chọn, Ctrl+U để bỏ chọn hết!"
                        : "No e selected!\nPlease use SpaceBar or Ctrl+A, Ctrl+U for unselect all!");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Nhan", ex);
            }
        }

        private List<IDictionary<string, object>> GetSelectedData()
        {
            List<IDictionary<string, object>> result = new List<IDictionary<string, object>>();
            foreach (DataRow row in _data.Rows)
            {
                if ((string)row["Tag"] == "x") result.Add(row.ToDataDictionary());
            }
            return result;
        }

        private void Huy()
        {
            Close();
        }

        private string GetFilterStringByFieldsR(Control control, List<string> fields, bool and)
        {
            var result = "";
            var line = control as FilterLineBase;
            if (line != null && line.IsSelected && fields.Contains(line.FieldName.ToUpper()))
            {
                result += (and ? "\nand " : "\nor  ") + line.Query;
            }
            if (control.Controls.Count > 0)
            {
                foreach (Control c in control.Controls)
                {
                    result += GetFilterStringByFieldsR(c, fields, and);
                }
            }
            return result;
        }

        public virtual string GetFilterStringByFields(List<string> fields, bool and)
        {
            var result = GetFilterStringByFieldsR(this, fields, and);
            if (result.Length > 4) result = result.Substring(4);
            return result;
        }

        public List<SqlParameter> GetFilterParameters()//Copy sửa lại từ AINCDHSDC
        {
            if (TxtMakho.StringValue != "")
            {
                V6Setting.M_Ma_kho = TxtMakho.StringValue;
            }

            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;

            var result = new List<SqlParameter>();

            


            var and = radAnd.Checked;

            var cKey = "";

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_KHO","MA_VT","MA_VITRI","MA_LO"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
                "NH_VT1","NH_VT2","NH_VT3","NH_VT4","NH_VT5","NH_VT6"
            }, and);

            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey += string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey += string.Format("(1=2 OR {0})", key0);
                }
            }
            else
            {
                cKey = "1=1";
            }

            if (!string.IsNullOrEmpty(key1))
            {
                cKey = cKey + string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key1);
            }
            
            result.Add(new SqlParameter("@EndDate", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@Condition", cKey));
            result.Add(new SqlParameter("@Vttonkho", TxtVttonkho.Text.Trim()));
            result.Add(new SqlParameter("@Kieu_in", txtKieuIn.Text.Trim()));
            result.Add(new SqlParameter("@HSD_YN", chkTonHSD.Checked ? "1" : "0"));
            result.Add(new SqlParameter("@SL_GT", chkSoluong.Checked ? "1" : "2"));
            

            return result;
        }

        void LoadDataThread()
        {
            plist = GetFilterParameters();

            _dataloaded = false;

            var tLoadData = new Thread(LoadData);
            CheckForIllegalCrossThreadCalls = false;
            tLoadData.Start();
            timerViewReport.Start();

        }

        void LoadData()
        {
            try
            {
                _dataloading = true;
                _dataloaded = false;
                _ds = V6BusinessHelper.ExecuteProcedure("AINCDVITRI3ALL", plist.ToArray());
                if (_ds.Tables.Count > 0)
                {
                    _data = _ds.Tables[0];
                    _dataloaded = true;
                }
                else
                {
                    _data = null;
                    _dataloaded = false;
                }
            }
            catch (Exception ex)
            {
                _message = V6Text.Text("QUERY_FAILED") + "\n";
                if (ex.Message.StartsWith("Could not find stored procedure")) _message += V6Text.NotExist + ex.Message.Substring(31);
                else _message += ex.Message;

                _ds = null;
                _data = null;
                _dataloaded = false;
            }
            _dataloading = false;
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            try
            {
                btnNhanImage = Properties.Resources.Search;
                LoadDataThread();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, ex.Message));
            }
        }

        private bool CheckDataFields(DataTable dataTable)
        {
            var columns = CheckFields.Split(',');
            foreach (string column in columns)
            {
                if (!dataTable.Columns.Contains(column))
                {
                    return false;
                }
            }
            return true;
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            Nhan();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Huy();
        }

        private void timerViewReport_Tick(object sender, EventArgs e)
        {
            if (_dataloaded)
            {
                timerViewReport.Stop();
                btnLoc.Image = btnNhanImage;
                ii = 0;

                try
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = _data;

                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, _aldmConfig.GRDS_V1, _aldmConfig.GRDF_V1, V6Setting.IsVietnamese ? _aldmConfig.GRDHV_V1 : _aldmConfig.GRDHE_V1);
                    var conditionColor = ObjectAndString.StringToColor(_aldmConfig.COLORV);
                    V6ControlFormHelper.FormatGridView(dataGridView1, _aldmConfig.FIELDV, _aldmConfig.OPERV, _aldmConfig.VALUEV,
                        _aldmConfig.BOLD_YN, _aldmConfig.COLOR_YN, conditionColor);
                    dataGridView1.Focus();
                }
                catch (Exception ex)
                {
                    timerViewReport.Stop();
                    _dataloaded = false;
                    V6Message.Show(ex.Message);
                }
            }
            else if (_dataloading)
            {
                btnLoc.Image = waitingImages.Images[ii++];
                if (ii >= waitingImages.Images.Count) ii = 0;
            }
            else
            {
                timerViewReport.Stop();
                btnLoc.Image = btnNhanImage;
                this.ShowErrorMessage(_message);
            }
        }

        private void dataGridView1_RowSelectChanged(object sender, SelectRowEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control) return;
            try
            {
                string COLUMN = dataGridView1.CurrentCell.OwningColumn.DataPropertyName.ToUpper();
                if (COLUMN == "MA_KHO")
                {
                    bool isSelect = e.Row.IsSelect();
                    string ma_kho0 = ("" + e.Row.Cells["MA_KHO"].Value).Trim();
                    string code0 = ma_kho0;
                    foreach (DataGridViewRow row1 in e.Row.DataGridView.Rows)
                    {
                        if (row1 == e.Row) continue;
                        string ma_kho = ("" + row1.Cells["MA_KHO"].Value).Trim();
                        string code = ma_kho;
                        if (code0 == code) row1.Select(isSelect);
                    }
                }
                else if (COLUMN == "MA_LO")
                {
                    bool isSelect = e.Row.IsSelect();
                    string ma_lo0 = ("" + e.Row.Cells["MA_LO"].Value).Trim();
                    string code0 = ma_lo0;
                    foreach (DataGridViewRow row1 in e.Row.DataGridView.Rows)
                    {
                        if (row1 == e.Row) continue;
                        string ma_lo = ("" + row1.Cells["MA_LO"].Value).Trim();
                        string code = ma_lo;
                        if (code0 == code) row1.Select(isSelect);
                    }
                }
                else if (COLUMN == "MA_VT")
                {
                    bool isSelect = e.Row.IsSelect();
                    string ma_vt0 = ("" + e.Row.Cells["MA_VT"].Value).Trim();
                    string code0 = ma_vt0;
                    foreach (DataGridViewRow row1 in e.Row.DataGridView.Rows)
                    {
                        if (row1 == e.Row) continue;
                        string ma_vt = ("" + row1.Cells["MA_VT"].Value).Trim();
                        string code = ma_vt;
                        if (code0 == code) row1.Select(isSelect);
                    }
                }
                else
                {
                    bool isSelect = e.Row.IsSelect();
                    string ma_vt0 = ("" + e.Row.Cells["MA_VT"].Value).Trim();
                    string ma_kho0 = ("" + e.Row.Cells["MA_KHO"].Value).Trim();
                    string code0 = ma_vt0 + ma_kho0;
                    foreach (DataGridViewRow row1 in e.Row.DataGridView.Rows)
                    {
                        if (row1 == e.Row) continue;
                        string ma_vt = ("" + row1.Cells["MA_VT"].Value).Trim();
                        string code = ma_vt;
                        if (ma_kho0 != "")
                        {
                            string ma_kho = ("" + row1.Cells["MA_KHO"].Value).Trim();
                            code += ma_kho;
                        }
                        if (code0 == code) row1.Select(isSelect);
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView1_RowSelectChanged", ex);
            }
        }

        
    }
}
