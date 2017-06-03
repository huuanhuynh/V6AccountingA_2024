using System;
using System.Text;

namespace HaUtility.Converter
{
    public class VietnameseText
    {
        #region ==== HOA thường ====
        public static string HoaTatCa(string s)
        {
            return s.ToUpper();
        }
        /// <summary>
        /// Chuyển tất cả các từ thành viết hoa đầu từ và thường cuối từ
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HoaDauTuThuongCuoiTu(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            string rs = "";
            string space = " .,:;'\"|[]{}<>?~!@#$%^&*()-+=/\\";
            char prc = ' ';//ký tự đầu sẽ hoa
            foreach (char c in s)
            {
                //Nếu phía trước ký tự này là khoảng cách thì viết hoa ký tự hiện tại
                if (space.IndexOf(prc) >= 0)
                    rs += c.ToString().ToUpper();
                else
                    rs += c.ToString().ToLower();
                prc = c;
            }
            return rs;
        }

        /// <summary>
        /// Hoa đầu từ. chữ trong từ sao kệ
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HoaDauTu(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            string rs = "";
            string space = " .,:;'\"|[]{}<>?~!@#$%^&*()-+=/\\";
            char prc = ' ';//ký tự đầu sẽ hoa
            foreach (char c in s)
            {
                //Nếu phía trước ký tự này là khoảng cách thì viết hoa ký tự hiện tại
                if (space.IndexOf(prc) >= 0)
                    rs += c.ToString().ToUpper();
                else
                    rs += c.ToString();
                prc = c;
            }
            return rs;
        }

        /// <summary>
        /// Chuyển tất cả các chữ sau ký tự đầu tiên của mỗi từ thành chữ thường
        /// Không đụng đến ký tự đầu mỗi từ
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ThuongCuoiTu(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            string rs = "";
            string space = " .,:;'\"|[]{}<>?~!@#$%^&*()-+=/\\";
            char prc = ' ';
            foreach (char c in s)
            {
                //Nếu phía trước ký tự này là khoảng cách thì "kệ" ký tự hiện tại
                if (space.IndexOf(prc) >= 0)
                    rs += c.ToString();
                else
                    rs += c.ToString().ToLower();
                prc = c;
            }
            return rs;
        }
        public static string ThuongTatCa(string s)
        {
            return s.ToLower();
        }
        #endregion

        #region chuỗi/mảng các bảng mã, đừng sửa trong này! nguy hiểm!
        static string strTCVN =    "¸µ¶·¹¨¾»¼½Æ©ÊÇÈÉËÐÌÎÏÑªÕÒÓÔÖÝ×ØÜÞãßáâä«èåæçé¬íêëìîóïñòô­øõö÷ùýúûüþ®¸µ¶·¹¡¾»¼½Æ¢ÊÇÈÉËÐÌÎÏÑ£ÕÒÓÔÖÝ×ØÜÞãßáâä¤èåæçé¥íêëìîóïñòô¦øõö÷ùýúûüþ§§";
        static int[] TCVN = { 184, 181, 182, 183, 185, 168, 190, 187, 188, 189, 198, 169, 202, 199, 200, 201, 203, 208, 204, 206, 207, 209, 170, 213, 210, 211, 212, 214, 221, 215, 216, 220, 222, 227, 223, 225, 226, 228, 171, 232, 229, 230, 231, 233, 172, 237, 234, 235, 236, 238, 243, 239, 241, 242, 244, 173, 248, 245, 246, 247, 249, 253, 250, 251, 252, 254, 174, 184, 181, 182, 183, 185, 161, 190, 187, 188, 189, 198, 162, 202, 199, 200, 201, 203, 208, 204, 206, 207, 209, 163, 213, 210, 211, 212, 214, 221, 215, 216, 220, 222, 227, 223, 225, 226, 228, 164, 232, 229, 230, 231, 233, 165, 237, 234, 235, 236, 238, 243, 239, 241, 242, 244, 166, 248, 245, 246, 247, 249, 253, 250, 251, 252, 254, 167, 167 };


        static string strUNICODE = "áàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐÐ";//Đ272, Ð208 
        static int[] UNICODE = {
            225, 224, 7843, 227, 7841, 259, 7855, 7857, 7859, 7861, 7863, 226, 7845, 7847, 7849, 7851, 7853,
            233, 232, 7867, 7869, 7865, 234, 7871, 7873, 7875, 7877, 7879, 237, 236, 7881, 297, 7883, 243, 242, 7887, 245, 7885, 244, 7889, 7891, 7893, 7895, 7897, 417, 7899, 7901, 7903, 7905, 7907, 250, 249, 7911, 361, 7909, 432, 7913, 7915, 7917, 7919, 7921, 253, 7923, 7927, 7929, 7925, 273, 193, 192, 7842, 195, 7840, 258, 7854, 7856, 7858, 7860, 7862, 194, 7844, 7846, 7848, 7850, 7852, 201, 200, 7866, 7868, 7864, 202, 7870, 7872, 7874, 7876, 7878, 205, 204, 7880, 296, 7882, 211, 210, 7886, 213, 7884, 212, 7888, 7890, 7892, 7894, 7896, 416, 7898, 7900, 7902, 7904, 7906, 218, 217, 7910, 360, 7908, 431, 7912, 7914, 7916, 7918, 7920, 221, 7922, 7926, 7928, 7924, 272, 208 };

        //use for U to VNI
        static string strUNICODE2 = ",á ,à ,ả ,ã ,ạ ,ă ,ắ ,ằ ,ẳ ,ẵ ,ặ ,â ,ấ ,ầ ,ẩ ,ẫ ,ậ ,é ,è ,ẻ ,ẽ ,ẹ ,ê ,ế ,ề ,ể ,ễ ,ệ ,í,ì,ỉ,ĩ,ị,ó ,ò ,ỏ ,õ ,ọ ,ô ,ố ,ồ ,ổ ,ỗ ,ộ ,ơ,ớ ,ờ ,ở ,ỡ ,ợ ,ú ,ù ,ủ ,ũ ,ụ ,ư,ứ ,ừ ,ử ,ữ ,ự ,ý ,ỳ ,ỷ ,ỹ ,ỵ,đ,Á ,À ,Ả ,Ã ,Ạ ,Ă ,Ắ ,Ằ ,Ẳ ,Ẵ ,Ặ ,Â ,Ấ ,Ầ ,Ẩ ,Ẫ ,Ậ ,É ,È ,Ẻ ,Ẽ ,Ẹ ,Ê ,Ế ,Ề ,Ể ,Ễ ,Ệ ,Í,Ì,Ỉ,Ĩ,Ị,Ó ,Ò ,Ỏ ,Õ ,Ọ ,Ô ,Ố ,Ồ ,Ổ ,Ỗ ,Ộ ,Ơ,Ớ ,Ờ ,Ở ,Ỡ ,Ợ ,Ú ,Ù ,Ủ ,Ũ ,Ụ ,Ư,Ứ ,Ừ ,Ử ,Ữ ,Ự ,Ý ,Ỳ ,Ỷ ,Ỹ ,Ỵ,Đ,Ð,";//272, 208"
        static string strVNI = ",aù,aø,aû,aõ,aï,aê,aé,aè,aú,aü,aë,aâ,aá,aà,aå,aã,aä,eù,eø,eû,eõ,eï,eâ,eá,eà,eå,eã,eä,í,ì,æ,ó,ò,où,oø,oû,oõ,oï,oâ,oá,oà,oå,oã,oä,ô,ôù,ôø,ôû,ôõ,ôï,uù,uø,uû,uõ,uï,ö,öù,öø,öû,öõ,öï,yù,yø,yû,yõ,î,ñ,AÙ,AØ,AÛ,AÕ,AÏ,AÊ,AÉ,AÈ,AÚ,AÜ,AË,AÂ,AÁ,AÀ,AÅ,AÃ,AÄ,EÙ,EØ,EÛ,EÕ,EÏ,EÂ,EÁ,EÀ,EÅ,EÃ,EÄ,Í,Ì,Æ,Ó,Ò,OÙ,OØ,OÛ,OÕ,OÏ,OÂ,OÁ,OÀ,OÅ,OÃ,OÄ,Ô,ÔÙ,ÔØ,ÔÛ,ÔÕ,ÔÏ,UÙ,UØ,UÛ,UÕ,UÏ,Ö,ÖÙ,ÖØ,ÖÛ,ÖÕ,ÖÏ,YÙ,YØ,YÛ,YÕ,Î,Ñ,Ñ,";

        //use for Vni to
        static string strVNI1 = "aaaaaaaaaaaaaaaaaeeeeeeeeeeeíìæóòoooooooooooôôôôôôuuuuuööööööyyyyîñAAAAAAAAAAAAAAAAAEEEEEEEEEEEÍÌÆÓÒOOOOOOOOOOOÔÔÔÔÔÔUUUUUÖÖÖÖÖÖYYYYÎÑ";
        static string strVNI2 = "ùøûõïêéèúüëâáàåãäùøûõïâáàåãäùøûõïâáàåãäùøûõïùøûõïùøûõïùøûõÙØÛÕÏÊÉÈÚÜËÂÁÀÅÃÄÙØÛÕÏÂÁÀÅÃÄÙØÛÕÏÂÁÀÅÃÄÙØÛÕÏÙØÛÕÏÙØÛÕÏÙØÛÕ";
        static string strVNI3 = "íìæóòôöîñÍÌÆÓÒÔÖÎÑ";
        static string[] strVNIarray = "aù,aø,aû,aõ,aï,aê,aé,aè,aú,aü,aë,aâ,aá,aà,aå,aã,aä,eù,eø,eû,eõ,eï,eâ,eá,eà,eå,eã,eä,í,ì,æ,ó,ò,où,oø,oû,oõ,oï,oâ,oá,oà,oå,oã,oä,ô,ôù,ôø,ôû,ôõ,ôï,uù,uø,uû,uõ,uï,ö,öù,öø,öû,öõ,öï,yù,yø,yû,yõ,î,ñ,AÙ,AØ,AÛ,AÕ,AÏ,AÊ,AÉ,AÈ,AÚ,AÜ,AË,AÂ,AÁ,AÀ,AÅ,AÃ,AÄ,EÙ,EØ,EÛ,EÕ,EÏ,EÂ,EÁ,EÀ,EÅ,EÃ,EÄ,Í,Ì,Æ,Ó,Ò,OÙ,OØ,OÛ,OÕ,OÏ,OÂ,OÁ,OÀ,OÅ,OÃ,OÄ,Ô,ÔÙ,ÔØ,ÔÛ,ÔÕ,ÔÏ,UÙ,UØ,UÛ,UÕ,UÏ,Ö,ÖÙ,ÖØ,ÖÛ,ÖÕ,ÖÏ,YÙ,YØ,YÛ,YÕ,Î,Ñ,Ñ".Split(',');
        #endregion chuỗi/mảng các bảng mã

        static string ASCII_UNICODE_To_String(int ascii_unicode)
        {
            return Convert.ToChar(ascii_unicode).ToString();
        }
        static int Char_To_ASCII_UNICODE(string str)
        {
            if (str.Length > 0)
                str = str.Substring(0, 1);
            else return 0;
            return Convert.ToChar(str);
        }

        /// <summary>
        /// Nhận dạng mã tiếng Việt: Còn sơ sài nhưng sài khá tốt :D
        /// Dựa trên cơ chế đếm các ký tự đặc biệt chỉ có riêng ở các bảng mã
        /// Duyệt qua 1024 ký tự đầu tiên, nếu ký tự nào có mặt trong chuỗi ký
        ///     tự đặc biệt của mỗi bảng mã sẽ tăng điểm cho bảng mã đó.
        ///     độ ưu tiên đặc biệt dành cho bảng mã VNI vì bảng mã này phải
        ///     dùng 2 ký tự liên tục để tạo thành 1 ký tự Vni
        ///     Nếu điểm cho bảng mã VNI quá thấp sẽ so sánh điểm của 2 bảng
        ///     mã còn lại. Hai bảng mã UNICODE và TCVN thì dễ dàng so sánh hơn
        ///     do các ký tự trên hai bảng mã này có mã ASCII gần như khác nhau.
        /// </summary>
        /// <param name="VnString">Chuỗi cần nhận dạng mã</param>
        /// <returns></returns>
        public static string NhanDangMaTiengViet(string VnString)
        {
            int UnicodeCount = 0;
            int TcvnCount = 0;
            int VniCount = 0;
            int VniCount2 = 0;
            string strFlag = "U";
            int intFlag = 0;

            int MaxCount = 1024;
            if (VnString.Length < 1024)
            {
                MaxCount = VnString.Length;
            }
            try
            {
                for (int i = 0; i < MaxCount; i++)
                {
                    string strCurrentChar = VnString.Substring(i, 1);
                    if (strUNICODE.Contains(strCurrentChar))
                    {
                        UnicodeCount++;
                    }
                    if (strTCVN.Contains(strCurrentChar))
                    {
                        TcvnCount++;
                    }
                    if ((VnString.Substring(i).Length >= 2 && (strVNI1.Contains(VnString.Substring(i, 1)) && strVNI2.Contains(VnString.Substring(i + 1, 1))))
                        )//||strVNI3.Contains(strCurrentChar)
                    {
                        VniCount++;
                    }
                    if (strVNI3.Contains(strCurrentChar))
                    {
                        VniCount2++;
                    }
                }
                //Dac biet                
                if (UnicodeCount == 0 && TcvnCount == 0 && VniCount == 0)
                    return "UNICODE";
                if ((VniCount > 0 || VniCount2 > 1) &&
                    (float)(VniCount + VniCount2) / (float)TcvnCount > 0.75 &&
                    (float)(VniCount + VniCount2) / (float)UnicodeCount > 0.75)
                    return "VNI";

                if (UnicodeCount > 0)
                {
                    strFlag = "UNICODE"; intFlag = UnicodeCount;
                    if ((intFlag > 0 && intFlag < TcvnCount) || (intFlag > 1 && intFlag <= TcvnCount))
                    {
                        strFlag = "TCVN";
                        intFlag = TcvnCount;
                        if (intFlag < VniCount + VniCount2)
                        {
                            strFlag = "VNI";
                            intFlag = VniCount;
                        }
                    }
                }
                else
                {
                    strFlag = "TCVN";
                    intFlag = TcvnCount;
                    if (intFlag < VniCount + VniCount2)
                    {
                        strFlag = "VNI";
                        intFlag = VniCount;
                    }
                }

                return strFlag;
            }
            catch
            {
                return "UNICODE";
            }
        }

        /// <summary>
        /// Tự động nhận dạng mã và chuyển sang UNICODE
        /// </summary>
        /// <param name="VnString">VnString</param>
        /// <returns>UniVnString</returns>
        public static string VIETNAM_CONVERT(string VnString)
        {
            return VIETNAM_CONVERT_ToUNICODE(VnString);
        }
        /// <summary>
        /// Tự động đoán mã nguồn và chuyển về vni
        /// </summary>
        /// <param name="VnString"></param>
        /// <returns></returns>
        public static string VIETNAM_CONVERT_ToVNI(string VnString)
        {
            string strMaNguon = NhanDangMaTiengViet(VnString);
            return VIETNAM_CONVERT(VnString, strMaNguon, "VNI");
        }
        /// <summary>
        /// Tự động đoán mã nguồn và chuyển về abc
        /// </summary>
        /// <param name="VnString"></param>
        /// <returns></returns>
        public static string VIETNAM_CONVERT_ToTCVN(string VnString)
        {
            string strMaNguon = NhanDangMaTiengViet(VnString);
            return VIETNAM_CONVERT(VnString, strMaNguon, "TCVN");
        }
        /// <summary>
        /// Tự động đoán mã nguồn và chuyển về unicode
        /// </summary>
        /// <param name="VnString"></param>
        /// <returns></returns>
        public static string VIETNAM_CONVERT_ToUNICODE(string VnString)
        {
            string strMaNguon = NhanDangMaTiengViet(VnString);
            return VIETNAM_CONVERT_ToUNICODE(VnString, strMaNguon);
        }
        public static string VIETNAM_CONVERT_ToUNICODE(string VnString, string form)
        {
            return VIETNAM_CONVERT(VnString, form, "UNICODE");
        }
        public static string VIETNAM_CONVERT(string VnString, string form, string to)
        {
            string strUniTemp = "";
            switch (form.ToUpper())
            {
                case "U":
                case "UNI":
                case "UNICODE":
                    {
                        strUniTemp = VnString;
                        break;
                    }
                case "A":
                case "ABC":
                case "TCVN":
                case "TCVN3":
                    {
                        strUniTemp = TCVNtoUNICODE(VnString);
                        break;
                    }
                case "V":
                case "VNI":
                    {
                        strUniTemp = VNItoUNICODE(VnString);
                        break;
                    }
                default:
                    strUniTemp = VIETNAM_CONVERT_ToUNICODE(VnString);
                    break;
            }

            switch (to.ToUpper())
            {
                case "U":
                case "UNI":
                case "UNICODE":
                    {
                        return strUniTemp;
                    }
                case "A":
                case "ABC":
                case "TCVN":
                case "TCVN3":
                    {
                        return UNICODEtoTCVN(strUniTemp);
                    }
                case "V":
                case "VNI":
                    {
                        return UNICODEtoVNI(strUniTemp);
                    }
                default:
                    return strUniTemp;
            }
        }

        public static string VNItoUNICODE(string VniString)
        {
            string strReturn = "";
            int charIndex = -1;
            for (int i = 0; i < VniString.Length; i++)
            {
                if (strVNI3.IndexOf(VniString[i]) >= 0)//trường hợp 1 ký tự vni đặc biệt
                {
                    charIndex = strVNI.IndexOf("," + VniString[i] + ",") + 1;
                    strReturn += strUNICODE2[charIndex];
                }
                //Nếu chuỗi vẫn còn đủ 2 ký tự
                //Và hai ký tự liên tiếp hợp thành 1 ký tự VNI
                else
                    if (VniString.Substring(i).Length >= 2)
                    {
                        //Neu van con du 2 ky tu
                        charIndex = strVNI.IndexOf("," + VniString.Substring(i, 2) + ",");
                        //Neu co trong chuoi vni thi lay tuong ung trong chuoi unicode
                        if (charIndex >= 0)
                        {
                            strReturn += strUNICODE2[charIndex + 1];//+1 cho dau ,
                            i++;
                        }
                        else
                        {
                            strReturn += VniString[i];
                        }

                    }
                    else
                    {
                        strReturn += VniString.Substring(i, 1);
                    }
            }
            return strReturn;
        }
        public static string UNICODEtoVNI(string UniString)
        {
            string strReturn = "";
            int charIndex = -1;
            foreach (char item in UniString)
            {
                charIndex = strUNICODE.IndexOf(item);
                if (charIndex >= 0)
                {
                    strReturn += strVNIarray[charIndex];
                }
                else
                {
                    strReturn += item;
                }
            }
            return strReturn;
        }

        public static string TCVNtoUNICODE(string TcnvString)
        {
            string strReturn = "";
            int charIndex = -1;
            foreach (char item in TcnvString)
            {
                //Nếu chữ này nằm trong mãng ký tự TCVN thì chuyển thành UNICODE và gán vào strReturn
                charIndex = strTCVN.IndexOf(item);
                if (charIndex >= 0)
                {
                    strReturn += strUNICODE[charIndex];
                }
                else strReturn += item;
            }
            return strReturn;
        }

        public static string UNICODEtoTCVN(string UniString)
        {
            string strReturn = "";
            int charIndex = -1;
            foreach (char item in UniString)
            {
                //Nếu chữ này nằm trong mãng ký tự UNICODE thì chuyển thành TCVN và gán vào strReturn
                charIndex = strUNICODE.IndexOf(item);
                if (charIndex >= 0)
                {
                    strReturn += strTCVN[charIndex];
                }
                else strReturn += item;
            }
            return strReturn;
        }

        /// <summary>
        /// Chuyển về Tiếng Việt không dấu.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToUnSign(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }
    }
}
