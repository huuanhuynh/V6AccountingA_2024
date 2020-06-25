using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Controls.GridView;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuNhapKho.ChonDeNghiNhap
{
    public partial class INY_PNKho_KetQua : LocKetQuaBase
    {
        public INY_PNKho_KetQua()
        {
            InitializeComponent();
        }

        public INY_PNKho_KetQua(V6InvoiceBase invoice)
        {
            InitializeComponent();
            _invoice = invoice;
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                _aldmConfig = ConfigManager.GetAldmConfig("AMAD94A");
                if (_aldmConfig.HaveInfo) gridViewSummary1.NoSumColumns = _aldmConfig.GRDT_V1;
                //dataGridView1.RowSelect += dataGridView1_RowSelect;
                dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        /// <summary>
        /// Chức năng sửa hàng loạt một cột dữ liệu.
        /// </summary>
        /// <param name="many">Thanh thế hết giá trị cho các cột được cấu hình Alct.Extra_info.CT_REPLACE bằng giá trị của dòng đang đứng.</param>
        public void ChucNang_ThayThe(bool many = false)
        {
            try
            {
                //Hien form chuc nang co options *-1 or input
                
                if (dataGridView1.CurrentRow == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }

                if (!_aldmConfig.EXTRA_INFOR.ContainsKey("CT_REPLACE"))
                {
                    ShowMainMessage(V6Text.NoDefine + "EXTRA_INFOR[CT_REPLACE]");
                    return;
                }
                var listFieldCanReplace = ObjectAndString.SplitString(_aldmConfig.EXTRA_INFOR["CT_REPLACE"]);

                if (many)
                {
                    IDictionary<string, object> data = new Dictionary<string, object>();
                    if (dataGridView1.CurrentRow != null)
                    {
                        foreach (string field in listFieldCanReplace)
                        {
                            data[field.ToUpper()] = dataGridView1.CurrentRow.Cells[field].Value;
                        }

                        V6ControlFormHelper.UpdateDKlistAll(data, listFieldCanReplace, _am, dataGridView1.CurrentRow.Index);
                    }
                }
                else // Thay thế giá trị của cột đang đứng từ dòng hiện tại trở xuống bằng giá trị mới.
                {
                    //int field_index = dataGridView1.CurrentCell.ColumnIndex;
                    string FIELD = dataGridView1.CurrentCell.OwningColumn.DataPropertyName.ToUpper();
                    Type valueType = dataGridView1.CurrentCell.OwningColumn.ValueType;


                    if (!listFieldCanReplace.Contains(FIELD))
                    {
                        ShowMainMessage(V6Text.NoDefine + " CT_REPLACE:" + FIELD);
                        return;
                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Index < dataGridView1.CurrentRow.Index) continue;

                        object newValue = ObjectAndString.ObjectTo(valueType, dataGridView1.CurrentRow.Cells[FIELD].Value);
                        if (ObjectAndString.IsDateTimeType(valueType) && newValue != null)
                        {
                            DateTime newDate = (DateTime)newValue;
                            if (newDate < new DateTime(1700, 1, 1))
                            {
                                newValue = null;
                            }
                        }

                        SortedDictionary<string, object> newData = new SortedDictionary<string, object>();
                        newData.Add(FIELD, newValue);
                        V6ControlFormHelper.UpdateGridViewRow(row, newData);
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ChucNang_ThayThe", ex);
            }
        }

        void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null) return;
                var vcol = dataGridView1.Columns[e.ColumnIndex] as V6VvarDataGridViewColumn;
                if (vcol != null && vcol.DataPropertyName.ToUpper() == "MA_KHO_I")
                {
                    string mavt = dataGridView1.CurrentRow.Cells["MA_VT"].Value + "";
                    var where = _invoice.GetMaKhoFilterByMaVt("IND", mavt, V6Login.Madvcs);
                    vcol.InitFilter = where;
                    vcol.CheckNotEmpty = true;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView1_CellBeginEdit", ex);
            }
        }

        //void dataGridView1_RowSelect(object sender, SelectRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Select)
        //        {
        //            if ("A" == "B")
        //            {
        //                e.CancelSelect = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.WriteExLog(GetType() + ".dataGridView1_RowSelect", ex);
        //    }
        //}

        private DataTable _am = null;
        public void SetAM(DataTable am)
        {
            _am = am.Copy();
            dataGridView1.DataSource = _am;
            FormatGridView();
        }

        private void FormatGridView()
        {
            try
            {
                if (!_aldmConfig.HaveInfo) return;

                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, _aldmConfig.GRDS_V1, _aldmConfig.GRDF_V1, _aldmConfig.GRDH_LANG_V1);

                if (!string.IsNullOrEmpty(_aldmConfig.FIELD))
                {
                    var field_valid = ObjectAndString.SplitString(_aldmConfig.FIELD);

                    dataGridView1.SetEditColumnParams(field_valid[0]);
                    dataGridView1.CongThuc_CellEndEdit_ApplyAllRow = false;
                    dataGridView1.ChangeColumnType(field_valid[0], typeof(V6NumberDataGridViewColumn), null);
                    if (!string.IsNullOrEmpty(_aldmConfig.CACH_TINH1))
                    {
                        dataGridView1.GanCongThuc(field_valid[0], _aldmConfig.CACH_TINH1);
                    }

                    if (!string.IsNullOrEmpty(_aldmConfig.FIELD2))
                    {
                        dataGridView1.SetValid(field_valid[0], field_valid[1], _aldmConfig.FIELD2);
                    }
                }
                //dataGridView1.GanCongThuc("SL_QD", "SO_LUONG=SL_QD*HS_QD1");
                //dataGridView1.ThemCongThuc("SL_QD", "TIEN_NT2=ROUND(SO_LUONG*GIA2,M_ROUND)");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridView", ex);
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            Refresh0(dataGridView1);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnAcceptSelectEvent();
            }
            else if (e.KeyData == (Keys.Control | Keys.A))
            {
                e.Handled = true;
                dataGridView1.SelectAllRow();
            }
            else if (e.KeyData == (Keys.Control | Keys.U))
            {
                dataGridView1.UnSelectAllRow();
            }
        }
        /// <summary>
        /// Hiện (tải) lại chi tiết.
        /// </summary>
        public override void Refresh0(DataGridView grid1)
        {
            
        }

        private void thayTheMenu_Click(object sender, EventArgs e)
        {
            ChucNang_ThayThe(false);
        }

        private void thayTheNhieuMenu_Click(object sender, EventArgs e)
        {
            ChucNang_ThayThe(true);
        }
    }
}
