using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy.NhanSu
{
    public class HPRCONGCT_XL1 : XuLyBase
    {
        public HPRCONGCT_XL1(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            SetupGridview();
        }

        private void SetupGridview()
        {
            try
            {
                dataGridView1.ShowCellErrors = false;
                dataGridView1.ShowRowErrors = false;
                dataGridView1.DataError+= delegate(object sender, DataGridViewDataErrorEventArgs args)
                {
                    args.ThrowException = false;
                };

                dataGridView1.ReadOnly = false;
                dataGridView1.EditMode = DataGridViewEditMode.EditOnF2;

                LookupInfo = V6ControlsHelper.GetAldmConfig("Prhlcong");
                InitFilter = V6Login.GetInitFilter(LookupInfo.TABLE_NAME);
                //var tblCong = V6BusinessHelper.Select("Prhlcong", "MA_CONG,TEN_CONG,TEN_CONG2", "Status='1'").Data;
                //string displayMember = V6Setting.LanguageIsVietnamese ? "TEN_CONG" : "TEN_CONG2";
                //string valueMember = "MA_CONG";
                //object[] listCong = "A,B,C".Split(',');



                dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;
                dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
                dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
                dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;

                dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
                dataGridView1.CellPainting += dataGridView1_CellPainting;
                dataGridView1.MouseMove += dataGridView1_MouseMove;
                dataGridView1.MouseLeave += dataGridView1_MouseLeave;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetupGridview", ex);
            }
        }

        void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
                selectedColumnIndex = dataGridView1.CurrentCell.ColumnIndex;
            }
        }


        private string cFIELD = "CONG_";// + ("00" + FilterControl.Date1.Day).Right(2);
        private int selectedRowIndex = -1;
        private int selectedColumnIndex = -1;
        void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {   
                var FIELD = dataGridView1.Columns[e.ColumnIndex].DataPropertyName.ToUpper();
                

                if (FIELD == cFIELD && (e.RowIndex != selectedRowIndex || e.ColumnIndex != selectedColumnIndex))//e.RowIndex == -1 && 
                {
                    e.PaintBackground(e.CellBounds, true);
                    RenderColumnHeader(e.Graphics, e.CellBounds, e.CellBounds.Contains(hotSpot) ? hotSpotColor : backColor);
                    RenderColumnHeaderBorder(e.Graphics, e.CellBounds, e.ColumnIndex);
                    using (Brush brush = new SolidBrush(e.CellStyle.ForeColor))
                    {
                        using (StringFormat sf = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center })
                        {
                            e.Graphics.DrawString(e.FormattedValue.ToString(), e.CellStyle.Font, brush, e.CellBounds, sf);
                        }
                    }
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView1_CellPainting ", ex);
            }
        }
        Color hotSpotColor = Color.LightGreen;//For hover backcolor
        Color backColor = Color.LimeGreen;    //For backColor    
        Point hotSpot;
        private void RenderColumnHeader(Graphics g, Rectangle headerBounds, Color c)
        {
            int topHeight = 10;
            Rectangle topRect = new Rectangle(headerBounds.Left, headerBounds.Top + 1, headerBounds.Width, topHeight);
            RectangleF bottomRect = new RectangleF(headerBounds.Left, headerBounds.Top + 1 + topHeight, headerBounds.Width, headerBounds.Height - topHeight - 4);
            Color c1 = Color.FromArgb(180, c);
            using (SolidBrush brush = new SolidBrush(c1))
            {
                g.FillRectangle(brush, topRect);
                brush.Color = c;
                g.FillRectangle(brush, bottomRect);
            }
        }
        private void RenderColumnHeaderBorder(Graphics g, Rectangle headerBounds, int colIndex)
        {
            g.DrawRectangle(new Pen(Color.White, 0.1f), headerBounds.Left + 0.5f, headerBounds.Top + 0.5f, headerBounds.Width - 1f, headerBounds.Height - 1f);
            ControlPaint.DrawBorder(g, headerBounds, Color.Gray, 0, ButtonBorderStyle.Inset,
                                                   Color.Gray, 0, ButtonBorderStyle.Inset,
                                                 Color.Gray, colIndex != dataGridView1.ColumnCount - 1 ? 1 : 0, ButtonBorderStyle.Inset,
                                               Color.Gray, 1, ButtonBorderStyle.Inset);
        }
        //MouseMove event handler for your dataGridView1
        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            hotSpot = e.Location;
        }
        //MouseLeave event handler for your dataGridView1
        private void dataGridView1_MouseLeave(object sender, EventArgs e)
        {
            hotSpot = Point.Empty;
        }

        protected override void FormatGridViewExtern()
        {
            try
            {
                var selectDate = FilterControl.Date1;
                var daysInMonth = DateTime.DaysInMonth(selectDate.Year, selectDate.Month);
                for (int i = 1; i <= 31; i++)
                {
                    //DataGridViewComboBoxColumn cbc = new DataGridViewComboBoxColumn();
                    string fieldName = "CONG_" + ("00" + i).Right(2);
                    var column = dataGridView1.Columns[fieldName];
                    if (column != null)
                    {
                        if (i <= daysInMonth)
                        {
                            var cDate = new DateTime(selectDate.Year, selectDate.Month, i);
                            if (cDate.DayOfWeek == DayOfWeek.Saturday)
                            {
                                //column.DefaultCellStyle.Font = new Font(column.DefaultCellStyle.Font, FontStyle.Bold);
                                column.DefaultCellStyle.ForeColor = Color.Blue;
                            }
                            else if (cDate.DayOfWeek == DayOfWeek.Sunday)
                            {
                                //column.DefaultCellStyle.Font = new Font(column.DefaultCellStyle.Font, FontStyle.Bold);
                                column.DefaultCellStyle.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            column.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridViewExtern", ex);
            }
        }


        private DataGridViewCell edit_cell;
        private object begin_cell_value;
        void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            edit_cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            begin_cell_value = edit_cell.Value;
        }
        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!ExistRowInTable(edit_cell.Value.ToString().Trim()))
            {
                Lookup();
            }

            edit_cell.Value = edit_cell.Value.ToString().Trim().ToUpper();
            var end_cell_value = edit_cell.Value;
            //Nếu có thay đổi
            if (!object.Equals(begin_cell_value, end_cell_value))
            {
                if (UpdateCurrentCell(edit_cell))
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.Updated + ": " + edit_cell.Value);
                }
                else
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.UpdateFail);
                }
            }
        }

        bool UpdateCurrentCell(DataGridViewCell cell)
        {
            try
            {
                int nTime = 0;
                int nAmount = 0;
                SqlParameter[] plist =
                {
                    new SqlParameter("@nYear", cell.OwningRow.Cells["NAM"].Value),
                    new SqlParameter("@nPeriod", cell.OwningRow.Cells["KY"].Value),
                    new SqlParameter("@cEmployID", cell.OwningRow.Cells["MA_NS"].Value),
                    new SqlParameter("@cD", cell.OwningColumn.DataPropertyName.Right(2)),
                    new SqlParameter("@cDept", ""),
                    new SqlParameter("@cW", cell.Value),
                    new SqlParameter("@nTime", nTime),
                    new SqlParameter("@nAmount", nAmount),
                    new SqlParameter("@nUserID", V6Login.UserId)
                };
                if (V6BusinessHelper.ExecuteProcedureNoneQuery("VPH_SaveTimeSheet_Month", plist) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".UpdateCurrentCell", ex);
            }
            return false;
        }

        private AldmConfig LookupInfo;
        private string InitFilter = "";
        private string LookupInfo_F_NAME = "MA_CONG";
        private bool ExistRowInTable(string text)
        {
            try
            {
                if (!string.IsNullOrEmpty(LookupInfo_F_NAME))
                {
                    string tableName = LookupInfo.TABLE_NAME;
                    var filter = InitFilter;
                    if (!string.IsNullOrEmpty(filter)) filter = " and (" + filter + ")";

                    SqlParameter[] plist =
                    {
                        new SqlParameter("@text", text)
                    };
                    var tbl = V6BusinessHelper.Select(tableName, "*", LookupInfo_F_NAME + "=@text " + filter, "", "", plist).Data;

                    if (tbl != null && tbl.Rows.Count >= 1)
                    {
                        return true;
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage(ex.Message);
                return false;
            }
            return false;
        }

        private void DoLookup(bool multi = false)
        {
            if (LookupInfo.NoInfo) return;
            
            var filter = InitFilter;
            if (!string.IsNullOrEmpty(InitFilter)) filter = "and " + filter;
            var lookupForm = new V6LookupTextboxForm(null, edit_cell.Value.ToString().Trim(), LookupInfo, " 1=1 " + filter, LookupInfo_F_NAME, multi, false);
            //Looking = true;
            if (lookupForm.ShowDialog(this) == DialogResult.OK)
            {
                edit_cell.Value = lookupForm._senderText;
            }
            else
            {
                edit_cell.Value = DBNull.Value;
            }

        }

        private void Lookup(bool multi = false)
        {
            DoLookup(multi);
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                var txt = e.Control as TextBox;
                if (txt != null)
                {
                    V6ControlFormHelper.ApplyLookup(txt, "Prhlcong", "MA_CONG");
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView1_EditingControlShowing", ex);
            }
        }

        void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //cell.OwningRow.DefaultCellStyle.BackColor = Color.Red;
                //cell.OwningColumn.DefaultCellStyle.BackColor = Color.Red;
                //cell.Style.ApplyStyle(cell.OwningColumn.DefaultCellStyle);
                //cell.OwningColumn.DefaultCellStyle.ForeColor = Color.Red;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                
            }
        }

        
        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("SpaceBar: Chọn, F9: Gán tất cả nhân viên = ô đang chọn.");
        }

        protected override void MakeReport2()
        {
            base.MakeReport2();
            cFIELD = "CONG_" + ("00" + FilterControl.Date1.Day).Right(2);
        }

        #region ==== Xử lý F8 ====

        private bool F8Running;
        private string F8Error = "";
        private string F8ErrorAll = "";
        protected override void XuLyF8()
        {
            try
            {
                SaveSelectedCellLocation(dataGridView1);
                V6ControlFormHelper.ShowMessage("Chưa xử lý.");
                return;
                Timer tF8 = new Timer();
                tF8.Interval = 500;
                tF8.Tick += tF8_Tick;
                Thread t = new Thread(F8Thread);
                CheckForIllegalCrossThreadCalls = false;
                remove_list_g = new List<DataGridViewRow>();
                t.IsBackground = true;
                t.Start();
                tF8.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF8: " + ex.Message);
            }
        }

        private void F8Thread()
        {
            F8Running = true;
            F8ErrorAll = "";

            int i = 0;
            while (i < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {
                        //string soct = row.Cells["So_ct"].Value.ToString();
                        SqlParameter[] plist =
                        {
                            new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                            new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                            new SqlParameter("@Set_kieu_post", FilterControl.Kieu_post),
                            new SqlParameter("@UserID", V6Login.UserId)
                        };

                        V6BusinessHelper.ExecuteProcedureNoneQuery(_program + "F8", plist);
                        remove_list_g.Add(row);
                        //i--;
                    }
                }
                catch (Exception ex)
                {
                    F8Error += ex.Message;
                    F8ErrorAll += ex.Message;
                }

            }
            F8Running = false;
        }

        void tF8_Tick(object sender, EventArgs e)
        {
            if (F8Running)
            {
                var cError = F8Error;
                F8Error = F8Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F8 running "
                    + (cError.Length > 0 ? "Error: " : "")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();
                btnNhan.PerformClick();
                LoadSelectedCellLocation(dataGridView1);
                V6ControlFormHelper.SetStatusText("F8 finish "
                    + (F8ErrorAll.Length > 0 ? "Error: " : "")
                    + F8ErrorAll);

                V6ControlFormHelper.ShowMainMessage("F8 Xử lý xong!");
            }
        }
        #endregion xulyF8


        #region ==== Xử lý F9 ====
        
        private bool f9Running;
        private string f9Error = "";
        private string f9ErrorAll = "";
        protected override void XuLyF9()
        {
            try
            {
                SaveSelectedCellLocation(dataGridView1);
                edit_cell = dataGridView1.CurrentCell;
                var FIELD = edit_cell.OwningColumn.DataPropertyName.ToUpper();
                //CONG_01
                var check = false;
                if (FIELD.Length == 7 && FIELD.Left(5) == "CONG_")
                {
                    var day = ObjectAndString.ObjectToInt(FIELD.Right(2));
                    if (day >= 1 && day <= 31)
                    {
                        check = true;
                    }
                }
                if (!check) return;

                Timer tF9 = new Timer();
                tF9.Interval = 500;
                tF9.Tick += tF9_Tick;
                Thread t = new Thread(F9Thread);
                t.SetApartmentState(ApartmentState.STA);
                CheckForIllegalCrossThreadCalls = false;
                remove_list_g = new List<DataGridViewRow>();
                t.IsBackground = true;
                t.Start();
                tF9.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF9: " + ex.Message);
            }
        }

        int update_count = 0, update_fail = 0;
        private void F9Thread()
        {
            f9Running = true;
            f9ErrorAll = "";
            update_count = update_fail = 0;
            int i = 0;
            while(i<dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {
                        var cCell = row.Cells[edit_cell.ColumnIndex];
                        cCell.Value = edit_cell.Value;
                        if (UpdateCurrentCell(cCell))
                        {
                            update_count ++;
                        }
                        else
                        {
                            update_fail++;
                        }
                        
                        //remove_list_g.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    
                    f9Error += ex.Message;
                    f9ErrorAll += ex.Message;
                }

            }
            f9Running = false;
        }
        
        void tF9_Tick(object sender, EventArgs e)
        {
            if (f9Running)
            {
                var cError = f9Error;
                f9Error = f9Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F9 running "
                    + (cError.Length>0?"Error: ":"")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();
                btnNhan.PerformClick();
                LoadSelectedCellLocation(dataGridView1);
                V6ControlFormHelper.SetStatusText("F9 finish "
                    + (f9ErrorAll.Length > 0 ? "Error: " : "")
                    + f9ErrorAll);

                V6ControlFormHelper.ShowMainMessage(string.Format("F9 Xử lý xong! Success:{0} Fail:{1}", update_count, update_fail));
            }
        }
        #endregion xulyF9
        
    }
}
