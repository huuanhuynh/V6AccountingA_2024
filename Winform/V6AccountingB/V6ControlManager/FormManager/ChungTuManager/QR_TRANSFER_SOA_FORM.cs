using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager
{
    public partial class QR_TRANSFER_SOA_FORM : V6Form
    {
        public QR_TRANSFER_SOA_FORM()
        {
            InitializeComponent();
        }

        public QR_TRANSFER_SOA_FORM(V6InvoiceBase invoice, string program)
        {
            InitializeComponent();
            _invoice = invoice;
            _program = program;
            MyInit();
        }

        private V6InvoiceBase _invoice = null;
        private string _program;
        public DataTable _table = null;
        public string DynamicFixMethodName { get; set; }
        public string InitFixMethodName { get; set; }
        public V6QRTextBox _parentQRinput = null;

        private void MyInit()
        {
            try
            {
                var fields = "QR_CODE0,MA_KHO_I,MA_VT,MA_VITRI,MA_LO,HSD,SO_LUONG1,GIA_NT21,TIEN_NT2";
                _table = V6BusinessHelper.SelectSimple(_invoice.AD_TableName, fields, "1=0");
                InvokeInitFix();
                dataGridView1.DataSource = _table;
            }
            catch (Exception ex)
            {
                this.ShowErrorException("MyInit", ex);
            }
        }

        

        public void LoadData()
        {
            if (rInventory.Checked) LoadData_Inventory();
            else if (rScan.Checked) LoadData_Scan();
        }

        public void LoadData_Inventory()
        {
            try
            {
                int no_length_count = 0;
                foreach (string line in txtQR_INFOR.Lines)
                {
                    if (line.Trim() == "") continue;
                    var ssss = ObjectAndString.SplitStringBy(line, '\t', false);
                    if (ssss.Length < 3)
                    {
                        no_length_count++;
                        if (no_length_count >= txtQR_INFOR.Lines.Length - 1) rScan.Checked = true;
                        continue;
                    }
                    var new_row = _table.NewRow();
                    string qr_code0 = ssss[1];
                    new_row["QR_CODE0"] = qr_code0;
                    new_row["SO_LUONG1"] = ssss[2];

                    V6QRTextBox txt = new V6QRTextBox()
                    {
                        BrotherFields = _parentQRinput.BrotherFields,
                        NeighborFields = _parentQRinput.NeighborFields
                    };
                    txt.Text = qr_code0;
                    if (txt.Data != null)
                    {
                        var nData = MadeNeighbor(txt);
                        foreach (var item in nData)
                        {
                            if (_table.Columns.Contains(item.Key))
                            {
                                var column = _table.Columns[item.Key];
                                new_row[item.Key] = ObjectAndString.ObjectTo(column.DataType, item.Value);
                            }
                        }
                    }
                    _table.Rows.Add(new_row);
                }
                dataGridView1.SelectAllRow();
                InvokeDynamicFix();
            }
            catch (Exception ex)
            {
                this.ShowErrorException("LoadData_Inventory", ex);
            }
        }

        public void LoadData_Scan()
        {
            try
            {
                Dictionary<string, int> QR_Quantity = new Dictionary<string, int>();
                foreach (string line in txtQR_INFOR.Lines)
                {
                    if (line.Trim() == "") continue;
                    var ssss = ObjectAndString.SplitStringBy(line, '\t', false);
                    string qr_code0 = ssss[0].Trim();
                    if (QR_Quantity.ContainsKey(qr_code0))
                    {
                        QR_Quantity[qr_code0] = QR_Quantity[qr_code0] + 1;
                    }
                    else
                    {
                        QR_Quantity.Add(qr_code0, 1);
                    }
                }

                foreach (var qr_q in QR_Quantity)
                {
                    var new_row = _table.NewRow();
                    new_row["QR_CODE0"] = qr_q.Key;
                    new_row["SO_LUONG1"] = qr_q.Value;

                    V6QRTextBox txt = new V6QRTextBox()
                    {
                        BrotherFields = _parentQRinput.BrotherFields,
                        NeighborFields = _parentQRinput.NeighborFields
                    };
                    txt.Text = qr_q.Key;
                    if (txt.Data != null)
                    {
                        var nData = MadeNeighbor(txt);
                        foreach (var item in nData)
                        {
                            if (_table.Columns.Contains(item.Key))
                            {
                                var column = _table.Columns[item.Key];
                                new_row[item.Key] = ObjectAndString.ObjectTo(column.DataType, item.Value);
                            }
                        }
                    }
                    _table.Rows.Add(new_row);
                }
                dataGridView1.SelectAllRow();
                InvokeDynamicFix();
            }
            catch (Exception ex)
            {
                this.ShowErrorException("LoadData_Scan", ex);
            }
        }

        private Dictionary<string, string> MadeNeighbor(V6QRTextBox txt)
        {
            Dictionary<string, string> result = null;
            try
            {
                if (txt.Data != null && txt.Data.Count > 0)
                {
                    result = new Dictionary<string, string>();
                    var bbb = ObjectAndString.SplitString(txt.BrotherFields);
                    var nnn = ObjectAndString.SplitString(txt.NeighborFields);
                    for (int i = 0; i < bbb.Length; i++)
                    {
                        string bb = bbb[i].ToUpper(), nn = nnn[i].ToUpper();
                        if (txt.Data.ContainsKey(bb)) result[nn] = txt.Data[bb];
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return result;
        }

        public void ClearData()
        {
            try
            {
                _table.Clear();
                txtQR_INFOR.Clear();
                txtQR_INFOR.Focus();
            }
            catch (Exception ex)
            {
                this.ShowErrorException("ClearData", ex);
            }
        }

        private object InvokeDynamicFix()
        {
            try // Dynamic invoke
            {
                if (Form_program != null && !string.IsNullOrEmpty(DynamicFixMethodName))
                {
                    All_Objects["data"] = _table;
                    All_Objects["qrForm"] = this;
                    return V6ControlsHelper.InvokeMethodDynamic(Form_program, DynamicFixMethodName, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke " + DynamicFixMethodName, ex1);
            }
            return null;
        }

        private object InvokeInitFix()
        {
            try // Dynamic invoke
            {
                if (Form_program != null && !string.IsNullOrEmpty(InitFixMethodName))
                {
                    All_Objects["data"] = _table;
                    All_Objects["qrForm"] = this;
                    return V6ControlsHelper.InvokeMethodDynamic(Form_program, InitFixMethodName, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke " + InitFixMethodName, ex1);
            }
            return null;
        }


        private void Form_Load(object sender, EventArgs e)
        {

        }

        private void SelectMultiIDForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                this.ShowInfoMessage(V6Text.NoData);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        //public override bool DoHotKey0(Keys keyData)
        //{
        //    if (keyData == Keys.Enter)
        //    {
        //        btnNhan.PerformClick();
        //        return true;
        //    }
        //    return base.DoHotKey0(keyData);
        //}
        
        

        private void FixSize()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FixSize", ex);
            }
        }

        private void QR_TRANSFER_SOA_FORM_SizeChanged(object sender, EventArgs e)
        {
            FixSize();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}