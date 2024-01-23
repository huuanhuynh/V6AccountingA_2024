using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager
{
    public partial class QR_LOADTEMP_FORM : V6Form
    {
        
        public QR_LOADTEMP_FORM()
        {
            InitializeComponent();
        }

        public QR_LOADTEMP_FORM(V6InvoiceBase invoice, string program, string qrad)
        {
            InitializeComponent();
            _invoice = invoice;
            _program = program;
            _qrad = qrad;
            MyInit();
        }

        private V6InvoiceBase _invoice = null;
        private string _program;
        
        public string DynamicFixMethodName { get; set; }
        public string InitFixMethodName { get; set; }
        /// <summary>
        /// Các trường dữ liệu khởi tạo.
        /// </summary>
        public string _qrad { get; set; }
        public DataTable _table = null;
        public V6QRTextBox _parentQRinput = null;

        private void MyInit()
        {
            try
            {
                SetSaveTempKey("QR_TRANSFER_" + _invoice.Mact);

                var fields = _qrad;
                _table = V6BusinessHelper.SelectSimple(_invoice.AD_TableName, fields, "1=0");
                dataGridView1.DataSource = _table;

                LoadTempFiles();
                
                LoadDefaultData(2, "", "QRLOADTEMP", ItemID);
            }
            catch (Exception ex)
            {
                this.ShowErrorException("MyInit", ex);
            }
        }

        

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                InvokeFormProgram("QR_LOADTEMP_INIT2");
                listView1.Items[0].Selected = true;
            }
            catch (Exception ex)
            {
                this.ShowErrorException("Form_load", ex);
            }
        }
        
        private void LoadTempFiles()
        {
            try
            {
                int size = 10;
                var now = DateTime.Now;
                //mã form, mã file, date?, index.
                string[] files = new string[size];
                SortedList<TimeSpan, FileInfo> finfos = new SortedList<TimeSpan, FileInfo>();
                
                for (int i = 0; i < size; i++)
                {
                    files[i] = Path.Combine(V6Setting.V6ATempLocalAppData_Directory, savetempkey + i + ".txt");
                    var infoi = new FileInfo(files[i]);
                    if (infoi.Exists)
                    {
                        finfos.Add(now - infoi.LastWriteTime, infoi);
                    }
                }
                
                foreach (var item in finfos)
                {
                    ListViewItem listItem = new ListViewItem(item.Value.Name);
                    listItem.SubItems.Add(item.Value.LastWriteTime.ToString());
                    listItem.Tag = item.Value;

                    listView1.Items.Add(listItem);
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        
        

        private object InvokeFormProgram(string method_name)
        {
            try // Dynamic invoke
            {
                if (Form_program != null && !string.IsNullOrEmpty(method_name))
                {   
                    All_Objects["qrLoadTempForm"] = this;
                    return V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke " + InitFixMethodName, ex1);
            }
            return null;
        }
        

        private void SelectMultiIDForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if (this.ShowConfirmMessage(V6Text.CloseConfirm) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
        
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                txtQR_INFOR.ReadOnly = false;
                txtQR_INFOR.BackColor = Color.PaleGreen;
                txtQR_INFOR.Focus();
                txtQR_INFOR.SelectionStart = txtQR_INFOR.TextLength;
                btnSave.Enabled = true;
                btnEdit.Enabled = false;
            }
            catch (Exception ex)
            {
                this.ShowErrorException("btnEdit_Click", ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0 && this.ShowConfirmMessage(V6Text.SaveOverrideConfirm) == DialogResult.Yes)
            {
                var li = listView1.SelectedItems[0];
                FileInfo fi = (FileInfo)li.Tag;
                
                File.WriteAllLines(fi.FullName, txtQR_INFOR.Lines);
                fi.LastWriteTime = DateTime.Now;
                li.SubItems[1].Text = fi.LastWriteTime.ToString();
                btnSave.Enabled = false;
                
                txtQR_INFOR.ReadOnly = true;
                txtQR_INFOR.BackColor = SystemColors.Info;
                btnEdit.Enabled = true;
            }
            else
            {

            }
        }

        private void txtQR_INFOR_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        string savetempkey = "QR_LOADTEMP_FORM";
        public void SetSaveTempKey(string key)
        {
            savetempkey = key;
        }
        
        private void txtQR_INFOR_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void txtQR_INFOR_Enter(object sender, EventArgs e)
        {
            
        }
        

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    FileInfo fi = (FileInfo)listView1.SelectedItems[0].Tag;
                    txtQR_INFOR.BackColor = SystemColors.Info;
                    txtQR_INFOR.Text = File.ReadAllText(fi.FullName);
                    txtQR_INFOR.ReadOnly = true;

                    _table.Rows.Clear();
                    var xmlFile = fi.FullName + ".xml";
                    if (File.Exists(xmlFile))
                    {
                        DataSet ds = new DataSet();
                        ds.ReadXml(xmlFile);
                        if (ds.Tables.Count > 0) _table.AddRowByTable(ds.Tables[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException("listView1_SelectedIndexChanged", ex);
            }
        }


    }
}