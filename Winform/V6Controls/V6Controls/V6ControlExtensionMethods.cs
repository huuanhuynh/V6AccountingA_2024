﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using V6Controls.Controls;
using V6Controls.Forms;
using V6Controls.Forms.Viewer;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls
{
    public static class V6ControlExtensionMethods
    {

        #region ==== TreeListView ====
        public static SortedDictionary<string, string> ToNhanSuDictionary(this TreeListViewItem item)
        {
            if (item != null)
            {
                var result = new SortedDictionary<string, string>();
                for (int i = 0; i < item.SubItems.Count; i++)
                {
                    var sub = item.SubItems[i];
                    result.Add(item.TreeListView.Columns[i].Name.ToUpper(), sub.Text);
                }

                if (item.Level == 0)
                {
                    result["MA_DVCS"] = item.Text;
                }
                else if (item.Level == 1)
                {
                    result["MA_BPNS"] = item.Text;
                    result["MA_DVCS"] = item.Parent.Text;
                }
                else if (item.Level == 2)
                {
                    result["MA_NS"] = item.Text;
                    result["MA_BPNS"] = item.Parent.Text;
                    result["MA_DVCS"] = item.Parent.Parent.Text;
                }

                return result;
            }
            return null;
        }
        #endregion treelistview

        #region ==== DataGridView ====

        public static DataGridViewRow GetFirstSelectedRow(this DataGridView grid)
        {
            DataGridViewRow row = grid.CurrentRow;
            if (row == null)
            {
                if (grid.SelectedRows.Count == 1)
                    row = grid.SelectedRows[0];
                else if (grid.SelectedCells.Count > 0)
                    row = grid.Rows[grid.SelectedCells[0].RowIndex];
            }
            return row;
        }

        /// <summary>
        /// Hàm mở rộng. Lấy danh sách từ điển dữ liệu.
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static List<IDictionary<string, object>> GetData(this DataGridView grid)
        {
            var result = new List<IDictionary<string, object>>();
            for (int i = 0; i < grid.RowCount; i++)
            {
                if (grid.Rows[i].IsNewRow) continue;
                var row = grid.Rows[i].ToDataDictionary();
                result.Add(row);
            }
            return result;
        }
        /// <summary>
        /// Hàm mở rộng. Lấy danh sách từ điển dữ liệu, gán lại giá trị trường STT_REC.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="sttRec">Gán vào trường STT_REC</param>
        /// <returns></returns>
        public static List<IDictionary<string, object>> GetData(this DataGridView grid, string sttRec)
        {
            var result = new List<IDictionary<string, object>>();
            for (int i = 0; i < grid.RowCount; i++)
            {
                if (grid.Rows[i].IsNewRow) continue;
                var row = grid.Rows[i].ToDataDictionary();
                row["STT_REC"] = sttRec;
                result.Add(row);
            }
            return result;
        }

        /// <summary>
        /// Lấy danh sách từ điển dữ liệu với SttRec0 gán lại theo thứ tự từ 00001
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="sttRec"></param>
        /// <returns></returns>
        public static List<IDictionary<string, object>> GetDataDictionaryList_Auto(this DataGridView grid, string sttRec)
        {
            var result = new List<IDictionary<string, object>>();
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                if (grid.Rows[i].IsNewRow) continue;

                var row = grid.Rows[i].ToDataDictionary();
                if (sttRec != null)
                    row["STT_REC"] = sttRec;
                row["STT_REC0"] = ("00000" + (i + 1)).Right(5);
                result.Add(row);
            }
            return result;
        }

        /// <summary>
        /// Gán header cho các cột của gridview theo ID trong corplan2
        /// </summary>
        /// <param name="grid"></param>
        public static void SetCorplan2(this DataGridView grid)
        {
            var listColumn =
                (from DataGridViewColumn column in grid.Columns select column.DataPropertyName).ToList();
            var header_dictionary = CorpLan2.GetFieldsHeader(listColumn, V6Setting.Language);

            foreach (DataGridViewColumn column in grid.Columns)
            {
                var FIELD = column.DataPropertyName.ToUpper();
                if (header_dictionary.ContainsKey(FIELD))
                    column.HeaderText = header_dictionary[FIELD];
            }
        }

        /// <summary>
        /// Ẩn tất cả các cột ngoại trừ Frozen
        /// </summary>
        /// <param name="grid"></param>
        public static void HideAllColumns(this DataGridView grid)
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.Visible = column.Frozen;
            }
        }
        
        /// <summary>
        /// Kiểm tra nếu không có trường dữ liệu hoặc có 1 dữ liệu rỗng, 0, null thì trả về false.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fields">Các trường dữ liệu cần kiểm tra, chú ý có phân biệt hoa thường.</param>
        /// <returns></returns>
        public static bool HaveValues(this IDictionary<string, object> data, IList<string> fields)
        {
            if (data != null)
            {
                foreach (string field in fields)
                {
                    if (!data.ContainsKey(field))
                    {
                        return false;
                    }
                    
                    var value = data[field];
                    if (value == null || value is DBNull) return false;
                    if (ObjectAndString.IsNumberType(value.GetType()))
                    {
                        if (ObjectAndString.ObjectToDecimal(value) == 0) return false;
                    }
                    if ("" + value == "") return false;
                }
                return true;
            }
            
            return false;
        }

        public static List<IDictionary<string, object>> GetSelectedData(this DataGridView grid)
        {
            var result = new List<IDictionary<string, object>>();

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                if (grid.Rows[i].IsNewRow) continue;
                DataGridViewRow crow = grid.Rows[i];
                if (crow.IsSelect())
                {
                    var row = new SortedDictionary<string, object>();
                    for (int j = 0; j < grid.ColumnCount; j++)
                    {
                        row[grid.Columns[j].DataPropertyName.ToUpper()] = crow.Cells[j].Value;
                    }
                    result.Add(row);
                }
            }

            return result;
        }

        public static IDictionary<string, object> GetCurrentRowData(this DataGridView grid)
        {
            DataGridViewRow crow = grid.CurrentRow;
            if (crow != null)
            {
                var row = new SortedDictionary<string, object>();
                for (int j = 0; j < grid.ColumnCount; j++)
                {
                    row[grid.Columns[j].DataPropertyName.ToUpper()] = crow.Cells[j].Value;
                }
                return row;
            }
            return null;
        }


        /// <summary>
        /// Hàm mở rộng, chọn dòng, đổi thành màu cam, Gán Tag = "x"
        /// </summary>
        /// <param name="row"></param>
        public static void Select(this DataGridViewRow row)
        {
            row.DefaultCellStyle.BackColor = Color. FromArgb(247, 192, 91);
            
            if (row.DataGridView.Columns.Contains( "Tag"))
            {
                row.Cells[ "Tag"].Value = "x";
            }
            else // Hoặc dùng HeaderCell.Tag
            {
                row.HeaderCell.Tag = "x";
            }

            if (row.DataGridView.CurrentRow == row)
            {
                row.DataGridView.DefaultCellStyle.SelectionBackColor = Color.Brown;
                row.DataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            }

            if (row.DataGridView is V6ColorDataGridView) ((V6ColorDataGridView)row.DataGridView).OnRowSelectChanged(new SelectRowEventArgs(row){Select = true});
        }
        /// <summary>
        /// Hàm mở rộng, bỏ chọn, gán Tag = "";
        /// </summary>
        /// <param name="row"></param>
        public static void UnSelect(this DataGridViewRow row)
        {
            if (row.Index % 2 == 0)
                row.DefaultCellStyle.BackColor = row.DataGridView
                    .RowsDefaultCellStyle.BackColor;
            else
                row.DefaultCellStyle.BackColor = row.DataGridView
                    .AlternatingRowsDefaultCellStyle.BackColor;

            if (row.DataGridView.Columns.Contains( "Tag"))
            {
                row.Cells[ "Tag"].Value = "";
            }
            else // Hoặc dùng HeaderCell.Tag
            {
                row.HeaderCell.Tag = "";
            }

            if (row.DataGridView.CurrentRow == row)
            {
                row.DataGridView.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                row.DataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            }

            if(row.DataGridView is V6ColorDataGridView) ((V6ColorDataGridView)row.DataGridView).OnRowSelectChanged(new SelectRowEventArgs(row));
        }

        /// <summary>
        /// Hàm mở rộng, chọn theo điều kiện
        /// </summary>
        /// <param name="row"></param>
        /// <param name="select"></param>
        public static void Select(this DataGridViewRow row, bool select)
        {
            if(select) row.Select();
            else row.UnSelect();
        }
        
        /// <summary>
        /// Kiểm tra cell [Tag].Trim() != "" hoặc HeaderCell.Tag là được chọn
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static bool IsSelect(this DataGridViewRow row)
        {
            //Có 2 trường hợp, Nếu có Cột Tag
            if (row.DataGridView.Columns.Contains("Tag"))
            {
                var tag = (row.Cells["Tag"].Value ?? "").ToString().Trim();
                return tag != "";
            }
            else // Hoặc dùng HeaderCell.Tag
            {
                if (!(row.HeaderCell.Tag is string)) return false;
                var tag = row.HeaderCell.Tag.ToString().Trim();
                return tag != "";
            }
        }

        public static void ChangeSelect(this DataGridViewRow row)
        {
            row.Select(!row.IsSelect());
        }

        /// <summary>
        /// Hiển thị dữ liệu của DataGridView lên 1 form mới.
        /// </summary>
        /// <param name="gridview"></param>
        /// <param name="title"></param>
        public static void ViewDataToNewForm(this DataGridView gridview, string title = "Data")
        {
            try
            {
                var data = gridview.DataSource;
                DataViewerForm dataViewer = new DataViewerForm(data);
                dataViewer.ShowDialog(gridview);
            }
            catch (Exception ex)
            {
                gridview.ShowErrorMessage("ViewDataToNewForm: " + ex.Message);
            }
        }

        /// <summary>
        /// Hiển thị Ucontrol lên form. Có xác nhận đóng khi nhấn X
        /// </summary>
        /// <param name="control">Đối tượng hiển thị trên form.</param>
        /// <param name="owner">Form chủ, không có để null.</param>
        /// <param name="title">Tiêu đề trên form.</param>
        /// <param name="fullScreen">Mở rộng form khi hiển thị</param>
        /// <param name="dialog">Hiển thị form kiểu dialog.</param>
        /// <param name="closeConfirm">Xác nhận khi đóng form.</param>
        public static DialogResult ShowToForm(this UserControl control, IWin32Window owner, string title = "Form",
            bool fullScreen = false, bool dialog = true, bool closeConfirm = true)
        {
            try
            {
                var f  = new V6Form
                {
                    Text = title,
                    AutoSize = true,
                    FormBorderStyle = FormBorderStyle.Sizable,//.FixedSingle,
                    Size = new Size(600, 300),
                    MinimumSize = new Size(600, 300),
                };
                if (fullScreen) f.WindowState = FormWindowState.Maximized;
                if (closeConfirm)
                    f.FormClosing += (sender, e) =>
                    {
                        if (!control.IsDisposed && !control.Disposing   // không hỏi khi form control đã tự hủy.
                            && !f.IsDisposed && !f.Disposing
                            && f.ShowConfirmMessage(V6Text.CloseConfirm) != DialogResult.Yes)
                        {
                            e.Cancel = true;
                        }
                    };
                
                f.Controls.Add(control);
                control.Dock = DockStyle.Fill;
                control.Disposed += delegate
                {
                    if (!f.IsDisposed)
                    {
                        f.Close();
                        //f.Dispose();        // còn form tàng hình.                
                    }
                };
                f.KeyPreview = true;
                
                if (dialog)
                {
                    return f.ShowDialog(owner);
                }
                else
                {
                    f.Show(owner);
                }
            }
            catch (Exception ex)
            {
                if (ex is ObjectDisposedException) return DialogResult.Abort;
                control.ShowErrorMessage("UserControl ShowToForm: " + ex.Message);
            }
            //Giả không có result (Abort ít dùng).
            return DialogResult.Abort;
        }

        public static void ShowFullScreen(this UserControl child)
        {
            var container = child.Parent;
            var f  = new V6Form
            {
                WindowState = FormWindowState.Maximized,
                ShowInTaskbar = false,
                FormBorderStyle = FormBorderStyle.None,                
                MinimumSize = new Size(600, 300),
            };
            f.Controls.Add(child);
            f.FormClosing += (se, a) =>
            {
                container.Controls.Add(child);
                ////tsFull.Enabled = true;
                //tsFull.Image = Properties.Resources.ZoomIn24;
                //tsFull.Text = V6Text.ZoomIn;
            };
            //tsFull.Enabled = false;
            //tsFull.Image = Properties.Resources.ZoomOut24;
            //tsFull.Text = V6Text.ZoomOut;

            f.ShowDialog(container);
        }

        #endregion datagridview

        

        #region ==== Control ====

        /// <summary>
        /// Báo động bằng BackColor.
        /// </summary>
        /// <param name="c"></param>
        public static void Alert(this Control c)
        {
            Timer t = new Timer()
            {
                Interval = 800
            };
            var oldBackColor = c.BackColor;
            var alertColor = Color.Red == oldBackColor ? Color.Yellow : Color.Red;
            var tickCount = 0;
            t.Tick += (sender, e) =>
            {
                tickCount++;
                if (tickCount < 6)
                {
                    c.BackColor = c.BackColor == oldBackColor ? alertColor : oldBackColor;
                }
                else
                {
                    t.Stop();
                    c.BackColor = oldBackColor;
                    t.Dispose();
                }
            };
            t.Start();
        }


        public static void SetAllVvarBrotherFields(this Control control)
        {
            try
            {
                if (control is V6VvarTextBox)
                {
                    ((V6VvarTextBox)control).SetBrotherFormData();
                }
                else if (control.Controls.Count > 0)
                {
                    foreach (Control c in control.Controls)
                    {
                        c.SetAllVvarBrotherFields();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("ExtensionMethods SetAllVvarBrotherFields " + ex.Message, "V6Controls");
            }
        }

        public static void AddTagString(this Control control, string tagString)
        {
            V6ControlFormHelper.AddTagString(control, tagString);
        }

        public static void RemoveTagString(this Control control, string tagString)
        {
            V6ControlFormHelper.RemoveTagString(control, tagString);
        }

        /// <summary>
        /// Gán Enabled = false và Tag += "disable" 
        /// </summary>
        /// <param name="control"></param>
        public static void DisableTag(this Control control)
        {
            control.EnableTag(false);
        }

        public static void EnableTag(this Control control, bool enable=true)
        {
            control.Enabled = enable;
            if(enable) control.RemoveTagString("disable");
            else control.AddTagString("disable");
        }

        /// <summary>
        /// Gán Readonly và tag.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="read_only"></param>
        public static void ReadOnlyTag(this Control control, bool read_only = true)
        {
            if (control is TextBox) ((TextBox)control).ReadOnlyTag(read_only);
            else if (control is RichTextBox) ((RichTextBox)control).ReadOnlyTag(read_only);
            else if (control is DataGridView) ((DataGridView)control).ReadOnlyTag(read_only);
            else if (control is FileButton) ((FileButton)control).ReadOnly = read_only;
        }

        /// <summary>
        /// Set readonly và gán tag đánh dấu cho control.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="read_only"></param>
        public static void ReadOnlyTag(this TextBox control, bool read_only = true)
        {
            if (read_only)
            {
                control.ReadOnly = true;
                control.AddTagString("readonly");
            }
            else
            {
                control.ReadOnly = false;
                control.RemoveTagString("readonly");
            }
        }

        public static void ReadOnlyTag(this RichTextBox control, bool read_only = true)
        {
            if (read_only)
            {
                control.ReadOnly = true;
                control.AddTagString("readonly");
            }
            else
            {
                control.ReadOnly = false;
                control.RemoveTagString("readonly");
            }
        }

        public static void ReadOnlyTag(this DataGridView control, bool read_only = true)
        {
            if (read_only) control.AddTagString("readonly");
            else control.RemoveTagString("readonly");
            control.ReadOnly = read_only;
        }

        /// <summary>
        /// Gán Visible = true và tag = null
        /// </summary>
        /// <param name="control"></param>
        public static void VisibleTag(this Control control)
        {
            control.Visible = true;
            control.Tag = null;
        }
        /// <summary>
        /// Gán Visible = false và tag = "hide"
        /// </summary>
        /// <param name="control"></param>
        public static void InvisibleTag(this Control control)
        {
            control.Visible = false;
            control.AddTagString("hide");
        }

        /// <summary>
        /// Kiểm tra control chưa bị khóa tag.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool IsVisibleTag(this Control control)
        {
            return !control.IsTag("hide") && !control.IsTag("invisible");
        }
        /// <summary>
        /// Kiểm tra control đã bị khóa tag.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool IsInvisibleTag(this Control control)
        {
            return control.IsTag("hide") || control.IsTag("invisible");
        }

        public static bool IsTag(this Control control, string tag)
        {
            var tagString = String.Format(";{0};", control.Tag ?? "");
            var isTag = tagString.Contains(";"+tag+";");
            return isTag;
        }

        /// <summary>
        /// Hiển thị control ra một form riêng. (vd gridView)
        /// </summary>
        /// <param name="control"></param>
        /// <param name="title"></param>
        public static void ToFullForm(this Control control, string title = "V6Form")
        {
            var container = control.Parent;
            var oldLocation = control.Location;
            var oldSize = control.Size;
            var oldDock = control.Dock;
            var child = control;
            bool hardExit = false;
            //if (container is Form)
            {
                //((Form)container).Close();
            }
            //else
            {

                var f  = new V6Form
                {
                    WindowState = FormWindowState.Normal,
                    MaximizeBox = true,
                    MinimizeBox = false,
                    ShowInTaskbar = false,
                    //FormBorderStyle = FormBorderStyle.None
                    Text = title,
                    Size = new Size(800,600),
                    MinimumSize = new Size(600, 300),
                };

                child.Location = new Point(0,0);
                child.Dock = DockStyle.Fill;
                f.Controls.Add(child);
                //f.Load += delegate
                //{
                //    if (loadCall != null) loadCall();
                //};
                f.FormClosing += (se, a) =>
                {
                    //if (!f.IsDisposed && f.ShowConfirmMessage(V6Text.CloseConfirm) != DialogResult.Yes)
                    //{
                    //    a.Cancel = true;
                    //    return;
                    //}
                    
                    child.Location = oldLocation;
                    child.Size = oldSize;
                    child.Dock = oldDock;
                    if(container != null) container.Controls.Add(child);
                    else child.Dispose();
                };
                child.Disposed += (s, b) =>
                {
                    if (!f.IsDisposed)
                    {
                        f.Dispose();
                    }
                };
                f.KeyPreview = true;
                f.KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Escape)
                    {
                        hardExit = true;
                        f.Close();
                    }
                };

                f.ShowDialog(container);
            }
        }

        #region ---- ShowMessage ----
        /// <summary>
        /// Hiển thị hộp thông báo.
        /// </summary>
        /// <param name="owner">Nơi hình thành.</param>
        /// <param name="message">Nội dung thông báo.</param>
        /// <param name="showTime">Thời gian tự ẩn tính bằng % giây (100=1s).</param>
        /// <returns></returns>
        public static DialogResult ShowMessage(this IWin32Window owner, string message, int showTime = 0)
        {
            return V6Message.Show(message, showTime, owner);
        }

        /// <summary>
        /// Hiện thông báo lỗi.
        /// </summary>
        /// <param name="owner">Control gây lỗi</param>
        /// <param name="message">Thông báo lỗi</param>
        /// <param name="showTime">Thời gian tự đóng %s</param>
        /// <returns></returns>
        public static DialogResult ShowErrorMessage(this IWin32Window owner, string message, int showTime = 0)
        {
            return V6ControlFormHelper.ShowErrorMessage(message, owner, showTime);
        }

        /// <summary>
        /// Hiện thông báo lỗi.
        /// </summary>
        /// <param name="owner">Control gây lỗi</param>
        /// <param name="message">Thông báo lỗi</param>
        /// <param name="caption">Tiêu đề thông báo</param>
        /// <param name="showTime">Thời gian tự đóng %s</param>
        /// <returns></returns>
        public static DialogResult ShowErrorMessage(this IWin32Window owner, string message, string caption, int showTime = 0)
        {
            return V6ControlFormHelper.ShowErrorMessage(message, caption, owner, showTime);
        }

        public static DialogResult ShowErrorException(this IWin32Window owner, Exception ex)
        {
            string address = owner.GetType() + "." + ex.TargetSite;
            return V6ControlFormHelper.ShowErrorException(address, ex, null, owner, 0);
        }

        /// <summary>
        /// Hiển thị lỗi dạng address: ex.Message
        /// Và ghi log dạng chi tiết hơn.
        /// </summary>
        /// <param name="owner">Control đang gọi hàm này (this).</param>
        /// <param name="address">Vị trí hàm gây lỗi (tên_class.tên_hàm hoặc GetType()+.tên_hàm).</param>
        /// <param name="ex">Lỗi được bắt lại bởi try catch.</param>
        /// <param name="showTime">Thời gian tự đóng thông báo.</param>
        /// <returns></returns>
        public static DialogResult ShowErrorException(this IWin32Window owner, string address, Exception ex, int showTime = 0)
        {
            return V6ControlFormHelper.ShowErrorException(address, ex, null, owner, showTime);
        }

        /// <summary>
        /// Hiển thị lỗi dạng address: ex.Message
        /// Và ghi log dạng chi tiết hơn.
        /// </summary>
        /// <param name="owner">Control đang gọi hàm này (this).</param>
        /// <param name="address">Vị trí hàm gây lỗi (tên_class.tên_hàm hoặc GetType()+.tên_hàm).</param>
        /// <param name="ex">Lỗi được bắt lại bởi try catch.</param>
        /// <param name="caption">Tiêu đề hộp thông báo.</param>
        /// <param name="showTime">Thời gian tự đóng thông báo.</param>
        /// <returns></returns>
        public static DialogResult ShowErrorException(this IWin32Window owner, string address, Exception ex, string caption, int showTime = 0)
        {
            return V6ControlFormHelper.ShowErrorException(address, ex, caption, owner, showTime);
        }

        public static DialogResult ShowConfirmMessage(this IWin32Window owner, string message)
        {
            return V6Message.Show(message, null, 0, MessageBoxButtons.YesNo, MessageBoxIcon.Question, 0, owner);
        }
        /// <summary>
        /// Return yes/no
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="message"></param>
        /// <param name="caption"></param>
        /// <param name="showTime"></param>
        /// <param name="dbutton"></param>
        /// <returns>yes/no</returns>
        public static DialogResult ShowConfirmMessage(this IWin32Window owner, string message, string caption, int showTime, int dbutton)
        {
            return V6Message.Show(message, caption, showTime, MessageBoxButtons.YesNo, MessageBoxIcon.Question, dbutton, owner);
        }

        public static DialogResult ShowConfirmMessage(this IWin32Window owner, string message, string caption, int showTime = 0, int dbutton = 0, string acc_description = ".")
        {
            return V6Message.Show(message, caption, showTime, MessageBoxButtons.YesNo, MessageBoxIcon.Question, dbutton, owner, acc_description);
        }

        public static DialogResult ShowConfirmCancelMessage(this IWin32Window owner, string message, int showTime = 0)
        {
            return V6Message.Show(message, null, showTime, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, 3, owner);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="message"></param>
        /// <param name="showTime"></param>
        /// <param name="accessible_description">Mã ngôn ngữ để có thể sửa được nội dung bằng chuột phải.</param>
        /// <returns></returns>
        public static DialogResult ShowConfirmCancelMessage(this IWin32Window owner, string message, int showTime, string accessible_description)
        {
            return V6Message.Show(message, null, showTime, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, 3, owner, accessible_description);
        }


        public static void ShowWarningMessage(this IWin32Window owner, string message)
        {
            V6Message.Show(message, V6Setting.Language == "V" ? "Cảnh báo!" : "Warning!", 0, MessageBoxButtons.OK, MessageBoxIcon.Warning, owner);
        }
        public static void ShowWarningMessage(this IWin32Window owner, string message, string acc_description)
        {
            V6Message.Show(message, V6Setting.Language == "V" ? "Cảnh báo!" : "Warning!", 0, MessageBoxButtons.OK, MessageBoxIcon.Warning, owner, acc_description);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="message"></param>
        /// <param name="showTime">Phần trăm giây</param>
        public static void ShowWarningMessage(this IWin32Window owner, string message, int showTime)
        {
            V6Message.Show(message, V6Setting.Language == "V" ? "Cảnh báo!" : "Warning!", showTime, MessageBoxButtons.OK, MessageBoxIcon.Warning, owner);
        }

        public static void ShowWarningMessage(this IWin32Window owner, string message, int showTime, string acc_description)
        {
            V6Message.Show(message, V6Setting.Language == "V" ? "Cảnh báo!" : "Warning!", showTime, MessageBoxButtons.OK, MessageBoxIcon.Warning, owner, acc_description);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="message"></param>
        /// <param name="showTime">Thời gian hiển thị tính bằng %giây</param>
        /// <returns></returns>
        public static DialogResult ShowInfoMessage(this IWin32Window owner, string message, int showTime = 0)
        {
            return V6ControlFormHelper.ShowInfoMessage(message, showTime, owner);
        }

        #endregion showmessage

        /// <summary>
        /// Ghi log.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="address">GetType() + ".Method"</param>
        /// <param name="ex">ex</param>
        /// <param name="logFile"></param>
        public static void WriteExLog(this IWin32Window owner, string address, Exception ex, string logFile = "V6Log")
        {
            V6ControlFormHelper.WriteExLog(address, ex, logFile);
        }
        
        public static void WriteToLog(this IWin32Window owner, string address, string message, string logFile = "V6Log")
        {
            V6ControlFormHelper.WriteToLog(address, message, logFile);
        }


        #endregion control

    }
}
