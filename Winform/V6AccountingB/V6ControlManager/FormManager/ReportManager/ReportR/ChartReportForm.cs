using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using V6ControlManager.FormManager.ReportManager.Filter;
using V6Controls;
using V6Controls.Forms;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.ReportR
{
    public partial class ChartReportForm : V6Form
    {
        public ChartReportForm()
        {
            InitializeComponent();
        }

        private FilterBase _filter;
        private string _rptFile;
        private ReportDocument _rpDoc;
        private DataTable _tbl, _tbl2;
        private SortedDictionary<string, object> _rptParameters;
        public ChartReportForm(FilterBase filter, string rptFile, DataTable tbl, DataTable tbl2,
            SortedDictionary<string, object> reportDocumentParameters)
        {
            V6ControlFormHelper.AddLastAction(GetType() + " " + rptFile);
            InitializeComponent();

            _filter = filter;
            _rptFile = rptFile;
            _tbl = tbl;
            _tbl2 = tbl2;
            _rptParameters = reportDocumentParameters;

            MyInitChartReportForm();
        }

        private void MyInitChartReportForm()
        {
            try
            {
                //DataTable tbl = new DataTable();
                //tbl.Columns.Add("Name", typeof (string));
                //tbl.Columns.Add("Value", typeof(string));
                //DataRow newRow = tbl.NewRow();
                //newRow["Name"] = "Số lượng";
                //newRow["Value"] = "Ten_vt,So_luong1,So_luong2";
                //tbl.Rows.Add(newRow);

                var tbl0 = _filter.GenTableForReportType();
                AddReportTypeList(tbl0);

                var tbl = new DataTable();
                tbl.Columns.Add("Name", typeof(string));
                tbl.Columns.Add("Value", typeof(string));
                var newRow = tbl.NewRow();
                newRow["Name"] = "Biểu đồ cột";
                newRow["Value"] = _rptFile;
                tbl.Rows.Add(newRow);
                
                newRow = tbl.NewRow();
                newRow["Name"] = "Biểu đồ cột 3D";
                newRow["Value"] = _rptFile.Left(_rptFile.Length - 4) + "1.rpt";
                tbl.Rows.Add(newRow);

                newRow = tbl.NewRow();
                newRow["Name"] = "Biểu đồ tròn";
                newRow["Value"] = _rptFile.Left(_rptFile.Length - 4) + "2.rpt";
                tbl.Rows.Add(newRow);

                newRow = tbl.NewRow();
                newRow["Name"] = "Biểu đồ tròn 3D";
                newRow["Value"] = _rptFile.Left(_rptFile.Length - 4) + "3.rpt";
                tbl.Rows.Add(newRow);

                AddChartTypeList(tbl);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".MyInitChartReportForm: " + ex.Message);
            }
            Ready();
        }

        private void ChartReportForm_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            if (cboLoaiBieuDo.SelectedValue != null)
            {
                var rptFile = cboLoaiBieuDo.SelectedValue.ToString().Trim();
                string[] fields = cboLoaiReport.SelectedValue.ToString().Trim().Split(',');
                var tbl = GetChartData(_tbl, fields);
                var tbl2 = _tbl2.Copy();
                _rpDoc = new ReportDocument();
                _rpDoc.Load(rptFile);
                DataSet ds = new DataSet();
                ds.Tables.Add(tbl);
                ds.Tables.Add(tbl2);
                _rpDoc.SetDataSource(ds);

                //parameters
                if (_rptParameters != null)
                {
                    string errors = "";
                    foreach (KeyValuePair<string, object> item in _rptParameters)
                    {
                        try
                        {
                            _rpDoc.SetParameterValue(item.Key, item.Value);
                        }
                        catch (Exception ex)
                        {
                            errors += item.Key + " " + ex.Message + "\n";
                        }
                    }

                    if (errors.Length > 0)
                    {
                        this.WriteToLog(GetType() + ".LoadReport Set_rptParameters", errors);
                        this.ShowWarningMessage("Lỗi tham số:\n" + errors);
                    }
                }
                crystalReportViewer1.ReportSource = _rpDoc;
                crystalReportViewer1.Show();
            }
        }

        public DataTable GetChartData(DataTable tbl, string[] fields)
        {
            DataTable newTbl = new DataTable("DataTable1");

            for (int i = 0, index = 0; i < fields.Length; i++)
            {
                var field = fields[i].Trim();
                var column = tbl.Columns[field];
                if (column != null)
                {
                    newTbl.Columns.Add(index == 0 ? "NAME" : "COL" + index, column.DataType);
                    index++;
                }
            }
            foreach (DataRow row in tbl.Rows)
            {
                var newRow = newTbl.NewRow();
                for (int i = 0, index = 0; i < fields.Length; i++)
                {
                    var field = fields[i].Trim();
                    var column = tbl.Columns[field];
                    if (column != null)
                    {
                        newRow[index == 0 ? "NAME" : "COL" + index] = row[field];
                        index++;
                    }
                }
                newTbl.Rows.Add(newRow);
            }
            return newTbl;
        }

        public void AddReportTypeList(DataTable tbl)
        {
            cboLoaiReport.DisplayMember = "Name";
            cboLoaiReport.ValueMember = "Value";
            cboLoaiReport.DataSource = tbl;
            cboLoaiReport.SelectedIndex = 0;
            cboLoaiReport.DisplayMember = "Name";
            cboLoaiReport.ValueMember = "Value";
        }
        public void AddChartTypeList(DataTable tbl)
        {
            cboLoaiBieuDo.DisplayMember = "Name";
            cboLoaiBieuDo.ValueMember = "Value";
            cboLoaiBieuDo.DataSource = tbl;
            cboLoaiBieuDo.SelectedIndex = 0;
            cboLoaiBieuDo.DisplayMember = "Name";
            cboLoaiBieuDo.ValueMember = "Value";
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.DoHotKey0(keyData);
        }

        //public void ChangeChartType()
        //{
        //    ChartObject cobj = (ChartObject) _rpDoc.ReportDefinition.ReportObjects["Name"];
            
        //}

        private void cboLoaiReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void cboLoaiBieuDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReport();
        }
    }
}
