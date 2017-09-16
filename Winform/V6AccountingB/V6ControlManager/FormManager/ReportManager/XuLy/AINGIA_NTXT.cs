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

                //Các tham số
                //@Year int,
                //@Period1 int,
                //@Period2 int,
                //@Ma_vt VARCHAR(50),
                //@Warning int,
                //@Tinh_giatb int,
                //@Advance VARCHAR(255) = ''
                int year = ObjectAndString.ObjectToInt(_pList[0].Value);
                int period1 = ObjectAndString.ObjectToInt(_pList[1].Value);
                int period2 = ObjectAndString.ObjectToInt(_pList[2].Value);
                string ma_vt = ObjectAndString.ObjectToString(_pList[3].Value);
                int warning = ObjectAndString.ObjectToInt(_pList[4].Value);
                int tinh_giatb = ObjectAndString.ObjectToInt(_pList[5].Value);
                string advance = ObjectAndString.ObjectToString(_pList[6].Value);

                //
                // Lặp tháng
                int startMonth = period1, endMonth = period2;
                for (int i = startMonth; i < endMonth; i++)
                {
                    //EXEC2
                    var date01 = new DateTime(year, i, 1);
                    var date02 = new DateTime(year, i, DateTime.DaysInMonth(year, i));
                    TinhGia_NTXT2(date01, date02);
                    //EXEC3
                    TinhGia_NTXT3(year, i, date01, date02);
                    //Lặp vật tư @Year, @m, @Vt, @Ngay_ct, @M_MA_NT0
                    {
                        TinhGia_NTXT_A45(year, i, ma_vt, warning, tinh_giatb, advance);
                        //EXEC4
                        //EXEC5
                    }
                    //EXEC8 @Year,@Ngay_ct1, @Ngay_ct2, @Tinh_giatb, @M_MA_NT0
                    TinhGia_NTXT8(year, date01, date02, tinh_giatb, V6Options.M_MA_NT0);
                    //EXEC6 @Year, @m, @Ngay1, @Ngay2
                    TinhGia_NTXT6(year, i, date01, date02);
                    //EXEC7 @Ngay_ct1, @Ngay_ct2, @Tinh_giatb, @M_MA_NT0
                    TinhGia_NTXT7(date01, date02, tinh_giatb, V6Options.M_MA_NT0);
                    //EXEC90 @Year, @Period2, @Ngay_ct1, @Ngay_ct2
                    if (warning == 1) TinhGia_NTXT90(new object[] { year, period2, date01, date02 });
                    //EXEC91
                    TinhGia_NTXT91();
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

        private DataSet TinhGia_NTXT6(params object[] objects)
        {
            try
            {
                return V6BusinessHelper.ExecuteProcedure("VPA_ingia_ntxt_6", objects);
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT6 " + ex.Message);
            }
        }
        
        private DataSet TinhGia_NTXT7(params object[] objects)
        {
            try
            {
                return V6BusinessHelper.ExecuteProcedure("VPA_ingia_ntxt_7", objects);
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

        /// <summary>
        /// Lặp vật tư, Gom EXEC4 và 5 vào trong 1 th
        /// </summary>
        private void TinhGia_NTXT_A45(int year, int month, string ma_vt, int warning, int tinh_giatb, string advance)
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@year", year),
                    new SqlParameter("@month", month),
                    new SqlParameter("@Ma_vt", ma_vt),
                    new SqlParameter("@Warning", warning),
                    new SqlParameter("@Tinh_giatb", tinh_giatb),
                    new SqlParameter("@Advance", advance),
                };
                V6BusinessHelper.ExecuteProcedure("[VPA_Ingia_ntxt_A45]", plist);
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT4 " + ex.Message);
            }
        }

        private void TinhGia_NTXT3(params object[] plist)
        {
            try
            {
                V6BusinessHelper.ExecuteProcedure("VPA_Ingia_ntxt_3", plist);
            }
            catch (Exception ex)
            {
                throw new Exception("VPA_Ingia_ntxt_3 " + ex.Message);
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

        private DataSet TinhGia_NTXT2(DateTime startDate, DateTime endDate)
        {
            try
            {
                SqlParameter[] plist = new []
                {
                    new SqlParameter("@Ngay_ct1", startDate),
                    new SqlParameter("@Ngay_ct2", endDate),
                };
                return V6BusinessHelper.ExecuteProcedure("[VPA_Ingia_ntxt_2]", plist);
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT2 " + ex.Message);
            }
        }

        private DataSet TinhGia_NTXT8(params object[] objects)
        {
            try
            {
                return V6BusinessHelper.ExecuteProcedure("VPA_Ingia_ntxt_8", objects);
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT90 " + ex.Message);
            }
        }

        private DataSet TinhGia_NTXT90(params object[] objects)
        {
            try
            {
                return V6BusinessHelper.ExecuteProcedure("VPA_Ingia_NTXT_90", objects);
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT90 " + ex.Message);
            }
        }

        private DataSet TinhGia_NTXT91()
        {
            try
            {
                return V6BusinessHelper.ExecuteProcedure("[VPA_Ingia_ntxt_91]", new SqlParameter[]{});
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT91 " + ex.Message);
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
