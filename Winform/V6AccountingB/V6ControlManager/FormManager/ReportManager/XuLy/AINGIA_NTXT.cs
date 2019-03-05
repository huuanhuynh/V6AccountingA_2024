using System;
using System.Collections.Generic;
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
        private System.Windows.Forms.Label lblStatus;
    
        public AINGIA_NTXT(string itemId, string program, string reportProcedure, string reportFile, string text)
            : base(itemId, program, reportProcedure, reportFile, text, true)
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            lblStatus.AutoSize = true;
            lblStatus.Left = btnHuy.Right + 3;
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(V6Text.Text("TINHGIANTXT") + " (FIFO).");
        }

        public override void SetStatusText(string text)
        {
            lblStatus.Text = text;
            base.SetStatusText(text);
        }

        /// <summary>
        /// Viết đè hàm thực thi.
        /// </summary>
        protected override void ExecuteProcedure()
        {
            if (GenerateProcedureParameters())
            {
                var m_BigData = ObjectAndString.ObjectToString(V6Options.GetValue("M_BIG_DATA"));
                if (m_BigData == "1")
                {
                    var tTinhToan = new Thread(TinhGia_NTXT);
                    tTinhToan.Start();
                }
                else
                {
                    var tTinhToan_All = new Thread(TinhGia_NTXT_All);
                    tTinhToan_All.Start();
                }
                timerViewReport.Start();
            }
        }

        /// <summary>
        /// Chạy tính giá tách nhiều phần
        /// </summary>
        public void TinhGia_NTXT()
        {
            try
            {
                _executing = true;
                _message = "Đang tính giá ntxt... ";
                SetStatusText(_message);
                

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

                //Lấy danh sách vật tư.
                DataSet ds = TinhGia_NTXT_A1(_pList.ToArray());
                DataTable alvt = ds.Tables[0];
                
                // Lặp tháng
                int startMonth = period1, endMonth = period2;
                for (int i = startMonth; i <= endMonth; i++)
                {
                    //EXEC2
                    var date01 = new DateTime(year, i, 1);
                    var date02 = new DateTime(year, i, DateTime.DaysInMonth(year, i));
                    var ds_i = TinhGia_NTXT_A2(date01, date02);//Tra ve V_ALVT
                    var tbl_i = ds_i.Tables[0];
                    //EXEC3
                    TinhGia_NTXT_A3(year, i, date01, date02);
                    //Lặp vật tư @Year, @m, @Vt, @Ngay_ct, @M_MA_NT0
                    foreach (DataRow row in tbl_i.Rows)
                    {
                        //@Year, @m, @Vt, @Ngay_ct, @M_MA_NT0
                        if (row["NGAY_CT"] == null) continue;
                        var c_mavt = row["MA_VT"].ToString().Trim();
                        _message = string.Format("Đang tính tháng({0}) ma_vt({1})", i, c_mavt);
                        SetStatusText(_message);

                        SqlParameter[] plist =
                        {
                            new SqlParameter("@Year", year),
                            new SqlParameter("@Period1", startMonth),
                            new SqlParameter("@Period2", endMonth),
                            new SqlParameter("@Ma_vt", c_mavt),
                            new SqlParameter("@Warning", warning),
                            new SqlParameter("@Tinh_giatb", tinh_giatb),
                            new SqlParameter("@Advance", advance),
                            new SqlParameter("@Period", i),
                            new SqlParameter("@Ngay_ct", row["NGAY_CT"]),
                            new SqlParameter("@M_MA_NT0", V6Options.M_MA_NT0),
                        };
                        TinhGia_NTXT_A4(plist);
                        TinhGia_NTXT_A5(plist);
                    }

                    //EXEC8 @Year,@Ngay_ct1, @Ngay_ct2, @Tinh_giatb, @M_MA_NT0
                    TinhGia_NTXT_A8(date01, date02);
                    //EXEC6 @Year, @m, @Ngay1, @Ngay2
                    TinhGia_NTXT_A6(i, date01, date02);
                    //EXEC7 @Ngay_ct1, @Ngay_ct2, @Tinh_giatb, @M_MA_NT0
                    TinhGia_NTXT_A7(date01, date02);
                    //EXEC90 @Year, @Period2, @Ngay_ct1, @Ngay_ct2
                    if (warning == 1) TinhGia_NTXT_A90(i, date01, date02);
                    //EXEC91
                    for (DateTime j = date01; j <= date02; j = j.AddDays(1))
                    {
                        TinhGia_NTXT_A91(j);
                    }
                }
                //SELECT * FROM #POSTLIST

                _message = V6Text.Finish;
                SetStatusText(_message);
                _success = true;
                _executing = false;
            }
            catch (Exception ex)
            {
                _executing = false;
                _success = false;
                _message = ex.Message;
                SetStatusText(_message);
                Logger.WriteToLog(GetType() + ".TinhGia_NTXT " + ex.Message);
                SetStatusText("Tính lỗi: " + ex.Message);
            }
        }

        /// <summary>
        /// Chạy tính giá full in proc. (timeout)
        /// </summary>
        public void TinhGia_NTXT_All()
        {
            base.ExecuteProcedure();
        }

        private DataSet TinhGia_NTXT2(DateTime startDate, DateTime endDate)
        {
            try
            {
                SqlParameter[] plist = new[]
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

        /// <summary>
        /// Hàm tính toán có trả về danh sách V_ALVT trong tháng
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private DataSet TinhGia_NTXT_A2(DateTime startDate, DateTime endDate)
        {
            try
            {
                var plist = new List<SqlParameter>();
                plist.AddRange(_pList);
                plist.Add(new SqlParameter("@Ngay_ct1", startDate.ToString("yyyyMMdd")));
                plist.Add(new SqlParameter("@Ngay_ct2", endDate.ToString("yyyyMMdd")));

                return V6BusinessHelper.ExecuteProcedure("VPA_Ingia_ntxt_A2", plist.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT_A2 " + ex.Message);
            }
        }

        public void TinhGia_NTXT3(params object[] plist)
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

        public void TinhGia_NTXT_A3(int year, int month, DateTime ngay1, DateTime ngay2)
        {
            try
            {
                List<SqlParameter> plist = new List<SqlParameter>();
                plist.AddRange(_pList);
                plist.Add(new SqlParameter("@Period", year));
                plist.Add(new SqlParameter("@Ngay_ct1", ngay1.ToString("yyyyMMdd")));
                plist.Add(new SqlParameter("@Ngay_ct2", ngay2.ToString("yyyyMMdd")));
                V6BusinessHelper.ExecuteProcedure("VPA_Ingia_ntxt_A3", plist.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT_A3 " + ex.Message);
            }
        }

        public void TinhGia_NTXT3_All(SqlParameter[] plist)
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

        public void TinhGia_NTXT_A4(SqlParameter[] plist)
        {
            try
            {
                V6BusinessHelper.ExecuteProcedure("VPA_Ingia_NTXT_A4", plist);
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT_A4 " + ex.Message);
            }
        }

        public void TinhGia_NTXT_5(SqlParameter[] plist)
        {
            try
            {
                V6BusinessHelper.ExecuteProcedure("VPA_Ingia_NTXT_5", plist);
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT_5 " + ex.Message);
            }
        }
        
        public void TinhGia_NTXT_A5(SqlParameter[] plist)
        {
            try
            {
                V6BusinessHelper.ExecuteProcedure("VPA_Ingia_NTXT_A5", plist);
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT_A5 " + ex.Message);
            }
        }

        /// <summary>
        /// Trả về danh sách vật tư cần tính toán.
        /// </summary>
        /// <param name="plist"></param>
        /// <returns></returns>
        private DataSet TinhGia_NTXT_A1(SqlParameter[] plist)
        {
            try
            {
                return V6BusinessHelper.ExecuteProcedure("VPA_Ingia_ntxt_A1", plist);
            }
            catch (Exception ex)
            {
                throw new Exception("VPA_Ingia_ntxt_A1 " + ex.Message);
            }
        }

        /// <summary>
        /// Lặp vật tư, Gom EXEC4 và 5 vào trong 1 th
        /// </summary>
        public void TinhGia_NTXT_A45(int year, int month, string ma_vt, int warning, int tinh_giatb, string advance)
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

        private DataSet TinhGia_NTXT_A6(int period, DateTime ngay1, DateTime ngay2)
        {
            try
            {
                List<SqlParameter> list = new List<SqlParameter>();
                list.AddRange(_pList);
                list.Add(new SqlParameter("@Period", period));
                list.Add(new SqlParameter("@Ngay_ct1", ngay1.ToString("yyyyMMdd")));
                list.Add(new SqlParameter("@Ngay_ct2", ngay2.ToString("yyyyMMdd")));
                return V6BusinessHelper.ExecuteProcedure("VPA_ingia_ntxt_A6", list.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT_A6 " + ex.Message);
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
                throw new Exception("TinhGia_NTXT7 " + ex.Message);
            }
        }

        private DataSet TinhGia_NTXT_A7(DateTime ngay1, DateTime ngay2)
        {
            try
            {
                List<SqlParameter> list = new List<SqlParameter>();
                list.AddRange(_pList);
                list.Add(new SqlParameter("@Ngay_ct1", ngay1.ToString("yyyyMMdd")));
                list.Add(new SqlParameter("@Ngay_ct2", ngay2.ToString("yyyyMMdd")));
                list.Add(new SqlParameter("@M_MA_NT0", V6Options.M_MA_NT0));
                return V6BusinessHelper.ExecuteProcedure("VPA_ingia_ntxt_A7", list.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT_A7 " + ex.Message);
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
                throw new Exception("TinhGia_NTXT8 " + ex.Message);
            }
        }

        private DataSet TinhGia_NTXT_A8(DateTime ngay1, DateTime ngay2)
        {
            try
            {
                List<SqlParameter> list = new List<SqlParameter>(_pList);
                list.Add(new SqlParameter("@Ngay_ct1", ngay1.ToString("yyyyMMdd")));
                list.Add(new SqlParameter("@Ngay_ct2", ngay2.ToString("yyyyMMdd")));
                list.Add(new SqlParameter("@M_MA_NT0", V6Options.M_MA_NT0));
                return V6BusinessHelper.ExecuteProcedure("VPA_Ingia_ntxt_A8", list.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT_A8 " + ex.Message);
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

        private DataSet TinhGia_NTXT_A90(int period, DateTime date01, DateTime date02)
        {
            try
            {
                List<SqlParameter> list = new List<SqlParameter>(_pList);
                list.Add(new SqlParameter("@Period", period));
                list.Add(new SqlParameter("@Ngay_ct1", date01.ToString("yyyyMMdd")));
                list.Add(new SqlParameter("@Ngay_ct2", date02.ToString("yyyyMMdd")));
                return V6BusinessHelper.ExecuteProcedure("VPA_Ingia_NTXT_A90", list.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT_A90 " + ex.Message);
            }
        }

        private DataSet TinhGia_NTXT_A91(DateTime date)
        {
            try
            {
                List<SqlParameter> list = new List<SqlParameter>(_pList);
                list.Add(new SqlParameter("@Ngay_ct", date.ToString("yyyyMMdd")));
                
                return V6BusinessHelper.ExecuteProcedure("[VPA_Ingia_ntxt_A91]", list.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_NTXT_A91 " + ex.Message);
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

        private void InitializeComponent()
        {
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblStatus.Location = new System.Drawing.Point(181, 390);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(222, 18);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = ".";
            // 
            // AINGIA_NTXT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.lblStatus);
            this.Name = "AINGIA_NTXT";
            this.Controls.SetChildIndex(this.lblStatus, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.ResumeLayout(false);

        }

    }
}
