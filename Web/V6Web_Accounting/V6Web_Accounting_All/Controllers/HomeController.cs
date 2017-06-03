using System;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers
{
    public class HomeController : Controller
    {

        //
        // GET: /Home/Index or /
        public async Task<ActionResult> Index()
        {
            await Task.Yield();

            return View();
        }
        
        //
        // GET: /Home/Dashboard or /Dashboard
        public ActionResult Dashboard()
        {            
            //return PartialView("~/Views/Home/_Dashboard.cshtml");
            return PartialView();
        }

        //
        // GET: /Home/Header or /Header
        public ActionResult Header()
        {
            return PartialView("~/Views/Shared/_Header.cshtml");
        }

        //
        // GET: /Home/Footer or /Footer
        public ActionResult Footer()
        {
            return PartialView("~/Views/Shared/_Footer.cshtml");
        }

        //
        // GET: /Home/PageHead or /PageHead
        public ActionResult PageHead()
        {
            return PartialView("~/Views/Shared/_Page-head.cshtml");
        }

        //
        // GET: /Home/Header or /Header
        public ActionResult Webix()
        {
            return PartialView("~/Views/Home/Webix.cshtml");
        }
        public ActionResult Test()
        {
            return PartialView("~/Views/Home/test.cshtml");
        }
      
        public ActionResult Customer()
        {
            return PartialView("~/Views/Home/Customer.cshtml");
        }
        public ActionResult Login()
        {
            return PartialView("~/Views/Home/Login.cshtml");
        }
        public ActionResult PhieuNhapChiPhiMuaHang()
        {
            return PartialView("~/Views/Home/PhieuNhapChiPhiMuaHang.cshtml");
        }
        public ActionResult HoaDonMuaHangDichVu()
        {
            return PartialView("~/Views/Home/HoaDonMuaHangDichVu.cshtml");
        }
        public ActionResult HangTraLai()
        {
            return PartialView("~/Views/Home/HangTraLai.cshtml");
        }
        public ActionResult HoaDonDichVu()
        {
            return PartialView("~/Views/Home/HoaDonDichVu.cshtml");
        }
        public ActionResult PhieuNhapKhau()
        {
            return PartialView("~/Views/Home/PhieuNhapKhau.cshtml");
        }
        public ActionResult PhieuThanhToanTamUng()
        {
            return PartialView("~/Views/Home/PhieuThanhToanTamUng.cshtml");
        }
        public ActionResult TestWebix()
        {
            return PartialView("~/Views/Home/TestWebix.cshtml");
        }
    }
}
