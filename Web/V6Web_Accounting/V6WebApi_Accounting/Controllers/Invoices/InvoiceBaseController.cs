using System.Web.Http;
using DataAccessLayer.Interfaces;
using V6Structs;
using V6Tools.V6Convert;

namespace V6Soft.WebApi.Accounting.Controllers.Invoices
{
    [RoutePrefix("api/invoicebase")]
    [Authorize]
    public class InvoiceBaseController : ApiController
    {
        private IInvoiceServices Services;
        public InvoiceBaseController(IInvoiceServices services)
        {
            Services = services;
        }

        [HttpGet]
        [Route("GetAlnt")]
        public IHttpActionResult GetAlnt()
        {
            var result = Services.GetAlnt();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAlct")]
        public IHttpActionResult GetAlct(string mact)
        {
            var result = Services.GetAlct(mact);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAlct1")]
        public IHttpActionResult GetAlct1(string mact)
        {
            var result = Services.GetAlct1(mact);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAlct3")]
        public IHttpActionResult GetAlct3(string mact)
        {
            var result = Services.GetAlct3(mact);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAlPost")]
        public IHttpActionResult GetAlPost(string mact)
        {
            var result = Services.GetAlPost(mact);
            return Ok(result);
        }

        [HttpPost]
        [Route("PostErrorLog")]
        public IHttpActionResult PostErrorLog(string mact, string sttRec, string mode, string message)
        {
            try
            {
                Services.PostErrorLog(mact, sttRec, mode, message);
                return Ok(new SimpleResult
                {
                    Status = 1,
                    IntValue = 1
                });
            }
            catch (System.Exception)
            {
                return Ok(new SimpleResult
                {
                    Status = 0,
                    IntValue = 0
                });
            }
        }

        /// <summary>
        /// Lấy tỷ giá ngoại tệ.
        /// </summary>
        /// <param name="mact"></param>
        /// <param name="ngayct">Định dạng dd/MM/yyyy</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTyGia")]
        public IHttpActionResult GetTyGia(string mact, string ngayct)
        {
            try
            {
                var result = Services.GetTyGia(mact, ObjectAndString.StringToDate(ngayct));
                return Ok(new SimpleResult
                {
                    Status = 1,
                    DecimalValue = result
                });
            }
            catch (System.Exception ex)
            {
                return Ok(new SimpleResult
                {
                    Status = 0,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("GetCheck_VC")]
        public IHttpActionResult GetCheck_VC(string status, string kieu_post, string stt_rec)
        {
            string s;
            var result = Services.GetCheck_VC(status, kieu_post, stt_rec, out s);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetCheck_VC_Save")]
        public IHttpActionResult GetCheck_VC_Save(string status, string kieu_post, string soct, string masonb, string stt_rec)
        {
            string s;
            var result = Services.GetCheck_VC_Save(status, kieu_post, soct, masonb, stt_rec, out s);
            return Ok(result);
        }

    }
}
