using GSM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace GSM.DATA
{
    /* *
     * CẤU TRÚC TẬP TIN
     * === 200 BYTES HEADER ===
     * First byte: 1:inbox, 2:outbox
     * Not use
     * === CONTINUEUS 200 BYTES === start from index 1
     * --- 10 Bytes sms header ---
     * 0: length of pdu in byte, if 0 is no use
     * 1: type of sms, 0 REC UNREAD, 1 REC READ, 2 STO UNSENT, 3 STO SENT, FF UNKNOW
     * ...Not use
     * --- 190 Bytes sms pdu --- if pdu length>190 byte => error
     * Length of pdu: pdu
     * waste and null
     * pdu: max 140 bytes for text, 1 length 1 for encode (DSC),
     * ~10 to 16 for smsc, 12~ to 20 for phone number,
     * 7 for datetime, earch one for firstOctet, PID
     * */
    public class SmsData
    {
        /**
         * 1 sms have ~ 200 bytes data => 10 rows is 2000 bytes,
         * 100 rows is 20kB, 1000 rows is 200kB, 10,000 rows is ~2MB
         * use 200 bytes data to store header
         * next 200 bytes is index 1...
         * 
         * */
        string _name = "SmsData";
        string _file = "SmsData.dat";
        bool _fresh = false;
        public bool Fresh{get{return _fresh;}}
        FileStream fs;
        BinaryReader br;
        //StreamWriter sw;
        const int rowSize = 200;
        /// <summary>
        /// Tạm lưu để load nhanh.
        /// </summary>
        Dictionary<int, ShortMessage> smsDic;
        public ShortMessageCollection MessageList
        {
            get
            {
                //_fresh = false;//chua hoan thanh.
                if(_fresh)
                {
                    ShortMessageCollection smsColection = new ShortMessageCollection();
                    foreach (var item in smsDic)
                    {
                        smsColection.Add(item.Value);
                    }
                    return smsColection;
                }
                else
                {
                    return ReadSmsDic();
                }
            }
        }

        /// <summary>
        /// Đọc hết danh sách tin nhắn
        /// </summary>
        public ShortMessageCollection ReadMessageList
        {
            get
            {
                if (!_fresh) ReadSmsDic();
                {
                    ShortMessageCollection smsColection = new ShortMessageCollection();
                    foreach (var item in smsDic)
                    {
                        if(item.Value.MessageType == Message_Type.REC_READ)
                            smsColection.Add(item.Value);
                    }
                    return smsColection;
                }
                
            }
        }

        public ShortMessageCollection UnreadMessageList
        {
            get
            {
                if (!_fresh) ReadSmsDic();

                {
                    ShortMessageCollection smsColection = new ShortMessageCollection();
                    foreach (var item in smsDic)
                    {
                        if (item.Value.MessageType != Message_Type.REC_READ)
                            smsColection.Add(item.Value);
                    }
                    return smsColection;
                }
            }
        }

        public ShortMessageCollection UnsendMessageList
        {
            get
            {
                if (!_fresh) ReadSmsDic();

                {
                    ShortMessageCollection smsColection = new ShortMessageCollection();
                    foreach (var item in smsDic)
                    {
                        if (item.Value.MessageType == Message_Type.STO_UNSENT)
                            smsColection.Add(item.Value);
                    }
                    return smsColection;
                }
            }
        }

        public ShortMessageCollection SendMessageList
        {
            get
            {
                if (!_fresh) ReadSmsDic();

                {
                    ShortMessageCollection smsColection = new ShortMessageCollection();
                    foreach (var item in smsDic)
                    {
                        if (item.Value.MessageType != Message_Type.STO_UNSENT)
                            smsColection.Add(item.Value);
                    }
                    return smsColection;
                }
            }
        }

        SmsDataHeader _header;
        public SmsData()
        {
            smsDic = new Dictionary<int, ShortMessage>();
            _header.Type = 0;
            WriteHeader();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="file"></param>
        /// <param name="type">1 inbox, 2 outbox</param>
        public SmsData(string name, string file, byte type)
        {
            _name = name; _file = file;
            smsDic = new Dictionary<int, ShortMessage>();
            _header.Type = type;
            //if(!File.Exists(file))
                WriteHeader();
        }

        public bool DeleteSmsAt(int index)
        {
            _fresh = false;
            try
            {
                fs = new FileStream(_file, FileMode.OpenOrCreate);
                int maxIndex = (int)(fs.Length / rowSize);
                if (index > maxIndex) return false;

                fs.Position = (long)index * rowSize;
                fs.WriteByte(0);

                if (smsDic.ContainsKey(index)) smsDic.Remove(index);
                
                fs.Close();
                return true;
            }
            catch// (Exception ex)
            {
                fs.Close();
                return false;
            }
        }

        int FindFirstEmptyIndex()
        {
            try
            {
                //**1**2**3**4* => 9/2 = 4; if 8/2=4 no +, but 9 ++ !!
                fs = new FileStream(_file, FileMode.OpenOrCreate);
                int newMaxIndex = (int)(fs.Length/rowSize);
                if (newMaxIndex == 0) newMaxIndex = 1;
                if (fs.Length * rowSize > 0) newMaxIndex++;
                for (int i = 1; i<newMaxIndex; i++)
                {
                    fs.Position = (long)i * rowSize;
                    if (fs.ReadByte() == 0)//Nouse index
                    {
                        fs.Close();
                        return i;
                    }
                }
                fs.Close();
                return newMaxIndex;//Max index is new index
            }
            catch// (Exception ex)
            {
                fs.Close();
                throw;
            }
        }

        public ShortMessage ReadSmsAt(int index)
        {
            try
            {
                //if (smsDic.ContainsKey(index)) return smsDic[index];//Can hoan lai

                fs = new FileStream(_file, FileMode.OpenOrCreate);
                br = new BinaryReader(fs);
                ShortMessage result = ReadSmsAt(fs, index);
                br.Close();
                fs.Close();
                return result;
            }
            catch (Exception)
            {
                br.Close();
                fs.Close();
                throw;
            }
        }

        /// <summary>
        /// Đọc một tin nhắn ở vị trí xác định
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="index">1 là đâu tiên</param>
        /// <returns></returns>
        public ShortMessage ReadSmsAt(FileStream fs, int index)
        {
            try
            {
                fs.Position = (long)index * rowSize;
                int pduLength = fs.ReadByte();
                int messageType = fs.ReadByte();
                Message_Type message_Type = Message_Type.UNKNOW;
                switch (messageType)
                {
                    case 0: message_Type = Message_Type.REC_UNREAD; break;
                    case 1: message_Type = Message_Type.REC_READ; break;
                    case 2: message_Type = Message_Type.STO_UNSENT; break;
                    case 3: message_Type = Message_Type.STO_SENT; break;
                    default: message_Type = Message_Type.UNKNOW; break;
                }
                int length = 0;
                if (pduLength == 0)
                {
                    return null;
                }
                else
                {
                    
                    fs.Position = (long)index * rowSize + 10;
                    byte[] bytes = br.ReadBytes(pduLength);
                    //br.Close();
                    string pduString = ByteArrayToHexString(bytes);
                    ShortMessage sms = new ShortMessage
                        (index, message_Type, length, pduString, 1);

                    if (_header.Type == 1)
                        sms.Storage = GSM.Storage.COM1;
                    else if (_header.Type == 2)
                        sms.Storage = GSM.Storage.COM2;
                    else
                        sms.Storage = GSM.Storage.COM;
                    
                    //sms.Index = index.ToString();
                    //string test = sms.Message;
                    smsDic[index] = sms;
                    return sms;
                }
            }
            catch// (Exception)
            {
                return null;
            }
        }
        
        /// <summary>
        /// Đọc hết
        /// </summary>
        /// <returns></returns>
        public ShortMessageCollection ReadSmsDic()
        {
            try
            {
                fs = new FileStream(_file, FileMode.OpenOrCreate);
                br = new BinaryReader(fs);
                smsDic = new Dictionary<int, ShortMessage>();
                ShortMessageCollection smsColection = new ShortMessageCollection();
                int maxIndex = (int)(fs.Length / rowSize);
                for (int i = 1; i <= maxIndex; i++)
                {
                    ShortMessage sms = ReadSmsAt(fs, i);
                    if(sms!=null)
                    {
                        smsColection.Add(sms);
                    }
                }
                fs.Close();
                _fresh = true;
                return smsColection;
            }
            catch (Exception ex)
            {
                throw new Exception("ReadSmsDic " + ex.Message);
            }
        }


        /// <summary>
        /// Ghi xuống một sms vào vị trí trống nhỏ nhất.
        /// </summary>
        /// <param name="sms"></param>
        /// <returns>Trả về vị trí được ghi.</returns>
        public int WriteSms(ShortMessage sms)
        {
            _fresh = false;
            int index = FindFirstEmptyIndex();
            WriteSmsAt(index, sms);
            return index;
        }

        /// <summary>
        /// Ghi xuống file tất cả tin nhắn trong dic, cần phát triển lưu ra file khác, hiện tại chưa xài tới.
        /// </summary>
        public void WriteAllSms()
        {
            _fresh = false;
            try
            {
                fs = new FileStream(_file, FileMode.OpenOrCreate);
                foreach (var item in smsDic)
                {
                    WriteSmsAt(fs, item.Key, item.Value);
                }
                fs.Close();
            }
            catch (Exception ex)
            {
                fs.Close();
                throw new Exception("WriteAllSms " + ex.Message);
            }
        }
        
        /// <summary>
        /// Ghi xuống một tin nhắn ở vị trí xác định, ghi đè nếu đã có.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="sms"></param>
        public void WriteSmsAt(int index, ShortMessage sms)
        {
            //_fresh = false;
            try
            {
                smsDic[index] = sms;
                fs = new FileStream(_file, FileMode.OpenOrCreate);
                WriteSmsAt(fs, index, sms);
                fs.Close();
            }
            catch { fs.Close(); }
        }

        /// <summary>
        /// Ghi xuống một tin nhắn ở vị trí xác định, ghi đè nếu đã có.
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="index"></param>
        /// <param name="sms"></param>
        private void WriteSmsAt(FileStream fs, int index, ShortMessage sms)
        {
            _fresh = false;
            fs.Position = (long)index * rowSize;
            //int[] temp;
            string[] pdu = sms.GetPDUwrite();
            string writepdu = pdu[0];
            
            byte[] bytes = ConvertHexStringToByteArray(writepdu);
            byte pduLength = (byte)bytes.Length;
            //Đánh dấu sự có mặt của sms = độ dài pdu (bắt đầu pdu sau 10byte)
            fs.WriteByte(pduLength);
            //Index mặc nhiên là vị trí, khỏi phải ghi.
            //Loại unread, read, unsent, sent
            fs.WriteByte((byte)sms.MessageType);

            fs.Position = (long)index * rowSize + 10;
            fs.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Cắt bỏ bớt file dữ liệu từ vị trí truyền vào.
        /// </summary>
        /// <param name="index">Nhỏ nhất là 1</param>
        public void SetEOF(int index)
        {
            _fresh = false;
            if (index < 1) index = 1;
            try
            {
                fs = new FileStream(_file, FileMode.OpenOrCreate);
                fs.SetLength((long)index * rowSize);
                fs.Close();
            }
            catch// (Exception ex)
            {
                fs.Close();
            }
        }
        
        /// <summary>
        /// Cập nhập thông tin header xuống file
        /// </summary>
        public void WriteHeader()
        {
            try
            {
                fs = new FileStream(_file, FileMode.OpenOrCreate);
                fs.WriteByte(_header.Type);
                fs.Close();
            }
            catch// (Exception ex)
            {
                fs.Close();
            }
        }

        /// <summary>
        /// Đọc thông tin header.
        /// </summary>
        private void ReadHeader()
        {
            _header = new SmsDataHeader();
            try
            {
                fs = new FileStream(_file, FileMode.OpenOrCreate);
                _header.Type = (byte)fs.ReadByte();
                fs.Close();
            }
            catch
            {
                fs.Close();
            }
        }
        
        /// <summary>
        /// Hàm hỗ trợ chuyển AABB1A1BFF thành mảng số 255
        /// </summary>
        /// <param name="hexString">AABB1A1BFF</param>
        /// <returns>mảng số</returns>
        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            try
            {
                if (hexString.Length % 2 != 0)
                {
                    throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
                }

                byte[] HexAsBytes = new byte[hexString.Length / 2];
                for (int index = 0; index < HexAsBytes.Length; index++)
                {
                    string byteValue = hexString.Substring(index * 2, 2);
                    HexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                }

                return HexAsBytes;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Hàm hỗ trợ chuyển mảng số thành chuỗi HEX
        /// </summary>
        /// <param name="bytes">mảng số</param>
        /// <returns>AABB1A1BFF</returns>
        public static string ByteArrayToHexString(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        
        
        byte[] IntToBytes(int value)
        {
            byte[] intBytes = BitConverter.GetBytes(value);
            Array.Reverse(intBytes);
            byte[] result = intBytes;
            return result;
        }
        int BytesToInt(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            int i = BitConverter.ToInt32(bytes, 0);
            return i;
        }

        /// <summary>
        /// Chuyển chuỗi chữ thành mảng số.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// Chuyển lại mảng số thành chuỗi chữ.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        byte[] StringToBytes(string myString)
        {
            byte[] b2 = System.Text.Encoding.ASCII.GetBytes(myString);
            return b2;
        }
        string BytesToString(byte[] bytes)
        {
            string s = System.Text.Encoding.ASCII.GetString(bytes);
            return s;
        }


        /// <summary>
        /// Ví dụ chuyển một tin chưa đọc thành tin đã đọc.
        /// </summary>
        /// <param name="index">Vị trí tin nhắn được lưu</param>
        /// <param name="message_Type">REC_UNREAD = 0, REC_READ = 1, STO_UNSENT = 2, STO_SENT = 3,</param>
        public void SetMessageType(int index, Message_Type message_Type)
        {
            fs = new FileStream(_file, FileMode.OpenOrCreate);
            fs.Position = ((long)index * rowSize) + 1;
            fs.WriteByte((byte)message_Type);
            fs.Close();
        }
    }

    public struct SmsDataHeader
    {
        /// <summary>
        /// 1 inbox(REC), 2 outbox(STO)
        /// </summary>
        public byte Type;
        public string MessageType
        {
            get
            {
                string s = "";
                switch (Type)
                {
                    case 1:
                        return "REC";
                    case 2:
                        return "STO";
                    default:
                        s = "";
                        break;
                }
                return s;
            }
        }
    }
}
