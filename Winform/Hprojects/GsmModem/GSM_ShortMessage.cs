using System;
using System.Collections.Generic;
using System.Text;

namespace GSM
{
    public class ShortMessage
    {

        #region ==== Contructor ====
        public ShortMessage()
        {
            ServiceCentreNumber = "";
            this.SC_Number = "00";
            this.Date_time = DateTime.Now;
        }
        /// <summary>
        /// Lấy thông tin từ chuổi PDU đọc được
        /// </summary>
        /// <param name="strPDU"></param>
        /// <param name="type012">0: phân tích theo messageType
        /// 1: phân tích theo kiểu tin nhận
        /// 2: phân tích theo kiểu tin gửi</param>
        public ShortMessage(int index, Message_Type messageType, int length, string strPDU, int type012 = 0)
        {//sửa lại parameter index, type, lengh?, pdu. :D
            this._index = index;
            this._SMS_mode = SMS_Source.PDU_mode;
            this._messageType = messageType;
            
            if (type012 == 1
                || messageType == Message_Type.REC_READ
                || messageType == Message_Type.REC_UNREAD
                || messageType == Message_Type.UNKNOW)
            {
            
                #region Phân tích tin nhắn nhận
                //Lay 1 byte 8bits = 2Hex
                SCAddressLength = GsmEncoding.GetByte(ref strPDU);// (byte)Convert.ToSByte(PDUCode.Substring(0, 2), 16);

                //Lấy chuỗi HEX, - 1 vì đâu?
                if (SCAddressLength > 0)
                {
                    SCAddressType = GsmEncoding.GetByte(ref strPDU);// (byte)Convert.ToSByte(PDUCode.Substring(0, 2), 16);
                    if (SCAddressLength == 1)
                    {
                        this.serviceCentreNumber = "Type01:" + GsmEncoding.GetString(ref strPDU, 2);
                    }
                    else
                    {
                        int getLength = (SCAddressLength - 1) * 2;
                        string address = GsmEncoding.GetString(ref strPDU, getLength);
                        //Giải mã chuỗi address, đảo ngược từng cặp , bỏ F ở cuối.
                        this.ServiceCentreNumber = GetAddress(address);
                    }
                }

                FirstOctet = GsmEncoding.GetByte(ref strPDU);// (byte)Convert.ToSByte(PDUCode.Substring(0, 2), 16);

                this.phoneNumberLength = GsmEncoding.GetByte(ref strPDU);//(byte)Convert.ToSByte(PDUCode.Substring(0, 2), 16);
                this.phoneNumberType = GsmEncoding.GetByte(ref strPDU);// (byte)Convert.ToSByte(PDUCode.Substring(0, 2), 16);

                //Độ dài số điện thoại, Nếu lẻ thì + thêm 1 cho chữ F
                this.phoneNumberLength = (byte)(this.phoneNumberLength + (this.phoneNumberLength % 2));
                string sdtN = GsmEncoding.GetString(ref strPDU, this.phoneNumberLength);
                this.PhoneNumber = GetPhoneNumber(sdtN);

                TP_PID = GsmEncoding.GetByte(ref strPDU);// (byte)Convert.ToSByte(PDUCode.Substring(0, 2), 16); PDUCode = PDUCode.Substring(2);
                TP_DCS = GsmEncoding.GetByte(ref strPDU);// (byte)Convert.ToSByte(PDUCode.Substring(0, 2), 16); PDUCode = PDUCode.Substring(2);

                //Ngày tháng hoặc thời hạn gửi tin
                //string firstOctetBin = GsmEncoding.IntToFixBin(FirstOctet, 8);

                string hexDateString = strPDU.Substring(0, 14);// PDUreceive = PDUreceive.Substring(14);

                try
                {
                    TP_SCTS = GetDate(hexDateString);
                    strPDU = strPDU.Substring(14);
                }
                catch
                {
                    this.TP_VP = GsmEncoding.GetByte(ref strPDU);
                    TP_SCTS = new DateTime(1900, 1, 1);
                }

                TP_UDL = GsmEncoding.GetByte(ref strPDU);// SMSBase.GetByte(ref PDUCode);
                //TP_UD = GetString(ref PDUreceive, TP_UDL * 2);
                TP_UD = strPDU;
                #endregion
            }
            else if(type012 == 2
                || messageType == Message_Type.STO_SENT
                || messageType == Message_Type.STO_UNSENT)
            {
                #region Phân tích tin nhắn gửi đi
                SCAddressLength = GsmEncoding.GetByte(ref strPDU);
                //Lấy chuỗi HEX, - 1 vì đâu?
                if (SCAddressLength > 0)
                {
                    SCAddressType = GsmEncoding.GetByte(ref strPDU);
                    int getLength = (SCAddressLength - 1) * 2;
                    string address = GsmEncoding.GetString(ref strPDU, getLength);
                    //Giải mã chuỗi address, đảo ngược từng cặp , bỏ F ở cuối.
                    this.serviceCentreNumber = GetAddress(address);
                }

                FirstOctet = GsmEncoding.GetByte(ref strPDU);// (byte)Convert.ToSByte(PDUCode.Substring(0, 2), 16);
                TP_MR = GsmEncoding.GetByte(ref(strPDU));

                this.phoneNumberLength = GsmEncoding.GetByte(ref strPDU);
                this.phoneNumberType = GsmEncoding.GetByte(ref strPDU);

                //Độ dài số điện thoại, Nếu lẻ thì + thêm 1 cho chữ F
                this.phoneNumberLength = (byte)(this.phoneNumberLength + (this.phoneNumberLength % 2));
                string sdtN = GsmEncoding.GetString(ref strPDU, this.phoneNumberLength);
                this.PhoneNumber = GetPhoneNumber(sdtN);

                TP_PID = GsmEncoding.GetByte(ref strPDU);// (byte)Convert.ToSByte(PDUCode.Substring(0, 2), 16); PDUCode = PDUCode.Substring(2);
                TP_DCS = GsmEncoding.GetByte(ref strPDU);// (byte)Convert.ToSByte(PDUCode.Substring(0, 2), 16); PDUCode = PDUCode.Substring(2);
                //string hexDateString = PDUsend.Substring(0, 14); PDUreceive = PDUreceive.Substring(14);
                //TP_SCTS = GetDate(hexDateString);
                TP_VP = GsmEncoding.GetByte(ref strPDU);
                TP_UDL = GsmEncoding.GetByte(ref strPDU);
                TP_UD = strPDU;// GetString(ref PDUsend, TP_UDL * 2);
                #endregion
            }
        }

        //public ShortMessage(string PDUsend)
        //{
        //    SMS_mode = SMS_Source.PDU_mode;
        //    //Lay 1 byte 8bits = 2Hex
        //}

        
        //Thử một tin dài thiệt dài. Thử một tin dài thiệt dài. Thử một tin dài thiệt dài. 
        
        #endregion ctor
        //long sms pdu http://mobiletidings.com/2009/02/18/combining-sms-messages/
        #region ==== First Octet ====
        /// <summary>
        /// Reply path. Parameter indicating that reply path exists.
        /// </summary>
        private byte TP_RP = 0 << 7;
        private byte TP_UDHI_User_data_have_info = 0 << 6; //rd0 mti1
        private byte TP_SRR = 0 << 5;
        private byte TP_VPF = 2 << 3; //4 and 3, value = 10000 = 0x10
        private byte TP_RD = 0 << 2;
        private byte TP_MTI = 1;     //01 mean SMS-SUBMIT
        private byte FirstOctet
        {
            //7 TP-RP 	Reply path. Parameter indicating that reply path exists.
            //6 TP-UDHI	User data header indicator. This bit is set to 1 if the User Data field starts with a header 
            //5 TP-SRR	Status report request. This bit is set to 1 if a status report is requested
            //43 TP-VPF //0 0 : TP-VP field not present
            //1 0 : TP-VP field present. Relative format (one octet)
            //0 1 : TP-VP field present. Enhanced format (7 octets)
            //1 1 : TP-VP field present. Absolute format (7 octets)
            //2 TP_RD     Reject duplicates. Parameter indicating whether or not the SC shall accept
            //an SMS-SUBMIT for an SM still held in the SC which has the same TP-MR and the same
            //TP-DA as a previously submitted SM from the same OA.
            //10    TP-MTI	Message type indicator. Bits no 1 and 0 are set to
            //0 and 1 respectively to indicate that this PDU is an SMS-SUBMIT 
            get
            {
                return (byte)(TP_RP + TP_UDHI_User_data_have_info + TP_SRR + TP_VPF + TP_RD + TP_MTI);//.ToString("X2");
            }
            set
            {
                int bValue = value;// Convert.ToInt32(value, 16);
                TP_RP = (byte)(bValue & Convert.ToInt32("10000000", 2));
                TP_UDHI_User_data_have_info = (byte)(bValue & Convert.ToInt32("01000000", 2));
                TP_SRR = (byte)(bValue & Convert.ToInt32("00100000", 2));
                TP_VPF = (byte)(bValue & Convert.ToInt32("00011000", 2));
                TP_RD = (byte)(bValue & Convert.ToInt32("00000100", 2));
                TP_MTI = (byte)(bValue & Convert.ToInt32("00000011", 2));
            }
        }
        #endregion first octest
        public bool FlashMode { get; set; }
        
        
        public string Text;
        /// <summary>
        /// Kieu encode
        /// </summary>
        public byte TP_DCS { get; set; }
        /// <summary>
        /// Thời hạn gửi tin
        /// </summary>
        byte TP_VP = 0xa7;
        /// <summary>
        /// Thời hạn gửi tin
        /// </summary>
        public ENUM_TP_VALID_PERIOD Validity_Period
        {
            get
            {
                return (ENUM_TP_VALID_PERIOD)this.TP_VP;
            }
            set
            {
                this.TP_VP = (byte)value;
            }
        }
        public byte TP_PID;
        /// <summary>
        /// Nội dung tin nhắn mã hóa
        /// </summary>
        
        public void Set_MessageCode(string userDataCode, ENUM_TP_DCS encode)
        {
            _SMS_mode = SMS_Source.PDU_mode;
            TP_DCS = (byte)encode;
            TP_UDL = GsmEncoding.GetByte(ref userDataCode);
            TP_UD = GsmEncoding.GetString(ref userDataCode, TP_UDL * 2);
        }
        /// <summary>
        /// Độ dài tin nhắn (text length)
        /// </summary>
        public int TP_UDL;
        public string TP_UD;
        //public SMSType Type;
        public string UserData;

        
        
        
        
		
		
		private string alphabet;
		
        
        

        

        
        
        
        //==========================================
        #region Public Properties
        public string Alphabet
        {
            get { return alphabet; }
            set { alphabet = value; }
        }
        private int _index;
        public int Index
		{
			get { return _index;}
			set { _index = value;}
		}
        /// <summary>
        /// Message Reference
        /// </summary>
        private int TP_MR = 1;
        public int MessageReference
        {
            get { return TP_MR; }
            set { TP_MR = value; }
        }
        /// <summary>
        /// Số điện thoại mã hóa [Length][Format][SwapNum[F]]
        /// </summary>
        string TP_DA;
        private string phoneNumber;
        /// <summary>
        /// Số người gửi hoặc số gửi đi.
        /// </summary>
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                this.TP_DA = "";
                
                if (value.StartsWith("+"))
                {
                    this.phoneNumberType = 0x91;    // = 145// Quoc te
                }
                else
                {
                    this.phoneNumberType = 0x81;    // = 129
                }
                value = value.Replace("+", "");

                if (IsNumberType(value))
                {
                    this.phoneNumberLength = (byte)value.Length;
                    if ((value.Length % 2) == 1)
                    {
                        value = value + "F";
                    }
                    int length = value.Length;
                    for (int i = 0; i < length; i += 2)
                    {
                        string swap = "" + value[i + 1] + value[i];
                        this.TP_DA = this.TP_DA + swap;
                    }
                }
                else
                {
                    if (value.Length % 2 == 1)
                    {
                        this.phoneNumberType = 0xD1;//209
                    }
                    else this.phoneNumberType = 0xD0;//208

                    string encode7 = GsmEncoding.Encode7BitHex(value);
                    this.phoneNumberLength = (byte)encode7.Length;
                    
                    this.TP_DA = encode7;
                }

                this.TP_DA = phoneNumberLength.ToString("X2") + phoneNumberType.ToString("X2") + this.TP_DA;
            }
        }
        bool IsNumberType(string number)
        {
            foreach (char c in number)
            {
                if (!char.IsDigit(c)) return false;
            }
            return true;
        }
        public byte phoneNumberLength;
        public byte phoneNumberType;

        private Message_Type _messageType;
        public Message_Type MessageType
        {
            get { return _messageType; }
            set { _messageType = value; }
        }
        /// <summary>
        /// REC READ, REC UNREAD, STO SENT, STO UNSENT
        /// </summary>
        public string MessageTypeString
		{
			get {
                if (_messageType == Message_Type.REC_UNREAD)
                    return "REC UNREAD";
                else if (_messageType == Message_Type.REC_READ)
                    return "REC READ";
                else if (_messageType == Message_Type.STO_UNSENT)
                    return "STO UNSENT";
                else if (_messageType == Message_Type.STO_SENT)
                    return "STO SENT";
                else
                    return "UNKNOW";
            }
            set
            {
                value = value.ToUpper();
                if (value == "REC UNREAD")
                    _messageType = 0;
                else if (value == "REC READ")
                    _messageType = Message_Type.REC_READ;
                else if (value == "STO UNSENT")
                    _messageType = Message_Type.STO_UNSENT;
                else if (value == "STO SENT")
                    _messageType = Message_Type.STO_SENT;
                else
                    _messageType = Message_Type.UNKNOW;
            }
		}
        public bool StatusReportRequest
        {
            get { return TP_SRR == (1 << 5); }
            set
            {
                if (value)
                    TP_SRR = 1 << 5;
                else TP_SRR = 0;
            }
        }

        /// <summary>
        /// Ngày giờ
        /// </summary>
        private DateTime tp_scts;
        /// <summary>
        /// Ngày giờ
        /// </summary>
        public DateTime TP_SCTS
        {
            get { return this.tp_scts; }
            set
            {
                this.tp_scts = value;
            }
        }
        /// <summary>
        /// TP_SCTS
        /// </summary>
        public DateTime Date_time
        {
            get
            {
                return tp_scts;
            }
            set
            {
                this.tp_scts = value;
            }
        }
        /// <summary>
        /// yyMMddHHmmss00
        /// </summary>
        private string TP_SCTS_PDU
        {
            get
            {
                //yyMMddHHmmss
                string t = this.tp_scts.ToString("yyMMddHHmmss");
                return "" + t[1] + t[0] + t[3] + t[2] + t[5] + t[4]
                    + t[7] + t[6] + t[9] + t[8] + t[11] + t[10]
                    + "00";
            }
        }
        //private string date_time;
        //public string Date_time
        //{
        //    get { return date_time;}
        //    set
        //    {
        //        date_time = value;
        //        if (value == "") return;
        //        try
        //        {
        //            if(value.Contains("-"))
        //                value = value.Substring(0, value.IndexOf('-'));
        //            DateTime d = DateTime.ParseExact(value, "yyyy/M/d HH:mm:ss", null);
        //            this.TP_SCTS = d;
        //        }
        //        catch
        //        {
                    
        //        }
        //    }
        //}

        public SMS_Source _SMS_mode = SMS_Source.Text_mode;
        //bool isLongMessage = false;
        bool isPartMessage = false;
        /// <summary>
        /// Lưu nội dung tin nhắn dạng chuỗi tường minh.
        /// </summary>
        string message;
        /// <summary>
        /// Gán trực tiếp nội dung tin nhắn kể cả giá trị code.
        /// </summary>
        /// <param name="m"></param>
        public void SetMessage(string m)
        {
            _SMS_mode = SMS_Source.Text_mode;
            message = m;
        }
        /// <summary>
        /// Lấy hoặc gán nội dung tin nhắn. Khi gán có dạng code sẽ tự giải mã
        /// </summary>
		public string Message
		{
			get 
            {
                if (_SMS_mode == SMS_Source.Text_mode)
                {
                    return message;
                }
                else if (message != null)
                {
                    return message;
                }
                else
                {
                    message = "";
                    try
                    {
                        this.isPartMessage = GsmEncoding.IsPartMessageHexString(TP_UD);
                        switch (this.TP_DCS)
                        {
                            case (byte)ENUM_TP_DCS.DefaultAlphabet:
                            case (byte)ENUM_TP_DCS.Class0_UD_7bits:
                            case (byte)ENUM_TP_DCS.Class1_UD_7bits:
                            case (byte)ENUM_TP_DCS.Class2_UD_7bits:
                            case (byte)ENUM_TP_DCS.Class3_UD_7bits:
                                //if (GsmEncoding.IsUnicodeHexString(this.TP_UD))
                                //    result = GsmEncoding.Decode16bitHex(TP_UD);
                                //else if
                                if (isPartMessage)
                                    message = GsmEncoding.DecodePartLong7bitHexMessage(this.TP_UD);
                                else
                                    message = GsmEncoding.Decode7BitHex(this.TP_UD);
                                break;
                            case (byte)ENUM_TP_DCS.Class0_UD_8bits:
                            case (byte)ENUM_TP_DCS.Class1_UD_8bits:
                            case (byte)ENUM_TP_DCS.Class2_UD_8bits:
                            case (byte)ENUM_TP_DCS.Class3_UD_8bits:
                                if (isPartMessage)
                                    message = GsmEncoding.DecodePartLong8bitHexMessage(TP_UD);
                                else message = GsmEncoding.Decode8BitHex(this.TP_UD);
                                break;
                            case (byte)ENUM_TP_DCS.UCS2:
                                if (isPartMessage)
                                    message = GsmEncoding.DocodePartLong16bitHexMessage(this.TP_UD);
                                else message = GsmEncoding.Decode16BitHex(this.TP_UD);
                                break;
                            
                            default:
                                message = GsmEncoding.Decode7BitHex(this.TP_UD);
                                break;
                        }
                    }
                    catch
                    {
                        message = this.TP_UD;
                    }
                    //Chua co truong hop tin nhan dai
                    
                    return message;
                }
            }

            set
            {
                _SMS_mode = SMS_Source.Text_mode;
                //message = value;

                if (GsmEncoding.IsPartMessageHexString(value))
                {
                    this.TP_UD = value;
                    try
                    {
                        message = GsmEncoding.DecodePartLongHexMessage(value);
                    }
                    catch
                    {
                        try
                        {
                            message = GsmEncoding.Decode7BitHex(value);
                            this.TP_DCS = 0;
                        }
                        catch
                        {
                            message = value;
                        }

                    }
                    
                }
                else if (GsmEncoding.IsUnicodeHexString(value))
                {
                    this.TP_UD = value;
                    try
                    {
                        message = GsmEncoding.Decode16BitHex(value);
                        this.TP_DCS = (byte)ENUM_TP_DCS.UCS2;
                    }
                    catch
                    {
                        try
                        {
                            message = GsmEncoding.Decode7bitHex8bitFirstOne(value);
                        }
                        catch
                        {
                            message = value;
                        }
                    }
                }
                else if (GsmEncoding.Is8bitHexMessageHaveSpace(value))
                {
                    this.TP_UD = value;
                    message = GsmEncoding.Decode8BitHex(value);
                    this.TP_DCS = (byte)ENUM_TP_DCS.Class1_UD_8bits;
                }
                else if(GsmEncoding.IsUnicodeNotGSM7String(value))
                {
                    message = value;
                    this.TP_DCS = (byte)ENUM_TP_DCS.UCS2;
                }
                else if(GsmEncoding.IsNotGSM7bitString(value))
                {
                    message = value;
                    this.TP_DCS = (byte)ENUM_TP_DCS.Class1_UD_8bits;
                }
                else
                {
                    message = value;
                    this.TP_DCS = 0;
                }
            }
        }

        #region Storage
        private GSM.Storage storage;
        /// <summary>
        /// Nơi lưu trữ tin nhắn SM ME...
        /// </summary>
        public GSM.Storage Storage
        {
            get { return storage; }
            set { storage = value; }
        }
        #endregion storage

        #region Service Centre Number
        
        private string serviceCentreNumber;
        /// <summary>
        /// serviceCentreNumber??? số này sẵn sàng cho pdu.
        /// [Length][type][NumberSwapF] hoặc 00 : Skip
        /// </summary>
        private string SC_Number;

        public byte SCAddressLength;
        public byte SCAddressType;
        /// <summary>
        /// Số tổng đài tin nhắn
        /// </summary>
        public string ServiceCentreNumber
        {
            get { return serviceCentreNumber; }
            set
            {
                serviceCentreNumber = value;

                if ((value == "00") | (value.Length == 0))
                {
                    this.SC_Number = "00";
                }
                else
                {
                    if (value.Contains("+"))
                    {
                        this.SC_Number = "91";
                    }
                    else
                    {
                        this.SC_Number = "81";
                    }

                    value = value.Substring(1);
                    if ((value.Length % 2) == 1)
                    {
                        value = value + "F";
                    }
                    int length = value.Length;
                    for (int i = 0; i < length; i += 2)
                    {
                        string twoBitSwap = "" + value[i + 1] + value[i];// Strings.Mid(value, i, 2);
                        this.SC_Number = this.SC_Number + twoBitSwap;
                    }
                    this.SC_Number = ((byte)Math.Round((double)((((double)(this.SC_Number.Length - 2)) / 2.0) + 1.0))).ToString("X2") + this.SC_Number;
                }
            }
        }
        #endregion scnumber

        #endregion properties

        //==========================================
        #region ==== Function ====
        
        

        
        
        
        /// <summary>
        /// Giải mã chuỗi address, đảo ngược từng cặp , bỏ F ở cuối.
        /// </summary>
        /// <param name="code">có độ dài chẵn nha. lẽ lỗi ráng chịu</param>
        /// <returns></returns>
        public string GetAddress(string code)
        {
            string str2 = "";

            for (int i = 0; i < code.Length; i += 2)
            {
                string swap = "" + code[i + 1] + code[i];
                str2 = str2 + swap;
            }
            //Nếu có chữ F ở cuối thì bỏ đi.
            if (str2.EndsWith("F") || str2.EndsWith("f"))
            {
                str2 = str2.Substring(0, str2.Length - 1);
            }
            return str2;
        }
        bool IsTextNumberType(string codeSwap)
        {
            for (int i = 0; i < codeSwap.Length-1; i++)
            {
                if (!char.IsDigit(codeSwap[i])) return false;
            }
            return true;
        }
        public string GetPhoneNumber(string code)
        {
            string str2 = "";

            for (int i = 0; i < code.Length; i += 2)
            {
                string swap = "" + code[i + 1] + code[i];
                str2 = str2 + swap;
            }
            //Nếu có chữ F ở cuối thì bỏ đi.
            if (IsTextNumberType(str2))
            {
                if (str2.EndsWith("F") || str2.EndsWith("f"))
                {
                    str2 = str2.Substring(0, str2.Length - 1);
                }
                return str2;
            }
            else//TextNumberAddressMode
            {
                string text = GsmEncoding.Decode7BitHex(code);
                return text;
            }
        }

        /// <summary>
        /// Độ dài chuỗi PDU - SMSC
        /// </summary>
        /// <param name="pduString"></param>
        /// <returns></returns>
        public int GetATLength(string pduString)
        {
            return ((pduString.Length - (Convert.ToInt16(pduString.Substring(0, 2), 16) * 2)) - 2)
                / 2;
        }
        /// <summary>
        /// Lấy giá trị ngày tháng chuỗi PDU (những phần trước đã cắt bỏ, lấy 14Hex = 7Byte
        /// </summary>
        /// <param name="SCTS">Phải đủ 14 ký tự</param>
        /// <returns></returns>
        public DateTime GetDate(string SCTS)
        {
            string s = "";
            s = GsmEncoding.GetString(ref SCTS, 2);
            int year = int.Parse("" + s[1] + s[0]) + 2000;// (int)Math.Round((double)(Conversion.Val(Swap(ref s)) + 2000.0));
            s = GsmEncoding.GetString(ref SCTS, 2);
            int month = int.Parse("" + s[1] + s[0]);// (int)Math.Round(Conversion.Val(Swap(ref s2)));
            s = GsmEncoding.GetString(ref SCTS, 2);
            int day = int.Parse("" + s[1] + s[0]);
            s = GsmEncoding.GetString(ref SCTS, 2);
            int hour = int.Parse("" + s[1] + s[0]);// (int)Math.Round(Conversion.Val(Swap(ref s4)));
            s = GsmEncoding.GetString(ref SCTS, 2);
            int minute = int.Parse("" + s[1] + s[0]);// (int)Math.Round(Conversion.Val(Swap(ref s5)));
            s = GsmEncoding.GetString(ref SCTS, 2);
            int second = int.Parse("" + s[1] + s[0]);// (int)Math.Round(Conversion.Val(Swap(ref s6)));
            s = GsmEncoding.GetString(ref SCTS, 2);
            int num6 = Convert.ToInt32("" + s[1] + s[0], 16);
            //this.Date_time = string.Format("{0}/{1}/{2} {3}:{4}:{5}+{6}",
                //year, month, day, hour, minute, second, num6);
            return new DateTime(year, month, day, hour, minute, second);
        }



        public string[] GetPDUsend(out int[] lengths)
        {
            int part7bitLength = 153, part8bitLength = 134, part16bitLength = 66;
            string this_UD = this.TP_UD;
            //Tin nhan da giai ma.
            string thisMessage = Message;
            string referenUHD = GsmEncoding.GetRandomByte().ToString("X2");
            string currentUHD_UserHeaderData = "";
            int currentUDL_MessageLength = 0;
            string currentUD = "";
            string currentMessage = "";
            string currentPDU = "";
            int bitType = 0;
            int totalLength = 0;
            int totalMessage = 1;
            ENUM_TP_DCS codeType = (ENUM_TP_DCS)this.TP_DCS;
            switch (codeType)
            {
                case ENUM_TP_DCS.DefaultAlphabet:
                case ENUM_TP_DCS.Class0_UD_7bits:
                case ENUM_TP_DCS.Class1_UD_7bits:
                case ENUM_TP_DCS.Class2_UD_7bits:
                case ENUM_TP_DCS.Class3_UD_7bits:
                    bitType = 7;
                    //referenUHD = referenUHD.Substring(2);
                    if (_SMS_mode == SMS_Source.Text_mode)
                    {
                        this.TP_UDL = thisMessage.Length;
                        totalLength = thisMessage.Length;// ((TP_UD.Length * 4) / 7);
                    }
                    else
                    {
                        totalLength = ((TP_UD.Length * 4) / 7);
                        this.TP_UDL = totalLength;
                    }

                    if (totalLength > 160)
                    {
                        totalMessage = totalLength / part7bitLength;
                        if (totalLength % part7bitLength > 0) totalMessage++;
                    }
                    if (FlashMode) this.TP_DCS = (byte)ENUM_TP_DCS.Class0_UD_7bits;
                    break;
                case ENUM_TP_DCS.Class0_UD_8bits:
                case ENUM_TP_DCS.Class1_UD_8bits:
                case ENUM_TP_DCS.Class2_UD_8bits:
                case ENUM_TP_DCS.Class3_UD_8bits:
                    bitType = 8;
                    //referenUHD = referenUHD.Substring(2);
                    if (_SMS_mode == SMS_Source.Text_mode)
                    {
                        this.TP_UDL = thisMessage.Length;
                        totalLength = thisMessage.Length;// ((TP_UD.Length * 4) / 7);
                    }
                    else
                    {
                        totalLength = TP_UD.Length / 2;
                        this.TP_UDL = totalLength;
                    }

                    if (totalLength > 140)
                    {
                        totalMessage = totalLength / part8bitLength;
                        if (totalLength % part8bitLength > 0) totalMessage++;
                    }

                    if (FlashMode) this.TP_DCS = (byte)ENUM_TP_DCS.Class0_UD_8bits;
                    break;

                case ENUM_TP_DCS.UCS2:
                    bitType = 16;
                    if (_SMS_mode == SMS_Source.Text_mode)
                    {
                        this.TP_UDL = thisMessage.Length * 2;
                        totalLength = thisMessage.Length * 2;
                    }
                    else
                    {
                        totalLength = TP_UD.Length / 2;
                        this.TP_UDL = totalLength;
                    }
                    if (totalLength/2 > 70)
                    {
                        totalMessage = totalLength/2 / part16bitLength;
                        if ((totalLength/2) % part16bitLength > 0) totalMessage++;
                    }
                    //if (FlashMode) this.TP_DCS = (byte)ENUM_TP_DCS.Class0_UD_8bits;
                    break;
                default:
                    bitType = 7;
                    break;
            }

            string[] resultPDU = new string[totalMessage];
            lengths = new int[totalMessage];

            if (totalMessage == 1)
            {
                TP_UDHI_User_data_have_info = 0 << 6;
                if (_SMS_mode == SMS_Source.Text_mode)
                {
                    if (bitType == 7)
                        this.TP_UD = GsmEncoding.Encode7BitHex(thisMessage);
                    else if (bitType == 8)
                        this.TP_UD = GsmEncoding.Encode8BitHex(thisMessage);
                    else this.TP_UD = GsmEncoding.Encode16bitHex(thisMessage);
                }
                else
                {

                }

                currentPDU = ""
                    + this.SC_Number
                    + this.FirstOctet.ToString("X2")
                    + this.TP_MR.ToString("X2")
                    + this.TP_DA
                    + this.TP_PID.ToString("X2")
                    + this.TP_DCS.ToString("X2")
                    + this.TP_VP.ToString("X2")
                    + this.TP_UDL.ToString("X2")
                    + this.TP_UD;
                lengths[0] = GetATLength(currentPDU);
                resultPDU[0] = currentPDU;

            }
            else
            {
                TP_UDHI_User_data_have_info = 1 << 6;
                if(_SMS_mode == SMS_Source.Text_mode)
                {
                    if (bitType == 7)
                        this.TP_UD = GsmEncoding.Encode7BitHex(thisMessage);
                    else if (bitType == 8)
                        this.TP_UD = GsmEncoding.Encode8BitHex(thisMessage);
                    else this.TP_UD = GsmEncoding.Encode16bitHex(thisMessage);
                }
                this_UD = this.TP_UD;

                for (int i = 0; i < totalMessage; i++)
                {
                    //Cắt chuỗi UD, tạo phần đầu UHD
                    switch (bitType)
                    {
                        case 7:
                            currentUHD_UserHeaderData = "050003";
                            if(thisMessage.Length>part7bitLength)
                            {
                                currentMessage = thisMessage.Substring(0, part7bitLength);
                                currentUD = GsmEncoding.Encode7bitHex8First(currentMessage);
                                thisMessage = thisMessage.Substring(part7bitLength);
                            }
                            else
                            {
                                currentMessage = thisMessage;
                                currentUD = GsmEncoding.Encode7bitHex8First(currentMessage);
                                thisMessage = "";
                            }
                            currentUDL_MessageLength = currentMessage.Length + 7;//12 * 4 / 7;

                            //if ((totalUD.Length * 4) / 7 > part7bitLength)
                            //{
                            //    currentUD = totalUD.Substring(0, (part7bitLength * 7) / 4);
                            //    totalUD = totalUD.Substring((part7bitLength * 7) / 4);

                            //}
                            //else
                            //{
                            //    currentUD = totalUD;
                            //    totalUD = "";
                            //}
                            //currentUDL = ((currentUD.Length + 12) * 4) / 7;
                            //currentUDL = ((currentUD.Length+14)*4)/7;// / 2 + 5 + 1;//5 UDH 1 gì không biết
                            break;
                        case 8:
                            //currentUHD = "05" + this.TP_DCS.ToString("X2") + "03";
                            //currentUHD = "050003";
                            currentUHD_UserHeaderData = "050003";
                            if (this_UD.Length > part8bitLength * 2)
                            {
                                currentUD = this_UD.Substring(0, part8bitLength * 2);
                                this_UD = this_UD.Substring(part8bitLength * 2);
                            }
                            else
                            {
                                currentUD = this_UD;
                                this_UD = "";
                            }
                            currentUDL_MessageLength = currentUD.Length/2 + 6;
                            break;
                        case 16:
                            //currentUHD = "050804"; // Lúc này referenUHD sẽ có 16 bit (vd: FFFF), Lúc đó độ dài tin nhắn giảm 1.
                            currentUHD_UserHeaderData = "050003";
                            //TP_RP = 1 << 7;
                            if (this_UD.Length > part16bitLength * 4)
                            {
                                currentUD = this_UD.Substring(0, part16bitLength * 4);
                                this_UD = this_UD.Substring(part16bitLength * 4);
                            }
                            else
                            {
                                currentUD = this_UD;
                                this_UD = "";
                            }
                            currentUDL_MessageLength = ((currentUD.Length / 2) + 6);
                            break;
                        default:
                            break;
                    }
                    //Hoàn thành UHD
                    // 050803 cố định là sai? nó gồm UHD = 0x05 IEI = 0×00 và IEDL = 0×03
                    currentUHD_UserHeaderData = currentUHD_UserHeaderData + referenUHD + totalMessage.ToString("X2") + (i + 1).ToString("X2");
                    currentPDU = ""
                        + this.SC_Number
                        + this.FirstOctet.ToString("X2")
                        + this.TP_MR.ToString("X2")
                        + this.TP_DA
                        + this.TP_PID.ToString("X2")
                        + this.TP_DCS.ToString("X2")
                        + this.TP_VP.ToString("X2") //+ "A7"//OneDay
                        + currentUDL_MessageLength.ToString("X2")
                        + currentUHD_UserHeaderData
                        + currentUD;
                    lengths[i] = GetATLength(currentPDU);
                    resultPDU[i] = currentPDU;
                }
            }
            
            return resultPDU;
        }

        public string[] GetPDUwrite()
        {
            int part7bitLength = 153, part8bitLength = 134, part16bitLength = 66;
            string this_UD = this.TP_UD;
            //Tin nhan da giai ma.
            string thisMessage = Message;
            string referenUHD = GsmEncoding.GetRandomByte().ToString("X2");
            string currentUHD = "";
            int currentUDL = 0;
            string currentUD = "";
            string currentMessage = "";
            string currentPDU = "";
            int bitType = 0;
            int totalLength = 0;
            int totalMessage = 1;
            ENUM_TP_DCS codeType = (ENUM_TP_DCS)this.TP_DCS;
            switch (codeType)
            {
                case ENUM_TP_DCS.DefaultAlphabet:
                case ENUM_TP_DCS.Class0_UD_7bits:
                case ENUM_TP_DCS.Class1_UD_7bits:
                case ENUM_TP_DCS.Class2_UD_7bits:
                case ENUM_TP_DCS.Class3_UD_7bits:
                    bitType = 7;
                    //referenUHD = referenUHD.Substring(2);
                    if (_SMS_mode == SMS_Source.Text_mode)
                    {
                        this.TP_UDL = thisMessage.Length;
                        
                        totalLength = thisMessage.Length;// ((TP_UD.Length * 4) / 7);
                    }
                    else
                    {
                        totalLength = ((TP_UD.Length * 4) / 7);
                        this.TP_UDL = totalLength;
                    }

                    if (totalLength > 160)
                    {
                        totalMessage = totalLength / part7bitLength;
                        if (totalLength % part7bitLength > 0) totalMessage++;
                    }
                    if (FlashMode) this.TP_DCS = (byte)ENUM_TP_DCS.Class0_UD_7bits;
                    break;
                case ENUM_TP_DCS.Class0_UD_8bits:
                case ENUM_TP_DCS.Class1_UD_8bits:
                case ENUM_TP_DCS.Class2_UD_8bits:
                case ENUM_TP_DCS.Class3_UD_8bits:
                    bitType = 8;
                    //referenUHD = referenUHD.Substring(2);
                    if (_SMS_mode == SMS_Source.Text_mode)
                    {
                        this.TP_UDL = thisMessage.Length;
                        totalLength = thisMessage.Length;// ((TP_UD.Length * 4) / 7);
                    }
                    else
                    {
                        totalLength = TP_UD.Length / 2;
                        this.TP_UDL = totalLength;
                    }

                    if (totalLength > 140)
                    {
                        totalMessage = totalLength / part8bitLength;
                        if (totalLength % part8bitLength > 0) totalMessage++;
                    }

                    if (FlashMode) this.TP_DCS = (byte)ENUM_TP_DCS.Class0_UD_8bits;
                    break;

                case ENUM_TP_DCS.UCS2:
                    bitType = 16;
                    if (_SMS_mode == SMS_Source.Text_mode)
                    {
                        this.TP_UDL = thisMessage.Length * 2;
                        totalLength = thisMessage.Length * 2;
                    }
                    else
                    {
                        totalLength = TP_UD.Length / 2;
                        this.TP_UDL = totalLength;
                    }
                    if (totalLength / 2 > 70)
                    {
                        totalMessage = totalLength / 2 / part16bitLength;
                        if ((totalLength / 2) % part16bitLength > 0) totalMessage++;
                    }
                    //if (FlashMode) this.TP_DCS = (byte)ENUM_TP_DCS.Class0_UD_8bits;
                    break;
                default:
                    bitType = 7;
                    break;
            }

            string[] resultPDU = new string[totalMessage];

            if (totalMessage == 1)
            {
                if (_SMS_mode == SMS_Source.Text_mode)
                {
                    if (bitType == 7)
                        this.TP_UD = GsmEncoding.Encode7BitHex(thisMessage);
                    else if (bitType == 8)
                        this.TP_UD = GsmEncoding.Encode8BitHex(thisMessage);
                    else this.TP_UD = GsmEncoding.Encode16bitHex(thisMessage);
                }
                else
                {

                }
                ////Lay 1 byte 8bits = 2Hex
                //SCAddressLength = GsmEncoding.GetByte(ref PDUreceive);// (byte)Convert.ToSByte(PDUCode.Substring(0, 2), 16);
                //SCAddressType = GsmEncoding.GetByte(ref PDUreceive);// (byte)Convert.ToSByte(PDUCode.Substring(0, 2), 16);

                ////Lấy chuỗi HEX, - 1 vì đâu?
                //if (SCAddressLength > 0)
                //{
                //    int getLength = (SCAddressLength - 1) * 2;
                //    string address = PDUreceive.Substring(0, getLength);
                //    //Giải mã chuỗi address, đảo ngược từng cặp , bỏ F ở cuối.
                //    SCAddressValue = GetAddress(address);
                //}

                //FirstOctet = GsmEncoding.GetByte(ref PDUreceive);// (byte)Convert.ToSByte(PDUCode.Substring(0, 2), 16);
                //this.phoneNumberLength = GsmEncoding.GetByte(ref PDUreceive);//(byte)Convert.ToSByte(PDUCode.Substring(0, 2), 16);
                //this.phoneNumberType = GsmEncoding.GetByte(ref PDUreceive);// (byte)Convert.ToSByte(PDUCode.Substring(0, 2), 16);

                ////Độ dài số điện thoại, Nếu lẻ thì + thêm 1 cho chữ F
                //this.phoneNumberLength = (byte)(this.phoneNumberLength + (this.phoneNumberLength % 2));
                //string sdtN = GetString(ref PDUreceive, this.phoneNumberLength);
                //this.PhoneNumber = GetAddress(sdtN);

                //TP_PID = GsmEncoding.GetByte(ref PDUreceive);// (byte)Convert.ToSByte(PDUCode.Substring(0, 2), 16); PDUCode = PDUCode.Substring(2);
                //TP_DCS = GsmEncoding.GetByte(ref PDUreceive);// (byte)Convert.ToSByte(PDUCode.Substring(0, 2), 16); PDUCode = PDUCode.Substring(2);
                //string hexDateString = PDUreceive.Substring(0, 14); PDUreceive = PDUreceive.Substring(14);
                //TP_SCTS = GetDate(hexDateString);
                //TP_UDL = GsmEncoding.GetByte(ref PDUreceive);// SMSBase.GetByte(ref PDUCode);
                //TP_UD = GetString(ref PDUreceive, TP_UDL * 2);
                currentPDU = ""
                    + this.SC_Number
                    + this.FirstOctet.ToString("X2")
                    //+ this.TP_MR.ToString("X2")       //bỏ
                    + this.TP_DA
                    + this.TP_PID.ToString("X2")
                    + this.TP_DCS.ToString("X2")
                    //+ this.TP_VP.ToString("X2")       //bỏ
                    + this.TP_SCTS_PDU                  //thêm
                    + this.TP_UDL.ToString("X2")
                    + this.TP_UD;
                
                //lengths[0] = GetATLength(currentPDU);
                resultPDU[0] = currentPDU;

            }
            else
            {
                for (int i = 0; i < totalMessage; i++)
                {
                    //Cắt chuỗi UD, tạo phần đầu UHD
                    switch (bitType)
                    {
                        case 7:
                            currentUHD = "050003";
                            if (thisMessage.Length > part7bitLength)
                            {
                                currentMessage = thisMessage.Substring(0, part7bitLength);
                                currentUD = GsmEncoding.Encode7bitHex8First(currentMessage);
                                thisMessage = thisMessage.Substring(part7bitLength);
                            }
                            else
                            {
                                currentMessage = thisMessage;
                                currentUD = GsmEncoding.Encode7bitHex8First(currentMessage);
                                thisMessage = "";
                            }
                            currentUDL = currentMessage.Length + 7;//12 * 4 / 7;

                            //if ((totalUD.Length * 4) / 7 > part7bitLength)
                            //{
                            //    currentUD = totalUD.Substring(0, (part7bitLength * 7) / 4);
                            //    totalUD = totalUD.Substring((part7bitLength * 7) / 4);

                            //}
                            //else
                            //{
                            //    currentUD = totalUD;
                            //    totalUD = "";
                            //}
                            //currentUDL = ((currentUD.Length + 12) * 4) / 7;
                            //currentUDL = ((currentUD.Length+14)*4)/7;// / 2 + 5 + 1;//5 UDH 1 gì không biết
                            break;
                        case 8:
                            //currentUHD = "05" + this.TP_DCS.ToString("X2") + "03";
                            currentUHD = "050003";
                            if (this_UD.Length > part8bitLength * 2)
                            {
                                currentUD = this_UD.Substring(0, part8bitLength * 2);
                                this_UD = this_UD.Substring(part8bitLength * 2);
                            }
                            else
                            {
                                currentUD = this_UD;
                                this_UD = "";
                            }
                            currentUDL = currentUD.Length / 2 + 6;
                            break;
                        case 16:
                            currentUHD = "050003";
                            if (this_UD.Length > part16bitLength * 4)
                            {
                                currentUD = this_UD.Substring(0, part16bitLength * 4);
                                this_UD = this_UD.Substring(part16bitLength * 4);
                            }
                            else
                            {
                                currentUD = this_UD;
                                this_UD = "";
                            }
                            currentUDL = ((currentUD.Length / 2) + 6);
                            break;
                        default:
                            break;
                    }
                    //Hoàn thành UHD
                    currentUHD = currentUHD + referenUHD + totalMessage.ToString("X2") + (i + 1).ToString("X2");
                    currentPDU = ""
                        + this.SC_Number
                        + this.FirstOctet.ToString("X2")
                        //+ this.TP_MR.ToString("X2")       //bỏ
                        + this.TP_DA
                        + this.TP_PID.ToString("X2")
                        + this.TP_DCS.ToString("X2")
                        //+ this.TP_VP.ToString("X2")       //bỏ
                        + this.TP_SCTS_PDU                  //thêm
                        + currentUDL.ToString("X2")
                        + currentUHD + currentUD;
                    //currentPDU = ""
                    //    + this.SC_Number
                    //    + this.FirstOctet.ToString("X2")
                    //    + this.TP_MR.ToString("X2")
                    //    + this.TP_DA
                    //    + this.TP_PID.ToString("X2")
                    //    + this.TP_DCS.ToString("X2")
                    //    + this.TP_VP.ToString("X2")
                    //    + currentUDL.ToString("X2")
                    //    + currentUHD
                    //    + currentUD;
                    
                    //lengths[i] = GetATLength(currentPDU);
                    resultPDU[i] = currentPDU;
                }
            }

            return resultPDU;
        }
        #endregion function


        
    }

    public class ShortMessageCollection : List<ShortMessage>
    {
    }
}
