using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HangTraLai.ChonPhieuXuat
{
    public partial class CPXKetQuaHangTraLai : LocKetQuaBase
    {
        private readonly DataTable _am;
        private readonly DataTable _ad;

        public CPXKetQuaHangTraLai()
        {
            InitializeComponent();
        }
        
        public CPXKetQuaHangTraLai(V6InvoiceBase invoice, DataTable AM, DataTable AD)
        {
            _am = AM;
            _ad = AD;
            _invoice = invoice;
            InitializeComponent();
            //MyInitBase(dataGridView1, null, AM, AD);//Bỏ qua vì không đúng AM, AD
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                _aldmConfig = ConfigManager.GetAldmConfig("AMAD76A");
                if (_aldmConfig.HaveInfo) gridViewSummary1.NoSumColumns = _aldmConfig.GRDT_V1;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }
        
        void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            CurrentIndex = -1;
        }

        public void SetAM(DataTable am)
        {
            dataGridView1.DataSource = am.Copy();
            FormatGridView();
        }

        private void FormatGridView()
        {
            try
            {
                if (!_aldmConfig.HaveInfo) return;

                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, _aldmConfig.GRDS_V1, _aldmConfig.GRDF_V1, _aldmConfig.GRDH_LANG_V1);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridView", ex);
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            //if (OnSelectAMRow != null)
            //{
            //    if (dataGridView1.SelectedCells.Count > 0)
            //    {
            //        var index = dataGridView1.SelectedCells[0].RowIndex;
            //        if (index != CurrentIndex)
            //        {
            //            var sttrec = dataGridView1.Rows[index].Cells["Stt_rec"].Value.ToString();
            //            var mant = dataGridView1.Rows[index].Cells["Ma_nt"].Value.ToString();
            //            var ttt = ObjectAndString.ObjectToDecimal(dataGridView1.Rows[index].Cells["T_TT"].Value);
                        

            //            CurrentIndex = index;
            //            CurrentSttRec = sttrec;
                        
            //            OnSelectAMRow(CurrentIndex, CurrentSttRec, ttt, mant);
            //        }
            //    }
            //}
        }

       private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnAcceptSelectEvent();
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (dataGridView1.CurrentRow != null)
                {
                    dataGridView1.CurrentRow.ChangeSelect();
                }

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
        /// Bỏ không dùng.
        /// </summary>
        public override void Refresh0(DataGridView grid1)
        {
            
        }
    }
}
