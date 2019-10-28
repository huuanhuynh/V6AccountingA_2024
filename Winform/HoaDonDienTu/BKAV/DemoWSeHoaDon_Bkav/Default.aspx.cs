using BSECUS;
using System;
using System.Collections.Generic;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    RemoteCommand remoteCommand = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        ExecCommandFunc wsExecCommand = null;
        var webservice = new WSPublicEHoaDon.WSPublicEHoaDon();
        wsExecCommand = webservice.ExecuteCommand;

        remoteCommand = new RemoteCommand(wsExecCommand, Constants.BkavPartnerGUID, Constants.BkavPartnerToken, Constants.Mode);
    }

    protected void btnCreateInvoice_Click(object sender, EventArgs e)
    {
        string sGUID = null;
        string msg = DoCreateInvoice(out sGUID);
        if (msg.Length > 0) lblMsg.Text = "Có lỗi: " + msg;
        else
        {
            txtInvoiceGUID.Text = sGUID;
            lblMsg.Text = "GUID của Hóa đơn vừa được tạo: " + sGUID;
        }
    }
    string DoCreateInvoice(out string sGUID)
    {
        string msg = "";
        sGUID = null;

        string list = null;
        msg = GetListInvoiceDataWS(int.Parse(ddlCommandType.SelectedValue), out list);//CommandType0 = 100, Tạo HĐ mới.
        if (msg.Length > 0) return msg;

        Result result = null;
        msg = remoteCommand.TransferCommandAndProcessResult(int.Parse(ddlCommandType.SelectedValue), list, out result);
        if (msg.Length > 0) return msg;

        // Không có lỗi, Hệ thống trả ra danh sách kết quả của Hóa đơn
        List<InvoiceResult> listInvoiceResult = null;
        msg = Convertor.StringToObject(rdXML.Checked, Convert.ToString(result.Object), out listInvoiceResult);
        if (msg.Length > 0) return msg;

        foreach (InvoiceResult invoiceResult in listInvoiceResult)
        {
            if (invoiceResult.Status == 0) sGUID = sGUID + "; " + invoiceResult.InvoiceGUID.ToString();
            else msg = msg + "; " + invoiceResult.MessLog;
        }

        return msg;
    }

    protected void btnGetInfo_Click(object sender, EventArgs e)
    {
        string msg = "";

        int InvoiceNo = 0;
        msg = DoGetInfo(out InvoiceNo);
        if (msg.Length > 0) lblMsg.Text = "Có lỗi: " + msg;
        else lblMsg.Text = "Số Hóa đơn: " + InvoiceNo;
    }
    string DoGetInfo(out int InvoiceNo)
    {
        string msg = "";
        InvoiceNo = 0;

        Result result = null;
        msg = remoteCommand.TransferCommandAndProcessResult(CommandType.GetInvoiceDataWS, txtInvoiceGUID.Text, out result);
        if (msg.Length > 0) return msg;

        InvoiceDataWS invoiceDataWS = null;
        msg = Convertor.StringToObject(rdXML.Checked, Convert.ToString(result.Object), out invoiceDataWS);
        if (msg.Length > 0) return msg;

        InvoiceNo = invoiceDataWS.Invoice.InvoiceNo;

        return msg;
    }

    protected void btnGetStatus_Click(object sender, EventArgs e)
    {
        string msg = "";

        Result result = null;
        msg = remoteCommand.TransferCommandAndProcessResult(CommandType.GetInvoiceStatusID, txtInvoiceGUID.Text, out result);
        if (msg.Length > 0) lblMsg.Text = "Có lỗi: " + msg;
        else lblMsg.Text = "Mã trạng thái: " + result.Object.ToString(); // Lấy Mã trạng thái của Hóa đơn trong result.Object
    }

    protected void btnGetHistory_Click(object sender, EventArgs e)
    {
        string msg = "";

        Result result = null;
        msg = remoteCommand.TransferCommandAndProcessResult(CommandType.GetInvoiceHistory, txtInvoiceGUID.Text, out result);

        if (msg.Length > 0) lblMsg.Text = "Có lỗi: " + msg;
        else
        {
            // Nếu cần chuyển từ kết quả trả về là 1 json sang list object HistoryLog thì thực hiện các lệnh bên dưới:
            List<HistoryLog> ListHistoryLog = null;
            msg = Convertor.StringToObject(rdXML.Checked, Convert.ToString(result.Object), out ListHistoryLog);
            if (msg.Length > 0) lblMsg.Text = msg;
            if (ListHistoryLog != null && ListHistoryLog.Count > 0) lblMsg.Text = "Lịch sử: " + ListHistoryLog[0].LogContent;
            else lblMsg.Text = "Lịch sử: Không có";
        }
    }

    protected void btnUpdateInvoiceByInvoiceGUID_Click(object sender, EventArgs e)
    {
        string msg = DoUpdateInvoice(CommandType.UpdateInvoiceByInvoiceGUID);
        if (msg.Length > 0) lblMsg.Text = "Có lỗi: " + msg;
        else lblMsg.Text = "Update thành công";
    }
    protected void btnUpdateInvoiceByPartnerInvoiceID_Click(object sender, EventArgs e)
    {
        string msg = DoUpdateInvoice(CommandType.UpdateInvoiceByPartnerInvoiceID);
        if (msg.Length > 0) lblMsg.Text = "Có lỗi: " + msg;
        else lblMsg.Text = "Update thành công";
    }
    string DoUpdateInvoice(int CmdType)
    {
        string msg = "";

        string list = null;
        msg = GetListInvoiceDataWS(int.Parse(ddlCommandType.SelectedValue), out list);
        if (msg.Length > 0) return msg;

        Result result = null;
        msg = remoteCommand.TransferCommandAndProcessResult(CmdType, list, out result);
        if (msg.Length > 0) return msg;

        // Không có lỗi, Hệ thống trả ra danh sách kết quả của Hóa đơn
        List<InvoiceResult> listInvoiceResult = null;
        msg = Convertor.StringToObject(rdXML.Checked, Convert.ToString(result.Object), out listInvoiceResult);
        if (msg.Length > 0) return msg;

        foreach (InvoiceResult invoiceResult in listInvoiceResult)
            if (invoiceResult.Status != 0) return invoiceResult.MessLog;

        return msg;
    }

    protected void btnCancelInvoiceByInvoiceGUID_Click(object sender, EventArgs e)
    {
        string msg = CancelInvoice(CommandType.CancelInvoiceByInvoiceGUID);
        if (msg.Length > 0) lblMsg.Text = "Có lỗi: " + msg;
        else lblMsg.Text = "Update thành công";
    }
    protected void btnCancelInvoiceByPartnerInvoiceID_Click(object sender, EventArgs e)
    {
        string msg = CancelInvoice(CommandType.CancelInvoiceByPartnerInvoiceID);
        if (msg.Length > 0) lblMsg.Text = "Có lỗi: " + msg;
        else lblMsg.Text = "Update thành công";
    }
    private string CancelInvoice(int CmdType)
    {
        string msg = "";

        List<InvoiceDataWS> listInvoiceDataWS = new List<InvoiceDataWS>();
        InvoiceDataWS invoiceDataWS = new InvoiceDataWS();
        if (CmdType == CommandType.CancelInvoiceByPartnerInvoiceID)
        {
            invoiceDataWS.PartnerInvoiceID = (txtCancelPartnerInvoiceID.Text.Length > 0 ? long.Parse(txtCancelPartnerInvoiceID.Text) : (long.Parse(DateTime.Now.ToString("ddMMyyy HH:mm:ss").Replace(" ", "").Replace(":", ""))));//hoắc   
            invoiceDataWS.PartnerInvoiceStringID = "";
        }
        else
        {
            invoiceDataWS.Invoice = new InvoiceWS();
            Guid guid = Guid.Empty;
            msg = Convertor.StringToGuid(txtCancelPartnerInvoiceID.Text, out guid);
            if (msg.Length > 0) return "Giá trị không thể convert sang GUID";
            invoiceDataWS.Invoice.InvoiceGUID = guid;
        }

        listInvoiceDataWS.Add(invoiceDataWS);

        string list = null;
        msg = Convertor.ObjectToString<List<InvoiceDataWS>>(rdXML.Checked, listInvoiceDataWS, out list);
        if (msg.Length > 0) return msg;

        Result result = null;
        msg = remoteCommand.TransferCommandAndProcessResult(CmdType, list, out result);
        if (msg.Length > 0) return msg;

        // Không có lỗi, Hệ thống trả ra danh sách kết quả của Hóa đơn
        List<InvoiceResult> listInvoiceResult = null;
        msg = Convertor.StringToObject(rdXML.Checked, Convert.ToString(result.Object), out listInvoiceResult);
        if (msg.Length > 0) return msg;

        foreach (InvoiceResult invoiceResult in listInvoiceResult)
            if (invoiceResult.Status != 0) return invoiceResult.MessLog;

        return msg;
    }

    protected void btnDeleteInvoiceByInvoiceGUID_Click(object sender, EventArgs e)
    {
        string msg = DeleteInvoice(CommandType.DeleteInvoiceByInvoiceGUID);
        if (msg.Length > 0) lblMsg.Text = "Có lỗi: " + msg;
        else lblMsg.Text = "Update thành công";
    }
    protected void btnDeleteInvoiceByPartnerInvoiceID_Click(object sender, EventArgs e)
    {
        string msg = DeleteInvoice(CommandType.DeleteInvoiceByPartnerInvoiceID);
        if (msg.Length > 0) lblMsg.Text = "Có lỗi: " + msg;
        else lblMsg.Text = "Update thành công";
    }
    private string DeleteInvoice(int CmdType)
    {
        string msg = "";

        List<InvoiceDataWS> listInvoiceDataWS = new List<InvoiceDataWS>();
        InvoiceDataWS invoiceDataWS = new InvoiceDataWS();
        if (CmdType == CommandType.DeleteInvoiceByPartnerInvoiceID)
        {
            //Chỉ được truyền giá trị cho PartnerInvoiceID trong hoặc PartnerInvoiceStringID
            invoiceDataWS.PartnerInvoiceID = (txtDeleteInvoice.Text.Length > 0 ? long.Parse(txtDeleteInvoice.Text) : (long.Parse(DateTime.Now.ToString("ddMMyyy HH:mm:ss").Replace(" ", "").Replace(":", ""))));
            invoiceDataWS.PartnerInvoiceStringID = "";
        }
        else
        {
            invoiceDataWS.Invoice = new InvoiceWS();
            Guid guid = Guid.Empty;
            msg = Convertor.StringToGuid(txtDeleteInvoice.Text, out guid);
            if (msg.Length > 0) return "Giá trị không thể convert sang GUID";
            invoiceDataWS.Invoice.InvoiceGUID = guid;
        }

        listInvoiceDataWS.Add(invoiceDataWS);

        string list = null;
        msg = Convertor.ObjectToString<List<InvoiceDataWS>>(rdXML.Checked, listInvoiceDataWS, out list);
        if (msg.Length > 0) return msg;

        Result result = null;
        msg = remoteCommand.TransferCommandAndProcessResult(CmdType, list, out result);
        if (msg.Length > 0) return msg;

        // Không có lỗi, Hệ thống trả ra danh sách kết quả của Hóa đơn
        List<InvoiceResult> listInvoiceResult = null;
        msg = Convertor.StringToObject(rdXML.Checked, Convert.ToString(result.Object), out listInvoiceResult);
        if (msg.Length > 0) return msg;

        foreach (InvoiceResult invoiceResult in listInvoiceResult)
            if (invoiceResult.Status != 0) return invoiceResult.MessLog;

        return msg;
    }

    protected void btnReplace_Click(object sender, EventArgs e)
    {
        string msg = DoReplaceInvoice();
        if (msg.Length > 0) lblMsg.Text = "Có lỗi: " + msg;
        else lblMsg.Text = "Thay thế thành công";
    }
    string DoReplaceInvoice()
    {
        string msg = "";

        string list = null;
        msg = GetListInvoiceDataWS(CommandType.CreateInvoiceReplace, out list);
        if (msg.Length > 0) return msg;

        Result result = null;
        msg = remoteCommand.TransferCommandAndProcessResult(CommandType.CreateInvoiceReplace, list, out result);
        if (msg.Length > 0) return msg;

        // Không có lỗi, Hệ thống trả ra danh sách kết quả của Hóa đơn
        List<InvoiceResult> listInvoiceResult = null;
        msg = Convertor.StringToObject(rdXML.Checked, Convert.ToString(result.Object), out listInvoiceResult);
        if (msg.Length > 0) return msg;

        foreach (InvoiceResult invoiceResult in listInvoiceResult)
            if (invoiceResult.Status != 0) return invoiceResult.MessLog;

        return msg;
    }

    protected void btnAdjustInvoice_Click(object sender, EventArgs e)
    {
        string msg = DoAdjustInvoice();
        if (msg.Length > 0) lblMsg.Text = "Có lỗi: " + msg;
        else lblMsg.Text = "Điều chỉnh thành công";
    }
    string DoAdjustInvoice()
    {
        string msg = "";

        string list = null;
        msg = GetListInvoiceDataWS(CommandType.CreateInvoiceAdjust, out list);
        if (msg.Length > 0) return msg;

        Result result = null;
        msg = remoteCommand.TransferCommandAndProcessResult(CommandType.CreateInvoiceAdjust, list, out result);
        if (msg.Length > 0) return msg;

        // Không có lỗi, Hệ thống trả ra danh sách kết quả của Hóa đơn
        List<InvoiceResult> listInvoiceResult = null;
        msg = Convertor.StringToObject(rdXML.Checked, Convert.ToString(result.Object), out listInvoiceResult);
        if (msg.Length > 0) return msg;

        foreach (InvoiceResult invoiceResult in listInvoiceResult)
            if (invoiceResult.Status != 0) return invoiceResult.MessLog;

        return msg;
    }

    protected void btnCreateAcount_Click(object sender, EventArgs e)
    {
        string msg = CreateAcountDemo();
        if (msg.Length > 0) lblMsg.Text = "Có lỗi: " + msg;
        else lblMsg.Text = "Tạo tài khoản thành công";
    }
    private string CreateAcountDemo()
    {
        string msg = "";
        CreateAccountInfoFromPartner createAccountInfoFromPartner = new CreateAccountInfoFromPartner();
        //Thông tin đơn vị
        createAccountInfoFromPartner.TaxCode = DateTime.Now.ToString("ddMMyy mm:ss").Replace(" ", "").Replace(":", ""); //Mã số thuế
        createAccountInfoFromPartner.UnitName = "Công ty BKAV"; //Tên đơn vị
        createAccountInfoFromPartner.UnitAddress = "Tòa nhà HH1, P. Yên Hòa, Q. Cầu Giấy, Hà Nội"; //Địa chỉ
        createAccountInfoFromPartner.TaxDepartmentID = 1; //Cơ quan Thuế quản lý
        createAccountInfoFromPartner.UnitPersonRepresent = "Nguyễn Văn A"; //Người đại diện pháp luật
        createAccountInfoFromPartner.UnitPersonRepresentPosition = "Giám Đốc"; //Chức danh 
        createAccountInfoFromPartner.UnitEmail = "demo@bkav.com"; //Email nhận Hoá đơn
        createAccountInfoFromPartner.UnitPhone = "1234567890"; //SĐT liên hệ
        createAccountInfoFromPartner.BankAccount = "2222222222"; //Tài khoản Ngân hàng
        createAccountInfoFromPartner.BankName = "BIDV"; //Tên ngân Hàng        
        createAccountInfoFromPartner.BrandName = "BKAV"; //Tên thương hiệu
        createAccountInfoFromPartner.DomainCheckInvoice = "tracuu.ehoadon.vn"; //Tên miền tra cứu

        string strReturn = "";
        msg = Convertor.ObjectToString<CreateAccountInfoFromPartner>(rdXML.Checked, createAccountInfoFromPartner, out strReturn);

        Result result = null;
        msg = remoteCommand.TransferCommandAndProcessResult(CommandType.CreateAccount, strReturn, out result);
        if (msg.Length > 0) return msg;

        // Không có lỗi, Hệ thống trả ra danh sách kết quả của Hóa đơn
        AccountResult oAccountResult = null;
        msg = Convertor.StringToObject(rdXML.Checked, Convert.ToString(result.Object), out oAccountResult);
        if (msg.Length > 0) return msg;

        txtAccount.Text = oAccountResult.Account + "/" + oAccountResult.Password;
        return msg;
    }

    protected void btnGetInvoiceLink_Click(object sender, EventArgs e)
    {
        string msg = GetInvoiceLink();
        if (msg.Length > 0) lblMsg.Text = "Có lỗi: " + msg;
        else lblMsg.Text = "Update thành công";
    }
    private string GetInvoiceLink()
    {
        string msg = "";

        List<InvoiceDataWS> listInvoiceDataWS = new List<InvoiceDataWS>();
        InvoiceDataWS invoiceDataWS = new InvoiceDataWS();
        //Chỉ được truyền giá trị cho PartnerInvoiceID trong hoặc PartnerInvoiceStringID
        invoiceDataWS.PartnerInvoiceID = (txtGetInvoiceLink.Text.Length > 0 ? long.Parse(txtGetInvoiceLink.Text) : (long.Parse(DateTime.Now.ToString("ddMMyyy HH:mm:ss").Replace(" ", "").Replace(":", ""))));
        invoiceDataWS.PartnerInvoiceStringID = "";
        listInvoiceDataWS.Add(invoiceDataWS);

        string list = null;
        msg = Convertor.ObjectToString<List<InvoiceDataWS>>(rdXML.Checked, listInvoiceDataWS, out list);

        Result result = null;
        msg = remoteCommand.TransferCommandAndProcessResult(CommandType.GetInvoiceLink, list, out result);
        if (msg.Length > 0) return msg;

        // Không có lỗi, Hệ thống trả ra danh sách kết quả của Hóa đơn
        List<InvoiceResult> listInvoiceResult = null;
        msg = Convertor.StringToObject(rdXML.Checked, Convert.ToString(result.Object), out listInvoiceResult);
        if (msg.Length > 0) return msg;

        foreach (InvoiceResult invoiceResult in listInvoiceResult)
        {
            if (invoiceResult.Status == 0) { msg = ""; lblMsg.Text = invoiceResult.MessLog; }//Link tải trả về trong trường MessLog
            else return invoiceResult.MessLog;
        }

        return msg;
    }

    protected void btnGetUnitInforByTaxCode_Click(object sender, EventArgs e)
    {
        string msg = "";

        Result result = null;
        msg = remoteCommand.TransferCommandAndProcessResult(CommandType.GetUnitInforByTaxCode, txtTaxcode.Text, out result);
        if (msg.Length > 0) lblMsg.Text = "Có lỗi: " + msg;
        else
        {// Không có lỗi, Hệ thống trả đối tượng chưa thông tin doanh nghiệp
            BusinessInfo oBusinessInfo = null;
            msg = Convertor.StringToObject(rdXML.Checked, Convert.ToString(result.Object), out oBusinessInfo);
            if (msg.Length > 0) lblMsg.Text = msg;

            lblMsg.Text = oBusinessInfo.TrangThaiHoatDong;
        }
    }

    protected void btnGetDLLContent_Click(object sender, EventArgs e)
    {
        string msg = "";

        Result result = null;
        msg = remoteCommand.TransferCommandAndProcessResult(CommandType.GetDLLContent, txtTaxcode.Text, out result);
        if (msg.Length > 0) lblMsg.Text = "Có lỗi: " + msg;
        else
        {
            DllInfo oDllInfo = null;
            msg = Convertor.StringToObject(rdXML.Checked, Convert.ToString(result.Object), out oDllInfo);
            if (msg.Length > 0) lblMsg.Text = msg;

            var fullPath = "E:\\" + oDllInfo.DLLName;
            File.WriteAllBytes(fullPath, oDllInfo.DLLContent);
        }
    }

    #region Tạo và gán dữ liệu cho 1 tờ hóa đơn (object InvoiceDataWS)
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Ví dụ đoạn code tạo và gán dữ liệu cho 1 tờ hóa đơn (object InvoiceDataWS) 
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// out dữ liệu json hoặc xml.
    /// </summary>
    /// <param name="commandType"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    string GetListInvoiceDataWS(int commandType, out string list)
    {
        list = null;

        List<InvoiceDataWS> listInvoiceDataWS = null;
        PrepareInvoiceData(commandType, out listInvoiceDataWS);

        return Convertor.ObjectToString<List<InvoiceDataWS>>(rdXML.Checked, listInvoiceDataWS, out list);
    }
    void PrepareInvoiceData(int commandType, out List<InvoiceDataWS> listInvoiceDataWS)
    {
        listInvoiceDataWS = new List<InvoiceDataWS>();
        // Một tờ Hóa đơn được lưu trong 1 object InvoiceDataWS. Object này có 4 properties Invoice, ListInvoiceDetails, IsSetInvoiceNo, TransactionID
        // Header của tờ Hóa đơn (như thông tin người bán, người mua...) được lưu trong Invoice
        // Chi tiết của tờ Hóa đơn (là các item hàng hóa, dịch vụ) được lưu trong InvoiceDetails. Nhiều item được lưu trong ListInvoiceDetails

        // Tạo Header của tờ Hóa đơn
        InvoiceWS invoice = GetOneInvoiceWS(commandType);

        // Tạo danh sách các item
        List<InvoiceDetailsWS> invoiceDetails = new List<InvoiceDetailsWS>();
        InvoiceDetailsWS invoiceDetail = null;
        // Tạo 1 item
        if (commandType == CommandType.CreateInvoiceAdjust)// Điều chỉnh thì sẽ bổ sung thêm IsIncrease: True là báo tăng, False báo giảm
            invoiceDetail = GetOneInvoiceDetailsWS(true);
        else invoiceDetail = GetOneInvoiceDetailsWS();

        // Add item vào list các item
        invoiceDetails.Add(invoiceDetail);


        ////Tạo List tương tự
        InvoiceDataWS invoiceDataWS = new InvoiceDataWS();
        invoiceDataWS.Invoice = invoice;
        invoiceDataWS.ListInvoiceDetailsWS = invoiceDetails;
        invoiceDataWS.PartnerInvoiceID = long.Parse(DateTime.Now.ToString("ddMMyyyy HH:mm:ss").Replace(" ", "").Replace(":", ""));//hoắc        
        invoiceDataWS.PartnerInvoiceStringID = "";

        //InvoiceAttachFileWS oInvoiceAttachFileWS = new InvoiceAttachFileWS();

        //byte[] barr = File.ReadAllBytes(@"D:\Working\DemoWSeHoaDon_Bkav\FileTest\Test.docx");
        //string content = Convert.ToBase64String(barr);

        //oInvoiceAttachFileWS.FileName = "Test";
        //oInvoiceAttachFileWS.FileExtension = "docx";
        //oInvoiceAttachFileWS.FileContent = "";// content;// "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";

        //List<InvoiceAttachFileWS> oListInvoiceAttachFileWSWS = new List<InvoiceAttachFileWS>();
        //oListInvoiceAttachFileWSWS.Add(oInvoiceAttachFileWS);
        //oListInvoiceAttachFileWSWS.Add(oInvoiceAttachFileWS);
        //invoiceDataWS.ListInvoiceAttachFileWS = oListInvoiceAttachFileWSWS;

        //listInvoiceDataWS.Add(invoiceDataWS);
        listInvoiceDataWS.Add(invoiceDataWS);
    }

    /// <summary>
    /// Khởi tạo dữ ilệu InvoiceWS
    /// </summary>
    /// <returns></returns>
    InvoiceWS GetOneInvoiceWS(int commandType)
    {
        InvoiceWS invoiceWS = new InvoiceWS();
        invoiceWS.InvoiceTypeID = 1;
        invoiceWS.InvoiceDate = DateTime.Now;
        invoiceWS.BuyerName = "Nguyễn Văn A Update";
        invoiceWS.BuyerTaxCode = "0104746603";
        invoiceWS.BuyerUnitName = "Công Ty Luật TNHH ABC";
        invoiceWS.BuyerAddress = "Nhà N2D Khu ĐT Trung Hoà-Nhân Chính, Phường Nhân Chính, Quận Thanh Xuân, Hà Nội";
        invoiceWS.BuyerBankAccount = "";
        invoiceWS.PayMethodID = 1;
        invoiceWS.ReceiveTypeID = 3;
        invoiceWS.ReceiverEmail = "dtdnhathoang2017@gmail.com";
        invoiceWS.ReceiverMobile = "01789143399";
        invoiceWS.ReceiverAddress = "Nhà N2D Khu ĐT Trung Hoà-Nhân Chính, Phường Nhân Chính, Quận Thanh Xuân, Hà Nội";
        invoiceWS.ReceiverName = "Nguyễn Văn A";
        invoiceWS.Note = "Test eHoaDon";
        invoiceWS.BillCode = "";
        invoiceWS.CurrencyID = "VND";
        invoiceWS.ExchangeRate = 1;
        invoiceWS.InvoiceStatusID = 1;
        invoiceWS.SignedDate = DateTime.Now;

        switch (commandType)
        {
            case CommandType.CreateInvoiceMT:
                invoiceWS.InvoiceNo = 0;
                invoiceWS.InvoiceForm = "";
                invoiceWS.InvoiceSerial = ""; break;
            case CommandType.CreateInvoiceTR:
                invoiceWS.InvoiceNo = 0;
                invoiceWS.InvoiceForm = "";
                invoiceWS.InvoiceSerial = ""; break;
            case CommandType.CreateInvoiceWithFormSerial:
                invoiceWS.InvoiceNo = 0;
                invoiceWS.InvoiceForm = "01GTKT0/003";
                invoiceWS.InvoiceSerial = "AB/17E"; break;
            case CommandType.CreateInvoiceWithFormSerialNo:
                invoiceWS.InvoiceNo = 12034;
                invoiceWS.InvoiceForm = "01GTKT0/003";
                invoiceWS.InvoiceSerial = "AB/17E"; break;
            case CommandType.CreateInvoiceReplace:
                invoiceWS.InvoiceNo = 0;
                invoiceWS.InvoiceForm = "";
                invoiceWS.InvoiceSerial = "";
                invoiceWS.OriginalInvoiceIdentify = "[01GTKT0/003]_[AA/17E]_[0000105]"; break;
            case CommandType.CreateInvoiceAdjust:
                invoiceWS.InvoiceNo = 0;
                invoiceWS.InvoiceForm = "";
                invoiceWS.InvoiceSerial = "";
                invoiceWS.OriginalInvoiceIdentify = "[01GTKT0/003]_[AB/17E]_[0000133]"; break;
            case CommandType.UpdateInvoiceByInvoiceGUID:
                Guid guid = Guid.Empty;
                Convertor.StringToGuid(txtPartnerInvoiceID.Text, out guid);
                invoiceWS.InvoiceGUID = guid;
                break;

            default:
                invoiceWS.InvoiceNo = 0;
                invoiceWS.InvoiceForm = "";
                invoiceWS.InvoiceSerial = "";
                break;
        }
        return invoiceWS;
    }

    /// <summary>
    /// Khởi tạo dữ liệu InvoiceDetailsWS
    /// </summary>
    /// <returns></returns>
    InvoiceDetailsWS GetOneInvoiceDetailsWS()
    {
        InvoiceDetailsWS invoiceDetailsWS = new InvoiceDetailsWS();
        invoiceDetailsWS.ItemName = "Chữ ký số Bkav CA ENT BN (bao gồm Thiết bị USB Token) update";
        invoiceDetailsWS.UnitName = "Gói";
        invoiceDetailsWS.Qty = 1;
        invoiceDetailsWS.Price = 600000;
        invoiceDetailsWS.Amount = 600000;
        invoiceDetailsWS.TaxRateID = 3;
        invoiceDetailsWS.TaxAmount = 60000;
        invoiceDetailsWS.IsDiscount = false; // Có giảm giá hay không?
        invoiceDetailsWS.UserDefineDetails = "string đối tượng dạng json hoặc xml";
        return invoiceDetailsWS;
    }

    /// <summary>
    /// Khởi tạo dữ liệu InvoiceDetailsWS
    /// </summary>
    /// <returns></returns>
    InvoiceDetailsWS GetOneInvoiceDetailsWS(bool isIncrease)
    {
        InvoiceDetailsWS invoiceDetailsWS = new InvoiceDetailsWS();
        invoiceDetailsWS.ItemName = "Chữ ký số Bkav CA ENT BN (bao gồm Thiết bị USB Token) update";
        invoiceDetailsWS.UnitName = "Gói";
        invoiceDetailsWS.Qty = 1;
        invoiceDetailsWS.Price = 600000;
        invoiceDetailsWS.Amount = 600000;
        invoiceDetailsWS.TaxRateID = 3;
        invoiceDetailsWS.TaxAmount = 60000;
        invoiceDetailsWS.IsDiscount = false; // Có giảm giá hay không?
        invoiceDetailsWS.UserDefineDetails = "string đối tượng dạng json hoặc xml";
        invoiceDetailsWS.IsIncrease = true;// Fix mặc định báo tăng
        return invoiceDetailsWS;
    }
    #endregion

}