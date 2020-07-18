using Comestics.Common;
using Models.DAO;
using Models.EFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Comestics.Areas.Admin.Controllers
{
    [Authorize]
    public class ProducerAdminController : Controller
    {
        [HasCredential(RoleId = "VIEW_USER")]
        // GET: Admin/Producer
        public ActionResult Index(int? currentPage, int pageSize = 1)
        {
            ViewBag.PageListPR = new ProducerDAO().PageList(currentPage, pageSize);
            return View();
        }
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult Detail(int id)
        {
            ViewBag.ListDL = new ProducerDAO().FindProducer(id);
            return View();
        }
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult Update(int id)
        {
            ViewBag.ListDL = new ProducerDAO().FindProducer(id);
            return View();
        }
        [HttpPost]
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult Update(Producer producer)
        {
            var Upp = new ProducerDAO().Update1(producer);
            if (Upp == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult Delete(int id)
        {
            var del1 = new ProducerDAO().Delete(id);
            return RedirectToAction("Index", new { currentPage = 1 });
        }
    }
}