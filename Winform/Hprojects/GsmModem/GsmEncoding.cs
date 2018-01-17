using System;
using System.Collections.Generic;
using System.Text;

namespace GSM
{
    public class GsmEncoding
    {
        public static int GetGSM7Length(string gsm7string)
        {
            int leng = gsm7string.Length;
            int more = 0;
            foreach (char c in gsm7string)
            {
                if (_GSM7Char2byte.IndexOf(c) > -1) more++;
            }
            return leng + more;
        }
        public static string[] GetPDUs()
        {
            return null;
        }
        private static Random r = new Random();
        public static int GetRandom(int from, int to)
        {
            return r.Next(from, to);
        }
        /// <summary>
        /// Random từ 1 đến 255
        /// </summary>
        /// <returns></returns>
        public static int GetRandomByte()
        {
            return r.Next(1, 255);
        }
        public static int GetRandomByte2()
        {
            return r.Next(1, 0xFFFF);
        }
        
        public static bool IsHexString(string value)
        {
            //Phải chẳn 2
            if (value.Length % 2 != 0) return false;
            value = value.ToUpper();
            foreach (char c in value)
            {
                if (!((c >= 0 && c <= 9) || (c >= 'A' && c <= 'F'))) return false;
            }
            return true;
        }
        /// <summary>
        /// Kiểm tra chuỗi Hex có phải là một phần của tin nhắn dài hay không?
        /// Hàm này có tính tương đối số đông.
        /// </summary>
        /// <param name="value">050X03 XXCCII AAAAAAAA or 060X04 XXXXCCII AAAAAAAA</param>
        /// <returns></returns>
        public static bool IsPartMessageHexString(string value)
        {
            if (value.Length < 12) return false;
            //000000 00001111
            //012345 67890123
            //050X03 XXCCII
            //060X04 XXXXCCII
            if (value[0] != '0' || value[2] != '0' || value[4] != '0') return false;
            int total = 2, index = 1;
            if (value.Substring(0, 2) == "05")
            {
                total = Convert.ToInt16(value.Substring(8, 2), 16);
                index = Convert.ToInt16(value.Substring(10, 2), 16);
                if (index > total) return false;
            }
            if (value.Substring(0, 2) == "06")
            {
                total = Convert.ToInt16(value.Substring(10, 2), 16);
                index = Convert.ToInt16(value.Substring(12, 2), 16);
                if (index > total) return false;
            }
            return true;
        }
        /// <summary>
        /// Tính chính xác khoảng 90%
        /// </summary>
        /// <param name="value">Phải đảm bảo là chuỗi Hex, đưa ký tự unicode vào vẫn không báo lỗi!</param>
        /// <returns></returns>
        public static bool IsUnicodeHexString(string value)
        {
            if (value.Length % 4 != 0) return false;
            int count = 0;
            int char16leng = value.Length/4;
            for (int i = 0; i < value.Length; i+=4)
            {
                if (value.Substring(i, 2) == "00")
                    count++;
            }
            if (count > (char16leng - count)) return true;
            return false;
        }
        /// <summary>
        /// Phải kiểm tra IsUnicode trước, nếu sai kiểm tiếp cái này.
        /// Trong chuỗi giải mã thường sẽ có khoảng trắng!
        /// Khả thi 70%
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Is8bitHexMessageHaveSpace(string value)
        {
            string decode = "";
            if (value.Length % 2 != 0) return false;
            try
            {
                decode = Decode8BitHex(value);
                if (!decode.Contains(" ")) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// >8bit string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUnicodeNotGSM7String(string value)
        {
            foreach (char c in value)
            {
                if (_GSM7CharMap_Full.IndexOf(c) == -1)
                {
                    if (c > 0xFF) return true;
                }
            }
            return false;
            //ASCIIEncoding a = new ASCIIEncoding();
            //UnicodeEncoding u = new UnicodeEncoding();
            //byte[] ascii = a.GetBytes(value);
            //byte[] unicode = u.GetBytes(value);
            //string value1 = a.GetString(ascii);
            //string value2 = u.GetString(unicode);
            //return (value1 != value2);
        }
        public static bool IsGSM7bitString(string value)
        {
            foreach (char c in value)
            {
                if (_GSM7CharMap_Full.IndexOf(c) == -1) return false;
            }
            return true;
        }
        public static bool IsNotGSM7bitString(string value)
        {
            foreach (char c in value)
            {
                if (_GSM7CharMap_Full.IndexOf(c) == -1) return true;
                //if (c > 127) return true;
            }
            return false;
        }

        public static string DecodePartLongHexMessage(string partHex)
        {
            byte UDHlength = GetByte(ref partHex);// Convert.ToInt16(partHex.Substring(0, 2));
            byte encodeType = GetByte(ref partHex);
            byte HDlength = GetByte(ref partHex);
            int id=0, total=0, index=0;
            if(HDlength==3)
            {
                id = GetByte(ref partHex);
                total = GetByte(ref partHex);
                index = GetByte(ref partHex);
            }
            else if(HDlength == 4)
            {
                id = GetByte(ref partHex);
                id = id << 8;
                id = id + GetByte(ref partHex);
                total = GetByte(ref partHex);
                index = GetByte(ref partHex);
            }
            string text = "";

            ENUM_TP_DCS a = (ENUM_TP_DCS)encodeType;
            switch (a)
            {
                case ENUM_TP_DCS.Class0_UD_7bits:
                case ENUM_TP_DCS.Class1_UD_7bits:
                case ENUM_TP_DCS.Class2_UD_7bits:
                case ENUM_TP_DCS.Class3_UD_7bits:
                    if (IsUnicodeHexString(partHex))
                        text = Decode16BitHex(partHex);
                    else
                    text = Decode7BitHex(partHex);
                    break;
                case ENUM_TP_DCS.Class0_UD_8bits:
                case ENUM_TP_DCS.Class1_UD_8bits:
                case ENUM_TP_DCS.Class2_UD_8bits:
                case ENUM_TP_DCS.Class3_UD_8bits:
                    text = Decode8BitHex(partHex);
                    break;
                
                case ENUM_TP_DCS.DefaultAlphabet:
                    try
                    {
                        if (IsUnicodeHexString(partHex))
                            text = Decode16BitHex(partHex);
                        else
                        text = Decode7bitHex8bitFirstOne(partHex);
                    }
                    catch
                    {
                        text = Decode8BitHex(partHex);
                    }
                    break;

                case ENUM_TP_DCS.UCS2:
                    try
                    {
                        text = Decode16BitHex(partHex);
                    }
                    catch
                    {
                        try
                        {
                            text = Decode8BitHex(partHex);
                        }
                        catch
                        {
                            text = Decode7BitHex(partHex);
                        }
                        
                    }
                    break;
                default:
                    text = partHex;
                    break;
            }
            
            return string.Format("{0}{1}{2}:", id, total, index) + text;
        }

        public static string DecodePartLong7bitHexMessage(string part7bitHex)
        {
            //012345.ID.TT.IN.87MG
            string rs = "";
            int id = Convert.ToInt32(part7bitHex.Substring(6, 2), 16);
            int total = Convert.ToInt16(part7bitHex.Substring(8, 2), 16);
            int index = Convert.ToInt16(part7bitHex.Substring(10, 2), 16);
            if (index > total)
                throw new Exception("Có gì đó không ổn!");
            rs = Decode7bitHex8bitFirstOne(part7bitHex.Substring(12));
            return "" + id + total + index + ":" + rs;
        }

       
        

        public static string DecodePartLong8bitHexMessage(string partHex)
        {
            string rs = "";
            int id = Convert.ToInt32(partHex.Substring(6, 2), 16);
            int total = Convert.ToInt16(partHex.Substring(8, 2), 16);
            int index = Convert.ToInt16(partHex.Substring(10, 2), 16);
            rs += Decode8BitHex(partHex.Substring(12));
            return "" + id + total + index + ":" + rs;
        }

        public static string DocodePartLong16bitHexMessage(string partHex)
        {
            string rs = "";
            int id = Convert.ToInt32(partHex.Substring(6, 2), 16);
            int total = Convert.ToInt16(partHex.Substring(8, 2), 16);
            int index = Convert.ToInt16(partHex.Substring(10, 2), 16);
            rs += Decode16BitHex(partHex.Substring(12));
            return "" + id + total + index + ":" + rs;
        }

        public static string Decode8BitHex(string hex)
        {
            string rs = "";
            //while (hex.Length % 2 != 0)
            //{
            //    hex += "0";
            //}
            for (int i = 0; i < hex.Length; i += 2)
            {
                string s = hex.Substring(i, 2);
                rs += Convert.ToChar(Convert.ToInt16(s, 16));
            }
            return rs;
        }

        public static string Decode16BitHex(string hex)
        {
            string rs = "";
            //while (hex.Length % 4 != 0)
            //{
            //    hex += "0";
            //}
            for (int i = 0; i < hex.Length; i += 4)
            {
                string s = hex.Substring(i, 4);
                try
                {
                    rs += Convert.ToChar(Convert.ToInt16(s, 16));
                }
                catch
                {
                    rs += "▒";
                }
            }
            return rs;
        }

        /// <summary>
        /// Tính PDU length
        /// </summary>
        /// <param name="pduString"></param>
        /// <returns></returns>
        public int GetATLength(string pduString)
        {
            return ((pduString.Length - (Convert.ToInt16(pduString.Substring(0, 2), 16) * 2)) - 2)
                / 2;
        }

        /// <summary>
        /// Đưa bin7 về đúng trật tự ngược 7bit
        /// </summary>
        /// <param name="bincode7"></param>
        /// <returns></returns>
        private static string Bincode7toBinde7(string bincode7)
        {
            //21111111.33222222.44433333.55554444.00000555

            //Fix
            //lần lượt đưa 8 bit vào stack
            string stack = "";
            
            for (int i = 0; i < bincode7.Length; i += 8)
            {
                try
                {
                    stack = bincode7.Substring(i, 8) + stack;
                }
                catch
                {
                    stack = bincode7.Substring(i) + stack; //(nếu phần còn lại không đủ 8
                }
            }
            
            return stack;
        }
        private static List<byte> DeBin7ToBytes(string debin7)
        {
            List<byte> rs = new List<byte>();
            string s = "";
            for (int i = debin7.Length - 7; i >= 0; i -= 7)
            {
                try
                {
                    s = debin7.Substring(i, 7);
                    rs.Add(Convert.ToByte(s, 2));
                }
                catch { }
            }
            
            return rs;
        }

        //                            0         10          21        31        41       50         61        71        81       90         102     110         122=z 127
        //                            0123456789 012 34567890123456   7890123 4567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567
        static string _GSM7CharMap = "@£$¥èéùìòÇ\nØø\rÅåΔ_ΦΓΛΩΠΨΣΘΞ\x1BÆæßÉ !\"#¤%&'()*+,-./0123456789:;<=>?¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà";
        static string _GSM7CharMap_Full = "@£$¥èéùìòÇ\nØø\rÅåΔ_ΦΓΛΩΠΨΣΘΞ\x1BÆæßÉ !\"#¤%&'()*+,-./0123456789:;<=>?¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà\f^{}\\[~]|€";
        static string _GSM7Char2byte = "\f^{}\\[~]|€";
        static char _GSM_Form_feed = '\f';
        //static char _ESC = '\x1B';
        private static string BytesToGSM7string(List<byte> lb)
        {
            string rs = "";
            bool _1B = false;
            foreach (byte b in lb)
            {
                if(_1B)
                {
                    _1B = false;
                    switch (b)
                    {
                        case 0x0A://10
                            rs += _GSM_Form_feed; break;
                        case 0x14://20
                            rs += '^'; break;
                        case 0x28://40
                            rs += '{'; break;
                        case 0x29://41
                            rs += '}'; break;
                        case 0x2F://47
                            rs += '\\'; break;
                        case 0x3C://60
                            rs += '['; break;
                        case 0x3D://61
                            rs += '~'; break;
                        case 0x3E://62
                            rs += ']'; break;
                        case 0x40://64
                            rs += '|'; break;
                        case 0x65://101
                            rs += '€'; break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (b)
                    {
                        case 0x1B://0x1B == 27;
                            _1B = true;
                            break;
                        default:
                            rs += _GSM7CharMap[b];
                            break;
                    }
                }
            }
            if (rs.EndsWith("@"))
                rs = rs.Substring(0, rs.Length - 1);
            return rs;
        }
        /// <summary>
        /// Giải mã chuỗi 7bít ngược thành chuỗi thường.
        /// </summary>
        /// <param name="debin7"></param>
        /// <returns></returns>
        private static string Decode7bitBinde(string debin7)
        {
            string rs = "";
            string s = "";
            for (int i = debin7.Length-7; i >= 7; i -= 7)
            {
                try
                {
                    s = debin7.Substring(i, 7);
                    rs += Convert.ToChar(Convert.ToInt16(s, 2));
                }
                catch { }
            }
            //string is abcde
            if (rs.EndsWith("\0"))
                rs = rs.Substring(0, rs.Length - 1);
            return rs;
        }

        public static string Decode7bitGSMBinDe(string binde7)
        {
            List<byte> lb = DeBin7ToBytes(binde7);
            return BytesToGSM7string(lb);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bin7code"></param>
        /// <returns></returns>
        private static string Decode7BitBinCode(string bin7code)
        {
            //21111111.33222222.44433333.55554444.00000555
            string rs = "";

            //Fix
            //lần lượt đưa 8 bit vào stack
            string stack = "";
            string _binFix = "";


            for (int i = 0; i < bin7code.Length; i += 8)
            {
                try
                {
                    stack = bin7code.Substring(i, 8) + stack;
                }
                catch
                {
                    stack = bin7code.Substring(i) + stack; //(nếu phần còn lại không đủ 8
                }
            }
            //stack become 00000555.55554444.44433333.33222222.21111111
            for (int j = 0; j < stack.Length; j += 7)
            {
                try
                {
                    _binFix += stack.Substring(stack.Length - j - 7, 7);
                }
                catch
                {
                    _binFix += stack.Substring(0, stack.Length - j);
                }
            }
            //_binFix become 11111112.22222233.33333444.444455555.5500000
            //will get each 7 bin 1111111.2222222.3333333.4444444.5555555, 00000 FAIL don't get!
            
            for (int i = 0; i < _binFix.Length; i += 7)
            {
                try
                {
                    string s = _binFix.Substring(i, 7);
                    rs += Convert.ToChar(Convert.ToInt16(s, 2));
                }
                catch { }
            }
            //string is abcde
            if (rs.EndsWith("\0"))
                rs = rs.Substring(0, rs.Length - 1);
            return rs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Hex7BitString"></param>
        /// <returns></returns>
        public static string Decode7BitHex(string Hex7BitString)
        {
            string bin7code = HexStringToBinString(Hex7BitString);
            string deBin7code = Bincode7toBinde7(bin7code);
            return Decode7bitGSMBinDe(deBin7code);
        }

        public static string Encode7BitHex(string s)
        {
            return Encode7BitBinTo7BitHex(Encode7BitBin(s));
        }

        /// <summary>
        /// Byte đầu tiên = 8bits = 2Hex cắt lấy 7 bit đầu tiên.
        /// </summary>
        /// <param name="partHex"></param>
        /// <returns></returns>
        public static string Decode7bitHex8bitFirstOne(string partHex)
        {
            //string rs = "";
            string first7bit = "0";
            string restbincode7 = "0";
            string restbinde7 = "0";

            if (partHex.Length >= 2)
                first7bit = (HexCharToBin4(partHex[0]) + HexCharToBin4(partHex[1]).Substring(0, 3));
            if (partHex.Length > 2)
            {
                restbincode7 = HexStringToBinString(partHex.Substring(2));
                restbinde7 = Bincode7toBinde7(restbincode7);
            }
            //rs += Decode7BitHex(partHex.Substring(2));
            return Decode7bitGSMBinDe(restbinde7 + first7bit);
        }

        public static string Encode7bitHex8First(string value)
        {
            if(string.IsNullOrEmpty(value))return "";
            string rs = "";
            //lấy ký tự đầu tiên chuyển thành "7 bít" + "0";
            string firstBin = IntToFixBin((int)value[0], 7) + "0";
            rs += (Bin4ToHexChar(firstBin.Substring(0, 4)) + Bin4ToHexChar(firstBin.Substring(4)));
            rs += Encode7BitHex(value.Substring(1));
            return rs;
        }
        public static string Encode8BitHex(string str)
        {
            string rs = "";
            foreach (char c in str)
            {
                rs += ((short)c).ToString("X2");
            }
            return rs;
        }
        /// <summary>
        /// Mã hóa pdu nội dung tin cho unicode
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static string Encode16bitHex(string Content)
        {
            string str2 = "";
            int length = Content.Length;
            for (int i = 0; i < length; i++)
            {
                //string str3 = Strings.Format(Strings.AscW(Content.Substring(i, 4)), "X4");
                string str3 = ((short)Content[i]).ToString("X4");
                str2 = str2 + str3;
            }
            return str2;
        }


        /// <summary>
        /// Lấy 2Hex đầu tiên tính thành số, cắt luôn chuỗi đưa vào
        /// </summary>
        /// <param name="pdu"></param>
        /// <returns></returns>
        public static byte GetByte(ref string pdu)
        {
            if (pdu.Length > 2)
            {
                byte result = (byte)Convert.ToSByte(pdu.Substring(0, 2), 16);
                pdu = pdu.Substring(2);
                return result;
            }
            else
            {
                return 0;
            }
        }

        public static string GetString(ref string PDUCode, int Length)
        {
            string str2 = PDUCode.Substring(0, Length);
            PDUCode = PDUCode.Substring(Length);
            return str2;
        }

        public static string GetString (ref string s, string flag, bool trim = true)
        {
            if(s.Contains(flag))
            {
                string get = s.Substring(0, s.IndexOf(flag));
                s = s.Substring(s.IndexOf(flag) + flag.Length);
                if (trim) get = get.Trim();
                return get;
            }
            else
            {
                return "";
            }
        }
        
        private static string CharToGsm7Binde(char c)
        {
            int code = 0;//00=@
            //Nếu ký tự tồn tại trong _GSM7CharMap lấy index đó
            code = _GSM7CharMap.IndexOf(c);
            if(code>=0) return IntToFixBin(code,7);
            else
	        {
                code = _GSM7Char2byte.IndexOf(c);
                if(code>=0)
                {
                    string bin = "0011011";//27=1B=Escape?
                    switch (c)// "\f^{}\\[~]|€"
                    {
                       case '\f': bin = "0001010" + bin; break;
                        case '^': bin = "0010100" + bin; break;
                        case '{': bin = "0101000" + bin; break;
                        case '}': bin = "0101001" + bin; break;
                       case '\\': bin = "0101111" + bin; break;
                        case '[': bin = "0111100" + bin; break;
                        case '~': bin = "0111101" + bin; break;
                        case ']': bin = "0111110" + bin; break;
                        case '|': bin = "1000000" + bin; break;
                        case '€': bin = "1100101" + bin; break;
                        default: bin = "0111111"; break; //?
                    }
                    return bin;
                }
	        }
            return "0111111";//?
        }
        public static string IntToFixBin(int Dec, int fix)
        {
            int value = Dec;
            string rs = "";
            while (value > 0)
            {
                rs = (value % 2).ToString() + rs;
                value /= 2;
            }
            while (rs.Length<fix)
            {
                rs = "0" + rs;
            }
            return rs;
        }
        private static List<byte> GSM7stringToBytes(string s)
        {
            List<byte> lb = new List<byte>();
            int gsm7charcode = -1;

            foreach (char c in s)
            {
                switch (c)
                {
                    case '\f':// 0x0C://12 form feed
                        lb.Add(0x1B); lb.Add(0x0A); break;
                    case '^':// 0x5E://94 ^
                        lb.Add(0x1B); lb.Add(0x14); break;
                    case '{'://7B = 123
                        lb.Add(0x1B); lb.Add(0x28); break;
                    case '}'://1D = 125
                        lb.Add(0x1B); lb.Add(0x29); break;
                    case '\\'://5C = 92
                        lb.Add(0x1B); lb.Add(0x2F); break;
                    case '['://5B = 91
                        lb.Add(0x1B); lb.Add(0x3C); break;
                    case '~'://7E = 126
                        lb.Add(0x1B); lb.Add(0x3D); break;
                    case ']'://5D = 93
                        lb.Add(0x1B); lb.Add(0x3E); break;
                    case '|'://7C = 124
                        lb.Add(0x1B); lb.Add(0x40); break;
                    case '€'://164 (ISO-8859-15)
                        lb.Add(0x1B); lb.Add(0x65); break;
                    default:
                        gsm7charcode = _GSM7CharMap.IndexOf(c);
                        if(gsm7charcode > -1)
                        {
                            lb.Add((byte)gsm7charcode);
                        }
                        else
                        {
                            lb.Add((byte)'?');//same 63
                        }
                        break;
                }
            }

            return lb;
        }
        /// <summary>
        /// Mã hóa chuỗi thành bincode7
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Encode7BitBin(string s)
        {
            //List<byte> lb = GSM7stringToBytes(s);
            string stack = "";

            foreach (char c in s)
            {
                stack = CharToGsm7Binde(c) + stack;
            }
            //foreach (byte item in lb)
            //{
            //    stack = IntToFixBin(item, 7) + stack;
            //}
            //foreach (char c in s)
            //{
            //    string bit7 = IntToFixBin((int)c, 7);
                
            //    stack = bit7 + stack;
            //}
            
            while (stack.Length % 8 > 0)
            {
                stack = "0" + stack;
            }
            //bin become 00000555.55554444.44433333.33222222.21111111
            
            string rs = "";
            for (int i = 0; i < stack.Length; i += 8)
            {
                rs = stack.Substring(i, 8) + rs;
            }
            //bin become 21111111.33222222.44433333.55554444.00000555
            return rs;
        }
        public static string Encode7BitBinTo7BitHex(string bin7)
        {
            //21111111.33222222.44433333.55554444.00000555
            //make to BACBDCED0E
            string rs = "";
            while (bin7.Length % 4 != 0)
            {
                bin7 += "0";
            }
            for (int i = 0; i < bin7.Length; i += 4)
            {
                string s = bin7.Substring(i, 4);

                rs += Bin4ToHexChar(s);
            }
            return rs;
        }

        static string Bin4ToHexChar(string bin4)
        {
            string h = "0";
            switch (bin4)
            {
                case "0000":
                    h = "0";
                    break;
                case "0001":
                    h = "1";
                    break;
                case "0010":
                    h = "2";
                    break;
                case "0011":
                    h = "3";
                    break;
                case "0100":
                    h = "4";
                    break;
                case "0101":
                    h = "5";
                    break;
                case "0110":
                    h = "6";
                    break;
                case "0111":
                    h = "7";
                    break;
                case "1000":
                    h = "8";
                    break;
                case "1001":
                    h = "9";
                    break;
                case "1010":
                    h = "A";
                    break;
                case "1011":
                    h = "B";
                    break;
                case "1100":
                    h = "C";
                    break;
                case "1101":
                    h = "D";
                    break;
                case "1110":
                    h = "E";
                    break;
                case "1111":
                    h = "F";
                    break;
                default:
                    break;
            }
            return h;
        }

        /// <summary>
        /// Mỗi hex thành 4 bit.
        /// </summary>
        /// <param name="HexString"></param>
        /// <returns></returns>
        private static string HexStringToBinString(string HexString)
        {
            string rs = "";
            foreach (char c in HexString)
            {
                rs += HexCharToBin4(c);
            }
            return rs;
        }
        
        public static string HexCharToBin4(char HexChar)
        {
            //byte hexValue = Convert.ToByte(HexChar.ToString(), 16);
            //string binString = Convert.ToString(hexValue,2);
            //while (binString.Length < 4)
            //    binString = "0" + binString;
            //return binString;
            string binString = "0000";
            switch (HexChar)
            {
                case '0':
                    binString = "0000";
                    break;
                case '1':
                    binString = "0001";
                    break;
                case '2':
                    binString = "0010";
                    break;
                case '3':
                    binString = "0011";
                    break;
                case '4':
                    binString = "0100";
                    break;
                case '5':
                    binString = "0101";
                    break;
                case '6':
                    binString = "0110";
                    break;
                case '7':
                    binString = "0111";
                    break;
                case '8':
                    binString = "1000";
                    break;
                case '9':
                    binString = "1001";
                    break;
                case 'A': case 'a':
                    binString = "1010";
                    break;
                case 'B': case 'b':
                    binString = "1011";
                    break;
                case 'C': case 'c':
                    binString = "1100";
                    break;
                case 'D': case 'd':
                    binString = "1101";
                    break;
                case 'E': case 'e':
                    binString = "1110";
                    break;
                case 'F': case 'f':
                    binString = "1111";
                    break;
                default:
                    break;
            }
            return binString;
        }
    }
}
