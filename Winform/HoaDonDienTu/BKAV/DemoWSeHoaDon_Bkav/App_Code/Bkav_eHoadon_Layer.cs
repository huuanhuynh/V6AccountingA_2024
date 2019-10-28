using BSECUS;
using System;
using System.Collections.Generic;

// Edit... nhung cho TO EDIT ben duoi

public class Constants
{
    public const string BkavPartnerGUID = "AA2FD9CB-71AC-4BFB-A169-994E13C10ABA";// "Liên hệ BKAV"; // -TO EDIT- (2) Do Bkav cấp cho từng đối tác, mỗi đối tác 1 GUID khác nhau
    public const string BkavPartnerToken = "LN1sCTQ8Vn7NxnNaOw1/XfHWeD+LhP/kEZZb0etwZWA=:wx3f6aLHdnDEDr7onDXTkw==";//"Liên hệ BKAV"; // -TO EDIT- (1) Do Bkav cấp cho từng đối tác, mỗi đối tác 1 mã khác nhau
    public const uint Mode = RemoteCommand.DefaultMode; // -TO EDIT- (3) Do Bkav cấp cho từng đối tác, mỗi đối tác 1 mã khác nhau

    static public class TaxRateID
    {
        public const int Khong = 1;
        public const int Nam = 2;
        public const int Muoi = 3;
        public const int KhongChiuThue = 4;
        public const int KhongKeKhaiThue = 5;
        public const int Khac = 6;
    }
}

public static class CommandType
{
    public const int Undefined = 0;

    //1xx: Thêm mới
    public const int CreateInvoiceMT = 100; // Tạo Hóa đơn, eHD tự cấp InvoiceForm, InvoiceSerial; InvoiceNo = 0 (tạo Hóa đơn mới)
    public const int CreateInvoiceTR = 101; // Tạo Hóa đơn, eHD tự cấp InvoiceForm, InvoiceSerial và cấp InvoiceNo (tạo Hóa đơn Trống)
    public const int CreateInvoiceWithFormSerial = 110; // Tạo Hóa đơn, Client tự cấp InvoiceForm, InvoiceSerial; InvoiceNo = 0 (tạo Hóa đơn mới)
    public const int CreateInvoiceWithFormSerialNo = 111; // Tạo Hóa đơn, Client tự cấp InvoiceForm, InvoiceSerial, InvoiceNo (tạo Hóa đơn mới, có sẵn Số Hóa đơn)

    public const int CreateInvoiceReplace = 120; // Tạo Hóa đơn thay thế cho 1 Hoá đơn khác
    public const int CreateInvoiceAdjust = 121; // Tạo Hóa đơn điều chỉnh cho 1 Hoá đơn khác
    // 2xx: Cập nhật
    public const int UpdateInvoiceByPartnerInvoiceID = 200; // Cập nhật thông tin của Hoá đơn
    public const int UpdateInvoiceByInvoiceGUID = 204; // Cập nhật thông tin của Hoá đơn

    public const int CancelInvoiceByInvoiceGUID = 201; // Hủy hóa đơn
    public const int CancelInvoiceByPartnerInvoiceID = 202; // Hủy hóa đơn

    // 3xx: Xóa
    public const int DeleteInvoiceByPartnerInvoiceID = 301; // Xoá hóa đơn chưa phát hành
    public const int DeleteInvoiceByInvoiceGUID = 303; // Xoá hóa đơn chưa phát hành

    // 5xx: File
    public const int UploadFile = 500;//Upload file excel dữ liệu Hóa đơn
    // 6xx: View hoá đơn
    public const int ViewInvoice = 600;//Trả về cho Partner file PDF bản thể hiện của Hoá đơn theo trạng thái
    public const int ViewInvoiceConversion = 601;//Trả về cho Partner file PDF chuyển đổi
    // 8xx: Các hàm lấy thông tin, báo cáo
    public const int GetInvoiceDataWS = 800; // Lấy thông tin của tờ Hóa đơn
    public const int GetInvoiceStatusID = 801; // Lấy trạng thái của tờ Hóa đơn
    public const int GetInvoiceHistory = 802; // Lấy lịch sử thay đổi của 1 tờ Hóa đơn
    public const int GetInvoiceLink = 804;//Lấy link để tải file hóa đơn in chuyển đổi
    public const int CreateAccount = 902;//Tạo tài khoản Demo trên eHD demo
    public const int UpdateAccount = 903; // Cập nhật thông tin tài khoản
    public const int GetUnitInforByTaxCode = 904; // Lấy thông tin doanh nghiệp từ thuế

    //10xx Trao đổi với tool
    public const int GetRunTypeInfo = 1000; // Lấy kiểu xử lý và FileName, ClassName.
    public const int GetDLLContent = 1001; // Lấy nội dung file DLL.
}

[Serializable]
public class InvoiceResult
{
    public long PartnerInvoiceID { get; set; }
    public string PartnerInvoiceStringID { get; set; }

    public Guid InvoiceGUID { get; set; }

    public string InvoiceForm { get; set; }
    public string InvoiceSerial { get; set; }
    public int InvoiceNo { get; set; }

    /// <summary>
    /// Trạng thái xử lý: 0 - thêm mới thành công, 1 - lỗi
    /// </summary>
    public int Status { get; set; }
    public string MessLog { get; set; }
}

[Serializable]
public class InvoiceDataWS
{
    public InvoiceWS Invoice { get; set; }
    public List<InvoiceDetailsWS> ListInvoiceDetailsWS { get; set; }
    public List<InvoiceAttachFileWS> ListInvoiceAttachFileWS { get; set; }

    // Định danh duy nhất cho hóa đơn ở hệ thống của Partner (Phía Client).
    // Chỉ dùng 1 trong 2 trường PartnerInvoiceID, PartnerInvoiceStringID theo 1 trong 2 cách sau:
    // Cách 1: Dùng PartnerInvoiceID: Set PartnerInvoiceID > 0 và PartnerInvoiceStringID = null
    // Cách 2: Dùng PartnerInvoiceStringID: Set PartnerInvoiceID = 0 và PartnerInvoiceStringID != null
    public long PartnerInvoiceID { get; set; }
    public string PartnerInvoiceStringID { get; set; }



    public InvoiceDataWS()
    {
        Invoice = new InvoiceWS();
        ListInvoiceDetailsWS = new List<InvoiceDetailsWS>();
        ListInvoiceAttachFileWS = new List<InvoiceAttachFileWS>();
        PartnerInvoiceID = 0;
        PartnerInvoiceStringID = null;
    }

}

[Serializable]
public class InvoiceWS
{
    /// <summary>
    /// Loại Hoá đơn: luôn là 1	(Hóa đơn giá trị gia tăng)
    /// </summary>
    public int InvoiceTypeID { get; set; }
    /// <summary>
    /// Ngày trên Hoá đơn
    /// </summary>
    public DateTime InvoiceDate { get; set; }
    /// <summary>
    /// Tên người mua hàng
    /// </summary>
    public string BuyerName { get; set; }
    /// <summary>
    /// Mã số thuế Người mua hàng
    /// </summary>
    public string BuyerTaxCode { get; set; }
    /// <summary>
    /// Tên đơn vị mua hàng
    /// </summary>
    public string BuyerUnitName { get; set; }
    /// <summary>
    /// Địa chỉ đơn vị mua hàng
    /// </summary>
    public string BuyerAddress { get; set; }
    /// <summary>
    /// Thông tin tài khoản ngân hàng người mua ví dụ: 11111111111 - BIDV chi nhánh Tây Hồ
    /// </summary>
    public string BuyerBankAccount { get; set; }
    /// <summary>
    /// Hình thức thanh toán: 1	Tiền mặt (mặc định), 2	Chuyển khoản, 3	Tiền mặt/Chuyển khoản, 4	Xuất hàng cho chi nhánh, 5	Hàng biếu tặng
    /// </summary>
    public int PayMethodID { get; set; }
    /// <summary>
    /// Hình thức nhận Hoá đơn: 1	Email , 2	Tin nhắn, 3	Email và tin nhắn, 4	Chuyển phát nhanh
    /// </summary>
    public int ReceiveTypeID { get; set; }
    /// <summary>
    /// eMail nhận Hoá đơn
    /// </summary>
    public string ReceiverEmail { get; set; }
    /// <summary>
    /// Số điện thoại nhận Hoá đơn
    /// </summary>
    public string ReceiverMobile { get; set; }
    /// <summary>
    /// Địa chỉ nhận Hoá đơn (Hoá đơn in chuyển đổi)
    /// </summary>
    public string ReceiverAddress { get; set; }
    /// <summary>
    /// Tên người nhận Hoá đơn (Hoá đơn in chuyển đổi)
    /// </summary>
    public string ReceiverName { get; set; }
    /// <summary>
    /// Ghi chú Hoá đơn
    /// </summary>
    public string Note { get; set; }
    /// <summary>
    /// Dữ liệu KH tự định nghĩa (dạng json)
    /// </summary>
    public string UserDefine { get; set; }
    /// <summary>
    /// Mã ID chứng từ kế toán hoặc số Bill code của Hoá đơn Bán hàng
    /// </summary>
    public string BillCode { get; set; }
    /// <summary>
    /// ID tiền tệ: VND	- Việt Nam đồng (mặc định), USD - Đô la Mỹ, EUR - Đồng Euro, GBP - Bảng Anh, CNY - Nhân dân tệ,CHF - Phơ răng Thuỵ Sĩ ...
    /// </summary>
    public string CurrencyID { get; set; }
    /// <summary>
    /// Tỷ giá ngoại tệ so với VND: mặc định là 1
    /// </summary>
    public double ExchangeRate { get; set; }
    /// <summary>
    /// ID hệ thống tự sinh dùng để giao tiếp giữa các hệ thống
    /// </summary>
    public Guid InvoiceGUID { get; set; }
    /// <summary>
    /// Trạng thái của hóa đơn
    /// </summary>
    public int InvoiceStatusID { get; set; }
    /// <summary>
    /// Mẫu số Hóa đơn
    /// </summary>
    public string InvoiceForm { get; set; }
    /// <summary>
    /// Ký hiệu Hóa đơn
    /// </summary>
    public string InvoiceSerial { get; set; }
    /// <summary>
    /// Số hóa đơn
    /// </summary>
    public int InvoiceNo { get; set; }
    /// <summary>
    /// Mã tra cứu
    /// </summary>
    public string InvoiceCode { get; set; }
    /// <summary>
    /// Ngày ký
    /// </summary>
    public DateTime SignedDate { get; set; }
    /// <summary>
    /// default 0: Tạo Hoá đơn, 1: Tạo Hoá đơn thay thế, 2: Tạo Hoá đơn điều chỉnh thông tin, 3: Tạo Hoá đơn điều chỉnh tăng, 4: Tạo Hoá đơn điều chỉnh giảm
    /// </summary>
    public int TypeCreateInvoice { get; set; }
    /// <summary>
    /// Thông tin Hoá đơn gốc dùng trong trường hợp thay thế, điều chỉnh. Định dạng như sau: [Mẫu Số]_[Ký hiệu]_[Số Hoá đơn], ví dụ: [01GTKT0/001]_[AA/17E]_[0000001]
    /// </summary>
    public string OriginalInvoiceIdentify { get; set; }

}

[Serializable]
public class InvoiceDetailsWS
{
    public InvoiceDetailsWS()
    {
        UnitName = "";
        Qty = 0;
        Price = 0;
        Amount = 0;
        TaxRateID = Constants.TaxRateID.Muoi;
        TaxAmount = 0;
        IsDiscount = false;
    }
    /// Tên hàng hóa, dịch vụ hoặc nội dung giảm giá chiết khấu (IsDiscount = 1)
    /// </summary>
    public string ItemName { get; set; }
    /// <summary>
    /// Đơn vị tính hàng hóa, dịch vụ
    /// </summary>
    public string UnitName { get; set; }
    /// <summary>
    /// Số lượng hàng hóa dịch vụ
    /// </summary>
    public double Qty { get; set; }
    /// <summary>
    /// Giá của hàng hóa
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Thành tiền hàng hóa dịch vụ hoặc số tiền chiết khấu
    /// </summary>
    public double Amount { get; set; }
    /// <summary>
    /// ID thuế suất: 1	0%, 2	5%, 3	10%, 4	Không chịu thuế, 5	Không kê khai thuế
    /// </summary>
    public int TaxRateID { get; set; }
    /// <summary>
    /// Thành tiền thuế
    /// </summary>
    public double TaxAmount { get; set; }
    /// <summary>
    /// Là chiết khấu ghi trên Hoá đơn: 1 - là chiết khấu, mặc định là 0 
    /// </summary>
    public bool IsDiscount { get; set; }
    /// <summary>
    /// Dữ liệu KH tự định nghĩa (dạng json/xml)
    /// </summary>
    public string UserDefineDetails { get; set; }
    /// <summary>
    /// Dùng mã lệnh 121: Điều chỉnh thì Báo tăng là True, Báo giảm là False
    /// </summary>
    public bool IsIncrease { get; set; }
}

[Serializable]
public class InvoiceAttachFileWS
{
    /// <summary>
    /// Tên file
    /// </summary>
    public string FileName { set; get; }
    /// <summary>
    /// Phần mở rộng (docx,pdf...)
    /// </summary>
    public string FileExtension { set; get; }
    /// <summary>
    /// Nội dung file dạng Base64
    /// </summary>
    public string FileContent { set; get; }

    public InvoiceAttachFileWS()
    {
        FileName = "";
        FileExtension = "";
        FileContent = "";
    }
}

/// <summary>
/// Lịch sử của tờ Hóa đơn
/// </summary>
[Serializable()]
public class HistoryLog
{
    public DateTime CreateDate { get; set; }
    public long ID { get; set; }
    public string IP { get; set; }
    public string LogContent { get; set; }
    public Guid ObjectGUID { get; set; }
    public int UserID { get; set; }
}

[Serializable()]
public class BusinessInfo
{
    public string MaSoThue { get; set; }

    public string TenChinhThuc { get; set; }
    public string DiaChiGiaoDichChinh { get; set; }
    public string DiaChiGiaoDichPhu { get; set; }
    public string TrangThaiHoatDong { get; set; }
}

[Serializable()]
public class DllInfo
{
    public int RunType { get; set; }
    public string ClassName { get; set; }
    public string DLLName { get; set; }
    public string Code { get; set; }
    public byte[] DLLContent { get; set; }
}

[Serializable]
public class CreateAccountInfoFromPartner
{
    public CreateAccountInfoFromPartner()
    {
        UnitName = "";
        UnitAddress = "";
        UnitPersonRepresent = "";
        UnitPersonRepresentPosition = "";
        UnitEmail = "";
        UnitPhone = "";
        TaxCode = "";
        BankAccount = "";
        BankName = "";
        TaxDepartmentID = 0;
        BrandName = "";
        DomainCheckInvoice = "";
    }

    #region Class Property Declarations

    //Thông tin tài khoản
    /// <summary>
    /// Mã số thuế
    /// </summary>
    public string TaxCode { get; set; }
    /// <summary>
    /// Tên đơn vị
    /// </summary>
    public string UnitName { get; set; }
    /// <summary>
    /// Địa chỉ
    /// </summary>
    public string UnitAddress { get; set; }
    /// <summary>
    /// Cơ quan Thuế quản lý
    /// </summary>
    public int TaxDepartmentID { get; set; }
    /// <summary>
    /// Người đại diện pháp luật
    /// </summary>
    public string UnitPersonRepresent { get; set; }
    /// <summary>
    /// Chức danh 
    /// </summary>
    public string UnitPersonRepresentPosition { get; set; }
    /// <summary>
    /// Email nhận Hoá đơn
    /// </summary>
    public string UnitEmail { get; set; }
    /// <summary>
    /// SĐT liên hệ
    /// </summary>
    public string UnitPhone { get; set; }
    /// <summary>
    /// Tài khoản Ngân hàng
    /// </summary>   
    public string BankAccount { get; set; }
    /// <summary>
    /// Tên ngân Hàng     
    /// </summary>
    public string BankName { get; set; }
    /// <summary>
    /// Tên Thương hiệu
    /// </summary>
    public string BrandName { get; set; }
    /// <summary>
    /// Tên miền tra cứu
    /// </summary>
    public string DomainCheckInvoice { get; set; }
    #endregion
}

[Serializable]
public class AccountResult
{
    public Guid AccountGUID { get; set; }
    public string Account { get; set; }
    public string Password { get; set; }
    public int NumberInvoice { get; set; }
    public int NumberMSG { get; set; }
}