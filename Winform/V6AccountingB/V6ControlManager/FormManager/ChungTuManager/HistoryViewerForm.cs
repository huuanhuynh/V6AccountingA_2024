using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager
{
    public partial class HistoryViewerForm : V6Form
    {
        DataTable _data = null;
        public HistoryViewerForm()
        {
            InitializeComponent();
            //MyInit();
        }

        public HistoryViewerForm(DataTable data)
        {
            _data = data.Copy();
            InitializeComponent();
            MyInit();
        }

        //public IDictionary<string, object> Data { get; set; }
        private void MyInit()
        {
            try
            {
                //dataGridView1.DataSource = _data;
                GenDataForGridViews();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".MyInit", ex);
            }
        }

        private void HistoryViewerForm_Load(object sender, EventArgs e)
        {
            //GenDataToGridView();
        }

        private void GenDataForGridViews()
        {
            try
            {
                DataTable AM_DATA = new DataTable();
                foreach (DataRow row in _data.Rows)
                {
                    var master_string = row["CONTENT"].ToString();
                    var am_data = ObjectAndString.StringToStringDictionary(master_string);
                    Dictionary<string, object> a = new Dictionary<string, object>();
                    a["DATE"] = ObjectAndString.ObjectToString(row["DATE"], "yyyy-MM-dd HH:mm:ss");
                    a["USER_NAME"] = row["USER_NAME"];
                    a["CLIENT_NAME"] = row["CLIENT_NAME"];
                    foreach (KeyValuePair<string, string> item in am_data)
                    {
                        a[item.Key] = item.Value.Replace("|", "=>");
                    }
                    a["CONTENT4"] = row["CONTENT4"];
                    AM_DATA.AddRow(a, true);
                }

                dataGridView1.DataSource = AM_DATA;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".GenDataToGridView", ex);
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            ViewSelectDetail(dataGridView1.CurrentRow);
        }

        private void ViewSelectDetail(DataGridViewRow row)
        {
            try
            {
                if (row == null) return;
                DataTable AD_DATA = new DataTable();
                var Data = row.ToDataDictionary();
                // AD
                var details_string = ObjectAndString.SplitStringBy(Data["CONTENT4"].ToString(), '~');
                foreach (string s in details_string)
                {
                    int index = s.IndexOf(' ');
                    string stt_rec0 = s.Substring(0, index);
                    string detail1 = s.Substring(index + 1);
                    var data = ObjectAndString.StringToStringDictionary(detail1);
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    rowData["STT_REC0"] = stt_rec0;
                    foreach (KeyValuePair<string, string> item in data)
                    {
                        if (stt_rec0.StartsWith("EDIT_"))
                        {
                            rowData[item.Key] = item.Value.Replace("|", "=>");
                        }
                        else
                        {
                            rowData[item.Key] = item.Value;
                        }
                    }

                    AD_DATA.AddRow(rowData, true);
                }
                dataGridView2.DataSource = AD_DATA;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ViewSelectDetail", ex);
            }
        }
    }
}
