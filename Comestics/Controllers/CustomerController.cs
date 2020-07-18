using Comestics.Models;
using System.Web.Mvc;

namespace Comestics.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            ViewBag.Title = "Tạo tài khoản";
            return View();
        }
        [HttpPost]
        public ActionResult CreateAccount(CreateAccountModel createAccount)
        {
            if (ModelState.IsValid)
            {
                if (createAccount.confirmPassword != createAccount.address)
                {
                    ModelState.AddModelError("confirmPassword", "Bạn đã nhập sai 1 ký tự nào đó! Mời bạn nhập lại");
                }
            }
            return RedirectToAction("Index");
        }
    }
}