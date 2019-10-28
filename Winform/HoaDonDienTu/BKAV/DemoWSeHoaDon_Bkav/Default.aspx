<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table>
        <tr>
            <td>Trao đổi dữ liệu với WS</td>
            <td class="auto-style1">
                <asp:RadioButton ID="rdJson" runat="server" ClientIDMode="Static" Text="Json"
                    GroupName="TransactionType" Checked="true" />
                <asp:RadioButton ID="rdXML" runat="server" ClientIDMode="Static" Text="XML"
                    GroupName="TransactionType" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Bước 1:</td>
            <td colspan="2" style="height: 50px">
                <asp:DropDownList ID="ddlCommandType" runat="server">
                    <asp:ListItem Value="100" Selected="True">Tạo HĐ, eHD tự cấp InvoiceForm, InvoiceSerial; InvoiceNo = 0 (tạo HĐ mới)</asp:ListItem>
                    <asp:ListItem Value="101">Tạo HĐ, eHD tự cấp InvoiceForm, InvoiceSerial và cấp InvoiceNo (tạo HĐ Trống)</asp:ListItem>
                    <asp:ListItem Value="110">Tạo HĐ, Client tự cấp InvoiceForm, InvoiceSerial; InvoiceNo = 0 (tạo HĐ mới)</asp:ListItem>
                    <asp:ListItem Value="111">Tạo HĐ, Client tự cấp InvoiceForm, InvoiceSerial, InvoiceNo (tạo HĐ mới, có sẵn Số HĐ)</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnCreateInvoice" runat="server" OnClick="btnCreateInvoice_Click" Text="Tạo và Gửi Hóa đơn tới Webservice" /></td>
        </tr>
        <tr>
            <td>Bước 2:</td>
            <td class="auto-style1">
                <asp:TextBox runat="server" ID="txtInvoiceGUID" placeholder="InvoiceGUID" Width="304px"></asp:TextBox></td>
            <td>
                <asp:Button ID="btnGetInfo" runat="server" OnClick="btnGetInfo_Click" Text="Lấy thông tin" />
                <asp:Button ID="btnGetStatus" runat="server" OnClick="btnGetStatus_Click" Text="Lấy trạng thái" />
                <asp:Button ID="btnGetHistory" runat="server" OnClick="btnGetHistory_Click" Text="Xem lịch sử" />
            </td>
        </tr>
        <tr>
            <td>Bước 3:</td>
            <td class="auto-style1">
                <asp:TextBox runat="server" ID="txtPartnerInvoiceID" placeholder="PartnerInvoiceID" Width="304px"></asp:TextBox></td>
            <td>
                <asp:Button ID="btnUpdateInvoiceByInvoiceGUID" runat="server" Text="Cập nhật thông tin Hóa đơn theo InvoiceGUID " OnClick="btnUpdateInvoiceByInvoiceGUID_Click" Width="298px" />
                <asp:Button ID="btnUpdateInvoiceByPartnerInvoiceID" runat="server" Text="Cập nhật thông tin Hóa đơn theo PartnerInvoiceID" OnClick="btnUpdateInvoiceByPartnerInvoiceID_Click" Width="318px" />

                <asp:Button ID="btnReplace" runat="server" OnClick="btnReplace_Click" Text="Thay thế" />
                <asp:Button ID="btnAdjustInvoice" runat="server" OnClick="btnAdjustInvoice_Click" Text="Điều chỉnh" />

            </td>
        </tr>
        <tr>
            <td>Bước 4:</td>
            <td class="auto-style1">
                <asp:TextBox runat="server" ID="txtCancelPartnerInvoiceID" placeholder="PartnerInvoiceID" Width="304px"></asp:TextBox></td>
            <td>
                 <asp:Button ID="btnCancelInvoiceByInvoiceGUID" runat="server" Text="Hủy HĐ theo InvoiceGUID" OnClick="btnCancelInvoiceByInvoiceGUID_Click" />

                <asp:Button ID="btnCancelInvoiceByPartnerInvoiceID" runat="server" Text="Hủy HĐ theo PartnerInvoiceID" OnClick="btnCancelInvoiceByPartnerInvoiceID_Click" />

            </td>
        </tr>
        <tr>
            <td>Bước 5:</td>
            <td class="auto-style1">
                <asp:TextBox runat="server" ID="txtDeleteInvoice" placeholder="PartnerInvoiceID" Width="304px"></asp:TextBox></td>
            <td>
                <asp:Button ID="btnDeleteInvoiceByInvoiceGUID" runat="server" Text="Xóa HĐ theo InvoiceGUID" OnClick="btnDeleteInvoiceByInvoiceGUID_Click" />
                <asp:Button ID="btnDeleteInvoiceByPartnerInvoiceID" runat="server" Text="Xóa HĐ theo PartnerInvoiceID" OnClick="btnDeleteInvoiceByPartnerInvoiceID_Click" />

            </td>
        </tr>
        <tr>
            <td>Bước 6:</td>
            <td class="auto-style1">
                <asp:TextBox runat="server" ID="txtGetInvoiceLink" placeholder="PartnerInvoiceID" Width="304px"></asp:TextBox></td>
            <td>
                <asp:Button ID="btnGetInvoiceLink" runat="server" Text="Lấy link tải HD in chuyển đổi" OnClick="btnGetInvoiceLink_Click" />

            </td>
        </tr>
        <tr>
            <td>Lấy thông tin BTI</td>
            <td class="auto-style1">
                <asp:TextBox runat="server" ID="txtTaxcode" placeholder="nhập mã số thuế" Width="304px"></asp:TextBox></td>
            <td>
                <asp:Button ID="btnGetUnitInforByTaxCode" runat="server" Text="Lấy thông tin từ thuế" OnClick="btnGetUnitInforByTaxCode_Click" />

            </td>
        </tr>
        <tr style="display: none">
            <td>Lấy File</td>
            <td class="auto-style1"></td>
            <td>
                <asp:Button ID="btnGetDLLContent" runat="server" Text="Lấy DLL" OnClick="btnGetDLLContent_Click" />

            </td>
        </tr>
        <tr >
            <td>Tạo tài khoản Demo</td>
            <td class="auto-style1">
                <asp:TextBox runat="server" ID="txtAccount" placeholder="Tài khoản/mật khẩu" Width="304px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnCreateAcount" runat="server" Text="Tạo tài khoản Demo trên eHD Demo" OnClick="btnCreateAcount_Click" />

            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 50px; word-break: break-all;">
                <asp:Label ID="lblNote" runat="server" Text="<b>Ghi chú</b>: Các nghiệp vụ khác vui lòng check theo <b>CommandType</b> trong <b>class CommandType</b> (file: <b>Bkav_eHoadon_Library.cs</b>)" ForeColor="Blue"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 50px; word-break: break-all;">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <style>
        ::-webkit-input-placeholder
        { /* WebKit, Blink, Edge */
            color: #999999;
            font-style: italic;
        }

        ::-moz-placeholder
        { /* Mozilla Firefox 19+ */
            color: #999999;
            opacity: 1;
            font-style: italic;
        }

        :-ms-input-placeholder
        { /* Internet Explorer 10-11 */
            color: #999999;
            font-style: italic;
        }

        ::-ms-input-placeholder
        { /* Microsoft Edge */
            color: #999999;
            font-style: italic;
        }

        .auto-style1
        {
            width: 319px;
        }
    </style>
</asp:Content>
