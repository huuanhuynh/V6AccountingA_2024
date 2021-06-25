using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager
{
    public class LocKetQuaBase : V6Control
    {
        protected V6InvoiceBase _invoice;
        public AldmConfig _aldmConfig;
        public delegate void SelectAMRow(int index, string mact, string sttrec, decimal ttt_nt, decimal ttt, string mant);
        public event SelectAMRow OnSelectAMRow;
        public delegate void AcceptSelect();
        public event AcceptSelect AcceptSelectEvent;
        protected virtual void OnAcceptSelectEvent()
        {
            var handler = AcceptSelectEvent;
            if (handler != null) handler();
        }
        public int CurrentIndex { get; protected set; }
        public string CurrentSttRec { get; private set; }
        protected bool _cancel_SelectionChange;
        private V6ColorDataGridView _grid1, _grid2;

        /// <summary>
        /// Định dạng hiển thị GridView theo thông tin lưu trong Alct
        /// </summary>
        /// <param name="grid1"></param>
        /// <param name="grid2"></param>
        /// <param name="AM">Bảng mẫu không có dữ liệu nhưng có trường để lấy cấu trúc cho GridView</param>
        /// <param name="AD">Bảng mẫu không có dữ liệu nhưng có trường để lấy cấu trúc cho GridView</param>
        protected void MyInitBase(DataGridView grid1, DataGridView grid2, DataTable AM, DataTable AD)
        {
            try
            {
                _grid1 = grid1 as V6ColorDataGridView;
                _grid2 = grid2 as V6ColorDataGridView;
                if (_grid1 != null)
                {
                    _grid1.DataSourceChanged += _grid1_DataSourceChanged;
                    SetAM(AM);
                    
                }
                if (_grid2 != null)
                {
                    _grid2.DataSourceChanged += _grid2_DataSourceChanged;
                    SetAD(AD);
                    V6ControlFormHelper.FormatGridViewAndHeader(
                        grid2, _invoice.AlctConfig.GRDS_AD, _invoice.AlctConfig.GRDF_AD,
                        V6Setting.IsVietnamese ? _invoice.AlctConfig.GRDHV_AD : _invoice.AlctConfig.GRDHE_AD);

                    if(_grid2 != null) V6ControlFormHelper.FormatGridViewHideColumns(_grid2, _invoice.Mact);
                }

                if (_aldmConfig != null && _aldmConfig.HaveInfo
                                        && _aldmConfig.EXTRA_INFOR.ContainsKey("OFF_TOPFILTER")
                                        && ObjectAndString.ObjectToBool(_aldmConfig.EXTRA_INFOR["OFF_TOPFILTER"]))
                {
                    var gridViewTopFilter1 = this.GetControlByName("gridViewTopFilter1") as GridViewTopFilter;
                    if (gridViewTopFilter1 == null) return;
                    var dataGridView1 = this.GetControlByName("dataGridView1") as DataGridView;
                    if (dataGridView1 == null) return;

                    gridViewTopFilter1.DeConnectGridView();
                    dataGridView1.Top -= gridViewTopFilter1.Height;
                    dataGridView1.Height += gridViewTopFilter1.Height;
                    gridViewTopFilter1.Dispose();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".LocKetQuaBaseInit", ex);
            }
        }

        void _grid1_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
                if (!_grid1.IsFormated)
                {
                    V6ControlFormHelper.FormatGridViewAndHeader(_grid1, _invoice.AlctConfig.GRDS_AM,
                        _invoice.AlctConfig.GRDF_AM,
                        V6Setting.IsVietnamese ? _invoice.AlctConfig.GRDHV_AM : _invoice.AlctConfig.GRDHE_AM);
                    V6ControlFormHelper.FormatGridViewHideColumns(_grid1, _invoice.Mact);
                    _grid1.Formated();
                }
                //Đổi màu dữ liệu
                if (_aldmConfig == null) _aldmConfig = ConfigManager.GetAldmConfig("SEARCH_" + _invoice.Mact);
                if (_aldmConfig.HaveInfo)
                    V6ControlFormHelper.FormatGridView(_grid1, _aldmConfig.FIELDV, _aldmConfig.OPERV, _aldmConfig.VALUEV, _aldmConfig.BOLD_YN, _aldmConfig.COLOR_YN,
                        ObjectAndString.StringToColor(_aldmConfig.COLORV));
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "._grid1_DataSourceChanged", ex);
            }
        }
        void _grid2_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!_grid2.IsFormated)
                {
                    V6ControlFormHelper.FormatGridViewAndHeader(
                        _grid2, _invoice.AlctConfig.GRDS_AD, _invoice.AlctConfig.GRDF_AD,
                        V6Setting.IsVietnamese ? _invoice.AlctConfig.GRDHV_AD : _invoice.AlctConfig.GRDHE_AD);

                    V6ControlFormHelper.FormatGridViewHideColumns(_grid2, _invoice.Mact);
                    _grid2.Formated();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "._grid2_DataSourceChanged", ex);
            }
        }

        /// <summary>
        /// Hiện (tải) lại chi tiết.
        /// </summary>
        /// <param name="grid1">Datagridview AM</param>
        public virtual void Refresh0(DataGridView grid1)
        {
            if (OnSelectAMRow != null)
            {
                if (grid1.CurrentCell != null)
                {
                    var index = grid1.CurrentCell.RowIndex;
                    CurrentIndex = index;
                    
                    var ma_ct = grid1.Rows[index].Cells["Ma_ct"].Value.ToString();
                    var sttrec = grid1.Rows[index].Cells["Stt_rec"].Value.ToString();
                    var mant = grid1.Rows[index].Cells["Ma_nt"].Value.ToString();
                    
                    string ttt_field;
                    string ttt_nt_field;
                    switch (_invoice.Mact)
                    {
                        //soa,sof,ar1,poa,pob,c,ixc,ca1,bn1,ap1,2, soh,poh t_tt+nt
                        //ind,ixa,ixb,ta1,bc1,gl1,ap9,ar9 t_tien + nt
                        case "SOA":
                            ttt_nt_field = "T_TT_NT";
                            ttt_field = "T_TT";
                            break;
                        case "IND":
                        case "IXA":
                        case "IXB":
                        case "TA1":
                        case "BC1":
                        case "GL1":
                        case "AP9":
                        case "AR9":
                            ttt_nt_field = "T_TIEN_NT";
                            ttt_field = "T_TIEN";
                            break;
                        default:
                            ttt_nt_field = "T_TT_NT";
                            ttt_field = "T_TT";
                            break;
                    }

                    decimal ttt_nt=0, ttt = 0;
                    if (grid1.Columns.Contains(ttt_nt_field))
                        ttt_nt = ObjectAndString.ObjectToDecimal(grid1.Rows[index].Cells[ttt_nt_field].Value ?? 0);
                    if (grid1.Columns.Contains(ttt_field))
                        ttt = ObjectAndString.ObjectToDecimal(grid1.Rows[index].Cells[ttt_field].Value ?? 0);

                    CurrentIndex = index;
                    CurrentSttRec = sttrec;

                    OnSelectAMRow(CurrentIndex, ma_ct, CurrentSttRec, ttt_nt, ttt, mant);
                }
            }
        }

        public void SetAM(DataTable am)
        {
            if (_grid1 != null && am != null) _grid1.DataSource = am.Copy();
        }

        public void SetAD(DataTable ad)
        {
            if (_grid2 != null && ad != null) _grid2.TableSource = ad.Copy();
        }
    }//end class
}
