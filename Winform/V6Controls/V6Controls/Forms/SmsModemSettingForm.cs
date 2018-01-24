using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Forms;
using GSM;

namespace V6Controls.Forms
{
    public partial class SmsModemSettingForm : V6Form
    {
        private GSM_Phone smsModem;
        List<GSM.GSM_Phone> listModem;

        public SmsModemSettingForm()
        {
            InitializeComponent();
            smsModem = V6ControlFormHelper.SmsModem;
        }

        private void btnTimModem_Click(object sender, EventArgs e)
        {
            LoadModemList();
        }

        private void LoadModemList()
        {
            listModem = new List<GSM_Phone>();
            string[] portNames = SerialPort.GetPortNames();
            foreach (string port in portNames)
            {
                GSM_Phone newModem = new GSM_Phone();
                if (newModem.Connect(port, 9600, 8, 300, 300))
                {
                    newModem.ClosePort();
                    listModem.Add(newModem);
                }
            }

            comboBox1.Items.Clear();
            foreach (GSM_Phone modem in listModem)
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
            if (listModem == null) listModem = new List<GSM_Phone>();
            string s = comboBox1.Text;
            if (s != "" && s.StartsWith("COM") && s.Contains(":"))
            {
                string com = s.Substring(0, s.IndexOf(':'));
                foreach (var item in listModem)
                {
                    if (item.PortName == com)
                    {
                        smsModem = item;
                        smsModem.OpenPort();
                        txtConnectPort.Text = "Đã kết nối " + smsModem.PortName + ":" + smsModem.Operator;
                        btnNgatKetNoi.Enabled = true;
                        V6ControlFormHelper.SmsModem_SettingPort = smsModem.PortName;
                        return;
                    }
                }
                //khong co trong list modem
                smsModem = new GSM_Phone();
                if (smsModem.Connect(com, 9600, 8, 300, 300))
                {
                    txtConnectPort.Text = "Đã kết nối " + smsModem.PortName + ":" + smsModem.Operator;
                    V6ControlFormHelper.SmsModem_SettingPort = smsModem.PortName;
                    listModem.Add(smsModem);
                    return;
                }
            }
            else if (s != "" && s.StartsWith("COM") && s.Length > 3)
            {
                smsModem = new GSM_Phone();
                if (smsModem.Connect(s, 9600, 8, 300, 300))
                {
                    txtConnectPort.Text = "Đã kết nối " + smsModem.PortName + ":" + smsModem.Operator;
                    V6ControlFormHelper.SmsModem_SettingPort = smsModem.PortName;
                    listModem.Add(smsModem);
                    return;
                }
            }
        }

    }
}
