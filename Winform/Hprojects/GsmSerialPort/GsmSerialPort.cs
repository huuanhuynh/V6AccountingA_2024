using System;
using System.Text;
using System.Threading;
using System.IO.Ports;

namespace GsmSerialPort
{
    public class GsmComPort : SerialPort
    {
        public bool Executing { get; set; }
        public bool ManualRead { get; set; }
        public bool Reading { get; set; }
        public string LastReceivedData { get; set; }
        public AutoResetEvent ReceiveNowEvent;

        //public delegate void NewMessageHandle(object o);
        //public event NewMessageHandle OnNewMessageCome;
        #region ==== Event ====
        public delegate void NewDataIncomming(string data);
        public event NewDataIncomming OnNewDataReceived;
        #endregion event



        #region  ==== Open and Close Ports ====
        //Open Port
        public GsmComPort(string p_strPortName, int p_uBaudRate, int p_uDataBits, int p_uReadTimeout, int p_uWriteTimeout)
        {
            ReceiveNowEvent = new AutoResetEvent(false);
            //COM_PORT = new SerialPort();

            try
            {
                this.PortName = p_strPortName;                 //COM1
                this.BaudRate = p_uBaudRate;                   //9600
                this.DataBits = p_uDataBits;                   //8
                this.StopBits = StopBits.One;  //1
                this.Parity = System.IO.Ports.Parity.None;     //None
                this.ReadTimeout = p_uReadTimeout;             //300
                this.WriteTimeout = p_uWriteTimeout;           //300
                this.Encoding = Encoding.GetEncoding("iso-8859-1");
                //Thêm sự kiện nếu cổng được mở.
                this.DataReceived += GsmSerialPort_DataReceived;

                this.Open();
                this.DtrEnable = true;
                this.RtsEnable = true;

                string t = ExecATCommand("AT", 300, "Modem không thể kết nối!");
                if (!t.Contains("OK")) throw new Exception("No AT OK");
            }
            catch (Exception ex)
            {
                this.Close();
                this.DataReceived -= GsmSerialPort_DataReceived;
                //this = null;
                throw ex;
            }
            //return this;
        }

        

        public void OpenPort()
        {
            try
            {
                this.Open();
            }
            catch// (Exception)
            {

                throw;
            }
        }
        public void ClosePort()
        {
            try
            {
                this.Close();
            }
            catch// (Exception ex)
            {
                throw;
            }
        }

        #endregion o n c

        #region ==== ExecuteCommand ====

        /// <summary>
        /// Chỉ truyền lệnh, không cần nhận kết quả.
        /// Kết quả sẽ tự gọi sự kiện nếu có.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="forErrorMessage"></param>
        public void ExecATCommand(string command, string forErrorMessage)
        {
            while (this.Executing)
            {
                //Do nothing;
            }
            try
            {
                this.Executing = true;
                //if (COM_PORT == null) throw new ApplicationException("Cổng chưa được kết nối!");
                this.DiscardOutBuffer();
                this.DiscardInBuffer();
                ReceiveNowEvent.Reset();

                ManualRead = false;
                string t = this.ReadExisting();//Làm sạch
                this.Write(command + "\r");

                this.Executing = false;

                return;
            }
            catch (Exception ex)
            {
                Executing = false;
                throw new ApplicationException(forErrorMessage + " " + ex.Message, ex);
            }
        }

        //Execute AT Command
        /// <summary>
        /// Thực hiện một lệnh AT.
        /// Quăng câu thông báo lỗi nếu thực hiện không thành công!
        /// </summary>
        /// <param name="port">Cổng đang kết nối.</param>
        /// <param name="command">Câu lệnh AT</param>
        /// <param name="responseTimeout"></param>
        /// <param name="errorMessage">Câu thông báo lỗi nếu lệnh không thực hiện được.</param>
        /// <returns>Chuỗi kết quả</returns>
        public string ExecATCommand(string command, int responseTimeout, string errorMessage, int wait = 30)
        {
            while (this.Executing)
            {
                //Chờ
            }
            try
            {
                this.Executing = true;
                //if (COM_PORT == null) throw new ApplicationException("Cổng chưa được kết nối!");
                this.DiscardOutBuffer();
                this.DiscardInBuffer();
                ReceiveNowEvent.Reset();

                ManualRead = true;
                string t = this.ReadExisting();//Làm sạch
                this.Write(command + "\r");
                WaiteData(wait);
                string output = ReadResponse(responseTimeout);
                ManualRead = false;

                this.Executing = false;

                return output;
            }
            catch (Exception ex)
            {
                ManualRead = false;
                Executing = false;
                throw new ApplicationException(errorMessage + "\n" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="responseTimeout"></param>
        /// <param name="errorMessage"></param>
        /// <param name="endRespone">\r\nOK\r\n</param>
        /// <param name="orEnd"></param>
        /// <returns></returns>
        public string ExecATCommand(string command, int responseTimeout, string errorMessage, string endRespone, params string[] orEnd)
        {
            int wait = 30;
            while (this.Executing)
            {
                //Chờ
            }
            try
            {
                this.Executing = true;
                //if (COM_PORT == null) throw new ApplicationException("Cổng chưa được kết nối!");
                this.DiscardOutBuffer();
                this.DiscardInBuffer();
                ReceiveNowEvent.Reset();

                ManualRead = true;
                string t = this.ReadExisting();//Làm sạch
                this.Write(command + "\r");
                WaiteData(wait);
                string input = ReadResponse(responseTimeout, endRespone, orEnd);
                ManualRead = false;

                this.Executing = false;
                return input;
            }
            catch (Exception ex)
            {
                ManualRead = false;
                Executing = false;
                throw new ApplicationException(errorMessage + "\n" + ex.Message, ex);
            }
        }

        /// <summary>
        /// Đợi phản hồi BytesToRead
        /// </summary>
        /// <param name="times"></param>
        private void WaiteData(int times)
        {
            int count = 0;
            while (count < times)
            {
                if (this.BytesToRead > 0) break;
                Thread.Sleep(100);
                count++;
            }
        }
        #endregion execute



        #region ==== Received Data ====
        
        void GsmSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (ManualRead)//Nếu đã gọi hàm có lấy dữ liệu trả về
            {
                ReceiveNowEvent.Set();
                return;
            }

            //AutoReadData
            Reading = true;
            string data = "";
            try
            {
                int count = 0;
                string t = "";
                do
                {
                    if (this.BytesToRead <= 0)
                    {
                        Thread.Sleep(100);
                        count++;
                    }

                    t = this.ReadExisting();
                    data += t;
        
                    //Return tức thì
                    //"\r\nRING\r\n\r\n+CLIP: \"0862570563\",161,\"\",0,\"\",0\r\n"
                    if(BytesToRead == 0)
                    {
                        if (data.StartsWith("\r\nRING\r\n\r\n+CLIP:"))
                        {
                            break;
                        }
                    }


                    if (count >= 100) break;
                    if (this.BytesToRead == 0
                        && (data.TrimStart().StartsWith("+CMTI") || data.TrimStart().StartsWith("RING"))
                        && data.EndsWith("\r\n")
                        && data.Contains(":"))
                        break;

                }
                //"\r\n+CMTI: \"T ò*\", 206\r\n"
                while (!data.EndsWith("\r\nOK\r\n") && !data.EndsWith("\r\n> ")
                    && !data.EndsWith("\r\nERROR\r\n") && !data.EndsWith("\r\nNO CARRIER\r\n"));
            }
            catch (Exception ex)
            {
                data += ex.Message;
            }

            this.LastReceivedData = data.Trim();
            Reading = false;

            //do event...
            if (OnNewDataReceived != null)
                OnNewDataReceived(data);

        }

        /// <summary>
        /// Đọc lại phản hồi từ cổng com
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public string ReadResponse(int timeout)
        {
            string data = string.Empty;
            string t = "";
            int count = 0;
            if (this.BytesToRead <= 0) return t;
            Reading = true;
            try
            {
                int count2 = 0;
                do
                {
                    if (ReceiveNowEvent.WaitOne(timeout, false) || this.BytesToRead > 0)
                    {
                        t = this.ReadExisting();
                        data += t;
                        count++;
                        count2 = 0;
                    }
                    else if (this.BytesToRead == 0)
                    {
                        count2++;
                    }
                }
                //while (!string.IsNullOrEmpty(t));
                while (!data.EndsWith("\r\nOK\r\n") && !data.EndsWith("\r\n> ")
                    && !data.EndsWith("\r\nERROR\r\n") && !data.EndsWith("\r\nNO CARRIER\r\n")
                    && count2 < 10);

                //HasData = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            this.LastReceivedData = data;
            Reading = false;
            return data;
        }

        public string ReadResponse(int timeout, string endWith, params string[] orEndWith)
        {
            string data = string.Empty;
            string t = "";
            int count = 0;
            bool br = false;
            if (this.BytesToRead <= 0) return t;
            Reading = true;
            try
            {

                //while (!HasData)
                //{
                //  Thread.Sleep(200);
                //}
                int count2 = 0;
                do
                {
                    if (ReceiveNowEvent.WaitOne(timeout, false) || this.BytesToRead > 0)
                    {
                        t = this.ReadExisting();
                        data += t;
                        count++;
                        count2 = 0;
                    }
                    else if (this.BytesToRead == 0)
                    {
                        foreach (string s in orEndWith)
                        {
                            if (data.EndsWith(s))
                                br = true;
                        }
                        if (br) break;//break do while
                        count2++;
                    }
                }
                //while (!string.IsNullOrEmpty(t));
                while (!data.EndsWith(endWith)
                    && count2 < 10);

                //HasData = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            this.LastReceivedData = data;
            this.Reading = false;
            return data;
        }

        #endregion read data



    }
}
