namespace V6Init
{
    public static class V6Text
    {
        public static string FieldCaption(string field)
        {
            return CorpLan2.GetFieldHeader(field);
        }
        
        public static string LabelCaption(string accessibleName)
        {
            return CorpLan.GetText(accessibleName);
        }

        public static string Text(string ID)
        {
            return CorpLan1.GetText(ID);
        }

        public static bool LoadText(string s)
        {
            return true;
        }

        public static string DaPhatSinh_KhongDuocXoa
        {
            get
            {
                var a = Text("DACOPSKOXOA");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "Đã có phát sinh, không được xóa!" : "Has been used, can not delete!";
            }
        }

        
        public static string ExitQuestion
        {
            get
            {
                var a = Text("V6INIT0003");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "Thoát chương trình?" : "Exit?";
            }
        }

        public static string New
        {
            get { return V6Setting.Language == "V" ? "Mới" : "New"; }
        }

        public static string Edit
        {
            get { return V6Setting.Language == "V" ? "Sửa" : "Edit"; }
        }

        public static string EditWarning
        {
            get { return V6Setting.Language == "V" ? "Chú ý đã có chứng từ thanh toán!" : "Vouchers have been paid!"; }
        }


        public static string Cancel
        {
            get { return V6Setting.Language == "V" ? "Hủy" : "Cancel"; }
        }

        public static string ChuaNhapSTT
        {
            get { return Text("INPUT00001"); }
        }

        public static string ChuaNhapTenButToan
        {
            get { return Text("INPUT00002"); }
        }

        public static string PhanBo
        {
            get
            {
                var a = Text("V6INIT0006");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "Phân bổ hóa đơn" : "Allocation";
            }
        }
        public static string PhanBoTrucTiep
        {
            get
            {
                var a = Text("V6INIT0008");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "Phân bổ trực tiếp" : "Direct Allocation";
            }
        }

        public static string PrintAuto
        {
            get
            {
                var a = Text("PRINTAUTO");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In tự động." : "Print automatically.";
            }
        }

        public static string PrintAP1
        {
            get
            {
                var a = Text("PRINTAP1");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In hóa đơn mua hàng dịch vụ" : "Print purchase services invoice";
            }
        }
        public static string PrintAR1
        {
            get
            {
                var a = Text("PRINTAR1");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In hóa đơn dịch vụ" : "Print services invoice";
            }
        }
        public static string PrintGL1
        {
            get
            {
                var a = Text("PRINTGL1");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In phiếu kế toán" : "Print general voucher";
            }
        }
        /// <summary>
        /// In hóa đơn / Print invoice
        /// </summary>
        public static string PrintSOA
        {
            get
            {
                var a = Text("PRINTSOA");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In hóa đơn" : "Print invoice";
            }
        }
        public static string PrintSOB
        {
            get
            {
                var a = Text("PRINTSOB");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In hóa đơn dịch vụ" : "Print services invoice";
            }
        }
        public static string PrintSOC
        {
            get
            {
                var a = Text("PRINTSOC");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In hóa đơn bán lẻ" : "Print retail invoice";
            }
        }
        public static string PrintSOF
        {
            get
            {
                var a = Text("PRINTSOF");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In hàng trả lại" : "Print sales returns";
            }
        }
        public static string PrintSOH
        {
            get
            {
                var a = Text("PRINTSOH");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In đơn đặt hàng bán" : "Print sales order";
            }
        }
        public static string PrintSOR
        {
            get
            {
                var a = Text("PRINTSOR");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In báo giá" : "Print price quotation";
            }
        }

        public static string PrintTA1
        {
            get
            {
                var a = Text("PRINTTA1");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In phiếu thu" : "Print receipt";
            }
        }
        public static string PrintBC1
        {
            get
            {
                var a = Text("PRINTBC1");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In phiếu thu ngân hàng" : "Print bank receipt";
            }
        }
        public static string PrintCA1
        {
            get
            {
                var a = Text("PRINTCA1");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In phiếu thu" : "Print disbursement ";
            }
        }
        public static string PrintBN1
        {
            get
            {
                var a = Text("PRINTBN1");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In phiếu chi ngân hàng" : "Print bank disbursement ";
            }
        }
        public static string PrintPOA
        {
            get
            {
                var a = Text("PRINTPOA");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In phiếu nhập mua" : "Print purchase";
            }
        }
        public static string PrintPOB
        {
            get
            {
                var a = Text("PRINTPOB");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In phiếu nhập khẩu" : "Print import purchase";
            }
        }
        public static string PrintPOC
        {
            get
            {
                var a = Text("PRINTPOC");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In phiếu nhập chi phí mua hàng" : "Print purchase fee invoice";
            }
        }
        public static string PrintPOH
        {
            get
            {
                var a = Text("PRINTPOH");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In đơn đặt hàng mua" : "Print purchase order";
            }
        }
        public static string PrintIXA
        {
            get
            {
                var a = Text("PRINTIXA");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In phiếu xuất kho" : "Print stock out";
            }
        }
        public static string PrintIXB
        {
            get
            {
                var a = Text("PRINTIXB");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In phiếu xuất điều chuyển" : "Print stock transfer";
            }
        }
        public static string PrintIXC
        {
            get
            {
                var a = Text("PRINTIXC");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In phiếu xuất trả lại nhà cung cấp" : "Print return to supplier";
            }
        }
        public static string PrintIND
        {
            get
            {
                var a = Text("PRINTIND");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "In phiếu nhập kho" : "Print stock in";
            }
        }
        public static string ShowMenu
        {
            get
            {
                var text = Text("SHOW");
                if (text != "") return text;
                return V6Setting.Language == "V" ? "Hiện ra" : "Show";
            }
        }

        public static string NotSupported
        {
            get
            {
                var text = Text("NotSupported");
                if (text != "") return text;
                return V6Setting.Language == "V" ? "Chưa hỗ trợ!" : "Not Supported!";
            }
        }

        public static string NoRight
        {
            get
            {
                var a = Text("CORPRUN0001");
                if (a != "") return a;
                return V6Setting.Language == "V" ? "Không có quyền!" : "No right!";
            }
        }

        public static string Action { get { return V6Setting.Language == "V" ? "Xử lý dữ liệu." : "Action."; } }
        public static string Add { get { return V6Setting.Language == "V" ? "Thêm " : "Add "; } }
        public static string AddInParentheses { get { return V6Setting.Language == "V" ? "(Thêm)" : "(Add)"; } }
        public static string Added { get { return V6Setting.Language == "V" ? "Đã thêm " : "Added "; } }
        public static string AddDenied { get { return V6Setting.Language == "V" ? "Không được thêm." : "Add denied."; } }
        public static string AddSuccess { get { return V6Setting.Language == "V" ? "Thêm xong!" : "Add success!"; }}
        public static string AddFail { get { return V6Setting.Language == "V" ? "Thêm lỗi!" : "Add fail!"; } }
        public static string BackConfirm { get { return V6Setting.Language == "V" ? "Có chắc bạn muốn quay ra?" : "Are you sure you want to go back?"; } }
        public static string Busy { get { return V6Setting.Language == "V" ? "Đang bận!" : "Busy!"; } }
        public static string Close { get { return V6Setting.Language == "V" ? "Đóng lại" : "Close"; } }
        public static string CloseConfirm { get { return V6Setting.Language == "V" ? "Có chắc bạn muốn đóng lại?" : "Are you sure want to close?"; } }
        public static string Delete { get { return V6Setting.Language == "V" ? "Xóa" : "Delete"; } }
        public static string Deleted { get { return V6Setting.Language == "V" ? "Đã xóa" : "Deleted"; } }
        public static string Confirm { get { return V6Setting.Language == "V" ? "Xác nhận" : "Confirm"; } }
        public static string CheckData { get { return V6Setting.Language == "V" ? "Kiểm tra lại dữ liệu!" : "Check data!"; } }
        public static string CheckInfor { get { return V6Setting.Language == "V" ? "Kiểm tra thông tin chưa đầy đủ!" : "Check information again !"; } }
        public static string CheckLock { get { return V6Setting.Language == "V" ? "Đã khóa số liệu!" : "Data locking !"; } }
        public static string CheckDeclare { get { return V6Setting.Language == "V" ? "Kiểm tra khai báo hệ thống quản lý !" : "Check system information again !"; } }
        public static string DataLoading { get { return V6Setting.Language == "V" ? "Đang tải dữ liệu." : "Data loading."; } }
        public static string DeleteConfirm { get { return V6Setting.Language == "V" ? "Có chắc chắn xóa?" : "Are you sure to delete?"; } }
        public static string DeleteRowConfirm { get { return V6Setting.Language == "V" ? "Có chắc chắn xóa dòng đang chọn?" : "Are you sure to delete selected row?"; } }
        public static string DeleteDenied { get { return V6Setting.Language == "V" ? "Không được xóa." : "Delete Denied."; } }
        
        public static string DeleteFail { get { return V6Setting.Language == "V" ? "Xóa không thành công!" : "Delete fail!"; } }
        public static string DeleteSuccess { get { return V6Setting.Language == "V" ? "Xóa thành công." : "Delete success."; } }
        public static string DetailNotComplete { get { return V6Setting.Language == "V" ? "Chưa hoàn tất chi tiết!" : "Detail is not complete!"; } }
        public static string DiscardConfirm { get { return V6Setting.Language == "V" ? "Hủy bỏ các thay đổi?" : "Discard changes?"; } }
        public static string EditDenied { get { return V6Setting.Language == "V" ? "Không được sửa." : "Edit Denied."; } }
        public static string ExecuteConfirm { get { return V6Setting.Language == "V" ? "Có chắc chắn thực hiện hay không?" : "Are you sure want to proceed?"; } }
        public static string Export { get { return V6Setting.Language == "V" ? "Xuất" : "Export"; } }
        public static string ExportFinish { get { return V6Setting.Language == "V" ? "Xuất hoàn tất." : "Export finish."; } }
        public static string ExportFail { get { return V6Setting.Language == "V" ? "Xuất lỗi." : "Export fail."; } }
        public static string Exporting { get { return V6Setting.Language == "V" ? "Đang xuất..." : "Exporting..."; } }
        public static string Exist { get { return V6Setting.Language == "V" ? "Đã tồn tại." : "Exist."; } }
        public static string DataExist { get { return V6Setting.Language == "V" ? "Dữ liệu đã tồn tại." : "Data Exist."; } }
        public static string EditSuccess { get { return V6Setting.Language == "V" ? "Sửa xong!" : "Edit success!"; } }
        public static string EditFail { get { return V6Setting.Language == "V" ? "Sửa bị lỗi!" : "Edit fail!"; } }
        public static object Empty { get { return V6Setting.Language == "V" ? "Rỗng" : "Empty"; } }
        public static string Executing { get { return V6Setting.Language == "V" ? "Đang xử lý." : "Executing."; } }
        public static string F348 { get { return V6Setting.Language == "V" ? "F3: Sửa, F4: Thêm, F8: Xóa" : "F3: Edit, F4: Add New, F8: Delete "; } }
        
        public static string Fail { get { return V6Setting.Language == "V" ? "Hỏng." : "Fail."; } }
        public static string Finish { get { return V6Setting.Language == "V" ? "Hoàn thành." : "Finish."; } }
        public static string Help { get { return V6Setting.Language == "V" ? "Trợ giúp" : "Help"; } }
        public static string Invoice { get { return V6Setting.Language == "V" ? "Chứng từ" : "Invoice"; } }
        public static string InvoiceF3AddDetailSuccess { get { return V6Setting.IsVietnamese ? "Đã thêm thành công 1 chi tiết và bắt đầu thêm mới một dòng khác." : "Successfully added 1 detail and started adding a new line."; } }
        public static string InvoiceF3EditDetailSuccess { get { return V6Setting.IsVietnamese ? "Đã sửa chi tiết và bắt đầu thêm mới một dòng khác." : "Corrected details and started adding another line."; } }
        public static string NoConnection { get { return V6Setting.Language == "V" ? "Không thể kết nối server!" : "Can not connect to server!"; } }
        public static string NoData { get { return V6Setting.Language == "V" ? "Không có dữ liệu." : "No data."; } }
        public static string NoDefine { get { return V6Setting.Language == "V" ? "Chưa định nghĩa." : "No define."; } }
        /// <summary>
        /// Chưa nhập giá trị. (Chưa nhập liệu)
        /// </summary>
        public static string NoInput { get { return V6Setting.Language == "V" ? "Chưa nhập giá trị. " : "No input. "; } }
        public static string NoInputDetail { get { return V6Setting.Language == "V" ? "Chưa nhập chi tiết." : "No detail(s) input."; } }
        /// <summary>
        /// Không có chứng từ nào như vậy!
        /// </summary>
        public static string NoInvoiceFound { get { return V6Setting.Language == "V" ? "Không có chứng từ nào như vậy!" : "No invoice found!"; } }
        public static string NoSelection { get { return V6Setting.Language == "V" ? "Chưa có lựa chọn." : "No selection."; } }
        public static string NoSTTREC { get { return V6Setting.Language == "V" ? "Không có STTREC." : "No STTREC."; } }
        public static string NotExist { get { return V6Setting.Language == "V" ? "Không tồn tại." : "Not exist."; } }
        public static string NotFound { get { return V6Setting.Language == "V" ? "Không tìm thấy." : "Not found."; } }
        public static string NotRunHere { get { return V6Setting.Language == "V" ? "Không xử lý ở đây." : "Not run here."; } }
        public static string NotValid { get { return V6Setting.Language == "V" ? "Không hợp lệ." : "Not valid."; } }
        public static string NoUID { get { return V6Setting.Language == "V" ? "Không có UID." : "No UID."; } }
        
        public static string NotAllow { get { return V6Setting.Language == "V" ? "Không được phép!" : "Not Allow!"; } }
        public static string NotAllowed { get { return V6Setting.Language == "V" ? "Chưa được phép!" : "Not Allowed!"; } }
        public static string NotAnAdmin { get { return V6Setting.Language == "V" ? "Không phải admin." : "Not an admin."; } }
        public static string OverFlow { get { return V6Setting.Language == "V" ? "Tràn dữ liệu!" : "OverFlow!"; } }
        public static string PreviewingMode { get { return V6Setting.IsVietnamese ? "Đang ở trạng thái xem!" : "Previewing mode!"; } }
        public static string ProcessNotComplete { get { return V6Setting.Language == "V" ? "Tiến trình chưa hoàn thành!" : "The process is not complete!"; } }
        public static string ReadOnly { get { return V6Setting.IsVietnamese ? "Chỉ đọc!" : "Readonly!"; } }
        public static string Ready { get { return V6Setting.Language == "V" ? "Sẵn sàng." : "Ready."; } }
        public static string ReloadConfirm { get { return V6Setting.Language == "V" ? "Có chắc bạn muốn tải lại?" : "Are you sure you want to reload?"; } }
        public static string ReportError { get { return V6Setting.Language == "V" ? "Báo cáo bị lỗi." : "Report error."; } }
        public static string Sec { get { return V6Setting.Language == "V" ? "Giây" : "Sec"; } }
        public static string SecretKeyFail { get { return V6Setting.Language == "V" ? "Sai khóa." : "No key."; } }
        public static string SelectAccount { get { return V6Setting.Language == "V" ? "Chưa chọn tài khoản!" : "Select account please!"; } }
        public static string SelectCustomer { get { return V6Setting.Language == "V" ? "Chưa chọn khách hàng!" : "Select customer please!"; } }
        public static string SelectItem { get { return V6Setting.Language == "V" ? "Chưa chọn mã vật tư!" : "Select item please!"; } }
        public static string SelectWarehouse { get { return V6Setting.Language == "V" ? "Chưa chọn mã kho!" : "Select warehouse please!"; } }
        /// <summary>
        /// Xuất > tồn
        /// </summary>
        public static string StockoutWarning { get { return V6Setting.Language == "V" ? "Xuất lớn hơn tồn!" : "Out > Stock!"; } }
        public static string Toolong { get { return V6Setting.Language == "V" ? "Quá dài !" : "Too long! "; } }
        public static string UnFinished { get { return V6Setting.IsVietnamese ? "Chưa xử lý xong!" : "UnFinished!"; } }
        public static string Unsupported { get { return V6Setting.Language == "V" ? "Chưa hỗ trợ!" : "Unsupported!"; } }
        public static string Updated { get { return V6Setting.Language == "V" ? "Đã cập nhập." : "Updated."; } }
        public static string UpdateSuccess { get { return V6Setting.Language == "V" ? "Cập nhập thành công." : "Update success."; } }
        public static string UpdateFail { get { return V6Setting.Language == "V" ? "Cập nhập lỗi." : "UpdateFail."; } }
        public static string ValidateFail { get { return V6Setting.IsVietnamese ? "Validate Fail!" : "Validate Fail"; } }
        public static string Voucher_exist { get { return V6Setting.Language == "V" ? "Chứng từ trùng số , vẫn cho lưu!" : "This voucher is exist, still save!"; } }
        public static string Voucher_exist_not_save { get { return V6Setting.Language == "V" ? "Chứng từ trùng số , không lưu được!" : "This voucher is exist, Not save!"; } }
        public static string Warning { get { return V6Setting.Language == "V" ? "Cảnh báo!" : "Warning!"; } }
        public static string Wrong { get { return V6Setting.Language == "V" ? "Nhập thông tin chưa đúng!" : "Wrong information!"; } }
        public static string WrongData { get { return Text("WRONGDATA"); } }
        public static string ZoomIn { get { return V6Setting.Language == "V" ? "Phóng" : "ZoomIn"; } }
        public static string ZoomOut { get { return V6Setting.Language == "V" ? "Thu" : "ZoomOut"; } }
        
        #region ==== Fuctions ====

        

        #endregion

        
    }
}
