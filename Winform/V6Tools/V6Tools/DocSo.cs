using System;
using System.Globalization;

namespace V6Tools
{
    public class DocSo
    {
        public static string MoneyToWords(decimal money, string lang, string ma_nt)
        {
            if (lang == "V")
            {
                return DocSo.DOI_SO_CHU_NEW(money, V6Alnt_begin1(ma_nt), V6Alnt_end1(ma_nt), V6Alnt_only1(ma_nt), V6Alnt_point1(ma_nt),
                    V6Alnt_endpoint1(ma_nt));
            }
            else
            {
                return DocSo.NumWordsWrapper(money, V6Alnt_begin2(ma_nt), V6Alnt_end2(ma_nt), V6Alnt_only2(ma_nt), V6Alnt_point2(ma_nt),
                    V6Alnt_endpoint2(ma_nt));
            }
        }
        #region ==== ALNT ====
        private static string V6Alnt_endpoint1(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "xu";
                case "EUR":
                    return "";
                case "USD":
                    return "cent";
                case "VND":
                    return "";
                default:
                    return "";
            }
        }

        private static string V6Alnt_point1(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "phẩy";
                case "EUR":
                    return "";
                case "USD":
                    return "phẩy";
                case "VND":
                    return "phẩy";
                default:
                    return "";
            }
        }

        private static string V6Alnt_only1(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "";
                case "EUR":
                    return "";
                case "USD":
                    return "";
                case "VND":
                    return "chẵn";
                default:
                    return "";
            }
        }

        private static string V6Alnt_end1(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "đô la Úc";
                case "EUR":
                    return "Euro";
                case "USD":
                    return "đô la Mỹ";
                case "VND":
                    return "đồng";
                default:
                    return "";
            }
        }

        private static string V6Alnt_begin1(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "";
                case "EUR":
                    return "";
                case "USD":
                    return "";
                case "VND":
                    return "";
                default:
                    return "";
            }
        }

        private static string V6Alnt_endpoint2(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "Cent(s)";
                case "EUR":
                    return "";
                case "USD":
                    return "Cent(s)";
                case "VND":
                    return "";
                default:
                    return "";
            }
        }

        private static string V6Alnt_point2(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "and";
                case "EUR":
                    return "";
                case "USD":
                    return "and";
                case "VND":
                    return "point";
                default:
                    return "";
            }
        }

        private static string V6Alnt_only2(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "only";
                case "EUR":
                    return "";
                case "USD":
                    return "only";
                case "VND":
                    return "";
                default:
                    return "";
            }
        }

        private static string V6Alnt_end2(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "AUD Dollars";
                case "EUR":
                    return "";
                case "USD":
                    return "Dollars";
                case "VND":
                    return "VND";
                default:
                    return "";
            }
        }

        private static string V6Alnt_begin2(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "";
                case "EUR":
                    return "";
                case "USD":
                    return "";
                case "VND":
                    return "";
                default:
                    return "";
            }
        }
        #endregion ==== ALNT ====

        //========================== Đọc số V6 - Tuanmh ========================
        //============================= Encode - Huuan =========================
        //
        private static readonly string[] MangChuSo = {" không"," một"," hai"," ba"," bốn"," năm"," sáu"," bảy"," tám"," chín"};
        private static string Tu_Bon = " bốn";
        private static readonly string decimalSymbol = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="so"></param>
        /// <param name="begin1"></param>
        /// <param name="end1">Đồng</param>
        /// <param name="only1">Chẵn</param>
        /// <param name="point1">Phẩy</param>
        /// <param name="endPoint1">xu/cent</param>
        /// <returns></returns>
        public static string DOI_SO_CHU_NEW(decimal so, string begin1, string end1, string only1, string point1, string endPoint1)
        {
            var result = "";
            if (string.IsNullOrEmpty(endPoint1))
	        {
                result = DOI_SO_CHU(so + ";" + end1 + " " + only1);
            }
            else
            {
                string[] chanle = so.ToString(CultureInfo.InvariantCulture).Split(decimalSymbol[0]);
                if (chanle.Length==2 && int.Parse(chanle[1]) > 0)
                {
                    result = DOI_SO_CHU(chanle[0] + ";" + end1) + " và " + DOI_SO_CHU(chanle[1] + ";" + endPoint1).ToLower();
                }
                else
                {
                    result = DOI_SO_CHU(chanle[0] + ";" + end1 + " " + only1);
                }
            }

            if (string.IsNullOrEmpty(begin1))
            {
                return ChuyenMaTiengViet.HoaDauDoan(result);
            }
            else
            {
                return begin1.Trim() + " " + ChuyenMaTiengViet.HoaDauDoan(result);
            }
        }
        /// <summary>
        /// Đọc số - tuanmh
        /// </summary>
        /// <param name="strParameters">strParameters= "nSo;DonViTinh;Doc_So_Bon"
        /// ví dụ: "1234500.01;Đồng;T"
        /// T đọc là tư, khác đọc 4</param>
        /// <returns></returns>
        public static string DOI_SO_CHU(string strParameters)
        {

            // strParameters= "nSo;DonViTinh;Doc_So_Bon"
            // ví dụ        : "1234500.01;Đồng;T"
            string[] PARAMETERS = strParameters.Split(';');
            
            //long nSo = 0;
            string DonViTinh = "";
            string Doc_So_Bon = "";

            decimal So;
            if (PARAMETERS[0] != "")
                So = V6Convert.Number.ToDecimalV6(PARAMETERS[0]);
            else
                So = 0;

            if (PARAMETERS.Length < 3)
            {
                Doc_So_Bon = "";
            }
            else
            {
                Doc_So_Bon = PARAMETERS[2];
            }
            //    IF PARAMETER() < 2
            //        DonViTinh = []
            //    ENDIF
            if (PARAMETERS.Length < 2)
            {
                DonViTinh = "";
            }
            else
            {
                DonViTinh = PARAMETERS[1];
            }
            //    Tu_Bon = ' bốn'
            Tu_Bon = " bốn";
            //    IF UPPER(Doc_So_Bon) = 'T'
            //        Tu_Bon = ' tư'
            //    ENDIF
            if (Doc_So_Bon.ToUpper() == "T")
            {
                Tu_Bon = " tư";
            }

            string SoChu = "";
            
            if (So < 0)
            {
                So = -So;
                SoChu = SoChu + " âm";
            }

            string So_sau_dau_phay = "0";
            

            if (So.ToString(CultureInfo.InvariantCulture).Contains(decimalSymbol))
            {
                So_sau_dau_phay = (So.ToString(CultureInfo.InvariantCulture).Split(decimalSymbol[0]))[1];
            }
            if (So_sau_dau_phay.Length > 6)//0.001234???
            {
                So_sau_dau_phay = So_sau_dau_phay.Substring(0, 6);
            }
            //****Doc phan nguyen truoc	
            //    So = INT(So)
            So = (long)So;
            if (So == 0)
            {
                SoChu = SoChu + " " + "không0";
            }
            else
            {
                string StrSo = So.ToString(CultureInfo.InvariantCulture).Trim();
                if (StrSo.Length % 3 == 1)
                {
                    StrSo = "00" + StrSo;
                }
                //        IF MOD(LEN (StrSo), 3 ) = 2
                //            StrSo = '0' + StrSo
                //        ENDIF
                if (StrSo.Length % 3 == 2)
                {
                    StrSo = "0" + StrSo;
                }
                //        So_Nhom = LEN(StrSo)/3
                //        Stt_Nhom = So_Nhom
                //        Stt_So = 1
                int So_Nhom = StrSo.Length / 3;
                int Stt_Nhom = So_Nhom;
                int Stt_So = 0;
                while (Stt_So <= StrSo.Length - 2)//&&Cho den vi tri cua chu so dau tien cua nhom cuoi cung
                {
                    string So_Phu = StrSo.Substring(Stt_So, 3);
                    Stt_Nhom = (StrSo.Length - Stt_So + 1) / 3;
                    if (DOI_SO_CHU_NHOM(So_Phu, Stt_Nhom, So_Nhom).Length > 0)
                    {
                        if (Stt_Nhom < 5)
                        {
                            SoChu = SoChu + DOI_SO_CHU_NHOM(So_Phu, Stt_Nhom, So_Nhom) + ",";
                        }
                        else
                        {
                            SoChu = SoChu + DOI_SO_CHU_NHOM(So_Phu, Stt_Nhom, So_Nhom);
                        }
                    }
                    else
                    {
                        SoChu = SoChu + DOI_SO_CHU_NHOM(So_Phu, Stt_Nhom, So_Nhom);
                    }
                    Stt_So = Stt_So + 3;
                }
                //    ENDIF
            }
            //    SoChu = LEFT(ALLT(SoChu), LEN(ALLT(SoChu)) - 1)
            SoChu = SoChu.Trim().Substring(0, SoChu.Trim().Length - 1);

            //****Doc phan thap phan	
            //    * Neu chu so dau tien sau dau phay khac khong thi doc nhu phan so nguyen
            //    IF So_sau_dau_phay > 0
            if (Convert.ToDouble(So_sau_dau_phay) > 0)
            {
                SoChu = SoChu + " phẩy";

                //        IF LEFT(So_sau_dau_phay, 1) <> '0'
                if (So_sau_dau_phay.Substring(0, 1) != "0")
                {
                    //            &&Cat nhung so 0 o ben phai do STR() di
                    //            DO WHILE RIGHT(So_sau_dau_phay, 1) = '0'
                    //                So_sau_dau_phay = LEFT(So_sau_dau_phay, LEN(So_sau_dau_phay) - 1)
                    //            ENDDO
                    while (So_sau_dau_phay.Substring(So_sau_dau_phay.Length - 1) == "0")
                    {
                        So_sau_dau_phay = So_sau_dau_phay.Substring(0, So_sau_dau_phay.Length - 1);
                    }
                    //            &&Cong them cac so khong vao dang truoc chuoi de duoc chan cac to hop 3 so
                    //            IF MOD(LEN(So_sau_dau_phay) , 3 ) = 1
                    //                So_sau_dau_phay = '00' + So_sau_dau_phay
                    //            ENDIF	
                    if (So_sau_dau_phay.Length % 3 == 1)
                    {
                        So_sau_dau_phay = "00" + So_sau_dau_phay;
                    }
                    //            IF MOD(LEN(So_sau_dau_phay), 3 ) = 2
                    //                So_sau_dau_phay = '0' + So_sau_dau_phay
                    //            ENDIF
                    if (So_sau_dau_phay.Length % 3 == 2)
                    {
                        So_sau_dau_phay = "0" + So_sau_dau_phay;
                    }
                    //            So_Nhom = ROUND(LEN(So_sau_dau_phay) / 3,0) 
                    //            Stt_Nhom = So_Nhom
                    //            Stt_So = 1
                    int So_Nhom = So_sau_dau_phay.Length / 3;
                    int Stt_Nhom = So_Nhom;
                    int Stt_So = 0;
                    while (Stt_So <= So_sau_dau_phay.Length - 2)
                    {
                        string So_Phu = So_sau_dau_phay.Substring(Stt_So, 3);
                        Stt_Nhom = (So_sau_dau_phay.Length - Stt_So + 1) / 3;//Không hiểu sao hàm Math.Round không dùng được ở đây 
                        SoChu = SoChu + DOI_SO_CHU_NHOM(So_Phu, Stt_Nhom, So_Nhom);
                        Stt_So = Stt_So + 3;
                    }
                }
                else
                {
                    while (So_sau_dau_phay.Length > 0 && So_sau_dau_phay.Substring(So_sau_dau_phay.Length - 1) == "0")
                    {
                        So_sau_dau_phay = So_sau_dau_phay.Substring(0, So_sau_dau_phay.Length - 1);
                    }
                    for (int i = 0; i < So_sau_dau_phay.Length; i++)
                    {
                        int j = int.Parse(So_sau_dau_phay.Substring(i, 1));
                        SoChu = SoChu + MangChuSo[j];
                    }
                    //        ENDIF
                }

                //    ENDIF
            }
            //    SoChu = ALLTR(UPPER(LEFT(SoChu, 1)) + SUBS(SoChu, 2) + [ ] + DonViTinh)
            SoChu = SoChu.Trim() + " " + DonViTinh;
            SoChu = SoChu.Replace(",", "");
            return SoChu;
        }

        private static string DOI_SO_CHU_NHOM(string Nhom_So, int Stt_Nhom, int So_Nhom)
        {
            //            FUNCTION DOI_SO_CHU_NHOM
            //PARAMETER Nhom_So, Stt_Nhom, So_Nhom
            //* Nhom_So: Nhom 3 so can doc
            //* Stt_Nhom: Stt cua nhom trong tong so cac nhom
            //* So_Nhom: Tong so nhom cua so can doc
            //PRIVATE Tram, Chuc, Don_Vi, Tram_Chu, Chuc_Chu, Don_Vi_Chu, Nhom_Chu
            int Tram, Chuc, Don_Vi;
            string Tram_Chu, Chuc_Chu, Don_Vi_Chu, Nhom_Chu;
            //    Nhom_Chu = ''
            //    Tram = LEFT(Nhom_So,1)
            //    Chuc = SUBSTR(Nhom_So,2,1)
            //    Don_Vi = RIGHT(Nhom_So,1)
            //    Tram_Chu = MangChuSo(VAL(Tram) +1 )
            //    Chuc_Chu = MangChuSo(VAL(Chuc) +1 )
            //    Don_Vi_Chu = MangChuSo(VAL(Don_Vi) +1 )
            Nhom_Chu = "";
            Tram = int.Parse(Nhom_So.Substring(0, 1));
            Chuc = int.Parse(Nhom_So.Substring(1, 1));
            Don_Vi = int.Parse(Nhom_So.Substring(2));
            Tram_Chu = MangChuSo[Tram];
            Chuc_Chu = MangChuSo[Chuc];
            Don_Vi_Chu = MangChuSo[Don_Vi];

            //    IF (Tram <> '0') .OR. ((Stt_Nhom = 1) .AND. (Stt_Nhom < So_Nhom)) 
            //        Nhom_Chu = Nhom_Chu + Tram_Chu + ' trăm'
            //    ENDIF
            //if (Tram != 0 || (Stt_Nhom ==1 && Stt_Nhom < So_Nhom))
            if (Tram != 0 || (Stt_Nhom < So_Nhom)) //11/05/2015
            {
                Nhom_Chu = Nhom_Chu + Tram_Chu + " trăm";
            }
            //    IF ALLTR(Chuc_Chu) <> 'không'
            if (Chuc_Chu.Trim() != "không")
            {
                //        IF ALLTR(Chuc_Chu) <> 'một'
                if (Chuc_Chu.Trim() != "một")
                {
                    //            Nhom_Chu = Nhom_Chu + Chuc_Chu + ' mươi'
                    Nhom_Chu = Nhom_Chu + Chuc_Chu + " mươi";
                    //            IF ALLTR(Don_Vi_Chu) <> 'năm'
                    if (Don_Vi_Chu.Trim() != "năm")
                    {
                        //                IF ALLTR(Don_Vi_Chu) <> 'không'
                        if (Don_Vi_Chu.Trim() != "không")
                        {
                            //                    IF ALLTR(Don_Vi_Chu) = 'một'
                            if (Don_Vi_Chu.Trim() == "một")
                            {
                                Nhom_Chu = Nhom_Chu + " mốt";
                                //                        Nhom_Chu = Nhom_Chu + ' mốt'
                            }
                            else
                            {
                                //                    ELSE
                                //                        IF (ALLTR(Don_Vi_Chu) = 'bốn') .AND. (Stt_Nhom > 1)
                                //                            Nhom_Chu = Nhom_Chu + Tu_Bon
                                //                        ELSE
                                //                            Nhom_Chu = Nhom_Chu + Don_Vi_Chu
                                //                        ENDIF
                                if (Don_Vi_Chu.Trim() == "bốn" && Stt_Nhom > 1)
                                    Nhom_Chu = Nhom_Chu + Tu_Bon;
                                else
                                    Nhom_Chu = Nhom_Chu + Don_Vi_Chu;
                                //                    ENDIF	
                            }
                            //                ENDIF	
                        }
                    }
                    else
                    {
                        //            ELSE
                        //                Nhom_Chu = Nhom_Chu + ' lăm'
                        Nhom_Chu = Nhom_Chu + " lăm";
                        //            ENDIF
                    }
                }
                else
                {
                    //        ELSE
                    //            Nhom_Chu = Nhom_Chu + ' mười'
                    //            IF ALLTR(Don_Vi_Chu) <> 'năm'
                    //                IF ALLTR(Don_Vi_Chu) <> 'không'
                    //                    Nhom_Chu = Nhom_Chu + Don_Vi_Chu	
                    //                ENDIF	
                    //            ELSE
                    //                Nhom_Chu = Nhom_Chu + ' lăm'
                    //            ENDIF		
                    Nhom_Chu = Nhom_Chu + " mười";
                    if (Don_Vi_Chu.Trim() != "năm")
                    {
                        if (Don_Vi_Chu.Trim() != "không")
                            Nhom_Chu = Nhom_Chu + Don_Vi_Chu;
                    }
                    else
                        Nhom_Chu = Nhom_Chu + " lăm";
                    //        ENDIF
                }
            }
            else
            {
                //    ELSE
                //        IF ALLTR(Don_Vi_Chu) <> 'không'
                //            Nhom_Chu = Nhom_Chu + ' linh'
                //            IF ALLTR(Don_Vi_Chu) <> 'bốn'
                //                Nhom_Chu = Nhom_Chu + Don_Vi_Chu		
                //            ELSE
                //                Nhom_Chu = Nhom_Chu + Tu_Bon
                //            ENDIF
                //        ENDIF	
                if (Don_Vi_Chu.Trim() != "không")
                {
                    Nhom_Chu = Nhom_Chu + " linh";
                    if (Don_Vi_Chu.Trim() != "bốn")
                        Nhom_Chu = Nhom_Chu + Don_Vi_Chu;
                    else
                        Nhom_Chu = Nhom_Chu + Tu_Bon;
                }
                //    ENDIF
            }
            if (Tram == 0 && Chuc == 0 && Don_Vi == 0 && Stt_Nhom > 1)
            {
                //    IF (Tram = '0') .AND. (Chuc = '0') .AND. (Don_Vi <> '0') .AND. (Stt_Nhom > 1)
                //        Nhom_Chu = Don_Vi_Chu
                Nhom_Chu = Don_Vi_Chu;
                //    ENDIF
            }
            if (Tram == 0 && Chuc == 0 && Don_Vi != 0 && So_Nhom == 1)
            {
                //    IF (Tram = '0') .AND. (Chuc = '0') .AND. (Don_Vi <> '0') .AND. (So_Nhom = 1)
                //        Nhom_Chu = Don_Vi_Chu
                Nhom_Chu = Don_Vi_Chu;
                //    ENDIF
            }
            //Huuan add
            if (Tram == 0 && Chuc == 0 && Don_Vi != 0 && Stt_Nhom == So_Nhom)
            {
                Nhom_Chu = Don_Vi_Chu;
            }
            //    DO CASE
            //        CASE Stt_Nhom = 2
            //            Nhom_Chu = Nhom_Chu + ' nghìn'
            //        CASE Stt_Nhom = 3
            //            Nhom_Chu = Nhom_Chu + ' triệu'
            //        CASE Stt_Nhom = 4
            //            Nhom_Chu = Nhom_Chu + ' tỷ'	
            //        CASE Stt_Nhom = 5
            //            Nhom_Chu = Nhom_Chu + ' nghìn'	
            //        CASE Stt_Nhom = 6
            //            Nhom_Chu = Nhom_Chu + ' triệu'	
            //    ENDCASE
            switch (Stt_Nhom)
            {
                case 2:
                    Nhom_Chu = Nhom_Chu + " nghìn";
                    break;
                case 3:
                    Nhom_Chu = Nhom_Chu + " triệu";
                    break;
                case 4:
                    Nhom_Chu = Nhom_Chu + " tỷ";
                    break;
                case 5:
                    Nhom_Chu = Nhom_Chu + " nghìn";
                    break;
                case 6:
                    Nhom_Chu = Nhom_Chu + " triệu";
                    break;
                default:
                    break;
            }
            //    IF (Tram = '0') .AND. (Chuc = '0') .AND. (Don_Vi = '0')
            //        Nhom_Chu = ''
            //    ENDIF
            if (Tram == 0 && Chuc == 0 && Don_Vi == 0)
            {
                Nhom_Chu = "";
            }
            //RETURN Nhom_Chu

            return Nhom_Chu;
        }

        //==================== Huuan add =============

        
        public static string DocSoTienLon(decimal soTien)
        {
            return DocSoTienLon(soTien.ToString(CultureInfo.InvariantCulture));
        }
        public static string DocSoTienLon(double soTien)
        {
            return DocSoTienLon(soTien.ToString(CultureInfo.InvariantCulture));
        }
        public static string DocSoTienLon(long soTien)
        {
            return DocSoTienLon(soTien.ToString());
        }
        
        /// <summary>
        /// Hàm đọc số tối ưu. Chỉ đọc đến 6 số phần lẽ. Số đưa vào nên loại bỏ dấu cách. Dấu thập phân là chấm (.)
        /// </summary>
        /// <param name="soTien"></param>
        /// <returns></returns>
        public static string DocSoTienLon(string soTien)
        {
            string strReturn = "";
            //Xử lý chuỗi số trước khi đọc
            soTien = soTien.Trim();
            
            if (!KiemTraChuoiSoLe(soTien))
            {
                return "Chuỗi số sai!";
            }            
            
            string soTien_Chan = "";
            string soTien_Le = "";
            //nếu có phần lẽ
            try
            {
                if (soTien.Contains(decimalSymbol))
                {
                    soTien_Chan = soTien.Split(decimalSymbol[0])[0];
                    soTien_Le   = soTien.Split(decimalSymbol[0])[1];
                }
                else
                {
                    soTien_Chan = soTien;
                    soTien_Le = "";
                }

                //Chỉ đọc đến 6 số phần lẽ:
                if (soTien_Le.Length > 6)
                {
                    soTien_Le = soTien_Le.Substring(0, 6);
                }

                //Loại bỏ số 0 ở trước phần chẵn
                while (soTien_Chan.Length>1 && soTien_Chan.Substring(0,1) == "0")
                {
                    soTien_Chan = soTien_Chan.Substring(1);
                }
                //Loại bỏ số 0 sau phần lẽ luôn
                while (soTien_Le.Length>1 && soTien_Le.Substring(soTien_Le.Length-1)=="0")
                {
                    soTien_Le = soTien_Le.Substring(0, soTien_Le.Length - 1);
                }
                
                if (soTien_Le != "" && Convert.ToInt32(soTien_Le) > 0)
                {                    
                    strReturn = DocSoLon(soTien_Chan) + " (phẩy) " + DocSoThanhChu_PhanLe(soTien_Le) + " đồng";
                }
                else
                {
                    strReturn = DocSoLon(soTien_Chan) + " đồng chẵn";
                }
                //
                strReturn = strReturn.Trim();
                //Nếu chứa "  "(hai khoảng trắng liên tục) thì thay bằng " "(một khoảng trắng)
                while (strReturn.Contains("  "))
                    strReturn = strReturn.Replace("  ", " ").Trim();
                //Viết hoa chữ đầu
                strReturn = strReturn.Substring(0, 1).ToUpper() + strReturn.Substring(1).ToLower();

                return strReturn;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }        
        private static bool KiemTraChuoiSoLe(string strChuoiSoLe)
        {            
            int soDauCham = 0;
            foreach (char item in strChuoiSoLe)
            {
                if (item==decimalSymbol[0])
                {
                    soDauCham++;
                }
            }
            if (soDauCham>1)
            {
                return false;
            }

            if (soDauCham==1)
            {
                string[] strTemp = strChuoiSoLe.Split(decimalSymbol[0]);

                if (!KiemTraChuoiSo(strTemp[0]))
                    return false;
                if (strTemp[1].Contains("-") || strTemp[1].Contains("+"))
                    return false;
                if (!KiemTraChuoiSo(strTemp[1]))
                    return false;                
            }
            else
            {
                return KiemTraChuoiSo(strChuoiSoLe);
            }

            return true;
        }
                
        public static string DocSoLon(long SoLon)
        {
            return DocSoLon(SoLon.ToString());
        }                
        public static string DocSoLon(string strSoLon)
        {
            string strReturn = "";
            bool âm = false;
            if (!KiemTraChuoiSo(strSoLon))
            {
                return "Chuỗi số sai";
            }
            //Xử lý chuỗi số trước khi đọc
            strSoLon = strSoLon.Trim();
            //loại bỏ dấu âm:
            while (strSoLon.Length>1 && (strSoLon.Substring(0, 1) == "-" 
                                        || strSoLon.Substring(0, 1) == "+"
                                        || strSoLon.Substring(0, 1) == " "))
            {
                if (strSoLon.Substring(0, 1) == "-")
                {
                    âm = !âm;                    
                }
                strSoLon = strSoLon.Substring(1);
            }
            //Bỏ các số "0" đằng trước, vẫn để dành lại một số để đọc :D 
            while (strSoLon.Length > 1 && strSoLon.Substring(0, 1) == "0")
            {
                strSoLon = strSoLon.Substring(1);
            }
            if (strSoLon=="")
            {
                return "Không";
            }
            //Chia thành các nhóm 9 số để đọc
            int SoChuSo = strSoLon.Length;
            int SoCapTy = 0;
            while (SoChuSo>0)
            {
                if (SoChuSo >= 9)
                {
                    strReturn = (Convert.ToInt32(strSoLon.Substring(SoChuSo - 9)) == 0 ? "" : DocSoThanhChu(strSoLon.Substring(SoChuSo - 9)) + " "+ ChuTy(SoCapTy) + " ") + strReturn;

                    SoChuSo = SoChuSo - 9;
                    strSoLon = strSoLon.Substring(0, SoChuSo);
                }
                else
                {
                    strReturn = (Convert.ToInt32(strSoLon) == 0 ? "" : DocSoThanhChu(strSoLon) + " " + ChuTy(SoCapTy) + " ") + strReturn;           
                    SoChuSo = 0;
                    strSoLon = "";
                }
                SoCapTy++;
            }
            //Gán phần âm
            if (âm)
            {
                strReturn = "Âm " + strReturn;
            }
            //Loại khoảng trắng dư
            strReturn = strReturn.Trim();
            while (strReturn.Contains("  "))
            {
                strReturn = strReturn.Replace("  ", " ");            
            }
            //Viết hoa chữ đầu:
            if(strReturn.Length>0)
            strReturn = strReturn.Substring(0, 1).ToUpper() + strReturn.Substring(1).ToLower();

            return strReturn;
        }
        
        private static string ChuTy(int SoCapTy)
        {
            if (SoCapTy <= 0)
                return "";
            else if (SoCapTy == 1)
                return "tỷ";
            else return ChuTy(SoCapTy - 1) + "_tỷ";
        }
        
        private static bool KiemTraChuoiSo(string strChuoiSo)
        {
            //loại bỏ dấu +- ở đầu chuỗi trước
            while (strChuoiSo.Length > 0 && (strChuoiSo.Substring(0, 1) == "-"
                                            || strChuoiSo.Substring(0, 1) == "+"
                                            || strChuoiSo.Substring(0, 1) == " "))
            {
                strChuoiSo = strChuoiSo.Substring(1);
            }
            if (strChuoiSo.Contains("-") || strChuoiSo.Contains("+") || strChuoiSo.Contains(" "))
            {
                return false;
            }
            //Duyệt qua từng đoạn 9 số để kiểm tra (có thể làm nhiều hơn 9 số tùy ngôn ngữ hỗ trợ)
            while (strChuoiSo.Length > 0)
            {
                int temp;
                if (strChuoiSo.Length > 9)
                {
                    try
                    {
                        temp = int.Parse(strChuoiSo.Substring(0, 9));
                    }
                    catch
                    {
                        return false;
                    }
                    strChuoiSo = strChuoiSo.Substring(9);
                }
                else
                {
                    try
                    {
                        temp = int.Parse(strChuoiSo);
                    }
                    catch
                    {
                        return false;
                    }
                    strChuoiSo = "";
                }
            }
            //Làm chuỗi số cũng khá khó khăn nhỉ :D
            return true;
        }

        public static string DocSoTien(Decimal soTien)
        {
            long soTien_Chan = 0;
            string soTien_Le = "";
            
            //nếu có phần lẽ
            try
            {
                if (soTien.ToString(CultureInfo.InvariantCulture).Contains(decimalSymbol))
                {
                    soTien_Chan = Convert.ToInt64(soTien.ToString(CultureInfo.InvariantCulture).Split(decimalSymbol[0])[0]);
                    soTien_Le = soTien.ToString(CultureInfo.InvariantCulture).Split(decimalSymbol[0])[1];
                }
                else
                {
                    soTien_Chan = (long)soTien;
                    soTien_Le = "";
                }

                //Chỉ đọc đến 6 số phần lẽ:
                if (soTien_Le.Length > 6)
                {
                    soTien_Le = soTien_Le.Substring(0, 6);
                }

                string strReturn = "";
                if (soTien_Le != "" && Convert.ToInt32(soTien_Le) > 0)
                {
                    strReturn = DocSoThanhChu(soTien_Chan) + " (phẩy) " + DocSoThanhChu_PhanLe(soTien_Le) + " đồng";
                }
                else
                {
                    strReturn = DocSoThanhChu(soTien_Chan) + " đồng chẵn";
                }

                strReturn = strReturn.Trim();
                while(strReturn.Contains("  "))
                    strReturn = strReturn.Replace("  ", " ").Trim();
                strReturn = strReturn.Substring(0, 1).ToUpper() + strReturn.Substring(1).ToLower();

                return strReturn;
            }
            catch
            {
                return "Lỗi số!";
            }
        }
       
        //========================== Sưu tầm Internet ========================
        
        /// <summary>
        /// Đọc số đến 9 số. Sưu tầm.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string DocSoThanhChu(string number)//<=
        {
            string strReturn = "";
            string s = number;
            while (s.Length>0 && s.Substring(0,1)=="0")
            {
                s = s.Substring(1);
            }
            string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
            int i, j, donvi, chuc, tram;

            bool booAm = false;
            decimal decS = 0;
            
            try
            {
                decS = Convert.ToDecimal(s);
            }
            catch {  }

            if (decS < 0)
            {
                decS = -decS;
                //s = decS.ToString();
                booAm = true;
            }
            i = s.Length;
            if (i == 0)
                strReturn = so[0] + strReturn;
            else
            {
                j = 0;
                while (i > 0)
                {
                    donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                    i--;
                    if (i > 0)
                        chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        chuc = -1;
                    i--;
                    if (i > 0)
                        tram = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        tram = -1;
                    i--;
                    if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                        strReturn = hang[j] + strReturn;
                    j++;
                    if (j > 3) j = 1;   //Tránh lỗi, nếu dưới 13 số thì không có vấn đề.
                                        //Hàm này chỉ dùng để đọc đến 9 số nên không phải bận tâm
                    if ((donvi == 1) && (chuc > 1))
                        strReturn = "mốt " + strReturn;
                    else
                    {
                        if ((donvi == 5) && (chuc > 0))
                            strReturn = "lăm " + strReturn;
                        else if (donvi > 0)
                            strReturn = so[donvi] + " " + strReturn;
                    }
                    if (chuc < 0) break;//Hết số
                    else
                    {
                        if ((chuc == 0) && (donvi > 0)) strReturn = "linh " + strReturn;
                        if (chuc == 1) strReturn = "mười " + strReturn;
                        if (chuc > 1) strReturn = so[chuc] + " mươi " + strReturn;
                    }
                    if (tram < 0) break;//Hết số
                    else
                    {
                        if ((tram > 0) || (chuc > 0) || (donvi > 0)) strReturn = so[tram] + " trăm " + strReturn;
                    }
                    strReturn = " " + strReturn;
                }
            }
            if (booAm) strReturn = "Âm " + strReturn;
            return strReturn.Trim();// = str+ "đồng chẵn";
        }
        
        /// <summary>
        /// Đọc phần lẽ.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string DocSoThanhChu_PhanLe(string number)//<=
        {
            string strReturn = "";            
            string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

            if (number.Substring(0, 1) != "0")
            {
                return DocSoThanhChu(number);
            }
            else
            {
                foreach (char item in number)
                {
                    int i = Convert.ToInt16(item.ToString());
                    strReturn = strReturn + " " + so[i];
                }
            }
            
            return strReturn.Trim();
        }        
        
        /// <summary>
        /// Đọc số nguyên. Sưu tầm.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string DocSoThanhChu(Decimal number)
        {
            string strReturn = "";
            string s = number.ToString("#");
            //while (s.Length > 0 && s.Substring(0, 1) == "0")
            //{
            //    s = s.Substring(1);
            //}
            string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
            int i, j, donvi, chuc, tram;

            bool booAm = false;
            Decimal decS = 0;
            //Tung addnew
            try
            {
                decS = Convert.ToDecimal(s);
            }
            catch
            {
            }
            if (decS < 0)
            {
                decS = -decS;
                s = decS.ToString(CultureInfo.InvariantCulture);
                booAm = true;
            }
            i = s.Length;
            if (i == 0)
                strReturn = so[0] + strReturn;
            else
            {
                j = 0;
                while (i > 0)
                {
                    donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                    i--;
                    if (i > 0)
                        chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        chuc = -1;
                    i--;
                    if (i > 0)
                        tram = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        tram = -1;
                    i--;
                    if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                        strReturn = hang[j] + strReturn;
                    j++;
                    
                    if (j > 3) j = 1;

                    if ((donvi == 1) && (chuc > 1))
                        strReturn = "mốt " + strReturn;
                    else
                    {
                        if ((donvi == 5) && (chuc > 0))
                            strReturn = "lăm " + strReturn;
                        else if (donvi > 0)
                            strReturn = so[donvi] + " " + strReturn;
                    }

                    if (chuc < 0)//Hết số
                        break;
                    else
                    {
                        if ((chuc == 0) && (donvi > 0)) strReturn = "linh " + strReturn;
                        if (chuc == 1) strReturn = "mười " + strReturn;
                        if (chuc > 1) strReturn = so[chuc] + " mươi " + strReturn;
                    }

                    if (tram < 0) break;
                    else
                    {
                        if ((tram > 0) || (chuc > 0) || (donvi > 0)) strReturn = so[tram] + " trăm " + strReturn;
                    }
                    strReturn = " " + strReturn;
                }
            }
            if (booAm) strReturn = "Âm " + strReturn;
            return strReturn.Trim() ;// = str+ "đồng chẵn";

        }


        private static string[] ChuSo = new string[10] { " không", " một", " hai", " ba", " bốn", " năm", " sáu", " bảy", " tám", " chín" };
        private static string[] Tien = new string[6] { "", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ" };        
        private static string DocSo3ChuSo(int baso)
        {
            int tram, chuc, donvi;
            string KetQua = "";
            tram = (int)(baso / 100);
            chuc = (int)((baso % 100) / 10);
            donvi = baso % 10;
            if ((tram == 0) && (chuc == 0) && (donvi == 0)) return "";
            if (tram != 0)
            {
                KetQua += ChuSo[tram] + " trăm";
                if ((chuc == 0) && (donvi != 0)) KetQua += " linh";
            }
            if ((chuc != 0) && (chuc != 1))
            {
                KetQua += ChuSo[chuc] + " mươi";
                if ((chuc == 0) && (donvi != 0)) KetQua = KetQua + " linh";
            }
            if (chuc == 1) KetQua += " mười";
            switch (donvi)
            {
                case 1:
                    if ((chuc != 0) && (chuc != 1))
                    {
                        KetQua += " mốt";
                    }
                    else
                    {
                        KetQua += ChuSo[donvi];
                    }
                    break;
                case 5:
                    if (chuc == 0)
                    {
                        KetQua += ChuSo[donvi];
                    }
                    else
                    {
                        KetQua += " lăm";
                    }
                    break;
                default:
                    if (donvi != 0)
                    {
                        KetQua += ChuSo[donvi];
                    }
                    break;
            }
            return KetQua;
        }


        /// <summary>
        /// Đọc số tiếng Anh
        /// </summary>
        /// <param name="inputNumber">1.05</param>
        /// <param name="begin2"></param>
        /// <param name="end2"></param>
        /// <param name="only2"></param>
        /// <param name="point2"></param>
        /// <param name="endPoint2"></param>
        /// <returns>one point zero five</returns>
        public static string NumWordsWrapper(decimal inputNumber, string begin2, string end2 = "", string only2 = "", string point2 = "point", string endPoint2 = "")
        {
            string words = "";
            decimal intPart;
            string decPart = "";
            if (inputNumber == 0)
                return "zero";
            try
            {
                string[] splitter = inputNumber.ToString(CultureInfo.InvariantCulture).Split('.');
                intPart = decimal.Parse(splitter[0]);
                decPart = splitter[1];
            }
            catch
            {
                intPart = inputNumber;
            }

            words = NumWords(intPart);
            string decWords = DecPartWord(decPart);

            if (!string.IsNullOrEmpty(decWords))
            {
                if (words != "")
                {
                    if (!string.IsNullOrEmpty(end2))
                    {
                        words += " " + end2;
                    }
                    words += string.Format(" {0} ", point2);
                }
                //words += DecPartWord((int)decPart);
                words += decWords;
                if (!string.IsNullOrEmpty(endPoint2))
                {
                    words += " " + endPoint2;
                }
                //words += NumWords((int)decPart);
                
                //int counter = decPart.ToString(CultureInfo.InvariantCulture).Length;
                //switch (counter)
                //{
                //    case 1: words += NumWords(decPart) + " tenths"; break;
                //    case 2: words += NumWords(decPart) + " hundredths"; break;
                //    case 3: words += NumWords(decPart) + " thousandths"; break;
                //    case 4: words += NumWords(decPart) + " ten-thousandths"; break;
                //    case 5: words += NumWords(decPart) + " hundred-thousandths"; break;
                //    case 6: words += NumWords(decPart) + " millionths"; break;
                //    case 7: words += NumWords(decPart) + " ten-millionths"; break;
                //}
            }
            else
            {
                if (words != "" && !string.IsNullOrEmpty(end2))
                {
                    words += " " + end2;
                }

                if (!string.IsNullOrEmpty(only2))
                {
                    words += " " + only2;
                }
            }

            if (string.IsNullOrEmpty(begin2))
            {
                return ChuyenMaTiengViet.HoaDauDoan(words);
            }
            else
            {
                return begin2.Trim() + " " + ChuyenMaTiengViet.HoaDauDoan(words);
            }

            
        }

        /// <summary>
        /// 80 eighty / 81 eighty one / 08 eight / 811 eight one one / 081 zero eight one
        /// </summary>
        /// <param name="numString"></param>
        /// <returns></returns>
        private static string DecPartWord(string numString)
        {
            // Xử lý chuỗi
            while (numString.EndsWith("0"))
            {
                numString = numString.Substring(0, numString.Length - 1);
            }

            if (numString.Length == 1)
            {
                numString += "0";
            }

            if (numString.Length > 2)
            {
                numString = numString.Substring(0, 2);
            }

            if (numString.Length == 2)
            {
                return NumWords(Convert.ToInt32(numString));
            }

            string[] numbersArr = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};
            string words = "";
            foreach (char c in numString)
            {
                words += " " + numbersArr[c - '0'];
            }

            //if (words.Length > 1) words = words.Substring(1);
            //while (num>0)
            //{
            //    words = numbersArr[num%10] + " " + words;
            //    num /= 10;
            //}
            return words;
        }

        private static string NumWords(decimal inputNumber) //converts double to words
        {
            string[] numbersArr = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string[] tensArr = new string[] { "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninty" };
            string[] suffixesArr = new string[] { "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion", "nonillion", "decillion", "undecillion", "duodecillion", "tredecillion", "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septdecillion", "Octodecillion", "Novemdecillion", "Vigintillion" };
            string words = "";

            bool tens = false;

            if (inputNumber < 0)
            {
                words += "negative ";
                inputNumber *= -1;
            }

            int power = (suffixesArr.Length + 1) * 3;
            double inputNumberD = (double) inputNumber;

            while (power > 3)
            {
                double pow = Math.Pow(10, power);
                if ((double)inputNumber >= pow)
                {
                    if (inputNumberD % pow > 0)
                    {
                        words += NumWords((decimal)Math.Floor(inputNumberD / pow)) + " " + suffixesArr[(power / 3) - 1] + " ";
                    }
                    else if ((int)(inputNumberD % pow) == 0)
                    {
                        words += NumWords((decimal)Math.Floor(inputNumberD / pow)) + " " + suffixesArr[(power / 3) - 1];
                    }
                    inputNumberD %= pow;
                    inputNumber = (decimal) inputNumberD;
                }
                power -= 3;
            }

            if (inputNumber >= 1000)
            {
                if (inputNumber % 1000 > 0) words += NumWords(Math.Floor(inputNumber / 1000)) + " thousand ";
                else words += NumWords(Math.Floor(inputNumber / 1000)) + " thousand";
                inputNumber %= 1000;
            }

            if (0 <= inputNumber && inputNumber <= 999)
            {
                if ((int)inputNumber / 100 > 0)
                {
                    words += NumWords(Math.Floor(inputNumber / 100)) + " hundred";
                    inputNumber %= 100;
                    //and
                    if (inputNumber > 0)
                    {
                        words += " and";
                    }
                }

                if ((int)inputNumber / 10 > 1)
                {
                    if (words != "")
                        words += " ";
                    words += tensArr[(int)inputNumber / 10 - 2];
                    tens = true;
                    inputNumber %= 10;
                }

                if (inputNumber < 20 && inputNumber > 0)
                {
                    if (words != "" && tens == false)
                        words += " ";
                    words += (tens ? "-" + numbersArr[(int)inputNumber - 1] : numbersArr[(int)inputNumber - 1]);
                    inputNumber -= Math.Floor(inputNumber);
                }
            }

            return words;

        }
    }
}
