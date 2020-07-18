using Comestics.Areas.Admin.Data;
using Comestics.Common;
using Models.DAO;
using Models.EFrame;
using System;
using System.Web.Mvc;

namespace Comestics.Areas.Admin.Controllers
{
    [Authorize]
    public class AccountCustomerController : Controller
    {
        // GET: Admin/AccountCustomer
        [HasCredential(RoleId = "VIEW_USER")]
        public ActionResult Index()
        {
            var account = new CustomerDAO();
            var listacc = account.Acc();
            return View(listacc);
        }
        [HasCredential(RoleId = "ADD_USER")]
        public ActionResult AddNewCustomer()
        {
            return View();
        }
        [HttpPost]
        [HasCredential(RoleId = "ADD_USER")]
        public ActionResult AddNewCustomer(AddNewAccModel khach)
        {
            if (khach.username != null && khach.password != null && khach.email != null)
            {
                var ac = new Customer()
                {
                    username = khach.username,
                    password = khach.password,
                    email = khach.email,
                    status = 1
                };
                var them = new CustomerDAO().AddNewCustomer(ac);
            }
            else if (khach.username == "")
            {
                ModelState.AddModelError("", "Bạn không được bỏ trống phần User");
                return View();
            }
            else if (khach.password == "")
            {
                ModelState.AddModelError("", "Bạn không được bỏ trống phần Password");
                return View();
            }
            else if (khach.email == "")
            {
                ModelState.AddModelError("", "Bạn không được bỏ trống phần Email");
                return View();
            }


            return RedirectToAction("Index");
        }
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult EditCustomer(int id)
        {
            ViewBag.TimTaiKhoan = new CustomerDAO().ViewDetail(id);
            return View();
        }
        [HttpPost]
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult EditCustomer(EditAccModel edit)
        {
            var customer = new Customer()
            {
                customerId = Convert.ToInt32(edit.id),
                email = edit.email,
                status = edit.status
            };

            var user = new CustomerDAO().UpdateCustomer(customer);
            if (user == true)
            {
                return RedirectToAction("Index");
            }
            ViewBag.TimTaiKhoan = new CustomerDAO().ViewDetail(Convert.ToInt32(edit.id));
            return View();

        }
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult Change(int id)
        {
            var change = new CustomerDAO().ChangeCustomer(id);
            if (change == true)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HasCredential(RoleId = "DELETE_USER")]
        public ActionResult DeleteCustomer(int id)
        {
            var del = new CustomerDAO().DeleteCustomer(id);
            if (del == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult EditAccountMe()
        {
            var user = Session[CommonConstants.User_SESSION] as UserSession;
            ViewBag.TimTaiKhoan = new CustomerDAO().ViewDetail(user.UserID);
            return View();
        }
        [HttpPost]
        [HasCredential(RoleId = "EDIT_USER")]
        [HasCredential(RoleId = "EDIT_CONTENT")]
        public ActionResult EditAccountMe(EditAccModel edit)
        {
            var customer = new Customer()
            {
                customerId = Convert.ToInt32(edit.id),
                email = edit.email,
                status = edit.status
            };

            var user = new CustomerDAO().UpdateCustomer(customer);
            if (user == true)
            {
                return RedirectToAction("Index");
            }
            ViewBag.TimTaiKhoan = new CustomerDAO().ViewDetail(Convert.ToInt32(edit.id));
            return View();
        }

        [HttpPost]
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult SearchCustomer(string customerName)
        {
            ViewBag.SearchCustomer = new CustomerDAO().Search(customerName);
            return View();
        }
    }
}