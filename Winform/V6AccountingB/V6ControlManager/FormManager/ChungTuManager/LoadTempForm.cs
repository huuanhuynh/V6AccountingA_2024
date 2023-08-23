using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager
{
    public partial class LoadTempForm : V6Form
    {
        public DataSet LoadDataSet { get; set; }
        private string _list;
        private V6InvoiceBase Invoice;
        public LoadTempForm()
        {
            InitializeComponent();
        }
        public LoadTempForm(string todayListFile, V6InvoiceBase invoice)
        {
            Invoice = invoice;
            _list = todayListFile;
            InitializeComponent();
            LoadList(_list);
            LoadCombobox();
        }

        Dictionary<string, string> tempDic = null;
        private void LoadList(string listFile)
        {
            try
            {
                listView1.Items.Clear();
                var lines = File.ReadAllLines(listFile);
                tempDic = new Dictionary<string, string>();
                foreach (string line in lines)
                {
                    if (string.IsNullOrEmpty(line) || line.Trim() == "") continue;
                    //Temp\N005838120SOA_V6.xml   08 / 17 / 2023 11:14:24
                    //Temp\N005838121SOA_V6.xml   08 / 17 / 2023 11:16:47
                    var ss = ObjectAndString.Split2(line, '\t');
                    if (ss.Length < 2) continue;
                    tempDic[ss[0]] = ss[1];
                }
                Stack<ListViewItem> stack = new Stack<ListViewItem>();
                foreach (KeyValuePair<string, string> item in tempDic)
                {
                    ListViewItem list_item = new ListViewItem(item.Key);
                    list_item.SubItems.Add(item.Value);

                    stack.Push(list_item);
                }
                foreach (ListViewItem item in stack)
                {
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadList", ex);
            }
        }
        private void LoadCombobox()
        {
            try
            {
                var files = Directory.GetFiles("Temp", Invoice.Mact + "_" + V6Login.UserName + "_*.list");
                for (int i = files.Length - 1; i >= 0; i--)
                {
                    string file = files[i];
                    comboBox1.Items.Add(file);
                    if (file.Length - i > 30) break;
                }
                
                comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadCombobox", ex);
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList(comboBox1.Text);
        }

        private void LoadTempForm_Load(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            try
            {
                // Load file data set
                string filePath =  listView1.SelectedItems[0].Text;
                LoadDataSet = new DataSet();
                LoadDataSet.ReadXml(filePath);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(ex);
                DialogResult = DialogResult.Abort;
            }
        }
    }
}
