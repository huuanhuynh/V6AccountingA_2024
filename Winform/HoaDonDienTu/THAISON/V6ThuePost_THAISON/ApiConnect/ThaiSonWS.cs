using System;
using System.IO;
using System.Net;
using System.Xml;
using V6ThuePost.ResponseObjects;
using V6ThuePost.VnptObjects;
using V6ThuePostThaiSonApi.EinvoiceService;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ThuePostThaiSonApi
{
    public class ThaiSonWS
    {
        private EinvoiceService.EinvoiceService _easyService = new EinvoiceService.EinvoiceService();
        private string _host = "", _username = "", _password = "", _token_serial;
        public ThaiSonWS(string host, string username, string password, string token_serial)
        {
            try
            {
                _host = host;
                _username = username;
                _password = password;
                _token_serial = token_serial;
                _easyService = new EinvoiceService.EinvoiceService(_host);
                _easyService.AuthenticationValue = new Authentication()
                {
                    userName = _username,
                    password = _password
                };
                
                return;
            }
            catch (Exception)
            {
                //EasyInvoice.Json.JsonConvert
            }
            return;
        }

        /// <summary>
        /// Phát hành hóa đơn.
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="v6return">Các giá trị trả về.</param>
        /// <returns>Thông báo phát hành hd.</returns>
        public string XuatHoaDonDienTu(HoaDonEntity invoice, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                var response = _easyService.XuatHoaDonDienTu(invoice);

                v6return.RESULT_OBJECT = response;
                v6return.RESULT_STRING = V6XmlConverter.ClassToXml(response);
                v6return.RESULT_MESSAGE = response.MsgError.Message;
                v6return.RESULT_ERROR_CODE = response.MsgError.Code;
                v6return.SECRET_CODE = response.MaEinvoice;
                v6return.SO_HD = response.SoHoaDon;

                if (response.MsgError == null || response.MsgError.Code == "0")
                {
                    result += "OK:" + string.Format("SoHoaDon:{0}, MaEinvoice:{1}", response.SoHoaDon, response.MaEinvoice);
                }
                else
                {
                    result += "ERR:" + response.MsgError.Code + " " + response.MsgError.Description + " ";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.ImportAndPublishInv " + result);
            return result;
        }

        public void XuatHoaDonCallMessage()
        {

            string url = _host;// "http://localhost:6666/EinvoiceService.asmx";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"" + url + "");
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";

            XmlDocument SOAPReqBody = new XmlDocument();
            //SOAP Body Request  
            SOAPReqBody.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:inv=""http://thaison.vn/inv"">
	<soapenv:Header>
		<inv:Authentication>
			<inv:userName>tsd</inv:userName>
			<inv:password>tsd@123</inv:password>
		</inv:Authentication>
	</soapenv:Header>
	<soapenv:Body>
		<inv:XuatHoaDonDienTu>
			<inv:hoaDonEntity>
				<inv:TenLoaiHoaDon>Hóa đơn giá trị gia tăng</inv:TenLoaiHoaDon>
				<inv:MauSo>01GTKT0/001</inv:MauSo>
				<inv:KyHieu>AA/16E</inv:KyHieu>
				<inv:SoHoaDon/>
				<inv:NgayTaoHoaDon>2016-10-03</inv:NgayTaoHoaDon>
				<inv:NgayXuatHoaDon>2016-10-03</inv:NgayXuatHoaDon>
				<inv:BenBanMaDonVi></inv:BenBanMaDonVi>
				<inv:BenBanMaSoThue>0304680974</inv:BenBanMaSoThue>
				<inv:BenBanTenDonVi>Công Ty TNHH Chuyển Phát Nhanh DHL - VNPT</inv:BenBanTenDonVi>
				<inv:BenBanDiaChi>Số 6, Đường Thăng Long, Phường 4, Quận Tân Bình, TP Hồ Chí Minh</inv:BenBanDiaChi>
				<inv:BenMuaMaDonVi/>
				<inv:BenMuaMaSoThue>0101300842</inv:BenMuaMaSoThue>
				<inv:BenMuaTenDonVi>Công ty phát triển công nghệ thái sơn</inv:BenMuaTenDonVi>
				<inv:BenMuaHoTen>vu tho tuyền</inv:BenMuaHoTen>
				<inv:BenMuaDiaChi>Số 99B tổ 70 Hồ Quỳnh, Phường Thanh Nhàn, Quận Hai Bà Trưng, Hà Nội</inv:BenMuaDiaChi>
				<inv:BenMuaDienThoai/>
				<inv:BenMuaFax/>
				<inv:BenMuaEmail/>
				<inv:BenMuaTaiKhoanNganHang>0308808576</inv:BenMuaTaiKhoanNganHang>
				<inv:BenMuaTenNganHang>Ngân hàng Quân đội</inv:BenMuaTenNganHang>
				<inv:TienThueVat>50000</inv:TienThueVat>
				<inv:TongTienHang>500000</inv:TongTienHang>
				<inv:TongTienThanhToan>550000</inv:TongTienThanhToan>
				<inv:TienChietKhau>0</inv:TienChietKhau>
				<inv:TongTienThanhToanBangChu>Năm trăm năm mươi nghìn đồng</inv:TongTienThanhToanBangChu>
				<inv:DongTienThanhToan>VND</inv:DongTienThanhToan>
				<inv:HinhThucThanhToan>01</inv:HinhThucThanhToan>
				<inv:TyGia>1</inv:TyGia>
				<inv:TamUng>0</inv:TamUng>
				<inv:TrangThaiDieuChinh>1</inv:TrangThaiDieuChinh>
				<inv:HangHoas>
					<inv:HangHoaEntity>
						<inv:SoThuTu>1</inv:SoThuTu>
						<inv:MaHang></inv:MaHang>
						<inv:TenHang>Máy tính Casio</inv:TenHang>
						<inv:DonViTinh>cái</inv:DonViTinh>
						<inv:SoLuong>1</inv:SoLuong>
						<inv:DonGia>500000</inv:DonGia>
						<inv:ThanhTien>500000</inv:ThanhTien>
						<inv:Vat>10</inv:Vat>
						<inv:TienVat>50000</inv:TienVat>
					</inv:HangHoaEntity>
				</inv:HangHoas>
			</inv:hoaDonEntity>
		</inv:XuatHoaDonDienTu>
	</soapenv:Body>
</soapenv:Envelope>");
            using (Stream stream = request.GetRequestStream())
            {
                SOAPReqBody.Save(stream);
            }
            //Geting response from request  
            using (WebResponse Serviceres = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                {
                    //reading stream  
                    var ServiceResult = rd.ReadToEnd();//giá trị trả về của webservice
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inv"></param>
        /// <param name="old_ikey"></param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public string AdjustInvoice(HoaDonEntity inv, string old_ikey, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.adjustInv " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inv"></param>
        /// <param name="old_ikey"></param>
        /// <param name="pattern"></param>
        /// <param name="serial"></param>
        /// <param name="issue">Phát hành.</param>
        /// <param name="signmode">Kiểu ký 1 client, mặc định server.</param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public string ReplaceInvoice(ReplaceInv inv, string old_ikey, string pattern, string serial, bool issue, string signmode, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
               
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.replaceInv " + result);
            return result;
        }

        /// <summary>
        /// Hủy bỏ hóa đơn (đã ký số).
        /// </summary>
        /// <param name="ikey"></param>
        /// <param name="pattern"></param>
        /// <param name="serial"></param>
        /// <returns></returns>
        public string CancelInvoice(string ikey, string pattern, string serial)
        {
            string result = null;
            try
            {
               
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.cancelInv " + result);
            return result;
        }

        /// <summary>
        /// Phát hành hóa đơn đã đưa lên
        /// </summary>
        /// <param name="ikey"></param>
        /// <param name="pattern"></param>
        /// <param name="serial"></param>
        /// <param name="signmode">Kiểu ký 1 client, mặc định server.</param>
        /// <returns></returns>
        public string IssueInvoices(string ikey, string pattern, string serial, string signmode)
        {
            string result = null;
            try
            {
                
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.IssueInvoices " + result);
            return result;
        }

        /// <summary>
        /// Tải hoá đơn định dạng PDF
        /// </summary>
        /// <param name="ikey"></param>
        /// <param name="option">0 - Bản pdf thông thường; 1 - Bản pdf chuyển đổi chứng minh nguồn gốc; 2 – Bản pdf chuyển đổi lưu trữ</param>
        /// <param name="pattern"></param>
        /// <param name="serial"></param>
        /// <param name="savefolder"></param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public string GetInvoicePdf(string ikey, int option, string pattern, string serial, string savefolder, out V6Return v6return)
        {
            string path = Path.Combine(savefolder, ikey + ".pdf");
            v6return = new V6Return();
            //var request = new Request()
            //{
            //    Ikey = ikey,
            //    Option = option,
            //    Pattern = pattern,
            //    Serial = serial
            //};

            //if (!File.Exists(path))
            //{
            //    var response = _easyService.GetInvoicePdf(request, path, _host, _username, _password);
            //    v6return.RESULT_OBJECT = response;
            //    v6return.RESULT_STRING = V6XmlConverter.ClassToXml(response);
            //    v6return.RESULT_MESSAGE = response.Message;
            //}

            v6return.PATH = path;
            return path;
        }

        /// <summary>
        /// Gạch nợ hóa đơn theo lstInvToken(01GTKT2/001;AA/13E;10)
        /// </summary>
        /// <param name="fkey_old"></param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public string ConfirmPayment(string fkey_old, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                result = "ERR:";// = new BusinessService(link_Business).confirmPayment(fkey_old, username, password);

                if (result.StartsWith("ERR:13"))
                {
                    result += "\r\nHóa đơn đã được gạch nợ.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "\r\nKhông gạch nợ được.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    result += "\r\nKhông tìm thấy hóa đơn tương ứng chuỗi đưa vào.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "\r\nTài khoản đăng nhập sai.";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.confirmPayment " + result);
            return result;
        }

        /// <summary>
        /// Gạch nợ hóa đơn theo fkey
        /// </summary>
        /// <param name="fkey_old"></param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public string ConfirmPaymentFkey(string fkey_old, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                result = "ERR:";// = new BusinessService(link_Business).confirmPaymentFkey(fkey_old, username, password);

                if (result.StartsWith("ERR:13"))
                {
                    result += "\r\nHóa đơn đã được gạch nợ.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "\r\nKhông gạch nợ được.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    result += "\r\nKhông tìm thấy hóa đơn tương ứng chuỗi đưa vào.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "\r\nTài khoản đăng nhập sai.";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.confirmPaymentFkey " + result);
            return result;
        }

        public string UnconfirmPaymentFkey(string fkey_old, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                result = "ERR:";// = newBusinessService(link_Business).UnConfirmPaymentFkey(fkey_old, username, password);

                if (result.StartsWith("ERR:13"))
                {
                    result += "\r\nHóa đơn đã được bỏ gạch nợ.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "\r\nKhông bỏ gạch nợ được.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    result += "\r\nKhông tìm thấy hóa đơn tương ứng chuỗi đưa vào.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "\r\nTài khoản đăng nhập sai.";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.UnconfirmPaymentFkey " + result);
            return result;
        }







        public string DownloadInvPDFFkeyNoPay(string fkeyA)
        {
            throw new NotImplementedException();
        }

        //public string DownloadInvFkeyNoPay(string fkeyA)
        //{
        //    //var response = _easyService.GetInvoicesByIkeys(null);
        //    //return response.Message;
        //}

        public string UploadInvAttachmentFkey(string fkeyA, string arg3)
        {
            throw new NotImplementedException();
        }
    }
}
