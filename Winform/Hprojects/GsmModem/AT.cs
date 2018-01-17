using System;
using System.Collections.Generic;
using System.Text;

namespace GSM
{
    /// <summary>
    /// Không hữu dụng lắm!
    /// </summary>
    public class AT
    {
        #region ==== ATA - Answers - Trả lời ====
        public const string ATA = "ATA";
        #endregion

        #region ==== ATD - Dial - Gọi ====
        public const string ATD = "ATD";
        /// <summary>
        /// Gọi đến một số điện thoại
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ATD_(string number)
        {
            return ATD + number.Trim() + ";";
        }
        #endregion

        #region ==== ATH - Hang - Ngắt
        public const string ATH = "ATH";
        #endregion

        #region ==== ATI - Infomation - Thông tin ====
        public const string ATI = "ATI";
        #endregion

        //=========================
        #region ==== +CNUM - Số chủ máy ====
        public const string AT_CNUM = "AT+CNUM";
        #endregion

        #region ==== +CSCA - Service Center Address ====
        /// <summary>
        /// Service Center Address
        /// </summary>
        public const string AT_CSCA = "AT+CSCA";
        public static string AT_CSCA_View()
        {
            return AT_CSCA + "=?";
        }
        /// <summary>
        /// Sửa số trung tâm tin nhắn?
        /// </summary>
        /// <param name="SCA">Số trung tâm</param>
        /// <returns></returns>
        public static string AT_CSCA_Set(string SCA)
        {
            if (!SCA.StartsWith("+")) SCA = "+" + SCA;
            return AT_CSCA + "=\"" + SCA + "\""; 
        }
        public static string GánTrungTâmTinNhắn(string Số_Trung_Tâm)
        { return AT_CSCA_Set(Số_Trung_Tâm); }
        //Còn nhiều thứ rắc rối khác
        #endregion +CSCA

        #region ==== +CMGF - Định dạng tin nhắn ====
        public static readonly string CMGF = "+CMGF";
        /// <summary>
        /// Message format command
        /// </summary>
        public static readonly string AT_CMGF = "AT+CMGF";
        public static readonly string AT_CMGF_get = "AT+CMGF?";
        /// <summary>
        /// Chuyển qua chế độ PDU AT+CMGF=0
        /// </summary>
        public static readonly string AT_CMGF_0 = "AT+CMGF=0";
        /// <summary>
        /// Chuyển qua chế độ text AT+CMGF=1
        /// </summary>
        public static readonly string AT_CMGF_1 = "AT+CMGF=1";
        public static string AT_CMGF_(int type)
        {
            return AT_CMGF + "=" + type;
        }
        /// <summary>
        /// Thiết lập định dạng tin nhắn.
        /// </summary>
        /// <param name="type">Loại định dạng</param>
        /// <returns></returns>
        public static string AT_CMGF_(MessageFormatType type)
        { return AT_CMGF + "=" + (int) type; }
        public static string ĐịnhDạngTinNhắn(MessageFormatType Loại_Định_Dạng)
        { return AT_CMGF_(Loại_Định_Dạng); }
        #endregion cmgf

        #region ==== +CPBF - Tìm theo tên??? test khong duoc ====
        public const string AT_CPBF = "AT+CPBF";
        #endregion

        #region ==== +CPBR - Command phonebook read - Lệnh đọc danh bạ ====
        /// <summary>
        /// +CPBR
        /// </summary>
        public const string _CPBR = "+CPBR";
        /// <summary>
        /// AT+CPBR
        /// </summary>
        public const string AT_CPBR = "AT+CPBR";
        /// <summary>
        /// AT+CPBR=index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string AT_CPBR_(int index)
        {
            return AT_CPBR + "=" + index;
        }
        /// <summary>
        /// AT+CPBR=index,toIndex
        /// </summary>
        /// <param name="index"></param>
        /// <param name="toIndex"></param>
        /// <returns></returns>
        public static string AT_CPBR_(int index, int toIndex)
        {
            return AT_CPBR + "=" + index + "," + toIndex;
        }
        #endregion

        #region ==== +CPBS - Command phonebook storage - Chọn bộ nhớ danh bạ ====
        public const string AT_CPBS = "AT+CPBS";
        internal static string AT_CPBS_(PhoneBookStorage storage)
        {
            return AT_CPBS + "=\""+storage+"\"";
        }
        #endregion

        #region ==== +CPMS - Lựa chọn bộ nhớ lưu trữ ====
        /// <summary>
        /// AT+CPMS
        /// </summary>
        public static readonly string AT_CPMS = "AT+CPMS";
        /// <summary>
        /// AT+CPMS="SM"
        /// </summary>
        public static readonly string AT_CPMS_SM = "AT+CPMS=\"SM\"";
        /// <summary>
        /// AT+CPMS="ME"
        /// </summary>
        public static readonly string AT_CPMS_ME = "AT+CPMS=\"ME\"";
        /// <summary>
        /// Chọn bộ nhớ tham chiếu.
        /// </summary>
        /// <param name="MS">Bộ nhớ</param>
        /// <returns></returns>
        public static string AT_CPMS_(Storage MS)
        { return AT_CPMS + "=\"" + MS + "\""; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="storage">"SM" or "ME" or "TM" or "T??"</param>
        /// <returns></returns>
        public static string AT_CPMS_(string storage)
        {
            return string.Format("{0}=\"{1}\"", AT_CPMS, storage);
        }
        public static string LựaChọnBộNhớThamChiếu(Storage Bộ_Nhớ_Tin_Nhắn)
        { return AT_CPMS_(Bộ_Nhớ_Tin_Nhắn); }
        #endregion

        #region ==== +CMGL - Liệt kê danh sách các tin nhắn ====
        /// <summary>
        /// +CMGL
        /// </summary>
        public const string CMGL = "+CMGL";
        public const string AT_CMGL = "AT+CMGL";
        public static string AT_CMGL_Help()
        { return AT_CMGL + "=?"; }
        /// <summary>
        /// Đọc tất cả tin nhắn trong loại được chọn trong bộ nhớ đang tham chiếu.
        /// Bộ nhớ tham chiếu xem hàm LựaChọnBộNhớThamChiếu (PreferredMessageStorage)
        /// </summary>
        /// <param name="AT_StoSmsType">REC READ REC UNREAD STO SENT...</param>
        /// <returns></returns>
        public static string AT_CMGL_(string AT_StoSmsType)
        { return AT_CMGL + "=\"" + AT_StoSmsType + "\""; }
        public static string AT_CMGL_(int type01234)
        { return AT_CMGL + "=" + type01234; }
        public static string LiệtKêDanhSáchCácTinNhắn(string Loại_TN)
        { return AT_CMGL_(Loại_TN); }
        #endregion



        #region ==== +CMGR - Đọc tin nhắn theo vị trí ====
        /// <summary>
        /// +CMGR:
        /// </summary>
        public const string CMGR_respone = "+CMGR:";
        public const string AT_CMGR = "AT+CMGR";
        public static string AT_CMGR_(int index)
        {
            return AT_CMGR + "=" + index;
        }
        public const string AT_CMGR_help = "AT+CMGR=?";
        #endregion cmgr

        #region ==== +CMTI ====
        /// <summary>
        /// Có tin nhắn mới. +CMTI
        /// </summary>
        public const string CMTI = "+CMTI";
        public const string AT_CNMI = "AT+CNMI";
        public const string AT_CNMI_request = "AT+CNMI?";
        public static string AT_CNMI_set(int p1, int p2)
        {
            return AT_CNMI + "=" + p1 + "," + p2;
        }
        #endregion
        
        

        #region ==== +CMGS - Send Message ====
        /// <summary>
        /// Gửi tin nhắn
        /// </summary>
        public const string AT_CMGS = "AT+CMGS";
        /// <summary>
        /// Lệnh chuẩn bị gửi tin chế độ PDU
        /// </summary>
        /// <param name="length">Độ dài chuỗi PDU length (bỏ đi ServiceCentreNumber length và 2</param>
        /// <returns></returns>
        public static string AT_CMGS_(int length)
        {
            return AT_CMGS + "=" + length;
        }
        public static string AT_CMGS_(string phoneNumber)
        {
            return AT_CMGS + "=\"" + phoneNumber + "\"";
        }
        #endregion send

        #region ==== +CMGW - Write message to memory ====
        /// <summary>
        /// Ghi tin nhắn vào bộ nhớ
        /// </summary>
        public const string AT_CMGW = "AT+CMGW";
        /// <summary>
        /// Chuẩn bị cho ghi tin kiểu PDU
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string AT_CMGW_(int length)
        {
            return AT_CMGW + "=" + length;
        }
        /// <summary>
        /// Chuẩn bị cho ghi tin kiểu text
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static string AT_CMGW_(string phoneNumber)
        {
            return string.Format("{0}=\"{1}\"", AT_CMGW, phoneNumber);
        }
        
        #endregion

        #region ==== +CMSS - Send message from memory ====
        /// <summary>
        /// Gửi tin nhắn từ bộ nhớ
        /// </summary>
        public const string AT_CMSS = "AT+CMSS";
        
        /// <summary>
        /// Gửi tin nhắn từ bộ nhớ.
        /// </summary>
        /// <param name="index">Vị trí trong bộ nhớ</param>
        /// <returns>AT+CMSS=index</returns>
        public static string AT_CMSS_(int index)
        {
            return AT_CMSS + "=" + index;
        }
        #endregion cmss

        #region ==== +CMGD - delete message ====
        public const string AT_CMGD = "AT+CMGD";
        /// <param name="delflag">Cờ xóa, nếu khác 0 sẽ bỏ qua tham số index
        /// 0 mặc định: xóa theo index
        /// 1 Xóa toàn bộ tin nhắn đã đọc trên bộ nhớ đang tham chiếu.
        /// 2 Xóa toàn bộ tin nhắn đã đọc và đã gửi.
        /// 3 Xóa toàn bộ tin nhắn đã đọc, đã gửi và chưa gửi.
        /// 4 Xóa hết, chừa lại tin nhắn chưa đọc.
        /// </param>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="flag">0:mặc định index, 1: all read, 2 read and sent, 3: 2 and unsend, 4: all except unread</param>
        /// <returns></returns>
        public static string AT_CMGD_(int index, int flag = 0)
        {
            return string.Format("{0}={1},{2}", AT_CMGD, index, flag);
        }
        #endregion

        //===================================================================
        #region ==== RING ====
        /// <summary>
        /// Có cuộc gọi tới. RING
        /// </summary>
        public const string RING = "RING";
        #endregion

        /// <summary>
        /// "REC UNREAD" "REC READ" "STO UNSENT" "STO SENT" "ALL"
        /// </summary>
        public class StoredMessageType
        {
            /// <summary>
            /// Tin đến chưa đọc.
            /// </summary>
            public const string REC_UNREAD = "REC UNREAD";
            /// <summary>
            /// Tin đến đã đọc.
            /// </summary>
            public const string REC_READ = "REC READ";
            /// <summary>
            /// Tin đi chưa gửi.
            /// </summary>
            public const string STO_UNSENT = "STO UNSENT";
            /// <summary>
            /// Tin đi đã gửi.
            /// </summary>
            public const string STO_SENT = "STO SENT";
            public const string ALL = "ALL";
        }
    }

    #region ENUM
    

    public enum MessageFormatType
    {
        PDU_mode = 0,
        Textmode = 1
    }

    /// <summary>
    /// FD, ME, SM, EN
    /// </summary>
    public enum PhoneBookStorage
    {
        /*
        values reserved by this TS:
        "DC" ME dialled calls list (+CPBW may not be applicable for this storage) $(AT R97)$
        "EN" SIM (or ME) emergency number (+CPBW is not be applicable for this storage) $(AT R97)$
        "FD" SIM fixdialling-phonebook
        "LD" SIM last-dialling-phonebook
        "MC" ME missed (unanswered received) calls list (+CPBW may not be applicable for this storage)
        "ME" ME phonebook
        "MT" combined ME and SIM phonebook
        "ON" SIM (or ME) own numbers (MSISDNs) list (reading of this storage may be available through +CNUM
        also) $(AT R97)$
        "RC" ME received calls list (+CPBW may not be applicable for this storage) $(AT R97)$
        "SM" SIM phonebook
        "TA" TA phonebook
        : integer type value indicating the number of used locations in selected memory
        : integer type value indicating the total number of locations in selected memory
    */

        /// <summary>
        /// SIM fixdialling phone book
        /// </summary>
        FD,
        /// <summary>
        /// ME phone book (bộ nhớ máy)
        /// </summary>
        ME,
        /// <summary>
        /// SIM phone book
        /// </summary>
        SM,
        /// <summary>
        /// SIM (or ME) emergency number (số khẩn)
        /// </summary>
        EN
    }

    /// <summary>
    /// Bộ nhớ lưu trữ tin nhắn "ME","MT","SM","SR"
    /// </summary>
    public enum Storage
    {
        NO = 0,
        /// <summary>
        /// Bộ nhớ sim
        /// </summary>
        SM = 1,//00000001
        /// <summary>
        /// Bộ nhớ máy
        /// </summary>
        ME = 2,//00000010
        /// <summary>
        /// Bộ nhớ kết hợp sim và máy
        /// </summary>
        MT = 3,//00000011
        /// <summary>
        /// Bộ nhớ tin gửi đi
        /// </summary>
        BM = 4,//100
        /// <summary>
        /// Thiết bị đầu cuối
        /// </summary>
        TA = 8,//1000
        /// <summary>
        /// Tin báo cáo
        /// </summary>
        SR = 16,//10000
        /// <summary>
        /// Kiểu mở rộng Đọc cả SM, ME +.
        /// </summary>
        ALL=0xFFFF,
        /// <summary>
        /// Kiểu mở rộng, trên máy tính
        /// </summary>
        COM1 = 32,//0100000
        COM2 = 64,//1000000
        COM = 96  //1100000
    }
    #endregion
}
