using Comestics.Common;
using Models.DAO;
using System.Web.Mvc;

namespace Comestics.Areas.Admin.Controllers
{
    [Authorize]
    public class CartCustomerController : Controller
    {
        // GET: Admin/CartCustomer
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult Index(int? currentPage,int pageSize = 5)
        {
            ViewBag.Title = "Đơn hàng";
            ViewBag.ListDetail = new DetailCartDAO().ListDetail(currentPage, pageSize);
            return View();
        }
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult ChangeDetailCart(string changeId,int id)
        {
            var change = new DetailCartDAO().ChangeDetailCart(changeId, id);
            if(change == 1)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}