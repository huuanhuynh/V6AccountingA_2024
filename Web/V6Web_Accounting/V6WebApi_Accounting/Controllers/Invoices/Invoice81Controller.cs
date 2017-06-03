using System;
using System.Collections.Generic;
using System.Web.Http;
using DataAccessLayer.Interfaces.Invoices;
using V6SqlConnect;
using V6Structs;
using V6Tools.V6Convert;

namespace V6Soft.WebApi.Accounting.Controllers.Invoices
{
    [RoutePrefix("api/invoice81")]
    //[Authorize]
    public class Invoice81Controller : ApiController
    {
        private IInvoice81Services Services;
        public Invoice81Controller(IInvoice81Services service)
        {
            Services = service;
        }

        [HttpPost]
        [Route("InsertInvoice")]
        public IHttpActionResult InsertInvoice(V6InvoiceData invoiceData)
        {
            var userId = this.GetUserId();
            var amStruct = V6SqlconnectHelper.GetTableStruct(invoiceData.TableNameAM);
            var adStruct = V6SqlconnectHelper.GetTableStruct(invoiceData.TableNameAD);
            var adStruct3 = V6SqlconnectHelper.GetTableStruct(invoiceData.TableNameAD3);
            var amData = invoiceData.DataAM;
            var adList = invoiceData.ADList;
            var adList3 = invoiceData.ADList3;
            string message = "";
            var result = Services.InsertInvoice(userId, amStruct, adStruct, adStruct3,
                amData, adList, adList3, out message);
            
            return Ok(new SimpleResult
            {
                Status = result?1:0,
                IntValue = result ? 1 : 0,
                Message = message
            });
        }

        [HttpPost]
        [Route("UpdateInvoice")]
        public IHttpActionResult UpdateInvoice(V6InvoiceData invoiceData)
        {
            var userId = this.GetUserId();
            var amStruct = V6SqlconnectHelper.GetTableStruct(invoiceData.TableNameAM);
            var adStruct = V6SqlconnectHelper.GetTableStruct(invoiceData.TableNameAD);
            var adStruct3 = V6SqlconnectHelper.GetTableStruct(invoiceData.TableNameAD3);
            var amData = invoiceData.DataAM;
            var adList = invoiceData.ADList;
            var adList3 = invoiceData.ADList3;
            var keys = invoiceData.UpdateKeys;
            string message = "";
            var result = Services.UpdateInvoice(userId, amStruct, adStruct, adStruct3,
                amData, adList, adList3, keys, out message);
            return Ok(new SimpleResult
            {
                Status = result ? 1 : 0,
                IntValue = result ? 1 : 0,
                Message = message
            });
        }

        [HttpPost]
        [Route("SearchAM")]
        public IHttpActionResult SearchAM(Dictionary<string, string> body)
        {
            var result = Services.SearchAM(body["amName"], body["adName"], body["mact"],
                body["where0Ngay"], body["where1AM"], body["where2AD"], body["where3NhVt"], body["where4Dvcs"]);
            return Ok(result);
        }

        [HttpGet]
        [Route("LoadAd81")]
        public IHttpActionResult LoadAd81(string AD, string sttRec)
        {
            var result = Services.LoadAd81(AD, sttRec);
            return Ok(result);
        }

        [HttpGet]
        [Route("LoadAD3")]
        public IHttpActionResult LoadAD3(string AD3, string sttRec)
        {
            var result = Services.LoadAD3(AD3, sttRec);
            return Ok(result);
        }

        [HttpGet]
        [Route("DeleteInvoice")]
        public IHttpActionResult DeleteInvoice(string mact, string sttRec)
        {
            try
            {
                var userId = this.GetUserId();
                var result = Services.DeleteInvoice(userId, mact, sttRec);
                return Ok(new SimpleResult
                {
                    Status = 1,
                    IntValue = result?1:0
                });
            }
            catch (Exception ex)
            {
                return Ok(new SimpleResult
                {
                    Status = 0,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("GetGiaBan")]
        public IHttpActionResult GetGiaBan(string field, string mact, string ngayct, string mant, string mavt, string dvt1, string makh,
            string magia)
        {
            var ngay_ct = ObjectAndString.StringToDate(ngayct);
            var result = Services.GetGiaBan(field, mact, ngay_ct, mant, mavt, dvt1, makh, magia);
            return Ok(result.Table);
        }

        [HttpGet]
        [Route("GetLoDate")]
        public IHttpActionResult GetLoDate(string mavt, string makho, string sttRec, string ngayct)
        {
            var ngay_ct = ObjectAndString.StringToDate(ngayct);
            var result = Services.GetLoDate(mavt, makho, sttRec, ngay_ct);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetLoDate13")]
        public IHttpActionResult GetLoDate13(string mavt, string makho, string malo, string sttRec, string ngayct)
        {
            var ngay_ct = ObjectAndString.StringToDate(ngayct);
            var result = Services.GetLoDate13(mavt, makho, malo, sttRec, ngay_ct);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetStock")]
        public IHttpActionResult GetStock(string mact, string mavt, string makho, string sttRec, string ngayct)
        {
            var ngay_ct = ObjectAndString.StringToDate(ngayct);
            var result = Services.GetStock(mact, mavt, makho, sttRec, ngay_ct);
            return Ok(result);
        }

    }
}
