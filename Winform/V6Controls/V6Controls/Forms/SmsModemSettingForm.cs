using System;
using System.Collections.Generic;
using System.IO.Ports;
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
            V6ControlFormHelper.listModem = new List<GSM_Phone>();
            string[] portNames = SerialPort.GetPortNames();
            foreach (string port in portNames)
            {
                GSM_Phone newModem = new GSM_Phone();
                if (newModem.Connect(port, 9600, 8, 300, 300))
                {
                    newModem.ClosePort();
                    V6ControlFormHelper.listModem.Add(newModem);
                }
            }

            comboBox1.Items.Clear();
            foreach (GSM_Phone modem in V6ControlFormHelper.listModem)
            {
                comboBox1.Items.Add("" + modem.PortName + ": " + modem.Nhà_mạng);
            }
            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Items.Add("Không kết nối được!");
            }
        }

        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void Connect()
        {
            if (V6ControlFormHelper.listModem == null) V6ControlFormHelper.listModem = new List<GSM_Phone>();
            string s = comboBox1.Text;
            if (s != "" && s.StartsWith("COM") && s.Contains(":"))
            {
                string com = s.Substring(0, s.IndexOf(':'));
                foreach (var item in V6ControlFormHelper.listModem)
                {
                    if (item.PortName == com)
                    {
                        V6ControlFormHelper.SmsModem = item;
                        V6ControlFormHelper.SmsModem.OpenPort();
                        txtConnectPort.Text = "Đã kết nối " + V6ControlFormHelper.SmsModem.PortName + ":" + V6ControlFormHelper.SmsModem.Operator;
                        btnNgatKetNoi.Enabled = true;
                        V6ControlFormHelper.SmsModem_SettingPort = V6ControlFormHelper.SmsModem.PortName;
                        return;
                    }
                }
                //khong co trong list modem
                V6ControlFormHelper.SmsModem = new GSM_Phone();
                if (V6ControlFormHelper.SmsModem.Connect(com, 9600, 8, 300, 300))
                {
                    txtConnectPort.Text = "Đã kết nối " + V6ControlFormHelper.SmsModem.PortName + ":" + V6ControlFormHelper.SmsModem.Operator;
                    V6ControlFormHelper.SmsModem_SettingPort = V6ControlFormHelper.SmsModem.PortName;
                    V6ControlFormHelper.listModem.Add(V6ControlFormHelper.SmsModem);
                    return;
                }
            }
            else if (s != "" && s.StartsWith("COM") && s.Length > 3)
            {
                V6ControlFormHelper.SmsModem = new GSM_Phone();
                if (V6ControlFormHelper.SmsModem.Connect(s, 9600, 8, 300, 300))
                {
                    txtConnectPort.Text = "Đã kết nối " + V6ControlFormHelper.SmsModem.PortName + ":" + V6ControlFormHelper.SmsModem.Operator;
                    V6ControlFormHelper.SmsModem_SettingPort = V6ControlFormHelper.SmsModem.PortName;
                    V6ControlFormHelper.listModem.Add(V6ControlFormHelper.SmsModem);
                    return;
                }
            }
        }

    }
}
