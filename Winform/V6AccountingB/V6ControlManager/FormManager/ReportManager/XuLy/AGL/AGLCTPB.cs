using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AGLCTPB : XuLyBase
    {
        public AGLCTPB(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                if (string.IsNullOrEmpty(_status2text))
                {
                    _status2text = "F4: Tạo phân bổ tự động, F8: Xóa phân bổ tự động, F9: Tính-> Cập nhật hệ số phân bổ";
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(ex);
            }
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = _status2text;
            }
            V6ControlFormHelper.SetStatusText2(text, id);
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            try
            {
                base.MakeReport2();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public override void FormatGridViewExtern()
        {
            dataGridView1.HideColumns("STT_REC", "UID", "STT_REC01", "STT_REC02", "STT_REC03", "STT_REC04", "STT_REC05", "STT_REC06",
                "STT_REC07", "STT_REC08", "STT_REC09", "STT_REC10", "STT_REC11", "STT_REC12");
            dataGridView2.HideColumns("STT_REC", "UID", "STT_REC0", "TK_HE_SO");
        }

        #region ==== Xử lý F9 ====

        protected override void XuLyF9()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (V6Login.UserRight.AllowAdd(Name, "00GL4"))
                    {

                        var currentRow = dataGridView1.CurrentRow;
                        if (dataGridView1.Columns.Contains("NAM") && dataGridView1.Columns.Contains("STT"))
                        {
                            //var selectedStt = currentRow.Cells["STT"].Value;
                            int selectedNam = ObjectAndString.ObjectToInt(currentRow.Cells["NAM"].Value);

                            List<string> _stt_recs = new List<string>();

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.IsSelect())
                                {
                                    var rowdata = row.ToDataDictionary();
                                    _stt_recs.Add(rowdata["STT_REC"].ToString().Trim());
                                }
                            }

                            if (_stt_recs.Count > 0)
                            {
                                var ketchuyenForm = new AGLCTPB_F9(_stt_recs, selectedNam, _reportProcedure);
                                ketchuyenForm.Text = "Tính hệ số phân bổ tự động";
                                if (ketchuyenForm.ShowDialog() == DialogResult.OK)
                                {
                                    ShowMainMessage(V6Text.Finish);
                                    btnNhan.PerformClick();
                                }
                                else if (ketchuyenForm.DialogResult == DialogResult.Abort)
                                {
                                    ShowMainMessage(V6Text.UnFinished + ketchuyenForm._message);
                                }

                                SetStatus2Text();
                            }
                            else
                            {
                                ShowMainMessage(V6Text.NoSelection);
                            }
                        }
                    }
                }
                else
                {
                    this.ShowWarningMessage("Không được phép xử lý!");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF9", ex);
            }
        }
        
        
        
        #endregion xulyF9

        #region ==== Xử lý F10 ====

        
        protected override void XuLyF10()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyBase XULYF10:\n" + ex.Message);
            }
        }

        #endregion xulyF10

        
        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                var data = LoadAD("ALPB1",sttRec);

                dataGridView2.DataSource = data;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ALCTPB ViewDetails: " + ex.Message);
            }
        }

        public DataTable LoadAD(string ad, string sttRec)
        {
            string sql = "SELECT c.* FROM [" + ad
                + "] c  Where c.stt_rec = @rec";
            var listParameters = new SqlParameter("@rec", sttRec);
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql, listParameters).Tables[0];
            return tbl;
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
        protected override void XuLyF8()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (!V6Login.UserRight.AllowDelete(Name, "GL4"))
                    {
                        this.ShowWarningMessage(V6Text.NoRight);
                        return;
                    }
                    var currentRow = dataGridView1.CurrentRow;
                    if (dataGridView1.Columns.Contains("NAM") && dataGridView1.Columns.Contains("STT"))
                    {
                        //var selectedStt = currentRow.Cells["STT"].Value;
                        int selectedNam = ObjectAndString.ObjectToInt(currentRow.Cells["NAM"].Value);

                        var _stt_recs = "";

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.IsSelect())
                            {
                                var rowdata = row.ToDataDictionary();
                                _stt_recs += string.Format(",{0}", rowdata["STT_REC"].ToString().Trim());
                            }
                        }

                        if (_stt_recs.Length > 0)
                        {
                            _stt_recs = _stt_recs.Substring(1);
                            //_stt_recs = "'" + _stt_recs + "'";

                            var fText = "Xóa phân bổ tự động ";
                            var f = new V6Form
                            {
                                Text = fText,
                                AutoSize = true,
                                FormBorderStyle = FormBorderStyle.FixedSingle
                            };

                            var ketchuyenForm = new AGLCTPB_F8(_stt_recs, selectedNam, _reportProcedure);


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
                else
                {
                    this.ShowWarningMessage("Không được phép xử lý!");
                }
            }

            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF8", ex);
            }
        }
        protected override void XuLyBoSungThongTinChungTuF4()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (V6Login.UserRight.AllowAdd(Name, "00GL4"))
                    {

                        var currentRow = dataGridView1.CurrentRow;
                        if (dataGridView1.Columns.Contains("NAM") && dataGridView1.Columns.Contains("STT"))
                        {
                            //var selectedStt = currentRow.Cells["STT"].Value;
                            int selectedNam = ObjectAndString.ObjectToInt(currentRow.Cells["NAM"].Value);

                            List<string> _stt_recs = new List<string>();

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.IsSelect())
                                {
                                    var rowdata = row.ToDataDictionary();
                                    //_stt_recs += string.Format(",{0}", rowdata["STT_REC"].ToString().Trim());
                                    _stt_recs.Add(rowdata["STT_REC"].ToString().Trim());
                                }
                            }

                            if (_stt_recs.Count > 0)
                            {
                                //_stt_recs = _stt_recs.Substring(1);
                                //_stt_recs = "'" + _stt_recs + "'";

                                
                                var ketchuyenForm = new AGLCTPB_F4(_stt_recs, selectedNam, _reportProcedure);
                                ketchuyenForm.Text = "Phân bổ tự động";
                                if (ketchuyenForm.ShowDialog() == DialogResult.OK)
                                {
                                    ShowMainMessage(V6Text.Finish);
                                    btnNhan.PerformClick();
                                    // Code trước đây:
                                    //ketchuyenForm.UpdateSuccessEvent += delegate
                                    //{
                                    //    foreach (DataGridViewRow row in dataGridView1.Rows)
                                    //    {
                                    //        if (row.IsSelect())
                                    //        {
                                    //            row.UnSelect();
                                    //        }
                                    //    }
                                    //};
                                }
                                else if (ketchuyenForm.DialogResult == DialogResult.Abort)
                                {
                                    ShowMainMessage(V6Text.UnFinished + ketchuyenForm._message);
                                }

                                SetStatus2Text();
                            }
                            else
                            {
                                ShowMainMessage(V6Text.NoSelection);
                            }
                        }
                    }
                }
                else
                {
                    this.ShowWarningMessage("Không được phép xử lý!");
                }
            }

            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyBoSungThongTinChungTuF4", ex);
            }
        }
            
    }
}
