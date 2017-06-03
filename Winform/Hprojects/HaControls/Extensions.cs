using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using H_Controls.Controls;

namespace H_Controls
{
    public static class Extensions
    {
        #region ---- ShowMessage ----
        
        public static DialogResult ShowMessage(this IWin32Window owner, string message)
        {
            return HMessage.Show(owner, message);
        }

        public static DialogResult ShowErrorMessage(this IWin32Window owner, string message)
        {
            return HMessage.Show(owner, message, null, MessageBoxButtons.OK, MessageBoxIcon.Error, 0);
        }

        public static DialogResult ShowErrorMessage(this IWin32Window owner, string message, string caption)
        {
            return HMessage.Show(owner, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error, 0);
        }

        public static DialogResult ShowConfirmMessage(this IWin32Window owner, string message, string caption = null, int dbutton = 0)
        {
            return HMessage.Show(owner, message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, dbutton);
        }

        public static DialogResult ShowConfirmCancelMessage(this IWin32Window owner, string message)
        {
            return HMessage.Show(owner, message, null, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, 3);
        }

        public static void ShowWarningMessage(this IWin32Window owner, string message)
        {
            HMessage.Show(owner, message, "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning, 0);
        }

        public static DialogResult ShowInfoMessage(this IWin32Window owner, string message)
        {
            //return HControlHelper.ShowInfoMessage(message, owner);
            return HMessage.Show(owner, message, "Thông tin!", MessageBoxButtons.OK, MessageBoxIcon.Information, 0);
        }

        #endregion showmessage

        public static void Alert(this Control c)
        {
            Timer t = new Timer()
            {
                Interval = 1000
            };
            var oldBackColor = c.BackColor;
            var tickCount = 0;
            t.Tick += (sender, e) =>
            {
                tickCount++;
                if (tickCount < 4)
                {
                    c.BackColor = c.BackColor == oldBackColor ? Color.Red : oldBackColor;
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

        public static string Left(this string str, int length)
        {
            return length >= str.Length ? str : str.Substring(0, length);
        }
        public static string Right(this string str, int length)
        {
            return length >= str.Length ? str : str.Substring(str.Length - length, length);
        }

        public static int ToExcelColumnIndex(this string str)
        {
            //max = IV
            if (string.IsNullOrEmpty(str)) throw new Exception("Không có giá trị!");
            if (str.Length > 2) throw new Exception("Quá dài!");

            str = str.ToUpper();
            if (str.Length == 1)
                return str[0] - 'A';

            const int max = ('Z' - 'A' + 1) * ('I' - 'A' + 1) + ('V' - 'A');
            var result = ('Z' - 'A' + 1) * (str[0] - 'A' + 1) + (str[1] - 'A');
            if (result > max) throw new Exception("Giá trị lớn nhất là IV = " + max + ". (A=0)");
            return result;
        }

        public static string ToStringWithComma(this string[] array)
        {
            var result = "";
            if (array != null && array.Length > 0)
            {
                foreach (string s in array)
                {
                    result += "," + s;
                }
                if (result.Length > 0) result = result.Substring(1);
            }
            return result;
        }

        ///// <summary>
        ///// Hàm mở rộng.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="target"></param>
        ///// <param name="source"></param>
        //public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
        //{
        //    if (target == null)
        //        throw new ArgumentNullException("target");
        //    if (source == null)
        //        throw new ArgumentNullException("source");
        //    foreach (var element in source)
        //    {
        //        try
        //        {
        //            target.Add(element);
        //        }
        //        catch
        //        {
        //            // ignored
        //        }
        //    }
        //}

        #region ==== DataGridView ====

        /// <summary>
        /// Hàm mở rộng. Lấy danh sách từ điển dữ liệu.
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static List<SortedDictionary<string, object>> GetData(this DataGridView grid)
        {
            var result = new List<SortedDictionary<string, object>>();
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
        public static List<SortedDictionary<string, object>> GetData(this DataGridView grid, string sttRec)
        {
            var result = new List<SortedDictionary<string, object>>();
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
        public static List<SortedDictionary<string, object>> GetDataDictionaryList_Auto(this DataGridView grid, string sttRec)
        {
            var result = new List<SortedDictionary<string, object>>();
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

        public static SortedDictionary<string, object> ToDataDictionary(this DataGridViewRow row)
        {
            var dataDic = new SortedDictionary<string, object>();
            if (row == null) return dataDic;
            for (int i = 0; i < row.DataGridView.Columns.Count; i++)
            {
                dataDic.Add(row.DataGridView.Columns[i].DataPropertyName.ToUpper(), row.Cells[i].Value);
            }
            return dataDic;
        }

        public static List<SortedDictionary<string, object>> GetSelectedData(this DataGridView grid)
        {
            var result = new List<SortedDictionary<string, object>>();

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

        public static SortedDictionary<string, object> GetCurrentRowData(this DataGridView grid)
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
            row.DefaultCellStyle.BackColor = Color.FromArgb(247, 192, 91);

            if (row.DataGridView.Columns.Contains("Tag"))
            {
                row.Cells["Tag"].Value = "x";
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

            if (row.DataGridView.Columns.Contains("Tag"))
            {
                row.Cells["Tag"].Value = "";
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
        }
        /// <summary>
        /// Hàm mở rộng, chọn theo điều kiện
        /// </summary>
        /// <param name="row"></param>
        /// <param name="select"></param>
        public static void Select(this DataGridViewRow row, bool select)
        {
            if (select) row.Select();
            else row.UnSelect();
        }
        /// <summary>
        /// Hàm mở rộng, Chọn tất cả các dòng
        /// </summary>
        /// <param name="gridView"></param>
        public static void SelectAllRow(this DataGridView gridView)
        {
            foreach (DataGridViewRow row in gridView.Rows)
            {
                row.Select();
            }
        }
        /// <summary>
        /// Hàm mở rộng, chọn tất cả dòng theo điều kiện
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="select"></param>
        public static void SelectAllRow(this DataGridView gridView, bool select)
        {
            if (select)
            {
                gridView.SelectAllRow();
            }
            else
            {
                gridView.UnSelectAllRow();
            }
        }
        /// <summary>
        /// Hàm mở rộng, bỏ chọn tất cả dòng
        /// </summary>
        /// <param name="gridView"></param>
        public static void UnSelectAllRow(this DataGridView gridView)
        {
            foreach (DataGridViewRow row in gridView.Rows)
            {
                row.UnSelect();
            }
        }

        /// <summary>
        /// Kiểm tra Tag.Trim() != "" là được chọn
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

        public static void ViewDataToNewForm(this DataGridView gridview, string title = "Data")
        {
            try
            {
                //var container = gridview.Parent;
                //var oldLocation = gridview.Location;
                //var oldSize = gridview.Size;
                //var oldDock = gridview.Dock;
                //var child = gridview;
                var data = gridview.DataSource;
                //if (container is Form)
                {
                    //((Form)container).Close();
                }
                //else
                {

                    var f = new BaseForm
                    {
                        WindowState = FormWindowState.Normal,
                        MaximizeBox = true,
                        MinimizeBox = false,
                        ShowInTaskbar = false,
                        //FormBorderStyle = FormBorderStyle.None
                        Text = title,
                        Size = new Size(800, 600)
                    };

                    //child.Location = new Point(0, 0);
                    //child.Dock = DockStyle.Fill;
                    DataGridView newGridView = new DataGridView
                    {
                        AllowUserToAddRows = false,
                        AllowUserToDeleteRows = false,
                        Dock = DockStyle.Fill,
                        ReadOnly = true
                    };


                    f.Controls.Add(newGridView);
                    newGridView.DataSource = data;

                    //f.FormClosing += (se, a) =>
                    //{
                    //    child.Location = oldLocation;
                    //    child.Size = oldSize;
                    //    child.Dock = oldDock;
                    //    container.Controls.Add(child);
                    //};
                    //child.Disposed += (s, b) =>
                    //{
                    //    if (!f.IsDisposed)
                    //    {
                    //        f.Dispose();
                    //    }
                    //};

                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                HControlHelper.ShowErrorMessage("ViewDataToNewForm: " + ex.Message);
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

        #endregion datagridview
        
        #region ==== Control ====

        public static void LoadComboboxLookup(this Control control)
        {
            if (control is LookupComboBox)
            {
                ((LookupComboBox) control).StartLoadLookupData();

            }
            else if(control.Controls.Count>0)
            {
                foreach (Control c in control.Controls)
                {
                    c.LoadComboboxLookup();
                }
            }
        }

        /// <summary>
        /// Gán Enabled = false và Tag = "disable" 
        /// </summary>
        /// <param name="control"></param>
        public static void DisableTag(this Control control)
        {
            control.Enabled = false;
            control.Tag = "disable";
        }

        public static void EnableTag(this Control control)
        {
            control.Enabled = true;
            control.Tag = null;
        }

        public static void ReadOnlyTag(this TextBox control, bool readOnly = true)
        {
            control.ReadOnly = readOnly;
            control.Tag = readOnly ? "readonly" : null;
        }

        public static void ReadOnlyTag(this DataGridView control, bool readOnly = true)
        {
            control.ReadOnly = readOnly;
            control.Tag = readOnly ? "readonly" : null;
        }

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
            control.Tag = "hide";
        }

        /// <summary>
        /// Hiển thị control ra một form riêng.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="title"></param>
        public static void ToFullForm(this Control control, string title = "Form")
        {
            var container = control.Parent;
            var oldLocation = control.Location;
            var oldSize = control.Size;
            var oldDock = control.Dock;
            var child = control;
            //if (container is Form)
            {
                //((Form)container).Close();
            }
            //else
            {

                var f = new BaseForm
                {
                    WindowState = FormWindowState.Normal,
                    MaximizeBox = true,
                    MinimizeBox = false,
                    ShowInTaskbar = false,
                    //FormBorderStyle = FormBorderStyle.None
                    Text = title,
                    Size = new Size(800, 600)
                };

                child.Location = new Point(0, 0);
                child.Dock = DockStyle.Fill;
                f.Controls.Add(child);
                f.FormClosing += (se, a) =>
                {
                    child.Location = oldLocation;
                    child.Size = oldSize;
                    child.Dock = oldDock;
                    container.Controls.Add(child);
                };
                child.Disposed += (s, b) =>
                {
                    if (!f.IsDisposed)
                    {
                        f.Dispose();
                    }
                };

                f.ShowDialog();
            }
        }



        #endregion control
    }
}
