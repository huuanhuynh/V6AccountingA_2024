﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6ReportControls;

namespace V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuXuatKho
{
    public partial class TinhHaoHutDataForm : V6Form
    {
        public TinhHaoHutDataForm()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            TxtVttonkho.Text = "1";
            txtKieuIn.Text = "*";
            chkSoluong.Checked = false;
            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);
            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

        }

        private DataSet _ds;
        private DataTable data;
        public event DataTableHandler AcceptData;
        public string CheckFields = null;
        private List<SqlParameter> plist;

        protected virtual void OnAcceptData(DataTable table)
        {
            var handler = AcceptData;
            if (handler != null) handler(table);
        }

        private void Nhan()
        {
            if (data == null)
            {
                this.ShowMessage(V6Text.NoData);
                return;
            }
            Close();
            OnAcceptData(data);
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

            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;

            var result = new List<SqlParameter>();

            


            var and = radAnd.Checked;

            var cKey = "";
            var cKey_SD = "";

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

            cKey_SD += cKey;

            int tinh_dc = 0;
            if (Chk_Tinh_dc.Checked) tinh_dc = 1;

            result.Add(new SqlParameter("@StartDate", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@EndDate", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@Tinh_dc", tinh_dc));
            result.Add(new SqlParameter("@Condition", cKey));
            result.Add(new SqlParameter("@Vttonkho", TxtVttonkho.Text.Trim()));
            result.Add(new SqlParameter("@ConditionSD", cKey_SD));
            result.Add(new SqlParameter("@Kieu_in", txtKieuIn.Text.Trim()));
            result.Add(new SqlParameter("@SL_GT", chkSoluong.Checked ? "1" : "2"));
            

            return result;
        }

        void LoadDataThread()
        {
            plist = GetFilterParameters();

            _executesuccess = false;

            var tLoadData = new Thread(LoadData);
            CheckForIllegalCrossThreadCalls = false;
            tLoadData.Start();
            timerViewReport.Start();

        }

        void LoadData()
        {
            try
            {
                _executing = true;
                _executesuccess = false;
                _ds = V6BusinessHelper.ExecuteProcedure("AINHHVTALL", plist.ToArray());
                if (_ds.Tables.Count > 0)
                {
                    data = _ds.Tables[0];
                    _executesuccess = true;
                }
                else
                {
                    data = null;
                    _executesuccess = false;
                }
            }
            catch (Exception ex)
            {
                _message = V6Text.Text("QUERY_FAILED") + "\n";
                if (ex.Message.StartsWith("Could not find stored procedure")) _message += V6Text.NotExist + ex.Message.Substring(31);
                else _message += ex.Message;

                _ds = null;
                data = null;
                _executesuccess = false;
            }
            _executing = false;
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
            if (_executesuccess)
            {
                timerViewReport.Stop();
                btnLoc.Image = btnNhanImage;
                ii = 0;

                try
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = data;

                    AldmConfig config = ConfigManager.GetAldmConfig("IXA_AINHHVTALL");
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, config.GRDS_V1, config.GRDF_V1, V6Setting.IsVietnamese ? config.GRDHV_V1 : config.GRDHE_V1);
                    dataGridView1.Focus();
                }
                catch (Exception ex)
                {
                    timerViewReport.Stop();
                    _executesuccess = false;
                    V6Message.Show(ex.Message);
                }
            }
            else if (_executing)
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

        
    }
}
