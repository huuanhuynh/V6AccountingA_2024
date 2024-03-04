using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
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


        private void MyInit()
        {
            try
            {
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
                    var master_string = row["XML_CT"].ToString();
                    var table = Data_Table.FromXmlString(master_string);
                    
                    Dictionary<string, object> a = new Dictionary<string, object>();
                    a["DATE"] = ObjectAndString.ObjectToString(row["DATE"], "yyyy-MM-dd HH:mm:ss");
                    a["USER_NAME"] = row["USER_NAME"];
                    a["CLIENT_NAME"] = row["CLIENT_NAME"];
                    if (table != null)
                    foreach (DataRow row1 in table.Rows)
                    {
                        a.AddRange(row1.ToDataDictionary());
                    }
                    a["XML_CT4"] = row["XML_CT4"];
                    AM_DATA.AddRow(a, true);
                }

                dataGridView1.DataSource = AM_DATA;
                // hide columns
                if (dataGridView1.Columns.Contains("XML_CT4"))
                {
                    dataGridView1.Columns["XML_CT4"].Visible = false;
                }
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
                string xml_ct4 = "" + Data["XML_CT4"];
                var table = Data_Table.FromXmlString(xml_ct4);
                
                if (table != null)
                foreach (DataRow s in table.Rows)
                {
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    rowData["KEY"] = s["Key"];
                    string json = "" + s["Value"];
                    var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    rowData.AddRange(dic);

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
