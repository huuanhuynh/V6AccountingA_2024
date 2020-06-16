using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AINGIA_DD_LO : XuLyBase0
    {
        private System.Windows.Forms.Label lblStatus;

        public AINGIA_DD_LO(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
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
            V6ControlFormHelper.SetStatusText2("Tính giá trung bình.");
        }

        protected override void Nhan()
        {
            base.Nhan();
        }

        //Sửa lại hoàn toàn. Tách thành nhiều hàm trong Thread.
        protected override void ExecuteProcedure()
        {
            _message = "";
            try
            {
                //_pList:
                //0 @Period1 int,
                //1 @Year1 int,
                //2 @Period2 int,
                //3 @Year2 int,
                //4 @Ma_kho VARCHAR(50),
                //5 @Ma_vt VARCHAR(50),
                //6 @Dk_cl int,
                //7 @Tinh_giatb int,
                //8 @Advance VARCHAR(255) = ''
                if (GenerateProcedureParameters())
                {
                    int check = V6BusinessHelper.CheckDataLocked("2", V6Setting.M_SV_DATE, (int)FilterControl.Number2, (int)FilterControl.Number3);
                    if (check == 1)
                    {
                        this.ShowWarningMessage(V6Text.CheckLock);
                        return;
                    }

                    //if (m_BigData == "1")
                    {
                        var tTinhToan = new Thread(TinhGia_DD_LO);
                        tTinhToan.Start();
                    }
                    
                    timerViewReport.Start();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public void TinhGia_DD_LO()
        {
            try
            {
                _executing = true;

                SetStatusText("Đang tính giá ... ");

                var tb2 = TinhGia_DD_LO_A2().Tables[0];
                var row = tb2.Rows[0];
                var @Ngay_ct1 = ObjectAndString.ObjectToDate(row["Ngay_ct1"]);
                var @Ngay_ct2 = ObjectAndString.ObjectToDate(row["Ngay_ct2"]);
                var @Gia_vt = row["Gia_vt"];
                var @Ma_Kho = row["Ma_Kho"];
                var @Ma_vt = row["Ma_vt"];
                var @Tinh_giatb = row["Tinh_giatb"];
                var @Advance = row["Advance"].ToString().Trim();

                if (Ngay_ct1 != null)
                    for (DateTime i = Ngay_ct1.Value; i <= Ngay_ct2; i = i.AddDays(1))
                    {
                        SqlParameter[] plist =
                        {
                            new SqlParameter("@Ngay_ct1", i), 
                            new SqlParameter("@Ngay_ct2", i), 
                            new SqlParameter("@Gia_vt", Gia_vt), 
                            new SqlParameter("@Ma_Kho", Ma_Kho), 
                            new SqlParameter("@Ma_vt", Ma_vt), 
                            new SqlParameter("@Tinh_giatb", Tinh_giatb), 
                            new SqlParameter("@Advance", Advance), 
                        };
                        _message = "Đang cập nhật giá ... " + i.ToString("dd/MM/yyyy");
                        SetStatusText(_message);
                        TinhGia_DD_LO_A3(plist);
                    }
                
                
                
                TinhGia_DD_LO4();
                var Dk_cl = ObjectAndString.ObjectToInt(_pList[6].Value);
                if (Dk_cl != 0)
                {
                    SetStatusText("Đang tạo chênh lệch ... ");
                    TinhGia_DD_LO_A5(_pList.ToArray());
                }
                
                TinhGia_DD_LO6();
                SetStatusText(V6Text.Finish);
                _executesuccess = true;
                _executing = false;
            }
            catch (Exception ex)
            {
                _executing = false;
                _executesuccess = false;
                Logger.WriteToLog(GetType() + ".TinhGia_DD_LO " + ex.Message);
                SetStatusText(string.Format("{0}: {1}", V6Text.Text("TinhLoi"), ex.Message));
            }
        }
        public void TinhGia_DD_LO_All()
        {
            try
            {
                _executing = true;

                SetStatusText("Đang tính giá ... ");
                var ds1 = TinhGia_DD_LO_A1();
                var MaxCal = ds1.Tables[0];
                var count = ObjectAndString.ObjectToInt(MaxCal.Rows[0][0] ?? 0) + 2;


                var m_giavt = ObjectAndString.ObjectToString(V6Options.GetValue("M_GIA_VT"));
                if (m_giavt == "1" || m_giavt == "0")
                {
                    count = 1;
                }

                for (int no = 0; no < count; no++)
                {
                    var tb2 = TinhGia_DD_LO_A2().Tables[0];
                    var row = tb2.Rows[0];
                    var @Ngay_ct1 = ObjectAndString.ObjectToDate(row["Ngay_ct1"]);
                    var @Ngay_ct2 = ObjectAndString.ObjectToDate(row["Ngay_ct2"]);
                    var @Gia_vt = row["Gia_vt"];
                    var @Ma_Kho = row["Ma_Kho"];
                    var @Ma_vt = row["Ma_vt"];
                    var @Tinh_giatb = row["Tinh_giatb"];
                    var @Advance = row["Advance"].ToString().Trim();

                  
                            SqlParameter[] plist =
                            {
                                new SqlParameter("@Ngay_ct1", Ngay_ct1), 
                                new SqlParameter("@Ngay_ct2", Ngay_ct2), 
                                new SqlParameter("@Gia_vt", Gia_vt), 
                                new SqlParameter("@Ma_Kho", Ma_Kho), 
                                new SqlParameter("@Ma_vt", Ma_vt), 
                                new SqlParameter("@Tinh_giatb", Tinh_giatb), 
                                new SqlParameter("@Advance", Advance), 
                            };

                            SetStatusText("Đang cập nhật giá ... ");
                            TinhGia_DD_LO_A3_All(plist);
                      
                }


                TinhGia_DD_LO4();
                var Dk_cl = ObjectAndString.ObjectToInt(_pList[6].Value);
                if (Dk_cl != 0)
                {
                    SetStatusText("Đang tạo chênh lệch.. ");
                    TinhGia_DD_LO_A5(_pList.ToArray());
                }

                TinhGia_DD_LO6();
                SetStatusText(V6Text.Finish);
                _executesuccess = true;
                _executing = false;
            }
            catch (Exception ex)
            {
                _executing = false;
                _executesuccess = false;
                Logger.WriteToLog(GetType() + ".TinhGia_DD_LO " + ex.Message);
                SetStatusText(string.Format("{0}: {1}", V6Text.Text("TinhLoi"), ex.Message));
            }
        }

        public override void SetStatusText(string text)
        {
            lblStatus.Text = text;
            base.SetStatusText(text);
        }

        public void TinhGia_DD_LO6()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_DD_LO6 " + ex.Message);
            }
        }

        public void TinhGia_DD_LO_A5(SqlParameter[] plist)
        {
            try
            {
                V6BusinessHelper.ExecuteProcedure("VPA_Ingia_DD_LO_A5", plist);
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_DD_LO_A5 " + ex.Message);
            }
        }

        public void TinhGia_DD_LO4()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_DD_LO4 " + ex.Message);
            }
        }

        public void TinhGia_DD_LO_A3(SqlParameter[] plist)
        {
            try
            {
                V6BusinessHelper.ExecuteProcedure("VPA_Ingia_DD_LO_A3", plist);
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_DD_LO_A3 " + ex.Message);
            }
        }
        public void TinhGia_DD_LO_A3_All(SqlParameter[] plist)
        {
            try
            {
                V6BusinessHelper.ExecuteProcedure("VPA_Ingia_DD_LO_A3", plist);
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_DD_LO_A3 " + ex.Message);
            }
        }

        private DataSet TinhGia_DD_LO_A2()
        {
            try
            {
                return V6BusinessHelper.ExecuteProcedure("VPA_Ingia_DD_LO_A2", _pList.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_DD_LO_A2 " + ex.Message);
            }
        }

        private DataSet TinhGia_DD_LO_A1()
        {
            try
            {
                return V6BusinessHelper.ExecuteProcedure("VPA_Ingia_DD_LO_A1", _pList.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("TinhGia_DD_LO_A1 " + ex.Message);
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
            this.lblStatus.Location = new System.Drawing.Point(188, 391);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(222, 18);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = ".";
            // 
            // AINGIA_DD_LO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.lblStatus);
            this.Name = "AINGIA_DD_LO";
            this.Controls.SetChildIndex(this.btnSuaTTMauBC, 0);
            this.Controls.SetChildIndex(this.btnThemMauBC, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.lblStatus, 0);
            this.ResumeLayout(false);

        }
    }
}
