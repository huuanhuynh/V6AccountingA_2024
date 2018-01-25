using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using GsmSerialPort;
using GSM.DATA;

using System.ComponentModel;
using System.Media;

namespace GSM
{
    
    public class GSM_Phone
    {
        #region ==== Properties ====
        //bool _initAutoRec = false;
        //public bool InitAutoReceive
        //{

        //}
        /// <summary>
        /// Bận
        /// </summary>
        public bool Busy
        {
            get { return this.GSM_PORT.Executing || this.GSM_PORT.Reading; }
            set { }
            
        }
        
        /// <summary>
        /// Chế độ gửi lệnh
        /// </summary>
        public CommandMode Command_Mode {
            get { return _commandMode; }
            set
            {
                if (_commandMode != value)
                {
                    _commandMode = value;
                    if (GSM_PORT != null && GSM_PORT.IsOpen)
                        SetCommandMode(_commandMode);
                }
            }
        }
        CommandMode _commandMode = CommandMode.PDU_Mode;

        public bool EnableNewMessageEvent { 
            get
            {
                return _enableNewMessageEvent;
            }
            set
            {
                _enableNewMessageEvent = value;
                if(GSM_PORT!=null && GSM_PORT.IsOpen)
                {
                    if(value)
                    {
                        EnableAutoReceive();
                    }
                    else
                    {
                        DisableAutoReceive();
                    }
                }
            }
        }
        private bool _enableNewMessageEvent = false;
        
        [DefaultValue(true)]
        public bool EnableNewMessageSound { get; set; }

        public bool EnableNewCallEvent { get; set; }
        public bool EnableNewCallSound { get; set; }
        public bool EnableSaveNewMessage { get; set; }
        public bool EnableSaveSendMessage { get; set; }
        public string LastReceive
        {
            get
            {
                return this.GSM_PORT.LastReceivedData;
            }
        }
        
        public int NewUnreadMessageCount { get; set; }
        

        private Storage NewestMessageStorage = Storage.ME;
        private int NewestMessageIndex = 0;
        public void RemoveWarning()
        {
            NewestMessageStorage = Storage.SM;
            NewestMessageIndex = 1;
        }
        //public ShortMessageCollection CurrentMessagesList { get; private set; }

        #region ==== Inbox ====
        ShortMessageCollection inbox;
        //bool inboxFresh = false;
        public ShortMessageCollection InboxMessagesList
        {
            get
            {
                //if (inboxFresh) return inbox;

                inbox = new ShortMessageCollection();

                inbox.AddRange(InboxReadedMessagesList);
                inbox.AddRange(InboxUnreadMessagesList);
                
                //inboxFresh = true;
                return inbox;
            }
        }

        ShortMessageCollection inboxR;
        bool inboxRfresh = false;
        public ShortMessageCollection InboxReadedMessagesList
        {
            get
            {
                if (inboxRfresh) return inboxR;

                inboxR = new ShortMessageCollection();

                if ((this._storage & Storage.SM) != Storage.NO)
                    inboxR.AddRange(ReadListSMS(Storage.SM, Message_Type.REC_READ));
                if ((this._storage & Storage.ME) != Storage.NO)
                    inboxR.AddRange(ReadListSMS(Storage.ME, Message_Type.REC_READ));
                if((this._storage & Storage.COM1) != Storage.NO)
                    inboxR.AddRange(ReadListSMS_COM(Message_Type.REC_READ));
                //inboxR = Read_REC_READ();
                inboxRfresh = true;

                return inboxR;
            }
        }

        ShortMessageCollection inboxU;
        public ShortMessageCollection InboxUnreadMessagesList
        {
            get
            {
                //inboxU = Read_REC_UNREAD();
                inboxU = new ShortMessageCollection();

                if ((this._storage & Storage.SM) != Storage.NO)
                    inboxU.AddRange(ReadListSMS(Storage.SM, Message_Type.REC_UNREAD));
                if ((this._storage & Storage.ME) != Storage.NO)
                    inboxU.AddRange(ReadListSMS(Storage.ME, Message_Type.REC_UNREAD));
                if ((this._storage & Storage.COM1) != Storage.NO)
                    inboxU.AddRange(ReadListSMS_COM(Message_Type.REC_UNREAD));
                
                if (inboxU.Count > 0)
                {
                    inboxRfresh = false;
                }

                return inboxU;
            }
        }

        #endregion inbox

        #region ==== inbox outbox COM ====
        //ShortMessageCollection inboxCom;
        public SmsData inboxDataCom = new SmsData("Inbox", "inbox.dat", 1);
        public SmsData outboxDataCom = new SmsData("Outbox", "outbox.dat", 2);
        //bool inboxComFresh = false;
        public ShortMessageCollection InboxComMessageList
        {
            get
            {
                //return new ShortMessageCollection();
                return inboxDataCom.MessageList;
            }
        }
        public ShortMessageCollection OutboxComMessageList
        {
            get
            {
                return outboxDataCom.MessageList;
            }
        }

        #endregion com

        #region === Outbox ====
        ShortMessageCollection outbox;
        //bool outboxFresh = false;
        public ShortMessageCollection OutboxMessagesList
        {
            get
            {
                //if (outboxFresh) return outbox;

                outbox = new ShortMessageCollection();

                outbox.AddRange(OutboxSentMessagesList);
                outbox.AddRange(OutboxUnsendMessagesList);
                //outboxFresh = true;

                return outbox;
            }
        }

        ShortMessageCollection outboxS;
        bool _outboxSfresh = false;
        bool outboxSfresh
        {
            get { return _outboxSfresh; }
            set
            {
                _outboxSfresh = value;
                //if (!value) outboxFresh = value;
            }
        }
        public ShortMessageCollection OutboxSentMessagesList
        {
            get
            {
                if (outboxSfresh) return outboxS;

                outboxS = new ShortMessageCollection();
                if ((this._storage & Storage.SM) != Storage.NO)
                    outboxS.AddRange(ReadListSMS(Storage.SM, Message_Type.STO_SENT));
                if ((this._storage & Storage.ME) != Storage.NO)
                    outboxS.AddRange(ReadListSMS(Storage.ME, Message_Type.STO_SENT));
                if ((this._storage & Storage.COM2) != Storage.NO)
                    outboxS.AddRange(ReadListSMS_COM(Message_Type.STO_SENT));

                //outboxS = Read_STO_SENT();
                outboxSfresh = true;

                return outboxS;
            }
        }

        ShortMessageCollection outboxU;
        bool _outboxUfresh = false;
        bool outboxUfresh
        {
            get { return _outboxUfresh; }
            set
            {
                _outboxUfresh = value;
                //if (!value) outboxFresh = value;
            }
        }
        public ShortMessageCollection OutboxUnsendMessagesList
        {
            get
            {
                if (outboxUfresh) return outboxU;

                outboxU = new ShortMessageCollection();
                if ((this._storage & Storage.SM) != Storage.NO)
                    outboxU.AddRange(ReadListSMS(Storage.SM, Message_Type.STO_UNSENT));
                if ((this._storage & Storage.ME) != Storage.NO)
                    outboxU.AddRange(ReadListSMS(Storage.ME, Message_Type.STO_UNSENT));
                if ((this._storage & Storage.COM2) != Storage.NO)
                    outboxU.AddRange(ReadListSMS_COM(Message_Type.STO_UNSENT));

                //outboxU = Read_STO_UNSENT();
                outboxUfresh = true;

                return outboxU;
            }
        }
        #endregion outbox

        void UnFreshAll()
        {
            //inboxFresh = false;
            inboxRfresh = false;
            
            //outboxFresh = false;
            outboxSfresh = false;
            outboxUfresh = false;
        }


        //public ShortMessageCollection DrafMessagesList { get; private set; }
        

        public GsmComPort GSM_PORT;
        public string PortName
        {
            get
            {
                if (GSM_PORT != null)
                    return GSM_PORT.PortName;
                return "null";
            }
        }

        Storage _storage = Storage.ALL;

        public GSM.Storage SelectedStorage
        {
            get
            {
                return _storage;
            }
            set
            {
                if (value != _storage)
                {
                    _storage = value;
                    UnFreshAll();
                }
            }
        }

        public int MessageReference = 1;
        public ENUM_TP_VALID_PERIOD Validition = ENUM_TP_VALID_PERIOD.Maximum;

        #endregion p

        #region ==== Answers Call Hangup Operation ====
        public void Answers()
        {
            Ringing = false;
            StopNewCallSound();
            GSM_PORT.ExecATCommand(AT.ATA, "Answers error!");
            if(EnableNewCallSound && spC!= null)
            {
                spC.Stop();
            }
        }
        public void Call(string number)
        {
            GSM_PORT.ExecATCommand(AT.ATD_(number), Str.CallError);
        }
        public void Hangup()
        {
            GSM_PORT.ExecATCommand(AT.ATH, "Hangup error!");
            if (EnableNewCallSound && spC != null)
            {
                spC.Stop();
            }
        }
        #endregion call operation

        #region ==== Check Status ====


        /// <summary>
        /// Kiểm tra thiết bị có đang đặt chế độ tự gửi tín hiệu qua cổng com hay không.
        /// Dùng để tạo sự kiện nhận tin nhắn mới, nhận cuộc gọi.
        /// </summary>
        /// <returns></returns>
        public bool IsAutoReceive
        {
            get
            {
                bool result = false;// return result;
                try
                {
                    //"AT+CNMI?\r\r\n+CNMI: 0, 0, 0, 0, 0\r\n\r\nOK\r\n"
                    //"AT+CNMI=0,0\r\r\nOK\r\n"
                    //"AT+CNMI=1,1\r\r\nOK\r\n"
                    string s = this.GSM_PORT.ExecATCommand(AT.AT_CNMI_request, 300, "Error");
                    if (s.Trim().EndsWith("OK"))
                    {
                        string[] ss = s.Split(':');
                        if (ss.Length == 2)
                        {
                            string[] s01234 = ss[1].Split(',');
                            if (s01234.Length > 1)
                            {
                                if (s01234[0].Trim() == "1" && s01234[1].Trim() == "1")
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                catch// (Exception)
                {
                    result = false;
                }
                return result;
            }
        }
        public bool IsPDUmode
        {
            get
            {
                bool result = false;// return result;
                try
                {
                    string s = this.GSM_PORT.ExecATCommand(AT.AT_CMGF, 300, "Error");
                    if (s.Trim().EndsWith("OK"))
                    {
                        string[] ss = s.Split(':');
                        if (ss.Length == 2)
                        {
                            string[] s01234 = ss[1].Split(',');
                            if (s01234.Length > 1)
                            {
                                if (s01234[0].Trim() == "1" && s01234[1].Trim() == "1")
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                catch// (Exception)
                {
                    result = false;
                }
                return result;
            }
        }


        public bool DisableAutoReceive()
        {
            bool result = false;// return result;
            try
            {
                //"AT+CNMI?\r\r\n+CNMI: 0, 0, 0, 0, 0\r\n\r\nOK\r\n"
                //"AT+CNMI=0,0\r\r\nOK\r\n"
                //"AT+CNMI=1,1\r\r\nOK\r\n"
                string s = this.GSM_PORT.ExecATCommand(AT.AT_CNMI_set(0, 0), 300, "Error");
                if (s.Trim().EndsWith("OK"))
                {
                    s = s.Substring(0, s.IndexOf("OK")).Trim();
                    string[] ss = s.Split('=');
                    if (ss.Length == 2)
                    {
                        string[] s01234 = ss[1].Split(',');
                        if (s01234.Length > 1)
                        {
                            if (s01234[0].Trim() == "0" && s01234[1].Trim() == "0")
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch// (Exception)
            {
                result = false;
            }
            return result;
        }
        public bool EnableAutoReceive()
        {
            bool result = false;// return result;
            try
            {
                //"AT+CNMI?\r\r\n+CNMI: 0, 0, 0, 0, 0\r\n\r\nOK\r\n"
                //"AT+CNMI=0,0\r\r\nOK\r\n"
                //"AT+CNMI=1,1\r\r\nOK\r\n"
                string s = this.GSM_PORT.ExecATCommand(AT.AT_CNMI_set(1, 1), 300, "Error");
                if (s.Trim().EndsWith("OK"))
                {
                    s = s.Substring(0, s.IndexOf("OK")).Trim();
                    string[] ss = s.Split('=');
                    if (ss.Length == 2)
                    {
                        string[] s01234 = ss[1].Split(',');
                        if (s01234.Length > 1)
                        {
                            if (s01234[0].Trim() == "1" && s01234[1].Trim() == "1")
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch// (Exception)
            {
                result = false;
            }
            return result;
        }

        #endregion check status

        #region ==== Command Get Set ====
        
        public CommandMode GetCommandMode()
        {
            if (IsPDUmode) return CommandMode.PDU_Mode;
            else return CommandMode.Text_Mode;
        }
        public void SetCommandMode(CommandMode mode)
        {
            switch (mode)
            {
                case CommandMode.PDU_Mode:
                    SetPDUmode();
                    break;
                case CommandMode.Text_Mode:
                    SetTextMode();
                    break;
                default:
                    break;
            }
            UnFreshAll();
        }

        public void SetPhoneBookStorage(PhoneBookStorage storage)
        {
            GSM_PORT.ExecATCommand(AT.AT_CPBS_(storage), 300, "Lỗi chọn bộ nhớ danh bạ.");
        }

        public Contact GetContact(PhoneBookStorage storage, int index)
        {
            Contact c = new Contact("", "");
            SetPhoneBookStorage(storage);
            string o = GSM_PORT.ExecATCommand(AT.AT_CPBR_(index), 300, "");
            return c;
        }

        public void SetPDUmode()
        {
            GSM_PORT.ExecATCommand(AT.AT_CMGF_0, 300, "Lỗi cài đặt chế độ PDU.");
        }
        public void SetTextMode()
        {
            GSM_PORT.ExecATCommand(AT.AT_CMGF_1, 300, "Lỗi cài đặt chế độ Text.");
        }

        public Storage MessageStorage
        {
            get { return GetMessageStorage(); }
            set { SetMessageStorage(value); }
        }

        public Storage GetMessageStorage()
        {
            //return Storage.ALL;
            Storage result = Storage.ALL;
            try
            {
                string s = this.GSM_PORT.ExecATCommand(AT.AT_CPMS + "?", 300, "Error");
                if (s.Trim().EndsWith("OK"))
                {
                    string[] ss = s.Split(':');
                    if (ss.Length == 2)
                    {
                        string[] s01234 = ss[1].Split(',');
                        if (s01234.Length > 1)
                        {
                            s = s01234[0].Trim();
                            if (s == "\"SM\"")
                            {
                                result = Storage.SM;
                            }
                            else if(s == "\"ME\"")
                            {
                                result = Storage.ME;
                            }
                            else if(s== "\"MT\"")
                            {
                                result = Storage.MT;
                            }
                        }
                    }
                }
            }
            catch// (Exception)
            {
                result = Storage.ALL;
            }
            return result;
        }
        public void SetMessageStorage(Storage MS)
        {
            GSM_PORT.ExecATCommand(AT.AT_CPMS_(MS), 300, "Không thể chọn bộ nhớ tin nhắn.");
        }

        
        #endregion command set

        #region ==== Contructor ====
        public GSM_Phone()
        {
            SelectedStorage = GSM.Storage.ALL;
        }
        #endregion contructor

        #region ==== Connect and Close Port ====
        /// <summary>
        /// Kiểm tra lại kết nối với modem.
        /// Nếu không còn kết nối sẽ đóng cổng!
        /// </summary>
        /// <returns></returns>
        public bool RecheckConect()
        {
            try
            {
                GSM_PORT.ExecATCommand("AT", 300,
                        "Modem bị lỗi!");
                return true;

            }
            catch// (Exception ex)
            {
                ClosePort();
                return false;
            }
        }

        public bool Connect(string portName)
        {
            return Connect(portName, 9600, 8, 300, 300);
        }

        public bool Connect(string portName, int bRate, int dataBit, int rTimeOut, int wTimeOut)
        {

            try
            {
                Init_GSM_PORT(portName, bRate, dataBit, rTimeOut, wTimeOut);
                return GSM_PORT.IsOpen;
            }
            catch
            {
                ClosePort();
                return false;
            }
        }

        /// <summary>
        /// (portName, 9600, 8, 300, 300);
        /// </summary>
        /// <param name="portName"></param>
        /// <returns></returns>
        public bool TryConnect(string portName)
        {
            try
            {
                Init_GSM_PORT(portName, 9600, 8, 300, 300);

                return GSM_PORT.IsOpen;
            }
            catch
            {
                ClosePort();
                return false;
            }
        }

        public string AutoConnect()
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                if (TryConnect(port))
                {
                    return port;
                }
            }
            return null;
        }

        public string AutoConnect(string oldPort)
        {
            if (TryConnect(oldPort))
            {
                return oldPort;
            }
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                if (TryConnect(port))
                {
                    return port;
                }
            }

            return null;
        }



        public void ClosePort()
        {
            try
            {
                GSM_PORT.ClosePort();
            }
            catch
            {
            }
        }
        public void OpenPort()
        {
            try
            {
                GSM_PORT.OpenPort();
            }
            catch
            {

            }
        }
        private void Init_GSM_PORT(string portName, int bRate, int dataBit, int rTimeOut, int wTimeOut)
        {
            //Reset info
            _operator = "";

            GSM_PORT = new GsmComPort(portName, bRate, dataBit, rTimeOut, wTimeOut);
            GSM_PORT.OnNewDataReceived += GSM_PORT_OnNewDataReceived;


            if (GSM_PORT.IsOpen)
            {
                this.Command_Mode = this._commandMode;
                this.EnableNewMessageEvent = this._enableNewMessageEvent;
                //this.EnableNew
            }
            else
            {
                ClosePort();
            }
            //Get some info
            GetOperator();
        }
        #endregion conect and close port

        #region ==== Events ====

        public delegate void NewCallHandle(string data);
        public event NewCallHandle NewCallEvent;
        
        public delegate void NewMessageHandle(string data);
        public event NewMessageHandle NewMessageEvent;

        
        /// <summary>
        /// Phân tích data và đưa ra sự kiện tương ứng!
        /// </summary>
        /// <param name="data"></param>
        void GSM_PORT_OnNewDataReceived(string data)
        {
            data = data.Trim();
            if (data.StartsWith("+CMTI"))
            {
                HandleNewMessageIncomming(data);
            }
            //else if (data == "RING")
            //{
            //    StopNewCallSound();
            //}
            else if(data.StartsWith("RING"))
            {
                HandleNewCallIncomming(data);
            }
            
            
        }
        public bool Ringing = false;
        void HandleNewCallIncomming(string data)
        {
            if (EnableNewCallEvent)
            {
                if (EnableNewCallSound)
                {
                    PlayNewCallSound();

                    StartCountDownToStopNewCallSound();
                }
                
                Ringing = true;
                if (NewCallEvent != null)
                {
                    NewCallEvent(data);
                }
            }
        }

        int countdown = 0;
        private void StartCountDownToStopNewCallSound()
        {
            if (countdown == 0)
            {
                countdown = 5;
                Thread t = new Thread(CountDownToStopNewCallSound);
                t.IsBackground = true;
                t.Start();
            }
            else
            {
                countdown = 5;
            }
        }
        private void CountDownToStopNewCallSound()
        {
            while (countdown>0)
            {
                countdown--;
                Thread.Sleep(1000);
            }
            StopNewCallSound();
        }
        void HandleNewMessageIncomming(string data)
        {
            NewUnreadMessageCount++;
            //inboxUFresh = false;

            if (EnableNewMessageEvent && NewMessageEvent != null)
            {
                
                PlayNewMessageSound();
                NewMessageEvent(data);
            }
        }
        #endregion event

        #region ==== Send Command, Run Command ====
        /// <summary>
        /// Chạy lệnh không cần nhận kết quả trả về.
        /// Sau đó có thể lấy lastReceive
        /// </summary>
        /// <param name="ATcommand"></param>
        public void RunCommand(string ATcommand)
        {
            GSM_PORT.ExecATCommand(ATcommand, "Error!");
        }
        public string SendCommand(string ATcommand)
        {
            string output = GSM_PORT.ExecATCommand(ATcommand, 500, "Error!");
            return output;
        }
        #endregion send command


        #region ==== SMS ====

        #region ==== Read SMS ====

        #region ==== Read One ====
        //public ShortMessage Read1SMS(Storage storage, int index)
        //{
        //    return Read1SMS(storage.ToString(), index);
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="storage">"SM" or "ME" or "TM" or "T??"</param>
        /// <param name="index"></param>
        /// <returns></returns>
        public ShortMessage Read1SMS(Storage storage, int index)
        {
            ShortMessage sms = null;
            string input = "";
            try
            {
                #region Execute Command
                // Check connection
                //GSM_PORT.ExecATCommand("AT", 300, "Không có modem được kết nối");
                // Use message format "Text mode"
                input = GSM_PORT.ExecATCommand("AT+CMGF=1", 300, "Failed to set message format.");
                // Use character set "PCCP437"
                //ExecCommand(port,"AT+CSCS=\"PCCP437\"", 300, "Failed to set character set.");
                // Select storage
                input = GSM_PORT.ExecATCommand(AT.AT_CPMS_(storage), 300, "Không thể chọn bộ nhớ tin nhắn.");
                // Read the messages
                input = GSM_PORT.ExecATCommand(AT.AT_CMGR_(index), 300, "Không thể đọc tin nhắn!");
                #endregion

                #region Parse messages
                string[] strarray = input.Trim()
                    .Split(new string[] { AT.CMGR_respone }, StringSplitOptions.None);
                sms = ParseOneMessage(strarray[1]);
                sms.Storage = storage;
                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (sms != null)
                return sms;
            else
                return null;
        }
        public ShortMessage Read1SMS(int index)
        {
            ShortMessage sms = null;
            string input = "";
            try
            {
                #region Execute Command
                input = GSM_PORT.ExecATCommand("AT+CMGF=1", 300, "Failed to set message format.");
                input = GSM_PORT.ExecATCommand(AT.AT_CMGR_(index), 300, "Không thể đọc tin nhắn!");
                #endregion

                #region Parse messages
                string[] strarray = input.Trim()
                    .Split(new string[] { AT.CMGR_respone }, StringSplitOptions.None);
                string[] ss2 = strarray[1].Split(new string[] {"\r\n\r\n"}, StringSplitOptions.None);
                sms = ParseOneMessage("" + index + "," + ss2[0].Trim());
                sms.Storage = this.MessageStorage;
                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (sms != null)
                return sms;
            else
                return null;
        }
        #endregion read one

        #region ==== Read Any ====
        /// <summary>
        /// Đọc tất cả tin nhắn trên bộ nhớ đang chọn.
        /// </summary>
        /// <returns></returns>
        public ShortMessageCollection ReadListSMS()
        {
            return ReadListSMS(SelectedStorage);
        }
        /// <summary>
        /// Đọc tất cả tin nhắn từ bộ nhớ tham chiếu.
        /// Trước khi gọi hàm này nên kiểm tra kiểu MT và thay bằng cách gộp SM và ME
        /// </summary>
        /// <param name="MS">Bộ nhớ tham chiếu</param>
        /// <returns></returns>
        public ShortMessageCollection ReadListSMS(GSM.Storage MS)
        {
            return ReadListSMS_AnyType(MS,
                AT.StoredMessageType.REC_READ, AT.StoredMessageType.REC_UNREAD,
                AT.StoredMessageType.STO_SENT, AT.StoredMessageType.STO_UNSENT);
        }

        /// <summary>
        /// Đọc tin nhắn trên thiết bị bằng lệnh AT+CMGL
        /// Command message list
        /// </summary>
        /// <param name="MS">Bộ nhớ lưu trữ tin nhắn (trên sim hoặc trên máy, mấy thứ khác đang lỗi)</param>
        /// <param name="type">Loại tin nhắn cần đọc, đã đọc, chưa đọc, đã gửi chưa gửi</param>
        /// <returns></returns>
        public ShortMessageCollection ReadListSMS(Storage MS, Message_Type ATStoredMessageType)
        {
            switch (ATStoredMessageType)
            {
                case Message_Type.REC_UNREAD:
                    return ReadListSMS(MS, AT.StoredMessageType.REC_UNREAD);
                    //break;
                case Message_Type.REC_READ:
                    return ReadListSMS(MS, AT.StoredMessageType.REC_READ);
                    //break;
                case Message_Type.STO_UNSENT:
                    return ReadListSMS(MS, AT.StoredMessageType.STO_UNSENT);
                    //break;
                case Message_Type.STO_SENT:
                    return ReadListSMS(MS, AT.StoredMessageType.STO_SENT);
                    //break;
                //case Message_Type.ALL:
                    //return ReadListSMS(MS, AT.StoredMessageType.a.STO_UNSENT);
                    //break;
                //case Message_Type.UNKNOW:
                    //break;
                default:
                    return ReadListSMS(MS, AT.StoredMessageType.ALL);
                    //break;
            }
            
        }

       


        /// <summary>
        /// Đọc các kiểu trên bộ nhớ đang chọn.
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public ShortMessageCollection ReadListSMS_AnyType(params string[] types)
        {
            return ReadListSMS_AnyType(this.SelectedStorage, types);
        }
        public ShortMessageCollection ReadListSMS_AnyType(GSM.Storage storage, params string[] types)
        {
            ShortMessageCollection result = new ShortMessageCollection();
            if (storage != GSM.Storage.ALL)
            {
                //Duyệt qua các type, add vào list
                foreach (string type in types)
                {
                    result.AddRange(ReadListSMS(storage, type));
                }
            }
            else
            {
                foreach (string item in types)
                {
                    result.AddRange(ReadListSMS(GSM.Storage.SM, item));
                    result.AddRange(ReadListSMS(GSM.Storage.ME, item));
                }
            }
            return result;
        }
        #endregion read any

        //#region ==== REC READ ====
        //public ShortMessageCollection Read_REC_READ()
        //{
        //    return Read_REC_READ(SelectedStorage);
        //}
        //public ShortMessageCollection Read_REC_READ(GSM.Storage storage)
        //{
        //    ShortMessageCollection result = ReadListSMS(storage, AT.StoredMessageType.REC_READ);
        //    return result;
        //}
        ////public ShortMessageCollection Read_REC_READ_PDU(GSM.Storage storage)
        ////{
        ////    ShortMessageCollection result = ReadListSMS_PDU(storage, Message_Type.REC_READ);
        ////    return result;
        ////}
        //#endregion rec read

        //#region ==== REC UNREAD ====
        ///// <summary>
        ///// Đọc các tin nhắn chưa đọc trên bộ nhớ đang chọn.
        ///// </summary>
        ///// <returns></returns>
        //public ShortMessageCollection Read_REC_UNREAD()
        //{
        //    return Read_REC_UNREAD(SelectedStorage);
        //}
        ////public ShortMessageCollection Read_REC_UNREAD_PDU()
        ////{
        ////    return Read_REC_UNREAD_PDU(SelectedStorage);
        ////}
        ///// <summary>
        ///// Đọc tin nhắn chưa đọc trên bộ nhớ tham chiếu.
        ///// </summary>
        ///// <param name="storage"></param>
        ///// <returns></returns>
        //public ShortMessageCollection Read_REC_UNREAD(GSM.Storage storage)
        //{
        //    ShortMessageCollection result = ReadListSMS(storage, AT.StoredMessageType.REC_UNREAD);
        //    return result;
        //}
        ////public ShortMessageCollection Read_REC_UNREAD_PDU(GSM.Storage storage)
        ////{
        ////    ShortMessageCollection result = ReadListSMS_PDU(storage, Message_Type.REC_UNREAD);
        ////    return result;
        ////}
        //#endregion rec unread

        #region ==== STO SENT ====
        public ShortMessageCollection Read_STO_SENT()
        {
            return Read_STO_SENT(SelectedStorage);
        }

        public ShortMessageCollection Read_STO_SENT(GSM.Storage storage)
        {
            ShortMessageCollection result = ReadListSMS(storage, AT.StoredMessageType.STO_SENT);
            return result;
        }
        #endregion sto sent

        #region ==== STO UNSENT ====
        public ShortMessageCollection Read_STO_UNSENT()
        {
            return Read_STO_UNSENT(SelectedStorage);
        }

        public ShortMessageCollection Read_STO_UNSENT(GSM.Storage storage)
        {
            ShortMessageCollection result = ReadListSMS(storage, AT.StoredMessageType.STO_UNSENT);
            return result;
        }
        #endregion sto unsent

        #region ==== READ on SM ====
        /// <summary>
        /// Đọc tất cả các tin nhắn trên sim
        /// </summary>
        /// <returns></returns>
        public ShortMessageCollection ReadSMS_SiM()
        {
            return ReadListSMS(GSM.Storage.SM);
        }
        /// <summary>
        /// Đọc tin nhắn trên sim theo loại.
        /// </summary>
        /// <param name="type">Loại tin</param>
        /// <returns></returns>
        public ShortMessageCollection ReadSMS_SiM(string ATStoType)
        {
            return ReadListSMS(GSM.Storage.SM, ATStoType);
        }
        #endregion read sm

        #region ==== READ on ME ====
        /// <summary>
        /// Đọc tất cả các tin nhắn lưu trên thiết bị
        /// </summary>
        /// <returns></returns>
        public ShortMessageCollection ReadSMS_ME()
        {
            return ReadListSMS(GSM.Storage.ME);
        }
        /// <summary>
        /// Đọc các tin nhắn lưu trên thiết bị theo loại
        /// </summary>
        /// <param name="ATStoType">Loại</param>
        /// <returns></returns>
        public ShortMessageCollection ReadSMS_ME(string ATStoType)
        {
            return ReadListSMS(GSM.Storage.SM, ATStoType);
        }
        #endregion read me

        #region ==== READ on MT ====
        /// <summary>
        /// Đọc tất các tin nhắn trên cả bộ nhớ sim và máy.
        /// Call ReadSMS()
        /// </summary>
        /// <returns></returns>
        public ShortMessageCollection ReadSMS_MT()
        {
            return ReadListSMS();
        }
        /// <summary>
        /// Đọc các tin nhắn trên hai bộ nhớ theo loại
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ShortMessageCollection ReadSMS_MT(string ATStoType)
        {
            //Đọc trên máy trước
            ShortMessageCollection result = new ShortMessageCollection();
            result.AddRange(ReadListSMS(GSM.Storage.SM, ATStoType));
            result.AddRange(ReadListSMS(GSM.Storage.ME, ATStoType));
            return result;
        }
        #endregion read mt

        public ShortMessageCollection ReadListSMS_COM(string messageType)
        {
            messageType = messageType.ToUpper();

            if (messageType.StartsWith("REC UNREAD"))
                return ReadListSMS_COM(Message_Type.REC_UNREAD);
            else if (messageType.StartsWith("REC READ"))
                return ReadListSMS_COM(Message_Type.REC_READ);
            else if (messageType.StartsWith("STO UNSENT"))
                return ReadListSMS_COM(Message_Type.STO_UNSENT);
            else if (messageType.StartsWith("STO SENT"))
                return ReadListSMS_COM(Message_Type.STO_SENT);
            else if (messageType == "ALL")
                return ReadListSMS_COM(Message_Type.ALL);
            else
                return new ShortMessageCollection();
        }

        public ShortMessageCollection ReadListSMS_COM(Message_Type messageType)
        {
            switch (messageType)
            {
                case Message_Type.REC_UNREAD:
                    return inboxDataCom.UnreadMessageList;
                case Message_Type.REC_READ:
                    return inboxDataCom.ReadMessageList;
                    
                case Message_Type.STO_UNSENT:
                    return outboxDataCom.UnsendMessageList;
                case Message_Type.STO_SENT:
                    return outboxDataCom.SendMessageList;;
                    
                case Message_Type.ALL:
                    ShortMessageCollection sms_colection = new ShortMessageCollection();
                    sms_colection.AddRange(inboxDataCom.MessageList);
                    sms_colection.AddRange(outboxDataCom.MessageList);
                    return sms_colection;
                    
                default:
                    return new ShortMessageCollection();
                    
            }
        }

        #region ==== READ LIST MESSAGE ATCOMMAND ====
        /// <summary>
        /// Đọc tin nhắn trong bộ nhớ MS theo kiểu
        /// </summary>
        /// <param name="MS">SM, ME, MT...</param>
        /// <param name="messageType">REC UNREAD, REC READ, STO UNSENT, STO SENT</param>
        /// <returns></returns>
        public ShortMessageCollection ReadListSMS(Storage MS, string messageType)
        {
            ShortMessageCollection sms_colection = new ShortMessageCollection();
            if(MS == Storage.MT)//Bộ nhớ máy + sim
            {
                sms_colection.AddRange(ReadListSMS(Storage.SM, messageType));
                sms_colection.AddRange(ReadListSMS(Storage.ME, messageType));
                return sms_colection;
            }
            else if (MS == Storage.ALL)//Thuong lay het o day
            {
                sms_colection.AddRange(ReadListSMS(Storage.SM, messageType));
                sms_colection.AddRange(ReadListSMS(Storage.ME, messageType));
                sms_colection.AddRange(ReadListSMS_COM(messageType));
                return sms_colection;
            }
            else if (MS == Storage.COM || MS == Storage.COM1 || MS == Storage.COM2)
            {
                throw new Exception("Nouse");
                //return ReadListSMS_COM(messageType);
            }
            else//Còn lại các trường hợp đọc trên máy và sim
            {

                ShortMessageCollection SMS_colection = new ShortMessageCollection();
                string strRespone = "";

                if (_commandMode == CommandMode.Text_Mode)
                {
                    #region TextMode
                    try
                    {


                        #region Execute Command

                        SetTextMode();
                        SetMessageStorage(MS);
                        // Read the messages
                        strRespone = GSM_PORT.ExecATCommand(AT.AT_CMGL_(messageType), 6000, "Lỗi đọc tin nhắn!");
                        #endregion

                        #region Parse messages
                        SMS_colection = ParseListMessages(strRespone);
                        foreach (ShortMessage msg in SMS_colection)
                        {
                            msg.Storage = MS;
                        }
                        #endregion

                        if (messageType == AT.StoredMessageType.REC_UNREAD)
                        {
                            NewUnreadMessageCount = 0;
                            if(EnableSaveNewMessage)
                            foreach (ShortMessage item in SMS_colection)
                            {
                                inboxDataCom.WriteSms(item);
                            }
                            
                        }

                    }
                    catch (Exception ex)
                    {
                        Busy = false;
                        throw new Exception(ex.Message);
                    }

                    #endregion
                }
                else //PDU_mode
                {
                    int type = 0;
                    switch (messageType)
                    {
                        case AT.StoredMessageType.REC_UNREAD: type = 0; break;
                        case AT.StoredMessageType.REC_READ: type = 1; break;
                        case AT.StoredMessageType.STO_UNSENT: type = 2; break;
                        case AT.StoredMessageType.STO_SENT: type = 3; break;
                        default:
                            type = 4;//ALL
                            break;
                    }

                    #region Read message PDU mode
                    try
                    {
                        //if (MS == "MT" || MS == "ALL") return ReadListSMS(GSM.Storage.MT, AT_StoredMessageType);

                        #region Execute Command
                        SetPDUmode();
                        SetMessageStorage(MS);
                        string output = GSM_PORT.ExecATCommand(AT.AT_CMGL_((int)type), 1000, "Không thể đọc tin nhắn!");
                        #endregion

                        #region Parse messages pdu
                        if (output.Length > 0)
                        {
                            if (output.EndsWith("\r\nOK\r\n"))
                                output = output.Substring(0, output.Length - 6);
                            SMS_colection = ParseListMessages_PDU(output);

                        }
                        #endregion Parse messages pdu

                        if (type == 0)
                        {
                            NewUnreadMessageCount = 0;
                            if(EnableSaveNewMessage)
                            foreach (ShortMessage sms in SMS_colection)
                            {
                                inboxDataCom.WriteSms(sms);
                            }
                        }

                        foreach (ShortMessage msg in SMS_colection)
                        {
                            msg.Storage = MS;
                        }


                    }
                    catch (Exception ex)
                    {
                        Busy = false;
                        throw new Exception(ex.Message);
                    }
                    #endregion
                }

                Busy = false;
                if (SMS_colection != null)
                    return SMS_colection;
                else
                    return new ShortMessageCollection();
            }
        }
        #endregion readlist
        

        #region ==== Parse Message ====
        /// <summary>
        /// Gọt dũa tin nhắn từ tín hiệu trả về của lệnh CMGL
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ShortMessageCollection ParseListMessages(string input)
        {
            if (input.EndsWith("\r\n\r\nOK\r\n"))
            {
                input = input.TrimEnd('\r', '\n').Substring(0, input.Length - "\r\n\r\nOK\r\n".Length);
            }
            ShortMessageCollection messages = new ShortMessageCollection();
            string[] spliptString = input.Split(new string[]{AT.CMGL+":"}, StringSplitOptions.None);
            try
            {
                foreach (string str in spliptString)
                {
                    ShortMessage ms = ParseOneMessage(str);
                    if(ms!=null) messages.Add(ms);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return messages;
        }
        
        string messageText = "";
        //"3,\"STO SENT\",\"01687439716\",,time\r\nnội dung\r\n"
        Regex regexOneInMsg = new Regex(@"(\d+),""(.+)"",""(.*)"",(.*),(.*)");
        //"25,\"STO SENT\",\"841223335767\",\r\nan moi an com xong."
        Regex regexOneOutMsg = new Regex(@"(\d+),""(.+)"",(.*)");
        Match m;
        
        /// <summary>
        /// Phân tích 1 tin nhắn đọc ở dạng text
        /// </summary>
        /// <param name="str">"122,\"REC READ\",\"888\",,\"2014/12/08 12:10:10+28\"\r\nTin nhan sai\r\n</param>
        /// <returns>ShortMessage</returns>
        public ShortMessage ParseOneMessage(string str)
        {
            m = regexOneInMsg.Match(str);
            if (m.Success)
            {
                try
                {
                    ShortMessage msg = new ShortMessage();

                    msg.Index = int.Parse(m.Groups[1].Value);
                    msg.MessageTypeString = m.Groups[2].Value;
                    msg.PhoneNumber = m.Groups[3].Value;
                    if (!msg.PhoneNumber.StartsWith("0")) msg.PhoneNumber = "+" + msg.PhoneNumber;
                    msg.Alphabet = m.Groups[4].Value;
                    string strDate_time = m.Groups[5].Value.Trim('"').Split('+')[0];

                    if (strDate_time.Contains("-"))
                        strDate_time = strDate_time.Substring(0, strDate_time.IndexOf('-'));

                    DateTime d = new DateTime(1900, 1, 1);
                    if (strDate_time.Length >= 19)
                        d = DateTime.ParseExact(strDate_time, "yyyy/M/d HH:mm:ss", null);
                    msg.Date_time = d;

                    //msg.Message = m.Groups[6].Value.Trim();
                    messageText = str.Substring(str.IndexOf("\r\n") + 2).Trim();
                    //if (messageText.Length > 4) messageText = messageText.Substring(2, messageText.Length - 2);
                    //else messageText = "";
                    msg.Message = messageText;
                    //messages.Add(msg);
                    return msg;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

//AT+CMGL=1

//+CMGL: 2, 1,, 58
//07914889200030F72407D0C769730A0000511010919463822ECD30081F1E83DA69371A440FA7416BF43BEC061DDFEF33BB0C1AD7C32071D80D628741B9190D769301

//+CMGL: 3, 1,, 73
//07914889200009F3040C9148614568073400084121623235348236005400681EED0020006E0067006800691EC7006D0020006D1ED90074002000740069006E00200055004E00490043004F00440045002E

//+CMGL: 4, 1,, 155
//05914809806844038090F0000041217280154182A0050003C8020178D1A16F88AEBBCF20FA3B0DC2A7DD2074D80D4287DD6810B9FE1E83E8E8B7FB0C1287DF21D09448058DEB619038EC0691EBEF31A85E07D1D36537885D0F83C6E8B71B444787DBA0733A0C42BFDB2077380F229741F17A380FA2A3EB6FF719D44C169D20283209A2C1602E180CE682C16020F2DB7D06D9C3A018688C4E97C7A066593E2E93CB
        public ShortMessageCollection ParseListMessages_PDU(string code)
        {   
            ShortMessageCollection messages = new ShortMessageCollection();
            string[] spliptString = code.Split(new string[] { AT.CMGL + ":" }, StringSplitOptions.None);
            try
            {
                foreach (string str in spliptString)
                {
                    ShortMessage ms = ParseOneMessage_PDU(str.Trim());
                    if (ms != null) messages.Add(ms);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return messages;
        }

        Regex oneMsgPDU = new Regex(@"(\d+), (\d+),(.*), (\d+)\r\n(.*)");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">index, type(rec read = 1), name, length\r\n07914889200030F72407...</param>
        /// <param name="index"></param>
        /// <returns></returns>
        public ShortMessage ParseOneMessage_PDU(string code)
        {
//1, 1,, 58
//07914889200030F72407D0C769730A0000511010919463822ECD30081F1E83DA69371A440FA7416BF43BEC061DDFEF33BB0C1AD7C32071D80D628741B9190D769301
            m = oneMsgPDU.Match(code);
            if (m.Success)
            {
                try
                {
                    int index = int.Parse(m.Groups[1].Value);
                    int m_type = int.Parse(m.Groups[2].Value);
                    int length = int.Parse(m.Groups[4].Value);
                    Message_Type message_Type = Message_Type.UNKNOW;
                    switch (m_type)
                    {
                        case 0: message_Type = Message_Type.REC_UNREAD; break;
                        case 1: message_Type = Message_Type.REC_READ; break;
                        case 2: message_Type = Message_Type.STO_UNSENT; break;
                        case 3: message_Type = Message_Type.STO_SENT; break;
                        default:
                            break;
                    }
                    string pdu = m.Groups[5].Value;
                    ShortMessage sms = new ShortMessage(index, message_Type, length, pdu);

                    return sms;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
        #endregion parse

        #endregion ReadSMS


        #region ==== Send SMS ====

        /// <summary>
        /// Gửi tin nhắn ở chế độ text
        /// Chưa làm phần lưu tin đã gửi!
        /// </summary>
        /// <param name="number">Số điện thoại nhận tin nhắn.</param>
        /// <param name="message">Nội dung tin nhắn sẽ gửi.</param>
        /// <returns>Thông báo gửi vào modem (không phải send report)</returns>
        public string SendSms(string number, string message, bool save=false)
        {
            try
            {
                ShortMessage sms = new ShortMessage();
                sms.SetMessage(message);// = message;
                sms.PhoneNumber = number;
                sms.MessageType = Message_Type.STO_UNSENT;
                //string number = txtNumber.Text, message = txtMessage.Text;
                switch (SendMessage(sms, save))
	            {
		            case SendSmsStatus.NONE:
                        return "Chưa gửi đến " + number + " nội dung:\n" + message;
                        //break;
                    case SendSmsStatus.OK:
                        return "Đã gửi đến " + number + " nội dung:\n" + message;
                        //break;
                    case SendSmsStatus.ERROR:
                        return "Không gửi được tin nhắn được tới " + number + "!";
                        //break;
                    case SendSmsStatus.UNKNOWN:
                        return "Không chắc đã gửi đến " + number + " nội dung:\n" + message;
                        //break;
                    default:
                        return "";
                        //break;
	            }
                
            }
            catch (Exception ex)
            {
                throw new Exception("SenSms " + ex.Message);
            }
            //return "";
        }

        public int SendSmstoAny(string[] numbers, string message)
        {
            return numbers.Length;
        }


        public enum SendSmsStatus
        {
            NONE, OK, ERROR, UNKNOWN
        }
        public SendSmsStatus SendMessage(ShortMessage sms, bool save = false)
        {
            SendSmsStatus sendStatus = SendSmsStatus.NONE;
            string command = "";
            string recievedData = "";
            try
            {
                //Kiểm tra kết nối. (khi viết lại hàm gửi hàng loạt chỉ nên kiểm 1 lần!
                //string recievedData = GSM_PORT.ExecATCommand("AT", 300, "Không có modem được kết nối!");
                //Định dạng text mode = text. [1]
                recievedData = GSM_PORT.ExecATCommand(AT.AT_CMGF_1, 300, "Không thể thiết lập định dạng tin nhắn.");
                // "AT+CMGS=\"" + phoneNumber + "\"";
                recievedData = GSM_PORT.ExecATCommand(AT.AT_CMGS_(sms.PhoneNumber), 300, "Không chấp nhận số điện thoại!");
                command = sms.Message + char.ConvertFromUtf32(26) + "\r";
                //kết thúc bằng mã 26 (tương ứng với tổ hợp phím Ctrl+Z)
                recievedData = GSM_PORT.ExecATCommand(command, 300, "Không gửi được tin nhắn!", 20);
                
                if (recievedData.EndsWith("\r\nOK\r\n"))
                {
                    sendStatus = SendSmsStatus.OK;
                    sms.MessageType = Message_Type.STO_SENT;
                }
                else if (recievedData.Contains("ERROR"))
                {
                    sendStatus = SendSmsStatus.ERROR;
                    sms.MessageType = Message_Type.STO_UNSENT;
                }
                else
                {
                    sendStatus = SendSmsStatus.UNKNOWN;
                    //Đã gửi nhưng chưa chắc thành công
                    //Vì vài loại modem có kiểu trả về khác
                    sms.MessageType = Message_Type.UNKNOW;
                }

                if (save)
                {
                    outboxDataCom.WriteSms(sms);
                    outboxUfresh = false;
                }

                return sendStatus;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public SendSmsStatus SendMessage_PDU_Flash(string phoneNumber, string message, bool save = false)
        {
            ShortMessage sms = new ShortMessage();
            sms.Message = message;
            //sms.TP_DCS = (byte)ENUM_TP_DCS.Class0_UD_7bits;
            sms.FlashMode = true;
            sms.PhoneNumber = phoneNumber;
            sms.MessageReference = this.MessageReference;
            sms.Validity_Period = this.Validition;
            sms.TP_SCTS = DateTime.Now;
            sms.MessageType = Message_Type.STO_UNSENT;
            return SendMessage_PDU(sms,save);
        }

        public SendSmsStatus SendMessage_PDU(string phoneNumber, string message, bool save = false)
        {
            ShortMessage sms = new ShortMessage();
            sms.Message = message;
            sms.PhoneNumber = phoneNumber;
            sms.MessageReference = this.MessageReference;
            sms.Validity_Period = this.Validition;
            sms.TP_SCTS = DateTime.Now;
            sms.MessageType = Message_Type.STO_UNSENT;
            return SendMessage_PDU(sms, save);
        }

        private string ChooseSendCommand(string chooseKey, params string[] values)
        {
            //Giải mã chooseKey
            //Switch to Command ...
            return "";
        }
        public SendSmsStatus SendMessage_PDU(ShortMessage sms, bool save = false)
        {
            SendSmsStatus sendStatus = SendSmsStatus.NONE;
            string recievedData01 = "";
            string recievedData02 = "";
            try
            {
                recievedData01 = GSM_PORT.ExecATCommand("AT+CMGF=0", 300, "Không thể thiết lập định dạng tin nhắn PDU.");
                string commandSend = "";
                int[] lengthArray;
                string[] pduStringArray = sms.GetPDUsend(out lengthArray);
                for (int i = 0; i < pduStringArray.Length; i++)
                {
                    commandSend = "AT+CMGS=" + lengthArray[i];
                    recievedData01 = GSM_PORT.ExecATCommand(commandSend, 300, "Lỗi gửi tin PDU .");
                    
                    commandSend = pduStringArray[i] + char.ConvertFromUtf32(26);// "\x001a";
                    recievedData02 = GSM_PORT.ExecATCommand(commandSend, 300, "Không gửi được tin nhắn PDU!");

                }
                
                if (recievedData02.EndsWith("\r\nOK\r\n"))
                {
                    sendStatus = SendSmsStatus.OK;
                    sms.MessageType = Message_Type.STO_SENT;
                }
                else if (recievedData02.Contains("ERROR"))
                {
                    sendStatus = SendSmsStatus.ERROR;
                    sms.MessageType = Message_Type.STO_UNSENT;
                }
                else
                {
                    sendStatus = SendSmsStatus.UNKNOWN;
                    //Đã gửi nhưng chưa chắc thành công
                    //Vì vài loại modem có kiểu trả về khác hoặc bị chậm
                    //Nếu viết code tốt có thể sẽ không có trường hợp này
                    //Nhưng mình đã mắc thường xuyên
                    sms.MessageType = Message_Type.UNKNOW;
                }

                if (save)
                {
                    outboxDataCom.WriteSms(sms);
                    outboxUfresh = false;
                }

                return sendStatus;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SendMessage_ToManyContacts(Contacts contacts, string Message, out string errors)
        {
            int send = 0;
            string recievedData = "";
            try
            {
                //Kiểm tra kết nối. (khi viết lại hàm gửi hàng loạt chỉ nên kiểm 1 lần!
                //string recievedData = GSM_PORT.ExecATCommand("AT", 300, "Không có modem được kết nối!");
                //Định dạng text mode = text. [1]
                recievedData = GSM_PORT.ExecATCommand("AT+CMGF=1", 300, "Không thể thiết lập định dạng tin nhắn.");

                string errorStr = "";
                foreach (Contact contact in contacts)
                {
                    try
                    {
                        //Không hiểu lắm việc gửi tin nhắn lại phải chia ra làm 2.
                        String command = "AT+CMGS=\"" + contact.Number + "\"";
                        GSM_PORT.ExecATCommand(command,300, "Không chấp nhận số điện thoại!");
                        command = Message + char.ConvertFromUtf32(26) + "\r";
                        GSM_PORT.ExecATCommand(command,300, "Không gửi được tin nhắn");

                        send++;
                        //Thread.Sleep(300);
                    }
                    catch (Exception ex)
                    {
                        errorStr += contact.ToString() +": " + ex.Message + "\n";
                    }
                }

                errors = errorStr;
               
                return send;
            }
            catch (Exception ex)
            {
                errors = "SendManyExp " + ex.Message;
                return send;
            }
        }

        #endregion Send SMS

        #region ==== Send USSD ====
        public string SendUSSD(string ussd)
        {
            return ussd;
        }
        #endregion send ussd

        #region ==== Delete SMS ====

        public bool DeleteSms(ShortMessage sms)
        {
            bool result = false;
            try
            {
                if (sms.Storage.ToString().StartsWith("COM"))
                {
                    result = DeleteMessageCOM(sms.Storage, sms.Index);
                }
                else
                {
                    result = DeleteMsg_ATcommand(sms.Storage, sms.Index);
                    if (result)
                    {
                        switch (sms.MessageType)
                        {
                            case Message_Type.REC_READ:
                                //inboxFresh = false;
                                inboxRfresh = false;
                                break;

                            case Message_Type.STO_UNSENT:
                            case Message_Type.STO_SENT:
                                outboxSfresh = false;
                                //outboxFresh = false;
                                break;
                            default:
                                break;
                        }
                    }
                }
                return result;
            }
            catch// (Exception ex)
            {
                return false;
            }
        }

        private bool DeleteMessageCOM(Storage comStorageType, int index)
        {
            bool result = false;
            try
            {
                switch (comStorageType)
                {
                    case Storage.COM1:
                        result = inboxDataCom.DeleteSmsAt(index);
                        //inboxFresh = false;
                        inboxRfresh = false;
                        break;
                    case Storage.COM2:
                        result = outboxDataCom.DeleteSmsAt(index);
                        outboxSfresh = false;
                        //outboxFresh = false;
                        break;
                    case Storage.COM:
                        //Xóa cả 2 nơi.
                        result = inboxDataCom.DeleteSmsAt(index) || outboxDataCom.DeleteSmsAt(index);
                        break;
                    default:
                        break;
                }
                return result;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public bool DeleteAllReadSmsInSim()
        {
            try
            {
                bool result = DeleteMsg_ATcommand(Storage.SM, 0, 1);
                if(result)
                {
                    inboxRfresh = false;
                    //inboxFresh = false;
                }
                return result;
            }
            catch// (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteAllReadandSendSmsInSim()
        {
            try
            {
                bool result = DeleteMsg_ATcommand(Storage.SM, 0, 2);
                if (result)
                {
                    inboxRfresh = false;
                    //inboxFresh = false;
                    outboxSfresh = false;
                    //outboxFresh = false;
                }
                return result;
            }
            catch// (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Xóa tin nhắn modem
        /// </summary>
        /// <param name="port">Cổng kết nối thiết bị.</param>
        /// <param name="index">Vị trí tin nhắn chọn xóa.</param>
        /// <param name="delflag">Cờ xóa, nếu khác 0 sẽ bỏ qua tham số index
        /// 0 mặc định: xóa theo index
        /// 1 Xóa toàn bộ tin nhắn đã đọc trên bộ nhớ đang tham chiếu.
        /// 2 Xóa toàn bộ tin nhắn đã đọc và đã gửi.
        /// 3 Xóa toàn bộ tin nhắn đã đọc, đã gửi và chưa gửi.
        /// 4 Xóa hết, chừa lại tin nhắn chưa đọc.
        /// </param>
        /// <param name="storage">Chọn bộ nhớ AT+CPMS="ME","SM","MT"
        /// SM. It refers to the message storage area on the SIM card.
        /// ME. It refers to the message storage area on the GSM/GPRS modem or mobile phone. Usually its storage space is larger than that of the message storage area on the SIM card.
        /// MT. It refers to all message storage areas associated with the GSM/GPRS modem or mobile phone. For example, suppose a mobile phone can access two message storage areas: "SM" and "ME". The "MT" message storage area refers to the "SM" message storage area and the "ME" message storage area combined together.
        /// BM. It refers to the broadcast message storage area. It is used to store cell broadcast messages.
        /// SR. It refers to the status report message storage area. It is used to store status reports.
        /// TA. It refers to the terminal adaptor message storage area.</param>
        /// <returns></returns>
        public bool DeleteMsg_ATcommand(Storage storage, int index, int delflag=0)
        {
            Busy = true;
            //AT+CMGD=<index>[,<delflag>]
            //AT+CPMS chọn bộ nhớ, ? xem trạng thái, "=SM" chọn bộ nhớ sim, "=ME"
            bool isDeleted = false;
            string recievedData = "";
            try
            {
                string command = AT.AT_CMGD_(index, delflag);// "AT+CMGD=" + index;
                
                #region Execute Command
                //string recievedData = GSM_PORT.ExecATCommand("AT", 300, "Không có thiết bị được kết nối.");
                //Chua test khi xoa co can chon che do text hoac pdu hay khong.
                //recievedData = GSM_PORT.ExecATCommand("AT+CMGF=1", 300, "Không chọn được định dạng.");
                
                recievedData = GSM_PORT.ExecATCommand(AT.AT_CPMS_(storage), 300, "Không chọn được bộ nhớ.");

                recievedData = GSM_PORT.ExecATCommand(command, 3000, "Không thể xóa tin nhắn!");
                #endregion

                if (recievedData.EndsWith("\r\nOK\r\n"))
                {
                    isDeleted = true;
                    
                }
                else if (recievedData.Contains("ERROR"))
                {
                    isDeleted = false;
                }
                else
                {
                    //Don't know
                }
                
            }
            catch (Exception ex)
            {
                Busy = false;
                throw new Exception(ex.Message);
            }
            Busy = false;
            return isDeleted;
        }
        #endregion Delete sms

       

        #endregion SMS
        
        #region ==== Signal ====
        /// <summary>
        /// 0-1, 2-9, 11-14, 15-19, 20-30.
        /// ●♠♣♦♥
        /// </summary>
        /// <returns></returns>
        public int GetSignalStrength()
        {
            int strength = 0;
            int lever = 0;
            try
            {
                #region Execute Command
                // Check connection
                //GSM_PORT.ExecATCommand("AT", 300, "Không có modem được kết nối");
                // Use message format "Text mode"
                //GSM_PORT.ExecATCommand("AT+CMGF=1", 300, "Failed to set message format.");
                // Use character set "PCCP437"
                //ExecCommand(port,"AT+CSCS=\"PCCP437\"", 300, "Failed to set character set.");
                
                
                // Get the signal strength
                string input = GSM_PORT.ExecATCommand("AT+CSQ", 300, "Không thể lấy thông tin!");
                #endregion

                #region Parse
                strength = ParseSignal(input);
                if (strength == 0) lever = 0;
                if (strength == 1) lever = 1;
                if (strength >= 2) lever = 2;
                if (strength >= 9) lever = 3;
                if (strength >= 14) lever = 4;
                if (strength >= 19) lever = 5;
                return lever;
                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int ParseSignal(string input)
        {
            //"AT+CSQ\r\r\n+CSQ: 14, 99\r\n\r\nOK\r\n"
            string[] spliptString = input.Split(':');
            if (spliptString.Length > 1)
            {
                string info = spliptString[1];
                string[] s01 = info.Split(',');
                if (s01.Length > 0)
                {
                    return Convert.ToInt32(s01[0].Trim());
                }
            }
            return 0;
        }
        //AT+COPS?
        private string _operator = "";
        /// <summary>
        /// Tên mạng
        /// </summary>
        public string Operator
        {
            get { return GetOperator(); }
        }
        public string Nhà_mạng
        {
            get { return GetOperator(); }
        }
        public string GetOperator()
        {
            if (_operator.Length > 0) return _operator;
            Busy = true;
            string o0 = "";
            string o1 = "";
            //AT+COPS?
            try
            {
                #region Execute Command
                
                string input = GSM_PORT.ExecATCommand("AT+COPS?", 100, "Không thể lấy thông tin!",1);
                #endregion

                #region Parse
                o0 = ParseO(input);
                //45202 (Mạng Vinaphone)
                //45201 (Mạng Mobifone)
                //45203 (Mạng Beeline)
                //45204 (Mạng Viettel)
                //45205 (Mạng Vietnamobile)
                o1 = "Mạng:" + o0;
                if (o0 == "45202") o1 = "Vinaphone";
                if (o0 == "45201") o1 = "Mobifone";
                if (o0 == "45203") o1 = "Gmobile";
                if (o0 == "45204") o1 = "Viettel";
                if (o0 == "45205") o1 = "Vietnamobile";

                _operator = o1;
                
                #endregion

            }
            catch// (Exception ex)
            {
                _operator = "";
                Busy = false;
                return o1;
            }
            Busy = false;
            return o1;
        }

        /// <summary>
        /// Phân tích số hiệu nhà mạng
        /// </summary>
        /// <param name="input">port output</param>
        /// <returns>45204</returns>
        private string ParseO(string input)
        {
//AT+COPS?

//+COPS: 0,0,"45204"

//OK
            //"AT+COPS?\r\r\n+COPS: 0,0,\"45204\"\r\n\r\nOK\r\n"

            string[] spliptString = input.Split(new string[] { "+COPS: " }, StringSplitOptions.None);
            string info = spliptString[1];
            //+CSQ: 4,0
            //\d la so . la chuoi "" la dau "
            //+COPS: 0,0,\"45204\"
            Regex r = new Regex(@"(\d+),(\d+),""(\d+)""");
            Match m = r.Match(info);
            if (m.Success)
            {
                return m.Groups[3].Value;
            }

            return "???";
        }
        #endregion signal

        #region ==== Sound ====
        #region --- Message Sound ---

        string newMessageSoundFile = "Sounds\\NewMessage.wav";
        
        public string NewMessageSoundFile
        {
            get { return newMessageSoundFile; }
            set { newMessageSoundFile = value; }
        }

        private void PlayNewMessageSound()
        {
            if (EnableNewMessageSound)
            {
                if (File.Exists(newMessageSoundFile))
                {


                    try
                    {
                        var p = new  SoundPlayer();
                        //System.Windows.Forms.MessageBox.Show("Test1");
                        newMessageSoundFile = Path.GetFullPath(newMessageSoundFile);
                        p.SoundLocation = newMessageSoundFile;
                        //System.Windows.Forms.MessageBox.Show("TestLoad");
                        p.Load();
                        //System.Windows.Forms.MessageBox.Show("TestPlaySync");
                        p.Play();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Test " + ex.Message);
                    }
                }
            }
        }
        #endregion message sound

        #region --- NewCall Sound ---
        SoundPlayer spC;// = new MediaPlayer();
        string newCallSoundFile = "Sounds\\NewCall.wav";

        public string NewCallSoundFile
        {
            get { return newCallSoundFile; }
            set { newCallSoundFile = value; }
        }

        public bool IsConnected { get { return GSM_PORT != null && GSM_PORT.IsOpen; } }

        private void PlayNewCallSound()
        {
            if (EnableNewCallSound && File.Exists(newCallSoundFile))
            {
                if (spC == null)
                {
                    try
                    {
                        spC = new SoundPlayer();
                        newCallSoundFile = Path.GetFullPath(newCallSoundFile);
                        if (spC.SoundLocation != newCallSoundFile)
                        {
                            spC.SoundLocation = newCallSoundFile;
                            spC.Load();
                        }

                        //if(!spC.IsLoadCompleted)
                        {
                            spC.PlayLooping();
                        }
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
        }

        //void mpNewCall_MediaEnded(object sender, EventArgs e)
        //{

        //    MediaPlayer player = sender as MediaPlayer;
        //    if (player == null)
        //        return;

        //    player.Position = new TimeSpan(0);
        //    player.Play();

        //}

        public void StopNewCallSound()
        {
            if (spC != null)
            {
                spC.Stop();
                spC.Dispose();
                //spC = null;
            }
        }
        #endregion newcall sound

        #endregion sound

        #region ==== Phone book ====
        //Chưa có gì
        #endregion




        
    }
    
}
