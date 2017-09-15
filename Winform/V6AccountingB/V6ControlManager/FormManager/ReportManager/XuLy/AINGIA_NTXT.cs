using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using V6AccountingBusiness;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AINGIA_NTXT : XuLyBase0
    {
        public AINGIA_NTXT(string itemId, string program, string reportProcedure, string reportFile, string text)
            : base(itemId, program, reportProcedure, reportFile, text, true)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("Tính giá nhập trước xuất trước (FIFO).");
        }

        //Tạm thời chạy code cũ khi chưa viết xong, test hoặc viết xong cần mở lại.
        //protected override void ExecuteProcedure()
        //{
        //    if (GenerateProcedureParameters())
        //    {
        //        var m_BigData = ObjectAndString.ObjectToString(V6Options.V6OptionValues["M_BIG_DATA"]);
        //        if (m_BigData == "1")
        //        {
        //            var tTinhToan = new Thread(TinhGia_NTXT);
        //            tTinhToan.Start();
        //        }
        //        else
        //        {
        //            var tTinhToan_All = new Thread(TinhGia_NTXT_All);
        //            tTinhToan_All.Start();
        //        }
        //        timerViewReport.Start();
        //    }
        //}


        private void TinhGia_NTXT()
        {
            try
            {
                _executing = true;
                SetStatusText("Đang tính giá ... ");


                // Lặp tháng
                int startMonth = 1, endMonth = 12;
                for (int i = startMonth; i < endMonth; i++)
                {
                    //EXEC2
                    //EXEC3
                        //Lặp ngày
                        //EXEC8
                        //EXEC6
                    //EXEC7
                    //EXEC90
                    //EXEC91
                }
                //SELECT * FROM #POSTLIST

                SetStatusText(V6Text.Finish);
                _success = true;
                _executing = false;
            }
            catch (Exception ex)
            {
                _executing = false;
                _success = false;
                Logger.WriteToLog(GetType() + ".TinhGia_NTXT " + ex.Message);
                SetStatusText("Tính lỗi: " + ex.Message);
            }
        }

        private void TinhGia_NTXT_All()
        {
            base.ExecuteProcedure();
        }

        private void TinhGia_NTXT6()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT6 " + ex.Message);
            }
        }

        private void TinhGia_NTXT5(SqlParameter[] plist)
        {
            try
            {
                V6BusinessHelper.ExecuteProcedure("VPA_Ingia_NTXT_A5", plist);
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT5 " + ex.Message);
            }
        }

        private void TinhGia_NTXT4()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT4 " + ex.Message);
            }
        }

        private void TinhGia_NTXT3(SqlParameter[] plist)
        {
            try
            {
                V6BusinessHelper.ExecuteProcedure("VPA_Ingia_NTXT_A3", plist);
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT3 " + ex.Message);
            }
        }
        private void TinhGia_NTXT3_All(SqlParameter[] plist)
        {
            try
            {
                V6BusinessHelper.ExecuteProcedure("VPA_Ingia_NTXT_A3", plist);
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT3 " + ex.Message);
            }
        }

        private DataSet TinhGia_NTXT2()
        {
            try
            {
                return V6BusinessHelper.ExecuteProcedure("VPA_Ingia_NTXT_A2", _pList.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT2 " + ex.Message);
            }
        }

        private DataSet VPA_Ingia_ntxt(SqlParameter[] plist)
        {
            try
            {
                return V6BusinessHelper.ExecuteProcedure("VPA_Ingia_ntxt", plist);
            }
            catch (Exception ex)
            {
                throw new Exception("VPA_Ingia_ntxt " + ex.Message);
            }
        }

    }
}
