﻿using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using V6Tools.V6Convert;

namespace V6Controls.Forms.Viewer
{
    public partial class CalculatorForm : V6Form
    {
        /// <summary>
        /// Phép toán hoặc tính kq.
        /// </summary>
        enum Operation
        {
            Cong,
            Tru,
            Nhan,
            Chia,
            Bang,
            None
        }

        private decimal _memory;

        private string monitorData {get { return _monitor; } set { _monitor = value;
            ViewMonitorDataToScreen();
        }}
        private string _monitor = "0";

        private bool _error;

        //bool inCaculator;
        /// <summary>
        /// đang nhập số.
        /// </summary>
        bool _inInput;
        /// <summary>
        /// Đã bấm dấu .
        /// </summary>
        bool _dotPressed;
        decimal _resultValue;
        decimal so_hang_2;
        //decimal secondValue0;

        Operation _oldOperation;

        private void ViewMonitorDataToScreen()
        {
            toolTipV6FormControl.SetToolTip(txtScreen, _monitor);
            //decimal value = ObjectAndString.StringToDecimal(monitorData);
            //int decimals = value.ToString(CultureInfo.InvariantCulture).Split('.').Length > 1
            //  ? value.ToString(CultureInfo.InvariantCulture).Split('.')[1].Length
            //  : 0;
            //txtScreen.Text = ObjectAndString.NumberToString(value, decimals, ",");

            //return;

            string comma_monitor = _monitor.Replace(".", ",");

            if (comma_monitor.Length > 25)
            {
                int dot_index = comma_monitor.IndexOf(',');
                if (dot_index > 0 && dot_index < 25)
                {
                    txtScreen.Text = comma_monitor.Substring(0, 25);
                    //return;
                    //Có thể làm thêm màn hình phụ.
                    //string new_text = comma_monitor.Replace(",", "");
                    //new_text = new_text[0] + "," + new_text.Substring(1);
                    //txtScreen.Text = new_text.Substring(0, 25) + "E+" + (dot_index - 1);
                }
                else if (dot_index >= 25)
                {
                    txtScreen.Text = comma_monitor;
                }
                else
                {
                    txtScreen.Text = comma_monitor;
                    //return;
                    //string new_text = comma_monitor;
                    //new_text = new_text[0] + "," + new_text.Substring(1);
                    //txtScreen.Text = new_text.Substring(0, 25) + "E+" + (comma_monitor.Length - 1);
                }
            }
            else
            {
                txtScreen.Text = comma_monitor;
            }
        }

        private void ViewMemoryToScreen()
        {
            int decimals = _memory.ToString(CultureInfo.InvariantCulture).Split('.').Length > 1
                 ? _memory.ToString(CultureInfo.InvariantCulture).Split('.')[1].Length
                 : 0;
            lblMemory.Text = string.Format("M = {0}", ObjectAndString.NumberToString(_memory, decimals, ","));
        }

        private void ClearMemory()
        {
            _memory = 0;
            lblMemory.Text = "";
        }

        
        public CalculatorForm()
        {
            InitializeComponent();
            RefershAll();
            ResetMemory();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //this.txtMonitor.Focus();
            
        }

        void ResetMemory()
        {
            _memory = 0;
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (_error) return;
            Button currentButton = (Button)sender;
            string number = currentButton.AccessibleName;
            if (_inInput)
            {
                if (monitorData == "0")
                    SetStringToMonitorData(number);
                else
                    AppendStringToMonitorData(number);
            }
            else
            {
                _inInput = true;
                so_hang_2 = ObjectAndString.StringToDecimal(monitorData);
                SetStringToMonitorData(number.ToString(CultureInfo.InvariantCulture));
            }
        }

        void AppendStringToMonitorData(string text)
        {
            if (monitorData.Length >= 25)
            {
                txtScreen.Alert();
                return;
            }
            monitorData += text;
        }

        void SetStringToMonitorData(string text)
        {
            monitorData = text;
        }

        public override bool DoHotKey0(Keys keyData)
        {
            try
            {
                var eKeyCode = keyData & Keys.KeyCode;
                int keyCode = (int)eKeyCode;
                int key0 = (int)Keys.D0;
                int key9 = (int)Keys.D9;
                int k0 = (int)Keys.NumPad0;
                int k9 = (int)Keys.NumPad9;
                
                //else if (keyCode >= k0 && keyCode <= k9)
                //{
                //    if (_inInput)
                //    {
                //        if (monitorData == "0")
                //            SetStringToMonitorData((keyCode - k0).ToString());
                //        else
                //            AppendStringToMonitorData((keyCode - k0).ToString());
                //    }
                //    else
                //    {
                //        _inInput = true;
                //        SetStringToMonitorData((keyCode - k0).ToString());
                //    }
                //    return true;
                //}

                if ((keyCode >= key0 && keyCode <= key9) || (keyCode >= k0 && keyCode <= k9))
                {
                    int input_number = keyCode - key0;
                    if (input_number < 0 || input_number > 9)
                    {
                        input_number = keyCode - k0;
                    }

                    switch (input_number)
                    {
                        case 0:
                            btn0.Focus();
                            break;
                        case 1:
                            btn1.Focus();
                            break;
                        case 2:
                            btn2.Focus();
                            break;
                        case 3:
                            btn3.Focus();
                            break;
                        case 4:
                            btn4.Focus();
                            break;
                        case 5:
                            btn5.Focus();
                            break;
                        case 6:
                            btn6.Focus();
                            break;
                        case 7:
                            btn7.Focus();
                            break;
                        case 8:
                            btn8.Focus();
                            break;
                        case 9:
                            btn9.Focus();
                            break;
                    }

                    if (_inInput)
                    {
                        if (monitorData == "0")
                            SetStringToMonitorData(input_number.ToString());
                        else
                            AppendStringToMonitorData(input_number.ToString());
                    }
                    else // Nhập số mới.
                    {
                        _inInput = true;
                        SetStringToMonitorData(input_number.ToString());
                    }
                    return true;
                }
                else if (keyData == Keys.Decimal || keyData == Keys.Oemcomma || keyData == Keys.OemPeriod)
                {
                    btnDot.Focus();
                    btnDot.PerformClick();
                    return true;
                }
                //else if (keyData == (Keys.Control | Keys.C))  // Menu design
                //{
                //    Copy();
                //    return true;
                //}
                //else if (keyData == (Keys.Control | Keys.V))  // Menu design
                //{
                //    Paste();
                //    return true;
                //}
                else if (eKeyCode == Keys.Enter)
                {
                    btnEqual.Focus();
                    btnEqual.PerformClick();
                    return true;
                }
                else if (eKeyCode == Keys.Back)
                {
                    btnBackSpace.Focus();
                    btnBackSpace.PerformClick();
                    return true;
                }
                else if (eKeyCode == Keys.Insert)
                {
                    btnPlus.Focus();
                    btnPlus.PerformClick();
                    return true;
                }
                else if (eKeyCode == Keys.Delete)
                {
                    
                }
                else if (eKeyCode == Keys.Up || eKeyCode == Keys.Down || eKeyCode == Keys.Left || eKeyCode == Keys.Right)
                {
                    MoveFocus(eKeyCode);
                    return true;
                }
                else
                {
                    Button currentButton = null;
                    switch (eKeyCode)
                    {
                        case Keys.Divide:
                            currentButton = btnDivide;
                            break;
                        case Keys.Multiply:
                            currentButton = btnMultiply;
                            break;
                        case Keys.Add:
                            currentButton = btnPlus;
                            break;
                        case Keys.Subtract:
                            currentButton = btnSubtract;
                            break;
                    }
                    if (currentButton != null)
                    {
                        currentButton.Focus();
                        btnCalculate_Click(currentButton, null);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoHotKey", ex);
            }
            return base.DoHotKey0(keyData);
        }

        private void Copy()
        {
            Clipboard.SetText(ObjectAndString.StringToDecimal(monitorData).ToString(CultureInfo.CurrentCulture));
        }
        private void Paste()
        {
            decimal value = ObjectAndString.StringToDecimal(Clipboard.GetText());
            if (value != 0)
            {
                SetStringToMonitorData(value.ToString(CultureInfo.InvariantCulture));
                _inInput = true;
            }
        }

        private void MoveFocus(Keys eKeyCode)
        {
            Control current = ActiveControl;
            //Point old_point = current.Location;
            Point get_point = current.Location;
            switch (eKeyCode)
            {
                case Keys.Up:
                    get_point.X = current.Left + 10;
                    get_point.Y = current.Top - 25;
                    break;
                case Keys.Down:
                    get_point.X = current.Left + 10;
                    get_point.Y = current.Bottom + 25;
                    break;
                case Keys.Left:
                    get_point.X = current.Left - 25;
                    get_point.Y = current.Top + 10;
                    break;
                case Keys.Right:
                    get_point.X = current.Right + 25;
                    get_point.Y = current.Top + 10;
                    break;
            }
            Control get_control = GetChildAtPoint(get_point);
            if (get_control != null) get_control.Focus();
        }


        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // Cộng Nhân Chia Trừ Bằng
            if (_error) return;

            Operation newOperation = Operation.None;
            Button currentButton = (Button) sender;

            if (currentButton == btnPlus)
            {
                //btnPlus.Focus();
                newOperation = Operation.Cong;
            }
            else if (currentButton == btnMultiply)
            {
                //btnMultiply.Focus();
                newOperation = Operation.Nhan;
            }
            else if (currentButton == btnDivide)
            {
                //btnDivide.Focus();
                newOperation = Operation.Chia;
            }
            else if (currentButton == btnSubtract)
            {
                //btnSubtract.Focus();
                newOperation = Operation.Tru;
            }
            else if (currentButton == btnEqual)
            {
                //btnEqual.Focus();
                newOperation = Operation.Bang;
            }

            CalculateResult(newOperation);
        }


        Operation _lastOperationButton = Operation.None;
        void CalculateResult(Operation operationButton)
        {
            bool haveNewResult = false;
            try
            {
                decimal screen_value = ObjectAndString.StringToDecimal(monitorData);
                if (_inInput) // Cập nhập số hạng thứ 2
                {
                    so_hang_2 = screen_value;
                }

                if(_lastOperationButton == Operation.Bang && operationButton != Operation.Bang)
                {
                    _oldOperation = Operation.None;
                    so_hang_2 = screen_value;
                }

                // Tính toán theo phép tính cũ nếu có
                if(operationButton == Operation.Bang || _inInput)
                    switch (_oldOperation)
                    {
                        case Operation.Cong:
                            _resultValue += so_hang_2;
                            haveNewResult = true;
                            break;
                        case Operation.Tru:
                            _resultValue -= so_hang_2;
                            haveNewResult = true;
                            break;
                        case Operation.Nhan:
                            _resultValue *= so_hang_2;
                            haveNewResult = true;
                            break;
                        case Operation.Chia:
                            _resultValue /= so_hang_2;
                            haveNewResult = true;
                            break;
                        default: // Đưa số hạng 2 vào số hạng 1.
                            _resultValue = so_hang_2;
                            break;
                    }
                
                //Lưu lại phép tính cũ (không tính dấu bằng).
                if (operationButton != Operation.Bang)
                {
                    _oldOperation = operationButton;
                }

                // Cập nhập kết quả lên màn hình.
                if (haveNewResult)
                {
                    SetStringToMonitorData(_resultValue.ToString(CultureInfo.InvariantCulture));
                }
            }
            catch (Exception ex)
            {
                _error = true;
                txtScreen.Text = "Error";
                this.WriteExLog(GetType() + ".CalculateResult", ex);
            }

            _inInput = false;
            _dotPressed = false;
            _lastOperationButton = operationButton;
        }
        
        private void btnClear_Click(object sender, EventArgs e)
        {
            RefershAll();
        }

        void RefershAll(bool resetScreen = true)
        {
            _error = false;
            _resultValue = 0;
            so_hang_2 = 0;
            _inInput = false;
            _dotPressed = false;
            _oldOperation = Operation.None;
            _lastOperationButton = Operation.None;
            if (resetScreen) monitorData = "0";
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (_error) return;
            if (!_dotPressed)
            {
                if (_inInput)
                {
                    if (monitorData != "0")
                        AppendStringToMonitorData(".");
                    else
                        SetStringToMonitorData("0.");
                }
                else
                {
                    SetStringToMonitorData(".");
                    _inInput = true;
                }
                _dotPressed = true;
            }
        }

        private void btnOver_Click(object sender, EventArgs e)
        {
            // 1/x
            if (_error) return;
            decimal screen_value = ObjectAndString.StringToDecimal(monitorData);
            if (screen_value != 0)
            {
                screen_value = 1m/screen_value;
                RefershAll(false);
            }
            SetStringToMonitorData(screen_value.ToString(CultureInfo.InvariantCulture));
        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            if (_error) return;
            decimal screen_value = ObjectAndString.StringToDecimal(monitorData);
            screen_value = (decimal) Math.Sqrt((double) screen_value);
            RefershAll(false);
            SetStringToMonitorData(screen_value.ToString(CultureInfo.InvariantCulture));
        }

        private void btnInvert_Click(object sender, EventArgs e)
        {
            if (_error) return;
            if (_inInput && !string.IsNullOrEmpty(monitorData) && monitorData != "0")
            {
                if (monitorData[0] == '-')
                    monitorData = monitorData.Substring(1);
                else
                    monitorData = "-" + monitorData;
            }
        }

        private void btnMPlus_Click(object sender, EventArgs e)
        {
            if (_error) return;
            _memory += ObjectAndString.StringToDecimal(monitorData);
            ViewMemoryToScreen();
        }

        private void btnMMinus_Click(object sender, EventArgs e)
        {
            if (_error) return;
            _memory -= ObjectAndString.StringToDecimal(monitorData);
            ViewMemoryToScreen();
        }

        private void btnMR_Click(object sender, EventArgs e)
        {
            if (_error) return;
            monitorData = _memory.ToString(CultureInfo.InvariantCulture);
            _inInput = true;
        }

        private void btnMC_Click(object sender, EventArgs e)
        {
            if (_error) return;
            ClearMemory();
        }

        private void btnMS_Click(object sender, EventArgs e)
        {
            if (_error) return;
            _memory = ObjectAndString.StringToDecimal(monitorData);
            ViewMemoryToScreen();
        }

        private void btnBackSpace_Click(object sender, EventArgs e)
        {
            if (_error) return;
            if (_inInput)
            {
                if (monitorData == "")
                    monitorData = "0";
                else if (ObjectAndString.StringToDecimal(monitorData) != 0)
                {
                    monitorData = monitorData.Substring(0, monitorData.Length - 1);
                    if (monitorData == "")
                        monitorData = "0";
                }
            }
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            if (_error) return;
            SetStringToMonitorData("0");
            _inInput = true;
            _dotPressed = false;
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            Opacity = Math.Abs(Opacity - 1) < 0.1 ? 0.65 : 1;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_error) return;
            Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_error) return;
            Paste();
        }

        
    }
}