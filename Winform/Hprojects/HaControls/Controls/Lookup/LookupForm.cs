using System;
using System.Collections.Generic;
using System.Windows.Forms;
using H_Utility.Helper;

namespace H_Controls.Controls.Lookup
{
    public partial class LookupForm : BaseForm
    {
        public LookupForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Khởi tạo form lookup (tìm kiếm và chọn).
        /// </summary>
        /// <param name="tableName">Tên bảng dữ liệu sẽ lookup</param>
        /// <param name="lookupField">Trường dữ liệu cầnn lookup</param>
        /// <param name="showFields">Những trường sẽ hiện lên</param>
        /// <param name="showHeaders">Những tên trường tương ứng</param>
        /// <param name="searchText">Giá trị tìm kiếm ban đầu</param>
        public LookupForm(string tableName, string lookupField, string showFields, string showHeaders, string searchText)
        {
            InitializeComponent();
            pagingGridView1.TableName = tableName;
            pagingGridView1.DisplayFields = showFields;
            pagingGridView1.DisplayHeaders = showHeaders;
            pagingGridView1.IdField = lookupField;
            pagingGridView1.SortField = lookupField;

            pagingGridView1.FilterAnyFields(searchText);
        }

        public event AcceptData AcceptDataEvent;
        protected virtual void OnAcceptDataEvent(SortedDictionary<string, object> data)
        {
            var handler = AcceptDataEvent;
            if (handler != null) handler(data);
        }

        public override bool DoHotKey0(Keys keyData)
        {
            try
            {
                if (keyData == Keys.Enter)
                {
                    DoAccept();
                }
                if (keyData == Keys.F2)
                {
                    DoEdit();
                    return true;
                }
                if (keyData == Keys.F3)
                {
                    DoSearch();
                    return true;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return base.DoHotKey0(keyData);
        }

        private void DoAccept()
        {
            try
            {
                var g = pagingGridView1.dataGridView1;
                if (g.DataSource != null && g.CurrentRow != null)
                {
                    OnAcceptDataEvent(g.CurrentRow.ToDataDictionary());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("LookupForm DoAccept " + ex.Message);
            }
        }

        private void DoEdit()
        {
            throw new NotImplementedException();
        }

        private void DoSearch()
        {
            pagingGridView1.Search();
        }

        private void colorGridViewPaging1_CellDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DoAccept();
        }

        
    }
}
