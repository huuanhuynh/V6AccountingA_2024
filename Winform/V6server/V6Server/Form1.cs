using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace V6Server
{
    public partial class Form1 : Form
    {

        #region ==== Seri Server ====
        const int MAX_CONNECTION = 10;
        const int PORT_NUMBER = 6666;
        static int _connectionsCount = 0;
        static TcpListener serverListener;
        private IPAddress _address;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var computer_name = Environment.MachineName;
            _message += "Server " + computer_name + " Running";

            var ip = Dns.GetHostAddresses(computer_name)[1].ToString();

            _address = IPAddress.Parse(ip);
            //string localComputerName = Dns.GetHostName();
            //IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            CheckForIllegalCrossThreadCalls = false;
            Thread T = new Thread(ServerListener);
            T.IsBackground = true;
            T.Start();
        }

        void ServerListener()
        {
            serverListener = new TcpListener(_address, PORT_NUMBER);
            serverListener.Start();

            var T0 = new Thread(ShowMessage);
            T0.IsBackground = true;
            T0.Start();

            while (_connectionsCount < MAX_CONNECTION)
            {
                Socket ClientSocket = serverListener.AcceptSocket();
                _connectionsCount++;
                _message += "Co ket noi " + ClientSocket.LocalEndPoint;
                
                Thread t = new Thread((obj) =>
                {
                    DoWork((Socket)obj);
                });
                t.Start(ClientSocket);
            }
        }

        void ShowMessage()
        {
            while (true)
            {
                if (_message.Length > 0)
                {
                    richTextBox1.AppendText(_message);
                    richTextBox1.AppendText("\n");
                    richTextBox1.ScrollToCaret();
                    _message = _message.Substring(_message.Length);
                }
                Thread.Sleep(500);
            }
        }

        private string _message = "";
        void DoWork(Socket socket)
        {
            try
            {
                var stream = new NetworkStream(socket);
                var reader = new StreamReader(stream);
                var writer = new StreamWriter(stream);
                writer.AutoFlush = true;

                //writer.WriteLine("Welcome to Student TCP Server");
                //writer.WriteLine("Please enter the student id");
                //writer.WriteLine("C:\\abc");

                while (true)
                {
                    string client_name = reader.ReadLine();

                    if (!String.IsNullOrEmpty(client_name))
                    {
                        if (AllowConnect(client_name))
                        {
                            var seri = V6Init.License.GetSeri(Application.StartupPath);
                            _message = "Gui " + seri;
                            writer.WriteLine(seri);
                        }
                        break;//Gui 1 lan roi thoi
                    }
                }
                stream.Close();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error: " + ex);
            }

            Console.WriteLine("Client disconnected: {0}", socket.RemoteEndPoint);
            socket.Close();
        }

        bool AllowConnect(string clientName)
        {
            try
            {
                var xtbfile = "Clients.xtb";
                DataTable data = new DataTable("Clients");
                if (File.Exists(xtbfile))
                {
                    try
                    {
                        var ds = new DataSet();
                        ds.ReadXml(xtbfile);
                        data = ds.Tables[0];
                        //data.Load(xtbfile,);
                    }
                    catch (Exception ex)
                    {
                        _message += "read xml ex: " + ex.Message;
                        _message += "\nAuto create new xml!";
                        data.Columns.Add("Name", typeof(string));
                        data.Columns.Add("Allow", typeof(string));
                    }
                }
                else
                {
                    data.Columns.Add("Name", typeof (string));
                    data.Columns.Add("Allow", typeof (string));
                }
                var check = false;
                var have = false;
                foreach (DataRow row in data.Rows)
                {
                    if (row["Name"].ToString().Trim().ToUpper() == clientName.ToUpper())
                    {
                        have = true;
                        var allow = row["Allow"].ToString().Trim();
                        check = allow == "1";
                        if (check)
                        {
                            break;
                        }
                    }
                }
                if (check)
                {
                    return true;
                }
                
                if(!have)
                {
                    DataRow row = data.NewRow();
                    row["Name"] = clientName;
                    row["Allow"] = "0";
                    data.Rows.Add(row);
                    data.WriteXml("Clients.xtb", true);
                }

            }
            catch (Exception ex)
            {
                _message += "AllowConnect ex: " + ex.Message;
            }
            return false;
        }

        #endregion

    }
}
