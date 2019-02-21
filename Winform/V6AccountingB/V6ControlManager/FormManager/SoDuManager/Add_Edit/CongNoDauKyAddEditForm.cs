using System;
using System.Data.SqlClient;
using System.Threading;
using V6AccountingBusiness;
using V6Controls;
using V6Init;
using V6Structs;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class CongNoDauKyAddEditForm : SoDuAddEditControlVirtual
    {
        public CongNoDauKyAddEditForm()
        {
            InitializeComponent();
            
            TxtTk.SetInitFilter("loai_tk=1 and tk_cn=1");
        }

        public override void DoBeforeAdd()
        {
            TxtNam.Value = V6Setting.M_Nam_bd;
        }

        public override void DoBeforeEdit()
        {
            if (V6Setting.M_Ngay_ky1.Day == 1 && V6Setting.M_Ngay_ky1.Month == 1)
            {
                TxtDu_no1.Enabled = false;
                TxtDu_co1.Enabled = false;
                TxtDu_co_nt1.Enabled = false;
                TxtDu_no_nt1.Enabled = false;
            }
        }

        public override void ValidateData()
        {
            if ((TxtMa_dvcs.Text.Trim() == "")
                || (TxtMa_KH.Text.Trim() == "")
                || (TxtTk.Text.Trim() == ""))
            {
                throw new Exception("Chưa nhập đủ thông tin!");
            }
            else
            {
                // Check data 
                if (Mode == V6Mode.Edit)
                {

                    if (DataDic.ContainsKey("MA_DVCS") && DataOld.ContainsKey("MA_DVCS")
                        && DataDic.ContainsKey("MA_KH") && DataOld.ContainsKey("MA_KH")
                         && DataDic.ContainsKey("TK") && DataOld.ContainsKey("TK")
                        && DataDic.ContainsKey("NAM") && DataOld.ContainsKey("NAM"))
                    {
                        bool b = V6BusinessHelper.IsValidThreeCode_OneNumeric(TableName.ToString(), 0,
                            "MA_DVCS", TxtMa_dvcs.Text, DataOld["MA_DVCS"].ToString(),
                            "MA_KH", TxtMa_KH.Text, DataOld["MA_KH"].ToString(),
                            "TK", TxtTk.Text, DataOld["TK"].ToString(),
                            "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                        if (!b)
                            throw new Exception("Không được thêm mã đã tồn tại: ");

                    }


                }
                else if (Mode == V6Mode.Add)
                {
                    if (DataDic.ContainsKey("MA_DVCS") 
                        && DataDic.ContainsKey("MA_KH")
                         && DataDic.ContainsKey("TK") 
                        && DataDic.ContainsKey("NAM") )
                    {
                        bool b = V6BusinessHelper.IsValidThreeCode_OneNumeric(TableName.ToString(), 1,
                            "MA_DVCS", TxtMa_dvcs.Text, TxtMa_dvcs.Text,
                            "MA_KH", TxtMa_KH.Text, TxtMa_KH.Text,
                            "TK", TxtTk.Text, TxtTk.Text,
                            "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                        if (!b)
                            throw new Exception("Không được thêm mã đã tồn tại: ");

                    }
                }
            }
        }

        private void CapNhapDuDauNam()
        {
            if (V6Setting.M_Ngay_ky1.Day == 1 && V6Setting.M_Ngay_ky1.Month == 1)
            {
                TxtDu_no1.Value = TxtDu_no00.Value;
                TxtDu_co1.Value = TxtDu_co00.Value;
                TxtDu_co_nt1.Value = TxtDu_co_nt00.Value;
                TxtDu_no_nt1.Value = TxtDu_no_nt00.Value;
            }
        }

        public override bool InsertNew()
        {
            var result = base.InsertNew();
            if (result)
            {
                AGLSD0_UpdateTK(TxtMa_dvcs.Text.Trim());
            }
            return result;
        }

        public override int UpdateData()
        {
            var result = base.UpdateData();
            if (result > 0)
            {
                AGLSD0_UpdateTK(TxtMa_dvcs.Text.Trim());
            }
            return result;
        }

        private string ma_dvcs = "";

        private void AGLSD0_UpdateTK(string maDvcs)
        {
            ma_dvcs = maDvcs;
            CheckForIllegalCrossThreadCalls = false;
            Thread T = new Thread(AGLSD0_UpdateTK_Thread);
            T.IsBackground = true;
            T.Start();
            var timer = new Timer
            {
                Interval = 500
            };
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (thread_finish)
            {
                ((Timer)sender).Stop();
                ShowMainMessage(thread_finish_success
                    ? V6Text.Finish + " AGLSD0_UpdateTK_Thread."
                    : V6Text.Fail + " AGLSD0_UpdateTK_Thread.");
                thread_finish = false;
                thread_finish_success = false;
            }
        }

        private bool thread_finish;
        private bool thread_finish_success;
        private void AGLSD0_UpdateTK_Thread()
        {
            try
            {
                V6BusinessHelper.ExecuteProcedureNoneQuery("AGLSD0_UpdateTK", new SqlParameter("@ma_dvcs", ma_dvcs));
                thread_finish_success = true;
            }
            catch (Exception ex)
            {
                thread_finish_success = false;
                this.WriteExLog(GetType() + ".AGLSD0_UpdateTK_Thread", ex);
            }
            thread_finish = true;
        }

        private void TxtDu_no00_V6LostFocus(object sender)
        {
            if (TxtDu_no00.Value != 0)
            {
                TxtDu_co00.Value = 0;
            }
            CapNhapDuDauNam();
        }
        private void TxtDu_co00_V6LostFocus(object sender)
        {
            if (TxtDu_co00.Value != 0)
            {
                TxtDu_no00.Value = 0;
            }
            CapNhapDuDauNam();
        }

        private void TxtDu_no_nt00_V6LostFocus(object sender)
        {
            if (TxtDu_no_nt00.Value != 0)
            {
                TxtDu_co_nt00.Value = 0;
            }
            CapNhapDuDauNam();
        }
        private void TxtDu_co_nt00_V6LostFocus(object sender)
        {
            if (TxtDu_co_nt00.Value != 0)
            {
                TxtDu_no_nt00.Value = 0;
            }
            CapNhapDuDauNam();
        }

    }
}
