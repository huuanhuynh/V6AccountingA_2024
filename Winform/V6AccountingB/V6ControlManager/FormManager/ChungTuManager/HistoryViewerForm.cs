using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using V6Controls;
using V6Controls.Forms;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager
{
    public partial class HistoryViewerForm : V6Form
    {
        public HistoryViewerForm()
        {
            InitializeComponent();
            //MyInit();
        }


        public IDictionary<string, object> Data { get; set; }

        private void HistoryViewerForm_Load(object sender, EventArgs e)
        {
            GenDataToGridView();
        }

        private void GenDataToGridView()
        {
            try
            {
                DataTable AM_DATA = new DataTable();
                DataTable AD_DATA = new DataTable();

                // AM
                var master_string = Data["CONTENT"].ToString();
                var am_data = ObjectAndString.StringToStringDictionary(master_string);
                Dictionary<string, object> a = new Dictionary<string, object>();
                foreach (KeyValuePair<string, string> item in am_data)
                {
                    a[item.Key] = item.Value.Replace("|", "=>");
                }
                AM_DATA.AddRow(a, true);
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

                dataGridView1.DataSource = AM_DATA;
                dataGridView2.DataSource = AD_DATA;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "GenDataToGridView", ex);
            }
        }
    }
}
