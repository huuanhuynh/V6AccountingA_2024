using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Newtonsoft.Json;
using SignTokenCore;
using Spy;
using Spy.SpyObjects;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6ThuePost;
using V6ThuePost.MInvoiceObject.Request;
using V6ThuePost.MInvoiceObject.Response;
using V6ThuePost.MONET_Objects.Response;
using V6ThuePost.ResponseObjects;
using V6ThuePost.SoftDreamObjects;
using V6ThuePost.ViettelObjects;
using V6ThuePost.ViettelV2Objects;
using V6ThuePost.VnptObjects;
using V6ThuePostBkavApi;
using V6ThuePostBkavApi.PostObjects;
using V6ThuePostMInvoiceApi;
using V6ThuePostMonetApi;
using V6ThuePostSoftDreamsApi;
using V6ThuePostThaiSonApi;
using V6ThuePostThaiSonApi.EinvoiceService;
using V6ThuePostViettelApi;

using V6ThuePostXmlApi;

using V6Tools;
using V6Tools.V6Convert;
using V6Tools.V6Reader;

using V6ThuePostViettelV2Api;
using V6ThuePost_VIN_Api;
using V6ThuePost_MISA_Api;
using V6ThuePost_MISA_Api.Objects;
using V6ThuePost.MISA_Objects;
using V6Tools.V6Export;
using V6ThuePost_CYBER_Api;

namespace V6ThuePostManager
{
    /// <summary>
    /// Lớp quản lý POST GET
    /// </summary>
    public static class PostManager
    {
        static DataTable _map_table;
        static DataTable ad_table;
        static DataTable am_table;
        private static string Fkey_hd_tt = null;
        /// <summary>
        /// giá trị ngày được ghi nhận khi read_data_viettel, gán lại result.
        /// </summary>
        private static string ngay_ct_viettel = null;
        /// <summary>
        /// Bảng dữ liệu xuất Excel
        /// </summary>
        static DataTable ad2_table;
        /// <summary>
        /// Bảng dữ liệu thuế
        /// </summary>
        static DataTable ad3_table;

        /// <summary>
        /// Toàn bộ dữ liệu config V6Info (chưa giải mã nếu có mã hóa).
        /// </summary>
        public static Dictionary<string, string> V6Infos = new Dictionary<string, string>();
        /// <summary>
        /// Tài khoản ws vnpt
        /// </summary>
        public static string _username = "";
        public static string _password = "";
        public static string _codetax = "";
        /// <summary>
        /// Mặc định rỗng. Viettel V2(V2 call EXE) V45(V2 ref)
        /// </summary>
        public static string _version = "";
        /// <summary>
        /// Kiểu data gủi lên.
        /// </summary>
        public static string _datamode = "";
        public static string _ma_dvcs = "";
        public static string _baseUrl = "", _site = "", _datetype = "", _createInvoiceUrl = "", _modifylink = "";
        public static string _link_Publish_vnpt_thaison = "";
        public static string _appID = "";

        #region ==== SETTING ====
        public static Dictionary<string, string> _setting = new Dictionary<string, string>();
        public static string SETTING(string KEY)
        {
            if (_setting.ContainsKey(KEY)) return _setting[KEY];
            return null;
        }
        public static bool USETAXBREAKDOWNS { get { return SETTING("USETAXBREAKDOWNS") == "1"; } }
        public static bool USEMISAOBJECT { get { return SETTING("USEMISAOBJECT") == "1"; } }
        public static string SIGNMODE { get { return SETTING("SIGNMODE"); } }
        public static bool COMACQT { get { return SETTING("COMACQT") == "1"; } }
        #endregion ==== SETTING ====


        

        /// <summary>
        /// key trong data
        /// </summary>
        public static string fkeyA;
        /// <summary>
        /// Tên file xuất ra (chưa có phần mở rộng).
        /// </summary>
        public static string exportName;
        /// <summary>
        /// key param
        /// </summary>
        private static string fkeyP;

        private static string fkeyexcel0 = "V6";
        /// <summary>
        /// Tài khoản đăng nhập vnpt
        /// </summary>
        private static string _account = null;
        private static string _accountpassword = null;
        /// <summary>
        /// Số seri của TOKEN USB
        /// </summary>
        private static string _SERIAL_CERT = null;
        private static string _token_password_title = null;
        private static string _token_password = null;

        /// <summary>
        /// Có sau khi ReadData VNPT THAISON SOFTDREAMS VIETTEL
        /// </summary>
        private static string __pattern, __serial;
        private static string pattern_field;
        private static string seri_field, _reason_field;
        private static string convert = "0";
        private static string _signmode = "0";
        private static bool _write_log = false;
        private static string _noCdata = null;

        private static Dictionary<string, ConfigLine> generalInvoiceInfoConfig = null;
        /// <summary>
        /// Bkav PXK&VCNB.
        /// </summary>
        private static Dictionary<string, ConfigLine> uiDefine = null;
        private static Dictionary<string, ConfigLine> buyerInfoConfig = null;
        private static Dictionary<string, ConfigLine> sellerInfoConfig = null;
        private static Dictionary<string, ConfigLine> metadataConfig = null;
        private static Dictionary<string, ConfigLine> paymentsConfig = null;
        private static Dictionary<string, ConfigLine> itemInfoConfig = null;
        private static Dictionary<string, ConfigLine> summarizeInfoConfig = null;
        private static Dictionary<string, ConfigLine> taxBreakdownsConfig = null;
        private static Dictionary<string, ConfigLine> customerInfoConfig = null;
        private static Dictionary<string, ConfigLine> optionUserDefinedConfig = null;

        //Excel config
        private static string template_xls = "template.xls";
        private static string firstCell = "A4";
        private static bool insertRow = false;
        private static bool drawLine = false;
        private static string[] columns = null;
        private static IDictionary<string, string> column_config = new SortedDictionary<string, string>();
        private static List<ConfigLine> parameters_config = new List<ConfigLine>();

        //private static RemoteCommand remoteCommand = null;
        public static string BkavPartnerGUID = "";
        public static string BkavPartnerToken = "";
        public static int BkavCommandTypeNew = 112;
        public static int BkavCommandTypeEdit = BkavConst._124_CreateAdjust;
        public static int BkavCommandTypeEdit2 = BkavConst._126_DieuChinhCK_SoHD_BKAV;

        public static ViettelV2WS viettel_V2WS = null;
        public static VIN_WS vin_WS = null;
        public static CYBER_WS cyber_WS = null;
        public static MISA_WS misa_WS = null;

        public static void ResetWS()
        {
            viettel_V2WS = null;
            vin_WS = null;
            cyber_WS = null;
            misa_WS = null;
        }

        /// <summary>
        /// <para>Hàm post chính, sẽ chuyển hướng theo string1-pmparams.Branch</para>
        /// <para>Kết quả hàm sai sẻ có error.</para>
        /// <para>Kết quả trả về = kết quả api trả về.</para>
        /// <para>Các tham số out để hứng giá trị lưu lại theo từng Branch.</para>
        /// </summary>
        /// <param name="paras">Các tham số đầu vào cần thiết. Tùy mode, branch. Sau khi thực hiện hàm xong paras nhận luôn Result.</param>
        /// <returns></returns>
        public static string PowerPost(PostManagerParams paras)//, out string sohoadon, out string id, out string error)
        {
            string result0 = null;
            try
            {
                _map_table = paras.DataSet.Tables[0];
                ad_table = paras.DataSet.Tables[1];
                am_table = paras.DataSet.Tables[2];
                Fkey_hd_tt = paras.Fkey_hd_tt;
                //DataRow row0 = am_table.Rows[0];
                ad2_table = paras.DataSet.Tables[3];
                ad3_table = paras.DataSet.Tables.Count > 4 ? paras.DataSet.Tables[4] : null;

                ReadConfigInfo(_map_table);

                switch (paras.Branch)
                {
                    case "1":
                        result0 = EXECUTE_VIETTEL(paras);
                        break;
                    case "Test":
                        result0 = EXECUTE_VIETTEL_V2CALL(paras);
                        break;
                    case "2":
                        if (_version == "78")
                        {
                            result0 = EXECUTE_VNPT_78(paras);
                        }
                        else
                        {
                            result0 = EXECUTE_VNPT(paras);
                        }
                        break;
                    case "3":
                        result0 = EXECUTE_BKAV(paras);
                        break;
                    case "4":
                        result0 = EXECUTE_VNPT_TOKEN(paras);
                        break;
                    case "5":
                        result0 = EXECUTE_SOFTDREAMS(paras);
                        break;
                    case "6":
                        result0 = EXECUTE_THAI_SON(paras);
                        break;
                    case "7":
                        result0 = EXECUTE_MONET(paras);
                        break;
                    case "8":
                        result0 = EXECUTE_MINVOICE(paras);
                        break;
                    case "9":
                        if (vin_WS == null) vin_WS = new VIN_WS(_baseUrl, _username, _password, _codetax);
                        result0 = EXECUTE_VIN(paras);
                        break;
                    case "10":
                        if (misa_WS == null) misa_WS = new MISA_WS(_baseUrl, _username, _password, _codetax, _appID, COMACQT);
                        result0 = EXECUTE_MISA(paras);
                        break;
                    case "11":
                        if (cyber_WS == null) cyber_WS = new CYBER_WS(_baseUrl, _username, _password, _codetax);
                        result0 = EXECUTE_CYBER(paras);
                        break;
                    default:
                        paras.Result = new PM_Result();
                        paras.Result.V6ReturnValues = new V6Return();
                        paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = V6Text.NotSupported + paras.Branch;
                        break;
                }
            }
            catch (Exception ex)
            {
                paras.Result = new PM_Result();
                paras.Result.V6ReturnValues = new V6Return();
                paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = "EX: " + ex.Message;
                V6ControlFormHelper.WriteExLog("RequestManager.PowerPost", ex);
            }
            
            //sohoadon = sohoadon0;
            //id = id0;
            //error = error0;
            return result0;
        }

        public static string GetConfigBaseLink(DataTable mapTable)
        {
            _site = "";
            _map_table = mapTable;
            ReadConfigInfo(mapTable);
            return _site;
        }

        /// <summary>
        /// Hàm kiểm tra kết nối. Nếu thành công trả về rỗng hoặc null.
        /// </summary>
        /// <param name="paras"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string PowerCheckConnection(PostManagerParams paras, out string exception)
        {
            string result = null;
            exception = null;
            paras.Result = new PM_Result();
            try
            {
                _map_table = paras.DataSet.Tables[0];
                
                //ad_table = pmparams.DataSet.Tables[1];
                //am_table = pmparams.DataSet.Tables[2];
                //DataRow row0 = am_table.Rows[0];
                //ad2_table = pmparams.DataSet.Tables[3];

                ReadConfigInfo(_map_table);

                switch (paras.Branch)
                {
                    case "1":
                        if (_version == "V2")
                        {
                            var process = new Process
                            {
                                StartInfo = new ProcessStartInfo
                                {
                                    WorkingDirectory = "ViettelV2",
                                    FileName = "ViettelV2\\V6ThuePostViettelV2.exe",
                                    UseShellExecute = false,
                                    RedirectStandardOutput = true,
                                    CreateNoWindow = true
                                }
                            };
                            
                            process.StartInfo.Arguments = "MTEST_JSON V6ThuePost.xml";
                            process.Start();
                            string process_result = process.StandardOutput.ReadToEnd();
                            process.WaitForExit();
                            // Phân tích Result tại đây.
                            paras.Result.V6ReturnValues = GetV6ReturnFromCallExe(process_result);
                            if (paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE != null && paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE.Contains("JSON_PARSE_ERROR"))
                            {
                                return null;
                            }
                            if (paras.Result.V6ReturnValues.RESULT_STRING.Contains("\"errorCode\":\"TEMPLATE_NOT_FOUND\""))
                            {
                                return null;
                            }

                            return "Lỗi kết nối." + paras.Result.V6ReturnValues.RESULT_STRING;
                        }
                        else if (_version == "V45" || _version == "V45I")
                        {
                            if (viettel_V2WS == null) viettel_V2WS = new ViettelV2WS(_baseUrl, _username, _password, _codetax);
                            result = viettel_V2WS.CheckConnection();
                        }
                        else
                        {
                            ViettelWS viettel_ws = new ViettelWS(_baseUrl, _username, _password, _codetax);
                            result = viettel_ws.CheckConnection();
                        }
                        break;
                    case "2":
                    case "4":
                        if (_version == "78")
                        {
                            Vnpt78WS vnpt78WS = CreateVnpt78WS();
                            result = vnpt78WS.CheckConnection();
                        }
                        else
                        {
                            VnptWS vnptWS = CreateVnptWS();
                            result = vnptWS.CheckConnection();
                        }
                        break;
                    case "3":
                        BkavWS bkavWS = new BkavWS(_baseUrl, BkavPartnerGUID, BkavPartnerToken);
                        string testjson = "[{\"Invoice\":{\"InvoiceTypeID\":1,\"InvoiceDate\":\"2022-02-11T00:00:00\",\"BuyerName\":\"Người mua hàng XM7- mẫu 7 -USD\",\"BuyerTaxCode\":\"\",\"BuyerUnitName\":\"Mai Hữu Tuân\",\"BuyerAddress\":\"Lê Văn Huân\",\"BuyerBankAccount\":\"0132654979664111-nh tmcp vn\",\"PayMethodID\":2,\"ReceiveTypeID\":1,\"ReceiverEmail\":\"tuanmh@gmail.com\",\"ReceiverMobile\":\"\",\"ReceiverAddress\":\"Lê Văn Huân\",\"ReceiverName\":\"Mai Hữu Tuân\",\"Note\":\"\",\"UserDefine\":\"{\\\"DonViBanHang\\\":\\\"CÔNG TY CỔ PHẦN XÂY LẮP AN GIANG\\\",\\\"MaSoThue\\\":\\\"1600220016\\\",\\\"DiaChi\\\":\\\"Số 316/1A Trần Hưng Đạo, Phường Mỹ Long, Thành phố Long Xuyên , An Giang\\\",\\\"SoDienThoai\\\":\\\"02963841100\\\",\\\"Fax\\\":\\\"\\\",\\\"SoTaiKhoan\\\":\\\"\\\"}\",\"BillCode\":\"0000004\",\"CurrencyID\":\"USD\",\"ExchangeRate\":23000.00,\"InvoiceGUID\":\"615d1f41-dc8a-ec11-8109-6c2b59a11324\",\"InvoiceStatusID\":1,\"InvoiceForm\":\"0\",\"InvoiceSerial\":\"V6V6V6\",\"InvoiceNo\":4,\"InvoiceCode\":\"\",\"SignedDate\":\"2022-02-11T00:00:00\",\"TypeCreateInvoice\":0},\"ListInvoiceDetailsWS\":[{\"TaxRate\":10,\"ItemName\":\"Kệ pulmoll 1 2 3 4 5 6 7 8 9 0 1 2 3 4\",\"UnitName\":\"Cái\",\"Qty\":98.000,\"Price\":22.7273,\"Amount\":2227.28,\"TaxRateID\":3,\"TaxAmount\":222.73,\"IsDiscount\":false,\"IsIncrease\":false,\"DiscountRate\":0.0000,\"DiscountAmount\":0.00}],\"ListInvoiceAttachFileWS\":[],\"PartnerInvoiceID\":\"0\",\"PartnerInvoiceStringID\":\"N001624951SOA\"}]";
                        result = bkavWS.POST(testjson, BkavCommandTypeNew, out paras.Result.V6ReturnValues);
                        if (result == "ERR:Tài khoản chưa phát hành mẫu Hóa đơn hoặc dải số hoặc dải số đã hết")
                        {
                            result = null;
                        }
                        else
                        {
                            //result = "Kết nối lỗi.";
                        }

                        break;
                    case "5":
                        SoftDreamsWS softDreamsWs = new SoftDreamsWS(_baseUrl, _username, _password, _SERIAL_CERT);
                        result = softDreamsWs.GetInvoicePdf(paras.Fkey_hd, paras.Mode == "1" ? 0 : paras.Mode == "2" ? 1 : 2, paras.Pattern, paras.Serial, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        if (paras.Result.V6ReturnValues.RESULT_MESSAGE != null && paras.Result.V6ReturnValues.RESULT_MESSAGE.Contains("Có lỗi xả ra: {\"Status\":4,\"Message\":\"Ikey không được bỏ trống\""))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Kết nối lỗi.";
                        }
                        break;
                    case "6":
                        _map_table = paras.DataSet.Tables[0];
                        ad_table = paras.DataSet.Tables[1];
                        am_table = paras.DataSet.Tables[2];
                        Fkey_hd_tt = paras.Fkey_hd_tt;
                        DataRow row0 = am_table.Rows[0];
                        ad2_table = paras.DataSet.Tables[3];
                        if (paras.DataSet.Tables.Count > 4)
                        {
                            ad3_table = paras.DataSet.Tables[4];
                        }
                        else
                        {
                            ad3_table = null;
                        }

                        ReadConfigInfo(_map_table);

                        ThaiSonWS thaiSonWS = new ThaiSonWS(_baseUrl, _link_Publish_vnpt_thaison, _username, _password, _SERIAL_CERT);
                        var hoadon_entity = (HoaDonEntity)ReadData_ThaiSon(paras.Mode.Substring(0, 1));
                        hoadon_entity.SoHoaDon = "TEST";
                        hoadon_entity.KyHieu = "TEST";
                        hoadon_entity.MauSo = "TEST";
                        //result = thaiSonWS.ImportThongTinHoaDon(hoadon_entity, out paras.Result.V6ReturnValues);
                        result = thaiSonWS.XuatHoaDonDienTu(hoadon_entity, out paras.Result.V6ReturnValues);
                        if (result.Contains("Hóa đơn mang ký hiệu TEST,") || paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE.Contains("Nhập sai thông tin"))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Kết nối lỗi.";
                        }
                        break;
                    case "7":
                        MONET_WS monetWS = new MONET_WS(_baseUrl, _username, _password, _codetax);
                        result = monetWS.CheckConnection(_createInvoiceUrl);
                        break;
                    case "8":
                        MInvoiceWS mInvoiceWs = new MInvoiceWS(_baseUrl, _username, _password, _ma_dvcs, _codetax, _version);
                        result = mInvoiceWs.CheckConnection(out paras.Result.V6ReturnValues);
                        break;
                    case "9":
                        if (vin_WS == null) vin_WS = new VIN_WS(_baseUrl, _username, _password, _codetax);
                        result = vin_WS.CheckConnection();
                        break;
                    case "10":
                        if (misa_WS == null) misa_WS = new MISA_WS(_baseUrl, _username, _password, _codetax, _appID, COMACQT);
                        V6Return v6Return = null;
                        result = misa_WS.CheckConnection(out v6Return);
                        break;
                    case "11":
                        if (cyber_WS == null) cyber_WS = new CYBER_WS(_baseUrl, _username, _password, _codetax);
                        result = cyber_WS.CheckConnection();
                        break;
                    default:
                        paras.Result.ResultErrorMessage = V6Text.NotSupported + paras.Branch;
                        break;
                }
            }
            catch (Exception ex)
            {
                result += "Ex: " + ex.Message;
                paras.Result.ResultErrorMessage = ex.Message;
                exception = ex.Message;
                V6ControlFormHelper.WriteExLog("PostManager.PowerCheckConnection", ex);
            }
            return result;
        }

        static V6Return GetV6ReturnFromCallExe(string call_result)
        {
            var v6return = JsonConvert.DeserializeObject<V6Return>(call_result);
            if (!string.IsNullOrEmpty(v6return.RESULT_ERROR_MESSAGE))
            {
                v6return.RESULT_ERROR_MESSAGE = ChuyenMaTiengViet.TCVNtoUNICODE(v6return.RESULT_ERROR_MESSAGE);
            }
            if (!string.IsNullOrEmpty(v6return.RESULT_MESSAGE))
            {
                v6return.RESULT_MESSAGE = ChuyenMaTiengViet.TCVNtoUNICODE(v6return.RESULT_MESSAGE);
            }
            return v6return;
        }
        
        /// <summary>
        /// <para>Tham số cần thiết: DataSet[_map_table][ad_table][am_table], Branch[1viettel][2vnpt]</para>
        /// </summary>
        /// <param name="paras"></param>
        /// <param name="error">Lỗi trả về.</param>
        /// <returns>Trả về đường dẫn file pdf.</returns>
        public static string PowerDownloadPDF(PostManagerParams paras, out string error)
        {
            string result = null;
            error = null;
            paras.Result = new PM_Result();
            try
            {
                _map_table = paras.DataSet.Tables[0];
                //ad_table = pmparams.DataSet.Tables[1];
                //am_table = pmparams.DataSet.Tables[2];
                //DataRow row0 = am_table.Rows[0];
                //ad2_table = pmparams.DataSet.Tables[3];

                ReadConfigInfo(_map_table);

                switch (paras.Branch)
                {
                    case "1":
                        result = ViettelDownloadInvoicePDF(paras);
                        break;
                    case "2": case "4":
                        int option = ObjectAndString.ObjectToInt(paras.Mode);
                        if (_version == "78")
                        {
                            Vnpt78WS vnpt78WS = CreateVnpt78WS();
                            result = vnpt78WS.DownloadInvPDFFkey(paras.Fkey_hd, option, V6Setting.V6SoftLocalAppData_Directory, V6Setting.WriteExtraLog, out paras.Result.V6ReturnValues);
                        }
                        else
                        {
                            VnptWS vnptWS = CreateVnptWS();
                            result = vnptWS.DownloadInvPDFFkey(paras.Fkey_hd, option, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        }
                        break;
                    case "3":
                        BkavWS bkav_ws = new BkavWS(_baseUrl, BkavPartnerGUID, BkavPartnerToken);
                        result = bkav_ws.DownloadInvoicePDF(paras.Fkey_hd, V6Setting.V6SoftLocalAppData_Directory, paras.Mode, out paras.Result.V6ReturnValues);
                        break;
                    case "5":
                        SoftDreamsWS softDreamsWs = new SoftDreamsWS(_baseUrl, _username, _password, _SERIAL_CERT);
                        result = softDreamsWs.GetInvoicePdf(paras.Fkey_hd, paras.Mode == "1" ? 0 : paras.Mode == "2" ? 1 : 2, paras.Pattern, paras.Serial, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        break;
                    case "6":
                        ThaiSonWS thaiSonWS = new ThaiSonWS(_baseUrl, _link_Publish_vnpt_thaison, _username, _password, _SERIAL_CERT);
                        result = thaiSonWS.GetInvoicePdf(paras.V6PartnerID, paras.Mode == "1" ? 0 : 1, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        break;
                    case "7":
                        MONET_WS monetWS = new MONET_WS(_baseUrl, _username, _password, _codetax);
                        if (paras.Mode == "1") result = monetWS.DownloadInvoicePDF(paras.V6PartnerID, paras.Pattern, V6Setting.V6SoftLocalAppData_Directory);
                        else result = monetWS.DownloadInvoicePDFexchange(paras.V6PartnerID, paras.Pattern, "", V6Setting.V6SoftLocalAppData_Directory);
                        break;
                    case "8":
                        MInvoiceWS mInvoiceWs = new MInvoiceWS(_baseUrl, _username, _password, _ma_dvcs, _codetax, _version);
                        if (paras.Mode == "1") result = mInvoiceWs.DownloadInvoicePDF(paras.V6PartnerID, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        else result = mInvoiceWs.DownloadInvoicePDFexchange(paras.V6PartnerID, paras.Pattern, "", V6Setting.V6SoftLocalAppData_Directory);
                        break;
                    case "9":
                        if (vin_WS == null) vin_WS = new VIN_WS(_baseUrl, _username, _password, _codetax);
                        if (paras.Mode == "2") result = vin_WS.CHUYEN_DOI_HOA_DON_PDF(_codetax, paras.Partner_infor_dic["SECRET_CODE"], paras.Fkey_hd, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        else result = vin_WS.TAI_HOA_DON_PDF(_codetax, paras.Partner_infor_dic["SECRET_CODE"], paras.Fkey_hd, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        break;
                        // 10 MISA
                    case "11":
                        if (cyber_WS == null) cyber_WS = new CYBER_WS(_baseUrl, _username, _password, _codetax);
                        if (paras.Mode == "2") result = cyber_WS.CHUYEN_DOI_HOA_DON_PDF(_codetax, paras.Partner_infor_dic["SECRET_CODE"], paras.Fkey_hd, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        else result = cyber_WS.TAI_HOA_DON_PDF(_codetax, paras.Partner_infor_dic["SECRET_CODE"], paras.Fkey_hd, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        break;
                    default:
                        paras.Result.ResultErrorMessage = V6Text.NotSupported + paras.Branch;
                        break;
                }
            }
            catch (Exception ex)
            {
                paras.Result.ResultErrorMessage = ex.Message;
                error = ex.Message;
                V6ControlFormHelper.WriteExLog("PostManager.PowerDownloadPDF", ex);
            }

            error = paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE;
            return result;
        }
        
        /// <summary>
        /// Tải thông tin hóa đơn, trả về số hóa đơn.
        /// <para>Viettel V45 V45I cần paras.Fkey_hd</para>
        /// </summary>
        /// <param name="paras"></param>
        /// <param name="error"></param>
        /// <returns></returns>        
        public static string PowerDownloadInfo(PostManagerParams paras, out string error)
        {
            string result = null;
            error = null;
            paras.Result = new PM_Result();
            try
            {
                _map_table = paras.DataSet.Tables[0];
                //ad_table = pmparams.DataSet.Tables[1];
                //am_table = pmparams.DataSet.Tables[2];
                //DataRow row0 = am_table.Rows[0];
                //ad2_table = pmparams.DataSet.Tables[3];

                ReadConfigInfo(_map_table);

                switch (paras.Branch)
                {
                    case "1":
                        result = ViettelDownloadInvoiceInfo(paras);
                        break;
                    case "2": case "4":
                        int option = ObjectAndString.ObjectToInt(paras.Mode);
                        VnptWS vnptWS = new VnptWS(_baseUrl, _account, _accountpassword, _username, _password);
                        result = vnptWS.DownloadInvPDFFkey(paras.Fkey_hd, option,V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        break;
                    case "3":
                        BkavWS bkav_ws = new BkavWS(_baseUrl, BkavPartnerGUID, BkavPartnerToken);
                        result = bkav_ws.DownloadInvoicePDF(paras.Fkey_hd, V6Setting.V6SoftLocalAppData_Directory, paras.Mode, out paras.Result.V6ReturnValues);
                        break;
                    case "5":
                        SoftDreamsWS softDreamsWs = new SoftDreamsWS(_baseUrl, _username, _password, _SERIAL_CERT);
                        result = softDreamsWs.GetInvoicePdf(paras.Fkey_hd, paras.Mode == "1" ? 0 : paras.Mode == "2" ? 1 : 2, paras.Pattern, paras.Serial, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        break;
                    case "6":
                        ThaiSonWS thaiSonWS = new ThaiSonWS(_baseUrl, _link_Publish_vnpt_thaison, _username, _password, _SERIAL_CERT);
                        result = thaiSonWS.GetInvoicePdf(paras.V6PartnerID, paras.Mode == "1" ? 0 : 1, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        break;
                    case "7":
                        MONET_WS monetWS = new MONET_WS(_baseUrl, _username, _password, _codetax);
                        if (paras.Mode == "1") result = monetWS.DownloadInvoicePDF(paras.V6PartnerID, paras.Pattern, V6Setting.V6SoftLocalAppData_Directory);
                        else result = monetWS.DownloadInvoicePDFexchange(paras.V6PartnerID, paras.Pattern, "", V6Setting.V6SoftLocalAppData_Directory);
                        break;
                    case "8":
                        MInvoiceWS mInvoiceWs = new MInvoiceWS(_baseUrl, _username, _password, _ma_dvcs, _codetax, _version);
                        if (paras.Mode == "1") result = mInvoiceWs.DownloadInvoicePDF(paras.V6PartnerID, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        else result = mInvoiceWs.DownloadInvoicePDFexchange(paras.V6PartnerID, paras.Pattern, "", V6Setting.V6SoftLocalAppData_Directory);
                        break;
                    default:
                        paras.Result.ResultErrorMessage = V6Text.NotSupported + paras.Branch;
                        break;
                }
            }
            catch (Exception ex)
            {
                paras.Result.ResultErrorMessage = ex.Message;
                error = ex.Message;
                V6ControlFormHelper.WriteExLog("PostManager.PowerDownloadInfo", ex);
            }

            error = paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE;
            return result;
        }

        #region ==== BKAV ====

        private static string EXECUTE_BKAV(PostManagerParams paras)
        {
            string result = "";
            paras.Result = new PM_Result();
            try
            {
                BkavWS bkavWS = new BkavWS(_baseUrl, BkavPartnerGUID, BkavPartnerToken);
                
                string jsonBody;

                if (paras.Mode == "TestView")
                {
                    var xml = ReadData_Bkav("M");
                    result = xml;
                    paras.Result.ResultString = xml;
                }
                else if (paras.Mode == "E_G1") // Gạch nợ.
                {
                    paras.Result.ResultErrorMessage = V6Text.NotSupported;
                }
                else if (paras.Mode == "E_H1")
                {
                    string reason = "" + paras.AM_data[_reason_field];
                    result = bkavWS.CancelInvoice(BkavConst._202_CancelInvoiceByPartnerInvoiceID, paras.Fkey_hd, reason, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "E_H2") // Hủy và ký hủy
                {
                    string reason = "" + paras.AM_data[_reason_field];
                    result = bkavWS.CancelInvoice(BkavConst._202_CancelInvoiceByPartnerInvoiceID, paras.Fkey_hd, reason, out paras.Result.V6ReturnValues);
                    if (!result.StartsWith("ERR") && V6Infos.ContainsKey("BKAVSIGN") &&  V6Infos["BKAVSIGN"] == "1")
                    {
                        result = result + "\r\n" + bkavWS.SignInvoice(paras.V6PartnerID, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode == "E_S1")
                {
                    jsonBody = ReadData_Bkav("S");
                    int commandTType = BkavCommandTypeEdit;
                    if (commandTType != BkavConst._121_CreateAdjust && commandTType != BkavConst._124_CreateAdjust
                        && commandTType != BkavConst._122_DieuChinhCK_KoSoHD && commandTType != BkavConst._126_DieuChinhCK_SoHD_BKAV) commandTType = BkavConst._124_CreateAdjust;
                    result = bkavWS.POST(jsonBody, commandTType, out paras.Result.V6ReturnValues);
                    //if (!result.StartsWith("ERR") && V6Infos.ContainsKey("BKAVSIGN") && V6Infos["BKAVSIGN"] == "1")
                    //{
                    //    result = result + "\r\n" + bkavWS.SignInvoice(paras.V6PartnerID, out paras.Result.V6ReturnValues);
                    //}
                }
                else if (paras.Mode == "E_S2")
                {
                    jsonBody = ReadData_Bkav("S");
                    int commandTType = BkavCommandTypeEdit2;
                    if (commandTType != BkavConst._122_DieuChinhCK_KoSoHD && commandTType != BkavConst._126_DieuChinhCK_SoHD_BKAV) commandTType = BkavConst._124_CreateAdjust;
                    result = bkavWS.POST(jsonBody, commandTType, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "E_T1")
                {
                    jsonBody = ReadData_Bkav("T");
                    result = bkavWS.POST(jsonBody, BkavConst._123_CreateReplace, out paras.Result.V6ReturnValues);
                    if ((string.IsNullOrEmpty(paras.Key_Down) || paras.Key_Down == "F9")
                        && V6Infos.ContainsKey("BKAVSIGN") && V6Infos["BKAVSIGN"] == "1")
                    {
                        V6Return v6return2;
                        result = result + "\r\n" + bkavWS.SignInvoice(paras.Result.V6ReturnValues.ID, out v6return2);
                    }
                }
                else if (paras.Mode == "M")
                {
                    jsonBody = ReadData_Bkav("M");
                    int commandType = BkavCommandTypeNew;
                    if (paras.Key_Down == "F4") commandType = BkavConst._101_CreateEmpty;
                    else if (paras.Key_Down == "F6") commandType = BkavConst._200_Update;

                    result = bkavWS.POST(jsonBody, commandType, out paras.Result.V6ReturnValues);
                    if ((string.IsNullOrEmpty(paras.Key_Down) || paras.Key_Down == "F9")
                        && V6Infos.ContainsKey("BKAVSIGN") &&  V6Infos["BKAVSIGN"] == "1")
                    {
                        V6Return v6return2;
                        result = result + "\r\n" + bkavWS.SignInvoice(paras.Result.V6ReturnValues.ID, out v6return2);
                    }
                }
                else if (paras.Mode == "S")
                {
                    jsonBody = ReadData_Bkav("S");
                    result = bkavWS.POST(jsonBody, BkavConst._121_CreateAdjust, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("T"))
                {
                    jsonBody = ReadData_Bkav("T");
                    result = bkavWS.POST(jsonBody, BkavConst._123_CreateReplace, out paras.Result.V6ReturnValues);
                }
                else
                {
                    result = "ERR: Mode " + paras.Mode + " " + V6Text.NotSupported;
                }

                if (result.StartsWith("ERR"))
                {
                    if (string.IsNullOrEmpty(paras.Result.ResultErrorMessage)) paras.Result.ResultErrorMessage = result;
                }
                else
                {
                    //if(so_hd != 0) sohoadon = so_hd.ToString();
                }
            }
            catch (Exception ex)
            {
                paras.Result.ResultErrorMessage = ex.Message;
                result += "ERR:EX\r\n" + ex.Message;
            }

            return result;
        }


        /// <summary>
        /// Đọc dữ liệu cho Bkav
        /// </summary>
        /// <param name="mode">M or S or T</param>
        /// <returns></returns>
        public static string ReadData_Bkav(string mode)
        {
            string result = "";
            try
            {
                var postObject = new PostObjectBkav();

                
                
                DataRow row0 = am_table.Rows[0];
                fkeyA = "" + row0["FKEY_HD"];
                
                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    if (item.Key == "OriginalInvoiceIdentify")
                    {
                        if (mode == "T") // khi thay thế, cho số hóa đơn = 0 InvoiceNo
                        {
                            postObject.Invoice[item.Key] = Fkey_hd_tt;// GetValue(row0, item.Value);
                            //postObject.Invoice["InvoiceStatusID"] = "1";
                            postObject.Invoice["InvoiceCode"] = null;
                            postObject.Invoice["InvoiceNo"] = 0;
                            //postObject.Invoice["InvoiceForm"] = "";
                            //postObject.Invoice["InvoiceSerial"] = "";

                            //string OriginalInvoiceIdentify = string.Format("[{0}]_[{1}]_[{2}]",     //  "[01GTKT0/003]_[AA/17E]_[0000105]";
                            //    postObject.Invoice["InvoiceForm"],
                            //    postObject.Invoice["InvoiceSerial"],
                            //    postObject.Invoice["InvoiceNo"]);

                            //postObject.Invoice["OriginalInvoiceIdentify"] = OriginalInvoiceIdentify;
                        }
                        else if (mode == "S" || mode == "E_S1")
                        {
                            //?????!!!!!
                            postObject.Invoice[item.Key] = Fkey_hd_tt;// GetValue(row0, item.Value);
                        }
                    }
                    else
                    {
                        postObject.Invoice[item.Key] = item.Value.GetValue(row0);
                    }
                }

                if(uiDefine != null && uiDefine.Count > 0)
                {
                    Dictionary<string, object> uidefineDic = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in uiDefine)
                    {
                        uidefineDic[item.Key] = GetValue(row0, item.Value);
                    }

                    postObject.Invoice["UIDefine"] = V6Tools.V6Convert.V6JsonConverter.ObjectToJson(uidefineDic, "BKAV");
                }              


                

                //foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                //{
                //    postObject.buyerInfo[item.Key] = GetValue(row0, item.Value);
                //}

                //foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                //{
                //    postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
                //}

                //Dictionary<string, object> payment = new Dictionary<string, object>();
                //foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                //{
                //    payment[item.Key] = GetValue(row0, item.Value);
                //}
                //postObject.payments.Add(payment);//One payment only!

                foreach (DataRow row in ad_table.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;

                    string NOGEN_F = "";
                    if (row.Table.Columns.Contains("NOGEN_F")) NOGEN_F = ";" + row["NOGEN_F"].ToString().Trim().ToUpper() + ";";
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        var cell = GetValue(row, item.Value);
                        if (item.Value.NoGen && NOGEN_F.Contains(";" + item.Value.FieldV6.ToUpper() + ";"))
                        {
                            // NOGEN
                        }
                        else
                        {
                            rowData[item.Key] = cell;
                        }
                    }
                    postObject.ListInvoiceDetailsWS.Add(rowData);
                }

                //foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                //{
                //    postObject.summarizeInfo[item.Key] = GetValue(row0, item.Value);
                //}
                if (summarizeInfoConfig.ContainsKey("ListInvoiceAttachFileWS"))
                {
                    postObject.ListInvoiceAttachFileWS = new List<string>()
                    {
                        GetValue(row0, summarizeInfoConfig["ListInvoiceAttachFileWS"]).ToString()
                    };
                }
                if (summarizeInfoConfig.ContainsKey("PartnerInvoiceID"))
                {
                    postObject.PartnerInvoiceID =
                        ObjectAndString.ObjectToString(GetValue(row0, summarizeInfoConfig["PartnerInvoiceID"]), "ddMMyyyyHHmmss");
                    if (postObject.PartnerInvoiceID.Length < 14 && ObjectAndString.ObjectToInt(postObject.PartnerInvoiceID) != 0)
                    {
                        postObject.PartnerInvoiceID = ("00000000000000" + postObject.PartnerInvoiceID).Right("ddMMyyyyHHmmss".Length);
                    }
                }
                if (summarizeInfoConfig.ContainsKey("PartnerInvoiceStringID"))
                {
                    postObject.PartnerInvoiceStringID =
                        GetValue(row0, summarizeInfoConfig["PartnerInvoiceStringID"]).ToString();
                }

                result = "[" + postObject.ToJson() + "]";
                
                if (_write_log)
                {
                    string stt_rec = row0["STT_REC"].ToString();
                    string file = Path.Combine(Application.StartupPath, stt_rec + ".json");
                    File.WriteAllText(file, result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        
        

        #endregion bkav

        #region ==== VNPT ====

        /// <summary>
        /// Hàm chỉ áp dụng được sau khi đã chạy và nhận cấu hình.
        /// </summary>
        /// <returns></returns>
        private static VnptWS CreateVnptWS()
        {
            if (V6Infos == null || V6Infos.Count == 0)
            {
                throw new Exception(V6Text.NoDefine);
            }
            VnptWS vnptWS = new VnptWS(_baseUrl, _account, _accountpassword, _username, _password);
            return vnptWS;
        }

        private static Vnpt78WS CreateVnpt78WS()
        {
            if (V6Infos == null || V6Infos.Count == 0)
            {
                throw new Exception(V6Text.NoDefine);
            }
            Vnpt78WS vnptWS = new Vnpt78WS(_baseUrl, _account, _accountpassword, _username, _password);
            return vnptWS;
        }

        private static string EXECUTE_VNPT(PostManagerParams paras)
        {
            string result = "";
            paras.Result = new PM_Result();

            try
            {
                var row0 = am_table.Rows[0];
                VnptWS vnptWS = new VnptWS(_baseUrl, _account, _accountpassword, _username, _password);
                if (paras.Mode == "TestView")
                {
                    var xml = ReadData_Vnpt();
                    result = xml;
                    paras.Result.ResultString = xml;
                }
                else if (paras.Mode.StartsWith("E_"))
                {
                    if (paras.Mode == "E_G1") // Gạch nợ theo fkey
                    {
                        if (string.IsNullOrEmpty(paras.Fkey_hd))
                        {
                            paras.Result.ResultErrorMessage = "Không có Fkey_hd truyền vào.";
                        }
                        else
                        {
                            result = vnptWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                        }
                    }
                    else if (paras.Mode == "E_G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    {
                        result = vnptWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_G3") // Hủy gạch nợ theo fkey
                    {
                        result = vnptWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_H1")
                    {
                        result = vnptWS.cancelInv(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_S1")
                    {
                        var xml = ReadDataS_Vnpt();
                        result = vnptWS.adjustInv(xml, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_T1")
                    {
                        var xml = ReadDataXmlT();
                        result = vnptWS.replaceInv(xml, paras.Fkey_hd_tt, out paras.Result.V6ReturnValues);
                    }
                }
                else  if (paras.Mode.StartsWith("M") || paras.Mode == "") // MSHDT//Mới Sửa Hủy ĐiềuChỉnh(S) ThayThế
                {
                    try // Update danh mục khách hàng.
                    {
                        if (customerInfoConfig != null && customerInfoConfig.Count > 0)
                        {
                            DoUpdateCus(vnptWS, am_table);
                        }
                    }
                    catch (Exception)
                    {
                        // Bỏ qua lỗi.
                    }
                    var xml = ReadData_Vnpt();
                    
                    if (string.IsNullOrEmpty(_SERIAL_CERT))
                    {
                        result = vnptWS.ImportAndPublishInv(xml, __pattern, __serial, out paras.Result.V6ReturnValues);
                    }
                    else
                    {
                        StartAutoInputTokenPassword();
                        result = PublishInvWithToken_Dll(xml, __pattern, __serial, out paras.Result.V6ReturnValues);
                    }
                    

                    //string invXml = DownloadInvFkeyNoPay(fkeyA);
                    //paras.Result.InvoiceNo = GetSoHoaDon_VNPT(invXml);
                    
                    string filePath = Path.Combine(paras.Dir, paras.FileName);
                    if (filePath.Length > 0 && result.StartsWith("OK"))
                    {
                        if (paras.Mode.EndsWith("1"))//Gửi file excel có sẵn
                        {
                            if (File.Exists(filePath))
                            {
                                result += vnptWS.UploadInvAttachmentFkey(fkeyA, filePath);
                            }
                            else
                            {
                                result += "Không tồn tại " + filePath;
                            }
                        }
                        else if (paras.Mode.EndsWith("2")) // Tự xuất excel rồi gửi.
                        {
                            string export_file;
                            bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                            if (export_ok && File.Exists(export_file))
                            {
                                result += vnptWS.UploadInvAttachmentFkey(fkeyA, export_file);
                            }
                        }
                        else if (paras.Mode.EndsWith("3")) // Tự xuất pdf rồi gửi
                        {
                            string export_file;
                            if (string.IsNullOrEmpty(exportName))
                            {
                                var save = new SaveFileDialog
                                {
                                    Filter = "Pdf files (*.pdf)|*.pdf",
                                    Title = "Xuất pdf để gửi đi.",
                                };
                                if (save.ShowDialog() == DialogResult.OK)
                                {
                                    export_file = save.FileName;
                                }
                                else
                                {
                                    goto End;
                                }
                            }
                            else
                            {
                                export_file = exportName + ".pdf";
                            }
                            
                            string rptFile = Path.Combine(paras.Dir, paras.RptFileFull);
                            ReportDocument rpt = new ReportDocument();
                            rpt.Load(rptFile);
                            DataSet ds = new DataSet();
                            DataTable data1 = ad_table.Copy();
                            data1.TableName = "DataTable1";
                            DataTable data2 = am_table.Copy();
                            data2.TableName = "DataTable2";
                            ds.Tables.Add(data1);
                            ds.Tables.Add(data2);
                            string tien_bang_chu = MoneyToWords(ObjectAndString.ObjectToDecimal(row0["T_TT"]), "V", "VND");
                            rpt.SetDataSource(ds);
                            rpt.SetParameterValue("SoTienVietBangChu", tien_bang_chu);
                            bool export_ok = ExportRptToPdf(null, rpt, export_file);
                            if (export_ok)
                            {
                                result += "\r\nExport ok.";
                            }
                            else
                            {
                                result += "\r\nExport fail.";
                            }

                            if (export_ok && File.Exists(export_file))
                            {
                                result += vnptWS.UploadInvAttachmentFkey(fkeyA, export_file);
                            }
                        }
                    }
                    else if (!result.StartsWith("OK"))       // Hoặc đã có trên hệ thống HDDT ERR:0
                    {
                        V6Return v6return;
                        vnptWS.DownloadInvFkeyNoPay(fkeyA, out v6return);
                        paras.Result.InvoiceNo = v6return.SO_HD;
                        if (!string.IsNullOrEmpty(paras.Result.InvoiceNo))
                        {
                            result = "OK-Đã tồn tại fkey.";
                            paras.Result.ResultString = result;
                        }
                    }
                }
                else if (paras.Mode.ToLower() == "DownloadInvFkeyNoPay".ToLower())
                {
                    fkeyA = paras.Fkey_hd;
                    
                    string invXml = vnptWS.DownloadInvFkeyNoPay(fkeyA, out paras.Result.V6ReturnValues);
                    result += paras.Result.InvoiceNo;
                }
                else if (paras.Mode == "S")
                {
                    var xml = ReadDataS_Vnpt();
                    result = vnptWS.adjustInv(xml, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    string filePath = Path.Combine(paras.Dir, paras.FileName);
                    if (filePath.Length > 0 && result.StartsWith("OK"))
                    {
                        if (File.Exists(filePath))
                        {
                            result += vnptWS.UploadInvAttachmentFkey(fkeyA, filePath);
                        }
                        else
                        {
                            result += "Không tồn tại " + filePath;
                        }
                    }
                }
                else if (paras.Mode == "T")
                {
                    var xml = ReadDataXmlT();
                    result = vnptWS.replaceInv(xml, paras.Fkey_hd_tt, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("G"))
                {
                    if (paras.Mode == "G1") // Gạch nợ theo fkey
                    {
                        result = vnptWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    {
                        result = vnptWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G3") // Hủy gạch nợ theo fkey
                    {
                        result = vnptWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode == "H")
                {
                    result = vnptWS.cancelInv(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "D")
                {
                    // Danh muc??
                    string type = "1";
                    if (paras.Mode.Length > 1) type = paras.Mode[1].ToString();
                    result = DoUpdateCus(vnptWS, am_table, type);
                    //DataTable data = V6BusinessHelper.Select("ALKH", "*", "ISNULL([E_MAIL],'') <> ''").Data;
                    //result = DoUpdateCus(data, type);
                }
                else if (paras.Mode.StartsWith("U"))//U1,U2
                {
                    if (paras.Mode == "U")        // upload file có sẵn, fkey truyền vào
                    {
                        string fkey = paras.Fkey_hd;
                        string file = Path.Combine(paras.Dir, paras.FileName);
                        vnptWS.UploadInvAttachmentFkey(fkey, file);
                    }
                    else if (paras.Mode == "U1") // upload file có sẵn, fkey tự đọc từ data
                    {
                        //ReadDataXml(arg2);
                        string fkey = paras.Fkey_hd;
                        vnptWS.UploadInvAttachmentFkey(fkey, fkey + ".xls");
                    }
                    else if (paras.Mode == "U2") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel rồi upload.
                    {
                        string export_file;
                        //ReadDataXml(arg2);
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                        if (export_ok && File.Exists(export_file))
                        {
                            result += vnptWS.UploadInvAttachmentFkey(fkeyA, export_file);
                        }
                    }
                    else if (paras.Mode == "U3") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel để đó xem.
                    {
                        string export_file;
                        //ReadDataXml(arg2);
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                        if (export_ok && File.Exists(export_file))
                        {
                            result += "\r\nExport ok.";
                        }
                    }
                }
                else if (paras.Mode.StartsWith("E"))
                {
                    if (paras.Mode == "E")
                    {

                    }
                    else if (paras.Mode == "E1")
                    {
                        string export_file;
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);
                        if (export_ok && File.Exists(export_file))
                        {
                            result += "\r\nExport ok.";
                        }
                    }
                    else if (paras.Mode == "E2")  // Xuất PDF bằng RPT
                    {
                        string rptFile = paras.RptFileFull;
                        string saveFile = Path.Combine(paras.Dir, paras.FileName);// arg4;

                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(rptFile);
                        DataSet ds = new DataSet();
                        DataTable data1 = ad_table.Copy();
                        data1.TableName = "DataTable1";
                        DataTable data2 = am_table.Copy();
                        data2.TableName = "DataTable2";
                        ds.Tables.Add(data1);
                        ds.Tables.Add(data2);
                        string tien_bang_chu = MoneyToWords(ObjectAndString.ObjectToDecimal(row0["T_TT"]), "V", "VND");
                        rpt.SetDataSource(ds);
                        rpt.SetParameterValue("SoTienVietBangChu", tien_bang_chu);

                        bool export_ok = false;
                        if (string.IsNullOrEmpty(saveFile))
                        {
                            export_ok = ExportRptToPdf_As(null, rpt, saveFile);
                        }
                        else
                        {
                            export_ok = ExportRptToPdf(null, rpt, saveFile);
                        }

                        if (export_ok)
                        {
                            result += "\r\nExport ok.";
                        }
                        else
                        {
                            result += "\r\nExport fail.";
                        }
                    }
                }
                else
                {
                    result = "ERR: Mode " + paras.Mode + " " + V6Text.NotSupported;
                }


                if (result.StartsWith("ERR"))
                {
                    //error += result;
                    //paras.Result.ResultErrorMessage = result;
                }
                else
                {
                    //File.Create(flagFileName2).Close();
                }
            }
            catch (Exception ex)
            {
                result += "ERR:EX\r\n" + ex.Message;
                paras.Result.ResultErrorMessage = ex.Message;
            }
            StopAutoInputTokenPassword();
            End:
            return result;
        }

        private static string EXECUTE_VNPT_78(PostManagerParams paras)
        {
            string result = "";
            paras.Result = new PM_Result();

            try
            {
                var row0 = am_table.Rows[0];
                Vnpt78WS vnptWS = new Vnpt78WS(_baseUrl, _account, _accountpassword, _username, _password);
                if (paras.Mode == "TestView")
                {
                    var xml = ReadData_Vnpt();
                    result = xml;
                    paras.Result.ResultString = xml;
                }
                else if (paras.Mode == "TestView_Shift")
                {
                    // Download thôngg tin ?????
                }
                else if (paras.Mode.StartsWith("E_"))
                {
                    if (paras.Mode == "E_G1") // Gạch nợ theo fkey
                    {
                        if (string.IsNullOrEmpty(paras.Fkey_hd))
                        {
                            paras.Result.ResultErrorMessage = "Không có Fkey_hd truyền vào.";
                        }
                        else
                        {
                            result = vnptWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                        }
                    }
                    else if (paras.Mode == "E_G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    {
                        result = vnptWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_G3") // Hủy gạch nợ theo fkey
                    {
                        result = vnptWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_H1")
                    {
                        result = vnptWS.cancelInv(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_S1")
                    {
                        var xml = ReadDataS_Vnpt();
                        result = vnptWS.adjustInv(xml, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_T1")
                    {
                        var xml = ReadDataXmlT();
                        result = vnptWS.replaceInv(xml, paras.Fkey_hd_tt, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode.StartsWith("M") || paras.Mode == "") // MSHDT//Mới Sửa Hủy ĐiềuChỉnh(S) ThayThế
                {
                    try // Update danh mục khách hàng.
                    {
                        if (customerInfoConfig != null && customerInfoConfig.Count > 0)
                        {
                            DoUpdateCus78(vnptWS, am_table);
                        }
                    }
                    catch (Exception)
                    {
                        // Bỏ qua lỗi.
                    }
                    var xml = ReadData_Vnpt(paras.Key_Down);

                    if (string.IsNullOrEmpty(_SERIAL_CERT))
                    {
                        if (paras.Key_Down == "F4") // gửi nháp
                        {
                            result = vnptWS.ImportInvByPattern(xml, __pattern, __serial, out paras.Result.V6ReturnValues);
                            if (result.StartsWith("ERR:13")) // nếu đã tồn tại. thì xóa nháp + gửi lại.
                            {
                                result = vnptWS.DeleteInvoiceByFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                                if (result.StartsWith("OK:"))
                                {
                                    result = vnptWS.ImportInvByPattern(xml, __pattern, __serial, out paras.Result.V6ReturnValues);
                                }
                            }
                        }
                        else if (paras.Key_Down == "F6") // sửa hóa đơn nháp = xóa nháp + gửi lại.
                        {
                            result = vnptWS.DeleteInvoiceByFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                            if (result.StartsWith("OK:"))
                            {
                                result = vnptWS.ImportInvByPattern(xml, __pattern, __serial, out paras.Result.V6ReturnValues);
                            }
                        }
                        //else if (paras.Key_Down == "F8") // xóa nháp
                        //{
                        //    result = vnptWS.DeleteInvoiceByFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                        //}
                        else // F9 phát hành
                        {
                            result = vnptWS.ImportAndPublishInv(xml, __pattern, __serial, out paras.Result.V6ReturnValues);
                            if (result.StartsWith("ERR:13")) // nếu đã tồn tại, phát hành theo fkey.
                            {
                                result = vnptWS.PublishInvFkey(paras.Fkey_hd, __pattern, __serial, out paras.Result.V6ReturnValues);
                            }
                        }                        
                    }
                    else
                    {
                        StartAutoInputTokenPassword();
                        result = vnptWS.PublishInvWithToken32_Dll(xml, __pattern, __serial, _SERIAL_CERT, out paras.Result.V6ReturnValues);
                    }


                    //string invXml = DownloadInvFkeyNoPay(fkeyA);
                    //paras.Result.InvoiceNo = GetSoHoaDon_VNPT(invXml);

                    string filePath = Path.Combine(paras.Dir, paras.FileName);
                    if (filePath.Length > 0 && result.StartsWith("OK"))
                    {
                        if (paras.Mode.EndsWith("1"))//Gửi file excel có sẵn
                        {
                            if (File.Exists(filePath))
                            {
                                result += vnptWS.UploadInvAttachmentFkey(fkeyA, filePath);
                            }
                            else
                            {
                                result += "Không tồn tại " + filePath;
                            }
                        }
                        else if (paras.Mode.EndsWith("2")) // Tự xuất excel rồi gửi.
                        {
                            string export_file;
                            bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                            if (export_ok && File.Exists(export_file))
                            {
                                result += vnptWS.UploadInvAttachmentFkey(fkeyA, export_file);
                            }
                        }
                        else if (paras.Mode.EndsWith("3")) // Tự xuất pdf rồi gửi
                        {
                            string export_file;
                            if (string.IsNullOrEmpty(exportName))
                            {
                                var save = new SaveFileDialog
                                {
                                    Filter = "Pdf files (*.pdf)|*.pdf",
                                    Title = "Xuất pdf để gửi đi.",
                                };
                                if (save.ShowDialog() == DialogResult.OK)
                                {
                                    export_file = save.FileName;
                                }
                                else
                                {
                                    goto End;
                                }
                            }
                            else
                            {
                                export_file = exportName + ".pdf";
                            }

                            string rptFile = Path.Combine(paras.Dir, paras.RptFileFull);
                            ReportDocument rpt = new ReportDocument();
                            rpt.Load(rptFile);
                            DataSet ds = new DataSet();
                            DataTable data1 = ad_table.Copy();
                            data1.TableName = "DataTable1";
                            DataTable data2 = am_table.Copy();
                            data2.TableName = "DataTable2";
                            ds.Tables.Add(data1);
                            ds.Tables.Add(data2);
                            string tien_bang_chu = MoneyToWords(ObjectAndString.ObjectToDecimal(row0["T_TT"]), "V", "VND");
                            rpt.SetDataSource(ds);
                            rpt.SetParameterValue("SoTienVietBangChu", tien_bang_chu);
                            bool export_ok = ExportRptToPdf(null, rpt, export_file);
                            if (export_ok)
                            {
                                result += "\r\nExport ok.";
                            }
                            else
                            {
                                result += "\r\nExport fail.";
                            }

                            if (export_ok && File.Exists(export_file))
                            {
                                result += vnptWS.UploadInvAttachmentFkey(fkeyA, export_file);
                            }
                        }
                    }
                    else if (!string.IsNullOrEmpty(paras.Result.ResultErrorMessage))       // Hoặc đã có trên hệ thống HDDT ERR:0
                    {
                        V6Return v6return;
                        vnptWS.DownloadInvFkeyNoPay(fkeyA, out v6return);
                        paras.Result.InvoiceNo = v6return.SO_HD;
                        if (!string.IsNullOrEmpty(paras.Result.InvoiceNo))
                        {
                            result = "OK-Đã tồn tại fkey.";
                            paras.Result.ResultString = result;
                        }
                    }
                }
                else if (paras.Mode.ToLower() == "DownloadInvFkeyNoPay".ToLower())
                {
                    fkeyA = paras.Fkey_hd;

                    string invXml = vnptWS.DownloadInvFkeyNoPay(fkeyA, out paras.Result.V6ReturnValues);
                    result += paras.Result.InvoiceNo;
                }
                else if (paras.Mode == "S")
                {
                    var xml = ReadDataS_Vnpt();
                    result = vnptWS.adjustInv(xml, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    string filePath = Path.Combine(paras.Dir, paras.FileName);
                    if (filePath.Length > 0 && result.StartsWith("OK"))
                    {
                        if (File.Exists(filePath))
                        {
                            result += vnptWS.UploadInvAttachmentFkey(fkeyA, filePath);
                        }
                        else
                        {
                            result += "Không tồn tại " + filePath;
                        }
                    }
                }
                else if (paras.Mode == "T")
                {
                    var xml = ReadDataXmlT();
                    result = vnptWS.replaceInv(xml, paras.Fkey_hd_tt, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("G"))
                {
                    if (paras.Mode == "G1") // Gạch nợ theo fkey
                    {
                        result = vnptWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    {
                        result = vnptWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G3") // Hủy gạch nợ theo fkey
                    {
                        result = vnptWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode == "H")
                {
                    result = vnptWS.cancelInv(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "D")
                {
                    // Danh muc??
                    string type = "1";
                    if (paras.Mode.Length > 1) type = paras.Mode[1].ToString();
                    result = DoUpdateCus78(vnptWS, am_table, type);
                    //DataTable data = V6BusinessHelper.Select("ALKH", "*", "ISNULL([E_MAIL],'') <> ''").Data;
                    //result = DoUpdateCus(data, type);
                }
                else if (paras.Mode.StartsWith("U"))//U1,U2
                {
                    if (paras.Mode == "U")        // upload file có sẵn, fkey truyền vào
                    {
                        string fkey = paras.Fkey_hd;
                        string file = Path.Combine(paras.Dir, paras.FileName);
                        vnptWS.UploadInvAttachmentFkey(fkey, file);
                    }
                    else if (paras.Mode == "U1") // upload file có sẵn, fkey tự đọc từ data
                    {
                        //ReadDataXml(arg2);
                        string fkey = paras.Fkey_hd;
                        vnptWS.UploadInvAttachmentFkey(fkey, fkey + ".xls");
                    }
                    else if (paras.Mode == "U2") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel rồi upload.
                    {
                        string export_file;
                        //ReadDataXml(arg2);
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                        if (export_ok && File.Exists(export_file))
                        {
                            result += vnptWS.UploadInvAttachmentFkey(fkeyA, export_file);
                        }
                    }
                    else if (paras.Mode == "U3") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel để đó xem.
                    {
                        string export_file;
                        //ReadDataXml(arg2);
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                        if (export_ok && File.Exists(export_file))
                        {
                            result += "\r\nExport ok.";
                        }
                    }
                }
                else if (paras.Mode.StartsWith("E"))
                {
                    if (paras.Mode == "E")
                    {

                    }
                    else if (paras.Mode == "E1")
                    {
                        string export_file;
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);
                        if (export_ok && File.Exists(export_file))
                        {
                            result += "\r\nExport ok.";
                        }
                    }
                    else if (paras.Mode == "E2")  // Xuất PDF bằng RPT
                    {
                        string rptFile = paras.RptFileFull;
                        string saveFile = Path.Combine(paras.Dir, paras.FileName);// arg4;

                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(rptFile);
                        DataSet ds = new DataSet();
                        DataTable data1 = ad_table.Copy();
                        data1.TableName = "DataTable1";
                        DataTable data2 = am_table.Copy();
                        data2.TableName = "DataTable2";
                        ds.Tables.Add(data1);
                        ds.Tables.Add(data2);
                        string tien_bang_chu = MoneyToWords(ObjectAndString.ObjectToDecimal(row0["T_TT"]), "V", "VND");
                        rpt.SetDataSource(ds);
                        rpt.SetParameterValue("SoTienVietBangChu", tien_bang_chu);

                        bool export_ok = false;
                        if (string.IsNullOrEmpty(saveFile))
                        {
                            export_ok = ExportRptToPdf_As(null, rpt, saveFile);
                        }
                        else
                        {
                            export_ok = ExportRptToPdf(null, rpt, saveFile);
                        }

                        if (export_ok)
                        {
                            result += "\r\nExport ok.";
                        }
                        else
                        {
                            result += "\r\nExport fail.";
                        }
                    }
                }
                else
                {
                    result = "ERR: Mode " + paras.Mode + " " + V6Text.NotSupported;
                    if (paras.Result.V6ReturnValues == null) paras.Result.V6ReturnValues = new V6Return();
                    paras.Result.V6ReturnValues.RESULT_STRING = result;
                    paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = result;
                }
            }
            catch (Exception ex)
            {
                result += "ERR:EX\r\n" + ex.Message;
                paras.Result.ResultErrorMessage = ex.Message;
            }
            StopAutoInputTokenPassword();
        End:
            return result;
        }

        public static string DoUpdateCus(DataTable data, string type = "1")
        {
            return DoUpdateCus(CreateVnptWS(), data, type);
        }

        public static string DoUpdateCus78(DataTable data, string type = "1")
        {
            return DoUpdateCus78(CreateVnpt78WS(), data, type);
        }

        public static string DoUpdateCus(VnptWS vnptWS, DataTable data, string type = "1")
        {
            string update_cus_result = "";
            string error = "";
            int error_count = 0, success_count = 0;
            
            try
            {
                //var data = ReadDbf(dbf);
                if (type == "1") // 1 times 1 cus
                {
                    Customer cus = null;
                    string ma_kh = null;
                    int count = 0;
                    foreach (DataRow row in data.Rows)
                    {
                        count++;
                        try
                        {
                            cus = null;
                            ma_kh = null;
                            Customers cuss = new Customers();
                            cus = ReadCusDataXml(row);
                            ma_kh = row["MA_KH"].ToString().Trim();
                            cuss.Customer_List.Add(cus);
                            string xml = V6XmlConverter.ClassToXml(cuss);

                            Logger.WriteToLog(string.Format("Preparing UpdateCus {0} {1}\r\n{2}", count, ma_kh, xml));
                            V6Return v6return;
                            var num = vnptWS.UpdateCus(xml, out v6return);
                            if (num == "1")
                            {
                                success_count++;
                                Logger.WriteToLog(string.Format("UpdateCus {0} {1} Success.", count, ma_kh));
                            }
                            else error_count++;

                            update_cus_result += string.Format("\r\n Update {0} status: {1}", ma_kh, num);
                        }
                        catch (Exception ex)
                        {
                            error_count++;
                            if (!string.IsNullOrEmpty(ma_kh) && cus != null)
                            {
                                error += "\nCustomer " + count + " " + ma_kh;
                            }
                            else
                            {
                                error += "\nCustomer " + count + " null OR no code.";
                            }
                            error += "\n" + ex.Message;
                        }
                    }
                }
                else // 1 times all cus
                {
                    try
                    {
                        Customers cuss = new Customers();
                        foreach (DataRow row in data.Rows)
                        {
                            Customer cus = ReadCusDataXml(row);
                            cuss.Customer_List.Add(cus);
                        }
                        string xml = V6XmlConverter.ClassToXml(cuss);

                        Logger.WriteToLog("Preparing UpdateCus:\r\n" + xml);
                        V6Return v6return;
                        var num = vnptWS.UpdateCus(xml, out v6return);
                        success_count = Convert.ToInt32(num);
                        update_cus_result += "Success " + num + "/" + data.Rows.Count;
                    }
                    catch (Exception ex)
                    {
                        error += ex.Message;
                        error_count = 1;
                    }
                }

            }
            catch (Exception ex)
            {
                error += "\n" + ex.Message;
            }

            if (error.Length > 0) update_cus_result += "ERR: " + error;
            Logger.WriteToLog("Program.DoUpdateCus " + update_cus_result);
            //return result;
            return string.Format("Success {0}   Error {1}\n{2}", success_count, error_count, update_cus_result);
        }

        public static string DoUpdateCus78(Vnpt78WS vnptWS, DataTable data, string type = "1")
        {
            string update_cus_result = "";
            string error = "";
            int error_count = 0, success_count = 0;

            try
            {
                //var data = ReadDbf(dbf);
                if (type == "1") // 1 times 1 cus
                {
                    Customer cus = null;
                    string ma_kh = null;
                    int count = 0;
                    foreach (DataRow row in data.Rows)
                    {
                        count++;
                        try
                        {
                            cus = null;
                            ma_kh = null;
                            Customers cuss = new Customers();
                            cus = ReadCusDataXml(row);
                            ma_kh = row["MA_KH"].ToString().Trim();
                            cuss.Customer_List.Add(cus);
                            string xml = V6XmlConverter.ClassToXml(cuss);

                            Logger.WriteToLog(string.Format("Preparing UpdateCus {0} {1}\r\n{2}", count, ma_kh, xml));
                            V6Return v6return;
                            var num = vnptWS.UpdateCus(xml, out v6return);
                            if (num == "1")
                            {
                                success_count++;
                                Logger.WriteToLog(string.Format("UpdateCus {0} {1} Success.", count, ma_kh));
                            }
                            else error_count++;

                            update_cus_result += string.Format("\r\n Update {0} status: {1}", ma_kh, num);
                        }
                        catch (Exception ex)
                        {
                            error_count++;
                            if (!string.IsNullOrEmpty(ma_kh) && cus != null)
                            {
                                error += "\nCustomer " + count + " " + ma_kh;
                            }
                            else
                            {
                                error += "\nCustomer " + count + " null OR no code.";
                            }
                            error += "\n" + ex.Message;
                        }
                    }
                }
                else // 1 times all cus
                {
                    try
                    {
                        Customers cuss = new Customers();
                        foreach (DataRow row in data.Rows)
                        {
                            Customer cus = ReadCusDataXml(row);
                            cuss.Customer_List.Add(cus);
                        }
                        string xml = V6XmlConverter.ClassToXml(cuss);

                        Logger.WriteToLog("Preparing UpdateCus:\r\n" + xml);
                        V6Return v6return;
                        var num = vnptWS.UpdateCus(xml, out v6return);
                        success_count = Convert.ToInt32(num);
                        update_cus_result += "Success " + num + "/" + data.Rows.Count;
                    }
                    catch (Exception ex)
                    {
                        error += ex.Message;
                        error_count = 1;
                    }
                }

            }
            catch (Exception ex)
            {
                error += "\n" + ex.Message;
            }

            if (error.Length > 0) update_cus_result += "ERR: " + error;
            Logger.WriteToLog("Program.DoUpdateCus " + update_cus_result);
            //return result;
            return string.Format("Success {0}   Error {1}\n{2}", success_count, error_count, update_cus_result);
        }

        public static Customer ReadCusDataXml(DataRow row)
        {
            Customer cus = new Customer();
            foreach (KeyValuePair<string, ConfigLine> item in customerInfoConfig)
            {
                cus.Customer_Info[item.Key] = GetValue(row, item.Value);
            }
            return cus;
        }

        public static string ReadData_Vnpt(string mode = null)
        {
            string result = "";
            
            try
            {
                DataRow row0 = am_table.Rows[0];
                //PostObject obj = new PostObject();
                Invoices postObject = new Invoices();
                //ReadXmlInfo(xmlFile);
                //DataTable ad_table = ReadDbf(dbfFile);

                //Fill data to postObject
                //var invs = new List<Inv>();
                var inv = new Inv();
                //invs.Add(inv);
                postObject.Inv.Add(inv);

                //Sửa lại fkey dùng fkeyField
                fkeyA = row0["fkey_hd"].ToString().Trim();

                //{Tuanmh V6_05_2222222222222222_3333333333333
                exportName = string.Format("{0}_{1}_{2}_{3}", fkeyexcel0, row0["MA_KH"].ToString().Trim(), row0["SO_CT"].ToString().Trim(), row0["STT_REC"]);
                //}

                inv.key = fkeyA;
                __pattern = row0[pattern_field].ToString().Trim();
                __serial = row0[seri_field].ToString().Trim();
                //flagName = fkeyA;
                //MakeFlagNames(fkeyA);


                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.generalInvoiceInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.buyerInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> sellerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
                }


                foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }


                var products = new Products();
                foreach (DataRow row in ad_table.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;

                    Product product = new Product();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        product.Details[item.Key] = GetValue(row, item.Value);
                    }

                    products.Add(product);
                }
                inv.Invoice["Products"] = products;


                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                //if (mode == "F4") // tạo hóa đơn nháp, chuyển trạng thái gạch nợ = 0
                //{
                //    inv.Invoice["PaymentStatus"] = "0";
                //}

                foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                result = postObject.ToXml();
                if (_write_log)
                {
                    string stt_rec = row0["STT_REC"].ToString();
                    string file = Path.Combine(Application.StartupPath, stt_rec + ".json");
                    File.WriteAllText(file, result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static string ReadDataS_Vnpt()
        {
            string result = "";
            //try
            {
                //PostObject obj = new PostObject();
                AdjustInv inv = new AdjustInv();
                
                DataRow row0 = am_table.Rows[0];
                fkeyA = row0["fkey_hd"].ToString().Trim();
                inv.key = fkeyA;
                
                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.generalInvoiceInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.buyerInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> sellerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
                }


                foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                var products = new List<Product>();
                foreach (DataRow row in ad_table.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;

                    Product product = new Product();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        product.Details[item.Key] = GetValue(row, item.Value);
                    }

                    products.Add(product);
                }
                inv.Invoice["Products"] = products;

                //private static Dictionary<string, XmlLine> summarizeInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                result = inv.ToXml();
            }
            //catch (Exception ex)
            {
                //
            }
            return result;
        }

        public static string ReadDataXmlT()
        {
            string result = "";
            //try
            {
                //PostObject obj = new PostObject();
                ReplaceInv inv = new ReplaceInv();
                //ReadXmlInfo(xmlFile);

                DataRow row0 = am_table.Rows[0];
                fkeyA = row0["fkey_hd"].ToString().Trim();
                
                //inv.key = fkeyA;
                inv.Invoice["key"] = fkeyA;
                //pattern = row0[pattern_field].ToString().Trim();
                //seri = row0[seri_field].ToString().Trim();
                //MakeFlagNames(fkeyA);

                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.generalInvoiceInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.buyerInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> sellerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
                }


                foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                var products = new List<Product>();
                foreach (DataRow row in ad_table.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;

                    Product product = new Product();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        product.Details[item.Key] = GetValue(row, item.Value);
                    }

                    products.Add(product);
                }
                inv.Invoice["Products"] = products;

                //private static Dictionary<string, XmlLine> summarizeInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                result = inv.ToXml();
            }
            //catch (Exception ex)
            {
                //
            }
            return result;
        }

        public static Invoices ReadData_SoftDreams(string mode)
        {
            Invoices postObject = null;
            //string result = "";
            column_config = new SortedDictionary<string, string>();
            parameters_config = new List<ConfigLine>();
            //try
            {
                postObject = new Invoices();
                //ReadXmlInfo(xmlFile);
                //DataTable data = ReadDbf(dbfFile);

                var inv = new Inv();
                postObject.Inv.Add(inv);

                DataRow row0 = am_table.Rows[0];
                //fkeyA = fkey0 + row0["STT_REC"];
                fkeyA = row0["fkey_hd"].ToString().Trim();
                
                //{Tuanmh V6_05_2222222222222222_3333333333333
                exportName = string.Format("{0}_{1}_{2}_{3}", fkeyexcel0, row0["MA_KH"].ToString().Trim(), row0["SO_CT"].ToString().Trim(), row0["STT_REC"]);
                //}

                inv.Invoice["Ikey"] = fkeyA;
                __pattern = row0[pattern_field].ToString().Trim();
                __serial = row0[seri_field].ToString().Trim();
                //flagName = fkeyA;
                //MakeFlagNames(fkeyA);


                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.generalInvoiceInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.buyerInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> sellerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
                }


                foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }


                var products = new Products();
                foreach (DataRow row in ad_table.Rows)
                {
                    //if (row["STT"].ToString() == "0") continue;

                    Product product = new Product();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        product.Details[item.Key] = GetValue(row, item.Value);
                    }

                    products.Add(product);
                }
                inv.Invoice["Products"] = products;

                if (taxBreakdownsConfig != null && ad3_table != null && ad3_table.Rows.Count > 0)
                {
                    var taxBreakdowns = new InvoiceFees();
                    foreach (DataRow ad3_row in ad3_table.Rows)
                    {
                        var fee = new InvoiceFee();
                        foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                        {
                            fee.Details[item.Key] = GetValue(ad3_row, item.Value);
                        }
                        taxBreakdowns.Add(fee);
                    }

                    inv.Invoice["InvoiceFees"] = taxBreakdowns;
                }

                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                
                if (_write_log)
                {
                    string result = postObject.ToXml();
                    string stt_rec = row0["STT_REC"].ToString();
                    string file = Path.Combine(Application.StartupPath, stt_rec + ".json");
                    File.WriteAllText(file, result);
                }
            }
            //catch (Exception ex)
            {
                //
            }
            return postObject;
        }

        /// <summary>
        /// Đọc dữ liệu trả về Class object cho Webservice.
        /// </summary>
        /// <param name="mode">M-H</param>
        /// <returns></returns>
        public static object ReadData_ThaiSon(string mode)
        {
            object result_entity = null;

            column_config = new SortedDictionary<string, string>();
            var am_data = new Dictionary<string, object>();
            List<HangHoaEntity> list_hanghoa = new List<HangHoaEntity>();
            parameters_config = new List<ConfigLine>();
            //try
            {
                DataTable data = am_table;

                DataRow row0 = am_table.Rows[0];
                //fkeyA = fkey0 + row0["STT_REC"];
                fkeyA = row0["fkey_hd"].ToString().Trim();

                exportName = string.Format("{0}_{1}_{2}_{3}", fkeyexcel0, row0["MA_KH"].ToString().Trim(), row0["SO_CT"].ToString().Trim(), row0["STT_REC"]);
                
                __pattern = row0[pattern_field].ToString().Trim();
                __serial = row0[seri_field].ToString().Trim();
                
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    am_data[item.Key] = GetValue(row0, item.Value);
                }
                
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    am_data[item.Key] = GetValue(row0, item.Value);
                }
                
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    am_data[item.Key] = GetValue(row0, item.Value);
                }

                foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                {
                    am_data[item.Key] = GetValue(row0, item.Value);
                }


                if (mode == "H") goto EndDetail;
                foreach (DataRow row in ad_table.Rows)
                {
                    //if (row["STT"].ToString() == "0") continue;
                    var ad_data = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        ad_data[item.Key] = GetValue(row, item.Value);
                    }

                    var product = ad_data.ToClass<HangHoaEntity>();
                    list_hanghoa.Add(product);
                }
            EndDetail:

                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    am_data[item.Key] = GetValue(row0, item.Value);
                }

                foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                {
                    am_data[item.Key] = GetValue(row0, item.Value);
                }

                if (mode == "H")
                {
                    var hoa_don_huy_entity = am_data.ToClass<HoaDonHuyEntity>();
                    result_entity = hoa_don_huy_entity;
                }
                else // mode M
                {
                    var hoa_don_entity = am_data.ToClass<HoaDonEntity>();
                    hoa_don_entity.HangHoas = list_hanghoa.ToArray();
                    //hoa_don_entity.dataExtension = list_extension.ToArray();
                    //hoa_don_entity.emptysField = emptysField.ToArray();
                    result_entity = hoa_don_entity;
                }

                if (_write_log)
                {
                    string result = V6JsonConverter.ClassToJson(result_entity, "dd/MM/yyyy");
                    string stt_rec = row0["STT_REC"].ToString();
                    string file = Path.Combine(Application.StartupPath, stt_rec + ".json");
                    File.WriteAllText(file, result);
                }
            }
            //catch (Exception ex)
            {
                //
            }
            return result_entity;
        }



        public static string FileToBase64(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            string fileBase64 = Convert.ToBase64String(fileBytes);
            return fileBase64;
        }
        

        

        

        private static string GetSoHoaDon_VNPT(string invXml)
        {
            string result = "";
            try
            {
                string startTerm = "<InvoiceNo>";
                string endTerm = "</InvoiceNo>";
                int startIndex = invXml.IndexOf(startTerm, StringComparison.Ordinal);
                if (startIndex > 0)
                {
                    startIndex += startTerm.Length;
                    int endIndex = invXml.IndexOf(endTerm, StringComparison.Ordinal);
                    if (endIndex > startIndex)
                    {
                        result = invXml.Substring(startIndex, endIndex - startIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data1">Bảng 1</param>
        /// <param name="data2">Bảng nhiều</param>
        /// <param name="exportFile"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool ExportExcel(DataTable data1, DataTable data2, out string exportFile, ref string result)
        {
            try
            {
                var row0 = data1.Rows[0];
                //{Tuanmh 10/06/2018
                //string export_file = fkeyA + ".xls";
                string export_file = "";
                if (string.IsNullOrEmpty(exportName))
                {
                    var save = new SaveFileDialog
                    {
                        Filter = "Excel files (*.xls)|*.xls|*.xlsx|*.xlsx",
                        Title = "Xuất Excel.",
                    };
                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        export_file = save.FileName;
                    }
                    else
                    {
                        exportFile = null;
                        return false;
                    }
                }
                else
                {
                    export_file = exportName + ".xls";
                }

                // Tạo parameter.
                SortedDictionary<string, object> parameters = new SortedDictionary<string, object>();
                foreach (ConfigLine config_line in parameters_config)
                {
                    string content = config_line.Value;
                    if (config_line.Value.Contains("{") && config_line.Value.Contains("}"))
                    {
                        var regex = new Regex("{(.+?)}");
                        foreach (Match match in regex.Matches(config_line.Value))
                        {
                            var matchGroup0 = match.Groups[0].Value;
                            var matchContain = match.Groups[1].Value;
                            var matchColumn = matchContain;
                            var matchFormat = "";
                            if (matchContain.Contains(":"))
                            {
                                int _2dotIndex = matchContain.IndexOf(":", StringComparison.InvariantCulture);
                                matchColumn = matchContain.Substring(0, _2dotIndex);
                                matchFormat = matchContain.Substring(_2dotIndex+1);
                            }
                            if (data1.Columns.Contains(matchColumn))
                            {
                                if (data1.Columns[matchColumn].DataType == typeof(DateTime) && matchFormat == "")
                                {
                                    matchFormat = "dd/MM/yyyy";
                                }
                                content = content.Replace(matchGroup0, ObjectAndString.ObjectToString(row0[matchColumn], matchFormat));
                            }
                            //var matchKey = match.Groups[1].Value;
                            //if (data1.Columns.Contains(matchKey))
                            //{
                            //    replaced_value = config_line.Value.Replace(match.Groups[0].Value,
                            //        ObjectAndString.ObjectToString(row0[matchKey]));
                            //}
                        }
                        if (parameters.ContainsKey(config_line.Field))
                        {
                            MessageBox.Show("Trùng khóa cấu hình excel: key=" + config_line.Field);
                            continue;
                        }
                        parameters.Add(config_line.Field, content);
                    }
                    else
                    {
                        if (data1.Columns.Contains(config_line.Value))
                        {
                            parameters.Add(config_line.Field, row0[content]);
                        }
                    }
                }

                var setting = new ExportExcelSetting();
                setting.SetFirstCell(firstCell);
                setting.saveFile = export_file;
                setting.data = data2;
                setting.columns = columns;
                setting.parameters = parameters;
                setting.isInsertRow = insertRow;
                setting.isDrawLine = drawLine;
                bool export_ok = ExportData.ToExcelTemplate(template_xls, setting, NumberFormatInfo.InvariantInfo);

                //{Tuanmh test
                //File.Copy(export_file, "test_out.xls", true);
                //}

                if (export_ok)
                {
                    result += "\r\nExport Excel ok";
                    exportFile = export_file;
                }
                else
                {
                    exportFile = "";
                }

                return export_ok;
            
            }
            catch (Exception ex)
            {
                result += "Export Excel không thành công: " + ex.Message;
            }
            exportFile = "";
            return false;
        }

        public static bool ExportRptToPdf_As(IWin32Window owner, ReportDocument rpt, string defaultSaveName = "")
        {

            if (rpt == null)
            {
                return false;
            }
            try
            {
                var save = new SaveFileDialog
                {
                    Filter = "Pdf files (*.pdf)|*.pdf",
                    Title = "Xuất PDF.",
                    FileName = ChuyenMaTiengViet.ToUnSign(defaultSaveName)
                };
                if (save.ShowDialog(owner) == DialogResult.OK)
                {
                    return ExportRptToPdf(owner, rpt, save.FileName);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog("Program.ExportRptToPdf_As", ex, "");
            }
            return false;
        }

        public static bool ExportRptToPdf(IWin32Window owner, ReportDocument rpt, string fileName)
        {
            try
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = fileName;
                CrExportOptions = rpt.ExportOptions;

                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                CrExportOptions.FormatOptions = CrFormatTypeOptions;

                rpt.Export();
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteExLog("Program.ExportRptToPdf", ex, "");
            }
            return false;
        }

        #endregion ==== VNPT ====


        #region ==== VNPT_TOKEN ====

        /// <summary>
        /// Chạy tiến trình tự động điền password token nếu có thông tin pass và title.
        /// </summary>
        public static void StartAutoInputTokenPassword()
        {
            StopAutoInputTokenPassword();
            if (string.IsNullOrEmpty(_token_password)) return;
            if (string.IsNullOrEmpty(_token_password_title)) return;
            autoToken = new Thread(AutoInputTokenPassword);
            //autoToken.IsBackground = true;
            autoToken.Start();
        }

        private static Thread autoToken = null;
        public static void StopAutoInputTokenPassword()
        {
            if (autoToken != null && autoToken.IsAlive) autoToken.Abort();
        }
        private static void AutoInputTokenPassword()
        {
            try
            {
                //Find input password windows.
                Spy001 spy = new Spy001();
                var thisProcessID = Process.GetCurrentProcess().Id;
                SpyWindowHandle input_password_window = spy.FindWindow(_token_password_title, thisProcessID);

                while (input_password_window == null)
                {
                    input_password_window = spy.FindWindow(_token_password_title, thisProcessID);
                }
                //Find input password textbox, ok button
                //SpyWindowHandle input_handle = null;
                //SpyWindowHandle chk_soft_handle = null;
                //SpyWindowHandle ok_button_handle = null;
                //SpyWindowHandle soft_keyboard = null;

                //foreach (KeyValuePair<string, SpyWindowHandle> child_item in input_password_window.Childs)
                //{
                //    if (child_item.Value.Class.ClassName == "Edit")//Kích hoạt bàn phím ảo
                //    {
                //        input_handle = child_item.Value;
                //    }
                //    else if (child_item.Value.Text == "Đăng nhập")
                //    {
                //        ok_button_handle = child_item.Value;
                //    }
                //    else if (child_item.Value.Text.StartsWith("Kích hoạt"))
                //    {
                //        chk_soft_handle = child_item.Value;
                //    }
                //    else if (child_item.Value.Text == "soft keyboard")
                //    {
                //        soft_keyboard = child_item.Value;
                //    }
                //}

                //Input password
                {
                    input_password_window.SetForegroundWindow();
                    //if (input_handle != null) input_handle.SetFocus();
                    foreach (char c in _token_password)
                    {
                        spy.SendKeyPress(c);
                    }
                    spy.SendKeyPressEnter();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("Program.AutoInputTokenPassword " + ex.Message);
            }
        }

        private static string EXECUTE_VNPT_TOKEN(PostManagerParams paras)
        {
            string result = "";
            paras.Result = new PM_Result();
            try
            {
                var row0 = am_table.Rows[0];
                VnptWS vnptWS = new VnptWS(_baseUrl, _account, _accountpassword, _username, _password);
                if (paras.Mode == "TestView")
                {
                    var xml = ReadData_Vnpt();
                    result = xml;
                    paras.Result.ResultString = xml;
                }
                else if (paras.Mode.StartsWith("M"))     //  MSHDT//Mới Sửa Hủy ĐiềuChỉnh(S) ThayThế
                {
                    var xml = ReadData_Vnpt();
                    StartAutoInputTokenPassword();
                    string resultM = PublishInvWithToken_Dll(xml, __pattern, __serial, out paras.Result.V6ReturnValues);
                    result = resultM;
                    //"OK:mẫu số;ký hiệu-Fkey_Số hóa đơn,"
                    //"OK:01GTKT0/001;VT/19E-A0283806HDA_XXX"
                    if (resultM.StartsWith("ERR:0"))       // Hoặc đã có trên hệ thống HDDT ERR:0
                    {
                        V6Return v6return;
                        string invXml = vnptWS.DownloadInvFkeyNoPay(fkeyA, out v6return);
                        paras.Result.InvoiceNo = v6return.SO_HD;
                        if (!string.IsNullOrEmpty(paras.Result.InvoiceNo))
                        {
                            result = "OK-Đã tồn tại fkey. " + resultM;
                            paras.Result.ResultString = result;
                        }
                    }
                    else // chạy lần 2
                    {
                        StartAutoInputTokenPassword();
                        resultM = PublishInvWithToken_Dll(xml, __pattern, __serial, out paras.Result.V6ReturnValues);
                        result = resultM;
                    }

                    // Gửi file.
                    string filePath = Path.Combine(paras.Dir, paras.FileName);
                    if (filePath.Length > 0 && result.StartsWith("OK"))
                    {
                        if (paras.Mode.EndsWith("1"))//Gửi file excel có sẵn
                        {
                            if (File.Exists(filePath))
                            {
                                result += vnptWS.UploadInvAttachmentFkey(fkeyA, filePath);
                            }
                            else
                            {
                                result += "Không tồn tại " + filePath;
                            }
                        }
                        else if (paras.Mode.EndsWith("2")) // Tự xuất excel rồi gửi.
                        {
                            string export_file;
                            bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                            if (export_ok && File.Exists(export_file))
                            {
                                result += vnptWS.UploadInvAttachmentFkey(fkeyA, export_file);
                            }
                        }
                        else if (paras.Mode.EndsWith("3")) // Tự xuất pdf rồi gửi
                        {
                            string export_file = null;
                            if (string.IsNullOrEmpty(exportName))
                            {
                                var save = new SaveFileDialog
                                {
                                    Filter = "Pdf files (*.pdf)|*.pdf",
                                    Title = "Xuất pdf để gửi đi.",
                                };
                                if (save.ShowDialog() == DialogResult.OK)
                                {
                                    export_file = save.FileName;
                                }
                                else
                                {
                                    export_file = null;
                                    goto End;
                                }
                            }
                            else
                            {
                                export_file = exportName + ".pdf";
                            }

                            string rptFile = Path.Combine(paras.Dir, paras.RptFileFull);
                            ReportDocument rpt = new ReportDocument();
                            rpt.Load(rptFile);
                            DataSet ds = new DataSet();
                            DataTable data1 = ad_table.Copy();
                            data1.TableName = "DataTable1";
                            DataTable data2 = am_table.Copy();
                            data2.TableName = "DataTable2";
                            ds.Tables.Add(data1);
                            ds.Tables.Add(data2);
                            string tien_bang_chu = MoneyToWords(ObjectAndString.ObjectToDecimal(row0["T_TT"]), "V", "VND");
                            rpt.SetDataSource(ds);
                            rpt.SetParameterValue("SoTienVietBangChu", tien_bang_chu);
                            bool export_ok = ExportRptToPdf(null, rpt, export_file);
                            if (export_ok)
                            {
                                result += "\r\nExport ok.";
                            }
                            else
                            {
                                result += "\r\nExport fail.";
                            }

                            if (export_ok && File.Exists(export_file))
                            {
                                result += vnptWS.UploadInvAttachmentFkey(fkeyA, export_file);
                            }
                        }
                    }
                }
                else if (String.Equals(paras.Mode, "DownloadInvFkeyNoPay", StringComparison.CurrentCultureIgnoreCase))
                {
                    fkeyA = paras.Fkey_hd;
                    string invXml = vnptWS.DownloadInvFkeyNoPay(fkeyA, out paras.Result.V6ReturnValues);
                    result += paras.Result.InvoiceNo;
                }
                else if (paras.Mode == "S")
                {
                    var xml = ReadDataS_Vnpt();
                    result = vnptWS.adjustInv(xml, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    paras.Result.ResultString = result;
                    string filePath = Path.Combine(paras.Dir, paras.FileName);
                    if (filePath.Length > 0 && result.StartsWith("OK"))
                    {
                        if (File.Exists(filePath))
                        {
                            result += vnptWS.UploadInvAttachmentFkey(fkeyA, filePath);
                        }
                        else
                        {
                            result += "Không tồn tại " + filePath;
                        }
                    }
                }
                else if (paras.Mode == "T")
                {
                    var xml = ReadDataXmlT();
                    result = vnptWS.replaceInv(xml, paras.Fkey_hd_tt, out paras.Result.V6ReturnValues);
                    paras.Result.ResultString = result;
                }
                else if (paras.Mode.StartsWith("G"))
                {
                    if (paras.Mode == "G1") // Gạch nợ theo fkey
                    {
                        vnptWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    {
                        vnptWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G3") // Hủy gạch nợ theo fkey
                    {
                        vnptWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode == "H")
                {
                    //File.Create(flagFileName1).Close();
                    result = vnptWS.cancelInv_VNPT_TOKEN(paras.Fkey_hd, ReadData_Vnpt(), __pattern);
                }
                else if (paras.Mode == "D")
                {
                    //== S
                }
                else if (paras.Mode.StartsWith("U"))//U1,U2
                {
                    if (paras.Mode == "U")        // upload file có sẵn, fkey truyền vào
                    {
                        string fkey = paras.Fkey_hd;
                        string file = Path.Combine(paras.Dir, paras.FileName);
                        vnptWS.UploadInvAttachmentFkey(fkey, file);
                    }
                    else if (paras.Mode == "U1") // upload file có sẵn, fkey tự đọc từ data
                    {
                        //ReadDataXml(arg2);
                        string fkey = paras.Fkey_hd;
                        vnptWS.UploadInvAttachmentFkey(fkey, fkey + ".xls");
                    }
                    else if (paras.Mode == "U2") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel rồi upload.
                    {
                        string export_file;
                        //ReadDataXml(arg2);
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                        if (export_ok && File.Exists(export_file))
                        {
                            result += vnptWS.UploadInvAttachmentFkey(fkeyA, export_file);
                        }
                    }
                    else if (paras.Mode == "U3") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel để đó xem.
                    {
                        string export_file;
                        //ReadDataXml(arg2);
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                        if (export_ok && File.Exists(export_file))
                        {
                            result += "\r\nExport ok.";
                        }
                    }
                }
                else if (paras.Mode.StartsWith("E"))
                {
                    if (paras.Mode == "E")
                    {

                    }
                    else if (paras.Mode == "E1")
                    {
                        string export_file;
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);
                        if (export_ok && File.Exists(export_file))
                        {
                            result += "\r\nExport ok.";
                        }
                    }
                    else if (paras.Mode == "E2")  // Xuất PDF bằng RPT
                    {
                        string rptFile = paras.RptFileFull;
                        string saveFile = Path.Combine(paras.Dir, paras.FileName);// arg4;

                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(rptFile);
                        DataSet ds = new DataSet();
                        DataTable data1 = ad_table.Copy();
                        data1.TableName = "DataTable1";
                        DataTable data2 = am_table.Copy();
                        data2.TableName = "DataTable2";
                        ds.Tables.Add(data1);
                        ds.Tables.Add(data2);
                        string tien_bang_chu = MoneyToWords(ObjectAndString.ObjectToDecimal(row0["T_TT"]), "V", "VND");
                        rpt.SetDataSource(ds);
                        rpt.SetParameterValue("SoTienVietBangChu", tien_bang_chu);

                        bool export_ok = false;
                        if (string.IsNullOrEmpty(saveFile))
                        {
                            export_ok = ExportRptToPdf_As(null, rpt, saveFile);
                        }
                        else
                        {
                            export_ok = ExportRptToPdf(null, rpt, saveFile);
                        }

                        if (export_ok)
                        {
                            result += "\r\nExport ok.";
                        }
                        else
                        {
                            result += "\r\nExport fail.";
                        }
                    }
                }
                else
                {
                    result = "ERR: Mode " + paras.Mode + " " + V6Text.NotSupported;
                }


                if (result.StartsWith("ERR"))
                {
                    paras.Result.ResultErrorMessage = result;
                }
                else
                {
                    //File.Create(flagFileName2).Close();
                }
            }
            catch (Exception ex)
            {
                paras.Result.ResultErrorMessage = ex.Message;
                result += "ERR:EX\r\n" + ex.Message;
            }
            StopAutoInputTokenPassword();
        //File.Create(flagFileName9).Close();
        //BaseMessage.Show(result, 500);
        End:
            return result;
        }

        /// <summary>
        /// Đẩy lên và phát hành hóa đơn có ký chữ ký số (Token).
        /// </summary>
        /// <param name="xmlInvData">chuỗi xml hóa đơn.</param>
        /// <param name="pattern">Mấu số 01GTKT0/001</param>
        /// <param name="serial">Ký hiệu VT/19E</param>
        /// <param name="v6Return">Kết quả</param>
        /// <returns>Thành công: trả về "OK:" + mẫu số + “;” + ký hiệu + “-” + Fkey + “_” + Số hóa đơn + “,”</returns>
        public static string PublishInvWithToken_Dll(string xmlInvData, string pattern, string serial, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                result = VNPTEInvoiceSignToken.PublishInvWithToken(_account, _accountpassword, xmlInvData, _username, _password, _SERIAL_CERT, pattern, serial, _link_Publish_vnpt_thaison);
                v6Return.RESULT_STRING = result;
                if (result.StartsWith("OK"))
                {
                    //"OK:mẫu số;ký hiệu-Fkey_Số hóa đơn,"
                    //"OK:01GTKT0/001;VT/19E-A0283806HDA_XXX"
                    int index = result.IndexOf('_');
                    string so_hd = result.Substring(index + 1);
                    v6Return.SO_HD = so_hd;
                }
                else if (result.StartsWith("ERR:-3"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nCó lỗi trong quá trình lấy chứng thư.";
                }
                else if (result.StartsWith("ERR:-2"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nChứng thư không có privatekey.";
                }
                else if (result.StartsWith("ERR:-1"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nẤn nút hủy khi nhập mã pin của chứng thư.";
                }
                else if (result.StartsWith("ERR:30"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nTạo mới lô hóa đơn lỗi (fkey trùng,…).";
                }
                else if (result.StartsWith("ERR:28"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nThông tin chứng thư chưa có trong hệ thống.";
                }
                else if (result.StartsWith("ERR:27"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nChứng thư chưa đến thời điểm sử dụng.";
                }
                else if (result.StartsWith("ERR:26"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nChứng thư đã hết hạn.";
                }
                else if (result.StartsWith("ERR:24"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nChứng thư truyền lên không đúng với chứng thư công ty đăng ký trên hệ thống";
                }
                else if (result.StartsWith("ERR:23"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nChứng thư truyền lên không đúng định dạng.";
                }
                else if (result.StartsWith("ERR:22"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nCông ty chưa đăng ký thông tin keystore.";
                }
                else if (result.StartsWith("ERR:21"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông tìm thấy công ty trên hệ thống.";
                }
                else if (result.StartsWith("ERR:20"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nTham số mẫu số và ký hiệu truyền vào không hợp lệ.";
                }
                else if (result.StartsWith("ERR:19"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\npattern truyền vào không giống với pattern của hoá đơn cần điều chỉnh/thay thế.";
                }
                else if (result.StartsWith("ERR:10"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nSố lượng hóa đơn truyền vào lớn hơn maxBlockInv.";
                }
                else if (result.StartsWith("ERR:9"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\n???.";
                }
                else if (result.StartsWith("ERR:8"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nHoá đơn đã được điều chỉnh, thay thế.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông tìm thấy chứng thư trong máy. Hãy cắm token.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông còn đủ số hóa đơn cho lô phát hành.";
                }
                else if (result.StartsWith("ERR:5"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nCó lỗi xảy ra.";
                }
                else if (result.StartsWith("ERR:4"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\ntoken hóa đơn sai định dạng.";
                }
                else if (result.StartsWith("ERR:3"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nĐịnh dạng file xml hóa đơn không đúng.";
                }
                else if (result.StartsWith("ERR:2"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông tồn tại hoá đơn cần thay thế/điều chỉnh.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông có quyền truy cập webservice.";
                }
                else if (result.StartsWith("ERR:0"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nLỗi Fkey đã tồn tại.";
                }
                else
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "???";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
                v6Return.RESULT_ERROR_MESSAGE = result;
            }
            Logger.WriteToLog("Program.PublishInvWithToken " + result);
            return result;
        }


        /// <summary>
        /// Lấy số hóa đơn từ chuỗi OK:ký hiệu;mẫu số-fkey_Số hóa đơn.
        /// </summary>
        /// <param name="result_string">OK:01GTKT0/001;VT/19E-A0283806HDA_XXX</param>
        /// <returns></returns>
        private static string GetSoHoaDon_Dll(string result_string)
        {
            string result = "";
            try
            {
                int _index = result_string.IndexOf("_", StringComparison.InvariantCulture);
                if (_index < 5) return null;
                result = result_string.Substring(_index + 1, result_string.Length - _index - 1);
                if (result.EndsWith(",")) result = result.Substring(0, result.Length - 1);
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }
        #endregion ==== VNPT_TOKEN ====

        #region ==== VIETTEL ====

        private static string EXECUTE_VIETTEL(PostManagerParams paras)
        {
            if (_version == "V2") return EXECUTE_VIETTEL_V2CALL(paras);
            else if (_version == "V45" || _version == "V45I")
            {
                paras.Version = _version;
                return EXECUTE_VIETTEL_V2_45(paras);
            }

            string result = "";
            paras.Result = new PM_Result();
            V6Return rd = new V6Return();
            paras.Result.V6ReturnValues = rd;

            try
            {
                string jsonBody = "";
                //var _V6Http = new ViettelWS(_baseUrl, _username, _password);
                ViettelWS viettel_ws = new ViettelWS(_baseUrl, _username, _password, _codetax);

                if (paras.Mode == "TestView")
                {
                    var xml = ReadData_Viettel(paras);
                    result = xml;
                    paras.Result.ResultString = xml;
                }
                else if (paras.Mode == "E_G1") // Gạch nợ
                {
                    rd.RESULT_ERROR_MESSAGE = V6Text.NotSupported;
                }
                else if (paras.Mode == "E_H1") // Hủy hóa đơn
                {
                    
                    DataRow row0 = am_table.Rows[0];
                    var item = generalInvoiceInfoConfig["invoiceIssuedDate"];
                    string strIssueDate = ((DateTime)GetValue(row0, item)).ToString("yyyyMMddHHmmss");
                    string additionalReferenceDesc = paras.AM_data["STT_REC"].ToString();
                    paras.InvoiceNo = paras.AM_data["SO_SERI"].ToString().Trim() + paras.AM_data["SO_CT"].ToString().Trim();
                    result = viettel_ws.CancelTransactionInvoice(_codetax, paras.InvoiceNo, strIssueDate, additionalReferenceDesc, strIssueDate);
                    
                }
                else if (paras.Mode == "E_T1")
                {
                    jsonBody = ReadData_Viettel(paras);
                    result = viettel_ws.POST_REPLACE(_createInvoiceUrl, jsonBody);
                    paras.Result.V6ReturnValues.NGAY_CT_VIETTEL = ngay_ct_viettel;
                }
                else if (paras.Mode.StartsWith("M"))
                {
                    generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                    {
                        Field = "adjustmentType",
                        Value = "1",
                    };
                    Guid new_uid = Guid.NewGuid();
                    if (paras.Mode == "MG")
                    {
                        generalInvoiceInfoConfig["transactionUuid"] = new ConfigLine
                        {
                            Field = "transactionUuid",
                            Value = "" + new_uid,
                        };
                    }

                    if (string.IsNullOrEmpty(_SERIAL_CERT))
                    {
                        jsonBody = ReadData_Viettel(paras);
                        if (paras.Key_Down == "F4" || paras.Key_Down == "F6")
                        {
                            result = viettel_ws.POST_DRAFT(_codetax, jsonBody);
                        }
                        else
                        {
                            result = viettel_ws.POST_NEW(jsonBody);
                        }
                    }
                    else // Ký số client. /InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/{supplierTaxCode}
                    {
                        generalInvoiceInfoConfig["certificateSerial"] = new ConfigLine
                        {
                            Field = "certificateSerial",
                            Value = _SERIAL_CERT,
                        };
                        jsonBody = ReadData_Viettel(paras);

                        if (paras.Key_Down == "F4" || paras.Key_Down == "F6")
                        {
                            result = viettel_ws.POST_DRAFT(_codetax, jsonBody);
                        }
                        else
                        {
                            string templateCode = generalInvoiceInfoConfig["templateCode"].Value;
                            result = viettel_ws.CreateInvoiceUsbTokenGetHash_Sign(jsonBody, templateCode, _SERIAL_CERT);
                        }
                    }
                    paras.Result.V6ReturnValues.NGAY_CT_VIETTEL = ngay_ct_viettel;
                }
                else if (paras.Mode.StartsWith("S"))
                {
                    if (paras.Mode.EndsWith("3"))
                    {
                        generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                        {
                            Field = "adjustmentType",
                            Value = "3",
                        };
                    }
                    else if (paras.Mode.EndsWith("5"))
                    {
                        generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                        {
                            Field = "adjustmentType",
                            Value = "5",
                        };
                    }
                    else
                    {
                        generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                        {
                            Field = "adjustmentType",
                            Value = "3",
                        };
                    }

                    jsonBody = ReadData_Viettel(paras);
                    //File.Create(flagFileName1).Close();
                    result = viettel_ws.POST_EDIT(jsonBody);
                    paras.Result.V6ReturnValues.NGAY_CT_VIETTEL = ngay_ct_viettel;
                }

                //Phân tích result
                paras.Result.V6ReturnValues.RESULT_STRING = result;
                paras.Result.V6ReturnValues.NGAY_CT_VIETTEL = ngay_ct_viettel;
                string message = "";
                try
                {
                    VIETTEL_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<VIETTEL_CreateInvoiceResponse>(result);
                    paras.Result.V6ReturnValues.RESULT_OBJECT = responseObject;
                    if (!string.IsNullOrEmpty(responseObject.description))
                    {
                        message += " " + responseObject.description;
                    }

                    if (responseObject.result != null && !string.IsNullOrEmpty(responseObject.result.invoiceNo))
                    {
                        paras.Result.V6ReturnValues.SO_HD = responseObject.result.invoiceNo;
                        paras.Result.V6ReturnValues.ID = responseObject.result.transactionID;
                        paras.Result.V6ReturnValues.SECRET_CODE = responseObject.result.reservationCode;
                        message += " " + responseObject.result.invoiceNo;
                        
                    }
                    else if (responseObject.errorCode == null)
                    {
                        paras.Result.V6ReturnValues.SO_HD = paras.InvoiceNo;
                        paras.Result.V6ReturnValues.RESULT_MESSAGE = responseObject.description;
                    }
                    else
                    {
                        paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = responseObject.errorCode + ":" + responseObject.description;
                    }
                }
                catch (Exception ex)
                {
                    paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = "CONVERT EXCEPTION: " +  ex.Message;
                    Logger.WriteToLog("EXECUTE_VIETTEL ConverResultObjectException: " + paras.Fkey_hd + ex.Message);
                    message = "Kết quả:";
                }
                result = message + "\n" + result;
            }
            catch (Exception ex)
            {
                paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = "WS EXCEPTION: " + ex.Message;
                V6ControlFormHelper.WriteExLog("PostManager.EXECUTE_VIETTEL", ex);
            }

            return result;
        }

        private static string EXECUTE_VIETTEL_V2_45(PostManagerParams paras)
        {
            string result = "";
            paras.Result = new PM_Result();
            V6Return rd = new V6Return();
            paras.Result.V6ReturnValues = rd;

            try
            {
                string jsonBody = "";
                if (viettel_V2WS == null) viettel_V2WS = new ViettelV2WS(_baseUrl, _username, _password, _codetax);

                if (paras.Mode == "TestView")
                {
                    if (!string.IsNullOrEmpty(_SERIAL_CERT))
                    {
                        generalInvoiceInfoConfig["certificateSerial"] = new ConfigLine
                        {
                            Field = "certificateSerial",
                            Value = _SERIAL_CERT,
                        };
                    }
                    
                    jsonBody = ReadData_Viettel(paras);
                    result = jsonBody;
                    paras.Result.ResultString = jsonBody;
                }
                else if (paras.Mode == "TestView_Shift")
                {
                    string getPattern = paras.Pattern;
                    if (string.IsNullOrEmpty(getPattern))
                    {
                        jsonBody = ReadData_Viettel(paras);
                        getPattern = __pattern;
                    }
                    result = viettel_V2WS.GetMetaDataDefine(getPattern, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "E_G1") // Gạch nợ
                {
                    rd.RESULT_ERROR_MESSAGE = V6Text.NotSupported;
                }
                else if (paras.Mode == "E_H1") // Hủy hóa đơn
                {
                    DataRow row0 = am_table.Rows[0];
                    var item = generalInvoiceInfoConfig["invoiceIssuedDate"];
                    string strIssueDate = ((DateTime)GetValue(row0, item)).ToString("yyyyMMddHHmmss");
                    if (_version == "V45I")
                    {
                        // V45 yyyyMMddHHmmss nhưng khi lên V45I đã thay đổi.
                        strIssueDate = V6JsonConverter.ObjectToJson((DateTime)GetValue(row0, item), _datetype);
                    }
                    string additionalReferenceDesc = paras.AM_data["STT_REC"].ToString();
                    paras.InvoiceNo = paras.AM_data["SO_SERI"].ToString().Trim() + paras.AM_data["SO_CT"].ToString().Trim();
                    result = viettel_V2WS.CancelTransactionInvoice(_codetax, paras.InvoiceNo,
                        strIssueDate, additionalReferenceDesc, strIssueDate, out paras.Result.V6ReturnValues);

                }
                else if (paras.Mode == "E_T1")
                {
                    jsonBody = ReadData_Viettel(paras);
                    result = viettel_V2WS.POST_REPLACE(jsonBody, _version == "V45I", out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("M"))
                {
                    generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                    {
                        Field = "adjustmentType",
                        Value = "1",
                    };
                    Guid new_uid = Guid.NewGuid();
                    if (paras.Mode == "MG")
                    {
                        generalInvoiceInfoConfig["transactionUuid"] = new ConfigLine
                        {
                            Field = "transactionUuid",
                            Value = "" + new_uid,
                        };
                    }

                    if (string.IsNullOrEmpty(_SERIAL_CERT))
                    {
                        jsonBody = ReadData_Viettel(paras);

                        if (paras.Key_Down == "F4" || paras.Key_Down == "F6")
                        {
                            result = viettel_V2WS.POST_DRAFT(jsonBody, out paras.Result.V6ReturnValues);
                        }
                        else
                        {
                            result = viettel_V2WS.POST_CREATE_INVOICE(jsonBody, paras.Version == "V45I", out paras.Result.V6ReturnValues);
                        }
                    }
                    else // Ký số client. /InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/{supplierTaxCode}
                    {
                        generalInvoiceInfoConfig["certificateSerial"] = new ConfigLine
                        {
                            Field = "certificateSerial",
                            Value = _SERIAL_CERT,
                        };
                        jsonBody = ReadData_Viettel(paras);

                        if (paras.Key_Down == "F4" || paras.Key_Down == "F6")
                        {
                            result = viettel_V2WS.POST_DRAFT(jsonBody, out paras.Result.V6ReturnValues);
                        }
                        else
                        {
                            string templateCode = generalInvoiceInfoConfig["templateCode"].Value;
                            result = viettel_V2WS.CreateInvoiceUsbTokenGetHash_Sign(jsonBody, templateCode, _SERIAL_CERT, out paras.Result.V6ReturnValues);
                        }
                    }

                    // Nếu error null mà ko có so_hd thì chạy hàm lấy thông tin.
                    if (paras.Key_Down == "F9" && string.IsNullOrEmpty(paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE)
                        && string.IsNullOrEmpty(paras.Result.V6ReturnValues.SO_HD))
                    {
                        Thread.Sleep(10000); // Chờ 10 giây.
                        string oldTransactionID = paras.Result.V6ReturnValues.ID;
                        viettel_V2WS.SearchInvoiceByTransactionUuid(_codetax, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                        paras.Result.V6ReturnValues.ID = oldTransactionID;
                    }

                    paras.Result.V6ReturnValues.NGAY_CT_VIETTEL = ngay_ct_viettel;
                }
                else if (paras.Mode.StartsWith("E_S")) // S S1(tiền) S2(thông tin)
                {
                    jsonBody = ReadData_Viettel(paras);
                    result = viettel_V2WS.POST_EDIT(jsonBody, out paras.Result.V6ReturnValues);
                    paras.Result.V6ReturnValues.NGAY_CT_VIETTEL = ngay_ct_viettel;
                }

                //Phân tích result ra câu thông báo.
                //paras.Result.V6ReturnValues.RESULT_STRING = result;
                paras.Result.V6ReturnValues.NGAY_CT_VIETTEL = ngay_ct_viettel;
                string message = "";
                try
                {
                    //CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(result);
                    //paras.Result.V6ReturnValues.RESULT_OBJECT = responseObject;
                    if (!string.IsNullOrEmpty(paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE))
                    {
                        message += " " + paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE;
                    }
                    else if (!string.IsNullOrEmpty(paras.Result.V6ReturnValues.SO_HD))
                    {
                        message += " " + paras.Result.V6ReturnValues.SO_HD;
                    }
                }
                catch (Exception ex)
                {
                    paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = "CONVERT EXCEPTION: " + ex.Message;
                    Logger.WriteToLog("EXECUTE_VIETTEL ConverResultObjectException: " + paras.Fkey_hd + ex.Message);
                    message = "Kết quả:";
                }
                result = message + "\n" + result;
            }
            catch (Exception ex)
            {
                paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = "WS EXCEPTION: " + ex.Message;
                V6ControlFormHelper.WriteExLog("PostManager.EXECUTE_VIETTEL", ex);
            }

            return result;
        }
        
        private static string EXECUTE_VIETTEL_V2CALL(PostManagerParams paras)
        {
            string result = "";
            paras.Result = new PM_Result();
            V6Return rd = new V6Return();
            paras.Result.V6ReturnValues = rd;

            try
            {
                string jsonBody = "";
                //ViettelWS viettel_ws = new ViettelWS(_baseUrl, _username, _password, _codetax); // Thay thế WS bằng call Process
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = "ViettelV2",
                        FileName = "ViettelV2\\V6ThuePostViettelV2.exe",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                if (paras.Mode == "TestView")
                {
                    var xml = ReadData_Viettel(paras);
                    result = xml;
                    paras.Result.ResultString = xml;
                }
                else if (paras.Mode == "E_G1") // Gạch nợ
                {
                    rd.RESULT_ERROR_MESSAGE = V6Text.NotSupported;
                }
                else if (paras.Mode == "E_H1") // Hủy hóa đơn
                {
                    // V6ThuePostViettelV2.exe H_JSON "V6ThuePost.xml" "AB/19E0000341" "27/11/2019" "stt_rec"
                    DataRow row0 = am_table.Rows[0];
                    var item = generalInvoiceInfoConfig["invoiceIssuedDate"];
                    string strIssueDate = ((DateTime)GetValue(row0, item)).ToString("yyyyMMddHHmmss");
                    string additionalReferenceDesc = paras.AM_data["STT_REC"].ToString();
                    paras.InvoiceNo = paras.AM_data["SO_SERI"].ToString().Trim() + paras.AM_data["SO_CT"].ToString().Trim();
                    //result = viettel_ws.CancelTransactionInvoice(_codetax, paras.InvoiceNo, strIssueDate, additionalReferenceDesc, strIssueDate);
                    //V6BusinessHelper.WriteTextFile("ViettelV2\\tprint_soa.json", string.Format("{0};{1};{2};{3};{4}", _codetax, paras.InvoiceNo, strIssueDate, additionalReferenceDesc, strIssueDate));
                    process.StartInfo.Arguments = string.Format("H_JSON V6ThuePost.xml {0} {1} {2}",
                        paras.InvoiceNo, ObjectAndString.ObjectToString((DateTime)GetValue(row0, item), "dd/MM/yyyy"), additionalReferenceDesc);
                    process.Start();
                    string process_result = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    // Phân tích Result tại đây.
                    paras.Result.V6ReturnValues = GetV6ReturnFromCallExe(process_result);
                    result = paras.Result.V6ReturnValues.RESULT_STRING;
                    VIETTEL_CreateInvoiceResponseV2 resultObject = JsonConvert.DeserializeObject<VIETTEL_CreateInvoiceResponseV2>(result);
                    paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = resultObject.errorCode + resultObject.message;
                }
                else if (paras.Mode == "E_T1")
                {
                    jsonBody = ReadData_Viettel(paras);
                    V6BusinessHelper.WriteTextFile("ViettelV2\\tprint_soa.json", jsonBody);
                    process.StartInfo.Arguments = "E_T1_JSON V6ThuePost.xml tprint_soa.json";
                    process.Start();
                    result = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    //result = viettel_ws.POST_REPLACE(_createInvoiceUrl, jsonBody);
                }
                else if (paras.Mode.StartsWith("M"))
                {
                    generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                    {
                        Field = "adjustmentType",
                        Value = "1",
                    };
                    
                    if (string.IsNullOrEmpty(_SERIAL_CERT))
                    {
                        jsonBody = ReadData_Viettel(paras);
                        V6BusinessHelper.WriteTextFile("ViettelV2\\tprint_soa.json", jsonBody);

                        if (paras.Key_Down == "F4" || paras.Key_Down == "F6")
                        {
                            process.StartInfo.Arguments = "M_F4_JSON V6ThuePost.xml tprint_soa.json";
                        }
                        else
                        {
                            process.StartInfo.Arguments = "M_JSON V6ThuePost.xml tprint_soa.json";
                        }

                        process.Start();
                        string process_result = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();
                        // Phân tích Result tại đây.
                        paras.Result.V6ReturnValues = GetV6ReturnFromCallExe(process_result);
                        result = paras.Result.V6ReturnValues.RESULT_STRING;
                        //paras.Form.ShowInfoMessage(result);
                    }
                    else // Ký số client. /InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/{supplierTaxCode}
                    {
                        generalInvoiceInfoConfig["certificateSerial"] = new ConfigLine
                        {
                            Field = "certificateSerial",
                            Value = _SERIAL_CERT,
                        };
                        jsonBody = ReadData_Viettel(paras);
                        string templateCode = generalInvoiceInfoConfig["templateCode"].Value;
                        //result = viettel_ws.CreateInvoiceUsbTokenGetHash_Sign(jsonBody, templateCode, _SERIAL_CERT);
                        V6BusinessHelper.WriteTextFile("ViettelV2\\tprint_soa.json", jsonBody + "<;>" + templateCode);
                        process.StartInfo.Arguments = "M_TOKEN_JSON V6ThuePost.xml tprint_soa.json";
                        process.Start();
                        string process_result = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();
                        // Phân tích Result tại đây.
                        paras.Result.V6ReturnValues = GetV6ReturnFromCallExe(process_result);
                        result = paras.Result.V6ReturnValues.RESULT_STRING;
                    }

                    paras.Result.V6ReturnValues.NGAY_CT_VIETTEL = ngay_ct_viettel;
                }
                else if (paras.Mode.StartsWith("S"))
                {
                    if (paras.Mode.EndsWith("3"))
                    {
                        generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                        {
                            Field = "adjustmentType",
                            Value = "3",
                        };
                    }
                    else if (paras.Mode.EndsWith("5"))
                    {
                        generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                        {
                            Field = "adjustmentType",
                            Value = "5",
                        };
                    }
                    else
                    {
                        generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                        {
                            Field = "adjustmentType",
                            Value = "3",
                        };
                    }

                    jsonBody = ReadData_Viettel(paras);
                    V6BusinessHelper.WriteTextFile("ViettelV2\\tprint_soa.json", jsonBody);
                    process.StartInfo.Arguments = "S_JSON V6ThuePost.xml tprint_soa.json";
                    process.Start();
                    string process_result = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    // Phân tích Result tại đây.
                    paras.Result.V6ReturnValues = GetV6ReturnFromCallExe(process_result);
                    result = paras.Result.V6ReturnValues.RESULT_STRING;
                    paras.Result.V6ReturnValues.NGAY_CT_VIETTEL = ngay_ct_viettel;
                }

                //Phân tích result
                paras.Result.V6ReturnValues.NGAY_CT_VIETTEL = ngay_ct_viettel;
                paras.Result.V6ReturnValues.RESULT_STRING = result;
                string message = "";
                try
                {
                    VIETTEL_CreateInvoiceResponseV2 responseObject = JsonConvert.DeserializeObject<VIETTEL_CreateInvoiceResponseV2>(result);
                    paras.Result.V6ReturnValues.RESULT_OBJECT = responseObject;
                    if (!string.IsNullOrEmpty(responseObject.description))
                    {
                        message += " " + responseObject.description;
                    }

                    if (responseObject.result != null && !string.IsNullOrEmpty(responseObject.result.invoiceNo))
                    {
                        paras.Result.V6ReturnValues.SO_HD = responseObject.result.invoiceNo;
                        paras.Result.V6ReturnValues.ID = responseObject.result.transactionID;
                        paras.Result.V6ReturnValues.SECRET_CODE = responseObject.result.reservationCode;
                        message += " " + responseObject.result.invoiceNo;
                        
                    }
                    else if (responseObject.code == 400)
                    {
                        paras.Result.V6ReturnValues.SO_HD = paras.InvoiceNo;
                        paras.Result.V6ReturnValues.RESULT_MESSAGE = result;
                    }
                    else
                    {
                        paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = responseObject.errorCode + ":" + responseObject.description;
                        if (string.IsNullOrEmpty(responseObject.errorCode + "" + responseObject.description) && string.IsNullOrEmpty(paras.Result.V6ReturnValues.SO_HD))
                        {
                            paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = "Không có số Hóa Đơn phản hồi.";
                        }
                        else
                        {
                            paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = "Lỗi.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = "CONVERT EXCEPTION: " +  ex.Message;
                    Logger.WriteToLog("EXECUTE_VIETTEL ConverResultObjectException: " + paras.Fkey_hd + ex.Message);
                    message = "Kết quả:";
                }
                result = message + "\n" + result;
            }
            catch (Exception ex)
            {
                paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = "WS EXCEPTION: " + ex.Message;
                V6ControlFormHelper.WriteExLog("PostManager.EXECUTE_VIETTEL", ex);
            }

            return result;
        }


        public static string ReadData_Viettel(PostManagerParams paras)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(_datetype)) _datetype = "VIETTEL";
                DataRow row0 = am_table.Rows[0];
                var postObject = new PostObjectViettel();

                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    postObject.generalInvoiceInfo[item.Key] = GetValue(row0, item.Value);
                }

                // Giữ lại giá trị ngày ct viettel invoiceIssuedDate
                if (_datetype.StartsWith("VIETTELNOW"))
                {
                    int minutes = -3;
                    if (_datetype.Length > 10)
                    {
                        string s_minutes = _datetype.Substring(10);
                        if (s_minutes.StartsWith("+")) minutes = ObjectAndString.ObjectToInt(s_minutes.Substring(1));
                        else minutes = ObjectAndString.ObjectToInt(s_minutes);
                    }
                    var sv_date = V6BusinessHelper.GetServerDateTime();
                    paras.Result.MoreInfos["SERVER_DATE"] = sv_date;
                    var sv_date_10 = sv_date.AddMinutes(minutes);
                    sv_date_10 = sv_date_10.AddSeconds(-sv_date_10.Second);
                    sv_date_10 = sv_date_10.AddMilliseconds(-sv_date_10.Millisecond);
                    paras.Result.MoreInfos["SEND_DATE"] = sv_date_10;
                    postObject.generalInvoiceInfo["invoiceIssuedDate"] = sv_date_10;
                }
                
                ngay_ct_viettel = V6JsonConverter.ObjectToJson(postObject.generalInvoiceInfo["invoiceIssuedDate"], _datetype);
                paras.Result.MoreInfos["NGAY_CT_VIETTEL"] = ngay_ct_viettel;
                if (!string.IsNullOrEmpty(pattern_field) && am_table.Columns.Contains(pattern_field))
                    __pattern = row0[pattern_field].ToString().Trim();
                if (!string.IsNullOrEmpty(seri_field) && am_table.Columns.Contains(seri_field))
                    __serial = row0[seri_field].ToString().Trim();
                

                if (paras.Mode == "E_T1")
                {
                    //Lập hóa đơn thay thế:
                    //adjustmentType = ‘3’
                    postObject.generalInvoiceInfo["adjustmentType"] = "3";
                    //Các trường dữ liệu về hóa đơn gốc là bắt buộc
                    //originalInvoiceId
                    postObject.generalInvoiceInfo["originalInvoiceId"] = paras.AM_old["SO_SERI"].ToString().Trim() + paras.AM_old["SO_CT"].ToString().Trim();//  paras.Fkey_hd_tt;// .AM_old["FKEY_HD_TT"].ToString().Trim();  // AA/17E0003470
                    //originalInvoiceIssueDate
                    postObject.generalInvoiceInfo["originalInvoiceIssueDate"] = ObjectAndString.ObjectToInt64(paras.Partner_infor_dic["NGAY_CT"]);

                    //Thông tin về biên bản đính kèm hóa đơn gốc:
                    //additionalReferenceDate
                    postObject.generalInvoiceInfo["additionalReferenceDate"] = paras.AM_data["NGAY_CT"];
                    //additionalReferenceDesc
                    postObject.generalInvoiceInfo["additionalReferenceDesc"] = paras.AM_data["GHI_CHU_TT"];
                }
                else if (paras.Mode == "E_S" || paras.Mode == "E_S1" || paras.Mode == "E_S2")
                {
                    // Hóa đơn điều chỉnh.
                    postObject.generalInvoiceInfo["adjustmentType"] = "5";
                    postObject.generalInvoiceInfo["adjustmentInvoiceType"] = paras.Mode.EndsWith("S2") ? "2" : "1"; // 1 tiền, 2 thông tin
                    postObject.generalInvoiceInfo["originalInvoiceId"] = paras.Partner_infor_dic["SO_HD"];
                    postObject.generalInvoiceInfo["originalInvoiceIssueDate"] = ObjectAndString.ObjectToInt64(paras.Partner_infor_dic["NGAY_CT"]);
                    //Thời gian phát sinh văn bản thỏa thuận giữa bên mua và bên bán, bắt buộc khi lập hóa đơn thay thế, hóa đơn điều chỉnh
                    postObject.generalInvoiceInfo["additionalReferenceDate"] = paras.AM_data["NGAY_CT"];
                    //Thông tin tham khảo nếu có kèm theo của hóa đơn: văn bản thỏa thuận giữa bên mua và bên bán về việc thay thế, điều chỉnh hóa đơn
                    postObject.generalInvoiceInfo["additionalReferenceDesc"] = paras.AM_data["GHI_CHU_TT"];
                }
                

                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    postObject.buyerInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> sellerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
                }
                if (metadataConfig != null)
                {
                    foreach (KeyValuePair<string, ConfigLine> metaItem in metadataConfig)
                    {
                        Dictionary<string, object> metadata = new Dictionary<string, object>();
                        metadata["invoiceCustomFieldId"] = ObjectAndString.ObjectToInt(metaItem.Value.SL_TD1);
                        metadata["keyTag"] = metaItem.Key;
                        metadata["valueType"] = metaItem.Value.DataType; // text, number, date
                        if (metaItem.Value.DataType.ToLower() == "date")
                        {
                            metadata["dateValue"] = GetValue(row0, metaItem.Value);
                        }
                        else if (metaItem.Value.DataType.ToLower() == "number")
                        {
                            metadata["numberValue"] = GetValue(row0, metaItem.Value);
                        }
                        else if (metaItem.Value.DataType.ToUpper() == "N2C0VNDE")
                        {
                            string ma_nt = row0["MA_NT"].ToString().Trim().ToUpper();
                            if (ma_nt != "VND")
                            {
                                metadata["stringValue"] = ObjectAndString.ObjectToString(GetValue(row0, metaItem.Value));
                                metadata["valueType"] = "text";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            metadata["stringValue"] = ObjectAndString.ObjectToString(metaItem.Value.GetValue(row0)); // cách GetValue mới.
                            //metaItem.Value.DataType = "text";
                            metadata["valueType"] = "text";
                        }
                        metadata["keyLabel"] = ObjectAndString.ObjectToString(metaItem.Value.MA_TD2);
                        metadata["isRequired"] = ObjectAndString.ObjectToBool(metaItem.Value.SL_TD2);
                        metadata["isSeller"] = ObjectAndString.ObjectToBool(metaItem.Value.SL_TD3);

   //{
   //   "invoiceCustomFieldId": 1135,
   //   "keyTag": "dueDate",
   //   "valueType": "date",
   //   "dateValue": 1544115600000,
   //   "keyLabel": "Hạn thanh toán",
   //   "isRequired": false,
   //   "isSeller": false
   // },
                        postObject.metadata.Add(metadata);
                    }
                }
                
                //private static Dictionary<string, XmlLine> paymentsConfig = null;
                Dictionary<string, object> payment = new Dictionary<string, object>();
                foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                {
                    payment[item.Key] = GetValue(row0, item.Value);
                }
                postObject.payments.Add(payment);//One payment only!
                //private static Dictionary<string, XmlLine> itemInfoConfig = null;
                foreach (DataRow row in ad_table.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;
                    string NOGEN_F = "";
                    if (row.Table.Columns.Contains("NOGEN_F")) NOGEN_F = ";" + row["NOGEN_F"].ToString().Trim().ToUpper() + ";";
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        var cell = GetValue(row, item.Value);
                        if (item.Value.NoGen && NOGEN_F.Contains(";" + item.Value.FieldV6.ToUpper() + ";"))
                        {
                            // NOGEN
                        }
                        else
                        {
                            rowData[item.Key] = cell;
                        }
                    }
                    postObject.itemInfo.Add(rowData);
                }
                //private static Dictionary<string, XmlLine> summarizeInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    postObject.summarizeInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> taxBreakdownsConfig = null;
                //Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                //foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                //{
                //    taxBreakdown[item.Key] = GetValue(row0, item.Value);
                //}
                //postObject.taxBreakdowns.Add(taxBreakdown);//One only!
                if (taxBreakdownsConfig != null && ad3_table != null && ad3_table.Rows.Count > 0)
                {
                    foreach (DataRow ad3_row in ad3_table.Rows)
                    {
                        Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                        foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                        {
                            taxBreakdown[item.Key] = GetValue(ad3_row, item.Value);
                        }
                        postObject.taxBreakdowns.Add(taxBreakdown);
                    }
                }

                result = postObject.ToJson(_datetype);
                if (_write_log)
                {
                    string stt_rec = row0["STT_REC"].ToString();
                    string file = Path.Combine(Application.StartupPath, stt_rec + ".json");
                    File.WriteAllText(file, result);
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog("PostManager.ReadData", ex);
            }
            return result;
        }
        
        /// <summary>
        /// Trả về đường dẫn file pdf.
        /// </summary>
        /// <param name="paras"></param>
        /// <returns>Trả về đường dẫn file pdf.</returns>
        public static string ViettelDownloadInvoicePDF(PostManagerParams paras)
        {
            if (_version == "V2")
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = "ViettelV2",
                        FileName = "ViettelV2\\V6ThuePostViettelV2.exe",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                string soseri_soct = paras.Serial + paras.InvoiceNo;//"XL/20E0000019"
                string templateCode = paras.Pattern; // "01GTKT0/001" 
                string uid = paras.Fkey_hd;//"bf0a819a-dd5f-4446-8850-bc81263beb04"
                
                if (paras.Mode == "1") // Thể hiện V6ThuePostViettelV2.exe P "V6ThuePost.xml" "XL/20E0000019" "01GTKT0/001" "bf0a819a-dd5f-4446-8850-bc81263beb04"
                {
                    process.StartInfo.Arguments = string.Format("P_JSON V6ThuePost.xml \"{0}\" \"{1}\" \"{2}\"", soseri_soct, templateCode, uid);
                }
                else // V6ThuePostViettelV2.exe P2 "V6ThuePost.xml" "XL/20E0000019" "template" "uid"
                {
                    process.StartInfo.Arguments = string.Format("P2_JSON V6ThuePost.xml \"{0}\" \"{1}\" \"{2}\"", soseri_soct, templateCode, uid);
                }

                process.Start();
                string process_result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                // Phân tích Result tại đây.
                paras.Result.V6ReturnValues = GetV6ReturnFromCallExe(process_result);
                return paras.Result.V6ReturnValues.PATH;
                return process_result;
            }
            else if (_version == "V45" || _version == "V45I")
            {
                if (viettel_V2WS == null) viettel_V2WS = new ViettelV2WS(_baseUrl, _username, _password, _codetax);

                if (paras.Mode == "1") // Mode Thể hiện
                    return viettel_V2WS.DownloadInvoicePDF(_codetax, paras.Partner_infor_dic["SO_HD"], paras.Pattern, paras.Fkey_hd,
                        V6Setting.V6SoftLocalAppData_Directory, V6Setting.WriteExtraLog, out paras.Result.V6ReturnValues);
                //string strIssueDate = paras.InvoiceDate.ToString("yyyyMMddHHmmss"); // V1 dùng không thống nhất ???
                //string strIssueDate_Viettel = V6JsonConverter.ObjectToJson(paras.InvoiceDate, _datetype);

                string strIssueDate_Viettel = "";
                if (!paras.Partner_infor_dic.ContainsKey("NGAY_CT") || string.IsNullOrEmpty(paras.Partner_infor_dic["NGAY_CT"]))
                {
                    throw new Exception(V6Text.CheckData + "\nPART_INFOR[\"NGAY_CT\"]");
                }
                else
                {
                    strIssueDate_Viettel = paras.Partner_infor_dic["NGAY_CT"];
                }

                // Mode = 2 ; chuyen doi
                string downloaded_file = viettel_V2WS.DownloadInvoicePDFexchange(_codetax, paras.Partner_infor_dic["SO_HD"], paras.Fkey_hd, strIssueDate_Viettel,
                    V6Setting.V6SoftLocalAppData_Directory, V6Setting.WriteExtraLog, out paras.Result.V6ReturnValues);

                if (string.IsNullOrEmpty(paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE))
                {
                    return downloaded_file;
                }
                else if (paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE.Contains("NOT_FOUND_DATA"))
                {
                    // Lay lai thong tin issiueDate, update? download again.
                    viettel_V2WS.SearchInvoiceByTransactionUuid(_codetax, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    if (string.IsNullOrEmpty(paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE))
                    {
                        downloaded_file = viettel_V2WS.DownloadInvoicePDFexchange(_codetax, paras.Partner_infor_dic["SO_HD"], paras.Fkey_hd, paras.Result.V6ReturnValues.NGAY_CT_VIETTEL,
                            V6Setting.V6SoftLocalAppData_Directory, V6Setting.WriteExtraLog, out paras.Result.V6ReturnValues);
                    }

                    return downloaded_file; // ket qua cu.
                }
                else
                {
                    return downloaded_file; // ket qua cu.
                }
            }
            else
            {
                ViettelWS viettel_ws = new ViettelWS(_baseUrl, _username, _password, _codetax);

                if (paras.Mode == "1") // Mode Thể hiện
                    return viettel_ws.DownloadInvoicePDF(_codetax, paras.Serial + paras.InvoiceNo, paras.Pattern,
                        V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues); // !!! Cần update cho giống V2
                string strIssueDate = paras.InvoiceDate.ToString("yyyyMMddHHmmss"); // V1 dùng không thống nhất ???
                return viettel_ws.DownloadInvoicePDFexchange(_codetax, paras.Serial + paras.InvoiceNo, strIssueDate,
                    V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);  // !!! Cần update cho giống V2
            }
        }
        
        /// <summary>
        /// Lấy thông tin, trả về số hóa đơn.
        /// <para>V45 V45I cần paras.Fkey_hd</para>
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static string ViettelDownloadInvoiceInfo(PostManagerParams paras)
        {
            if (_version == "V2")
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = "ViettelV2",
                        FileName = "ViettelV2\\V6ThuePostViettelV2.exe",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                string soseri_soct = paras.Serial + paras.InvoiceNo;//"XL/20E0000019"
                string templateCode = paras.Pattern; // "01GTKT0/001" 
                string uid = paras.Fkey_hd;//"bf0a819a-dd5f-4446-8850-bc81263beb04"
                
                if (paras.Mode == "1") // Thể hiện V6ThuePostViettelV2.exe P "V6ThuePost.xml" "XL/20E0000019" "01GTKT0/001" "bf0a819a-dd5f-4446-8850-bc81263beb04"
                {
                    process.StartInfo.Arguments = string.Format("P_JSON V6ThuePost.xml \"{0}\" \"{1}\" \"{2}\"", soseri_soct, templateCode, uid);
                }
                else // V6ThuePostViettelV2.exe P2 "V6ThuePost.xml" "XL/20E0000019" "template" "uid"
                {
                    process.StartInfo.Arguments = string.Format("P2_JSON V6ThuePost.xml \"{0}\" \"{1}\" \"{2}\"", soseri_soct, templateCode, uid);
                }

                process.Start();
                string process_result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                // Phân tích Result tại đây.
                paras.Result.V6ReturnValues = GetV6ReturnFromCallExe(process_result);
                return paras.Result.V6ReturnValues.PATH;
                return process_result;
            }
            else if (_version == "V45" || _version == "V45I")
            {
                if (viettel_V2WS == null) viettel_V2WS = new ViettelV2WS(_baseUrl, _username, _password, _codetax);
                
                viettel_V2WS.SearchInvoiceByTransactionUuid(_codetax, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                return paras.Result.V6ReturnValues.SO_HD;
            }
            else
            {
                ViettelWS viettel_ws = new ViettelWS(_baseUrl, _username, _password, _codetax);

                if (paras.Mode == "1") // Mode Thể hiện
                    return viettel_ws.DownloadInvoicePDF(_codetax, paras.InvoiceNo, paras.Pattern,
                        V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues); // !!! Cần update cho giống V2
                string strIssueDate = paras.InvoiceDate.ToString("yyyyMMddHHmmss"); // V1 dùng không thống nhất ???
                return viettel_ws.DownloadInvoicePDFexchange(_codetax, paras.InvoiceNo, strIssueDate,
                    V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);  // !!! Cần update cho giống V2
            }
        }
        
        #endregion viettel


        #region ==== SOFTDREAMS ====
        /// <summary>
        /// Copy từ Vnpt, sửa từ từ.
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static string EXECUTE_SOFTDREAMS(PostManagerParams paras)
        {
            string result = "";
            paras.Result = new PM_Result();

            try
            {
                SoftDreamsWS softDreamsWS = new SoftDreamsWS(_baseUrl, _username, _password, _SERIAL_CERT);
                var row0 = am_table.Rows[0];

                if (paras.Mode == "TestView")
                {
                    var invoices = ReadData_SoftDreams(paras.Mode);
                    result = invoices.ToXml();
                    paras.Result.ResultString = result;
                }
                else if (paras.Mode.StartsWith("E_"))
                {
                    if (paras.Mode == "E_G1") // Gạch nợ theo fkey !!!!! SoftDreams không có hàm.
                    {
                        if (string.IsNullOrEmpty(paras.Fkey_hd))
                        {
                            paras.Result.ResultErrorMessage = "Không có Fkey_hd truyền vào.";
                        }
                        else
                        {
                            result = softDreamsWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                        }
                    }
                    else if (paras.Mode == "E_G2") // Gạch nợ // !!!!! SoftDreams không có hàm.
                    {
                        result = softDreamsWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_G3") // Hủy gạch nợ theo fkey !!!!! SoftDreams không có hàm.
                    {
                        result = softDreamsWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_H1")
                    {
                        result = softDreamsWS.CancelInvoice(paras.Fkey_hd, paras.Pattern, paras.Serial);
                    }
                    else if (paras.Mode == "E_S1")
                    {
                        var invoices = ReadData_SoftDreams(paras.Mode);
                        foreach (Inv inv in invoices.Inv)
                        {
                            var adj = inv.ToAdjustInv();
                            result += softDreamsWS.AdjustInvoice(adj, paras.Fkey_hd, paras.Pattern, paras.Serial, true, _signmode, out paras.Result.V6ReturnValues);
                        }
                    }
                    else if (paras.Mode == "E_T1")
                    {
                        var invoices = ReadData_SoftDreams(paras.Mode);
                        var inv = invoices.Inv[0].ToReplaceInv();
                        result = softDreamsWS.ReplaceInvoice(inv, paras.Fkey_hd, paras.Pattern, paras.Serial, true, _signmode, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode.StartsWith("M")) // MSHDT//Mới Sửa Hủy ĐiềuChỉnh(S) ThayThế
                {
                    Invoices invoices = ReadData_SoftDreams(paras.Mode.Substring(0, 1));
                    bool issue = true;
                    if (paras.Key_Down == "F4" || paras.Key_Down == "F6")
                    {
                        issue = false;
                        var no_cdata_fields = ObjectAndString.SplitString(_noCdata);
                        V6XmlConverter.SetConfigNotCdataField(no_cdata_fields);
                        result = softDreamsWS.ImportInvoicesNoIssue(invoices, __pattern, __serial, _signmode, out paras.Result.V6ReturnValues);
                    }
                    else
                    {
                        StartAutoInputTokenPassword();
                        result = softDreamsWS.ImportInvoices(invoices, __pattern, __serial, issue, _signmode, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode.ToLower() == "DownloadInvFkeyNoPay".ToLower())
                {
                    fkeyA = paras.Fkey_hd;

                    string invXml = softDreamsWS.DownloadInvFkeyNoPay(fkeyA, __pattern, __serial, out paras.Result.V6ReturnValues);
                    paras.Result.InvoiceNo = paras.Result.InvoiceNo;
                    result += paras.Result.InvoiceNo;
                }
                else if (paras.Mode == "S")
                {
                    var invoices = ReadData_SoftDreams(paras.Mode);
                    foreach (Inv inv in invoices.Inv)
                    {
                        var adj = inv.ToAdjustInv();
                        result += softDreamsWS.AdjustInvoice(adj, paras.Fkey_hd, paras.Pattern, paras.Serial, true, _signmode, out paras.Result.V6ReturnValues);
                    }
                    
                    //string filePath = Path.Combine(paras.Dir, paras.FileName);
                    //if (filePath.Length > 0 && result.StartsWith("OK"))
                    //{
                    //    if (File.Exists(filePath))
                    //    {
                    //        result += vnptWS.UploadInvAttachmentFkey(fkeyA, filePath);
                    //    }
                    //    else
                    //    {
                    //        result += "Không tồn tại " + filePath;
                    //    }
                    //}
                }
                else if (paras.Mode == "T")
                {
                    var invoices = ReadData_SoftDreams(paras.Mode);
                    var inv = invoices.Inv[0].ToReplaceInv();
                    result = softDreamsWS.ReplaceInvoice(inv, paras.Fkey_hd, paras.Pattern, paras.Serial, true, _signmode, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("G"))
                {
                    if (paras.Mode == "G1") // Gạch nợ theo fkey
                    {
                        result = softDreamsWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    {
                        result = softDreamsWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G3") // Hủy gạch nợ theo fkey
                    {
                        result = softDreamsWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode == "H")
                {
                    result = softDreamsWS.CancelInvoice(paras.Fkey_hd, paras.Pattern, paras.Serial);
                }
                else if (paras.Mode.StartsWith("U"))//U1,U2
                {
                    if (paras.Mode == "U")        // upload file có sẵn, fkey truyền vào
                    {
                        string fkey = paras.Fkey_hd;
                        string file = Path.Combine(paras.Dir, paras.FileName);
                        //vnptWS.UploadInvAttachmentFkey(fkey, file);
                    }
                    else if (paras.Mode == "U1") // upload file có sẵn, fkey tự đọc từ data
                    {
                        //ReadDataXml(arg2);
                        string fkey = paras.Fkey_hd;
                        //vnptWS.UploadInvAttachmentFkey(fkey, fkey + ".xls");
                    }
                    else if (paras.Mode == "U2") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel rồi upload.
                    {
                        string export_file;
                        //ReadDataXml(arg2);
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                        if (export_ok && File.Exists(export_file))
                        {
                            result += "Không có chức năng.";
                            //result += vnptWS.UploadInvAttachmentFkey(fkeyA, export_file);
                        }
                    }
                    else if (paras.Mode == "U3") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel để đó xem.
                    {
                        string export_file;
                        //ReadDataXml(arg2);
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                        if (export_ok && File.Exists(export_file))
                        {
                            result += "\r\nExport ok.";
                        }
                    }
                }
                else if (paras.Mode.StartsWith("E"))
                {
                    if (paras.Mode == "E")
                    {

                    }
                    else if (paras.Mode == "E1")
                    {
                        string export_file;
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);
                        if (export_ok && File.Exists(export_file))
                        {
                            result += "\r\nExport ok.";
                        }
                    }
                    else if (paras.Mode == "E2")  // Xuất PDF bằng RPT
                    {
                        string rptFile = paras.RptFileFull;
                        string saveFile = Path.Combine(paras.Dir, paras.FileName);// arg4;

                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(rptFile);
                        DataSet ds = new DataSet();
                        DataTable data1 = ad_table.Copy();
                        data1.TableName = "DataTable1";
                        DataTable data2 = am_table.Copy();
                        data2.TableName = "DataTable2";
                        ds.Tables.Add(data1);
                        ds.Tables.Add(data2);
                        string tien_bang_chu = MoneyToWords(ObjectAndString.ObjectToDecimal(row0["T_TT"]), "V", "VND");
                        rpt.SetDataSource(ds);
                        rpt.SetParameterValue("SoTienVietBangChu", tien_bang_chu);

                        bool export_ok = false;
                        if (string.IsNullOrEmpty(saveFile))
                        {
                            export_ok = ExportRptToPdf_As(null, rpt, saveFile);
                        }
                        else
                        {
                            export_ok = ExportRptToPdf(null, rpt, saveFile);
                        }

                        if (export_ok)
                        {
                            result += "\r\nExport ok.";
                        }
                        else
                        {
                            result += "\r\nExport fail.";
                        }
                    }
                }
                else
                {
                    result = "ERR: Mode " + paras.Mode + " " + V6Text.NotSupported;
                }


                if (result.StartsWith("ERR"))
                {
                    //error += result;
                    paras.Result.ResultErrorMessage = result;
                }
                else
                {
                    //File.Create(flagFileName2).Close();
                }
            }
            catch (Exception ex)
            {
                result += "ERR:EX\r\n" + ex.Message;
                paras.Result.ResultErrorMessage = ex.Message;
            }
            StopAutoInputTokenPassword();
        End:
            return result;
        }

        #endregion ==== SOFTDREAMS ====


        #region ==== THAI_SON ====
        /// <summary>
        /// Copy từ SoftDreams - Vnpt, sửa từ từ.
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static string EXECUTE_THAI_SON(PostManagerParams paras)
        {
            string result = "";
            paras.Result = new PM_Result();

            try
            {
                ThaiSonWS thaiSonWS = new ThaiSonWS(_baseUrl, _link_Publish_vnpt_thaison, _username, _password, _SERIAL_CERT);
                var row0 = am_table.Rows[0];

                if (paras.Mode == "TestView")
                {
                    var invoices = ReadData_ThaiSon(paras.Mode);
                    result = V6XmlConverter.ClassToXml(invoices);
                    paras.Result.ResultString = result;
                }
                else if (paras.Mode.StartsWith("E_"))
                {
                    if (paras.Mode == "E_G1") // Gạch nợ theo fkey !!!!! SoftDreams không có hàm.
                    {
                        if (string.IsNullOrEmpty(paras.Fkey_hd))
                        {
                            paras.Result.ResultErrorMessage = "Không có Fkey_hd truyền vào.";
                        }
                        else
                        {
                            result = thaiSonWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                        }
                    }
                    else if (paras.Mode == "E_G2") // Gạch nợ // !!!!! SoftDreams không có hàm.
                    {
                        result = thaiSonWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_G3") // Hủy gạch nợ theo fkey !!!!! SoftDreams không có hàm.
                    {
                        result = thaiSonWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_H1")
                    {
                        var hoadon_entity = ReadData_ThaiSon("H");
                        result = thaiSonWS.CancelInvoice((HoaDonHuyEntity)hoadon_entity, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_S1")
                    {
                        var invoice = (HoaDonEntity)ReadData_ThaiSon("S");
                        invoice.SoHoaDonGoc = paras.Fkey_hd_tt;
                        result += thaiSonWS.AdjustInvoice(invoice, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_T1")
                    {
                        var invoice = (HoaDonEntity)ReadData_ThaiSon("T");
                        invoice.SoHoaDonGoc = paras.Fkey_hd_tt;
                        result = thaiSonWS.ReplaceInvoice(invoice, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode.StartsWith("M")) // MSHDT//Mới Sửa Hủy ĐiềuChỉnh(S) ThayThế
                {
                    //Invoices invoices = ReadData_ThaiSon(paras.Mode.Substring(0, 1));
                    //bool issue = true;
                    //if (paras.Key_Down == "F4" || paras.Key_Down == "F6") issue = false;
                    //StartAutoInputTokenPassword();
                    //result = thaiSonWS.ImportInvoices(invoices, __pattern, __serial, issue, _signmode, out paras.Result.V6ReturnValues);

                    var hoadon_entity = ReadData_ThaiSon(paras.Mode.Substring(0, 1));
                    //File.Create(flagFileName1).Close();
                    result = thaiSonWS.XuatHoaDonDienTu((HoaDonEntity)hoadon_entity, out paras.Result.V6ReturnValues);
                    //result = XuatHoaDonDienTu_XML(xml);

                    if (result.StartsWith("OK"))
                    {
                        string filePath = Path.Combine(paras.Dir, paras.FileName);
                        if (paras.Mode.EndsWith("1"))//Gửi file excel có sẵn
                        {
                            if (File.Exists(filePath))
                            {
                                result += "Không có chức năng.";
                                //result += vnptWS.UploadInvAttachmentFkey(fkeyA, filePath);
                            }
                            else
                            {
                                result += "Không tồn tại " + filePath;
                            }
                        }
                        else if (paras.Mode.EndsWith("2")) // Tự xuất excel rồi gửi.
                        {
                            string export_file;
                            bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                            if (export_ok && File.Exists(export_file))
                            {
                                result += "Không có chức năng.";
                                //result += vnptWS.UploadInvAttachmentFkey(fkeyA, export_file);
                            }
                        }
                    }
                }
                else if (paras.Mode.ToLower() == "DownloadInvFkeyNoPay".ToLower())
                {
                    fkeyA = paras.Fkey_hd;

                    string invXml = thaiSonWS.DownloadInvPDFFkeyNoPay(fkeyA);
                    paras.Result.InvoiceNo = GetSoHoaDon_VNPT(invXml);
                    //WriteFlag(flagFileName4, so_hoa_don);
                    result += paras.Result.InvoiceNo;
                    //result += invXml;
                }
                else if (paras.Mode == "S")
                {
                    var invoice = (HoaDonEntity)ReadData_ThaiSon("S");
                    invoice.SoHoaDonGoc = paras.Fkey_hd_tt;
                    result += thaiSonWS.AdjustInvoice(invoice, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "T")
                {
                    var invoice = (HoaDonEntity)ReadData_ThaiSon("T");
                    invoice.SoHoaDonGoc = paras.Fkey_hd_tt;
                    result = thaiSonWS.ReplaceInvoice(invoice, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("G"))
                {
                    if (paras.Mode == "G1") // Gạch nợ theo fkey
                    {
                        result = thaiSonWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    {
                        result = thaiSonWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G3") // Hủy gạch nợ theo fkey
                    {
                        result = thaiSonWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode == "H")
                {
                    var hoadon_entity = ReadData_ThaiSon(paras.Mode.Substring(0, 1));
                    result = thaiSonWS.CancelInvoice((HoaDonHuyEntity)hoadon_entity, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("U"))//U1,U2
                {
                    if (paras.Mode == "U")        // upload file có sẵn, fkey truyền vào
                    {
                        string fkey = paras.Fkey_hd;
                        string file = Path.Combine(paras.Dir, paras.FileName);
                        // không có chức năng
                        //vnptWS.UploadInvAttachmentFkey(fkey, file);
                    }
                    else if (paras.Mode == "U1") // upload file có sẵn, fkey tự đọc từ data
                    {
                        //ReadDataXml(arg2);
                        string fkey = paras.Fkey_hd;
                        // không có chức năng.
                        //vnptWS.UploadInvAttachmentFkey(fkey, fkey + ".xls");
                    }
                    else if (paras.Mode == "U2") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel rồi upload.
                    {
                        string export_file;
                        //ReadDataXml(arg2);
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                        if (export_ok && File.Exists(export_file))
                        {
                            result += "Không có chức năng.";
                            //result += vnptWS.UploadInvAttachmentFkey(fkeyA, export_file);
                        }
                    }
                    else if (paras.Mode == "U3") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel để đó xem.
                    {
                        string export_file;
                        //ReadDataXml(arg2);
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                        if (export_ok && File.Exists(export_file))
                        {
                            result += "\r\nExport ok.";
                        }
                    }
                }
                else if (paras.Mode.StartsWith("E"))
                {
                    if (paras.Mode == "E")
                    {

                    }
                    else if (paras.Mode == "E1")
                    {
                        string export_file;
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);
                        if (export_ok && File.Exists(export_file))
                        {
                            result += "\r\nExport ok.";
                        }
                    }
                    else if (paras.Mode == "E2")  // Xuất PDF bằng RPT
                    {
                        string rptFile = paras.RptFileFull;
                        string saveFile = Path.Combine(paras.Dir, paras.FileName);// arg4;

                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(rptFile);
                        DataSet ds = new DataSet();
                        DataTable data1 = ad_table.Copy();
                        data1.TableName = "DataTable1";
                        DataTable data2 = am_table.Copy();
                        data2.TableName = "DataTable2";
                        ds.Tables.Add(data1);
                        ds.Tables.Add(data2);
                        string tien_bang_chu = MoneyToWords(ObjectAndString.ObjectToDecimal(row0["T_TT"]), "V", "VND");
                        rpt.SetDataSource(ds);
                        rpt.SetParameterValue("SoTienVietBangChu", tien_bang_chu);

                        bool export_ok = false;
                        if (string.IsNullOrEmpty(saveFile))
                        {
                            export_ok = ExportRptToPdf_As(null, rpt, saveFile);
                        }
                        else
                        {
                            export_ok = ExportRptToPdf(null, rpt, saveFile);
                        }

                        if (export_ok)
                        {
                            result += "\r\nExport ok.";
                        }
                        else
                        {
                            result += "\r\nExport fail.";
                        }
                    }
                }
                else
                {
                    result = "ERR: Mode " + paras.Mode + " " + V6Text.NotSupported;
                }


                if (result.StartsWith("ERR"))
                {
                    //error += result;
                    paras.Result.ResultErrorMessage = result;
                }
                else
                {
                    //File.Create(flagFileName2).Close();
                }
            }
            catch (Exception ex)
            {
                result += "ERR:EX\r\n" + ex.Message;
                paras.Result.ResultErrorMessage = ex.Message;
            }
            StopAutoInputTokenPassword();
        End:
            return result;
        }

        #endregion ==== THAI_SON ====

        #region ==== MONET ====

        /// <summary>
        /// Copy từ Thái sơn - SoftDreams - Vnpt, sửa từ từ.
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static string EXECUTE_MONET(PostManagerParams paras)
        {
            MONET_API_Response response = new MONET_API_Response();
            paras.Result = new PM_Result();

            try
            {
                MONET_WS monetWS = new MONET_WS(_baseUrl, _username, _password, _codetax);
                var row0 = am_table.Rows[0];

                if (paras.Mode == "TestView")
                {
                    var invoiceString = ReadData_Monet(paras.Mode);
                    paras.Result.ResultString = invoiceString;
                }
                else if (paras.Mode.StartsWith("E_"))
                {
                    
                }
                else if (paras.Mode.StartsWith("M")) // MSHDT//Mới Sửa Hủy ĐiềuChỉnh(S) ThayThế
                {
                    var json = ReadData_Monet(paras.Mode.Substring(0, 1));
                    response = monetWS.POST_NEW(_link_Publish_vnpt_thaison, json, out paras.Result.V6ReturnValues);
                    
                    if (response.isSuccess)
                    {
                        string filePath = Path.Combine(paras.Dir, paras.FileName);
                        if (paras.Mode.EndsWith("1"))//Gửi file excel có sẵn
                        {
                            
                        }
                    }
                }
                else if (paras.Mode.ToLower() == "DownloadInvFkeyNoPay".ToLower())
                {
                    //fkeyA = paras.Fkey_hd;

                    //string invXml = monetWS.DownloadInvPDFFkeyNoPay(fkeyA);
                    //paras.Result.InvoiceNo = GetSoHoaDon_VNPT(invXml);
                    ////WriteFlag(flagFileName4, so_hoa_don);
                    //response += paras.Result.InvoiceNo;
                    ////result += invXml;
                }
                else if (paras.Mode == "S")
                {
                    var invoice = ReadData_Monet("S");
                    //invoice.SoHoaDonGoc = paras.Fkey_hd_tt;
                    response = monetWS.POST_EDIT(_modifylink, "1", paras.Fkey_hd_tt, __serial, __pattern, paras.InvoiceNo, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "T")
                {
                    var invoice = ReadData_Monet("T");
                    //invoice.SoHoaDonGoc = paras.Fkey_hd_tt;
                    response = monetWS.POST_EDIT(_modifylink, "4", paras.Fkey_hd_tt, __serial, __pattern, paras.InvoiceNo, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("G"))
                {
                    //if (paras.Mode == "G1") // Gạch nợ theo fkey
                    //{
                    //    response = monetWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    //}
                    //else if (paras.Mode == "G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    //{
                    //    response = monetWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    //}
                    //else if (paras.Mode == "G3") // Hủy gạch nợ theo fkey
                    //{
                    //    response = monetWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    //}
                }
                else if (paras.Mode == "H")
                {
                    var hoadon_entity = ReadData_Monet(paras.Mode.Substring(0, 1));
                    response = monetWS.POST_DELETE(_SERIAL_CERT,paras.InvoiceNo, paras.Serial, __pattern, paras.Fkey_hd_tt, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("U"))//U1,U2
                {
                    
                }
                else if (paras.Mode.StartsWith("E"))
                {
                    if (paras.Mode == "E")
                    {

                    }
                    else if (paras.Mode == "E1")
                    {
                        string export_file;
                        string response0 = "";
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref response0);
                        if (export_ok && File.Exists(export_file))
                        {
                            response.isSuccess = true;// += "\r\nExport ok.";
                        }
                    }
                    else if (paras.Mode == "E2")  // Xuất PDF bằng RPT
                    {
                        string rptFile = paras.RptFileFull;
                        string saveFile = Path.Combine(paras.Dir, paras.FileName);// arg4;

                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(rptFile);
                        DataSet ds = new DataSet();
                        DataTable data1 = ad_table.Copy();
                        data1.TableName = "DataTable1";
                        DataTable data2 = am_table.Copy();
                        data2.TableName = "DataTable2";
                        ds.Tables.Add(data1);
                        ds.Tables.Add(data2);
                        string tien_bang_chu = MoneyToWords(ObjectAndString.ObjectToDecimal(row0["T_TT"]), "V", "VND");
                        rpt.SetDataSource(ds);
                        rpt.SetParameterValue("SoTienVietBangChu", tien_bang_chu);

                        bool export_ok = false;
                        if (string.IsNullOrEmpty(saveFile))
                        {
                            export_ok = ExportRptToPdf_As(null, rpt, saveFile);
                        }
                        else
                        {
                            export_ok = ExportRptToPdf(null, rpt, saveFile);
                        }

                        if (export_ok)
                        {
                            response.isSuccess = true;// += "\r\nExport ok.";
                        }
                        else
                        {
                            response.isSuccess = false;// += "\r\nExport fail.";
                        }
                    }
                }

                if (response.isSuccess)
                {

                }
                else
                //if (response.StartsWith("ERR"))
                {
                    //error += result;
                    paras.Result.ResultErrorMessage += "ERR";
                }
            }
            catch (Exception ex)
            {
                paras.Result.ResultErrorMessage = ex.Message;
            }
            StopAutoInputTokenPassword();
        End:
            return paras.Result.V6ReturnValues.RESULT_STRING;
        }

        public static string ReadData_Monet(string mode)
        {
            string result = "";
            
            var postObject = new Dictionary<string, object>();
            
            //Fill data to postObject
            DataRow row0 = am_table.Rows[0];

            fkeyA = "" + row0["FKEY_HD"];
            __pattern = row0[pattern_field].ToString().Trim();
            __serial = row0[seri_field].ToString().Trim();
            //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
            foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
            {
                postObject[item.Key] = GetValue(row0, item.Value);
            }

            if (mode == "T")
            {
                //Lập hóa đơn thay thế:
                //adjustmentType = ‘3’
                //postObject["adjustmentType"] = "3";
            }

            //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
            foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
            {
                postObject[item.Key] = GetValue(row0, item.Value);
            }

            foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
            {
                postObject[item.Key] = GetValue(row0, item.Value);
            }

            //private static Dictionary<string, XmlLine> summarizeInfoConfig = null;
            foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
            {
                postObject[item.Key] = GetValue(row0, item.Value);
            }


            var items = new List<Dictionary<string, object>>();
            postObject["items"] = items;
            foreach (DataRow row in ad_table.Rows)
            {
                if (row["STT"].ToString() == "0") continue;
                Dictionary<string, object> rowData = new Dictionary<string, object>();
                foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                {
                    rowData[item.Key] = GetValue(row, item.Value);
                }

                items.Add(rowData);
            }

            result = V6JsonConverter.ObjectToJson(postObject, "yyyy-MM-dd");
            if (_write_log)
            {
                string stt_rec = row0["STT_REC"].ToString();
                string file = Path.Combine(Application.StartupPath, stt_rec + ".json");
                File.WriteAllText(file, result);
            }
            return result;
        }

        #endregion

        #region ==== MINVOICE ====

        private static string EXECUTE_MINVOICE(PostManagerParams paras)
        {
            MInvoiceResponse response = new MInvoiceResponse();
            paras.Result = new PM_Result();

            try
            {
                MInvoiceWS mInvoiceWs = new MInvoiceWS(_baseUrl, _username, _password, _ma_dvcs, _codetax, _version);
                MInvoicePostObject jsonBodyObject = null;
                var row0 = am_table.Rows[0];

                if (paras.Mode == "TestView")
                {
                    jsonBodyObject = ReadData_Minvoice(paras.Mode);
                    paras.Result.ResultString = jsonBodyObject.ToJson();
                }
                else if (paras.Mode.StartsWith("M"))
                {
                    StartAutoInputTokenPassword();

                    if (string.IsNullOrEmpty(_SERIAL_CERT))
                    {
                        jsonBodyObject = ReadData_Minvoice("M");
                        //File.Create(flagFileName1).Close();
                        response = mInvoiceWs.POST_NEW(jsonBodyObject, out paras.Result.V6ReturnValues);
                    }
                    else // Ký số client. /InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/{supplierTaxCode}
                    {
                        generalInvoiceInfoConfig["certificateSerial"] = new ConfigLine
                        {
                            Field = "certificateSerial",
                            Value = _SERIAL_CERT,
                        };
                        jsonBodyObject = ReadData_Minvoice("M");
                        //string templateCode = generalInvoiceInfoConfig["templateCode"].Value;
                        response = mInvoiceWs.POST_NEW_TOKEN(jsonBodyObject, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode == "S")
                {
                    jsonBodyObject = ReadData_Minvoice("S");
                    response = mInvoiceWs.POST_EDIT(jsonBodyObject, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "T")
                {
                    jsonBodyObject = ReadData_Minvoice("T");
                    response = mInvoiceWs.POST_EDIT(jsonBodyObject, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("G"))
                {
                    //if (paras.Mode == "G1") // Gạch nợ theo fkey
                    //{
                    //    response = monetWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    //}
                    //else if (paras.Mode == "G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    //{
                    //    response = monetWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    //}
                    //else if (paras.Mode == "G3") // Hủy gạch nợ theo fkey
                    //{
                    //    response = monetWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    //}
                }
                else if (paras.Mode == "E_H1")
                {
                    response = mInvoiceWs.POST_CANCEL(paras.V6PartnerID, "", paras.InvoiceDate, "", out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "H")
                {
                    var hoadon_entity = ReadData_Minvoice(paras.Mode.Substring(0, 1));
                    //response = mInvoiceWs.POST_DELETE(_SERIAL_CERT,paras.InvoiceNo, paras.Serial, __pattern, paras.Fkey_hd_tt, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("U"))//U1,U2
                {
                    
                }
                else if (paras.Mode.StartsWith("E"))
                {
                    if (paras.Mode == "E")
                    {

                    }
                    else if (paras.Mode == "E1")
                    {
                        string export_file;
                        string response0 = "";
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref response0);
                        if (export_ok && File.Exists(export_file))
                        {
                            response.ok = "1";
                        }
                    }
                    else if (paras.Mode == "E2")  // Xuất PDF bằng RPT
                    {
                        string rptFile = paras.RptFileFull;
                        string saveFile = Path.Combine(paras.Dir, paras.FileName);// arg4;

                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(rptFile);
                        DataSet ds = new DataSet();
                        DataTable data1 = ad_table.Copy();
                        data1.TableName = "DataTable1";
                        DataTable data2 = am_table.Copy();
                        data2.TableName = "DataTable2";
                        ds.Tables.Add(data1);
                        ds.Tables.Add(data2);
                        string tien_bang_chu = MoneyToWords(ObjectAndString.ObjectToDecimal(row0["T_TT"]), "V", "VND");
                        rpt.SetDataSource(ds);
                        rpt.SetParameterValue("SoTienVietBangChu", tien_bang_chu);

                        bool export_ok = false;
                        if (string.IsNullOrEmpty(saveFile))
                        {
                            export_ok = ExportRptToPdf_As(null, rpt, saveFile);
                        }
                        else
                        {
                            export_ok = ExportRptToPdf(null, rpt, saveFile);
                        }

                        if (export_ok)
                        {

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                paras.Result.ResultErrorMessage = ex.Message;
            }
            StopAutoInputTokenPassword();
        End:
            return paras.Result.V6ReturnValues.RESULT_STRING;
        }

        public static MInvoicePostObject ReadData_Minvoice(string mode)
        {
            string result = "";
            MInvoicePostObject postObject = null;
            //try
            {
                postObject = new MInvoicePostObject();
                postObject.windowid = "WIN00187";
                if (mode.StartsWith("M")) postObject.editmode = "1";
                if (mode.StartsWith("S")) postObject.editmode = "2";
                postObject.data = new List<InvoiceData>();
                InvoiceData invoiceData = new InvoiceData();
                postObject.data.Add(invoiceData);
                
                //Fill data to postObject
                DataRow row0 = am_table.Rows[0];

                fkeyA = "" + row0["FKEY_HD"];
                //MakeFlagNames(fkeyA);
                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    invoiceData[item.Key] = GetValue(row0, item.Value);
                }

                if (mode.StartsWith("S"))
                {
                    invoiceData["inv_invoiceNumber"] = row0["SO_CT"];
                }

                if (mode == "T")
                {
                    //Lập hóa đơn thay thế:
                    //adjustmentType = '3'
                    invoiceData["adjustmentType"] = "3";
                    //Các trường dữ liệu về hóa đơn gốc là bắt buộc
                    //originalInvoiceId
                    invoiceData["originalInvoiceId"] = row0["FKEY_TT_OLD"].ToString().Trim();  // [AA/17E0003470]
                    //originalInvoiceIssueDate
                    invoiceData["originalInvoiceIssueDate"] = row0["NGAY_CT_OLD"];

                    //Thông tin về biên bản đính kèm hóa đơn gốc:
                    //additionalReferenceDate
                    invoiceData["additionalReferenceDate"] = row0["NGAY_CT_OLD"];
                    //additionalReferenceDesc
                    invoiceData["additionalReferenceDesc"] = row0["GHI_CHU03"];
                }

                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    invoiceData[item.Key] = GetValue(row0, item.Value);
                }
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    invoiceData[item.Key] = GetValue(row0, item.Value);
                }

                List<DetailObject> listDetailObject = new List<DetailObject>();
                DetailObject detailObject = new DetailObject();
                listDetailObject.Add(detailObject);
                detailObject.tab_id = "TAB00188";
                invoiceData["details"] = listDetailObject;

                foreach (DataRow row in ad_table.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        rowData[item.Key] = GetValue(row, item.Value);
                    }
                    detailObject.data.Add(rowData);
                }
                //private static Dictionary<string, XmlLine> summarizeInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    invoiceData[item.Key] = GetValue(row0, item.Value);
                }                

                if (_write_log)
                {
                    result = postObject.ToJson();
                    string stt_rec = row0["STT_REC"].ToString();
                    string file = Path.Combine(Application.StartupPath, stt_rec + ".json");
                    File.WriteAllText(file, result);
                }
            }
            //catch (Exception ex)
            {
                //
            }

            return postObject;
        }

        #endregion MINVOICE

        #region ===== VIN =====


        private static string EXECUTE_VIN(PostManagerParams paras)
        {
            
            paras.Result = new PM_Result();

            try
            {
                if (vin_WS == null) vin_WS = new VIN_WS(_baseUrl, _username, _password, _codetax);
                
                var row0 = am_table.Rows[0];

                if (paras.Mode == "TestView")
                {
                    paras.Result.ResultString = ReadData_VIN(paras.Mode);
                }
                else if (paras.Mode.StartsWith("M"))
                {
                    StartAutoInputTokenPassword();

                    if (string.IsNullOrEmpty(_SERIAL_CERT))
                    {
                        var jsonBodyObject = ReadData_VIN("M");
                        if (paras.Key_Down == "F4") // gửi nháp
                        {
                            var response = vin_WS.POST_CREATE_INVOICE(jsonBodyObject, false, out paras.Result.V6ReturnValues);
                            //if (result.StartsWith("ERR:13")) // nếu đã tồn tại. thì xóa nháp + gửi lại.
                            //{
                            //    result = vnptWS.DeleteInvoiceByFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                            //    if (result.StartsWith("OK:"))
                            //    {
                            //        result = vnptWS.ImportInvByPattern(xml, __pattern, __serial, out paras.Result.V6ReturnValues);
                            //    }
                            //}
                        }
                        else if (paras.Key_Down == "F6") // sửa hóa đơn nháp = xóa nháp + gửi lại.
                        {
                            var response = vin_WS.POST_CREATE_INVOICE(jsonBodyObject, false, out paras.Result.V6ReturnValues);
                            
                            //if (result.StartsWith("OK:"))
                            //{
                            //    result = vnptWS.ImportInvByPattern(xml, __pattern, __serial, out paras.Result.V6ReturnValues);
                            //}
                        }
                        else
                        {
                            var response = vin_WS.POST_CREATE_INVOICE(jsonBodyObject, _signmode == "HSM" || SIGNMODE == "HSM", out paras.Result.V6ReturnValues);
                            if (paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE == null && paras.Result.V6ReturnValues.SO_HD == null && paras.Result.V6ReturnValues.RESULT_MESSAGE.Contains("Trùng hồ sơ"))
                            {
                                // trùng hồ sơ, tải lại thông tin.
                                //response = vin_WS.SIGN_HSM(paras.Result.V6ReturnValues.SECRET_CODE, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                                paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = paras.Result.V6ReturnValues.RESULT_MESSAGE;
                            }
                            else if (paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE == null && paras.Result.V6ReturnValues.SO_HD == null && (_signmode == "HSM" || SIGNMODE == "HSM"))
                            {
                                response = vin_WS.SIGN_HSM(paras.Result.V6ReturnValues.SECRET_CODE, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                            }
                            else
                            {

                            }
                        }
                    }
                    else // Ký số client. /InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/{supplierTaxCode}
                    {
                        generalInvoiceInfoConfig["certificateSerial"] = new ConfigLine
                        {
                            Field = "certificateSerial",
                            Value = _SERIAL_CERT,
                        };
                        var jsonBodyObject = ReadData_VIN("M");
                        //string templateCode = generalInvoiceInfoConfig["templateCode"].Value;
                        var response = vin_WS.POST_CREATE_INVOICE(jsonBodyObject, true, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode == "E_H1")
                {
                    var hoadon = ReadData_VIN_Object("H");
                    var item = generalInvoiceInfoConfig["ngaylap"];

                    string so_bien_ban = paras.AM_data["STT_REC"].ToString();
                    string ngay_bien_ban = ObjectAndString.ObjectToString((DateTime)GetValue(row0, item), "yyyy-mm-dd hh:MM:ss");
                    hoadon["hopdong_so"] = so_bien_ban;
                    hoadon["hopdong_ngayky"] = ngay_bien_ban;
                    hoadon["file_hopdong"] = "NOFILE";
                    hoadon["nguoilap"] = ngay_bien_ban;     // Tên tài khoản người lập hd
                    
                    paras.InvoiceNo = paras.AM_data["SO_SERI"].ToString().Trim() + paras.AM_data["SO_CT"].ToString().Trim();

                    string json = ReadData_VIN_ObjectToJson(hoadon);
                    string result = vin_WS.HUY_HOA_DON(json, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("E_S")) // S S1(tiền) S2(thông tin)
                {
                    var hoadon = ReadData_VIN_Object("S");

                    string jsonBody = ReadData_VIN_ObjectToJson(hoadon);
                    string result = vin_WS.POST_EDIT(jsonBody, SIGNMODE == "HSM", out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "E_T1")
                {
                    var hoadon = ReadData_VIN_Object("T");
                    string jsonBody = ReadData_VIN_ObjectToJson(hoadon);
                    string result = vin_WS.POST_REPLACE(jsonBody, SIGNMODE == "HSM", out paras.Result.V6ReturnValues);
                }
            }
            catch (Exception ex)
            {
                paras.Result.ResultErrorMessage = ex.Message;
            }
            StopAutoInputTokenPassword();
        End:
            return paras.Result.V6ReturnValues.RESULT_STRING;
        }


        public static string ReadData_VIN(string mode)
        {
            var hoadon = ReadData_VIN_Object(mode);
            string result = ReadData_VIN_ObjectToJson(hoadon);
            return result;
        }

        private static string ReadData_VIN_ObjectToJson(Dictionary<string, object> hoadon)
        {
            string result = V6JsonConverter.ObjectToJson(hoadon, _datetype);
            if (_write_log)
            {
                DataRow row0 = am_table.Rows[0];
                string stt_rec = row0["STT_REC"].ToString();
                string file = Path.Combine(Application.StartupPath, stt_rec + ".json");
                File.WriteAllText(file, result);
            }
            return result;
        }

        /// <summary>
        /// Cần đọc xml trước!
        /// </summary>
        /// <param name="dbfFile"></param>
        /// <param name="mode">M mới hoặc T thay thế</param>
        /// <returns></returns>
        public static Dictionary<string, object> ReadData_VIN_Object(string mode)
        {
            //string result = "";
            var hoadon = new Dictionary<string, object>();
            //try
            {   
                //Fill data to postObject
                DataRow row0 = am_table.Rows[0];

                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    hoadon[item.Key] = GetValue(row0, item.Value);
                }

                

                if (mode == "H")
                {
                    //Lập hóa đơn HỦY: 
                    hoadon["hoadon_loai"] = 7;
                }

                if (mode == "S")
                {
                    //Lập hóa đơn điều chỉnh: trong chi tiết và dsthuesuat có thêm trường dieuchinh_tanggiam
                    hoadon["hoadon_goc"] = row0["FKEY_TT_OLD"].ToString().Trim();
                }

                if (mode == "T")
                {
                    //Lập hóa đơn thay thế:
                    hoadon["hoadon_goc"] = row0["FKEY_TT_OLD"].ToString().Trim();
                }

                //if (_TEST_)
                //{
                //    Guid new_uid = Guid.NewGuid();
                //    hoadon["ma_hoadon"] = "" + new_uid;
                //}

                fkeyA = "" + hoadon["ma_hoadon"];
                //MakeFlagNames(_ma_hoadon_or_fkey);

                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    hoadon[item.Key] = GetValue(row0, item.Value);
                }
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    hoadon[item.Key] = GetValue(row0, item.Value);
                }

                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    hoadon[item.Key] = GetValue(row0, item.Value);
                }

                if (metadataConfig != null)
                {
                    foreach (KeyValuePair<string, ConfigLine> metaItem in metadataConfig)
                    {
                        Dictionary<string, object> metadata = new Dictionary<string, object>();
                        metadata["invoiceCustomFieldId"] = ObjectAndString.ObjectToInt(metaItem.Value.SL_TD1);
                        metadata["keyTag"] = metaItem.Key;
                        metadata["valueType"] = metaItem.Value.DataType; // text, number, date
                        if (metaItem.Value.DataType.ToLower() == "date")
                        {
                            metadata["dateValue"] = GetValue(row0, metaItem.Value);
                        }
                        else if (metaItem.Value.DataType.ToLower() == "number")
                        {
                            metadata["numberValue"] = GetValue(row0, metaItem.Value);
                        }
                        else if (metaItem.Value.DataType.ToUpper() == "N2C0VNDE")
                        {
                            // N2C0VNDE thêm đọc số tiền nếu ma_nt != VND
                            string ma_nt = row0["MA_NT"].ToString().Trim().ToUpper();
                            if (ma_nt != "VND")
                            {
                                metadata["stringValue"] = ObjectAndString.ObjectToString(GetValue(row0, metaItem.Value));
                                metadata["valueType"] = "text";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {

                            metadata["stringValue"] = ObjectAndString.ObjectToString(GetValue(row0, metaItem.Value));
                        }
                        metadata["keyLabel"] = ObjectAndString.ObjectToString(metaItem.Value.MA_TD2);
                        metadata["isRequired"] = ObjectAndString.ObjectToBool(metaItem.Value.SL_TD2);
                        metadata["isSeller"] = ObjectAndString.ObjectToBool(metaItem.Value.SL_TD3);

                        //{
                        //   "invoiceCustomFieldId": 1135,
                        //   "keyTag": "dueDate",
                        //   "valueType": "date",
                        //   "dateValue": 1544115600000,
                        //   "keyLabel": "Hạn thanh toán",
                        //   "isRequired": false,
                        //   "isSeller": false
                        // },
                        hoadon["metadata"] = metadata;
                    }
                }


                var dschitiet = new List<Dictionary<string, object>>();
                foreach (DataRow row in ad_table.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        rowData[item.Key] = GetValue(row, item.Value);
                    }
                    dschitiet.Add(rowData);
                }
                hoadon["dschitiet"] = dschitiet;




                var dsthuesuat = new List<Dictionary<string, object>>();
                Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                {
                    taxBreakdown[item.Key] = GetValue(row0, item.Value);
                }
                dsthuesuat.Add(taxBreakdown);
                hoadon["dsthuesuat"] = dsthuesuat; // Chỉ có 1 dòng.

                //if (taxBreakdownsConfig != null && ad3_table != null && ad3_table.Rows.Count > 0)
                //{
                //    foreach (DataRow ad3_row in ad3_table.Rows)
                //    {
                //        Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                //        foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                //        {
                //            taxBreakdown[item.Key] = GetValue(ad3_row, item.Value);
                //        }
                //        postObject.taxBreakdowns.Add(taxBreakdown);
                //    }
                //}

                //result = postObject.ToJson(_dateType);
                
            }
            //catch (Exception ex)
            {
                //
            }
            
            return hoadon;
        }

        #endregion vin


        #region ===== CYBER =====


        private static string EXECUTE_CYBER(PostManagerParams paras)
        {

            paras.Result = new PM_Result();

            try
            {
                if (cyber_WS == null) cyber_WS = new CYBER_WS(_baseUrl, _username, _password, _codetax);

                var row0 = am_table.Rows[0];

                if (paras.Mode == "TestView")
                {
                    paras.Result.ResultString = ReadData_CYBER(paras.Mode);
                }
                else if (paras.Mode.StartsWith("M"))
                {
                    StartAutoInputTokenPassword();

                    if (string.IsNullOrEmpty(_SERIAL_CERT))
                    {
                        var jsonBodyObject = ReadData_CYBER("M");
                        if (paras.Key_Down == "F4") // gửi nháp
                        {
                            var response = cyber_WS.POST_CREATE_INVOICE(jsonBodyObject, false, out paras.Result.V6ReturnValues);
                            //if (result.StartsWith("ERR:13")) // nếu đã tồn tại. thì xóa nháp + gửi lại.
                            //{
                            //    result = vnptWS.DeleteInvoiceByFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                            //    if (result.StartsWith("OK:"))
                            //    {
                            //        result = vnptWS.ImportInvByPattern(xml, __pattern, __serial, out paras.Result.V6ReturnValues);
                            //    }
                            //}
                        }
                        else if (paras.Key_Down == "F6") // sửa hóa đơn nháp = xóa nháp + gửi lại.
                        {
                            var response = cyber_WS.POST_CREATE_INVOICE(jsonBodyObject, false, out paras.Result.V6ReturnValues);

                            //if (result.StartsWith("OK:"))
                            //{
                            //    result = vnptWS.ImportInvByPattern(xml, __pattern, __serial, out paras.Result.V6ReturnValues);
                            //}
                        }
                        else
                        {
                            var response = cyber_WS.POST_CREATE_INVOICE(jsonBodyObject, _signmode == "HSM" || SIGNMODE == "HSM", out paras.Result.V6ReturnValues);
                            if (paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE == null && paras.Result.V6ReturnValues.SO_HD == null && paras.Result.V6ReturnValues.RESULT_MESSAGE.Contains("Trùng hồ sơ"))
                            {
                                // trùng hồ sơ, tải lại thông tin.
                                //response = cyber_WS.SIGN_HSM(paras.Result.V6ReturnValues.SECRET_CODE, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                                paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = paras.Result.V6ReturnValues.RESULT_MESSAGE;
                            }
                            else if (paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE == null && paras.Result.V6ReturnValues.SO_HD == null && (_signmode == "HSM" || SIGNMODE == "HSM"))
                            {
                                response = cyber_WS.SIGN_HSM(paras.Result.V6ReturnValues.SECRET_CODE, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                            }
                            else
                            {

                            }
                        }
                    }
                    else // Ký số client. /InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/{supplierTaxCode}
                    {
                        generalInvoiceInfoConfig["certificateSerial"] = new ConfigLine
                        {
                            Field = "certificateSerial",
                            Value = _SERIAL_CERT,
                        };
                        var jsonBodyObject = ReadData_CYBER("M");
                        //string templateCode = generalInvoiceInfoConfig["templateCode"].Value;
                        var response = cyber_WS.POST_CREATE_INVOICE(jsonBodyObject, true, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode == "E_H1")
                {
                    var hoadon = ReadData_CYBER_Object("H");
                    var item = generalInvoiceInfoConfig["ngaylap"];

                    string so_bien_ban = paras.AM_data["STT_REC"].ToString();
                    string ngay_bien_ban = ObjectAndString.ObjectToString((DateTime)GetValue(row0, item), "yyyy-mm-dd hh:MM:ss");
                    hoadon["hopdong_so"] = so_bien_ban;
                    hoadon["hopdong_ngayky"] = ngay_bien_ban;
                    hoadon["file_hopdong"] = "NOFILE";
                    hoadon["nguoilap"] = ngay_bien_ban;     // Tên tài khoản người lập hd

                    paras.InvoiceNo = paras.AM_data["SO_SERI"].ToString().Trim() + paras.AM_data["SO_CT"].ToString().Trim();

                    string json = ReadData_CYBER_ObjectToJson(hoadon);
                    string result = cyber_WS.HUY_HOA_DON(json, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("E_S")) // S S1(tiền) S2(thông tin)
                {
                    var hoadon = ReadData_CYBER_Object("S");

                    string jsonBody = ReadData_CYBER_ObjectToJson(hoadon);
                    string result = cyber_WS.POST_EDIT(jsonBody, SIGNMODE == "HSM", out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "E_T1")
                {
                    var hoadon = ReadData_CYBER_Object("T");
                    string jsonBody = ReadData_CYBER_ObjectToJson(hoadon);
                    string result = cyber_WS.POST_REPLACE(jsonBody, SIGNMODE == "HSM", out paras.Result.V6ReturnValues);
                }
            }
            catch (Exception ex)
            {
                paras.Result.ResultErrorMessage = ex.Message;
            }
            StopAutoInputTokenPassword();
        End:
            return paras.Result.V6ReturnValues.RESULT_STRING;
        }


        public static string ReadData_CYBER(string mode)
        {
            var hoadon = ReadData_CYBER_Object(mode);
            string result = ReadData_CYBER_ObjectToJson(hoadon);
            return result;
        }

        private static string ReadData_CYBER_ObjectToJson(Dictionary<string, object> hoadon)
        {
            string result = V6JsonConverter.ObjectToJson(hoadon, _datetype);
            if (_write_log)
            {
                DataRow row0 = am_table.Rows[0];
                string stt_rec = row0["STT_REC"].ToString();
                string file = Path.Combine(Application.StartupPath, stt_rec + ".json");
                File.WriteAllText(file, result);
            }
            return result;
        }

        /// <summary>
        /// Cần đọc xml trước!
        /// </summary>
        /// <param name="dbfFile"></param>
        /// <param name="mode">M mới hoặc T thay thế</param>
        /// <returns></returns>
        public static Dictionary<string, object> ReadData_CYBER_Object(string mode)
        {
            //string result = "";
            var hoadon = new Dictionary<string, object>();
            //try
            {
                //Fill data to postObject
                DataRow row0 = am_table.Rows[0];

                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    hoadon[item.Key] = GetValue(row0, item.Value);
                }



                if (mode == "H")
                {
                    //Lập hóa đơn HỦY: 
                    hoadon["hoadon_loai"] = 7;
                }

                if (mode == "S")
                {
                    //Lập hóa đơn điều chỉnh: trong chi tiết và dsthuesuat có thêm trường dieuchinh_tanggiam
                    hoadon["hoadon_goc"] = row0["FKEY_TT_OLD"].ToString().Trim();
                }

                if (mode == "T")
                {
                    //Lập hóa đơn thay thế:
                    hoadon["hoadon_goc"] = row0["FKEY_TT_OLD"].ToString().Trim();
                }

                //if (_TEST_)
                //{
                //    Guid new_uid = Guid.NewGuid();
                //    hoadon["ma_hoadon"] = "" + new_uid;
                //}

                fkeyA = "" + hoadon["ma_hoadon"];
                //MakeFlagNames(_ma_hoadon_or_fkey);

                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    hoadon[item.Key] = GetValue(row0, item.Value);
                }
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    hoadon[item.Key] = GetValue(row0, item.Value);
                }

                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    hoadon[item.Key] = GetValue(row0, item.Value);
                }

                if (metadataConfig != null)
                {
                    foreach (KeyValuePair<string, ConfigLine> metaItem in metadataConfig)
                    {
                        Dictionary<string, object> metadata = new Dictionary<string, object>();
                        metadata["invoiceCustomFieldId"] = ObjectAndString.ObjectToInt(metaItem.Value.SL_TD1);
                        metadata["keyTag"] = metaItem.Key;
                        metadata["valueType"] = metaItem.Value.DataType; // text, number, date
                        if (metaItem.Value.DataType.ToLower() == "date")
                        {
                            metadata["dateValue"] = GetValue(row0, metaItem.Value);
                        }
                        else if (metaItem.Value.DataType.ToLower() == "number")
                        {
                            metadata["numberValue"] = GetValue(row0, metaItem.Value);
                        }
                        else if (metaItem.Value.DataType.ToUpper() == "N2C0VNDE")
                        {
                            // N2C0VNDE thêm đọc số tiền nếu ma_nt != VND
                            string ma_nt = row0["MA_NT"].ToString().Trim().ToUpper();
                            if (ma_nt != "VND")
                            {
                                metadata["stringValue"] = ObjectAndString.ObjectToString(GetValue(row0, metaItem.Value));
                                metadata["valueType"] = "text";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {

                            metadata["stringValue"] = ObjectAndString.ObjectToString(GetValue(row0, metaItem.Value));
                        }
                        metadata["keyLabel"] = ObjectAndString.ObjectToString(metaItem.Value.MA_TD2);
                        metadata["isRequired"] = ObjectAndString.ObjectToBool(metaItem.Value.SL_TD2);
                        metadata["isSeller"] = ObjectAndString.ObjectToBool(metaItem.Value.SL_TD3);

                        //{
                        //   "invoiceCustomFieldId": 1135,
                        //   "keyTag": "dueDate",
                        //   "valueType": "date",
                        //   "dateValue": 1544115600000,
                        //   "keyLabel": "Hạn thanh toán",
                        //   "isRequired": false,
                        //   "isSeller": false
                        // },
                        hoadon["metadata"] = metadata;
                    }
                }


                var dschitiet = new List<Dictionary<string, object>>();
                foreach (DataRow row in ad_table.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        rowData[item.Key] = GetValue(row, item.Value);
                    }
                    dschitiet.Add(rowData);
                }
                hoadon["dschitiet"] = dschitiet;




                var dsthuesuat = new List<Dictionary<string, object>>();
                Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                {
                    taxBreakdown[item.Key] = GetValue(row0, item.Value);
                }
                dsthuesuat.Add(taxBreakdown);
                hoadon["dsthuesuat"] = dsthuesuat; // Chỉ có 1 dòng.

                //if (taxBreakdownsConfig != null && ad3_table != null && ad3_table.Rows.Count > 0)
                //{
                //    foreach (DataRow ad3_row in ad3_table.Rows)
                //    {
                //        Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                //        foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                //        {
                //            taxBreakdown[item.Key] = GetValue(ad3_row, item.Value);
                //        }
                //        postObject.taxBreakdowns.Add(taxBreakdown);
                //    }
                //}

                //result = postObject.ToJson(_dateType);

            }
            //catch (Exception ex)
            {
                //
            }

            return hoadon;
        }

        #endregion CYBER


        #region ===== MISA =====


        private static string EXECUTE_MISA(PostManagerParams paras)
        {

            paras.Result = new PM_Result();
            string jsonBody = null;
            string result = null;

            try
            {
                if (misa_WS == null) misa_WS = new MISA_WS(_baseUrl, _username, _password, _codetax, _appID, COMACQT);

                var row0 = am_table.Rows[0];

                if (paras.Mode == "TestView")
                {
                    paras.Result.ResultString = ReadData_MISA(paras.Mode);
                }
                else if (paras.Mode.StartsWith("M"))
                {
                    StartAutoInputTokenPassword();
                    //generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                    //{
                    //    Field = "adjustmentType",
                    //    Value = "1",
                    //};
                    //Guid new_uid = Guid.NewGuid();
                    //if (mode == "MG")
                    //{
                    //    generalInvoiceInfoConfig["transactionUuid"] = new ConfigLine
                    //    {
                    //        Field = "transactionUuid",
                    //        Value = "" + new_uid,
                    //    };
                    //}

                    if (paras.Mode == "M0") // DRAF
                    {
                        jsonBody = ReadData_MISA("M");
                        result = misa_WS.POST_CREATE_INVOICE(jsonBody, out paras.Result.V6ReturnValues);
                    }
                    else if (string.IsNullOrEmpty(_SERIAL_CERT))
                    {
                        jsonBody = ReadData_MISA("M");
                        result = misa_WS.POST_CREATE_INVOICE_HSM(jsonBody, out paras.Result.V6ReturnValues);
                    }
                    else // Ký số client. /InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/{supplierTaxCode}
                    {
                        generalInvoiceInfoConfig["certificateSerial"] = new ConfigLine
                        {
                            Field = "certificateSerial",
                            Value = _SERIAL_CERT,
                        };
                        jsonBody = ReadData_MISA("M");
                        string templateCode = generalInvoiceInfoConfig["templateCode"].Value;
                        result = misa_WS.CreateInvoice_GetXml_Sign(jsonBody, _SERIAL_CERT, out paras.Result.V6ReturnValues);
                    }

                    //if (string.IsNullOrEmpty(paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE))
                    //{
                    //    message = "Thành công. Số hóa đơn: " + paras.Result.V6ReturnValues.SO_HD;
                    //}
                    //else
                    //{
                    //    message = paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE;
                    //}
                }
                else if (paras.Mode.StartsWith("S"))
                {
                    if (paras.Mode.EndsWith("3"))
                    {
                        generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                        {
                            Field = "adjustmentType",
                            Value = "3",
                        };
                    }
                    else if (paras.Mode.EndsWith("5"))
                    {
                        generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                        {
                            Field = "adjustmentType",
                            Value = "5",
                        };
                    }
                    else
                    {
                        generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                        {
                            Field = "adjustmentType",
                            Value = "3",
                        };
                    }

                    jsonBody = ReadData_MISA("S");
                    result = misa_WS.POST_EDIT(jsonBody, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "T")
                {
                    jsonBody = ReadData_MISA("T");
                    result = misa_WS.POST_REPLACE(jsonBody, _version == "V45I", out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("G")) // call exe như mode M
                {
                    V6Message.Show("Chưa hỗ trợ G!");
                    //jsonBody = ReadData(dbfFile, "M");
                    //if (mode == "G1" || mode == "G") // Gạch nợ theo fkey
                    //{
                    //    File.Create(flagFileName1).Close();
                    //    string invoiceNo = ""  + postObject.generalInvoiceInfo["invoiceSeries"] +  row0["SO_CT"].ToString().Trim();
                    //    string strIssueDate = V6JsonConverter.ObjectToJson(postObject.generalInvoiceInfo["invoiceIssuedDate"], null);
                    //    string templateCode = postObject.generalInvoiceInfo["templateCode"].ToString();
                    //    string buyerEmailAddress = postObject.buyerInfo["buyerEmail"].ToString();
                    //    string paymentType = postObject.generalInvoiceInfo["paymentType"].ToString();
                    //    string paymentTypeName = postObject.generalInvoiceInfo["paymentTypeName"].ToString();
                    //    result = _viettelV2_ws.UpdatePaymentStatus(_codetax, invoiceNo, strIssueDate, templateCode,
                    //        buyerEmailAddress, paymentType, paymentTypeName, "true", out paras.Result.V6ReturnValues);
                    //}
                    ////else if (mode == "G2") // Gạch nợ hóa đơn theo lstInvToken(01GTKT2/001;AA/13E;10)
                    ////{
                    ////    File.Create(flagFileName1).Close();
                    ////    result = confirmPayment(arg2);
                    ////}
                    //else if (mode == "G3") // Hủy gạch nợ theo fkey
                    //{
                    //    File.Create(flagFileName1).Close();
                    //    string invoiceNo = ""  + postObject.generalInvoiceInfo["invoiceSeries"] +  row0["SO_CT"].ToString().Trim();
                    //    string strIssueDate = V6JsonConverter.ObjectToJson(postObject.generalInvoiceInfo["invoiceIssuedDate"], null);
                    //    string templateCode = postObject.generalInvoiceInfo["templateCode"].ToString();
                    //    string buyerEmailAddress = postObject.buyerInfo["buyerEmail"].ToString();
                    //    string paymentType = postObject.generalInvoiceInfo["paymentType"].ToString();
                    //    string paymentTypeName = postObject.generalInvoiceInfo["paymentTypeName"].ToString();
                    //    result = _viettelV2_ws.UpdatePaymentStatus(_codetax, invoiceNo, strIssueDate, templateCode,
                    //        buyerEmailAddress, paymentType, paymentTypeName, "false", out paras.Result.V6ReturnValues);
                    //}

                }
                else if (paras.Mode == "H")
                {
                    V6Message.Show("Chưa hỗ trợ H!");
                    result = misa_WS.HUY_HOA_DON("transactionID", "invNo", "2022-01-01", "số liệu sai", out paras.Result.V6ReturnValues);
                }
                //else if (paras.Mode.StartsWith("P"))
                

                    
            }
            catch (Exception ex)
            {
                paras.Result.ResultErrorMessage = ex.Message;
            }
            StopAutoInputTokenPassword();
        End:
            return paras.Result.V6ReturnValues.RESULT_STRING;
        }


        public static string ReadData_MISA(string mode)
        {
            var hoadon = ReadData_MISA_Object(mode);
            string result = ReadData_MISA_ObjectToJson(hoadon);
            return result;
        }

        private static string ReadData_MISA_ObjectToJson(OriginalInvoiceData hoadonMISA) //object hoadon = List<OriginalInvoiceData> or var hsm_send_data = new List<Dictionary<string, object>>();
        {
            string result = "";

            if (SIGNMODE == "HSM")
            {
                var hsm_send_data = new List<Dictionary<string, object>>();
                var one = new Dictionary<string, object>();
                one["RefID"] = hoadonMISA.RefID;
                one["OriginalInvoiceData"] = hoadonMISA;
                hsm_send_data.Add(one);

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                result = JsonConvert.SerializeObject(hsm_send_data, settings);
            }
            else
            {
                var list_hoadonMISA = new List<OriginalInvoiceData>();
                list_hoadonMISA.Add(hoadonMISA);

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                result = JsonConvert.SerializeObject(list_hoadonMISA, settings);
            }


            if (_write_log)
            {
                DataRow row0 = am_table.Rows[0];
                string stt_rec = row0["STT_REC"].ToString();
                string file = Path.Combine(Application.StartupPath, stt_rec + ".json");
                File.WriteAllText(file, result);
            }
            return result;
        }

        /// <summary>
        /// Cần đọc xml trước!
        /// </summary>
        /// <param name="dbfFile"></param>
        /// <param name="mode">M mới hoặc T thay thế</param>
        /// <returns></returns>
        public static OriginalInvoiceData ReadData_MISA_Object(string mode) // List<OriginalInvoiceData>
        {
            var hoadonMISA = new OriginalInvoiceData();
            try
            {
                //var list_hoadon = new MISA_PostObject();
                //var hoadon = new Dictionary<string, object>();
                //list_hoadon.Add(hoadon);

                
                //var list_hoadonMISA = new List<OriginalInvoiceData>();


                //list_hoadonMISA.Add(hoadonMISA);
                //postObject.hoadon = hoadon;
                //ReadXmlInfo(xmlFile);
                //DataTable dataDbf = ParseDBF.ReadDBF(dbfFile);
                //DataTable data = Data_Table.FromTCVNtoUnicode(dataDbf);
                //DataTable table3 = null;
                //if (USETAXBREAKDOWNS && !string.IsNullOrEmpty(dbfFile3))
                //{
                //    DataTable dataDbf3 = ParseDBF.ReadDBF(dbfFile3);
                //    table3 = Data_Table.FromTCVNtoUnicode(dataDbf3);
                //}
                //Fill data to postObject
                DataRow row0 = am_table.Rows[0];

                //fkeyA = fkey0 + row0["STT_REC"];



                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    //hoadon[item.Key] = item.Value.GetValue(row0);
                    ObjectAndString.SetObjectPropertyValue(hoadonMISA, item.Key, item.Value.GetValue(row0));
                }



                if (mode == "S")
                {
                    //Lập hóa đơn điều chỉnh: trong chi tiết và dsthuesuat có thêm trường dieuchinh_tanggiam
                    //hoadon["OrgInvNo"] = row0["SO_CT_OLD"].ToString().Trim();
                    ObjectAndString.SetObjectPropertyValue(hoadonMISA, "OrgInvNo", row0["SO_CT_OLD"].ToString().Trim());
                }

                if (mode == "T")
                {
                    //Lập hóa đơn thay thế:
                    //hoadon["OrgInvNo"] = row0["SO_CT_OLD"].ToString().Trim();
                    ObjectAndString.SetObjectPropertyValue(hoadonMISA, "OrgInvNo", row0["SO_CT_OLD"].ToString().Trim());
                    ////thông tin hóa đơn thay thế hoặc điều chỉnh 
                    //if (rbAdjust.Checked)
                    //{
                    //    invoiceData.ReferenceType = 2;
                    //    invoiceData.OrgInvoiceType = 1;
                    //    invoiceData.OrgInvNo = txtNumberOrg.Text;
                    //    invoiceData.OrgInvTemplateNo = cboKyHieu.Text.Substring(0, 1);
                    //    invoiceData.OrgInvSeries = cboKyHieu.Text;
                    //    invoiceData.OrgInvDate = dteOrgDate.Value.Date;
                    //}
                    //else if (rbReplace.Checked)
                    //{
                    //    invoiceData.ReferenceType = 1;
                    //    invoiceData.OrgInvoiceType = 1;
                    //    invoiceData.OrgInvNo = txtNumberOrg.Text;
                    //    invoiceData.OrgInvTemplateNo = cboKyHieu.Text.Substring(0, 1);
                    //    invoiceData.OrgInvSeries = cboKyHieu.Text;
                    //    invoiceData.OrgInvDate = dteOrgDate.Value.Date;
                    //}
                }

                //if (_TEST_)
                //{
                //    Guid new_uid = Guid.NewGuid();
                //    hoadon["RefID"] = "" + new_uid;
                //    ObjectAndString.SetObjectPropertyValue(hoadonMISA, "RefID", "" + new_uid);
                //}

                fkeyA = "" + hoadonMISA.RefID;
                //MakeFlagNames(_RefID_or_fkey);

                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    //hoadon[item.Key] = item.Value.GetValue(row0);
                    ObjectAndString.SetObjectPropertyValue(hoadonMISA, item.Key, item.Value.GetValue(row0));

                }
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    //hoadon[item.Key] = item.Value.GetValue(row0);
                    ObjectAndString.SetObjectPropertyValue(hoadonMISA, item.Key, item.Value.GetValue(row0));
                }

                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    //hoadon[item.Key] = item.Value.GetValue(row0);
                    ObjectAndString.SetObjectPropertyValue(hoadonMISA, item.Key, item.Value.GetValue(row0));
                }

                if (metadataConfig != null)
                {
                    foreach (KeyValuePair<string, ConfigLine> metaItem in metadataConfig)
                    {
                        Dictionary<string, object> metadata = new Dictionary<string, object>();
                        metadata["invoiceCustomFieldId"] = ObjectAndString.ObjectToInt(metaItem.Value.SL_TD1);
                        metadata["keyTag"] = metaItem.Key;
                        metadata["valueType"] = metaItem.Value.DataType; // text, number, date
                        if (metaItem.Value.DataType.ToLower() == "date")
                        {
                            metadata["dateValue"] = metaItem.Value.GetValue(row0);
                        }
                        else if (metaItem.Value.DataType.ToLower() == "number")
                        {
                            metadata["numberValue"] = metaItem.Value.GetValue(row0);
                        }
                        else if (metaItem.Value.DataType.ToUpper() == "N2C0VNDE")
                        {
                            // N2C0VNDE thêm đọc số tiền nếu ma_nt != VND
                            string ma_nt = row0["MA_NT"].ToString().Trim().ToUpper();
                            if (ma_nt != "VND")
                            {
                                metadata["stringValue"] = ObjectAndString.ObjectToString(metaItem.Value.GetValue(row0));
                                metadata["valueType"] = "text";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {

                            metadata["stringValue"] = ObjectAndString.ObjectToString(metaItem.Value.GetValue(row0));
                        }
                        metadata["keyLabel"] = ObjectAndString.ObjectToString(metaItem.Value.MA_TD2);
                        metadata["isRequired"] = ObjectAndString.ObjectToBool(metaItem.Value.SL_TD2);
                        metadata["isSeller"] = ObjectAndString.ObjectToBool(metaItem.Value.SL_TD3);

                        //{
                        //   "invoiceCustomFieldId": 1135,
                        //   "keyTag": "dueDate",
                        //   "valueType": "date",
                        //   "dateValue": 1544115600000,
                        //   "keyLabel": "Hạn thanh toán",
                        //   "isRequired": false,
                        //   "isSeller": false
                        // },
                        //hoadon["metadata"] = metadata; // bị bỏ qua, code thừa.

                    }
                }




                if (USETAXBREAKDOWNS && ad3_table != null)
                {
                    var dsthuesuat = new List<TaxRateInfo>();
                    foreach (DataRow row3 in ad3_table.Rows)
                    {
                        Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                        foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                        {
                            taxBreakdown[item.Key] = item.Value.GetValue(row3);
                        }
                        dsthuesuat.Add(taxBreakdown.ToModel<TaxRateInfo>());
                    }
                    //hoadon["TaxRateInfo"] = dsthuesuat; // nhiều dòng tùy vào table3
                    hoadonMISA.TaxRateInfo = dsthuesuat;
                }
                else
                {
                    var dsthuesuat = new List<TaxRateInfo>();
                    Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                    {
                        taxBreakdown[item.Key] = item.Value.GetValue(row0);
                    }
                    dsthuesuat.Add(taxBreakdown.ToModel<TaxRateInfo>());
                    //hoadon["TaxRateInfo"] = dsthuesuat;
                    hoadonMISA.TaxRateInfo = dsthuesuat; // Chỉ có 1 dòng.
                }

                if (optionUserDefinedConfig != null)
                {
                    var options = new Dictionary<string, object>();

                    foreach (KeyValuePair<string, ConfigLine> item in optionUserDefinedConfig)
                    {
                        options[item.Key] = item.Value.GetValue(row0);
                    }
                    //hoadon["OptionUserDefined"] = options;
                    hoadonMISA.OptionUserDefined = options.ToModel<OptionUserDefined>();
                }

                var dschitiet = new List<Dictionary<string, object>>();
                var dschitietMISA = new List<OriginalInvoiceDetail>();
                foreach (DataRow row in ad_table.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        rowData[item.Key] = item.Value.GetValue(row);
                    }
                    dschitiet.Add(rowData);
                    dschitietMISA.Add(rowData.ToModel<OriginalInvoiceDetail>());
                }
                //hoadon["OriginalInvoiceDetail"] = dschitiet;
                hoadonMISA.OriginalInvoiceDetail = dschitietMISA;

                

                
            }
            catch (Exception ex)
            {
                V6Message.Show(ex.Message, 0);
            }
            //return result;
            return hoadonMISA;
        }

        #endregion misa

        private static object GetValue(DataRow row, ConfigLine config)
        {
            object fieldValue = config.Value;
            DataTable table = row.Table;

            string configTYPE = null, configDATATYPE = null;
            if (!string.IsNullOrEmpty(config.Type))
            {
                string[] ss = config.Type.Split(':');
                configTYPE = ss[0].ToUpper();
                if (ss.Length > 1) configDATATYPE = ss[1].ToUpper();
            }
            if (string.IsNullOrEmpty(configDATATYPE))
            {
                configDATATYPE = (config.DataType ?? "").ToUpper();
            }

            if (configTYPE == "ENCRYPT")
            {
                return UtilityHelper.DeCrypt(fieldValue.ToString());
            }

            if (configTYPE == "FIELD" && !string.IsNullOrEmpty(config.FieldV6))
            {
                // FieldV6 sẽ có dạng thông thường là (Field) hoặc dạng ghép là (Field1 + Field2) hoặc (Field1 + "abc" + field2)
                if (table.Columns.Contains(config.FieldV6))
                {
                    fieldValue = row[config.FieldV6];
                    if (table.Columns[config.FieldV6].DataType == typeof(string))
                    {
                        //Trim
                        fieldValue = fieldValue.ToString().Trim();
                    }
                }
                else
                {
                    decimal giatribt;
                    if (Number.GiaTriBieuThucTry(config.FieldV6, row.ToDataDictionary(), out giatribt))
                    {
                        fieldValue = giatribt;
                    }
                    else
                    {
                        var fields = ObjectAndString.SplitStringBy(config.FieldV6.Replace("\\+", "~plus~"), '+');

                        string fieldValueString = "";

                        foreach (string s in fields)
                        {
                            string field = s.Trim();
                            if (table.Columns.Contains(field))
                            {
                                fieldValueString += ObjectAndString.ObjectToString(row[field]).Trim();
                            }
                            else
                            {
                                //if (field.StartsWith("\"") && field.EndsWith("\""))
                                //{
                                //    field = field.Substring(1, field.Length - 2);
                                //}
                                fieldValueString += field;
                            }
                        }
                        // Chốt.
                        fieldValue = fieldValueString.Replace("~plus~", "+");
                    }// end else giatribieuthuc
                }
            }

            if (!string.IsNullOrEmpty(configDATATYPE))
            {
                switch (configDATATYPE)
                {
                    case "BOOL":
                        if (fieldValue is bool)
                        {
                            return fieldValue;
                        }
                        else
                        {
                            return fieldValue != null &&
                                (fieldValue.ToString() == "1" ||
                                    fieldValue.ToString().ToLower() == "true" ||
                                    fieldValue.ToString().ToLower() == "yes");
                        }
                    case "BOOLS":
                        if (fieldValue is bool)
                        {
                            return fieldValue;
                        }
                        else
                        {
                            if ((fieldValue + "").Trim() == "") return null;

                            return fieldValue.ToString() == "1" ||
                                   fieldValue.ToString().ToLower() == "true" ||
                                   fieldValue.ToString().ToLower() == "yes";
                        }
                    case "DATE":
                    case "DATETIME":
                        return ObjectAndString.ObjectToDate(fieldValue, config.Format);
                        break;
                    case "N2C":
                        return MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", "VND");
                    case "N2CE":
                        return MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "E", "VND");
                    case "N2CMANT":
                        return MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", row["MA_NT"].ToString().Trim());
                    case "N2CMANTE":
                        return MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "E", row["MA_NT"].ToString().Trim());
                    case "N2C0VNDE":
                    {
                        string ma_nt = row["MA_NT"].ToString().Trim().ToUpper();
                        if (ma_nt != "VND")
                        {
                            return MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "E", row["MA_NT"].ToString().Trim());
                        }
                        else
                        {
                            return "";
                        }
                    }
                    case "DECIMAL":
                    case "MONEY":
                    case "NUMBER":
                        return ObjectAndString.ObjectToDecimal(fieldValue);
                    case "INT":
                        return ObjectAndString.ObjectToInt(fieldValue);
                    case "INT64":
                    case "LONG":
                        return ObjectAndString.ObjectToInt64(fieldValue);
                    //case "UPPER": // Chỉ dùng ở exe gọi bằng Foxpro.
                    //    return (fieldValue + "").ToUpper();
                    case "INTSTRING": // Đưa kiểu số về chuỗi nguyên (không lấy phần thập phân).
                        return ObjectAndString.ObjectToInt(fieldValue).ToString();
                    case "STRING":
                        return "" + fieldValue;
                    default:    // Kiểu nguyên mẫu của dữ liệu.
                        return fieldValue;
                }
            }
            else
            {
                return fieldValue;
            }
        }

        public static string MoneyToWords(decimal money, string lang, string ma_nt)
        {
            return V6BusinessHelper.MoneyToWords(money, lang, ma_nt);
        }

        
        /// <summary>
        /// Cần viết thêm đọc xml.
        /// </summary>
        /// <param name="mapTable"></param>
        public static void ReadConfigInfo(DataTable mapTable)
        {
            V6Infos = new Dictionary<string, string>();
            generalInvoiceInfoConfig = new Dictionary<string, ConfigLine>();
            uiDefine = new Dictionary<string, ConfigLine>();
            buyerInfoConfig = new Dictionary<string, ConfigLine>();
            sellerInfoConfig = new Dictionary<string, ConfigLine>();
            metadataConfig = new Dictionary<string, ConfigLine>();
            paymentsConfig = new Dictionary<string, ConfigLine>();
            itemInfoConfig = new Dictionary<string, ConfigLine>();
            summarizeInfoConfig = new Dictionary<string, ConfigLine>();
            taxBreakdownsConfig = new Dictionary<string, ConfigLine>();
            customerInfoConfig = new Dictionary<string, ConfigLine>();
            optionUserDefinedConfig = new Dictionary<string, ConfigLine>();

            parameters_config = new List<ConfigLine>();
            _SERIAL_CERT = "";

            try
            {
                foreach (DataRow row in mapTable.Rows)
                {
                    string GROUP_NAME = row["GroupName"].ToString().Trim().ToUpper();
                    ConfigLine line = ReadConfigLine(row);
                    string line_field = line.Field.ToLower();
                    switch (GROUP_NAME)
                    {
                        case "V6INFO":
                        {
                            V6Infos[line.Field.ToUpper()] = line.Value;
                            switch (line_field)
                            {
                                case "username":
                                    _username = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "password":
                                    _password = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "codetax":
                                    _codetax = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "version":
                                    _version = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "datamode":
                                    _datamode = line.Value;
                                    break;
                                case "ma_dvcs":
                                    _ma_dvcs = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "baselink":
                                case "baseurl":
                                    _baseUrl = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "appid":
                                    _appID = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "site":
                                case "login":
                                case "sitelogin":
                                case "loginsite":
                                case "website":
                                    _site = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "datetype":
                                    _datetype = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                    //Vnpt, có dùng cả username, password
                                case "link_publish":
                                        _link_Publish_vnpt_thaison = UtilityHelper.DeCrypt(line.Value);
                                        _baseUrl = _link_Publish_vnpt_thaison.Substring(0, _link_Publish_vnpt_thaison.LastIndexOf('/'));
                                    break;
                                case "account":
                                    _account = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "accountpassword":
                                    _accountpassword = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "serialcert":
                                case "certificateserial":
                                    _SERIAL_CERT = UtilityHelper.DeCrypt(line.Value).ToUpper();
                                    break;
                                case "token_password_title":
                                    _token_password_title = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "token_password":
                                    _token_password = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "pattern":
                                case "partten":
                                    pattern_field = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "seri":
                                case "serial":
                                    seri_field = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                    //Bkav
                                case "bkavpartnerguid":
                                    BkavPartnerGUID = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "bkavpartnertoken":
                                    BkavPartnerToken = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "bkavcommandtypenew":
                                    BkavCommandTypeNew = ObjectAndString.ObjectToInt(UtilityHelper.DeCrypt(line.Value));
                                    break;
                                case "bkavcommandtypeedit":
                                    BkavCommandTypeEdit = ObjectAndString.ObjectToInt(UtilityHelper.DeCrypt(line.Value));
                                    break;
                                    case "bkavcommandtypeedit2":
                                        BkavCommandTypeEdit2 = ObjectAndString.ObjectToInt(UtilityHelper.DeCrypt(line.Value));
                                        break;
                                    case "signmode":
                                    _signmode = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;

                                case "reason":
                                    _reason_field = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "writelog":
                                    _write_log = ObjectAndString.ObjectToBool(line.Value);
                                    break;
                                case "nocdata":
                                    _noCdata = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                    
                            }
                            break;
                        }
                        case "GENERALINVOICEINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    generalInvoiceInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "UIDEFINE":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    uiDefine.Add(line.Field, line);
                                }
                                break;
                            }
                        case "BUYERINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    buyerInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "SELLERINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    sellerInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "METADATA":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    metadataConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "PAYMENTS":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    paymentsConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "ITEMINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    itemInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "SUMMARIZEINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    summarizeInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "TAXBREAKDOWNS":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    taxBreakdownsConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "OPTIONUSERDEFINED":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    optionUserDefinedConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "CUSTOMERINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    customerInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "EXCELCONFIG":
                            {
                                if (line.Type == "2")
                                {
                                    parameters_config.Add(line);
                                }
                                else if (line.Field == "TEMPLATE")
                                {
                                    template_xls = line.Value;
                                }
                                else if (line.Field == "FIRSTCELL")
                                {
                                    firstCell = line.Value;
                                }
                                else if (line.Field == "INSERTROW")
                                {
                                    insertRow = ObjectAndString.ObjectToBool(line.Value);
                                }
                                else if (line.Field == "DRAWLINE")
                                {
                                    drawLine = ObjectAndString.ObjectToBool(line.Value);
                                }
                                else if (line.Field == "COLUMNS")
                                {
                                    columns = ObjectAndString.SplitString(line.Value);
                                }
                                else if (line.Field.StartsWith("COLUMNS"))
                                {
                                    column_config[line.Field] = line.Value;
                                }

                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }

            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog("PostManager.ReadConfigInfo", ex);
                throw;
            }
        }

        private static ConfigLine ReadConfigLine(DataRow reader)
        {
            ConfigLine config = new ConfigLine();
            config.Field = reader["Field"].ToString().Trim();
            config.Value = reader["Value"].ToString().Trim();
            config.FieldV6 = reader["FieldV6"].ToString().Trim();
            config.Type = reader["Type"].ToString().Trim();
            string[] datatype_format = (reader["DataType"] + "").Trim().Split(':');
            config.DataType = datatype_format[0];
            config.Format = datatype_format.Length > 1 ? datatype_format[1] : "";
            if (reader.Table.Columns.Contains("NoGen")) config.NoGen = reader["NoGen"].ToString().Trim() == "1";
            if (reader.Table.Columns.Contains("NoGenCondition")) config.NoGenCondition = reader["NoGenCondition"].ToString().Trim();
            
            config.MA_TD2 = ObjectAndString.ObjectToString(reader["MA_TD2"]);
            config.MA_TD3 = ObjectAndString.ObjectToString(reader["MA_TD3"]);
            config.SL_TD1 = ObjectAndString.ObjectToDecimal(reader["SL_TD1"]);
            config.SL_TD2 = ObjectAndString.ObjectToDecimal(reader["SL_TD2"]);
            config.SL_TD3 = ObjectAndString.ObjectToDecimal(reader["SL_TD3"]);
            return config;
        }

        public static string SearchInvoice(PostManagerParams paras)
        {
            try
            {
                _map_table = paras.DataSet.Tables[0];
                //ad_table = paras.DataSet.Tables[1];
                //am_table = paras.DataSet.Tables[2];
                //Fkey_hd_tt = paras.Fkey_hd_tt;
                //DataRow row0 = am_table.Rows[0];
                //ad2_table = paras.DataSet.Tables[3];
                //if (paras.DataSet.Tables.Count > 4)
                //{
                //    ad3_table = paras.DataSet.Tables[4];
                //}
                //else
                //{
                //    ad3_table = null;
                //}

                ReadConfigInfo(_map_table);

                switch (paras.Branch)
                {
                    case "1":
                        ViettelWS viettel_ws = new ViettelWS(_baseUrl, _username, _password, _codetax);
                        return viettel_ws.GetListInvoiceDataControl(paras.InvoiceDate, paras.InvoiceDate);
                        break;
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog("PostManager.SearchInvoice", ex);
            }
            return null;
        }
    }
}
