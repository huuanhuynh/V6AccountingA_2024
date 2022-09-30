using System;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.HeThong.V6BarcodePrint;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AINVTBAR1_Control : XuLyBase0
    {
        public AINVTBAR1_Control()
        {
            InitializeComponent();
        }

        public AINVTBAR1_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                panel1.SizeChanged += panel1_SizeChanged;
                
                dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".V6COPY_RA Init: " + ex.Message);
            }
        }

        void panel1_SizeChanged(object sender, EventArgs e)
        {
            dataGridView1.Height = panel1.Height - 10;
            dataGridView1.Width = panel1.Width - dataGridView1.Left - 5;
            if (FilterControl != null)
            {
                FilterControl.Width = dataGridView1.Left - 5;
            }
        }

        void dataGridView1_EditingControlShowing(object sender0, DataGridViewEditingControlShowingEventArgs e0)
        {
            e0.Control.KeyPress += (sender, e) =>
            {
                try
                {
                    if (dataGridView1.CurrentCell == null) return;
                    var columnName = dataGridView1.CurrentCell.OwningColumn.DataPropertyName;
                    if (columnName == "GIA_IN")
                    {
                        if (e.KeyChar == '.' && dataGridView1.CurrentCell.EditedFormattedValue.ToString().Contains("."))
                        {
                            e.Handled = true;
                        }
                        else if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != '.')
                        {
                            e.Handled = true;
                        }
                    }
                    else if (columnName == "SL_IN")
                    {
                        if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                        {
                            e.Handled = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
                }
            };
        }

        private void AINVTBAR1_Control_Load(object sender, EventArgs e)
        {
            
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = string.Format("F9 {0}, F10 {1}.", V6Text.Text("IN"), V6Text.Text("ExportExcel"));
            }
            V6ControlFormHelper.SetStatusText2(text, id);
        }

        protected override void Nhan()
        {
            try
            {
                if (_executing)
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.Executing);
                    return;
                }

                FormManagerHelper.HideMainMenu();
                dataGridView1.DataSource = null;

                CheckForIllegalCrossThreadCalls = false;
                Thread T = new Thread(ExecProc);
                T.IsBackground = true;
                T.Start();

                Timer timerRunAll = new Timer();
                timerRunAll.Interval = 100;
                timerRunAll.Tick += timerRunAll_Tick;
                _executesuccess = false;
                _executing = true;
                timerRunAll.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Nhan: " + ex.Message);
            }
        }

        private string _error = "";
        private void ExecProc()
        {
            try
            {
                _message = "";
                var plist = FilterControl.GetFilterParameters();
                _ds = V6BusinessHelper.ExecuteProcedure("AINVTBAR1", plist.ToArray());
                _executing = false;
                _executesuccess = true;
            }
            catch (Exception ex)
            {
                _error = _message + " " + ex.Message;
                _executesuccess = false;
                _executing = false;
            }
        }

        private void timerRunAll_Tick(object sender, EventArgs e)
        {
            if (_executesuccess)
            {
                ((Timer)sender).Stop();
                btnNhan.Image = btnNhanImage;
                ii = 0;
                try
                {
                    ViewData();
                    //dataGridView1.Height = panel1.Height - 10;
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        if (column.DataPropertyName == "SL_IN" || column.DataPropertyName == "GIA_IN")
                        {
                            column.ReadOnly = false;
                        }
                        else
                        {
                            column.ReadOnly = true;
                        }
                    }
                    
                    DoAfterExecuteSuccess();
                    V6ControlFormHelper.ShowMainMessage(V6Text.Text("TAIDULIEUXONG") + "!\r\n" + _message);

                    _executesuccess = false;
                }
                catch (Exception ex)
                {
                    ((Timer)sender).Stop();
                    _executesuccess = false;
                    this.ShowErrorMessage(GetType() + ".TimerView" + ex.Message, ex.Source);
                }
            }
            else if (_executing)
            {
                btnNhan.Image = waitingImages.Images[ii];
                ii++;
                if (ii >= waitingImages.Images.Count) ii = 0;
            }
            else
            {
                ((Timer)sender).Stop();
                btnNhan.Image = btnNhanImage;
                this.ShowErrorMessage(_error);
            }
        }

        private void ViewData()
        {
            try
            {
                if (_ds == null || _ds.Tables.Count == 0) return;
                dataGridView1.DataSource = _ds.Tables[0];
                dataGridView1.Focus();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }


        private DataTable GenData()
        {
            var data = new DataTable();
            data.Columns.Add("Code", typeof(string));
            data.Columns.Add("Name", typeof(string));
            data.Columns.Add("Price", typeof(decimal));
            var data0 = _ds.Tables[0];
            foreach (DataRow row in data0.Rows)
            {
                int sl_in = ObjectAndString.ObjectToInt(row["SL_IN"]);
                if (sl_in <= 0) continue;

                string code = row["MA_VT"].ToString().Trim();
                string name = row["TEN_VT"].ToString().Trim();
                decimal price = ObjectAndString.ObjectToDecimal(row["GIA_IN"]);
                for (int i = 0; i < sl_in; i++)
                {
                    var newRow = data.NewRow();
                    newRow[0] = code;
                    newRow[1] = name;
                    newRow[2] = price;
                    data.Rows.Add(newRow);
                }
            }
            return data;
        }

        private void DoPrint()
        {
            try
            {
                dataGridView1.EndEdit();

                var data = GenData();
                PrintBarcodeForm pForm = new PrintBarcodeForm(data);
                
                pForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoPrint: " + ex.Message);
            }
        }

        private void DoExportExcel()
        {
            try
            {
                dataGridView1.EndEdit();
                var data = GenData();
                var saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel file|*.xls";
                saveDialog.Title = "Save as";
                
                if (saveDialog.ShowDialog(this) == DialogResult.OK)
                {
                    V6Tools.V6Export.ExportData.ToExcel(data, saveDialog.FileName, null, false);
                    V6ControlFormHelper.ShowMainMessage(V6Text.ExportFinish);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoExportExcel: " + ex.Message);
            }
        }
        
        
        public override void DoHotKey(Keys keyData)
        {
            if (keyData == Keys.F9)
            {
                DoPrint();
                return;
            }
            if (keyData == Keys.F10)
            {
                DoExportExcel();
                return;
            }
            base.DoHotKey(keyData);
        }


        void grid_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid != null && grid.CurrentRow != null)
            {
                if (e.KeyCode == Keys.Space)
                {
                    grid.CurrentRow.ChangeSelect();
                }
            }
        }

       

    }
}
