﻿using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class QR_TRANSFER_FORM : V6Form
    {
        
        public QR_TRANSFER_FORM()
        {
            InitializeComponent();
        }

        public QR_TRANSFER_FORM(V6InvoiceBase invoice, string program)
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
        /// <summary>
        /// Các trường dữ liệu khởi tạo.
        /// </summary>
        public string QR_AD { get; set; }
        public V6QRTextBox _parentQRinput = null;

        private void MyInit()
        {
            try
            {
                SetSaveTempKey("QR_TRANSFER_" + _invoice.Mact);
                TurnOffCapsLock();
                InvokeInitFix();
                LoadDefaultData(2, "", "QRTRANSFER", ItemID);
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
                TurnOffCapsLock();

                var fields = QR_AD;
                _table = V6BusinessHelper.SelectSimple(_invoice.AD_TableName, fields, "1=0");
                InvokeFormProgram("QR_TRANSFER_INIT2");
                dataGridView1.DataSource = _table;
            }
            catch (Exception ex)
            {
                this.ShowErrorException("Form_load", ex);
            }
        }

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        private void TurnOffCapsLock()
        {
            if (Control.IsKeyLocked(Keys.CapsLock)) // Checks Capslock is on
            {
                const int KEYEVENTF_EXTENDEDKEY = 0x1;
                const int KEYEVENTF_KEYUP = 0x2;
                keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP,
                (UIntPtr)0);
            }
        }

        public void LoadData()
        {
            if (rInventory.Checked) LoadData_Inventory();
            else if (rScan.Checked) LoadData_Scan();
        }

        public void CheckMode()
        {
            try
            {
                var line = txtQR_INFOR.Lines[0];
                var ssss = ObjectAndString.SplitStringBy(line, '\t', false);
                if (ssss.Length < 3)
                {
                    rScan.Checked = true;
                }
                else
                {
                    rInventory.Checked = true;
                }
            }
            catch (Exception ex)
            {
                
            }
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
                        if (no_length_count > 0 && no_length_count == txtQR_INFOR.Lines.Length - 1) rScan.Checked = true;
                        return;
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
                txtQR_INFOR.Clear();
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
                    if (ssss.Length > 2)
                    {
                        rInventory.Checked = true;
                        //ShowMainMessage
                        return;
                    }
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

                int wrong_count = 0;
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
                                new_row[item.Key] = ObjectAndString.ObjectTo(column.DataType, item.Value) ?? DBNull.Value;
                            }
                        }
                        _table.Rows.Add(new_row);
                    }
                    else
                    {
                        wrong_count++;
                    }
                }

                if (wrong_count > 0)
                {
                    ShowMainMessage(wrong_count + " " + V6Text.WrongData);
                }

                dataGridView1.SelectAllRow();
                InvokeDynamicFix();
                txtQR_INFOR.Clear();
            }
            catch (Exception ex)
            {
                this.ShowErrorException("LoadData_Scan", ex);
            }
        }

        public override void DoHotKey(Keys keyData)
        {
            if (keyData == Keys.F8)
            {
                btnDeleteOne.PerformClick();
            }
            else
            {
                base.DoHotKey(keyData);
            }
        }

        private Dictionary<string, object> MadeNeighbor(V6QRTextBox txt)
        {
            Dictionary<string, object> result = null;
            try
            {
                if (txt.Data != null && txt.Data.Count > 0)
                {
                    result = new Dictionary<string, object>();
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

        private object InvokeFormProgram(string method_name)
        {
            try // Dynamic invoke
            {
                if (Form_program != null && !string.IsNullOrEmpty(method_name))
                {
                    All_Objects["data"] = _table;
                    All_Objects["qrForm"] = this;
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
            //if (txtQR_INFOR.Text.Trim() != "")
            //{
            //    btnLoad.PerformClick();
            //}
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
            if (this.ShowConfirmMessage(V6Text.CloseConfirm) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
            }
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
            SaveTemp();
        }

        private void btnDeleteOne_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && this.ShowConfirmMessage(V6Text.DeleteRowConfirm) == DialogResult.Yes)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && this.ShowConfirmMessage(V6Text.DeleteConfirm) == DialogResult.Yes)
            {
                dataGridView1.Rows.Clear();
            }
        }

        private void txtQR_INFOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                CheckMode();
                TurnOffCapsLock();
                if (rScan.Checked && chkAutoTransfer.Checked)
                {
                    btnLoad.PerformClick();
                }
                SaveTemp();
            }
            else if (e.KeyCode == Keys.CapsLock)
            {
                TurnOffCapsLock();
            }
        }

        string savetempkey = "QR_TRANSFER_FORM";
        public void SetSaveTempKey(string key)
        {
            savetempkey = key;
        }
        public void SaveTemp()
        {
            try
            {
                int size = 10;
                //mã form, mã file, date?, index.
                string[] files = new string[size];
                FileInfo[] finfos = new FileInfo[size];
                FileInfo choosenone = null;
                for (int i = 0; i < size; i++)
                {
                    files[i] = Path.Combine(V6Setting.V6ATempLocalAppData_Directory, savetempkey + i + ".txt");
                    finfos[i] = new FileInfo(files[i]);
                    if (choosenone == null && !finfos[i].Exists)
                    {
                        // Neu chua co file i thi tao moi.
                        choosenone = finfos[i];
                        break;
                    }
                }
                if (choosenone == null)
                {
                    // Neu co 5 thi chon cai cu nhat.
                    DateTime time = DateTime.Now;
                    foreach (var item in finfos)
                    {
                        if (item != null && item.Exists && item.LastWriteTime < time)
                        {
                            time = item.LastWriteTime;
                            choosenone = item;
                        }
                    }
                }

                //choosenone.OpenWrite
                File.WriteAllLines(choosenone.FullName, txtQR_INFOR.Lines);
                _table.WriteXml(choosenone.FullName + ".xml");
            }
            catch (Exception ex)
            {
                
            }
        }

        public void LoadTemp()
        {
            try
            {
                // Hiện lên 1 form chọn
                QR_LOADTEMP_FORM form = new QR_LOADTEMP_FORM(_invoice, _program, QR_AD);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    txtQR_INFOR.Text = form.txtQR_INFOR.Text;

                    _table.Rows.Clear();
                    _table.AddRowByTable(form._table);
                    dataGridView1.SelectAllRow();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtQR_INFOR_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void txtQR_INFOR_Enter(object sender, EventArgs e)
        {
            TurnOffCapsLock();
        }

        private void btnLoadTemp_Click(object sender, EventArgs e)
        {
            LoadTemp();
        }
    }
}