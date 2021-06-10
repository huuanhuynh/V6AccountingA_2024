using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.Viewer;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AGLCTKC_F4 : V6FormControl
    {
        #region Biến toàn cục

        protected DataRow _am;
        protected string _numlist, _text, _reportProcedure;
        protected int _year;

        //protected string _reportFileF5, _reportTitleF5, _reportTitle2F5;
        public delegate void HandleF4Success();

        public event HandleF4Success UpdateSuccessEvent;

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

       
        #endregion properties
        public AGLCTKC_F4()
        {
            InitializeComponent();
        }

        public AGLCTKC_F4(string numlist,int year,string reportProcedure)
        {
            _numlist = numlist;
            _year = year;
            _reportProcedure = reportProcedure;

            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                txtNam.Value = _year;
                txtKy1.Value = V6Setting.M_SV_DATE.Month;
                txtKy2.Value = V6Setting.M_SV_DATE.Month;
                int one = 1, zero = 0;
                SqlParameter[] plist =
                {
                    new SqlParameter("@Year", (int)txtNam.Value),
                    new SqlParameter("@Month", (int)txtKy1.Value),
                    new SqlParameter("@Day01", one),
                };
                object result = V6BusinessHelper.ExecuteFunctionScalar("VFV_YearMonthToDate", plist);
                if (result is DateTime)
                {
                    ngay1.Value = (DateTime)result;
                }
                SqlParameter[] plist2 =
                {
                    new SqlParameter("@Year", (int)txtNam.Value),
                    new SqlParameter("@Month", (int)txtKy2.Value),
                    new SqlParameter("@Day01", zero),
                };
                object result2 = V6BusinessHelper.ExecuteFunctionScalar("VFV_YearMonthToDate", plist2);
                if (result2 is DateTime)
                {
                    ngay2.Value = (DateTime)result2;
                }

                txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
                if (V6Login.MadvcsCount <= 1){
                    txtMaDvcs.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        
        
        private void FormBaoCaoHangTonTheoKho_Load(object sender, EventArgs e)
        {
            //SetStatus2Text();
        }

        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            if (_executing)
            {
                return;
            }

            try
            {
                int check = V6BusinessHelper.CheckDataLocked("2", V6Setting.M_SV_DATE, (int)txtKy2.Value, (int)txtNam.Value);
                if (check == 1)
                {
                    this.ShowWarningMessage(V6Text.CheckLock);
                    return;
                }
                _executing = true;
                _executing_success = false;
                CheckForIllegalCrossThreadCalls = false;
                V6BusinessHelper.WriteV6UserLog(ItemID, GetType() + "." + MethodBase.GetCurrentMethod().Name,
                    string.Format("reportProcedure:{0} {1}", _reportProcedure, "paramss"));
                var tLoadData = new Thread(Executing);
                tLoadData.Start();
                timerViewReport.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnNhan_Click", ex);
            }
        }

        
        List<string> lstVV = new List<string>();
        List<string> lstVVNOT = new List<string>();
        List<string> lstTI_LE_VV = new List<string>();
        List<string> lstTI_LE_VVNOT = new List<string>();
        private void Executing()
        {
            try
            {
                List<SqlParameter> plist = new List<SqlParameter>()
                {
                    new SqlParameter("@Type", "NEW"),
                    new SqlParameter("@Year", txtNam.Value),
                    new SqlParameter("@Period1", txtKy1.Value),
                    new SqlParameter("@Period2", txtKy2.Value),
                    new SqlParameter("@NumList", _numlist),
                    new SqlParameter("@User_id", V6Login.UserId),
                    new SqlParameter("@Ma_dvcs", txtMaDvcs.StringValue),
                    new SqlParameter("@ngay1", ngay1.Value.Date),
                    new SqlParameter("@ngay2", ngay2.Value.Date),
                    
                    // thêm 42 biến.
                    // ngay1 ngay2 lst_vv01-10 lst_hsvv01-10 ... lst_vvnot01  lst_hsvvnot01

                };

                Dictionary<string, List<string>> _4NAME = new Dictionary<string, List<string>>
                {
                    {"LST_VV", lstVV},
                    {"LST_HSVV", lstTI_LE_VV},
                    {"LST_VVNOT", lstVVNOT},
                    {"LST_HSVVNOT", lstTI_LE_VVNOT}
                };
                foreach (KeyValuePair<string, List<string>> item in _4NAME)
                {
                    for (int i = 1; i < 10; i++)
                    {
                        string NAME = item.Key + i.ToString("00");
                        if (item.Value.Count >= i)
                        {
                            plist.Add(new SqlParameter("@" + NAME, item.Value[i]));
                        }
                        else
                        {
                            plist.Add(new SqlParameter("@" + NAME, ""));
                        }
                    }
                }
                
                int result = V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure, plist.ToArray());
                _executing_success = result > 0;
            }
            catch (Exception ex)
            {
                _message = ex.Message;
                _executing_success = false;
                this.WriteExLog(GetType() + ".Executing!", ex);
            }
            _executing = false;
        }

        private void timerViewReport_Tick(object sender, EventArgs e)
        {
            if (_executing)
            {
                btnNhan.Image = waitingImages.Images[ii++];
                if (ii >= waitingImages.Images.Count) ii = 0;
            }
            else
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
                if (_executing_success)
                {
                    OnUpdateSuccessEvent();
                    Dispose();
                }
                else
                {
                    timerViewReport.Stop();
                    ShowMainMessage(_message);
                }
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
        private bool _executing, _executing_success;

        protected virtual void OnUpdateSuccessEvent()
        {
            var handler = UpdateSuccessEvent;
            if (handler != null) handler();
        }

        private void v6NumberTextBox1_V6LostFocus(object sender)
        {
            if (txtTyLeVV.Value >= 1000)
            {
                txtTyLeVV.Value = 999 + (txtTyLeVV.Value-(int)txtTyLeVV.Value);
            }
        }

        private void btnChonVV_Click(object sender, EventArgs e)
        {
            try
            {
                // Chạy proc
                // lên from chọn gán tất cả = tỷ lệ, khi chọn tăng lên 100.
                SqlParameter[] plist =
                {
                    new SqlParameter("@Year", txtNam.Value),
                    new SqlParameter("@Period1", txtKy1.Value),
                    new SqlParameter("@Period2", txtKy2.Value),
                    new SqlParameter("@User_id", V6Login.UserId),
                    new SqlParameter("@Ma_dvcs", txtMaDvcs.StringValue),
                    new SqlParameter("@ngay1", ngay1.Value.Date),
                    new SqlParameter("@ngay2", ngay2.Value.Date),
                };
                var data = V6BusinessHelper.ExecuteProcedure("VPA_AGLKC_GetSDVV", plist).Tables[0];
                V6ControlFormHelper.UpdateDKlist(data, "TI_LE_VV", txtTyLeVV.Value);
                DataSelectorTagForm selectForm = new DataSelectorTagForm(data);
                if (selectForm.ShowDialog(this) == DialogResult.OK)
                {
                    string strMA_VV = "", strTI_LE_VV = "", strMA_VV_NOT = "", strTI_LE_VV_NOT = "";
                    lstVV = new List<string>();
                    lstVVNOT = new List<string>();
                    lstTI_LE_VV = new List<string>();
                    lstTI_LE_VVNOT = new List<string>();
                    //_selectLST = new SortedDictionary<string, string>();
                    foreach (DataGridViewRow row in selectForm.dataGridView1.Rows)
                    {
                        string ma_vv = row.Cells["MA_VV"].Value.ToString().Trim();
                        object ti_le_vv = row.Cells["TI_LE_VV"].Value;

                        if (row.IsSelect())
                        {
                            if (strMA_VV.Length + 1 + ma_vv.Length > 4000)
                            {
                                lstVV.Add(strMA_VV.Substring(1));
                                strMA_VV = ";" + ma_vv;
                            }
                            else
                            {
                                strMA_VV += ";" + ma_vv;
                            }

                        
                            if (strTI_LE_VV.Length + 1 + ti_le_vv.ToString().Length > 4000)
                            {
                                lstTI_LE_VV.Add(strTI_LE_VV.Substring(1));
                                strTI_LE_VV = ";" + ti_le_vv;
                            }
                            else
                            {
                                strTI_LE_VV += ";" + ti_le_vv;
                            }
                        }
                        else
                        {
                            if (strMA_VV_NOT.Length + 1 + ma_vv.Length > 4000)
                            {
                                lstVVNOT.Add(strMA_VV_NOT.Substring(1));
                                strMA_VV_NOT = ";" + ma_vv;
                            }
                            else
                            {
                                strMA_VV_NOT += ";" + ma_vv;
                            }


                            if (strTI_LE_VV_NOT.Length + 1 + ti_le_vv.ToString().Length > 4000)
                            {
                                lstTI_LE_VVNOT.Add(strTI_LE_VV_NOT.Substring(1));
                                strTI_LE_VV_NOT = ";" + ti_le_vv;
                            }
                            else
                            {
                                strTI_LE_VV_NOT += ";" + ti_le_vv;
                            }
                        }
                    }

                    if (strMA_VV.Length > 1) lstVV.Add(strMA_VV.Substring(1));
                    if (strMA_VV_NOT.Length > 1) lstVVNOT.Add(strMA_VV_NOT.Substring(1));
                    if (strTI_LE_VV.Length > 1) lstTI_LE_VV.Add(strTI_LE_VV.Substring(1));
                    if (strTI_LE_VV_NOT.Length > 1) lstTI_LE_VVNOT.Add(strTI_LE_VV_NOT.Substring(1));


                }
                // nhận list chọn vv hsvv vvnot(list ko chọn) hsvvnot...

                // tạo chuỗi nếu quá 4000 cắt tại ; value;value...

                //lúc nhận đưa vào sql
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnChonVV_Click", ex);
            }
        }

        private void txtKy1_V6LostFocus(object sender)
        {
            try
            {
                // tạo ngay1 gọi proc function VFV_YearMonthToDate @Year @Month @Day01 => ngay1.Value = result.
                int one = 1, zero = 0;
                SqlParameter[] plist =
                {
                    new SqlParameter("@Year", (int)txtNam.Value),
                    new SqlParameter("@Month", (int)txtKy1.Value),
                    new SqlParameter("@Day01", one),
                };
                object result = V6BusinessHelper.ExecuteFunctionScalar("VFV_YearMonthToDate", plist);
                if (result is DateTime)
                {
                    ngay1.Value = (DateTime)result;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".txtKy1_V6LostFocus", ex);
            }
        }

        private void txtKy2_V6LostFocus(object sender)
        {
            try
            {
                // tạo ngay1 gọi proc function VFV_YearMonthToDate @Year @Month @Day01 => ngay1.Value = result.
                int one = 1, zero = 0;
                SqlParameter[] plist =
                {
                    new SqlParameter("@Year", (int)txtNam.Value),
                    new SqlParameter("@Month", (int)txtKy2.Value),
                    new SqlParameter("@Day01", zero),
                };
                object result = V6BusinessHelper.ExecuteFunctionScalar("VFV_YearMonthToDate", plist);
                if (result is DateTime)
                {
                    ngay2.Value = (DateTime)result;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".txtKy2_V6LostFocus", ex);
            }
        }

        
    }
}
