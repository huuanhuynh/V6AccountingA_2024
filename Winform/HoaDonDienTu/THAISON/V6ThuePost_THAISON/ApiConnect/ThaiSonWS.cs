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
        private string _baseurl = "";
        private string _serviceurl = "", _username = "", _password = "", _token_serial;
        public ThaiSonWS(string baseurl, string serviceurl, string username, string password, string token_serial)
        {
            try
            {
                _baseurl = baseurl;
                if (!_baseurl.EndsWith("/")) _baseurl += "/";
                _serviceurl = serviceurl;
                _username = username;
                _password = password;
                _token_serial = token_serial;
                _easyService = new EinvoiceService.EinvoiceService(_serviceurl);
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

        public string ImportThongTinHoaDon(HoaDonEntity invoice, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                var response = _easyService.ImportThongTinHoaDon(invoice);

                v6return.RESULT_OBJECT = response;
                v6return.RESULT_STRING = V6XmlConverter.ClassToXml(response);
                if (response.MsgError == null)
                {
                    v6return.RESULT_MESSAGE = null;
                    v6return.RESULT_ERROR_CODE = null;
                }
                else
                {
                    v6return.RESULT_MESSAGE = response.MsgError.Message;
                    v6return.RESULT_ERROR_CODE = response.MsgError.Code;
                }
                
                v6return.ID = response.MaEinvoice;
                v6return.SECRET_CODE = response.MaEinvoice;
                v6return.SO_HD = null;

                if (response.MsgError == null || response.MsgError.Code == null || response.MsgError.Code == "000")
                {
                    result += "OK:" + string.Format("MaEinvoice:{0}", response.MaEinvoice);
                }
                else
                {
                    result += "ERR:" + response.MsgError.Code + " " + response.MsgError.Description + " " + response.MsgError.EDescription + " " + response.MsgError.EMessage;
                }
            }
            catch (Exception ex)
            {
                v6return.EXCEPTION_MESSAGE = ex.Message;
                result = "ERR:EX\r\n" + ex.Message;
            }
            return result;
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
                invoice.TrangThaiDieuChinh = 1;
                var response = _easyService.XuatHoaDonDienTu(invoice);

                v6return.RESULT_OBJECT = response;
                v6return.RESULT_STRING = V6XmlConverter.ClassToXml(response);
                v6return.RESULT_MESSAGE = response.MsgError.Description;
                v6return.RESULT_ERROR_CODE = response.MsgError.Code;
                v6return.RESULT_ERROR = response.MsgError.Message;
                v6return.ID = response.MaEinvoice;
                v6return.SECRET_CODE = response.MaEinvoice;
                v6return.SO_HD = response.SoHoaDon;

                if (response.MsgError == null || response.MsgError.Code == null || response.MsgError.Code == "000")
                {
                    result += "OK:" + string.Format("SoHoaDon:{0}, MaEinvoice:{1}", response.SoHoaDon, response.MaEinvoice);
                }
                else
                {
                    result += "ERR:" + response.MsgError.Code + " " + response.MsgError.Description + " " + response.MsgError.EDescription + " " + response.MsgError.EMessage;
                }
            }
            catch (Exception ex)
            {
                v6return.EXCEPTION_MESSAGE = ex.Message;
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.ImportAndPublishInv " + result);
            return result;
        }

        /// <summary>
        /// Hàm test của ThaiSon
        /// </summary>
        public void XuatHoaDonCallMessage()
        {

            string url = _serviceurl;// "http://localhost:6666/EinvoiceService.asmx";
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
        /// <param name="invoice"></param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public string AdjustInvoice(HoaDonEntity invoice, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                invoice.TrangThaiDieuChinh = 5; // Điều chỉnh
                
                var response = _easyService.XuatHoaDonDienTu(invoice);

                v6return.RESULT_OBJECT = response;
                v6return.RESULT_STRING = V6XmlConverter.ClassToXml(response);
                v6return.RESULT_MESSAGE = response.MsgError.Description;
                v6return.RESULT_ERROR_CODE = response.MsgError.Code;
                v6return.RESULT_ERROR = response.MsgError.Message;
                v6return.ID = response.MaEinvoice;
                v6return.SECRET_CODE = response.MaEinvoice;
                v6return.SO_HD = response.SoHoaDon;

                if (response.MsgError == null || response.MsgError.Code == null || response.MsgError.Code == "000")
                {
                    result += "OK:" + string.Format("SoHoaDon:{0}, MaEinvoice:{1}", response.SoHoaDon, response.MaEinvoice);
                }
                else
                {
                    result += "ERR:" + response.MsgError.Code + " " + response.MsgError.Description + " " + response.MsgError.EDescription + " " + response.MsgError.EMessage;
                }
            }
            catch (Exception ex)
            {
                v6return.EXCEPTION_MESSAGE = ex.Message;
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.ReplaceInvoice " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public string ReplaceInvoice(HoaDonEntity invoice, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                invoice.TrangThaiDieuChinh = 3; // Thay thế
                var response = _easyService.XuatHoaDonDienTu(invoice);

                v6return.RESULT_OBJECT = response;
                v6return.RESULT_STRING = V6XmlConverter.ClassToXml(response);
                v6return.RESULT_MESSAGE = response.MsgError.Description;
                v6return.RESULT_ERROR_CODE = response.MsgError.Code;
                v6return.RESULT_ERROR = response.MsgError.Message;
                v6return.ID = response.MaEinvoice;
                v6return.SECRET_CODE = response.MaEinvoice;
                v6return.SO_HD = response.SoHoaDon;

                if (response.MsgError == null || response.MsgError.Code == null || response.MsgError.Code == "000")
                {
                    result += "OK:" + string.Format("SoHoaDon:{0}, MaEinvoice:{1}", response.SoHoaDon, response.MaEinvoice);
                }
                else
                {
                    result += "ERR:" + response.MsgError.Code + " " + response.MsgError.Description + " " + response.MsgError.EDescription + " " + response.MsgError.EMessage;
                }
            }
            catch (Exception ex)
            {
                v6return.EXCEPTION_MESSAGE = ex.Message;
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.ReplaceInvoice " + result);
            return result;
        }

        /// <summary>
        /// Hủy bỏ hóa đơn (đã ký số).
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public string CancelInvoice(HoaDonHuyEntity invoice, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                var response = _easyService.HuyHoaDon(invoice);

                v6return.RESULT_OBJECT = response;
                v6return.RESULT_STRING = V6XmlConverter.ClassToXml(response);
                v6return.RESULT_MESSAGE = response.NoiDung;
                v6return.RESULT_ERROR_CODE = response.MsgError.Code;
                v6return.RESULT_ERROR = response.MsgError.Message;
                v6return.ID = response.MaEinvoice;
                v6return.SECRET_CODE = response.UserHuy;
                v6return.SO_HD = response.SoHoaDon;

                if (response.MsgError == null || response.MsgError.Code == null || response.MsgError.Code == "000")
                {
                    result += "OK:" + string.Format("SoHoaDon:{0}, MaEinvoice:{1}, UserHuy:{2}", response.SoHoaDon, response.MaEinvoice, response.UserHuy);
                }
                else
                {
                    result += "ERR:" + response.MsgError.Code + " " + response.MsgError.Description + " " + response.MsgError.EDescription + " " + response.MsgError.EMessage;
                }
            }
            catch (Exception ex)
            {
                v6return.EXCEPTION_MESSAGE = ex.Message;
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.CancelInvoice " + result);
            return result;
        }

        /// <summary>
        /// Phát hành hóa đơn đã đưa lên
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="v6return">.</param>
        /// <returns></returns>
        public string XacThucHoaDon(XacThucHoaDonEntity invoice, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                var response = _easyService.XacThucHoaDon(invoice);

                v6return.RESULT_OBJECT = response;
                v6return.RESULT_STRING = V6XmlConverter.ClassToXml(response);
                v6return.RESULT_MESSAGE = response.MsgError.Description;
                v6return.RESULT_ERROR_CODE = response.MsgError.Code;
                v6return.RESULT_ERROR = response.MsgError.Message;
                v6return.ID = response.MaEinvoice;
                v6return.SECRET_CODE = response.MaEinvoice;
                v6return.SO_HD = response.SoHoaDon;

                if (response.MsgError == null || response.MsgError.Code == null || response.MsgError.Code == "000")
                {
                    result += "OK:" + string.Format("SoHoaDon:{0}, MaEinvoice:{1}", response.SoHoaDon, response.MaEinvoice);
                }
                else
                {
                    result += "ERR:" + response.MsgError.Code + " " + response.MsgError.Description + " " + response.MsgError.EDescription + " " + response.MsgError.EMessage;
                }
            }
            catch (Exception ex)
            {
                v6return.EXCEPTION_MESSAGE = ex.Message;
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.XacThucHoaDon " + result);
            return result;
        }

        /// <summary>
        /// Tải hoá đơn định dạng PDF
        /// </summary>
        /// <param name="maEinvoice"></param>
        /// <param name="option">0 - Bản thể hiện; 1 - Bản pdf chuyển đổi</param>
        /// <param name="savefolder"></param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public string GetInvoicePdf(string maEinvoice, int option, string savefolder, out V6Return v6return)
        {
            if (option != 0 && option != 1) option = 1;
            string path = Path.Combine(savefolder, maEinvoice + ".pdf");
            v6return = new V6Return();

            string download_link =
                string.Format(_baseurl + @"HoaDonPDF.aspx?mhd={0}&iscd={1}", maEinvoice, option);
            using (var client = new WebClient())
            {
                client.DownloadFile(download_link, path);
            }
            
            v6return.RESULT_OBJECT = download_link;
            v6return.RESULT_STRING = download_link;
            v6return.RESULT_MESSAGE = path;
            v6return.PATH = path;
            if (File.Exists(path))
            {
                return path;
            }
            else
            {
                return null;
            }
        }
        
        public string ViewInvoiceWeb(string maEinvoice, int option, string savefolder, out V6Return v6return)
        {
            if (option != 0 && option != 1) option = 1;
            //string path = Path.Combine(savefolder, maEinvoice + ".pdf");
            v6return = new V6Return();

            string download_link =
                string.Format(_baseurl + @"hoadonviewer.aspx?mhd={0}&iscd={1}", maEinvoice, option);

            System.Diagnostics.Process.Start(download_link);
            
            v6return.RESULT_OBJECT = download_link;
            v6return.RESULT_STRING = download_link;
            //v6return.RESULT_MESSAGE = path;
            //v6return.PATH = path;
            
            return download_link;
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
