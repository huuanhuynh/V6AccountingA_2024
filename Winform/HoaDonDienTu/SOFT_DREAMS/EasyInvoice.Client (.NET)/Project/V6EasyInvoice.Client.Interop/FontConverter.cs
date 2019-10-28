// Decompiled with JetBrains decompiler
// Type: EasyInvoice.Client.Interop.FontConverter
// Assembly: EasyInvoice.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9D591C1A-17CB-4CA4-A650-8459834356C1
// Assembly location: E:\Copy\Code\HoaDonDienTu\SOFT_DREAMS\EasyInvoice.Client (.NET)\EasyInvoice.Client.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace V6EasyInvoice.Client.Interop
{
    public static class FontConverter
    {
        private static readonly string[,] Code = new string[135, 8];
        private const int nCode = 8;
        private const int iNCR = 0;
        private const int iUTF = 1;
        private const int iTCV = 2;
        private const int iVNI = 3;
        private const int iUNI = 6;
        private const int iUTH = 5;
        private const int iCP1258 = 4;
        private const int iVIQ = 8;
        private const int iNonDiacritic = 7;

        static FontConverter()
        {
            FontConverter.Code[0, 0] = 0.ToString();
            FontConverter.Code[0, 1] = 1.ToString();
            FontConverter.Code[0, 2] = 2.ToString();
            FontConverter.Code[0, 3] = 3.ToString();
            FontConverter.Code[0, 5] = 5.ToString();
            FontConverter.Code[0, 6] = 6.ToString();
            FontConverter.Code[0, 4] = 4.ToString();
            FontConverter.Code[0, 7] = 7.ToString();
            FontConverter.MapUnicode();
            FontConverter.MapVNI();
            FontConverter.MapTCVN3();
            FontConverter.MapUTH();
            FontConverter.MapUTF8();
            FontConverter.MapNCR();
            FontConverter.MapNonDiacritic();
            FontConverter.MapCP1258();
        }

        public static string ConvertVniToUnicode(this string value)
        {
            if (!Utils.IsNullOrWhiteSpace(value))
                return FontConverter.Convert(value, FontIndex.VNI, FontIndex.UNICODE);
            return value;
        }

        public static string ConvertTCVN3ToUnicode(this string value)
        {
            if (!Utils.IsNullOrWhiteSpace(value))
                return FontConverter.Convert(value, FontIndex.TCVN3, FontIndex.UNICODE);
            return value;
        }

        public static string ConvertUnicodeToVni(this string value)
        {
            if (!Utils.IsNullOrWhiteSpace(value))
                return FontConverter.Convert(value, FontIndex.UNICODE, FontIndex.VNI);
            return value;
        }

        public static string ConvertUnicodeToTCVN3(this string value)
        {
            if (!Utils.IsNullOrWhiteSpace(value))
                return FontConverter.Convert(value, FontIndex.UNICODE, FontIndex.TCVN3);
            return value;
        }

        public static string Convert(string str, FontIndex iSource, FontIndex iDestination)
        {
            return FontConverter.Convert(str, iSource, iDestination, FontCase.Normal);
        }

        public static string Convert(string str, FontIndex iSource, FontIndex iDestination, FontCase charCase)
        {
            int index1 = (int)iSource;
            int index2 = (int)iDestination;
            if (str.Trim() == "" || index1 == index2)
                return str;
            string str1 = "";
            string str2 = "";
            if (index1 == -1)
            {
                int num = (int)FontConverter.GetCode(str);
                if (num <= -1)
                    return str;
                index1 = num;
            }
            if (index2 == -1)
                index2 = 0;
            int num1 = FontConverter.GetnChar((FontIndex)int.Parse(FontConverter.Code[0, index1]));
            int num2 = num1 > 1 ? num1 - 1 : 1;
            string str3 = "";
            bool flag = false;
            str += "       ";
            int startIndex = 0;
            while (startIndex < str.Length - 7)
            {
                for (int length = num1; length >= num2; --length)
                {
                    str2 = "";
                    if (str.Substring(startIndex, 1) == " ")
                    {
                        str1 = " ";
                        break;
                    }
                    str1 = str.Substring(startIndex, length);
                    for (int index3 = 1; index3 < 135; ++index3)
                    {
                        if (str1 == FontConverter.Code[index3, index1])
                        {
                            str2 = charCase != FontCase.UpperCase || index3 >= 68 ? (charCase != FontCase.LowerCase || index3 < 68 ? FontConverter.Code[index3, index2] : FontConverter.Code[index3 - 67, index2]) : FontConverter.Code[index3 + 67, index2];
                            startIndex += length;
                            break;
                        }
                    }
                    if (str2 != "" || length == 5)
                        break;
                }
                if (str2 != "")
                {
                    str3 += str2;
                    flag = true;
                }
                else
                {
                    str3 = charCase != FontCase.UpperCase ? (charCase != FontCase.LowerCase ? str3 + str1.Substring(0, 1) : str3 + str1.Substring(0, 1).ToLower()) : str3 + str1.Substring(0, 1).ToUpper();
                    ++startIndex;
                }
            }
            if (!flag)
            {
                str = str.Remove(str.Length - 7, 7);
                if (charCase == FontCase.UpperCase)
                    str = str.ToUpper();
                else if (charCase == FontCase.LowerCase)
                    str = str.ToLower();
            }
            str = str3.TrimEnd();
            return str;
        }

        public static bool IsVietnamese(string s, ref FontIndex code)
        {
            code = FontConverter.GetCode(s);
            return code > FontIndex.UnKnown;
        }

        public static bool IsVietnamese(string s)
        {
            return FontConverter.GetCode(s) != FontIndex.UnKnown;
        }

        private static int GetnChar(FontIndex index)
        {
            if (index == FontIndex.UTF8)
                return 3;
            if (((uint)(index - 5) <= 1U ? 0 : (index != FontIndex.NonDiacritic ? 1 : 0)) == 0)
                return 1;
            return index != FontIndex.NCR ? 2 : 7;
        }

        private static int GetIntCode(string code)
        {
            for (int index = 0; index < 8; ++index)
            {
                if (FontConverter.Code[0, index] == code)
                    return index;
            }
            return -1;
        }

        public static bool IsSpecialChar(string s)
        {
            return FontConverter.IsSpecialChar(s, false);
        }

        private static bool IsSpecialChar(string s, bool isUnicode)
        {
            if (s.Length > 2)
                return false;
            string[] strArray1 = new string[37]
      {
        "í",
        "ì",
        "ó",
        "ò",
        "ô",
        "ñ",
        "î",
        "Ê",
        "È",
        "É",
        "á",
        "à",
        "â",
        "è",
        "é",
        "ê",
        "ù",
        "ý",
        "ú",
        "ö",
        "Í",
        "Ì",
        "Ó",
        "Ò",
        "Ô",
        "Ñ",
        "Î",
        "Õ",
        "Ý",
        "Ã",
        "oà",
        "oá",
        "oã",
        "uû",
        "OÁ",
        "OÀ",
        "OÃ"
      };
            string[] strArray2 = new string[44]
      {
        "ă",
        "â",
        "ê",
        "ô",
        "ơ",
        "ư",
        "đ",
        "í",
        "ì",
        "ó",
        "ò",
        "ô",
        "ñ",
        "î",
        "Ê",
        "È",
        "É",
        "á",
        "à",
        "â",
        "è",
        "é",
        "ê",
        "ù",
        "ý",
        "ú",
        "ö",
        "Í",
        "Ì",
        "Ó",
        "Ò",
        "Ô",
        "Ñ",
        "Î",
        "Õ",
        "Ý",
        "Ã",
        "oà",
        "oá",
        "oã",
        "uû",
        "OÁ",
        "OÀ",
        "OÃ"
      };
            return Enumerable.Any<string>(isUnicode ? (IEnumerable<string>)strArray2 : (IEnumerable<string>)strArray1, (Func<string, bool>)(strB => string.Compare(s, strB, StringComparison.Ordinal) == 0));
        }

        private static FontIndex GetCode(string str)
        {
            if (str.Trim() == "")
                return FontIndex.UnKnown;
            int index1 = -1;
            str += "       ";
            int startIndex = 0;
            while (startIndex < str.Length - 7)
            {
                if (str.Substring(startIndex, 1) == " ")
                {
                    ++startIndex;
                }
                else
                {
                    for (int length = 7; length > 0; --length)
                    {
                        string s = str.Substring(startIndex, length);
                        for (int index2 = 0; index2 < 7 && length != 4 && length != 5 && (length < 6 || index2 == 0); ++index2)
                        {
                            if ((length != 3 || index2 == 1) && (index2 != 3 && index2 != 2 && (index2 != 5 && index2 != 4) || length <= 2))
                            {
                                for (int index3 = 1; index3 < 135; ++index3)
                                {
                                    if (s == FontConverter.Code[index3, index2])
                                    {
                                        if (!FontConverter.IsSpecialChar(s, index2 == 5 || index2 == 6))
                                            return (FontIndex)int.Parse(FontConverter.Code[0, index2]);
                                        index1 = index2;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    ++startIndex;
                }
            }
            if (index1 < 0)
                return FontIndex.UnKnown;
            return (FontIndex)int.Parse(FontConverter.Code[0, index1]);
        }

        private static void MapUnicode()
        {
            FontConverter.Code[1, 6] = "á";
            FontConverter.Code[2, 6] = "à";
            FontConverter.Code[3, 6] = "ả";
            FontConverter.Code[4, 6] = "ã";
            FontConverter.Code[5, 6] = "ạ";
            FontConverter.Code[6, 6] = "ă";
            FontConverter.Code[7, 6] = "ắ";
            FontConverter.Code[8, 6] = "ằ";
            FontConverter.Code[9, 6] = "ẳ";
            FontConverter.Code[10, 6] = "ẵ";
            FontConverter.Code[11, 6] = "ặ";
            FontConverter.Code[12, 6] = "â";
            FontConverter.Code[13, 6] = "ấ";
            FontConverter.Code[14, 6] = "ầ";
            FontConverter.Code[15, 6] = "ẩ";
            FontConverter.Code[16, 6] = "ẫ";
            FontConverter.Code[17, 6] = "ậ";
            FontConverter.Code[18, 6] = "é";
            FontConverter.Code[19, 6] = "è";
            FontConverter.Code[20, 6] = "ẻ";
            FontConverter.Code[21, 6] = "ẽ";
            FontConverter.Code[22, 6] = "ẹ";
            FontConverter.Code[23, 6] = "ê";
            FontConverter.Code[24, 6] = "ế";
            FontConverter.Code[25, 6] = "ề";
            FontConverter.Code[26, 6] = "ể";
            FontConverter.Code[27, 6] = "ễ";
            FontConverter.Code[28, 6] = "ệ";
            FontConverter.Code[29, 6] = "í";
            FontConverter.Code[30, 6] = "ì";
            FontConverter.Code[31, 6] = "ỉ";
            FontConverter.Code[32, 6] = "ĩ";
            FontConverter.Code[33, 6] = "ị";
            FontConverter.Code[34, 6] = "ó";
            FontConverter.Code[35, 6] = "ò";
            FontConverter.Code[36, 6] = "ỏ";
            FontConverter.Code[37, 6] = "õ";
            FontConverter.Code[38, 6] = "ọ";
            FontConverter.Code[39, 6] = "ô";
            FontConverter.Code[40, 6] = "ố";
            FontConverter.Code[41, 6] = "ồ";
            FontConverter.Code[42, 6] = "ổ";
            FontConverter.Code[43, 6] = "ỗ";
            FontConverter.Code[44, 6] = "ộ";
            FontConverter.Code[45, 6] = "ơ";
            FontConverter.Code[46, 6] = "ớ";
            FontConverter.Code[47, 6] = "ờ";
            FontConverter.Code[48, 6] = "ở";
            FontConverter.Code[49, 6] = "ỡ";
            FontConverter.Code[50, 6] = "ợ";
            FontConverter.Code[51, 6] = "ú";
            FontConverter.Code[52, 6] = "ù";
            FontConverter.Code[53, 6] = "ủ";
            FontConverter.Code[54, 6] = "ũ";
            FontConverter.Code[55, 6] = "ụ";
            FontConverter.Code[56, 6] = "ư";
            FontConverter.Code[57, 6] = "ứ";
            FontConverter.Code[58, 6] = "ừ";
            FontConverter.Code[59, 6] = "ử";
            FontConverter.Code[60, 6] = "ữ";
            FontConverter.Code[61, 6] = "ự";
            FontConverter.Code[62, 6] = "ý";
            FontConverter.Code[63, 6] = "ỳ";
            FontConverter.Code[64, 6] = "ỷ";
            FontConverter.Code[65, 6] = "ỹ";
            FontConverter.Code[66, 6] = "ỵ";
            FontConverter.Code[67, 6] = "đ";
            FontConverter.Code[68, 6] = "Á";
            FontConverter.Code[69, 6] = "À";
            FontConverter.Code[70, 6] = "Ả";
            FontConverter.Code[71, 6] = "Ã";
            FontConverter.Code[72, 6] = "Ạ";
            FontConverter.Code[73, 6] = "Ă";
            FontConverter.Code[74, 6] = "Ắ";
            FontConverter.Code[75, 6] = "Ằ";
            FontConverter.Code[76, 6] = "Ẳ";
            FontConverter.Code[77, 6] = "Ẵ";
            FontConverter.Code[78, 6] = "Ặ";
            FontConverter.Code[79, 6] = "Â";
            FontConverter.Code[80, 6] = "Ấ";
            FontConverter.Code[81, 6] = "Ầ";
            FontConverter.Code[82, 6] = "Ẩ";
            FontConverter.Code[83, 6] = "Ẫ";
            FontConverter.Code[84, 6] = "Ậ";
            FontConverter.Code[85, 6] = "É";
            FontConverter.Code[86, 6] = "È";
            FontConverter.Code[87, 6] = "Ẻ";
            FontConverter.Code[88, 6] = "Ẽ";
            FontConverter.Code[89, 6] = "Ẹ";
            FontConverter.Code[90, 6] = "Ê";
            FontConverter.Code[91, 6] = "Ế";
            FontConverter.Code[92, 6] = "Ề";
            FontConverter.Code[93, 6] = "Ể";
            FontConverter.Code[94, 6] = "Ễ";
            FontConverter.Code[95, 6] = "Ệ";
            FontConverter.Code[96, 6] = "Í";
            FontConverter.Code[97, 6] = "Ì";
            FontConverter.Code[98, 6] = "Ỉ";
            FontConverter.Code[99, 6] = "Ĩ";
            FontConverter.Code[100, 6] = "Ị";
            FontConverter.Code[101, 6] = "Ó";
            FontConverter.Code[102, 6] = "Ò";
            FontConverter.Code[103, 6] = "Ỏ";
            FontConverter.Code[104, 6] = "Õ";
            FontConverter.Code[105, 6] = "Ọ";
            FontConverter.Code[106, 6] = "Ô";
            FontConverter.Code[107, 6] = "Ố";
            FontConverter.Code[108, 6] = "Ồ";
            FontConverter.Code[109, 6] = "Ổ";
            FontConverter.Code[110, 6] = "Ỗ";
            FontConverter.Code[111, 6] = "Ộ";
            FontConverter.Code[112, 6] = "Ơ";
            FontConverter.Code[113, 6] = "Ớ";
            FontConverter.Code[114, 6] = "Ờ";
            FontConverter.Code[115, 6] = "Ở";
            FontConverter.Code[116, 6] = "Ỡ";
            FontConverter.Code[117, 6] = "Ợ";
            FontConverter.Code[118, 6] = "Ú";
            FontConverter.Code[119, 6] = "Ù";
            FontConverter.Code[120, 6] = "Ủ";
            FontConverter.Code[121, 6] = "Ũ";
            FontConverter.Code[122, 6] = "Ụ";
            FontConverter.Code[123, 6] = "Ư";
            FontConverter.Code[124, 6] = "Ứ";
            FontConverter.Code[125, 6] = "Ừ";
            FontConverter.Code[126, 6] = "Ử";
            FontConverter.Code[(int)sbyte.MaxValue, 6] = "Ữ";
            FontConverter.Code[128, 6] = "Ự";
            FontConverter.Code[129, 6] = "Ý";
            FontConverter.Code[130, 6] = "Ỳ";
            FontConverter.Code[131, 6] = "Ỷ";
            FontConverter.Code[132, 6] = "Ỹ";
            FontConverter.Code[133, 6] = "Ỵ";
            FontConverter.Code[134, 6] = "Đ";
        }

        private static void MapNonDiacritic()
        {
            FontConverter.Code[1, 7] = "a";
            FontConverter.Code[2, 7] = "a";
            FontConverter.Code[3, 7] = "a";
            FontConverter.Code[4, 7] = "a";
            FontConverter.Code[5, 7] = "a";
            FontConverter.Code[6, 7] = "a";
            FontConverter.Code[7, 7] = "a";
            FontConverter.Code[8, 7] = "a";
            FontConverter.Code[9, 7] = "a";
            FontConverter.Code[10, 7] = "a";
            FontConverter.Code[11, 7] = "a";
            FontConverter.Code[12, 7] = "a";
            FontConverter.Code[13, 7] = "a";
            FontConverter.Code[14, 7] = "a";
            FontConverter.Code[15, 7] = "a";
            FontConverter.Code[16, 7] = "a";
            FontConverter.Code[17, 7] = "a";
            FontConverter.Code[18, 7] = "e";
            FontConverter.Code[19, 7] = "e";
            FontConverter.Code[20, 7] = "e";
            FontConverter.Code[21, 7] = "e";
            FontConverter.Code[22, 7] = "e";
            FontConverter.Code[23, 7] = "e";
            FontConverter.Code[24, 7] = "e";
            FontConverter.Code[25, 7] = "e";
            FontConverter.Code[26, 7] = "e";
            FontConverter.Code[27, 7] = "e";
            FontConverter.Code[28, 7] = "e";
            FontConverter.Code[29, 7] = "i";
            FontConverter.Code[30, 7] = "i";
            FontConverter.Code[31, 7] = "i";
            FontConverter.Code[32, 7] = "i";
            FontConverter.Code[33, 7] = "i";
            FontConverter.Code[34, 7] = "o";
            FontConverter.Code[35, 7] = "o";
            FontConverter.Code[36, 7] = "o";
            FontConverter.Code[37, 7] = "o";
            FontConverter.Code[38, 7] = "o";
            FontConverter.Code[39, 7] = "o";
            FontConverter.Code[40, 7] = "o";
            FontConverter.Code[41, 7] = "o";
            FontConverter.Code[42, 7] = "o";
            FontConverter.Code[43, 7] = "o";
            FontConverter.Code[44, 7] = "o";
            FontConverter.Code[45, 7] = "o";
            FontConverter.Code[46, 7] = "o";
            FontConverter.Code[47, 7] = "o";
            FontConverter.Code[48, 7] = "o";
            FontConverter.Code[49, 7] = "o";
            FontConverter.Code[50, 7] = "o";
            FontConverter.Code[51, 7] = "u";
            FontConverter.Code[52, 7] = "u";
            FontConverter.Code[53, 7] = "u";
            FontConverter.Code[54, 7] = "u";
            FontConverter.Code[55, 7] = "u";
            FontConverter.Code[56, 7] = "u";
            FontConverter.Code[57, 7] = "u";
            FontConverter.Code[58, 7] = "u";
            FontConverter.Code[59, 7] = "u";
            FontConverter.Code[60, 7] = "u";
            FontConverter.Code[61, 7] = "u";
            FontConverter.Code[62, 7] = "y";
            FontConverter.Code[63, 7] = "y";
            FontConverter.Code[64, 7] = "y";
            FontConverter.Code[65, 7] = "y";
            FontConverter.Code[66, 7] = "y";
            FontConverter.Code[67, 7] = "d";
            FontConverter.Code[68, 7] = "A";
            FontConverter.Code[69, 7] = "A";
            FontConverter.Code[70, 7] = "A";
            FontConverter.Code[71, 7] = "A";
            FontConverter.Code[72, 7] = "A";
            FontConverter.Code[73, 7] = "A";
            FontConverter.Code[74, 7] = "A";
            FontConverter.Code[75, 7] = "A";
            FontConverter.Code[76, 7] = "A";
            FontConverter.Code[77, 7] = "A";
            FontConverter.Code[78, 7] = "A";
            FontConverter.Code[79, 7] = "A";
            FontConverter.Code[80, 7] = "A";
            FontConverter.Code[81, 7] = "A";
            FontConverter.Code[82, 7] = "A";
            FontConverter.Code[83, 7] = "A";
            FontConverter.Code[84, 7] = "A";
            FontConverter.Code[85, 7] = "E";
            FontConverter.Code[86, 7] = "E";
            FontConverter.Code[87, 7] = "E";
            FontConverter.Code[88, 7] = "E";
            FontConverter.Code[89, 7] = "E";
            FontConverter.Code[90, 7] = "E";
            FontConverter.Code[91, 7] = "E";
            FontConverter.Code[92, 7] = "E";
            FontConverter.Code[93, 7] = "E";
            FontConverter.Code[94, 7] = "E";
            FontConverter.Code[95, 7] = "E";
            FontConverter.Code[96, 7] = "I";
            FontConverter.Code[97, 7] = "I";
            FontConverter.Code[98, 7] = "I";
            FontConverter.Code[99, 7] = "I";
            FontConverter.Code[100, 7] = "I";
            FontConverter.Code[101, 7] = "O";
            FontConverter.Code[102, 7] = "O";
            FontConverter.Code[103, 7] = "O";
            FontConverter.Code[104, 7] = "O";
            FontConverter.Code[105, 7] = "O";
            FontConverter.Code[106, 7] = "O";
            FontConverter.Code[107, 7] = "O";
            FontConverter.Code[108, 7] = "O";
            FontConverter.Code[109, 7] = "O";
            FontConverter.Code[110, 7] = "O";
            FontConverter.Code[111, 7] = "O";
            FontConverter.Code[112, 7] = "O";
            FontConverter.Code[113, 7] = "O";
            FontConverter.Code[114, 7] = "O";
            FontConverter.Code[115, 7] = "O";
            FontConverter.Code[116, 7] = "O";
            FontConverter.Code[117, 7] = "O";
            FontConverter.Code[118, 7] = "U";
            FontConverter.Code[119, 7] = "U";
            FontConverter.Code[120, 7] = "U";
            FontConverter.Code[121, 7] = "U";
            FontConverter.Code[122, 7] = "U";
            FontConverter.Code[123, 7] = "U";
            FontConverter.Code[124, 7] = "U";
            FontConverter.Code[125, 7] = "U";
            FontConverter.Code[126, 7] = "U";
            FontConverter.Code[(int)sbyte.MaxValue, 7] = "U";
            FontConverter.Code[128, 7] = "U";
            FontConverter.Code[129, 7] = "Y";
            FontConverter.Code[130, 7] = "Y";
            FontConverter.Code[131, 7] = "Y";
            FontConverter.Code[132, 7] = "Y";
            FontConverter.Code[133, 7] = "Y";
            FontConverter.Code[134, 7] = "D";
        }

        private static void MapVNI()
        {
            FontConverter.Code[1, 3] = "aù";
            FontConverter.Code[2, 3] = "aø";
            FontConverter.Code[3, 3] = "aû";
            FontConverter.Code[4, 3] = "aõ";
            FontConverter.Code[5, 3] = "aï";
            FontConverter.Code[6, 3] = "aê";
            FontConverter.Code[7, 3] = "aé";
            FontConverter.Code[8, 3] = "aè";
            FontConverter.Code[9, 3] = "aú";
            FontConverter.Code[10, 3] = "aü";
            FontConverter.Code[11, 3] = "aë";
            FontConverter.Code[12, 3] = "aâ";
            FontConverter.Code[13, 3] = "aá";
            FontConverter.Code[14, 3] = "aà";
            FontConverter.Code[15, 3] = "aå";
            FontConverter.Code[16, 3] = "aã";
            FontConverter.Code[17, 3] = "aä";
            FontConverter.Code[18, 3] = "eù";
            FontConverter.Code[19, 3] = "eø";
            FontConverter.Code[20, 3] = "eû";
            FontConverter.Code[21, 3] = "eõ";
            FontConverter.Code[22, 3] = "eï";
            FontConverter.Code[23, 3] = "eâ";
            FontConverter.Code[24, 3] = "eá";
            FontConverter.Code[25, 3] = "eà";
            FontConverter.Code[26, 3] = "eå";
            FontConverter.Code[27, 3] = "eã";
            FontConverter.Code[28, 3] = "eä";
            FontConverter.Code[29, 3] = "í";
            FontConverter.Code[30, 3] = "ì";
            FontConverter.Code[31, 3] = "æ";
            FontConverter.Code[32, 3] = "ó";
            FontConverter.Code[33, 3] = "ò";
            FontConverter.Code[34, 3] = "où";
            FontConverter.Code[35, 3] = "oø";
            FontConverter.Code[36, 3] = "oû";
            FontConverter.Code[37, 3] = "oõ";
            FontConverter.Code[38, 3] = "oï";
            FontConverter.Code[39, 3] = "oâ";
            FontConverter.Code[40, 3] = "oá";
            FontConverter.Code[41, 3] = "oà";
            FontConverter.Code[42, 3] = "oå";
            FontConverter.Code[43, 3] = "oã";
            FontConverter.Code[44, 3] = "oä";
            FontConverter.Code[45, 3] = "ô";
            FontConverter.Code[46, 3] = "ôù";
            FontConverter.Code[47, 3] = "ôø";
            FontConverter.Code[48, 3] = "ôû";
            FontConverter.Code[49, 3] = "ôõ";
            FontConverter.Code[50, 3] = "ôï";
            FontConverter.Code[51, 3] = "uù";
            FontConverter.Code[52, 3] = "uø";
            FontConverter.Code[53, 3] = "uû";
            FontConverter.Code[54, 3] = "uõ";
            FontConverter.Code[55, 3] = "uï";
            FontConverter.Code[56, 3] = "ö";
            FontConverter.Code[57, 3] = "öù";
            FontConverter.Code[58, 3] = "öø";
            FontConverter.Code[59, 3] = "öû";
            FontConverter.Code[60, 3] = "öõ";
            FontConverter.Code[61, 3] = "öï";
            FontConverter.Code[62, 3] = "yù";
            FontConverter.Code[63, 3] = "yø";
            FontConverter.Code[64, 3] = "yû";
            FontConverter.Code[65, 3] = "yõ";
            FontConverter.Code[66, 3] = "î";
            FontConverter.Code[67, 3] = "ñ";
            FontConverter.Code[68, 3] = "AÙ";
            FontConverter.Code[69, 3] = "AØ";
            FontConverter.Code[70, 3] = "AÛ";
            FontConverter.Code[71, 3] = "AÕ";
            FontConverter.Code[72, 3] = "AÏ";
            FontConverter.Code[73, 3] = "AÊ";
            FontConverter.Code[74, 3] = "AÉ";
            FontConverter.Code[75, 3] = "AÈ";
            FontConverter.Code[76, 3] = "AÚ";
            FontConverter.Code[77, 3] = "AÜ";
            FontConverter.Code[78, 3] = "AË";
            FontConverter.Code[79, 3] = "AÂ";
            FontConverter.Code[80, 3] = "AÁ";
            FontConverter.Code[81, 3] = "AÀ";
            FontConverter.Code[82, 3] = "AÅ";
            FontConverter.Code[83, 3] = "AÃ";
            FontConverter.Code[84, 3] = "AÄ";
            FontConverter.Code[85, 3] = "EÙ";
            FontConverter.Code[86, 3] = "EØ";
            FontConverter.Code[87, 3] = "EÛ";
            FontConverter.Code[88, 3] = "EÕ";
            FontConverter.Code[89, 3] = "EÏ";
            FontConverter.Code[90, 3] = "EÂ";
            FontConverter.Code[91, 3] = "EÁ";
            FontConverter.Code[92, 3] = "EÀ";
            FontConverter.Code[93, 3] = "EÅ";
            FontConverter.Code[94, 3] = "EÃ";
            FontConverter.Code[95, 3] = "EÄ";
            FontConverter.Code[96, 3] = "Í";
            FontConverter.Code[97, 3] = "Ì";
            FontConverter.Code[98, 3] = "Æ";
            FontConverter.Code[99, 3] = "Ó";
            FontConverter.Code[100, 3] = "Ò";
            FontConverter.Code[101, 3] = "OÙ";
            FontConverter.Code[102, 3] = "OØ";
            FontConverter.Code[103, 3] = "OÛ";
            FontConverter.Code[104, 3] = "OÕ";
            FontConverter.Code[105, 3] = "OÏ";
            FontConverter.Code[106, 3] = "OÂ";
            FontConverter.Code[107, 3] = "OÁ";
            FontConverter.Code[108, 3] = "OÀ";
            FontConverter.Code[109, 3] = "OÅ";
            FontConverter.Code[110, 3] = "OÃ";
            FontConverter.Code[111, 3] = "OÄ";
            FontConverter.Code[112, 3] = "Ô";
            FontConverter.Code[113, 3] = "ÔÙ";
            FontConverter.Code[114, 3] = "ÔØ";
            FontConverter.Code[115, 3] = "ÔÛ";
            FontConverter.Code[116, 3] = "ÔÕ";
            FontConverter.Code[117, 3] = "ÔÏ";
            FontConverter.Code[118, 3] = "UÙ";
            FontConverter.Code[119, 3] = "UØ";
            FontConverter.Code[120, 3] = "UÛ";
            FontConverter.Code[121, 3] = "UÕ";
            FontConverter.Code[122, 3] = "UÏ";
            FontConverter.Code[123, 3] = "Ö";
            FontConverter.Code[124, 3] = "ÖÙ";
            FontConverter.Code[125, 3] = "ÖØ";
            FontConverter.Code[126, 3] = "ÖÛ";
            FontConverter.Code[(int)sbyte.MaxValue, 3] = "ÖÕ";
            FontConverter.Code[128, 3] = "ÖÏ";
            FontConverter.Code[129, 3] = "YÙ";
            FontConverter.Code[130, 3] = "YØ";
            FontConverter.Code[131, 3] = "YÛ";
            FontConverter.Code[132, 3] = "YÕ";
            FontConverter.Code[133, 3] = "Î";
            FontConverter.Code[134, 3] = "Ñ";
        }

        private static void MapTCVN3()
        {
            FontConverter.Code[1, 2] = "¸";
            FontConverter.Code[2, 2] = "µ";
            FontConverter.Code[3, 2] = "¶";
            FontConverter.Code[4, 2] = "·";
            FontConverter.Code[5, 2] = "\x00B9";
            FontConverter.Code[6, 2] = "¨";
            FontConverter.Code[7, 2] = "\x00BE";
            FontConverter.Code[8, 2] = "»";
            FontConverter.Code[9, 2] = "\x00BC";
            FontConverter.Code[10, 2] = "\x00BD";
            FontConverter.Code[11, 2] = "Æ";
            FontConverter.Code[12, 2] = "©";
            FontConverter.Code[13, 2] = "Ê";
            FontConverter.Code[14, 2] = "Ç";
            FontConverter.Code[15, 2] = "È";
            FontConverter.Code[16, 2] = "É";
            FontConverter.Code[17, 2] = "Ë";
            FontConverter.Code[18, 2] = "Ð";
            FontConverter.Code[19, 2] = "Ì";
            FontConverter.Code[20, 2] = "Î";
            FontConverter.Code[21, 2] = "Ï";
            FontConverter.Code[22, 2] = "Ñ";
            FontConverter.Code[23, 2] = "ª";
            FontConverter.Code[24, 2] = "Õ";
            FontConverter.Code[25, 2] = "Ò";
            FontConverter.Code[26, 2] = "Ó";
            FontConverter.Code[27, 2] = "Ô";
            FontConverter.Code[28, 2] = "Ö";
            FontConverter.Code[29, 2] = "Ý";
            FontConverter.Code[30, 2] = "×";
            FontConverter.Code[31, 2] = "Ø";
            FontConverter.Code[32, 2] = "Ü";
            FontConverter.Code[33, 2] = "Þ";
            FontConverter.Code[34, 2] = "ã";
            FontConverter.Code[35, 2] = "ß";
            FontConverter.Code[36, 2] = "á";
            FontConverter.Code[37, 2] = "â";
            FontConverter.Code[38, 2] = "ä";
            FontConverter.Code[39, 2] = "«";
            FontConverter.Code[40, 2] = "è";
            FontConverter.Code[41, 2] = "å";
            FontConverter.Code[42, 2] = "æ";
            FontConverter.Code[43, 2] = "ç";
            FontConverter.Code[44, 2] = "é";
            FontConverter.Code[45, 2] = "¬";
            FontConverter.Code[46, 2] = "í";
            FontConverter.Code[47, 2] = "ê";
            FontConverter.Code[48, 2] = "ë";
            FontConverter.Code[49, 2] = "ì";
            FontConverter.Code[50, 2] = "î";
            FontConverter.Code[51, 2] = "ó";
            FontConverter.Code[52, 2] = "ï";
            FontConverter.Code[53, 2] = "ñ";
            FontConverter.Code[54, 2] = "ò";
            FontConverter.Code[55, 2] = "ô";
            FontConverter.Code[56, 2] = "\x00AD";
            FontConverter.Code[57, 2] = "ø";
            FontConverter.Code[58, 2] = "õ";
            FontConverter.Code[59, 2] = "ö";
            FontConverter.Code[60, 2] = "÷";
            FontConverter.Code[61, 2] = "ù";
            FontConverter.Code[62, 2] = "ý";
            FontConverter.Code[63, 2] = "ú";
            FontConverter.Code[64, 2] = "û";
            FontConverter.Code[65, 2] = "ü";
            FontConverter.Code[66, 2] = "þ";
            FontConverter.Code[67, 2] = "®";
            FontConverter.Code[68, 2] = "¸";
            FontConverter.Code[69, 2] = "µ";
            FontConverter.Code[70, 2] = "¶";
            FontConverter.Code[71, 2] = "·";
            FontConverter.Code[72, 2] = "\x00B9";
            FontConverter.Code[73, 2] = "¡";
            FontConverter.Code[74, 2] = "\x00BE";
            FontConverter.Code[75, 2] = "»";
            FontConverter.Code[76, 2] = "\x00BC";
            FontConverter.Code[77, 2] = "\x00BD";
            FontConverter.Code[78, 2] = "Æ";
            FontConverter.Code[79, 2] = "¢";
            FontConverter.Code[80, 2] = "Ê";
            FontConverter.Code[81, 2] = "Ç";
            FontConverter.Code[82, 2] = "È";
            FontConverter.Code[83, 2] = "É";
            FontConverter.Code[84, 2] = "Ë";
            FontConverter.Code[85, 2] = "Ð";
            FontConverter.Code[86, 2] = "Ì";
            FontConverter.Code[87, 2] = "Î";
            FontConverter.Code[88, 2] = "Ï";
            FontConverter.Code[89, 2] = "Ñ";
            FontConverter.Code[90, 2] = "£";
            FontConverter.Code[91, 2] = "Õ";
            FontConverter.Code[92, 2] = "Ò";
            FontConverter.Code[93, 2] = "Ó";
            FontConverter.Code[94, 2] = "Ô";
            FontConverter.Code[95, 2] = "Ö";
            FontConverter.Code[96, 2] = "Ý";
            FontConverter.Code[97, 2] = "×";
            FontConverter.Code[98, 2] = "Ø";
            FontConverter.Code[99, 2] = "Ü";
            FontConverter.Code[100, 2] = "Þ";
            FontConverter.Code[101, 2] = "ã";
            FontConverter.Code[102, 2] = "ß";
            FontConverter.Code[103, 2] = "á";
            FontConverter.Code[104, 2] = "â";
            FontConverter.Code[105, 2] = "ä";
            FontConverter.Code[106, 2] = "¤";
            FontConverter.Code[107, 2] = "è";
            FontConverter.Code[108, 2] = "å";
            FontConverter.Code[109, 2] = "æ";
            FontConverter.Code[110, 2] = "ç";
            FontConverter.Code[111, 2] = "é";
            FontConverter.Code[112, 2] = "¥";
            FontConverter.Code[113, 2] = "í";
            FontConverter.Code[114, 2] = "ê";
            FontConverter.Code[115, 2] = "ë";
            FontConverter.Code[116, 2] = "ì";
            FontConverter.Code[117, 2] = "î";
            FontConverter.Code[118, 2] = "ó";
            FontConverter.Code[119, 2] = "ï";
            FontConverter.Code[120, 2] = "ñ";
            FontConverter.Code[121, 2] = "ò";
            FontConverter.Code[122, 2] = "ô";
            FontConverter.Code[123, 2] = "¦";
            FontConverter.Code[124, 2] = "ø";
            FontConverter.Code[125, 2] = "õ";
            FontConverter.Code[126, 2] = "ö";
            FontConverter.Code[(int)sbyte.MaxValue, 2] = "÷";
            FontConverter.Code[128, 2] = "ù";
            FontConverter.Code[129, 2] = "ý";
            FontConverter.Code[130, 2] = "ú";
            FontConverter.Code[131, 2] = "û";
            FontConverter.Code[132, 2] = "ü";
            FontConverter.Code[133, 2] = "þ";
            FontConverter.Code[134, 2] = "§";
        }

        private static void MapUTH()
        {
            FontConverter.Code[1, 5] = "á";
            FontConverter.Code[2, 5] = "à";
            FontConverter.Code[3, 5] = "ả";
            FontConverter.Code[4, 5] = "ã";
            FontConverter.Code[5, 5] = "ạ";
            FontConverter.Code[6, 5] = "ă";
            FontConverter.Code[7, 5] = "ắ";
            FontConverter.Code[8, 5] = "ằ";
            FontConverter.Code[9, 5] = "ẳ";
            FontConverter.Code[10, 5] = "ẵ";
            FontConverter.Code[11, 5] = "ặ";
            FontConverter.Code[12, 5] = "â";
            FontConverter.Code[13, 5] = "ấ";
            FontConverter.Code[14, 5] = "ầ";
            FontConverter.Code[15, 5] = "ẩ";
            FontConverter.Code[16, 5] = "ẫ";
            FontConverter.Code[17, 5] = "ậ";
            FontConverter.Code[18, 5] = "é";
            FontConverter.Code[19, 5] = "è";
            FontConverter.Code[20, 5] = "ẻ";
            FontConverter.Code[21, 5] = "ẽ";
            FontConverter.Code[22, 5] = "ẹ";
            FontConverter.Code[23, 5] = "ê";
            FontConverter.Code[24, 5] = "ế";
            FontConverter.Code[25, 5] = "ề";
            FontConverter.Code[26, 5] = "ể";
            FontConverter.Code[27, 5] = "ễ";
            FontConverter.Code[28, 5] = "ệ";
            FontConverter.Code[29, 5] = "í";
            FontConverter.Code[30, 5] = "ì";
            FontConverter.Code[31, 5] = "ỉ";
            FontConverter.Code[32, 5] = "ĩ";
            FontConverter.Code[33, 5] = "ị";
            FontConverter.Code[34, 5] = "ó";
            FontConverter.Code[35, 5] = "ò";
            FontConverter.Code[36, 5] = "ỏ";
            FontConverter.Code[37, 5] = "õ";
            FontConverter.Code[38, 5] = "ọ";
            FontConverter.Code[39, 5] = "ô";
            FontConverter.Code[40, 5] = "ố";
            FontConverter.Code[41, 5] = "ồ";
            FontConverter.Code[42, 5] = "ổ";
            FontConverter.Code[43, 5] = "ỗ";
            FontConverter.Code[44, 5] = "ộ";
            FontConverter.Code[45, 5] = "ơ";
            FontConverter.Code[46, 5] = "ớ";
            FontConverter.Code[47, 5] = "ờ";
            FontConverter.Code[48, 5] = "ở";
            FontConverter.Code[49, 5] = "ỡ";
            FontConverter.Code[50, 5] = "ợ";
            FontConverter.Code[51, 5] = "ú";
            FontConverter.Code[52, 5] = "ù";
            FontConverter.Code[53, 5] = "ủ";
            FontConverter.Code[54, 5] = "ũ";
            FontConverter.Code[55, 5] = "ụ";
            FontConverter.Code[56, 5] = "ư";
            FontConverter.Code[57, 5] = "ứ";
            FontConverter.Code[58, 5] = "ừ";
            FontConverter.Code[59, 5] = "ử";
            FontConverter.Code[60, 5] = "ữ";
            FontConverter.Code[61, 5] = "ự";
            FontConverter.Code[62, 5] = "ý";
            FontConverter.Code[63, 5] = "ỳ";
            FontConverter.Code[64, 5] = "ỷ";
            FontConverter.Code[65, 5] = "ỹ";
            FontConverter.Code[66, 5] = "ỵ";
            FontConverter.Code[67, 5] = "đ";
            FontConverter.Code[68, 5] = "Á";
            FontConverter.Code[69, 5] = "À";
            FontConverter.Code[70, 5] = "Ả";
            FontConverter.Code[71, 5] = "Ã";
            FontConverter.Code[72, 5] = "Ạ";
            FontConverter.Code[73, 5] = "Ă";
            FontConverter.Code[74, 5] = "Ắ";
            FontConverter.Code[75, 5] = "Ằ";
            FontConverter.Code[76, 5] = "Ẳ";
            FontConverter.Code[77, 5] = "Ẵ";
            FontConverter.Code[78, 5] = "Ặ";
            FontConverter.Code[79, 5] = "Â";
            FontConverter.Code[80, 5] = "Ấ";
            FontConverter.Code[81, 5] = "Ầ";
            FontConverter.Code[82, 5] = "Ẩ";
            FontConverter.Code[83, 5] = "Ẫ";
            FontConverter.Code[84, 5] = "Ậ";
            FontConverter.Code[85, 5] = "É";
            FontConverter.Code[86, 5] = "È";
            FontConverter.Code[87, 5] = "Ẻ";
            FontConverter.Code[88, 5] = "Ẽ";
            FontConverter.Code[89, 5] = "Ẹ";
            FontConverter.Code[90, 5] = "Ê";
            FontConverter.Code[91, 5] = "Ế";
            FontConverter.Code[92, 5] = "Ề";
            FontConverter.Code[93, 5] = "Ể";
            FontConverter.Code[94, 5] = "Ễ";
            FontConverter.Code[95, 5] = "Ệ";
            FontConverter.Code[96, 5] = "Í";
            FontConverter.Code[97, 5] = "Ì";
            FontConverter.Code[98, 5] = "Ỉ";
            FontConverter.Code[99, 5] = "Ĩ";
            FontConverter.Code[100, 5] = "Ị";
            FontConverter.Code[101, 5] = "Ó";
            FontConverter.Code[102, 5] = "Ò";
            FontConverter.Code[103, 5] = "Ỏ";
            FontConverter.Code[104, 5] = "Õ";
            FontConverter.Code[105, 5] = "Ọ";
            FontConverter.Code[106, 5] = "Ô";
            FontConverter.Code[107, 5] = "Ố";
            FontConverter.Code[108, 5] = "Ồ";
            FontConverter.Code[109, 5] = "Ổ";
            FontConverter.Code[110, 5] = "Ỗ";
            FontConverter.Code[111, 5] = "Ộ";
            FontConverter.Code[112, 5] = "Ơ";
            FontConverter.Code[113, 5] = "Ớ";
            FontConverter.Code[114, 5] = "Ờ";
            FontConverter.Code[115, 5] = "Ở";
            FontConverter.Code[116, 5] = "Ỡ";
            FontConverter.Code[117, 5] = "Ợ";
            FontConverter.Code[118, 5] = "Ú";
            FontConverter.Code[119, 5] = "Ù";
            FontConverter.Code[120, 5] = "Ủ";
            FontConverter.Code[121, 5] = "Ũ";
            FontConverter.Code[122, 5] = "Ụ";
            FontConverter.Code[123, 5] = "Ư";
            FontConverter.Code[124, 5] = "Ứ";
            FontConverter.Code[125, 5] = "Ừ";
            FontConverter.Code[126, 5] = "Ử";
            FontConverter.Code[(int)sbyte.MaxValue, 5] = "Ữ";
            FontConverter.Code[128, 5] = "Ự";
            FontConverter.Code[129, 5] = "Ý";
            FontConverter.Code[130, 5] = "Ỳ";
            FontConverter.Code[131, 5] = "Ỷ";
            FontConverter.Code[132, 5] = "Ỹ";
            FontConverter.Code[133, 5] = "Ỵ";
            FontConverter.Code[134, 5] = "Đ";
        }

        private static void MapUTF8()
        {
            FontConverter.Code[1, 1] = "Ã¡";
            FontConverter.Code[2, 1] = "Ã ";
            FontConverter.Code[3, 1] = "áº£";
            FontConverter.Code[4, 1] = "Ã£";
            FontConverter.Code[5, 1] = "áº¡";
            FontConverter.Code[6, 1] = "Äƒ";
            FontConverter.Code[7, 1] = "áº¯";
            FontConverter.Code[8, 1] = "áº±";
            FontConverter.Code[9, 1] = "áº\x00B3";
            FontConverter.Code[10, 1] = "áºµ";
            FontConverter.Code[11, 1] = "áº·";
            FontConverter.Code[12, 1] = "Ã¢";
            FontConverter.Code[13, 1] = "áº¥";
            FontConverter.Code[14, 1] = "áº§";
            FontConverter.Code[15, 1] = "áº©";
            FontConverter.Code[16, 1] = "áº«";
            FontConverter.Code[17, 1] = "áº\x00AD";
            FontConverter.Code[18, 1] = "Ã©";
            FontConverter.Code[19, 1] = "Ã¨";
            FontConverter.Code[20, 1] = "áº»";
            FontConverter.Code[21, 1] = "áº\x00BD";
            FontConverter.Code[22, 1] = "áº\x00B9";
            FontConverter.Code[23, 1] = "Ãª";
            FontConverter.Code[24, 1] = "áº¿";
            FontConverter.Code[25, 1] = "á»\x0081";
            FontConverter.Code[26, 1] = "á»ƒ";
            FontConverter.Code[27, 1] = "á»…";
            FontConverter.Code[28, 1] = "á»‡";
            FontConverter.Code[29, 1] = "Ã\x00AD";
            FontConverter.Code[30, 1] = "Ã¬";
            FontConverter.Code[31, 1] = "á»‰";
            FontConverter.Code[32, 1] = "Ä©";
            FontConverter.Code[33, 1] = "á»‹";
            FontConverter.Code[34, 1] = "Ã\x00B3";
            FontConverter.Code[35, 1] = "Ã\x00B2";
            FontConverter.Code[36, 1] = "á»\x008F";
            FontConverter.Code[37, 1] = "Ãµ";
            FontConverter.Code[38, 1] = "á»\x008D";
            FontConverter.Code[39, 1] = "Ã´";
            FontConverter.Code[40, 1] = "á»‘";
            FontConverter.Code[41, 1] = "á»“";
            FontConverter.Code[42, 1] = "á»•";
            FontConverter.Code[43, 1] = "á»—";
            FontConverter.Code[44, 1] = "á»™";
            FontConverter.Code[45, 1] = "Æ¡";
            FontConverter.Code[46, 1] = "á»›";
            FontConverter.Code[47, 1] = "á»\x009D";
            FontConverter.Code[48, 1] = "á»Ÿ";
            FontConverter.Code[49, 1] = "á»¡";
            FontConverter.Code[50, 1] = "á»£";
            FontConverter.Code[51, 1] = "Ãº";
            FontConverter.Code[52, 1] = "Ã\x00B9";
            FontConverter.Code[53, 1] = "á»§";
            FontConverter.Code[54, 1] = "Å©";
            FontConverter.Code[55, 1] = "á»¥";
            FontConverter.Code[56, 1] = "Æ°";
            FontConverter.Code[57, 1] = "á»©";
            FontConverter.Code[58, 1] = "á»«";
            FontConverter.Code[59, 1] = "á»\x00AD";
            FontConverter.Code[60, 1] = "á»¯";
            FontConverter.Code[61, 1] = "á»±";
            FontConverter.Code[62, 1] = "Ã\x00BD";
            FontConverter.Code[63, 1] = "á»\x00B3";
            FontConverter.Code[64, 1] = "\x009Dá»·".Substring(1);
            FontConverter.Code[65, 1] = "á»\x00B9";
            FontConverter.Code[66, 1] = "á»µ";
            FontConverter.Code[67, 1] = "Ä‘";
            FontConverter.Code[68, 1] = "Ã\x0081";
            FontConverter.Code[69, 1] = "Ã€";
            FontConverter.Code[70, 1] = "áº¢";
            FontConverter.Code[71, 1] = "Ãƒ";
            FontConverter.Code[72, 1] = "áº ";
            FontConverter.Code[73, 1] = "Ä‚";
            FontConverter.Code[74, 1] = "áº®";
            FontConverter.Code[75, 1] = "áº°";
            FontConverter.Code[76, 1] = "áº\x00B2";
            FontConverter.Code[77, 1] = "áº´";
            FontConverter.Code[78, 1] = "áº¶";
            FontConverter.Code[79, 1] = "Ã‚";
            FontConverter.Code[80, 1] = "áº¤";
            FontConverter.Code[81, 1] = "áº¦";
            FontConverter.Code[82, 1] = "áº¨";
            FontConverter.Code[83, 1] = "áºª";
            FontConverter.Code[84, 1] = "áº¬";
            FontConverter.Code[85, 1] = "Ã‰";
            FontConverter.Code[86, 1] = "Ã\x02C6";
            FontConverter.Code[87, 1] = "áºº";
            FontConverter.Code[88, 1] = "áº\x00BC";
            FontConverter.Code[89, 1] = "áº¸";
            FontConverter.Code[90, 1] = "ÃŠ";
            FontConverter.Code[91, 1] = "áº\x00BE";
            FontConverter.Code[92, 1] = "á»€";
            FontConverter.Code[93, 1] = "á»‚";
            FontConverter.Code[94, 1] = "á»„";
            FontConverter.Code[95, 1] = "á»†";
            FontConverter.Code[96, 1] = "Ã\x008D";
            FontConverter.Code[97, 1] = "ÃŒ";
            FontConverter.Code[98, 1] = "á»\x02C6";
            FontConverter.Code[99, 1] = "Ä¨";
            FontConverter.Code[100, 1] = "á»Š";
            FontConverter.Code[101, 1] = "Ã“";
            FontConverter.Code[102, 1] = "Ã’";
            FontConverter.Code[103, 1] = "á»Ž";
            FontConverter.Code[104, 1] = "Ã•";
            FontConverter.Code[105, 1] = "á»Œ";
            FontConverter.Code[106, 1] = "Ã”";
            FontConverter.Code[107, 1] = "á»\x0090";
            FontConverter.Code[108, 1] = "á»’";
            FontConverter.Code[109, 1] = "á»”";
            FontConverter.Code[110, 1] = "á»–";
            FontConverter.Code[111, 1] = "á»˜";
            FontConverter.Code[112, 1] = "Æ ";
            FontConverter.Code[113, 1] = "á»š";
            FontConverter.Code[114, 1] = "á»œ";
            FontConverter.Code[115, 1] = "á»ž";
            FontConverter.Code[116, 1] = "á» ";
            FontConverter.Code[117, 1] = "á»¢";
            FontConverter.Code[118, 1] = "Ãš";
            FontConverter.Code[119, 1] = "Ã™";
            FontConverter.Code[120, 1] = "á»¦";
            FontConverter.Code[121, 1] = "Å¨";
            FontConverter.Code[122, 1] = "á»¤";
            FontConverter.Code[123, 1] = "Æ¯";
            FontConverter.Code[124, 1] = "á»¨";
            FontConverter.Code[125, 1] = "á»ª";
            FontConverter.Code[126, 1] = "á»¬";
            FontConverter.Code[(int)sbyte.MaxValue, 1] = "á»®";
            FontConverter.Code[128, 1] = "á»°";
            FontConverter.Code[129, 1] = "Ã\x009D";
            FontConverter.Code[130, 1] = "á»\x00B2";
            FontConverter.Code[131, 1] = "á»¶";
            FontConverter.Code[132, 1] = "á»¸";
            FontConverter.Code[133, 1] = "á»´";
            FontConverter.Code[134, 1] = "Ä\x0090";
        }

        private static void MapVIQR()
        {
            FontConverter.Code[1, 8] = "a'";
            FontConverter.Code[2, 8] = "a`";
            FontConverter.Code[3, 8] = "a?";
            FontConverter.Code[4, 8] = "a~";
            FontConverter.Code[5, 8] = "a.";
            FontConverter.Code[6, 8] = "a(";
            FontConverter.Code[7, 8] = "a('";
            FontConverter.Code[8, 8] = "a(`";
            FontConverter.Code[9, 8] = "a(?";
            FontConverter.Code[10, 8] = "a(~";
            FontConverter.Code[11, 8] = "a(.";
            FontConverter.Code[12, 8] = "a^";
            FontConverter.Code[13, 8] = "a^'";
            FontConverter.Code[14, 8] = "a^`";
            FontConverter.Code[15, 8] = "a^?";
            FontConverter.Code[16, 8] = "a^~";
            FontConverter.Code[17, 8] = "a^.";
            FontConverter.Code[18, 8] = "e'";
            FontConverter.Code[19, 8] = "e`";
            FontConverter.Code[20, 8] = "e?";
            FontConverter.Code[21, 8] = "e~";
            FontConverter.Code[22, 8] = "e.";
            FontConverter.Code[23, 8] = "e^";
            FontConverter.Code[24, 8] = "e^'";
            FontConverter.Code[25, 8] = "e^`";
            FontConverter.Code[26, 8] = "e^?";
            FontConverter.Code[27, 8] = "e^~";
            FontConverter.Code[28, 8] = "e^.";
            FontConverter.Code[29, 8] = "i'";
            FontConverter.Code[30, 8] = "i`";
            FontConverter.Code[31, 8] = "i?";
            FontConverter.Code[32, 8] = "i~";
            FontConverter.Code[33, 8] = "i.";
            FontConverter.Code[34, 8] = "o'";
            FontConverter.Code[35, 8] = "o`";
            FontConverter.Code[36, 8] = "o?";
            FontConverter.Code[37, 8] = "o~";
            FontConverter.Code[38, 8] = "o.";
            FontConverter.Code[39, 8] = "o^";
            FontConverter.Code[40, 8] = "o^'";
            FontConverter.Code[41, 8] = "o^`";
            FontConverter.Code[42, 8] = "o^?";
            FontConverter.Code[43, 8] = "o^~";
            FontConverter.Code[44, 8] = "o^.";
            FontConverter.Code[45, 8] = "o+";
            FontConverter.Code[46, 8] = "o+'";
            FontConverter.Code[47, 8] = "o+`";
            FontConverter.Code[48, 8] = "o+?";
            FontConverter.Code[49, 8] = "o+~";
            FontConverter.Code[50, 8] = "o+.";
            FontConverter.Code[51, 8] = "u'";
            FontConverter.Code[52, 8] = "u`";
            FontConverter.Code[53, 8] = "u?";
            FontConverter.Code[54, 8] = "u~";
            FontConverter.Code[55, 8] = "u.";
            FontConverter.Code[56, 8] = "u+";
            FontConverter.Code[57, 8] = "u+'";
            FontConverter.Code[58, 8] = "u+`";
            FontConverter.Code[59, 8] = "u+?";
            FontConverter.Code[60, 8] = "u+~";
            FontConverter.Code[61, 8] = "u+.";
            FontConverter.Code[62, 8] = "y'";
            FontConverter.Code[63, 8] = "y`";
            FontConverter.Code[64, 8] = "y?";
            FontConverter.Code[65, 8] = "y~";
            FontConverter.Code[66, 8] = "y.";
            FontConverter.Code[67, 8] = "dd";
            FontConverter.Code[68, 8] = "A'";
            FontConverter.Code[69, 8] = "A`";
            FontConverter.Code[70, 8] = "A?";
            FontConverter.Code[71, 8] = "A~";
            FontConverter.Code[72, 8] = "A.";
            FontConverter.Code[73, 8] = "A(";
            FontConverter.Code[74, 8] = "A('";
            FontConverter.Code[75, 8] = "A(`";
            FontConverter.Code[76, 8] = "A(?";
            FontConverter.Code[77, 8] = "A(~";
            FontConverter.Code[78, 8] = "A(.";
            FontConverter.Code[79, 8] = "A^";
            FontConverter.Code[80, 8] = "A^'";
            FontConverter.Code[81, 8] = "A^`";
            FontConverter.Code[82, 8] = "A^?";
            FontConverter.Code[83, 8] = "A^~";
            FontConverter.Code[84, 8] = "A^.";
            FontConverter.Code[85, 8] = "E'";
            FontConverter.Code[86, 8] = "E`";
            FontConverter.Code[87, 8] = "E?";
            FontConverter.Code[88, 8] = "E~";
            FontConverter.Code[89, 8] = "E.";
            FontConverter.Code[90, 8] = "E^";
            FontConverter.Code[91, 8] = "E^'";
            FontConverter.Code[92, 8] = "E^`";
            FontConverter.Code[93, 8] = "E^?";
            FontConverter.Code[94, 8] = "E^~";
            FontConverter.Code[95, 8] = "E^.";
            FontConverter.Code[96, 8] = "I'";
            FontConverter.Code[97, 8] = "I`";
            FontConverter.Code[98, 8] = "I?";
            FontConverter.Code[99, 8] = "I~";
            FontConverter.Code[100, 8] = "I.";
            FontConverter.Code[101, 8] = "O'";
            FontConverter.Code[102, 8] = "O`";
            FontConverter.Code[103, 8] = "O?";
            FontConverter.Code[104, 8] = "O~";
            FontConverter.Code[105, 8] = "O.";
            FontConverter.Code[106, 8] = "O^";
            FontConverter.Code[107, 8] = "O^'";
            FontConverter.Code[108, 8] = "O^`";
            FontConverter.Code[109, 8] = "O^?";
            FontConverter.Code[110, 8] = "O^~";
            FontConverter.Code[111, 8] = "O^.";
            FontConverter.Code[112, 8] = "O+";
            FontConverter.Code[113, 8] = "O+'";
            FontConverter.Code[114, 8] = "O+`";
            FontConverter.Code[115, 8] = "O+?";
            FontConverter.Code[116, 8] = "O+~";
            FontConverter.Code[117, 8] = "O+.";
            FontConverter.Code[118, 8] = "U'";
            FontConverter.Code[119, 8] = "U`";
            FontConverter.Code[120, 8] = "U?";
            FontConverter.Code[121, 8] = "U~";
            FontConverter.Code[122, 8] = "U.";
            FontConverter.Code[123, 8] = "U+";
            FontConverter.Code[124, 8] = "U+'";
            FontConverter.Code[125, 8] = "U+`";
            FontConverter.Code[126, 8] = "U+?";
            FontConverter.Code[(int)sbyte.MaxValue, 8] = "U+~";
            FontConverter.Code[128, 8] = "U+.";
            FontConverter.Code[129, 8] = "Y'";
            FontConverter.Code[130, 8] = "Y`";
            FontConverter.Code[131, 8] = "Y?";
            FontConverter.Code[132, 8] = "Y~";
            FontConverter.Code[133, 8] = "Y.";
            FontConverter.Code[134, 8] = "DD";
        }

        private static void MapNCR()
        {
            FontConverter.Code[1, 0] = "&#225;";
            FontConverter.Code[2, 0] = "&#224;";
            FontConverter.Code[3, 0] = "&#7843;";
            FontConverter.Code[4, 0] = "&#227;";
            FontConverter.Code[5, 0] = "&#7841;";
            FontConverter.Code[6, 0] = "&#259;";
            FontConverter.Code[7, 0] = "&#7855;";
            FontConverter.Code[8, 0] = "&#7857;";
            FontConverter.Code[9, 0] = "&#7859;";
            FontConverter.Code[10, 0] = "&#7861;";
            FontConverter.Code[11, 0] = "&#7863;";
            FontConverter.Code[12, 0] = "&#226;";
            FontConverter.Code[13, 0] = "&#7845;";
            FontConverter.Code[14, 0] = "&#7847;";
            FontConverter.Code[15, 0] = "&#7849;";
            FontConverter.Code[16, 0] = "&#7851;";
            FontConverter.Code[17, 0] = "&#7853;";
            FontConverter.Code[18, 0] = "&#233;";
            FontConverter.Code[19, 0] = "&#232;";
            FontConverter.Code[20, 0] = "&#7867;";
            FontConverter.Code[21, 0] = "&#7869;";
            FontConverter.Code[22, 0] = "&#7865;";
            FontConverter.Code[23, 0] = "&#234;";
            FontConverter.Code[24, 0] = "&#7871;";
            FontConverter.Code[25, 0] = "&#7873;";
            FontConverter.Code[26, 0] = "&#7875;";
            FontConverter.Code[27, 0] = "&#7877;";
            FontConverter.Code[28, 0] = "&#7879;";
            FontConverter.Code[29, 0] = "&#237;";
            FontConverter.Code[30, 0] = "&#236;";
            FontConverter.Code[31, 0] = "&#7881;";
            FontConverter.Code[32, 0] = "&#297;";
            FontConverter.Code[33, 0] = "&#7883;";
            FontConverter.Code[34, 0] = "&#243;";
            FontConverter.Code[35, 0] = "&#242;";
            FontConverter.Code[36, 0] = "&#7887;";
            FontConverter.Code[37, 0] = "&#245;";
            FontConverter.Code[38, 0] = "&#7885;";
            FontConverter.Code[39, 0] = "&#244;";
            FontConverter.Code[40, 0] = "&#7889;";
            FontConverter.Code[41, 0] = "&#7891;";
            FontConverter.Code[42, 0] = "&#7893;";
            FontConverter.Code[43, 0] = "&#7895;";
            FontConverter.Code[44, 0] = "&#7897;";
            FontConverter.Code[45, 0] = "&#417;";
            FontConverter.Code[46, 0] = "&#7899;";
            FontConverter.Code[47, 0] = "&#7901;";
            FontConverter.Code[48, 0] = "&#7903;";
            FontConverter.Code[49, 0] = "&#7905;";
            FontConverter.Code[50, 0] = "&#7907;";
            FontConverter.Code[51, 0] = "&#250;";
            FontConverter.Code[52, 0] = "&#249;";
            FontConverter.Code[53, 0] = "&#7911;";
            FontConverter.Code[54, 0] = "&#361;";
            FontConverter.Code[55, 0] = "&#7909;";
            FontConverter.Code[56, 0] = "&#432;";
            FontConverter.Code[57, 0] = "&#7913;";
            FontConverter.Code[58, 0] = "&#7915;";
            FontConverter.Code[59, 0] = "&#7917;";
            FontConverter.Code[60, 0] = "&#7919;";
            FontConverter.Code[61, 0] = "&#7921;";
            FontConverter.Code[62, 0] = "&#253;";
            FontConverter.Code[63, 0] = "&#7923;";
            FontConverter.Code[64, 0] = "&#7927;";
            FontConverter.Code[65, 0] = "&#7929;";
            FontConverter.Code[66, 0] = "&#7925;";
            FontConverter.Code[67, 0] = "&#273;";
            FontConverter.Code[68, 0] = "&#193;";
            FontConverter.Code[69, 0] = "&#192;";
            FontConverter.Code[70, 0] = "&#7842;";
            FontConverter.Code[71, 0] = "&#195;";
            FontConverter.Code[72, 0] = "&#7840;";
            FontConverter.Code[73, 0] = "&#258;";
            FontConverter.Code[74, 0] = "&#7854;";
            FontConverter.Code[75, 0] = "&#7856;";
            FontConverter.Code[76, 0] = "&#7858;";
            FontConverter.Code[77, 0] = "&#7860;";
            FontConverter.Code[78, 0] = "&#7862;";
            FontConverter.Code[79, 0] = "&#194;";
            FontConverter.Code[80, 0] = "&#7844;";
            FontConverter.Code[81, 0] = "&#7846;";
            FontConverter.Code[82, 0] = "&#7848;";
            FontConverter.Code[83, 0] = "&#7850;";
            FontConverter.Code[84, 0] = "&#7852;";
            FontConverter.Code[85, 0] = "&#201;";
            FontConverter.Code[86, 0] = "&#200;";
            FontConverter.Code[87, 0] = "&#7866;";
            FontConverter.Code[88, 0] = "&#7868;";
            FontConverter.Code[89, 0] = "&#7864;";
            FontConverter.Code[90, 0] = "&#202;";
            FontConverter.Code[91, 0] = "&#7870;";
            FontConverter.Code[92, 0] = "&#7872;";
            FontConverter.Code[93, 0] = "&#7874;";
            FontConverter.Code[94, 0] = "&#7876;";
            FontConverter.Code[95, 0] = "&#7878;";
            FontConverter.Code[96, 0] = "&#205;";
            FontConverter.Code[97, 0] = "&#204;";
            FontConverter.Code[98, 0] = "&#7880;";
            FontConverter.Code[99, 0] = "&#296;";
            FontConverter.Code[100, 0] = "&#7882;";
            FontConverter.Code[101, 0] = "&#211;";
            FontConverter.Code[102, 0] = "&#210;";
            FontConverter.Code[103, 0] = "&#7886;";
            FontConverter.Code[104, 0] = "&#213;";
            FontConverter.Code[105, 0] = "&#7884;";
            FontConverter.Code[106, 0] = "&#212;";
            FontConverter.Code[107, 0] = "&#7888;";
            FontConverter.Code[108, 0] = "&#7890;";
            FontConverter.Code[109, 0] = "&#7892;";
            FontConverter.Code[110, 0] = "&#7894;";
            FontConverter.Code[111, 0] = "&#7896;";
            FontConverter.Code[112, 0] = "&#416;";
            FontConverter.Code[113, 0] = "&#7898;";
            FontConverter.Code[114, 0] = "&#7900;";
            FontConverter.Code[115, 0] = "&#7902;";
            FontConverter.Code[116, 0] = "&#7904;";
            FontConverter.Code[117, 0] = "&#7906;";
            FontConverter.Code[118, 0] = "&#218;";
            FontConverter.Code[119, 0] = "&#217;";
            FontConverter.Code[120, 0] = "&#7910;";
            FontConverter.Code[121, 0] = "&#360;";
            FontConverter.Code[122, 0] = "&#7908;";
            FontConverter.Code[123, 0] = "&#431;";
            FontConverter.Code[124, 0] = "&#7912;";
            FontConverter.Code[125, 0] = "&#7914;";
            FontConverter.Code[126, 0] = "&#7916;";
            FontConverter.Code[(int)sbyte.MaxValue, 0] = "&#7918;";
            FontConverter.Code[128, 0] = "&#7920;";
            FontConverter.Code[129, 0] = "&#221;";
            FontConverter.Code[130, 0] = "&#7922;";
            FontConverter.Code[131, 0] = "&#7926;";
            FontConverter.Code[132, 0] = "&#7928;";
            FontConverter.Code[133, 0] = "&#7924;";
            FontConverter.Code[134, 0] = "&#272;";
        }

        private static void MapCP1258()
        {
            FontConverter.Code[1, 4] = "aì";
            FontConverter.Code[2, 4] = "aÌ";
            FontConverter.Code[3, 4] = "aÒ";
            FontConverter.Code[4, 4] = "aÞ";
            FontConverter.Code[5, 4] = "aò";
            FontConverter.Code[6, 4] = "ã";
            FontConverter.Code[7, 4] = "ãì";
            FontConverter.Code[8, 4] = "ãÌ";
            FontConverter.Code[9, 4] = "ãÒ";
            FontConverter.Code[10, 4] = "ãÞ";
            FontConverter.Code[11, 4] = "ãò";
            FontConverter.Code[12, 4] = "â";
            FontConverter.Code[13, 4] = "âì";
            FontConverter.Code[14, 4] = "âÌ";
            FontConverter.Code[15, 4] = "âÒ";
            FontConverter.Code[16, 4] = "âÞ";
            FontConverter.Code[17, 4] = "âò";
            FontConverter.Code[18, 4] = "eì";
            FontConverter.Code[19, 4] = "eÌ";
            FontConverter.Code[20, 4] = "eÒ";
            FontConverter.Code[21, 4] = "eÞ";
            FontConverter.Code[22, 4] = "eò";
            FontConverter.Code[23, 4] = "ê";
            FontConverter.Code[24, 4] = "êì";
            FontConverter.Code[25, 4] = "êÌ";
            FontConverter.Code[26, 4] = "êÒ";
            FontConverter.Code[27, 4] = "êÞ";
            FontConverter.Code[28, 4] = "êò";
            FontConverter.Code[29, 4] = "iì";
            FontConverter.Code[30, 4] = "iÌ";
            FontConverter.Code[31, 4] = "iÒ";
            FontConverter.Code[32, 4] = "iÞ";
            FontConverter.Code[33, 4] = "iò";
            FontConverter.Code[34, 4] = "oì";
            FontConverter.Code[35, 4] = "oÌ";
            FontConverter.Code[36, 4] = "oÒ";
            FontConverter.Code[37, 4] = "oÞ";
            FontConverter.Code[38, 4] = "oò";
            FontConverter.Code[39, 4] = "ô";
            FontConverter.Code[40, 4] = "ôì";
            FontConverter.Code[41, 4] = "ôÌ";
            FontConverter.Code[42, 4] = "ôÒ";
            FontConverter.Code[43, 4] = "ôÞ";
            FontConverter.Code[44, 4] = "ôò";
            FontConverter.Code[45, 4] = "õ";
            FontConverter.Code[46, 4] = "õì";
            FontConverter.Code[47, 4] = "õÌ";
            FontConverter.Code[48, 4] = "õÒ";
            FontConverter.Code[49, 4] = "õÞ";
            FontConverter.Code[50, 4] = "õò";
            FontConverter.Code[51, 4] = "uì";
            FontConverter.Code[52, 4] = "uÌ";
            FontConverter.Code[53, 4] = "uÒ";
            FontConverter.Code[54, 4] = "uÞ";
            FontConverter.Code[55, 4] = "uò";
            FontConverter.Code[56, 4] = "ý";
            FontConverter.Code[57, 4] = "ýì";
            FontConverter.Code[58, 4] = "ýÌ";
            FontConverter.Code[59, 4] = "ýÒ";
            FontConverter.Code[60, 4] = "ýÞ";
            FontConverter.Code[61, 4] = "ýò";
            FontConverter.Code[62, 4] = "yì";
            FontConverter.Code[63, 4] = "yÌ";
            FontConverter.Code[64, 4] = "yÒ";
            FontConverter.Code[65, 4] = "yÞ";
            FontConverter.Code[66, 4] = "yò";
            FontConverter.Code[67, 4] = "ð";
            FontConverter.Code[68, 4] = "Aì";
            FontConverter.Code[69, 4] = "AÌ";
            FontConverter.Code[70, 4] = "AÒ";
            FontConverter.Code[71, 4] = "AÞ";
            FontConverter.Code[72, 4] = "Aò";
            FontConverter.Code[73, 4] = "Ã";
            FontConverter.Code[74, 4] = "Ãì";
            FontConverter.Code[75, 4] = "ÃÌ";
            FontConverter.Code[76, 4] = "ÃÒ";
            FontConverter.Code[77, 4] = "ÃÞ";
            FontConverter.Code[78, 4] = "Ãò";
            FontConverter.Code[79, 4] = "Â";
            FontConverter.Code[80, 4] = "Âì";
            FontConverter.Code[81, 4] = "ÂÌ";
            FontConverter.Code[82, 4] = "ÂÒ";
            FontConverter.Code[83, 4] = "ÂÞ";
            FontConverter.Code[84, 4] = "Âò";
            FontConverter.Code[85, 4] = "Eì";
            FontConverter.Code[86, 4] = "EÌ";
            FontConverter.Code[87, 4] = "EÒ";
            FontConverter.Code[88, 4] = "EÞ";
            FontConverter.Code[89, 4] = "Eò";
            FontConverter.Code[90, 4] = "Ê";
            FontConverter.Code[91, 4] = "Êì";
            FontConverter.Code[92, 4] = "ÊÌ";
            FontConverter.Code[93, 4] = "ÊÒ";
            FontConverter.Code[94, 4] = "ÊÞ";
            FontConverter.Code[95, 4] = "Êò";
            FontConverter.Code[96, 4] = "Iì";
            FontConverter.Code[97, 4] = "IÌ";
            FontConverter.Code[98, 4] = "IÒ";
            FontConverter.Code[99, 4] = "IÞ";
            FontConverter.Code[100, 4] = "Iò";
            FontConverter.Code[101, 4] = "Oì";
            FontConverter.Code[102, 4] = "OÌ";
            FontConverter.Code[103, 4] = "OÒ";
            FontConverter.Code[104, 4] = "OÞ";
            FontConverter.Code[105, 4] = "Oò";
            FontConverter.Code[106, 4] = "Ô";
            FontConverter.Code[107, 4] = "Ôì";
            FontConverter.Code[108, 4] = "ÔÌ";
            FontConverter.Code[109, 4] = "ÔÒ";
            FontConverter.Code[110, 4] = "ÔÞ";
            FontConverter.Code[111, 4] = "Ôò";
            FontConverter.Code[112, 4] = "Õ";
            FontConverter.Code[113, 4] = "Õì";
            FontConverter.Code[114, 4] = "ÕÌ";
            FontConverter.Code[115, 4] = "ÕÒ";
            FontConverter.Code[116, 4] = "ÕÞ";
            FontConverter.Code[117, 4] = "Õò";
            FontConverter.Code[118, 4] = "Uì";
            FontConverter.Code[119, 4] = "UÌ";
            FontConverter.Code[120, 4] = "UÒ";
            FontConverter.Code[121, 4] = "UÞ";
            FontConverter.Code[122, 4] = "Uò";
            FontConverter.Code[123, 4] = "Ý";
            FontConverter.Code[124, 4] = "Ýì";
            FontConverter.Code[125, 4] = "ÝÌ";
            FontConverter.Code[126, 4] = "ÝÒ";
            FontConverter.Code[(int)sbyte.MaxValue, 4] = "ÝÞ";
            FontConverter.Code[128, 4] = "Ýò";
            FontConverter.Code[129, 4] = "Yì";
            FontConverter.Code[130, 4] = "YÌ";
            FontConverter.Code[131, 4] = "YÒ";
            FontConverter.Code[132, 4] = "YÞ";
            FontConverter.Code[133, 4] = "Yò";
            FontConverter.Code[134, 4] = "Ð";
        }

        private static void MapVISCII()
        {
            FontConverter.Code[1, 6] = "á";
            FontConverter.Code[2, 6] = "à";
            FontConverter.Code[3, 6] = "ä";
            FontConverter.Code[4, 6] = "ã";
            FontConverter.Code[5, 6] = "Õ";
            FontConverter.Code[6, 6] = "å";
            FontConverter.Code[7, 6] = "¡";
            FontConverter.Code[8, 6] = "¢";
            FontConverter.Code[9, 6] = "Æ";
            FontConverter.Code[10, 6] = "Ç";
            FontConverter.Code[11, 6] = "£";
            FontConverter.Code[12, 6] = "â";
            FontConverter.Code[13, 6] = "¤";
            FontConverter.Code[14, 6] = "¥";
            FontConverter.Code[15, 6] = "¦";
            FontConverter.Code[16, 6] = "ç";
            FontConverter.Code[17, 6] = "§";
            FontConverter.Code[18, 6] = "é";
            FontConverter.Code[19, 6] = "è";
            FontConverter.Code[20, 6] = "ë";
            FontConverter.Code[21, 6] = "¨";
            FontConverter.Code[22, 6] = "©";
            FontConverter.Code[23, 6] = "ê";
            FontConverter.Code[24, 6] = "ª";
            FontConverter.Code[25, 6] = "«";
            FontConverter.Code[26, 6] = "¬";
            FontConverter.Code[27, 6] = "\x00AD";
            FontConverter.Code[28, 6] = "®";
            FontConverter.Code[29, 6] = "í";
            FontConverter.Code[30, 6] = "ì";
            FontConverter.Code[31, 6] = "ï";
            FontConverter.Code[32, 6] = "î";
            FontConverter.Code[33, 6] = "¸";
            FontConverter.Code[34, 6] = "ó";
            FontConverter.Code[35, 6] = "ò";
            FontConverter.Code[36, 6] = "ö";
            FontConverter.Code[37, 6] = "õ";
            FontConverter.Code[38, 6] = "÷";
            FontConverter.Code[39, 6] = "ô";
            FontConverter.Code[40, 6] = "¯";
            FontConverter.Code[41, 6] = "°";
            FontConverter.Code[42, 6] = "±";
            FontConverter.Code[43, 6] = "\x00B2";
            FontConverter.Code[44, 6] = "µ";
            FontConverter.Code[45, 6] = "\x00BD";
            FontConverter.Code[46, 6] = "\x00BE";
            FontConverter.Code[47, 6] = "¶";
            FontConverter.Code[48, 6] = "·";
            FontConverter.Code[49, 6] = "Þ";
            FontConverter.Code[50, 6] = "þ";
            FontConverter.Code[51, 6] = "ú";
            FontConverter.Code[52, 6] = "ù";
            FontConverter.Code[53, 6] = "ü";
            FontConverter.Code[54, 6] = "û";
            FontConverter.Code[55, 6] = "ø";
            FontConverter.Code[56, 6] = "ß";
            FontConverter.Code[57, 6] = "Ñ";
            FontConverter.Code[58, 6] = "×";
            FontConverter.Code[59, 6] = "Ø";
            FontConverter.Code[60, 6] = "æ";
            FontConverter.Code[61, 6] = "ñ";
            FontConverter.Code[62, 6] = "ý";
            FontConverter.Code[63, 6] = "Ï";
            FontConverter.Code[64, 6] = "Ö";
            FontConverter.Code[65, 6] = "Û";
            FontConverter.Code[66, 6] = "Ü";
            FontConverter.Code[67, 6] = "ð";
            FontConverter.Code[68, 6] = "Á";
            FontConverter.Code[69, 6] = "À";
            FontConverter.Code[70, 6] = "Ä";
            FontConverter.Code[71, 6] = "Ã";
            FontConverter.Code[72, 6] = "€";
            FontConverter.Code[73, 6] = "Å";
            FontConverter.Code[74, 6] = "\x0081";
            FontConverter.Code[75, 6] = "‚";
            FontConverter.Code[76, 6] = "Æ";
            FontConverter.Code[77, 6] = "Ç";
            FontConverter.Code[78, 6] = "ƒ";
            FontConverter.Code[79, 6] = "Â";
            FontConverter.Code[80, 6] = "„";
            FontConverter.Code[81, 6] = "…";
            FontConverter.Code[82, 6] = "†";
            FontConverter.Code[83, 6] = "ç";
            FontConverter.Code[84, 6] = "‡";
            FontConverter.Code[85, 6] = "É";
            FontConverter.Code[86, 6] = "È";
            FontConverter.Code[87, 6] = "Ë";
            FontConverter.Code[88, 6] = "\x02C6";
            FontConverter.Code[89, 6] = "‰";
            FontConverter.Code[90, 6] = "Ê";
            FontConverter.Code[91, 6] = "Š";
            FontConverter.Code[92, 6] = "‹";
            FontConverter.Code[93, 6] = "Œ";
            FontConverter.Code[94, 6] = "\x008D";
            FontConverter.Code[95, 6] = "Ž";
            FontConverter.Code[96, 6] = "Í";
            FontConverter.Code[97, 6] = "Ì";
            FontConverter.Code[98, 6] = "›";
            FontConverter.Code[99, 6] = "Î";
            FontConverter.Code[100, 6] = "˜";
            FontConverter.Code[101, 6] = "Ó";
            FontConverter.Code[102, 6] = "Ò";
            FontConverter.Code[103, 6] = "™";
            FontConverter.Code[104, 6] = "õ";
            FontConverter.Code[105, 6] = "š";
            FontConverter.Code[106, 6] = "Ô";
            FontConverter.Code[107, 6] = "\x008F";
            FontConverter.Code[108, 6] = "\x0090";
            FontConverter.Code[109, 6] = "‘";
            FontConverter.Code[110, 6] = "’";
            FontConverter.Code[111, 6] = "“";
            FontConverter.Code[112, 6] = "´";
            FontConverter.Code[113, 6] = "•";
            FontConverter.Code[114, 6] = "–";
            FontConverter.Code[115, 6] = "—";
            FontConverter.Code[116, 6] = "\x00B3";
            FontConverter.Code[117, 6] = "”";
            FontConverter.Code[118, 6] = "Ú";
            FontConverter.Code[119, 6] = "Ù";
            FontConverter.Code[120, 6] = "œ";
            FontConverter.Code[121, 6] = "\x009D";
            FontConverter.Code[122, 6] = "ž";
            FontConverter.Code[123, 6] = "¿";
            FontConverter.Code[124, 6] = "º";
            FontConverter.Code[125, 6] = "»";
            FontConverter.Code[126, 6] = "\x00BC";
            FontConverter.Code[(int)sbyte.MaxValue, 6] = "ÿ";
            FontConverter.Code[128, 6] = "\x00B9";
            FontConverter.Code[129, 6] = "Ý";
            FontConverter.Code[130, 6] = "Ÿ";
            FontConverter.Code[131, 6] = "Ö";
            FontConverter.Code[132, 6] = "Û";
            FontConverter.Code[133, 6] = "Ü";
            FontConverter.Code[134, 6] = "Ð";
        }

        private static void MapVPS()
        {
            FontConverter.Code[1, 6] = "á";
            FontConverter.Code[2, 6] = "à";
            FontConverter.Code[3, 6] = "ä";
            FontConverter.Code[4, 6] = "ã";
            FontConverter.Code[5, 6] = "å";
            FontConverter.Code[6, 6] = "æ";
            FontConverter.Code[7, 6] = "¡";
            FontConverter.Code[8, 6] = "¢";
            FontConverter.Code[9, 6] = "£";
            FontConverter.Code[10, 6] = "¤";
            FontConverter.Code[11, 6] = "¥";
            FontConverter.Code[12, 6] = "â";
            FontConverter.Code[13, 6] = "Ã";
            FontConverter.Code[14, 6] = "À";
            FontConverter.Code[15, 6] = "Ä";
            FontConverter.Code[16, 6] = "Å";
            FontConverter.Code[17, 6] = "Æ";
            FontConverter.Code[18, 6] = "é";
            FontConverter.Code[19, 6] = "è";
            FontConverter.Code[20, 6] = "È";
            FontConverter.Code[21, 6] = "ë";
            FontConverter.Code[22, 6] = "Ë";
            FontConverter.Code[23, 6] = "ê";
            FontConverter.Code[24, 6] = "‰";
            FontConverter.Code[25, 6] = "Š";
            FontConverter.Code[26, 6] = "‹";
            FontConverter.Code[27, 6] = "Í";
            FontConverter.Code[28, 6] = "Œ";
            FontConverter.Code[29, 6] = "í";
            FontConverter.Code[30, 6] = "ì";
            FontConverter.Code[31, 6] = "Ì";
            FontConverter.Code[32, 6] = "ï";
            FontConverter.Code[33, 6] = "Î";
            FontConverter.Code[34, 6] = "ó";
            FontConverter.Code[35, 6] = "ò";
            FontConverter.Code[36, 6] = "Õ";
            FontConverter.Code[37, 6] = "õ";
            FontConverter.Code[38, 6] = "†";
            FontConverter.Code[39, 6] = "ô";
            FontConverter.Code[40, 6] = "Ó";
            FontConverter.Code[41, 6] = "Ò";
            FontConverter.Code[42, 6] = "°";
            FontConverter.Code[43, 6] = "‡";
            FontConverter.Code[44, 6] = "¶";
            FontConverter.Code[45, 6] = "Ö";
            FontConverter.Code[46, 6] = "§";
            FontConverter.Code[47, 6] = "©";
            FontConverter.Code[48, 6] = "ª";
            FontConverter.Code[49, 6] = "«";
            FontConverter.Code[50, 6] = "®";
            FontConverter.Code[51, 6] = "ú";
            FontConverter.Code[52, 6] = "ù";
            FontConverter.Code[53, 6] = "û";
            FontConverter.Code[54, 6] = "Û";
            FontConverter.Code[55, 6] = "ø";
            FontConverter.Code[56, 6] = "Ü";
            FontConverter.Code[57, 6] = "Ù";
            FontConverter.Code[58, 6] = "Ø";
            FontConverter.Code[59, 6] = "º";
            FontConverter.Code[60, 6] = "»";
            FontConverter.Code[61, 6] = "¿";
            FontConverter.Code[62, 6] = "š";
            FontConverter.Code[63, 6] = "ÿ";
            FontConverter.Code[64, 6] = "›";
            FontConverter.Code[65, 6] = "Ï";
            FontConverter.Code[66, 6] = "œ";
            FontConverter.Code[67, 6] = "Ç";
            FontConverter.Code[68, 6] = "Á";
            FontConverter.Code[69, 6] = "€";
            FontConverter.Code[70, 6] = "\x0081";
            FontConverter.Code[71, 6] = "‚";
            FontConverter.Code[72, 6] = "å";
            FontConverter.Code[73, 6] = "\x02C6";
            FontConverter.Code[74, 6] = "\x008D";
            FontConverter.Code[75, 6] = "Ž";
            FontConverter.Code[76, 6] = "\x008F";
            FontConverter.Code[77, 6] = "ð";
            FontConverter.Code[78, 6] = "¥";
            FontConverter.Code[79, 6] = "Â";
            FontConverter.Code[80, 6] = "ƒ";
            FontConverter.Code[81, 6] = "„";
            FontConverter.Code[82, 6] = "…";
            FontConverter.Code[83, 6] = "Å";
            FontConverter.Code[84, 6] = "Æ";
            FontConverter.Code[85, 6] = "É";
            FontConverter.Code[86, 6] = "×";
            FontConverter.Code[87, 6] = "Þ";
            FontConverter.Code[88, 6] = "þ";
            FontConverter.Code[89, 6] = "Ë";
            FontConverter.Code[90, 6] = "Ê";
            FontConverter.Code[91, 6] = "\x0090";
            FontConverter.Code[92, 6] = "“";
            FontConverter.Code[93, 6] = "”";
            FontConverter.Code[94, 6] = "•";
            FontConverter.Code[95, 6] = "Œ";
            FontConverter.Code[96, 6] = "´";
            FontConverter.Code[97, 6] = "µ";
            FontConverter.Code[98, 6] = "·";
            FontConverter.Code[99, 6] = "¸";
            FontConverter.Code[100, 6] = "Î";
            FontConverter.Code[101, 6] = "\x00B9";
            FontConverter.Code[102, 6] = "\x00BC";
            FontConverter.Code[103, 6] = "\x00BD";
            FontConverter.Code[104, 6] = "\x00BE";
            FontConverter.Code[105, 6] = "†";
            FontConverter.Code[106, 6] = "Ô";
            FontConverter.Code[107, 6] = "–";
            FontConverter.Code[108, 6] = "—";
            FontConverter.Code[109, 6] = "˜";
            FontConverter.Code[110, 6] = "™";
            FontConverter.Code[111, 6] = "¶";
            FontConverter.Code[112, 6] = "÷";
            FontConverter.Code[113, 6] = "\x009D";
            FontConverter.Code[114, 6] = "ž";
            FontConverter.Code[115, 6] = "Ÿ";
            FontConverter.Code[116, 6] = "¦";
            FontConverter.Code[117, 6] = "®";
            FontConverter.Code[118, 6] = "Ú";
            FontConverter.Code[119, 6] = "¨";
            FontConverter.Code[120, 6] = "Ñ";
            FontConverter.Code[121, 6] = "¬";
            FontConverter.Code[122, 6] = "ø";
            FontConverter.Code[123, 6] = "Ð";
            FontConverter.Code[124, 6] = "\x00AD";
            FontConverter.Code[125, 6] = "¯";
            FontConverter.Code[126, 6] = "±";
            FontConverter.Code[(int)sbyte.MaxValue, 6] = "»";
            FontConverter.Code[128, 6] = "¿";
            FontConverter.Code[129, 6] = "Ý";
            FontConverter.Code[130, 6] = "\x00B2";
            FontConverter.Code[131, 6] = "ý";
            FontConverter.Code[132, 6] = "\x00B3";
            FontConverter.Code[133, 6] = "œ";
            FontConverter.Code[134, 6] = "ñ";
        }

        private static void MapBKHCM2()
        {
            FontConverter.Code[1, 6] = "aá";
            FontConverter.Code[2, 6] = "aâ";
            FontConverter.Code[3, 6] = "aã";
            FontConverter.Code[4, 6] = "aä";
            FontConverter.Code[5, 6] = "aå";
            FontConverter.Code[6, 6] = "ù";
            FontConverter.Code[7, 6] = "ùæ";
            FontConverter.Code[8, 6] = "ùç";
            FontConverter.Code[9, 6] = "ùè";
            FontConverter.Code[10, 6] = "ùé";
            FontConverter.Code[11, 6] = "ùå";
            FontConverter.Code[12, 6] = "ê";
            FontConverter.Code[13, 6] = "êë";
            FontConverter.Code[14, 6] = "êì";
            FontConverter.Code[15, 6] = "êí";
            FontConverter.Code[16, 6] = "êî";
            FontConverter.Code[17, 6] = "êå";
            FontConverter.Code[18, 6] = "eá";
            FontConverter.Code[19, 6] = "eâ";
            FontConverter.Code[20, 6] = "eã";
            FontConverter.Code[21, 6] = "eä";
            FontConverter.Code[22, 6] = "eå";
            FontConverter.Code[23, 6] = "ï";
            FontConverter.Code[24, 6] = "ïë";
            FontConverter.Code[25, 6] = "ïì";
            FontConverter.Code[26, 6] = "ïí";
            FontConverter.Code[27, 6] = "ïî";
            FontConverter.Code[28, 6] = "ïå";
            FontConverter.Code[29, 6] = "ñ";
            FontConverter.Code[30, 6] = "ò";
            FontConverter.Code[31, 6] = "ó";
            FontConverter.Code[32, 6] = "ô";
            FontConverter.Code[33, 6] = "õ";
            FontConverter.Code[34, 6] = "oá";
            FontConverter.Code[35, 6] = "oâ";
            FontConverter.Code[36, 6] = "oã";
            FontConverter.Code[37, 6] = "oä";
            FontConverter.Code[38, 6] = "oå";
            FontConverter.Code[39, 6] = "ö";
            FontConverter.Code[40, 6] = "öë";
            FontConverter.Code[41, 6] = "öì";
            FontConverter.Code[42, 6] = "öí";
            FontConverter.Code[43, 6] = "öî";
            FontConverter.Code[44, 6] = "öå";
            FontConverter.Code[45, 6] = "ú";
            FontConverter.Code[46, 6] = "úá";
            FontConverter.Code[47, 6] = "úâ";
            FontConverter.Code[48, 6] = "úã";
            FontConverter.Code[49, 6] = "úä";
            FontConverter.Code[50, 6] = "úå";
            FontConverter.Code[51, 6] = "uá";
            FontConverter.Code[52, 6] = "uâ";
            FontConverter.Code[53, 6] = "uã";
            FontConverter.Code[54, 6] = "uä";
            FontConverter.Code[55, 6] = "uå";
            FontConverter.Code[56, 6] = "û";
            FontConverter.Code[57, 6] = "ûá";
            FontConverter.Code[58, 6] = "ûâ";
            FontConverter.Code[59, 6] = "ûã";
            FontConverter.Code[60, 6] = "ûä";
            FontConverter.Code[61, 6] = "ûå";
            FontConverter.Code[62, 6] = "yá";
            FontConverter.Code[63, 6] = "yâ";
            FontConverter.Code[64, 6] = "yã";
            FontConverter.Code[65, 6] = "yä";
            FontConverter.Code[66, 6] = "yå";
            FontConverter.Code[67, 6] = "à";
            FontConverter.Code[68, 6] = "AÁ";
            FontConverter.Code[69, 6] = "AÂ";
            FontConverter.Code[70, 6] = "AÃ";
            FontConverter.Code[71, 6] = "AÄ";
            FontConverter.Code[72, 6] = "AÅ";
            FontConverter.Code[73, 6] = "Ù";
            FontConverter.Code[74, 6] = "ÙÆ";
            FontConverter.Code[75, 6] = "ÙÇ";
            FontConverter.Code[76, 6] = "ÙÈ";
            FontConverter.Code[77, 6] = "ÙÉ";
            FontConverter.Code[78, 6] = "ÙÅ";
            FontConverter.Code[79, 6] = "Ê";
            FontConverter.Code[80, 6] = "ÊË";
            FontConverter.Code[81, 6] = "ÊÌ";
            FontConverter.Code[82, 6] = "ÊÍ";
            FontConverter.Code[83, 6] = "ÊÎ";
            FontConverter.Code[84, 6] = "ÊÅ";
            FontConverter.Code[85, 6] = "EÁ";
            FontConverter.Code[86, 6] = "EÂ";
            FontConverter.Code[87, 6] = "EÃ";
            FontConverter.Code[88, 6] = "EÄ";
            FontConverter.Code[89, 6] = "EÅ";
            FontConverter.Code[90, 6] = "Ï";
            FontConverter.Code[91, 6] = "ÏË";
            FontConverter.Code[92, 6] = "ÏÌ";
            FontConverter.Code[93, 6] = "ÏÍ";
            FontConverter.Code[94, 6] = "ÏÎ";
            FontConverter.Code[95, 6] = "Ïå";
            FontConverter.Code[96, 6] = "Ñ";
            FontConverter.Code[97, 6] = "Ò";
            FontConverter.Code[98, 6] = "Ó";
            FontConverter.Code[99, 6] = "Ô";
            FontConverter.Code[100, 6] = "Õ";
            FontConverter.Code[101, 6] = "OÁ";
            FontConverter.Code[102, 6] = "OÂ";
            FontConverter.Code[103, 6] = "OÃ";
            FontConverter.Code[104, 6] = "OÄ";
            FontConverter.Code[105, 6] = "OÅ";
            FontConverter.Code[106, 6] = "Ö";
            FontConverter.Code[107, 6] = "ÖË";
            FontConverter.Code[108, 6] = "ÖÌ";
            FontConverter.Code[109, 6] = "ÖÍ";
            FontConverter.Code[110, 6] = "ÖÎ";
            FontConverter.Code[111, 6] = "ÖÅ";
            FontConverter.Code[112, 6] = "Ú";
            FontConverter.Code[113, 6] = "ÚÁ";
            FontConverter.Code[114, 6] = "ÚÂ";
            FontConverter.Code[115, 6] = "ÚÃ";
            FontConverter.Code[116, 6] = "ÚÄ";
            FontConverter.Code[117, 6] = "ÚÅ";
            FontConverter.Code[118, 6] = "UÁ";
            FontConverter.Code[119, 6] = "UÂ";
            FontConverter.Code[120, 6] = "UÃ";
            FontConverter.Code[121, 6] = "UÄ";
            FontConverter.Code[122, 6] = "UÅ";
            FontConverter.Code[123, 6] = "Û";
            FontConverter.Code[124, 6] = "ÛÁ";
            FontConverter.Code[125, 6] = "ÛÂ";
            FontConverter.Code[126, 6] = "ÛÃ";
            FontConverter.Code[(int)sbyte.MaxValue, 6] = "ÛÄ";
            FontConverter.Code[128, 6] = "ÛÅ";
            FontConverter.Code[129, 6] = "YÁ";
            FontConverter.Code[130, 6] = "YÂ";
            FontConverter.Code[131, 6] = "YÃ";
            FontConverter.Code[132, 6] = "YÄ";
            FontConverter.Code[133, 6] = "YÅ";
            FontConverter.Code[134, 6] = "À";
        }

        private static void MapBKHCM1()
        {
            FontConverter.Code[1, 6] = "\x00BE";
            FontConverter.Code[2, 6] = "¿";
            FontConverter.Code[3, 6] = "À";
            FontConverter.Code[4, 6] = "Á";
            FontConverter.Code[5, 6] = "Â";
            FontConverter.Code[6, 6] = "×";
            FontConverter.Code[7, 6] = "Ø";
            FontConverter.Code[8, 6] = "Ù";
            FontConverter.Code[9, 6] = "Ú";
            FontConverter.Code[10, 6] = "Û";
            FontConverter.Code[11, 6] = "Ü";
            FontConverter.Code[12, 6] = "Ý";
            FontConverter.Code[13, 6] = "Þ";
            FontConverter.Code[14, 6] = "ß";
            FontConverter.Code[15, 6] = "à";
            FontConverter.Code[16, 6] = "á";
            FontConverter.Code[17, 6] = "â";
            FontConverter.Code[18, 6] = "Ã";
            FontConverter.Code[19, 6] = "Ä";
            FontConverter.Code[20, 6] = "Å";
            FontConverter.Code[21, 6] = "Æ";
            FontConverter.Code[22, 6] = "Ç";
            FontConverter.Code[23, 6] = "ã";
            FontConverter.Code[24, 6] = "ä";
            FontConverter.Code[25, 6] = "å";
            FontConverter.Code[26, 6] = "æ";
            FontConverter.Code[27, 6] = "ç";
            FontConverter.Code[28, 6] = "è";
            FontConverter.Code[29, 6] = "È";
            FontConverter.Code[30, 6] = "É";
            FontConverter.Code[31, 6] = "Ê";
            FontConverter.Code[32, 6] = "Ë";
            FontConverter.Code[33, 6] = "Ì";
            FontConverter.Code[34, 6] = "Í";
            FontConverter.Code[35, 6] = "Î";
            FontConverter.Code[36, 6] = "Ï";
            FontConverter.Code[37, 6] = "Ð";
            FontConverter.Code[38, 6] = "Ñ";
            FontConverter.Code[39, 6] = "é";
            FontConverter.Code[40, 6] = "ê";
            FontConverter.Code[41, 6] = "ë";
            FontConverter.Code[42, 6] = "ì";
            FontConverter.Code[43, 6] = "í";
            FontConverter.Code[44, 6] = "î";
            FontConverter.Code[45, 6] = "ï";
            FontConverter.Code[46, 6] = "ð";
            FontConverter.Code[47, 6] = "ñ";
            FontConverter.Code[48, 6] = "ò";
            FontConverter.Code[49, 6] = "ó";
            FontConverter.Code[50, 6] = "ô";
            FontConverter.Code[51, 6] = "Ò";
            FontConverter.Code[52, 6] = "Ó";
            FontConverter.Code[53, 6] = "Ô";
            FontConverter.Code[54, 6] = "Õ";
            FontConverter.Code[55, 6] = "Ö";
            FontConverter.Code[56, 6] = "õ";
            FontConverter.Code[57, 6] = "ö";
            FontConverter.Code[58, 6] = "÷";
            FontConverter.Code[59, 6] = "ø";
            FontConverter.Code[60, 6] = "ù";
            FontConverter.Code[61, 6] = "ú";
            FontConverter.Code[62, 6] = "û";
            FontConverter.Code[63, 6] = "ü";
            FontConverter.Code[64, 6] = "ý";
            FontConverter.Code[65, 6] = "þ";
            FontConverter.Code[66, 6] = "ÿ";
            FontConverter.Code[67, 6] = "\x00BD";
            FontConverter.Code[68, 6] = "€";
            FontConverter.Code[69, 6] = "\x0081";
            FontConverter.Code[70, 6] = "‚";
            FontConverter.Code[71, 6] = "ƒ";
            FontConverter.Code[72, 6] = "„";
            FontConverter.Code[73, 6] = "™";
            FontConverter.Code[74, 6] = "š";
            FontConverter.Code[75, 6] = "›";
            FontConverter.Code[76, 6] = "œ";
            FontConverter.Code[77, 6] = "\x009D";
            FontConverter.Code[78, 6] = "˜";
            FontConverter.Code[79, 6] = "Ÿ";
            FontConverter.Code[80, 6] = "~";
            FontConverter.Code[81, 6] = "¡";
            FontConverter.Code[82, 6] = "¢";
            FontConverter.Code[83, 6] = "£";
            FontConverter.Code[84, 6] = "¤";
            FontConverter.Code[85, 6] = "…";
            FontConverter.Code[86, 6] = "†";
            FontConverter.Code[87, 6] = "‡";
            FontConverter.Code[88, 6] = "\x02C6";
            FontConverter.Code[89, 6] = "‰";
            FontConverter.Code[90, 6] = "¥";
            FontConverter.Code[91, 6] = "¦";
            FontConverter.Code[92, 6] = "§";
            FontConverter.Code[93, 6] = "¨";
            FontConverter.Code[94, 6] = "©";
            FontConverter.Code[95, 6] = "ª";
            FontConverter.Code[96, 6] = "Š";
            FontConverter.Code[97, 6] = "‹";
            FontConverter.Code[98, 6] = "Œ";
            FontConverter.Code[99, 6] = "\x008D";
            FontConverter.Code[100, 6] = "Ž";
            FontConverter.Code[101, 6] = "\x008F";
            FontConverter.Code[102, 6] = "\x0090";
            FontConverter.Code[103, 6] = "‘";
            FontConverter.Code[104, 6] = "’";
            FontConverter.Code[105, 6] = "“";
            FontConverter.Code[106, 6] = "«";
            FontConverter.Code[107, 6] = "¬";
            FontConverter.Code[108, 6] = "\x00AD";
            FontConverter.Code[109, 6] = "®";
            FontConverter.Code[110, 6] = "¯";
            FontConverter.Code[111, 6] = "°";
            FontConverter.Code[112, 6] = "±";
            FontConverter.Code[113, 6] = "\x00B2";
            FontConverter.Code[114, 6] = "\x00B3";
            FontConverter.Code[115, 6] = "´";
            FontConverter.Code[116, 6] = "µ";
            FontConverter.Code[117, 6] = "¶";
            FontConverter.Code[118, 6] = "”";
            FontConverter.Code[119, 6] = "•";
            FontConverter.Code[120, 6] = "–";
            FontConverter.Code[121, 6] = "—";
            FontConverter.Code[122, 6] = "˜";
            FontConverter.Code[123, 6] = "·";
            FontConverter.Code[124, 6] = "¸";
            FontConverter.Code[125, 6] = "\x00B9";
            FontConverter.Code[126, 6] = "º";
            FontConverter.Code[(int)sbyte.MaxValue, 6] = "»";
            FontConverter.Code[128, 6] = "\x00BC";
            FontConverter.Code[129, 6] = "{";
            FontConverter.Code[130, 6] = "^";
            FontConverter.Code[131, 6] = "`";
            FontConverter.Code[132, 6] = "|";
            FontConverter.Code[133, 6] = "Ž";
            FontConverter.Code[134, 6] = "}";
        }

        private static void MapVietwareX()
        {
            FontConverter.Code[1, 6] = "aï";
            FontConverter.Code[2, 6] = "aì";
            FontConverter.Code[3, 6] = "aí";
            FontConverter.Code[4, 6] = "aî";
            FontConverter.Code[5, 6] = "aû";
            FontConverter.Code[6, 6] = "à";
            FontConverter.Code[7, 6] = "àõ";
            FontConverter.Code[8, 6] = "àò";
            FontConverter.Code[9, 6] = "àó";
            FontConverter.Code[10, 6] = "àô";
            FontConverter.Code[11, 6] = "àû";
            FontConverter.Code[12, 6] = "á";
            FontConverter.Code[13, 6] = "áú";
            FontConverter.Code[14, 6] = "áö";
            FontConverter.Code[15, 6] = "áø";
            FontConverter.Code[16, 6] = "áù";
            FontConverter.Code[17, 6] = "áû";
            FontConverter.Code[18, 6] = "eï";
            FontConverter.Code[19, 6] = "eì";
            FontConverter.Code[20, 6] = "eí";
            FontConverter.Code[21, 6] = "eî";
            FontConverter.Code[22, 6] = "eû";
            FontConverter.Code[23, 6] = "ã";
            FontConverter.Code[24, 6] = "ãú";
            FontConverter.Code[25, 6] = "ãö";
            FontConverter.Code[26, 6] = "ãø";
            FontConverter.Code[27, 6] = "ãù";
            FontConverter.Code[28, 6] = "ãû";
            FontConverter.Code[29, 6] = "ê";
            FontConverter.Code[30, 6] = "ç";
            FontConverter.Code[31, 6] = "è";
            FontConverter.Code[32, 6] = "é";
            FontConverter.Code[33, 6] = "ë";
            FontConverter.Code[34, 6] = "oï";
            FontConverter.Code[35, 6] = "oì";
            FontConverter.Code[36, 6] = "oí";
            FontConverter.Code[37, 6] = "oî";
            FontConverter.Code[38, 6] = "oü";
            FontConverter.Code[39, 6] = "ä";
            FontConverter.Code[40, 6] = "äú";
            FontConverter.Code[41, 6] = "äö";
            FontConverter.Code[42, 6] = "äø";
            FontConverter.Code[43, 6] = "äù";
            FontConverter.Code[44, 6] = "äü";
            FontConverter.Code[45, 6] = "å";
            FontConverter.Code[46, 6] = "åï";
            FontConverter.Code[47, 6] = "åì";
            FontConverter.Code[48, 6] = "åí";
            FontConverter.Code[49, 6] = "åî";
            FontConverter.Code[50, 6] = "åü";
            FontConverter.Code[51, 6] = "uï";
            FontConverter.Code[52, 6] = "uì";
            FontConverter.Code[53, 6] = "uí";
            FontConverter.Code[54, 6] = "uî";
            FontConverter.Code[55, 6] = "uû";
            FontConverter.Code[56, 6] = "æ";
            FontConverter.Code[57, 6] = "æï";
            FontConverter.Code[58, 6] = "æì";
            FontConverter.Code[59, 6] = "æí";
            FontConverter.Code[60, 6] = "æî";
            FontConverter.Code[61, 6] = "æû";
            FontConverter.Code[62, 6] = "yï";
            FontConverter.Code[63, 6] = "yì";
            FontConverter.Code[64, 6] = "yí";
            FontConverter.Code[65, 6] = "yî";
            FontConverter.Code[66, 6] = "yñ";
            FontConverter.Code[67, 6] = "â";
            FontConverter.Code[68, 6] = "AÏ";
            FontConverter.Code[69, 6] = "AÌ";
            FontConverter.Code[70, 6] = "AÍ";
            FontConverter.Code[71, 6] = "AÎ";
            FontConverter.Code[72, 6] = "AÛ";
            FontConverter.Code[73, 6] = "À";
            FontConverter.Code[74, 6] = "ÀÕ";
            FontConverter.Code[75, 6] = "ÀÒ";
            FontConverter.Code[76, 6] = "ÀÓ";
            FontConverter.Code[77, 6] = "ÀÔ";
            FontConverter.Code[78, 6] = "ÀÛ";
            FontConverter.Code[79, 6] = "Á";
            FontConverter.Code[80, 6] = "ÁÚ";
            FontConverter.Code[81, 6] = "ÁÖ";
            FontConverter.Code[82, 6] = "ÁØ";
            FontConverter.Code[83, 6] = "ÁÙ";
            FontConverter.Code[84, 6] = "ÁÛ";
            FontConverter.Code[85, 6] = "EÏ";
            FontConverter.Code[86, 6] = "EÌ";
            FontConverter.Code[87, 6] = "EÍ";
            FontConverter.Code[88, 6] = "EÎ";
            FontConverter.Code[89, 6] = "EÛ";
            FontConverter.Code[90, 6] = "Ã";
            FontConverter.Code[91, 6] = "ÃÚ";
            FontConverter.Code[92, 6] = "ÃÖ";
            FontConverter.Code[93, 6] = "ÃØ";
            FontConverter.Code[94, 6] = "ÃÙ";
            FontConverter.Code[95, 6] = "ÃÛ";
            FontConverter.Code[96, 6] = "Ê";
            FontConverter.Code[97, 6] = "Ç";
            FontConverter.Code[98, 6] = "È";
            FontConverter.Code[99, 6] = "É";
            FontConverter.Code[100, 6] = "Ë";
            FontConverter.Code[101, 6] = "OÏ";
            FontConverter.Code[102, 6] = "OÌ";
            FontConverter.Code[103, 6] = "OÍ";
            FontConverter.Code[104, 6] = "OÎ";
            FontConverter.Code[105, 6] = "OÜ";
            FontConverter.Code[106, 6] = "Ä";
            FontConverter.Code[107, 6] = "ÄÚ";
            FontConverter.Code[108, 6] = "ÄÖ";
            FontConverter.Code[109, 6] = "ÄØ";
            FontConverter.Code[110, 6] = "ÄÙ";
            FontConverter.Code[111, 6] = "ÄÜ";
            FontConverter.Code[112, 6] = "Å";
            FontConverter.Code[113, 6] = "ÅÏ";
            FontConverter.Code[114, 6] = "ÅÌ";
            FontConverter.Code[115, 6] = "ÅÍ";
            FontConverter.Code[116, 6] = "ÅÎ";
            FontConverter.Code[117, 6] = "ÅÜ";
            FontConverter.Code[118, 6] = "UÏ";
            FontConverter.Code[119, 6] = "UÌ";
            FontConverter.Code[120, 6] = "UÍ";
            FontConverter.Code[121, 6] = "UÎ";
            FontConverter.Code[122, 6] = "UÛ";
            FontConverter.Code[123, 6] = "Æ";
            FontConverter.Code[124, 6] = "ÆÏ";
            FontConverter.Code[125, 6] = "ÆÌ";
            FontConverter.Code[126, 6] = "ÆÍ";
            FontConverter.Code[(int)sbyte.MaxValue, 6] = "ÆÎ";
            FontConverter.Code[128, 6] = "ÆÛ";
            FontConverter.Code[129, 6] = "YÏ";
            FontConverter.Code[130, 6] = "YÌ";
            FontConverter.Code[131, 6] = "YÍ";
            FontConverter.Code[132, 6] = "YÎ";
            FontConverter.Code[133, 6] = "YÑ";
            FontConverter.Code[134, 6] = "Â";
        }

        private static void MapVietwareF()
        {
            FontConverter.Code[1, 6] = "À";
            FontConverter.Code[2, 6] = "ª";
            FontConverter.Code[3, 6] = "¶";
            FontConverter.Code[4, 6] = "º";
            FontConverter.Code[5, 6] = "Á";
            FontConverter.Code[6, 6] = "Ÿ";
            FontConverter.Code[7, 6] = "Å";
            FontConverter.Code[8, 6] = "Â";
            FontConverter.Code[9, 6] = "Ã";
            FontConverter.Code[10, 6] = "Ä";
            FontConverter.Code[11, 6] = "Æ";
            FontConverter.Code[12, 6] = "¡";
            FontConverter.Code[13, 6] = "Ê";
            FontConverter.Code[14, 6] = "Ç";
            FontConverter.Code[15, 6] = "È";
            FontConverter.Code[16, 6] = "É";
            FontConverter.Code[17, 6] = "Ë";
            FontConverter.Code[18, 6] = "Ï";
            FontConverter.Code[19, 6] = "Ì";
            FontConverter.Code[20, 6] = "Í";
            FontConverter.Code[21, 6] = "Î";
            FontConverter.Code[22, 6] = "Ñ";
            FontConverter.Code[23, 6] = "£";
            FontConverter.Code[24, 6] = "Õ";
            FontConverter.Code[25, 6] = "Ò";
            FontConverter.Code[26, 6] = "Ó";
            FontConverter.Code[27, 6] = "Ô";
            FontConverter.Code[28, 6] = "Ö";
            FontConverter.Code[29, 6] = "Û";
            FontConverter.Code[30, 6] = "Ø";
            FontConverter.Code[31, 6] = "Ù";
            FontConverter.Code[32, 6] = "Ú";
            FontConverter.Code[33, 6] = "Ü";
            FontConverter.Code[34, 6] = "â";
            FontConverter.Code[35, 6] = "ß";
            FontConverter.Code[36, 6] = "à";
            FontConverter.Code[37, 6] = "á";
            FontConverter.Code[38, 6] = "ã";
            FontConverter.Code[39, 6] = "¤";
            FontConverter.Code[40, 6] = "ç";
            FontConverter.Code[41, 6] = "ä";
            FontConverter.Code[42, 6] = "å";
            FontConverter.Code[43, 6] = "æ";
            FontConverter.Code[44, 6] = "è";
            FontConverter.Code[45, 6] = "¥";
            FontConverter.Code[46, 6] = "ì";
            FontConverter.Code[47, 6] = "é";
            FontConverter.Code[48, 6] = "ê";
            FontConverter.Code[49, 6] = "ë";
            FontConverter.Code[50, 6] = "í";
            FontConverter.Code[51, 6] = "ò";
            FontConverter.Code[52, 6] = "î";
            FontConverter.Code[53, 6] = "ï";
            FontConverter.Code[54, 6] = "ñ";
            FontConverter.Code[55, 6] = "ó";
            FontConverter.Code[56, 6] = "§";
            FontConverter.Code[57, 6] = "÷";
            FontConverter.Code[58, 6] = "ô";
            FontConverter.Code[59, 6] = "õ";
            FontConverter.Code[60, 6] = "ö";
            FontConverter.Code[61, 6] = "ø";
            FontConverter.Code[62, 6] = "ü";
            FontConverter.Code[63, 6] = "ù";
            FontConverter.Code[64, 6] = "ú";
            FontConverter.Code[65, 6] = "û";
            FontConverter.Code[66, 6] = "ÿ";
            FontConverter.Code[67, 6] = "¢";
            FontConverter.Code[68, 6] = "À";
            FontConverter.Code[69, 6] = "ª";
            FontConverter.Code[70, 6] = "¶";
            FontConverter.Code[71, 6] = "º";
            FontConverter.Code[72, 6] = "Á";
            FontConverter.Code[73, 6] = "–";
            FontConverter.Code[74, 6] = "Å";
            FontConverter.Code[75, 6] = "Â";
            FontConverter.Code[76, 6] = "Ã";
            FontConverter.Code[77, 6] = "Ä";
            FontConverter.Code[78, 6] = "Æ";
            FontConverter.Code[79, 6] = "—";
            FontConverter.Code[80, 6] = "Ê";
            FontConverter.Code[81, 6] = "Ç";
            FontConverter.Code[82, 6] = "È";
            FontConverter.Code[83, 6] = "É";
            FontConverter.Code[84, 6] = "Ë";
            FontConverter.Code[85, 6] = "Ï";
            FontConverter.Code[86, 6] = "Ì";
            FontConverter.Code[87, 6] = "Í";
            FontConverter.Code[88, 6] = "Î";
            FontConverter.Code[89, 6] = "Ñ";
            FontConverter.Code[90, 6] = "™";
            FontConverter.Code[91, 6] = "Õ";
            FontConverter.Code[92, 6] = "Ò";
            FontConverter.Code[93, 6] = "Ó";
            FontConverter.Code[94, 6] = "Ô";
            FontConverter.Code[95, 6] = "Ö";
            FontConverter.Code[96, 6] = "Û";
            FontConverter.Code[97, 6] = "Ø";
            FontConverter.Code[98, 6] = "Ù";
            FontConverter.Code[99, 6] = "Ú";
            FontConverter.Code[100, 6] = "Ü";
            FontConverter.Code[101, 6] = "â";
            FontConverter.Code[102, 6] = "ß";
            FontConverter.Code[103, 6] = "à";
            FontConverter.Code[104, 6] = "á";
            FontConverter.Code[105, 6] = "ã";
            FontConverter.Code[106, 6] = "š";
            FontConverter.Code[107, 6] = "ç";
            FontConverter.Code[108, 6] = "ä";
            FontConverter.Code[109, 6] = "å";
            FontConverter.Code[110, 6] = "æ";
            FontConverter.Code[111, 6] = "è";
            FontConverter.Code[112, 6] = "›";
            FontConverter.Code[113, 6] = "ì";
            FontConverter.Code[114, 6] = "é";
            FontConverter.Code[115, 6] = "ê";
            FontConverter.Code[116, 6] = "ë";
            FontConverter.Code[117, 6] = "í";
            FontConverter.Code[118, 6] = "ò";
            FontConverter.Code[119, 6] = "î";
            FontConverter.Code[120, 6] = "ï";
            FontConverter.Code[121, 6] = "ñ";
            FontConverter.Code[122, 6] = "ó";
            FontConverter.Code[123, 6] = "œ";
            FontConverter.Code[124, 6] = "÷";
            FontConverter.Code[125, 6] = "ô";
            FontConverter.Code[126, 6] = "õ";
            FontConverter.Code[(int)sbyte.MaxValue, 6] = "ö";
            FontConverter.Code[128, 6] = "ø";
            FontConverter.Code[129, 6] = "ü";
            FontConverter.Code[130, 6] = "ù";
            FontConverter.Code[131, 6] = "ú";
            FontConverter.Code[132, 6] = "û";
            FontConverter.Code[133, 6] = "ÿ";
            FontConverter.Code[134, 6] = "˜";
        }

        private static void MapNCRHex()
        {
            FontConverter.Code[1, 6] = "á";
            FontConverter.Code[2, 6] = "à";
            FontConverter.Code[3, 6] = "&#x1EA3;";
            FontConverter.Code[4, 6] = "ã";
            FontConverter.Code[5, 6] = "&#x1EA1;";
            FontConverter.Code[6, 6] = "&#x103;";
            FontConverter.Code[7, 6] = "&#x1EAF;";
            FontConverter.Code[8, 6] = "&#x1EB1;";
            FontConverter.Code[9, 6] = "&#x1EB3;";
            FontConverter.Code[10, 6] = "&#x1EB5;";
            FontConverter.Code[11, 6] = "&#x1EB7;";
            FontConverter.Code[12, 6] = "â";
            FontConverter.Code[13, 6] = "&#x1EA5;";
            FontConverter.Code[14, 6] = "&#x1EA7;";
            FontConverter.Code[15, 6] = "&#x1EA9;";
            FontConverter.Code[16, 6] = "&#x1EAB;";
            FontConverter.Code[17, 6] = "&#x1EAD;";
            FontConverter.Code[18, 6] = "é";
            FontConverter.Code[19, 6] = "è";
            FontConverter.Code[20, 6] = "&#x1EBB;";
            FontConverter.Code[21, 6] = "&#x1EBD;";
            FontConverter.Code[22, 6] = "&#x1EB9;";
            FontConverter.Code[23, 6] = "ê";
            FontConverter.Code[24, 6] = "&#x1EBF;";
            FontConverter.Code[25, 6] = "&#x1EC1;";
            FontConverter.Code[26, 6] = "&#x1EC3;";
            FontConverter.Code[27, 6] = "&#x1EC5;";
            FontConverter.Code[28, 6] = "&#x1EC7;";
            FontConverter.Code[29, 6] = "í";
            FontConverter.Code[30, 6] = "ì";
            FontConverter.Code[31, 6] = "&#x1EC9;";
            FontConverter.Code[32, 6] = "&#x129;";
            FontConverter.Code[33, 6] = "&#x1ECB;";
            FontConverter.Code[34, 6] = "ó";
            FontConverter.Code[35, 6] = "ò";
            FontConverter.Code[36, 6] = "&#x1ECF;";
            FontConverter.Code[37, 6] = "õ";
            FontConverter.Code[38, 6] = "&#x1ECD;";
            FontConverter.Code[39, 6] = "ô";
            FontConverter.Code[40, 6] = "&#x1ED1;";
            FontConverter.Code[41, 6] = "&#x1ED3;";
            FontConverter.Code[42, 6] = "&#x1ED5;";
            FontConverter.Code[43, 6] = "&#x1ED7;";
            FontConverter.Code[44, 6] = "&#x1ED9;";
            FontConverter.Code[45, 6] = "&#x1A1;";
            FontConverter.Code[46, 6] = "&#x1EDB;";
            FontConverter.Code[47, 6] = "&#x1EDD;";
            FontConverter.Code[48, 6] = "&#x1EDF;";
            FontConverter.Code[49, 6] = "&#x1EE1;";
            FontConverter.Code[50, 6] = "&#x1EE3;";
            FontConverter.Code[51, 6] = "ú";
            FontConverter.Code[52, 6] = "ù";
            FontConverter.Code[53, 6] = "&#x1EE7;";
            FontConverter.Code[54, 6] = "&#x169;";
            FontConverter.Code[55, 6] = "&#x1EE5;";
            FontConverter.Code[56, 6] = "&#x1B0;";
            FontConverter.Code[57, 6] = "&#x1EE9;";
            FontConverter.Code[58, 6] = "&#x1EEB;";
            FontConverter.Code[59, 6] = "&#x1EED;";
            FontConverter.Code[60, 6] = "&#x1EEF;";
            FontConverter.Code[61, 6] = "&#x1EF1;";
            FontConverter.Code[62, 6] = "ý";
            FontConverter.Code[63, 6] = "&#x1EF3;";
            FontConverter.Code[64, 6] = "&#x1EF7;";
            FontConverter.Code[65, 6] = "&#x1EF9;";
            FontConverter.Code[66, 6] = "&#x1EF5;";
            FontConverter.Code[67, 6] = "&#x111;";
            FontConverter.Code[68, 6] = "Á";
            FontConverter.Code[69, 6] = "À";
            FontConverter.Code[70, 6] = "&#x1EA2;";
            FontConverter.Code[71, 6] = "Ã";
            FontConverter.Code[72, 6] = "&#x1EA0;";
            FontConverter.Code[73, 6] = "&#x102;";
            FontConverter.Code[74, 6] = "&#x1EAE;";
            FontConverter.Code[75, 6] = "&#x1EB0;";
            FontConverter.Code[76, 6] = "&#x1EB2;";
            FontConverter.Code[77, 6] = "&#x1EB4;";
            FontConverter.Code[78, 6] = "&#x1EB6;";
            FontConverter.Code[79, 6] = "Â";
            FontConverter.Code[80, 6] = "&#x1EA4;";
            FontConverter.Code[81, 6] = "&#x1EA6;";
            FontConverter.Code[82, 6] = "&#x1EA8;";
            FontConverter.Code[83, 6] = "&#x1EAA;";
            FontConverter.Code[84, 6] = "&#x1EAC;";
            FontConverter.Code[85, 6] = "É";
            FontConverter.Code[86, 6] = "È";
            FontConverter.Code[87, 6] = "&#x1EBA;";
            FontConverter.Code[88, 6] = "&#x1EBC;";
            FontConverter.Code[89, 6] = "&#x1EB8;";
            FontConverter.Code[90, 6] = "Ê";
            FontConverter.Code[91, 6] = "&#x1EBE;";
            FontConverter.Code[92, 6] = "&#x1EC0;";
            FontConverter.Code[93, 6] = "&#x1EC2;";
            FontConverter.Code[94, 6] = "&#x1EC4;";
            FontConverter.Code[95, 6] = "&#x1EC6;";
            FontConverter.Code[96, 6] = "Í";
            FontConverter.Code[97, 6] = "Ì";
            FontConverter.Code[98, 6] = "&#x1EC8;";
            FontConverter.Code[99, 6] = "&#x128;";
            FontConverter.Code[100, 6] = "&#x1ECA;";
            FontConverter.Code[101, 6] = "Ó";
            FontConverter.Code[102, 6] = "Ò";
            FontConverter.Code[103, 6] = "&#x1ECE;";
            FontConverter.Code[104, 6] = "Õ";
            FontConverter.Code[105, 6] = "&#x1ECC;";
            FontConverter.Code[106, 6] = "Ô";
            FontConverter.Code[107, 6] = "&#x1ED0;";
            FontConverter.Code[108, 6] = "&#x1ED2;";
            FontConverter.Code[109, 6] = "&#x1ED4;";
            FontConverter.Code[110, 6] = "&#x1ED6;";
            FontConverter.Code[111, 6] = "&#x1ED8;";
            FontConverter.Code[112, 6] = "&#x1A0;";
            FontConverter.Code[113, 6] = "&#x1EDA;";
            FontConverter.Code[114, 6] = "&#x1EDC;";
            FontConverter.Code[115, 6] = "&#x1EDE;";
            FontConverter.Code[116, 6] = "&#x1EE0;";
            FontConverter.Code[117, 6] = "&#x1EE2;";
            FontConverter.Code[118, 6] = "Ú";
            FontConverter.Code[119, 6] = "Ù";
            FontConverter.Code[120, 6] = "&#x1EE6;";
            FontConverter.Code[121, 6] = "&#x168;";
            FontConverter.Code[122, 6] = "&#x1EE4;";
            FontConverter.Code[123, 6] = "&#x1AF;";
            FontConverter.Code[124, 6] = "&#x1EE8;";
            FontConverter.Code[125, 6] = "&#x1EEA;";
            FontConverter.Code[126, 6] = "&#x1EEC;";
            FontConverter.Code[(int)sbyte.MaxValue, 6] = "&#x1EEE;";
            FontConverter.Code[128, 6] = "&#x1EF0;";
            FontConverter.Code[129, 6] = "Ý";
            FontConverter.Code[130, 6] = "&#x1EF2;";
            FontConverter.Code[131, 6] = "&#x1EF6;";
            FontConverter.Code[132, 6] = "&#x1EF8;";
            FontConverter.Code[133, 6] = "&#x1EF4;";
            FontConverter.Code[134, 6] = "&#x110;";
        }

        private static void MapCString()
        {
            FontConverter.Code[1, 6] = "á";
            FontConverter.Code[2, 6] = "à";
            FontConverter.Code[3, 6] = "ả";
            FontConverter.Code[4, 6] = "ã";
            FontConverter.Code[5, 6] = "ạ";
            FontConverter.Code[6, 6] = "ă";
            FontConverter.Code[7, 6] = "ắ";
            FontConverter.Code[8, 6] = "ằ";
            FontConverter.Code[9, 6] = "ẳ";
            FontConverter.Code[10, 6] = "ẵ";
            FontConverter.Code[11, 6] = "ặ";
            FontConverter.Code[12, 6] = "â";
            FontConverter.Code[13, 6] = "ấ";
            FontConverter.Code[14, 6] = "ầ";
            FontConverter.Code[15, 6] = "ẩ";
            FontConverter.Code[16, 6] = "ẫ";
            FontConverter.Code[17, 6] = "ậ";
            FontConverter.Code[18, 6] = "é";
            FontConverter.Code[19, 6] = "è";
            FontConverter.Code[20, 6] = "ẻ";
            FontConverter.Code[21, 6] = "ẽ";
            FontConverter.Code[22, 6] = "ẹ";
            FontConverter.Code[23, 6] = "ê";
            FontConverter.Code[24, 6] = "ế";
            FontConverter.Code[25, 6] = "ề";
            FontConverter.Code[26, 6] = "ể";
            FontConverter.Code[27, 6] = "ễ";
            FontConverter.Code[28, 6] = "ệ";
            FontConverter.Code[29, 6] = "í";
            FontConverter.Code[30, 6] = "ì";
            FontConverter.Code[31, 6] = "ỉ";
            FontConverter.Code[32, 6] = "ĩ";
            FontConverter.Code[33, 6] = "ị";
            FontConverter.Code[34, 6] = "ó";
            FontConverter.Code[35, 6] = "ò";
            FontConverter.Code[36, 6] = "ỏ";
            FontConverter.Code[37, 6] = "õ";
            FontConverter.Code[38, 6] = "ọ";
            FontConverter.Code[39, 6] = "ô";
            FontConverter.Code[40, 6] = "ố";
            FontConverter.Code[41, 6] = "ồ";
            FontConverter.Code[42, 6] = "ổ";
            FontConverter.Code[43, 6] = "ỗ";
            FontConverter.Code[44, 6] = "ộ";
            FontConverter.Code[45, 6] = "ơ";
            FontConverter.Code[46, 6] = "ớ";
            FontConverter.Code[47, 6] = "ờ";
            FontConverter.Code[48, 6] = "ở";
            FontConverter.Code[49, 6] = "ỡ";
            FontConverter.Code[50, 6] = "ợ";
            FontConverter.Code[51, 6] = "ú";
            FontConverter.Code[52, 6] = "ù";
            FontConverter.Code[53, 6] = "ủ";
            FontConverter.Code[54, 6] = "ũ";
            FontConverter.Code[55, 6] = "ụ";
            FontConverter.Code[56, 6] = "ư";
            FontConverter.Code[57, 6] = "ứ";
            FontConverter.Code[58, 6] = "ừ";
            FontConverter.Code[59, 6] = "ử";
            FontConverter.Code[60, 6] = "ữ";
            FontConverter.Code[61, 6] = "ự";
            FontConverter.Code[62, 6] = "ý";
            FontConverter.Code[63, 6] = "ỳ";
            FontConverter.Code[64, 6] = "ỷ";
            FontConverter.Code[65, 6] = "ỹ";
            FontConverter.Code[66, 6] = "ỵ";
            FontConverter.Code[67, 6] = "đ";
            FontConverter.Code[68, 6] = "Á";
            FontConverter.Code[69, 6] = "À";
            FontConverter.Code[70, 6] = "Ả";
            FontConverter.Code[71, 6] = "Ã";
            FontConverter.Code[72, 6] = "Ạ";
            FontConverter.Code[73, 6] = "Ă";
            FontConverter.Code[74, 6] = "Ắ";
            FontConverter.Code[75, 6] = "Ằ";
            FontConverter.Code[76, 6] = "Ẳ";
            FontConverter.Code[77, 6] = "Ẵ";
            FontConverter.Code[78, 6] = "Ặ";
            FontConverter.Code[79, 6] = "Â";
            FontConverter.Code[80, 6] = "Ấ";
            FontConverter.Code[81, 6] = "Ầ";
            FontConverter.Code[82, 6] = "Ẩ";
            FontConverter.Code[83, 6] = "Ẫ";
            FontConverter.Code[84, 6] = "Ậ";
            FontConverter.Code[85, 6] = "É";
            FontConverter.Code[86, 6] = "È";
            FontConverter.Code[87, 6] = "Ẻ";
            FontConverter.Code[88, 6] = "Ẽ";
            FontConverter.Code[89, 6] = "Ẹ";
            FontConverter.Code[90, 6] = "Ê";
            FontConverter.Code[91, 6] = "Ế";
            FontConverter.Code[92, 6] = "Ề";
            FontConverter.Code[93, 6] = "Ể";
            FontConverter.Code[94, 6] = "Ễ";
            FontConverter.Code[95, 6] = "Ệ";
            FontConverter.Code[96, 6] = "Í";
            FontConverter.Code[97, 6] = "Ì";
            FontConverter.Code[98, 6] = "Ỉ";
            FontConverter.Code[99, 6] = "Ĩ";
            FontConverter.Code[100, 6] = "Ị";
            FontConverter.Code[101, 6] = "Ó";
            FontConverter.Code[102, 6] = "Ò";
            FontConverter.Code[103, 6] = "Ỏ";
            FontConverter.Code[104, 6] = "Õ";
            FontConverter.Code[105, 6] = "Ọ";
            FontConverter.Code[106, 6] = "Ô";
            FontConverter.Code[107, 6] = "Ố";
            FontConverter.Code[108, 6] = "Ồ";
            FontConverter.Code[109, 6] = "Ổ";
            FontConverter.Code[110, 6] = "Ỗ";
            FontConverter.Code[111, 6] = "Ộ";
            FontConverter.Code[112, 6] = "Ơ";
            FontConverter.Code[113, 6] = "Ớ";
            FontConverter.Code[114, 6] = "Ờ";
            FontConverter.Code[115, 6] = "Ở";
            FontConverter.Code[116, 6] = "Ỡ";
            FontConverter.Code[117, 6] = "Ợ";
            FontConverter.Code[118, 6] = "Ú";
            FontConverter.Code[119, 6] = "Ù";
            FontConverter.Code[120, 6] = "Ủ";
            FontConverter.Code[121, 6] = "Ũ";
            FontConverter.Code[122, 6] = "Ụ";
            FontConverter.Code[123, 6] = "Ư";
            FontConverter.Code[124, 6] = "Ứ";
            FontConverter.Code[125, 6] = "Ừ";
            FontConverter.Code[126, 6] = "Ử";
            FontConverter.Code[(int)sbyte.MaxValue, 6] = "Ữ";
            FontConverter.Code[128, 6] = "Ự";
            FontConverter.Code[129, 6] = "Ý";
            FontConverter.Code[130, 6] = "Ỳ";
            FontConverter.Code[131, 6] = "Ỷ";
            FontConverter.Code[132, 6] = "Ỹ";
            FontConverter.Code[133, 6] = "Ỵ";
            FontConverter.Code[134, 6] = "Đ";
        }
    }
}
