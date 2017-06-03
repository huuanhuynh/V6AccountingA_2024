using System.Web.Http;
using V6Soft.Accounting.NhomKhachHang.Dealers;
using V6Soft.Models.Accounting.ViewModels.CustomerGroup;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;

using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("nhomkhachhangs")]
    public class NhomKhachHangController : V6ApiControllerBase
    {
        private readonly INhomKhachHangDataDealer m_Dealer;

        public NhomKhachHangController(INhomKhachHangDataDealer dealer)
        {
            m_Dealer = dealer;
        }

        [HttpGet]
        public IHttpActionResult Ping()
        {
            object result;
            if (m_Dealer != null)
            {
                result = new { Resolved = true };
            }
            else
            {
                result = new { Resolved = false };
            }
            return Ok(result);
        }

        public object Dump()
        {
            object result = new { Name = "dumper", Age = 18 };
            return result;
        }

        [HttpPost]
        [Route("list")]
        public PagedSearchResult<CustomerGroupListItem> GetNhomKhachHangs(SearchCriteria criteria)
        {
            PagedSearchResult<CustomerGroupListItem> result =
                m_Dealer.GetNhomKhachHangs(criteria);

            return result;
        }
        [HttpPost]
        [Route("")]
        public IHttpActionResult AddNhomKhachHang(CustomerGroupDetail addedNhomKhachHangItem)
        {
            addedNhomKhachHangItem.UID = NextUID;
            var result = m_Dealer.AddNhomKhachHang(addedNhomKhachHangItem);
            return Ok(result);
        }
        
    }//end class
}