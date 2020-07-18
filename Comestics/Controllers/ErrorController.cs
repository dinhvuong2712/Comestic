using System.Web.Mvc;

namespace Comestics.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NotFound()
        {
            ViewBag.Title = "404";
            ViewBag.Message = "Không thể tìm thấy trang!";
            return PartialView("_Erorr");
        }
        public ActionResult Unauthorized()
        {
            ViewBag.Title = "401";
            ViewBag.Message = "Bạn bị từ trối truy cập trang này!";
            return PartialView("_Erorr");
        }
        public ActionResult Forbidden()
        {
            ViewBag.Title = "403";
            ViewBag.Message = "Hiện tại không thể truy cập vào tranh này!";
            return PartialView("_Erorr");
        }
        public ActionResult Request_Timeout()
        {
            ViewBag.Title = "408";
            ViewBag.Message = "Hiện tại máy chủ đang quá tải! Hãy load lại trang.";
            return PartialView("_Erorr");
        }
        public ActionResult Internal_Server_Error()
        {
            ViewBag.Title = "500";
            ViewBag.Message = "Máy chủ đang gặp sự cố!";
            return PartialView("_Erorr");
        }
        public ActionResult Not_Implemented()
        {
            ViewBag.Title = "501";
            ViewBag.Message = "Máy chủ không thể thực hiện được yêu cầu!";
            return PartialView("_Erorr");
        }
        public ActionResult Service_Unavailable()
        {
            ViewBag.Title = "503";
            ViewBag.Message = "Trang không có sẵn!";
            return PartialView("_Erorr");
        }
        public ActionResult Locked()
        {
            ViewBag.Title = "423";
            ViewBag.Message = "Bạn không có quyền truy cập!";
            return PartialView("_Erorr");
        }
        public ActionResult LockedAccount()
        {
            ViewBag.Message = "Tài khoản của bạn đã bị khóa!";
            return PartialView("_Erorr");
        }
        public ActionResult NotAccount()
        {
            ViewBag.Message = "Bạn chưa đăng nhập!";
            return PartialView("_Erorr");
        }
        public ActionResult AccountError()
        {
            ViewBag.Message = "Đăng nhập thất bại! Hãy kiểm tra lại tài khoản hoặc mật khẩu!";
            return PartialView("_Erorr");
        }
    }
}