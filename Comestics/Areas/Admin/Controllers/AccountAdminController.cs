using Comestics.Areas.Admin.Data;
using Comestics.Common;
using Models.DAO;
using Models.EFrame;
using System;
using System.Web.Mvc;

namespace Comestics.Areas.Admin.Controllers
{
    [Authorize]
    public class AccountAdminController : Controller
    {
        // GET: Admin/AccountAdmin
        [HasCredential(RoleId = "VIEW_USER")]
        public ActionResult Index()
        {
            var admin = new AccountAdminDAO();
            var listad = admin.Ad();
            return View(listad);
        }
        [HasCredential(RoleId = "ADD_USER")]
        public ActionResult AddNewAdmin()
        {
            return View();
        }
        [HttpPost]
        [HasCredential(RoleId = "ADD_USER")]
        public ActionResult AddNewAdmin(AddNewAdModel taikhoan)
        {

            if (taikhoan.confirmPassword == taikhoan.password)
            {
                var db = new Quantri()
                {
                    username = taikhoan.username,
                    password = taikhoan.password,
                    status = 1
                };
                var them = new AccountAdminDAO().AddNewAdmin(db);
            }
            else if (taikhoan.username == "")
            {
                ModelState.AddModelError("", "Bạn không được bỏ trống phần User");
            }
            else if (taikhoan.password == "")
            {
                ModelState.AddModelError("", "Bạn không được bỏ trống phần User");
            }

            return RedirectToAction("Index");
        }
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult EditAdmin(int id)
        {
            ViewBag.TimTaiKhoan = new AccountAdminDAO().ViewDetail(id);
            return View();

        }
        [HttpPost]
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult EditAdmin(EditAdModel edit)
        {
            var accountad = new Quantri()
            {
                adId = Convert.ToInt32(edit.id),
                password = edit.email
            };

            var user = new AccountAdminDAO().UpdateAdmin(accountad);
            if (user == true)
            {
                return RedirectToAction("Index");
            }
            ViewBag.TimTaiKhoan = new AccountAdminDAO().ViewDetail(Convert.ToInt32(edit.id));
            return View();

        }

        [HasCredential(RoleId = "DELETE_USER")]
        public ActionResult DeleteAdmin(int obj)
        {
            var del = new AccountAdminDAO().DeleteAccount(obj);
            if (del == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [HasCredential(RoleId = "DELETE_USER")]
        public ActionResult SearchAdmin(string AdminName)
        {
            ViewBag.SearchCustomer = new AccountAdminDAO().Search(AdminName);
            return View();
        }
    }
}