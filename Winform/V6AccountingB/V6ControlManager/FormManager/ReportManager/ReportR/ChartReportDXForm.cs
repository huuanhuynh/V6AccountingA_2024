using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6ControlManager.FormManager.ReportManager.DXreport;
using V6ControlManager.FormManager.ReportManager.Filter;
using V6Controls;
using V6Controls.Forms;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.ReportR
{
    public partial class ChartReportDXForm : V6Form
    {
        public ChartReportDXForm()
        {
            InitializeComponent();
        }

        private FilterBase _filter;
        private string _rptFile;
        private XtraReport _repx;
        private DataSet _ds;
        private DataTable _tbl, _tbl2;
        private IDictionary<string, object> _rptParameters;
        public ChartReportDXForm(FilterBase filter, string rptFile, DataTable tbl, DataTable tbl2,
            IDictionary<string, object> reportDocumentParameters)
        {
            V6ControlFormHelper.AddLastAction(GetType() + " " + rptFile);
            InitializeComponent();

            _filter = filter;
            _rptFile = rptFile;
            _tbl = tbl;
            _tbl2 = tbl2;
            _rptParameters = reportDocumentParameters;

            MyInitChartReportDXForm();
        }

        private void MyInitChartReportDXForm()
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
                this.ShowErrorMessage(GetType() + ".MyInitChartReportDXForm: " + ex.Message);
            }
            Ready();
        }

        private void ChartReportDXForm_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void SetAllReportParams(XtraReport repx)
        {
            string errors = "";
            foreach (KeyValuePair<string, object> item in _rptParameters)
            {
                try
                {
                    if (_repx.Parameters[item.Key] != null)
                    {
                        _repx.Parameters[item.Key].Value = item.Value;
                    }
                    else
                    {
                        // missing parameters warning!
                        //errors += "\n" + item.Key + ":\t " + V6Text.NotExist;
                        // Auto create Paramter for easy edit.
                        _repx.Parameters.Add(new Parameter()
                        {
                            Name = item.Key,
                            Value = item.Value,
                            Visible = false,
                            Type = item.Value.GetType(),
                            Description = item.Key,
                        });
                    }
                }
                catch (Exception ex)
                {
                    errors += "\n" + item.Key + ": " + ex.Message;
                }
            }

            DXreportManager.SetReportFormatByTag(_repx, _rptParameters);
            if (errors != "")
            {
                this.ShowErrorMessage(GetType() + ".SetAllReportParams: " + repx + " " + errors);
            }
        }

        private void LoadReport()
        {
            try
            {
                if (cboLoaiBieuDo.SelectedValue != null)
                {
                    var repxFile = cboLoaiBieuDo.SelectedValue.ToString().Trim();
                    string[] fields = cboLoaiReport.SelectedValue.ToString().Trim().Split(',');
                    var tbl = GetChartData(_tbl, fields);
                    var tbl2 = _tbl2.Copy();
                    _repx = DXreportManager.LoadV6XtraReportFromFile(repxFile);
                    _repx.PrintingSystem.ShowMarginsWarning = false;
                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);
                    ds.Tables.Add(tbl2);
                    _ds = ds;
                    _repx.DataSource = ds;

                    //parameters
                    //if (_rptParameters != null)
                    //{
                    //    string errors = "";
                    //    foreach (KeyValuePair<string, object> item in _rptParameters)
                    //    {
                    //        try
                    //        {
                    //            _repx.SetParameterValue(item.Key, item.Value);
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            errors += item.Key + " " + ex.Message + "\n";
                    //        }
                    //    }

                    //    if (errors.Length > 0)
                    //    {
                    //        this.WriteToLog(GetType() + ".LoadReport Set_rptParameters", errors);
                    //        this.ShowWarningMessage("Lỗi tham số:\n" + errors);
                    //    }
                    //}

                    SetAllReportParams(_repx);

                    documentViewer1.DocumentSource = _repx;
                    _repx.CreateDocument();
                    documentViewer1.Zoom = 1f;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".LoadReport", ex);
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

        private void btnSuaMau_Click(object sender, EventArgs e)
        {
            if (new ConfirmPasswordV6().ShowDialog(this) != DialogResult.OK) return;

            try
            {
                var repxFile = cboLoaiBieuDo.SelectedValue.ToString().Trim();
                var x = DXreportManager.LoadV6XtraReportFromFile(repxFile);
                if (x != null)
                {
                    x.DataSource = _ds.Copy();
                    SetAllReportParams(x);
                    XtraEditorForm1 form1 = new XtraEditorForm1(x, repxFile);
                    form1.Show(this);
                    //SetStatus2Text();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SuaMau_Click: " + ex.Message);
            }
        }
    }
}
