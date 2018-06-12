using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using GSM;

namespace V6Controls.Forms
{
    public partial class SmsModemSettingForm : V6Form
    {
        //private GSM_Phone V6ControlFormHelper.SmsModem;
        //List<GSM.GSM_Phone> V6ControlFormHelper.listModem;

        public SmsModemSettingForm()
        {
            InitializeComponent();
            LoadModemList();
        }

        private void btnTimModem_Click(object sender, EventArgs e)
        {
            LoadModemList();
        }

        private void LoadModemList()
        {
            //if (V6ControlFormHelper.listModem == null)
            {
                V6ControlFormHelper.listModem = new Dictionary<string, GSM_Phone>();
            }
            string[] portNames = SerialPort.GetPortNames();
            
            foreach (string port in portNames)
            {
                GSM_Phone newModem = new GSM_Phone();
                if (newModem.Connect(port, 9600, 8, 300, 300))
                {
                    newModem.ClosePort();
                    V6ControlFormHelper.listModem[port] = newModem;
                }
            }

            comboBox1.Items.Clear();
            foreach (var item in V6ControlFormHelper.listModem)
            {
                var modem = item.Value;
                comboBox1.Items.Add("" + modem.PortName + ": " + modem.Nhà_mạng);
            }

            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Items.Add("Không tìm thấy thiết bị phù hợp!");
            }
        }

        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            string s = comboBox1.Text;
            if (s != "" && s.StartsWith("COM") && s.Contains(":"))
            {
                string com = s.Substring(0, s.IndexOf(':'));
                Connect(com);
            }
            else if (s != "" && s.StartsWith("COM") && s.Length > 3)
            {
                Connect(s);
            }
        }

        public void AutoConnect()
        {
            try
            {
                foreach (object item in comboBox1.Items)
                {
                    string s = item.ToString();
                    if (s != "" && s.StartsWith("COM") && s.Contains(":"))
                    {
                        string com = s.Substring(0, s.IndexOf(':'));
                        Connect(com);
                    }
                    else if (s != "" && s.StartsWith("COM") && s.Length > 3)
                    {
                        Connect(s);
                    }

                    if (V6ControlFormHelper.SmsModem != null && V6ControlFormHelper.SmsModem.IsConnected)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".AutoConnect", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comPort">COM1 or COMXX with xx is a number.</param>
        private void Connect(string comPort)
        {
            if (V6ControlFormHelper.listModem == null) V6ControlFormHelper.listModem = new Dictionary<string, GSM_Phone>();
            //string s = comboBox1.Text;
            //if (s != "" && s.StartsWith("COM") && s.Contains(":"))
            {
                //string com = s.Substring(0, s.IndexOf(':'));
                bool have = false;
                foreach (var item in V6ControlFormHelper.listModem)
                {
                    var modem = item.Value;
                    if (modem.PortName == comPort)
                    {
                        have = true;
                        V6ControlFormHelper.SmsModem = modem;
                        //V6ControlFormHelper.SmsModem.ClosePort();
                        V6ControlFormHelper.SmsModem.OpenPort();
                        txtConnectPort.Text = "Đã kết nối " + V6ControlFormHelper.SmsModem.PortName + ":" +
                                              V6ControlFormHelper.SmsModem.Operator;
                        btnNgatKetNoi.Enabled = true;
                        //V6ControlFormHelper.SmsModem_SettingPort = V6ControlFormHelper.SmsModem.PortName;
                    }
                    else
                    {
                        modem.ClosePort();
                    }
                }
                
                //khong co trong list modem
                if (!have)
                {
                    V6ControlFormHelper.SmsModem = new GSM_Phone();
                    if (V6ControlFormHelper.SmsModem.Connect(comPort, 9600, 8, 300, 300))
                    {
                        txtConnectPort.Text = "Đã kết nối " + V6ControlFormHelper.SmsModem.PortName + ":" +
                                              V6ControlFormHelper.SmsModem.Operator;
                        //  V6ControlFormHelper.SmsModem_SettingPort = V6ControlFormHelper.SmsModem.PortName;
                        V6ControlFormHelper.listModem[comPort] = V6ControlFormHelper.SmsModem;
                        return;
                    }
                }
            }
            //else if (s != "" && s.StartsWith("COM") && s.Length > 3)
            //{
            //    V6ControlFormHelper.SmsModem = new GSM_Phone();
            //    if (V6ControlFormHelper.SmsModem.Connect(s, 9600, 8, 300, 300))
            //    {
            //        txtConnectPort.Text = "Đã kết nối " + V6ControlFormHelper.SmsModem.PortName + ":" + V6ControlFormHelper.SmsModem.Operator;
            //        V6ControlFormHelper.SmsModem_SettingPort = V6ControlFormHelper.SmsModem.PortName;
            //        V6ControlFormHelper.listModem[s] = V6ControlFormHelper.SmsModem;
            //        return;
            //    }
            //}
        }

        private void btnNgatKetNoi_Click(object sender, EventArgs e)
        {
            if (V6ControlFormHelper.SmsModem != null)
            {
                V6ControlFormHelper.SmsModem.ClosePort();
            }

            foreach (var item in V6ControlFormHelper.listModem)
            {
                item.Value.ClosePort();
            }

            txtConnectPort.Text = "";
        }

    }
}
