using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V6ThuePostBkavApi
{
    public static class BkavConst
    {
        #region ==== Const ====

        //Ma lenh Tạo hóa đơn
//100
//PartnerID
//Mẫu số, ký hiệu do Bkav cấp.Số HĐ = 0(bản nháp)

//101
//PartnerID
//Mẫu số, ký hiệu, số hđ do Bkav cấp (Hóa đơn chờ)

//110
//PartnerID
//Mẫu số, ký hiệu do PMKT cấp.Số HĐ = chưa có

//111
//PartnerID
//Mẫu số, ký hiệu, số hóa đơn do PMKT cấp 

//112
//PartnerID
//Mẫu số, ký hiệu do PMKT cấp, số HĐ do Bkav cấp



        /// <summary>
        /// Tạo HĐ, eHD tự cấp InvoiceForm, InvoiceSerial; InvoiceNo = 0 (tạo HĐ mới)
        /// </summary>
        public const int _100_CreateNew = 100;

        /// <summary>
        /// Tạo HĐ, eHD tự cấp InvoiceForm, InvoiceSerial và cấp InvoiceNo (tạo HĐ Trống)
        /// </summary>
        public const int _101_CreateEmpty = 101;

        /// <summary>
        /// Tạo HĐ, Client tự cấp InvoiceForm, InvoiceSerial; InvoiceNo = 0 (tạo HĐ mới)
        /// </summary>
        public const int _110_CreateClient = 110;

        /// <summary>
        /// Tạo HĐ, Client tự cấp InvoiceForm, InvoiceSerial, InvoiceNo (tạo HĐ mới, có sẵn Số HĐ)
        /// </summary>
        public const int _111_CreateClientNo = 111;

        /// <summary>
        /// Tạo HĐ, Client tự cấp InvoiceForm, InvoiceSerial, InvoiceNo eHD cấp (tạo HĐ mới)
        /// </summary>
        public const int _112_CreateWithParternSerial = 112;

        /// <summary>
        /// Tạo Hóa đơn thay thế cho  Hoá đơn khác, hóa đơn mới tạo, chưa có shd.
        /// </summary>
        public const int _120_CreateReplace = 120;

        /// <summary>
        /// Tạo Hóa đơn điều chỉnh cho  Hoá đơn khác
        /// </summary>
        public const int _121_CreateAdjust = 121;
        /// <summary>
        /// Tạo hóa đơn điều chỉnh, BKAV cấp số.
        /// </summary>
        public const int _124_CreateAdjust = 124;

        /// <summary>
        /// Tạo Hóa đơn thay thế cho  Hoá đơn khác, số hd do BKAV cấp, hóa đơn chờ
        /// </summary>
        public const int _123_CreateReplace = 123;

        /// <summary>
        /// Tạo Hóa đơn điều chỉnh CK, HD chưa có số, hoặc số tự do?
        /// </summary>
        public const int _122_DieuChinhCK_KoSoHD = 122;

        /// <summary>
        /// Tạo Hóa đơn điều chỉnh CK, số HD do BKAV cấp trả về.
        /// </summary>
        public const int _126_DieuChinhCK_SoHD_BKAV = 126;

        /// <summary>
        /// Cập nhật thông tin Hóa đơn khi chưa được phát hành
        /// </summary>
        public const int _200_Update = 200;

        /// <summary>
        /// Hủy hóa đơn
        /// </summary>
        public const int _201_CancelInvoiceByInvoiceGUID = 201; // Hủy hóa đơn
        public const int _202_CancelInvoiceByPartnerInvoiceID = 202; // Hủy hóa đơn

        public const int _205_SignGUID = 205; // Ký

        /// <summary>
        /// Xoá hóa đơn chưa phát hành
        /// </summary>
        public const int _301 = 301;

        /// <summary>
        /// Lấy thông tin chi tiết Hóa đơn
        /// </summary>
        public const int _800 = 800;

        /// <summary>
        /// Lấy trạng thái Hóa đơn
        /// </summary>
        public const int _801 = 801;

        /// <summary>
        /// Lấy lịch sử xử lý Hóa đơn
        /// </summary>
        public const int _802 = 802;

        /// <summary>
        /// Lấy link để tải file hóa đơn in chuyển đổi
        /// </summary>
        public const int _804_GetInvoiceLink_ChuyenDoi = 804;

        public const int _808_GetInvoicePDF64 = 808;

        public const int _809_GetInvoiceXML64 = 809;

        /// <summary>
        /// Lây thông tin doanh nghiệp theo MST
        /// </summary>
        public const int _904 = 904;

        #endregion const
    }
}
