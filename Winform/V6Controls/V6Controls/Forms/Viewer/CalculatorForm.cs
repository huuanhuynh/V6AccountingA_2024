using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using V6Tools.V6Convert;

namespace V6Controls.Forms.Viewer
{
    public partial class CalculatorForm : V6Form
    {
        enum Operation
        {
            Cong,
            Tru,
            Nhan,
            Chia,
            Bang,
            None
        };

        private string monitorData {get { return _monitor; } set { _monitor = value;
            ViewToScreen();
        }}
        private string _monitor = "0";

        private void ViewToScreen()
        {
            decimal value = ObjectAndString.StringToDecimal(monitorData);
            int decimals = value.ToString(CultureInfo.InvariantCulture).Split('.').Length > 1
              ? value.ToString(CultureInfo.InvariantCulture).Split('.')[1].Length
              : 0;
            txtScreen.Text = ObjectAndString.NumberToString(value, decimals, ",");
        }

        decimal _memory;

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

        /// <summary>
        /// Trong dòng tính toán.
        /// </summary>
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
                return;
            }
            monitorData += text;
        }

        void SetStringToMonitorData(string text)
        {
            if (text.Length > 25)
            {
                return;
            }
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

                if (keyCode >= key0 && keyCode <= key9)
                {
                    if (_inInput)
                    {
                        if (monitorData == "0")
                            SetStringToMonitorData((keyCode - key0).ToString());
                        else
                            AppendStringToMonitorData((keyCode - key0).ToString());
                    }
                    else
                    {
                        _inInput = true;
                        SetStringToMonitorData((keyCode - key0).ToString());
                    }
                    return true;
                }
                else if (keyCode >= k0 && keyCode <= k9)
                {
                    if (_inInput)
                    {
                        if (monitorData == "0")
                            SetStringToMonitorData((keyCode - k0).ToString());
                        else
                            AppendStringToMonitorData((keyCode - k0).ToString());
                    }
                    else
                    {
                        _inInput = true;
                        SetStringToMonitorData((keyCode - k0).ToString());
                    }
                    return true;
                }
                else if (keyData == Keys.Decimal || keyData == Keys.Oemcomma || keyData == Keys.OemPeriod)
                {
                    btnDot_Click(btnDot, null);
                    return true;
                }
                //else if (keyData == (Keys.Control | Keys.C))
                //{
                //    Copy();
                //    return true;
                //}
                //else if (keyData == (Keys.Control | Keys.V))
                //{
                //    Paste();
                //    return true;
                //}
                else if (eKeyCode == Keys.Enter)
                {
                    btnCalculate_Click(btnEqual, null);
                    return true;
                }
                else if (eKeyCode == Keys.Back)
                {
                    btnBackSpace_Click(null, null);
                    return true;
                }
                else if (eKeyCode == Keys.Insert)
                {
                    btnMPlus_Click(btnMPlus, null);
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
            Operation newOperation = Operation.None;
            Button currentButton = (Button) sender;

            if (currentButton == btnPlus)
                newOperation = Operation.Cong;
            else if (currentButton == btnMultiply)
                newOperation = Operation.Nhan;
            else if (currentButton == btnDivide)
                newOperation = Operation.Chia;
            else if (currentButton == btnSubtract)
                newOperation = Operation.Tru;
            else if (currentButton == btnEqual)
                newOperation = Operation.Bang;

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
            decimal screen_value = ObjectAndString.StringToDecimal(monitorData);
            screen_value = (decimal) Math.Sqrt((double) screen_value);
            RefershAll(false);
            SetStringToMonitorData(screen_value.ToString(CultureInfo.InvariantCulture));
        }

        private void btnInvert_Click(object sender, EventArgs e)
        {
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
            _memory += ObjectAndString.StringToDecimal(monitorData);
            ViewMemoryToScreen();
        }

        private void btnMMinus_Click(object sender, EventArgs e)
        {
            _memory -= ObjectAndString.StringToDecimal(monitorData);
            ViewMemoryToScreen();
        }

        private void btnMR_Click(object sender, EventArgs e)
        {
            monitorData = _memory.ToString(CultureInfo.InvariantCulture);
            _inInput = true;
        }

        private void btnMC_Click(object sender, EventArgs e)
        {
            ClearMemory();
        }

        private void btnMS_Click(object sender, EventArgs e)
        {
            _memory = ObjectAndString.StringToDecimal(monitorData);
            ViewMemoryToScreen();
        }

        private void btnBackSpace_Click(object sender, EventArgs e)
        {
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
            SetStringToMonitorData("0");
            _inInput = true;
            _dotPressed = false;
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            Opacity = Math.Abs(Opacity - 1) < 0.1 ? 0.75 : 1;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste();
        }

        
    }
}