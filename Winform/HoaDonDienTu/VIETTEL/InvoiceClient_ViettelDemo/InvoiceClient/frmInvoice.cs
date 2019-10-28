using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using System.Web;
using log4net;


namespace InvoiceClient
{
    public partial class frmInvoice : Form
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(frmInvoice));

        public frmInvoice()
        {
            InitializeComponent();
        }

        private void btnCreateInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                string userPass = ConfigurationManager.AppSettings["UserPass"].ToString();
                string codeTax = ConfigurationManager.AppSettings["CodeTax"].ToString();
                string apiLink = ConfigurationManager.AppSettings["APILink"].ToString() + @"/InvoiceAPI/InvoiceWS/createInvoice/" + codeTax;
                string autStr = CreateRequest.Base64Encode(userPass);
                string contentType = "application/json";

                InvoiceInfo objInvoice = new InvoiceInfo();
                objInvoice.uuId = System.Guid.NewGuid().ToString();
                objInvoice.invoiceType = "01GTKT";
                objInvoice.templateCode = "01GTKT0/001";
                objInvoice.invoiceSeries = "AA/18E";
                //DateTime issuedDate = new DateTime(2018, 09, 06, 10, 30, 10);
                //objInvoice.invoiceIssuedDate = ((Int64)(issuedDate.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds).ToString();
                objInvoice.invoiceIssuedDate = ((Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds).ToString();
                objInvoice.currencyCode = "VND";
                objInvoice.adjustmentType = "1";
                objInvoice.paymentStatus = "true";
                objInvoice.paymentType = "TM";
                objInvoice.paymentTypeName = "TM";
                objInvoice.cusGetInvoiceRight = "true";
                objInvoice.buyerIdNo = "123456789";
                objInvoice.buyerIdType = "1";
                BuyerInfo objBuyer = new BuyerInfo();
                objBuyer.buyerAddressLine = "HN VN";
                objBuyer.buyerIdNo = objInvoice.buyerIdNo;
                objBuyer.buyerIdType = objInvoice.buyerIdType;
                objBuyer.buyerName = "Đặng thị thanh tâm";
                objBuyer.buyerPhoneNumber = "098999990000";
                SellerInfo objSeller = new SellerInfo();
                objSeller.sellerAddressLine = "HN VN";
                objSeller.sellerBankAccount = "2345";
                objSeller.sellerBankName = "TPB";
                objSeller.sellerEmail = "sinvoice@viettel.vn";
                objSeller.sellerLegalName = "Bên bán";
                objSeller.sellerPhoneNumber = "180099999";
                objSeller.sellerTaxCode = codeTax;
                string paymentMethodName = "TM";
                List<ItemInfo> lstItem = new List<ItemInfo>();
                ItemInfo item = new ItemInfo();
                item.discount = "0.0";
                item.itemCode = "SP1";
                item.itemDiscount = "5000.0";
                item.itemName = "SP1";
                item.itemTotalAmountWithoutTax = "250000";
                item.lineNumber = "1";
                item.quantity = "10";
                item.taxAmount = "0.0";
                item.taxPercentage = 10;
                item.unitName = "kg";
                item.unitPrice = "25000";
                lstItem.Add(item);

                ItemInfo item1 = new ItemInfo();
                item1.discount = "0.0";
                item1.itemCode = "SP12";
                item1.itemDiscount = "5000.0";
                item1.itemName = "SP1";
                item1.itemTotalAmountWithoutTax = "250000";
                item1.lineNumber = "2";
                item1.quantity = "10";
                item1.taxAmount = "0.0";
                item1.taxPercentage = 10;
                item1.unitName = "kg";
                item1.unitPrice = "25000";
                lstItem.Add(item1);
                SummarizeInfo objSummary = new SummarizeInfo();
                objSummary.discountAmount = "0";
                objSummary.settlementDiscountAmount = "0";
                objSummary.sumOfTotalLineAmountWithoutTax = "250000";
                objSummary.taxPercentage = "10";
                objSummary.totalAmountWithoutTax = "250000";
                objSummary.totalAmountWithTax = "275000";
                objSummary.totalAmountWithTaxInWords = NumberUtil.DocSoThanhChu(objSummary.totalAmountWithTax);
                objSummary.totalTaxAmount = "25000";
                
                string request = @"{    ""generalInvoiceInfo"": " +
                    @"{       ""invoiceType"":""" + objInvoice.invoiceType +
                    @""",       ""templateCode"":""" + objInvoice.templateCode +
                    @""", 	""invoiceSeries"":""" + objInvoice.invoiceSeries +
                    @""",       ""transactionUuid"": """ + objInvoice.uuId +
                    @""",       ""invoiceIssuedDate"":" + objInvoice.invoiceIssuedDate +
                    @",       ""currencyCode"":""" + objInvoice.currencyCode + @""", 
                    ""adjustmentType"":""" + objInvoice.adjustmentType + @""",  
                    ""paymentStatus"":" + objInvoice.paymentStatus + @",       
                    ""paymentType"":""" + objInvoice.paymentType + @""",      
                    ""paymentTypeName"":""" + objInvoice.paymentTypeName + @""",       
                    ""cusGetInvoiceRight"":" + objInvoice.cusGetInvoiceRight + @",      
                    ""buyerIdNo"":""" + objInvoice.buyerIdNo + @""",        
                    ""buyerIdType"":""" + objInvoice.buyerIdType + @"""    },    
                    ""buyerInfo"":{       
                    ""buyerName"":""" + objBuyer.buyerName + @""",       
                    ""buyerLegalName"":""" + objBuyer.buyerLegalName + @""",       
                    ""buyerTaxCode"":""" + objBuyer.buyerTaxCode + @""",      
                    ""buyerAddressLine"":""" + objBuyer.buyerAddressLine + @""",       
                    ""buyerPhoneNumber"":""" + objBuyer.buyerPhoneNumber + @""",      
                    ""buyerEmail"":""" + objBuyer.buyerEmail + @""",      
                    ""buyerIdNo"":""" + objBuyer.buyerIdNo + @""",        
                    ""buyerIdType"":""" + objBuyer.buyerIdType + @"""    },    
                    ""sellerInfo"":{       
                    ""sellerLegalName"":""" + objSeller.sellerLegalName + @""",       
                    ""sellerTaxCode"":""" + objSeller.sellerTaxCode + @""",        
                    ""sellerAddressLine"":""" + objSeller.sellerAddressLine + @""",          
                    ""sellerPhoneNumber"":""" + objSeller.sellerPhoneNumber + @""",             
                    ""sellerEmail"":""" + objSeller.sellerEmail + @""",              
                    ""sellerBankName"":""" + objSeller.sellerBankName + @""",             
                    ""sellerBankAccount"":""" + objSeller.sellerBankAccount + @"""   },    
                    ""extAttribute"":[     ],    
                    ""payments"":[       {
                    ""paymentMethodName"":""" + paymentMethodName + @"""    }    ],    
                    ""deliveryInfo"":{     },    
                    ""itemInfo"":     ";

                var json = JsonConvert.SerializeObject(lstItem);
                
                request += json;
                request += @",    
                    ""discountItemInfo"":[     ],   
                    ""meterReading"": [{             
                    ""previousIndex"": ""5454"",             
                    ""currentIndex"": ""244"",             
                    ""factor"": ""22"",             
                    ""amount"": ""2""           },           
                    {             ""previousIndex"": ""44"",             
                    ""currentIndex"": ""44"",             
                    ""factor"": ""33"",             
                    ""amount"": ""3""           }],    
                    ""summarizeInfo"":{       
                    ""sumOfTotalLineAmountWithoutTax"":" + objSummary.sumOfTotalLineAmountWithoutTax + @",          
                    ""totalAmountWithoutTax"":" + objSummary.totalAmountWithoutTax + @",   
                    ""totalTaxAmount"":" + objSummary.totalTaxAmount + @",  
                    ""totalAmountWithTax"":" + objSummary.totalAmountWithTax + @", 
                    ""totalAmountWithTaxInWords"":""" + objSummary.totalAmountWithTaxInWords + @""",       
                    ""discountAmount"":" + objSummary.discountAmount + @",     
                    ""settlementDiscountAmount"":" + objSummary.settlementDiscountAmount + @",    
                    ""taxPercentage"":" + objSummary.taxPercentage + @"  },    
                    ""taxBreakdowns"":[       {          
                    ""taxPercentage"":10.0,          
                    ""taxableAmount"":250000,          
                    ""taxAmount"":25000.0       }    ] } ";
                string result = CreateRequest.webRequest(apiLink, request, autStr, "POST", contentType);
                MessageBox.Show("OK " + result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("NOK " + ex.Message);
            }
        }

        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                string userPass = ConfigurationManager.AppSettings["UserPass"].ToString();
                string codeTax = ConfigurationManager.AppSettings["CodeTax"].ToString();
                string apiLink = ConfigurationManager.AppSettings["APILink"].ToString() + @"/InvoiceAPI/InvoiceUtilsWS/getInvoiceFile";

                GetFileRequest objGetFile = new GetFileRequest();
                objGetFile.fileType = "zip";
                objGetFile.invoiceNo = "BR/18E0000014";
                objGetFile.strIssueDate = "20180320152309";

                string getData = "?supplierTaxCode=" + codeTax +
                    "&invoiceNo=" + objGetFile.invoiceNo +
                    "&fileType=" + objGetFile.fileType +
                    "&strIssueDate=" + objGetFile.strIssueDate;
                apiLink += getData;
                string autStr = CreateRequest.Base64Encode(userPass);
                string contentType = "application/x-www-form-urlencoded";

                string request = string.Empty;
                string result = CreateRequest.webRequest(apiLink, request, autStr, "GET", contentType);
                int startIndex = result.IndexOf(@"fileName"":""") + ((@"""fileName"":""").Length);
                int length = result.IndexOf(@""",""fileToBytes") - startIndex;
                string fileName = result.Substring(startIndex, length);

                ZipFileResponse objFile = JsonConvert.DeserializeObject<ZipFileResponse>(result);
                string path = Path.Combine(Application.StartupPath, "zipFile") + "\\" + fileName + ".zip";
                File.WriteAllBytes(path, objFile.fileToBytes);

                MessageBox.Show("File in " + path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("NOK " + ex.Message);
            }
        }

        //private void btnSavePDF_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string userPass = ConfigurationManager.AppSettings["UserPass"].ToString();
        //        string codeTax = ConfigurationManager.AppSettings["CodeTax"].ToString();
        //        string apiLink = ConfigurationManager.AppSettings["APILink"].ToString() + @"/InvoiceAPI/InvoiceWS/createInvoiceRepresentation";

        //        GetFileRequest objGetFile = new GetFileRequest();
        //        objGetFile.invoiceNo = "BR/18E0000014";
        //        objGetFile.strIssueDate = "20180320152309";

        //        string request = "supplierTaxCode=" + HttpUtility.UrlEncode(codeTax) +
        //            "&invoiceNo=" + HttpUtility.UrlEncode(objGetFile.invoiceNo) +
        //            "&strIssueDate=" + HttpUtility.UrlEncode(objGetFile.strIssueDate);
        //        string autStr = CreateRequest.Base64Encode(userPass);
        //        string contentType = "application/x-www-form-urlencoded";

        //        string result = CreateRequest.webRequest(apiLink, request, autStr, "POST", contentType);
        //        int startIndex = result.IndexOf(@"""links"":""") + ((@"""links"":""").Length);
        //        int length = result.IndexOf(@""",""paymentStatus") - startIndex;
        //        string pathFile = result.Substring(startIndex, length);
        //        Uri uri = new Uri(pathFile);
        //        string fileName = string.Empty;
        //        fileName = System.IO.Path.GetFileName(uri.LocalPath);
        //        string path = Path.Combine(Application.StartupPath, "pdfFile") + "\\" + fileName;

        //        using (WebClient webClient = new WebClient())
        //        {
        //            CreateRequest.InitiateSSLTrust();
        //            webClient.Proxy = new WebProxy();
        //            webClient.DownloadFile(pathFile, path);
        //        }

        //        MessageBox.Show("File in " + path);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("NOK " + ex.Message);
        //    }
        //}
        private void btnSavePDF_Click(object sender, EventArgs e)
        {
            try
            {
                string userPass = ConfigurationManager.AppSettings["UserPass"].ToString();
                string codeTax = ConfigurationManager.AppSettings["CodeTax"].ToString();
                string apiLink = ConfigurationManager.AppSettings["APILink"].ToString() + @"/InvoiceAPI/InvoiceUtilsWS/getInvoiceRepresentationFile";

                GetFileRequest objGetFile = new GetFileRequest();
                objGetFile.invoiceNo = "BR/18E0000036";
                objGetFile.pattern = "01GTKT0/206";
                objGetFile.fileType = "pdf";
                objGetFile.transactionUuid = "";

                string request = @"{
                            ""supplierTaxCode"":""" + codeTax + @""",
                            ""invoiceNo"":""" + objGetFile.invoiceNo + @""",
                            ""pattern"":""" + objGetFile.pattern + @""",
                            ""transactionUuid"":""" + objGetFile.transactionUuid + @""",
                            ""fileType"":""" + objGetFile.fileType + @"""
                            }";
                string autStr = CreateRequest.Base64Encode(userPass);
                string contentType = "application/json";

                string result = CreateRequest.webRequest(apiLink, request, autStr, "POST", contentType);
                int startIndex = result.IndexOf(@"fileName"":""") + ((@"""fileName"":""").Length);
                int length = result.IndexOf(@""",""fileToBytes") - startIndex;
                string fileName = result.Substring(startIndex, length);

                PDFFileResponse objFile = JsonConvert.DeserializeObject<PDFFileResponse>(result);
                string path = Path.Combine(Application.StartupPath, "pdfFile") + "\\" + fileName + ".pdf";
                File.WriteAllBytes(path, objFile.fileToBytes);

                MessageBox.Show("File in " + path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("NOK " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                string userPass = ConfigurationManager.AppSettings["UserPass"].ToString();
                string codeTax = ConfigurationManager.AppSettings["CodeTax"].ToString();
                string apiLink = ConfigurationManager.AppSettings["APILink"].ToString() + @"/InvoiceAPI/InvoiceWS/cancelTransactionInvoice";

                GetFileRequest objGetFile = new GetFileRequest();
                objGetFile.invoiceNo = "BR/18E0000032";
                objGetFile.strIssueDate = "20180321010531";
                objGetFile.additionalReferenceDesc = "File thoa thuan";
                objGetFile.additionalReferenceDate = "20180321100802";

                string request = "supplierTaxCode=" + HttpUtility.UrlEncode(codeTax) +
                    "&invoiceNo=" + HttpUtility.UrlEncode(objGetFile.invoiceNo) +
                    "&strIssueDate=" + HttpUtility.UrlEncode(objGetFile.strIssueDate) +
                    "&additionalReferenceDesc=" + HttpUtility.UrlEncode(objGetFile.additionalReferenceDesc) +
                    "&additionalReferenceDate=" + HttpUtility.UrlEncode(objGetFile.additionalReferenceDate);
                string autStr = CreateRequest.Base64Encode(userPass);
                string contentType = "application/x-www-form-urlencoded";

                string result = CreateRequest.webRequest(apiLink, request, autStr, "POST", contentType);

                MessageBox.Show("OK " + result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("NOK " + ex.Message);
            }
        }

        private void btnChangeMoney_Click(object sender, EventArgs e)
        {
            try
            {
                string userPass = ConfigurationManager.AppSettings["UserPass"].ToString();
                string codeTax = ConfigurationManager.AppSettings["CodeTax"].ToString();
                string apiLink = ConfigurationManager.AppSettings["APILink"].ToString() + @"/InvoiceAPI/InvoiceWS/createInvoice/" + codeTax;
                string autStr = CreateRequest.Base64Encode(userPass);
                string contentType = "application/json";

                InvoiceInfo objInvoice = new InvoiceInfo();
                objInvoice.uuId = System.Guid.NewGuid().ToString();
                objInvoice.invoiceType = "01GTKT";
                objInvoice.templateCode = "01GTKT0/206";
                objInvoice.invoiceSeries = "BR/18E";
                objInvoice.invoiceIssuedDate = ((Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds).ToString();
                objInvoice.currencyCode = "VND";
                objInvoice.adjustmentType = "5";
                objInvoice.paymentStatus = "true";
                objInvoice.paymentType = "TM";
                objInvoice.paymentTypeName = "TM";
                objInvoice.cusGetInvoiceRight = "true";
                objInvoice.buyerIdNo = "123456789";
                objInvoice.buyerIdType = "1";
                objInvoice.invoiceNote = "Thay đổi tiền";
                objInvoice.adjustmentInvoiceType = "1";
                objInvoice.originalInvoiceId = "BR/18E0000016";
                objInvoice.originalInvoiceIssueDate = "20180320152309";
                objInvoice.additionalReferenceDesc = "BC Thay đổi tiền";
                objInvoice.additionalReferenceDate = ((Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds).ToString();
                BuyerInfo objBuyer = new BuyerInfo();
                objBuyer.buyerAddressLine = "HN VN";
                objBuyer.buyerIdNo = objInvoice.buyerIdNo;
                objBuyer.buyerIdType = objInvoice.buyerIdType;
                objBuyer.buyerName = "Đặng thị thanh tâm";
                objBuyer.buyerPhoneNumber = "098999990000";
                SellerInfo objSeller = new SellerInfo();
                objSeller.sellerAddressLine = "HN VN";
                objSeller.sellerBankAccount = "2345";
                objSeller.sellerBankName = "TPB";
                objSeller.sellerEmail = "sinvoice@viettel.vn";
                objSeller.sellerLegalName = "Bên bán";
                objSeller.sellerPhoneNumber = "180099999";
                objSeller.sellerTaxCode = codeTax;
                string paymentMethodName = "TM";
                List<ItemInfo> lstItem = new List<ItemInfo>();
                ItemInfo item = new ItemInfo();
                item.discount = "0.0";
                item.itemCode = "SP1";
                item.itemDiscount = "5000.0";
                item.itemName = "SP1";
                item.itemTotalAmountWithoutTax = "250000";
                item.lineNumber = "1";
                item.quantity = "10";
                item.taxAmount = "0.0";
                item.taxPercentage = 10;
                item.unitName = "kg";
                item.unitPrice = "25000";
                item.adjustmentTaxAmount = "1.0";
                item.isIncreaseItem = "true";
                lstItem.Add(item);
                SummarizeInfo objSummary = new SummarizeInfo();
                objSummary.discountAmount = "0";
                objSummary.settlementDiscountAmount = "0";
                objSummary.sumOfTotalLineAmountWithoutTax = "250000";
                objSummary.taxPercentage = "10";
                objSummary.totalAmountWithoutTax = "250000";
                objSummary.totalAmountWithTax = "275000";
                objSummary.totalAmountWithTaxInWords = NumberUtil.DocSoThanhChu(objSummary.totalAmountWithTax);
                objSummary.totalTaxAmount = "25000";

                string request = @"{    ""generalInvoiceInfo"": " +
                    @"{       ""invoiceType"":""" + objInvoice.invoiceType +
                    @""",       ""templateCode"":""" + objInvoice.templateCode +
                    @""", 	""invoiceSeries"":""" + objInvoice.invoiceSeries +
                    @""",       ""transactionUuid"": """ + objInvoice.uuId +
                    @""",       ""invoiceIssuedDate"":" + objInvoice.invoiceIssuedDate +
                    @",       ""currencyCode"":""" + objInvoice.currencyCode + @""", 
                    ""adjustmentType"":""" + objInvoice.adjustmentType + @""",  
                    ""adjustmentInvoiceType"":""" + objInvoice.adjustmentInvoiceType + @""",  
                    ""invoiceNote"":""" + objInvoice.invoiceNote + @""",  
                    ""originalInvoiceId"":""" + objInvoice.originalInvoiceId + @""",  
                    ""originalInvoiceIssueDate"":" + objInvoice.adjustmentType + @",  
                    ""additionalReferenceDesc"":""" + objInvoice.additionalReferenceDesc + @""",  
                    ""additionalReferenceDate"":" + objInvoice.additionalReferenceDate + @",  
                    ""paymentStatus"":" + objInvoice.paymentStatus + @",       
                    ""paymentType"":""" + objInvoice.paymentType + @""",      
                    ""paymentTypeName"":""" + objInvoice.paymentTypeName + @""",       
                    ""cusGetInvoiceRight"":" + objInvoice.cusGetInvoiceRight + @",      
                    ""buyerIdNo"":""" + objInvoice.buyerIdNo + @""",        
                    ""buyerIdType"":""" + objInvoice.buyerIdType + @"""    },    
                    ""buyerInfo"":{       
                    ""buyerName"":""" + objBuyer.buyerName + @""",       
                    ""buyerLegalName"":""" + objBuyer.buyerLegalName + @""",       
                    ""buyerTaxCode"":""" + objBuyer.buyerTaxCode + @""",      
                    ""buyerAddressLine"":""" + objBuyer.buyerAddressLine + @""",       
                    ""buyerPhoneNumber"":""" + objBuyer.buyerPhoneNumber + @""",      
                    ""buyerEmail"":""" + objBuyer.buyerEmail + @""",      
                    ""buyerIdNo"":""" + objBuyer.buyerIdNo + @""",        
                    ""buyerIdType"":""" + objBuyer.buyerIdType + @"""    },    
                    ""sellerInfo"":{       
                    ""sellerLegalName"":""" + objSeller.sellerLegalName + @""",       
                    ""sellerTaxCode"":""" + objSeller.sellerTaxCode + @""",        
                    ""sellerAddressLine"":""" + objSeller.sellerAddressLine + @""",          
                    ""sellerPhoneNumber"":""" + objSeller.sellerPhoneNumber + @""",             
                    ""sellerEmail"":""" + objSeller.sellerEmail + @""",              
                    ""sellerBankName"":""" + objSeller.sellerBankName + @""",             
                    ""sellerBankAccount"":""" + objSeller.sellerBankAccount + @"""   },    
                    ""extAttribute"":[     ],    
                    ""payments"":[       {
                    ""paymentMethodName"":""" + paymentMethodName + @"""    }    ],    
                    ""deliveryInfo"":{     },    
                    ""itemInfo"":[       ";
                int indexItem = 1;
                foreach (var itemInfo in lstItem)
                {
                    request += @"{""lineNumber"":" + itemInfo.lineNumber + @",          
                    ""itemCode"":""" + itemInfo.itemCode + @""",    
                    ""itemName"":""" + itemInfo.itemName + @""",          
                    ""unitName"":""" + itemInfo.unitName + @""",     
                    ""unitPrice"":" + itemInfo.unitPrice + @",             
                    ""quantity"":" + itemInfo.quantity + @",               
                    ""itemTotalAmountWithoutTax"":" + itemInfo.itemTotalAmountWithoutTax + @",               
                    ""taxPercentage"":" + itemInfo.taxPercentage + @",              
                    ""taxAmount"":" + itemInfo.taxAmount + @",  
                    ""adjustmentTaxAmount"":" + itemInfo.adjustmentTaxAmount + @",  
                    ""isIncreaseItem"":" + itemInfo.isIncreaseItem + @",              
                    ""discount"":" + itemInfo.discount + @",                
                    ""itemDiscount"":" + itemInfo.itemDiscount + @"     }";
                    if (indexItem > 1)
                        request += " , ";
                    indexItem++;
                }
                request += @"],    
                    ""discountItemInfo"":[     ],   
                    ""meterReading"": [{             
                    ""previousIndex"": ""5454"",             
                    ""currentIndex"": ""244"",             
                    ""factor"": ""22"",             
                    ""amount"": ""2""           },           
                    {             ""previousIndex"": ""44"",             
                    ""currentIndex"": ""44"",             
                    ""factor"": ""33"",             
                    ""amount"": ""3""           }],    
                    ""summarizeInfo"":{
                    ""sumOfTotalLineAmountWithoutTax"":" + objSummary.sumOfTotalLineAmountWithoutTax + @",          
                    ""totalAmountWithoutTax"":" + objSummary.totalAmountWithoutTax + @",   
                    ""totalTaxAmount"":" + objSummary.totalTaxAmount + @",  
                    ""totalAmountWithTax"":" + objSummary.totalAmountWithTax + @", 
                    ""totalAmountWithTaxInWords"":""" + objSummary.totalAmountWithTaxInWords + @""",
                    ""isTotalAmountPos"":false,
                    ""isTotalTaxAmountPos"":false,
                    ""isTotalAmtWithoutTaxPos"":false,   
                    ""discountAmount"":" + objSummary.discountAmount + @",     
                    ""settlementDiscountAmount"":" + objSummary.settlementDiscountAmount + @",
                    ""isDiscountAmtPos"":false,
                    ""taxPercentage"":" + objSummary.taxPercentage + @"  },    
                    ""taxBreakdowns"":[       {          
                    ""taxPercentage"":10.0,          
                    ""taxableAmount"":250000,          
                    ""taxAmount"":25000.0       }    ] } ";
                string result = CreateRequest.webRequest(apiLink, request, autStr, "POST", contentType);
                MessageBox.Show("OK " + result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("NOK " + ex.Message);
            }
        }

        private void btnChangeInfo_Click(object sender, EventArgs e)
        {
            try
            {
                string userPass = ConfigurationManager.AppSettings["UserPass"].ToString();
                string codeTax = ConfigurationManager.AppSettings["CodeTax"].ToString();
                string apiLink = ConfigurationManager.AppSettings["APILink"].ToString() + @"/InvoiceAPI/InvoiceWS/createInvoice/" + codeTax;
                string autStr = CreateRequest.Base64Encode(userPass);
                string contentType = "application/json";

                InvoiceInfo objInvoice = new InvoiceInfo();
                objInvoice.uuId = System.Guid.NewGuid().ToString();
                objInvoice.invoiceType = "01GTKT";
                objInvoice.templateCode = "01GTKT0/206";
                objInvoice.invoiceSeries = "BR/18E";
                objInvoice.invoiceIssuedDate = ((Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds).ToString();
                objInvoice.currencyCode = "VND";
                objInvoice.adjustmentType = "5";
                objInvoice.paymentStatus = "true";
                objInvoice.paymentType = "TM";
                objInvoice.paymentTypeName = "TM";
                objInvoice.cusGetInvoiceRight = "true";
                objInvoice.buyerIdNo = "123456789";
                objInvoice.buyerIdType = "1";
                objInvoice.invoiceNote = "Thay đổi thông tin";
                objInvoice.adjustmentInvoiceType = "2";
                objInvoice.originalInvoiceId = "BR/18E0000029";
                objInvoice.originalInvoiceIssueDate = "20180321093412";
                objInvoice.additionalReferenceDesc = "BC Thay đổi thông tin";
                objInvoice.additionalReferenceDate = ((Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds).ToString();
                BuyerInfo objBuyer = new BuyerInfo();
                objBuyer.buyerAddressLine = "HN VN";
                objBuyer.buyerIdNo = objInvoice.buyerIdNo;
                objBuyer.buyerIdType = objInvoice.buyerIdType;
                objBuyer.buyerName = "Đặng thị thanh tâm 12";
                objBuyer.buyerPhoneNumber = "098999990000";
                SellerInfo objSeller = new SellerInfo();
                objSeller.sellerAddressLine = "HN VN";
                objSeller.sellerBankAccount = "2345";
                objSeller.sellerBankName = "TPB";
                objSeller.sellerEmail = "sinvoice@viettel.vn";
                objSeller.sellerLegalName = "Bên bán";
                objSeller.sellerPhoneNumber = "180099999";
                objSeller.sellerTaxCode = codeTax;
                string paymentMethodName = "TM";
                List<ItemInfo> lstItem = new List<ItemInfo>();
                ItemInfo item = new ItemInfo();
                item.discount = "0.0";
                item.itemCode = "SP1";
                item.itemDiscount = "5000.0";
                item.itemName = "SP1";
                item.itemTotalAmountWithoutTax = "250000";
                item.lineNumber = "1";
                item.quantity = "10";
                item.taxAmount = "0.0";
                item.taxPercentage = 10;
                item.unitName = "kg";
                item.unitPrice = "25000";
                item.adjustmentTaxAmount = "1.0";
                item.isIncreaseItem = "true";
                lstItem.Add(item);

                string request = @"{    ""generalInvoiceInfo"": " +
                    @"{       ""invoiceType"":""" + objInvoice.invoiceType +
                    @""",       ""templateCode"":""" + objInvoice.templateCode +
                    @""", 	""invoiceSeries"":""" + objInvoice.invoiceSeries +
                    @""",       ""transactionUuid"": """ + objInvoice.uuId +
                    @""",       ""invoiceIssuedDate"":" + objInvoice.invoiceIssuedDate +
                    @",       ""currencyCode"":""" + objInvoice.currencyCode + @""", 
                    ""adjustmentType"":""" + objInvoice.adjustmentType + @""",  
                    ""adjustmentInvoiceType"":""" + objInvoice.adjustmentInvoiceType + @""",  
                    ""invoiceNote"":""" + objInvoice.invoiceNote + @""",  
                    ""originalInvoiceId"":""" + objInvoice.originalInvoiceId + @""",  
                    ""originalInvoiceIssueDate"":" + objInvoice.adjustmentType + @",  
                    ""additionalReferenceDesc"":""" + objInvoice.additionalReferenceDesc + @""",  
                    ""additionalReferenceDate"":" + objInvoice.additionalReferenceDate + @",  
                    ""cusGetInvoiceRight"":" + objInvoice.cusGetInvoiceRight + @",      
                    ""buyerIdNo"":""" + objInvoice.buyerIdNo + @""",        
                    ""buyerIdType"":""" + objInvoice.buyerIdType + @"""    },    
                    ""buyerInfo"":{       
                    ""buyerName"":""" + objBuyer.buyerName + @""",       
                    ""buyerLegalName"":""" + objBuyer.buyerLegalName + @""",       
                    ""buyerTaxCode"":""" + objBuyer.buyerTaxCode + @""",      
                    ""buyerAddressLine"":""" + objBuyer.buyerAddressLine + @""",       
                    ""buyerPhoneNumber"":""" + objBuyer.buyerPhoneNumber + @""",      
                    ""buyerEmail"":""" + objBuyer.buyerEmail + @""",      
                    ""buyerIdNo"":""" + objBuyer.buyerIdNo + @""",        
                    ""buyerIdType"":""" + objBuyer.buyerIdType + @"""    },    
                    ""sellerInfo"":{       
                    ""sellerLegalName"":""" + objSeller.sellerLegalName + @""",       
                    ""sellerTaxCode"":""" + objSeller.sellerTaxCode + @""",        
                    ""sellerAddressLine"":""" + objSeller.sellerAddressLine + @""",          
                    ""sellerPhoneNumber"":""" + objSeller.sellerPhoneNumber + @""",             
                    ""sellerEmail"":""" + objSeller.sellerEmail + @""",              
                    ""sellerBankName"":""" + objSeller.sellerBankName + @""",             
                    ""sellerBankAccount"":""" + objSeller.sellerBankAccount + @"""   },    
                    ""extAttribute"":[     ],    
                    ""payments"":[       {
                    ""paymentMethodName"":""" + paymentMethodName + @"""    }    ],    
                    ""deliveryInfo"":{     },    
                    ""itemInfo"":[       ";
                int indexItem = 1;
                foreach (var itemInfo in lstItem)
                {
                    request += @"{""lineNumber"":" + itemInfo.lineNumber + @",          
                    ""itemCode"":""" + itemInfo.itemCode + @""",    
                    ""itemName"":""" + itemInfo.itemName + @""",          
                    ""unitName"":""" + itemInfo.unitName + @""",     
                    ""unitPrice"":" + itemInfo.unitPrice + @",             
                    ""quantity"":" + itemInfo.quantity + @",               
                    ""itemTotalAmountWithoutTax"":" + itemInfo.itemTotalAmountWithoutTax + @",               
                    ""taxPercentage"":" + itemInfo.taxPercentage + @",              
                    ""taxAmount"":" + itemInfo.taxAmount + @",  
                    ""discount"":" + itemInfo.discount + @",                
                    ""itemDiscount"":" + itemInfo.itemDiscount + @"     }";
                    if (indexItem > 1)
                        request += " , ";
                    indexItem++;
                }
                request += @"],    
                    ""discountItemInfo"":[     ],   
                    ""meterReading"": [],    
                    ""summarizeInfo"":{   },    
                    ""taxBreakdowns"":[        ] } ";
                string result = CreateRequest.webRequest(apiLink, request, autStr, "POST", contentType);
                MessageBox.Show("OK " + result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("NOK " + ex.Message);
            }
        }
    }
}
