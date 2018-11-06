using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager
{
    public class LocKetQuaBase : V6Control
    {
        protected V6InvoiceBase _invoice;
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
                if (grid1 != null)
                {
                    grid1.DataSource = AM;
                    V6ControlFormHelper.FormatGridViewAndHeader(
                        grid1, _invoice.GRDS_AM, _invoice.GRDF_AM,
                        V6Setting.IsVietnamese ? _invoice.GRDHV_AM : _invoice.GRDHE_AM);
                    if (_grid1 != null)
                        V6ControlFormHelper.FormatGridViewHideColumns(_grid1, _invoice.Mact);
                }
                if (grid2 != null)
                {
                    grid2.DataSource = AD;
                    V6ControlFormHelper.FormatGridViewAndHeader(
                        grid2, _invoice.GRDS_AD, _invoice.GRDF_AD,
                        V6Setting.IsVietnamese ? _invoice.GRDHV_AD : _invoice.GRDHE_AD);
                    if(_grid2 != null)
                    V6ControlFormHelper.FormatGridViewHideColumns(_grid2, _invoice.Mact);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".LocKetQuaBaseInit: " + ex.Message);
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

                    if (_grid1 != null)
                    {
                        V6ControlFormHelper.FormatGridViewAndHeader(
                            _grid1, _invoice.GRDS_AM, _invoice.GRDF_AM,
                            V6Setting.IsVietnamese ? _invoice.GRDHV_AM : _invoice.GRDHE_AM);

                        V6ControlFormHelper.FormatGridViewHideColumns(_grid1, _invoice.Mact);
                    }
                    if (_grid2 != null)
                    {
                        V6ControlFormHelper.FormatGridViewAndHeader(
                            _grid2, _invoice.GRDS_AD, _invoice.GRDF_AD,
                            V6Setting.IsVietnamese ? _invoice.GRDHV_AD : _invoice.GRDHE_AD);
                        
                        V6ControlFormHelper.FormatGridViewHideColumns(_grid2, _invoice.Mact);
                    }
                    
                }
            }
        }

        
    }//end class
}
