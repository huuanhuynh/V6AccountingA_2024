using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AGSCTGS02_Control : XuLyBase
    {
        public AGSCTGS02_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption,string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            _SpaceBar = false;
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F4: Đăng ký chứng từ ghi sổ, F8: Xóa đăng ký CTGS");
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
        }

        protected override void XuLyF7()
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF7", ex);
            }
        }

        #region ==== Xử lý F8 ====


        protected override void XuLyF8()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (V6Login.UserRight.AllowDelete(Name, "S07"))
                    {

                        var currentRow = dataGridView1.CurrentRow;
                        if (dataGridView1.Columns.Contains("NAM") &&dataGridView1.Columns.Contains("THANG") && dataGridView1.Columns.Contains("KHOA_CTGS"))
                        {
                            int selectedNam = ObjectAndString.ObjectToInt(currentRow.Cells
                                ["NAM"].Value);
                             int selectedthang = ObjectAndString.ObjectToInt
                               (currentRow.Cells["THANG"].Value);
                            var _numlist = "";

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.IsSelect())
                                {
                                    var rowdata = row.ToDataDictionary();
                                    _numlist = _numlist + string.Format(",'{0}'", rowdata["KHOA_CTGS"].ToString().Trim());

                                }
                            }

                            if (_numlist.Length > 0)
                            {
                                _numlist = _numlist.Substring(1);


                                var fText = "Xóa đăng ký chứng từ ghi sổ";
                                var f = new V6Form
                                {
                                    Text = fText,
                                    AutoSize = true,
                                    FormBorderStyle = FormBorderStyle.FixedSingle
                                };

                                var ketchuyenForm = new AGSCTGS02_F8(_numlist, selectedNam,selectedthang,selectedthang, _program);


                                ketchuyenForm.UpdateSuccessEvent += delegate
                                {
                                    foreach (DataGridViewRow row in dataGridView1.Rows)
                                    {
                                        if (row.IsSelect())
                                        {
                                            row.UnSelect();

                                        }
                                    }
                                };

                                f.Controls.Add(ketchuyenForm);
                                ketchuyenForm.Disposed += delegate
                                {
                                    f.Dispose();
                                };

                                f.ShowDialog(this);
                                SetStatus2Text();
                            }

                        }
                    }

                }
                else
                {
                    this.ShowWarningMessage("Chưa có dữ liệu!");
                }

            }

            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF8", ex);
            }
        }

        #endregion xử lý F8

        #region ==== Xử lý F4 ====

        private bool f4Running;
        private string f4Error = "";
        private string f4ErrorAll = "";
        AGSCTGS02_F4 form_F4 = null;
        protected override void XuLyBoSungThongTinChungTuF4()
        {
            if (f4Running)
            {
                ShowMainMessage("f4Running");
                return;
            }

            try
            {
                SaveSelectedCellLocation(dataGridView1);
                
                form_F4 = null;
                
                if (dataGridView1.CurrentRow != null)
                {
                    var currentRow = dataGridView1.CurrentRow;
                    if (dataGridView1.Columns.Contains("NAM") && dataGridView1.Columns.Contains("THANG") &&
                        dataGridView1.Columns.Contains("KHOA_CTGS"))
                    {
                        int selectedNam = ObjectAndString.ObjectToInt
                            (currentRow.Cells["NAM"].Value);

                        int selectedthang = ObjectAndString.ObjectToInt
                            (currentRow.Cells["THANG"].Value);

                        form_F4 = new AGSCTGS02_F4(selectedNam, selectedthang, selectedthang, _program);
                        form_F4.Text = "Đăng ký chứng từ ghi sổ";
                        form_F4.ShowDialog(this);
                        SetStatus2Text();
                        if (form_F4.DialogResult == DialogResult.OK)
                        {
                            //Luu lai nhung gia tri duoc chon vao bien!!!
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    this.ShowWarningMessage("Chưa có dữ liệu!");
                    return;
                }

                if (form_F4 == null) return;

                Timer tF4 = new Timer();
                tF4.Interval = 500;
                tF4.Tick += tF4_Tick;
                Thread t = new Thread(F4Thread);
                //t.SetApartmentState(ApartmentState.STA);
                CheckForIllegalCrossThreadCalls = false;
                remove_list_g = new List<DataGridViewRow>();
                t.IsBackground = true;
                t.Start();
                tF4.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF4: " + ex.Message);
            }
        }

        int update_count = 0, update_fail = 0;

        private void F4Thread()
        {
            try
            {
                f4Running = true;
                f4ErrorAll = "";
                update_count = update_fail = 0;

                SqlParameter[] plist1 =
                    {
                        new SqlParameter("@Year", (int)form_F4.txtNam.Value), 
                        new SqlParameter("@Period", (int)form_F4.txtKy1.Value), 
                    };
                SqlParameter[] plist2 =
                    {
                        new SqlParameter("@Year", (int)form_F4.txtNam.Value), 
                        new SqlParameter("@Period", (int)form_F4.txtKy2.Value), 
                    };
                var ngayDauKy = ObjectAndString.ObjectToFullDateTime(V6BusinessHelper.ExecuteFunctionScalar("vfa_GetStartDateOfPeriod", plist1));
                var ngayCuoiKy = ObjectAndString.ObjectToFullDateTime(V6BusinessHelper.ExecuteFunctionScalar("vfa_GetEndDateOfPeriod", plist2));

                foreach (DataGridViewRow grow in dataGridView1.Rows)
                {
                    var rowdata = grow.ToDataDictionary();

                    if (V6Login.UserRight.AllowAdd(Name, "S07"))
                    {
                        string khoa_ctgs = string.Format("{0}", rowdata["KHOA_CTGS"].ToString().Trim());
                        string so_lo = string.Format("{0}", rowdata["SO_LO"].ToString().Trim());

                        SqlParameter[] plistr =
                        {
                            new SqlParameter("@Year", form_F4.txtNam.Value),
                            new SqlParameter("@Period1", form_F4.txtKy1.Value),
                            new SqlParameter("@Period2", form_F4.txtKy2.Value),
                            new SqlParameter("@Khoa_ctgs", khoa_ctgs),
                            new SqlParameter("@User_id", V6Login.UserId),
                        };
                        V6BusinessHelper.ExecuteProcedureNoneQuery("AGSCTGS02_A3", plistr);
                        
                        for (DateTime i = ngayDauKy; i <= ngayCuoiKy; i = i.AddDays(1))
                        {
                            SqlParameter[] plist =
                            {
                                new SqlParameter("@Type", "NEW"),
                                new SqlParameter("@Year", form_F4.txtNam.Value),
                                new SqlParameter("@Period1", form_F4.txtKy1.Value),
                                new SqlParameter("@Period2", form_F4.txtKy2.Value),
                                new SqlParameter("@Khoa_ctgs", khoa_ctgs),
                                new SqlParameter("@User_id", V6Login.UserId),
                                new SqlParameter("@Ma_dvcs", form_F4.txtMaDvcs.StringValueCheck),
                                new SqlParameter("@Ngay_ct1", i.ToString("yyyyMMdd")),
                                new SqlParameter("@Ngay_ct2", i.ToString("yyyyMMdd")),
                            };
                            
                            try
                            {
                                V6BusinessHelper.ExecuteProcedureNoneQuery(_program + "_A", plist);
                                update_count++;
                                _message = string.Format("Updated {0}/{1}-->{2}", update_count, dataGridView1.Rows.Count, so_lo);
                            }
                            catch (Exception ex)
                            {
                                update_fail++;
                                _message = ex.Message;
                                f4Error += ex.Message;
                                f4ErrorAll += ex.Message;
                            }
                        }//end for ngay
                        SqlParameter[] plistA4 =
                        {
                            new SqlParameter("@Year", form_F4.txtNam.Value),
                            new SqlParameter("@Period1", form_F4.txtKy1.Value),
                            new SqlParameter("@Period2", form_F4.txtKy2.Value),
                            new SqlParameter("@Khoa_ctgs", khoa_ctgs),
                            new SqlParameter("@Ma_dvcs", form_F4.txtMaDvcs.StringValueCheck),
                            new SqlParameter("@User_id", V6Login.UserId),
                        };
                        V6BusinessHelper.ExecuteProcedureNoneQuery("AGSCTGS02_A4", plistA4);
                    }
                }

            }
            catch (Exception ex)
            {
                f4Error += ex.Message;
                f4ErrorAll += ex.Message;
            }

            End:
            f4Running = false;
        }

        void tF4_Tick(object sender, EventArgs e)
        {
            if (f4Running)
            {
                var cError = f4Error;
                f4Error = f4Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F4 running " + _message
                    + (cError.Length > 0 ? "Error: " : "")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();
                btnNhan.PerformClick();
                LoadSelectedCellLocation(dataGridView1);
                V6ControlFormHelper.SetStatusText("F4 finish "
                    + (f4ErrorAll.Length > 0 ? "Error: " : "")
                    + f4ErrorAll);

                V6ControlFormHelper.ShowMainMessage(string.Format("F4 Xử lý xong! Success:{0} Fail:{1}", update_count, update_fail));
                if (f4Error.Length > 0)
                {
                    this.WriteToLog(GetType() + ".F4", f4ErrorAll);
                }
            }
        }
        #endregion xulyF9

        protected override void XuLyHienThiFormSuaChungTuF3()
        {
            try
            {
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF3", ex);
            }
        }

        protected void F4Thread0()
        {
            try
            {
                

            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyBoSungThongTinChungTuF4", ex);
            }
        }

        public override void FormatGridViewExtern()
        {
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //dataGridView1.EnableEdit(("SO_CT01,SO_CT02,SO_CT03,SO_CT04,SO_CT05,SO_CT06," +
            //                         "SO_CT07,SO_CT08,SO_CT09,SO_CT10,SO_CT11,SO_CT12").Split(','));
            //V6TableStruct table_struct = V6BusinessHelper.GetTableStruct("ALKC");
            //dataGridView1.FormatEditTextBox(table_struct, "UPPER");
        }

        public override void GridView1CellEndEdit(DataGridViewRow row, string FIELD, object fieldData)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".GridView1CellEndEdit", ex);
            }
        }
    }
}
