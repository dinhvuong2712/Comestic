using Comestics.Areas.Admin.Data;
using Comestics.Common;
using Models.DAO;
using Models.EFrame;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Comestics.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryAdminController : Controller
    {
        // GET: Admin/CategoryAdmin
        [HasCredential(RoleId = "VIEW_USER")]
        public ActionResult Index(int? currentPage, int pageSize = 1)
        {
            ViewBag.PageListCATE = new CategoryDAO().PageListCategory(currentPage, pageSize);
            return View();
        }
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult DetailLSP(int id)
        {
            ViewBag.DetailLSP = new CategoryDAO().FindCategory(id);
            return View();
        }
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult UpdateLSP(int id)
        {
            ViewBag.UpdateLSP = new CategoryDAO().FindCategory(id);
            return View();
        }
        [HttpPost]
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult UpdateLSP(UpdateCategoryModel update)
        {
            var findcategory = new CategoryDAO().FindCategory(update.Id);
            string img;
            if (update.Images != null)
            {
                img = update.Images;
            }
            else
            {
                img = findcategory.cateImage;
            }
            var category = new Category()
            {
                cateId = update.Id,
                cateName = update.Name,
                cateImage = img
            };
            var updatenew = new CategoryDAO().UpdateLSP(category);
            return RedirectToAction("Update", new { id = updatenew.cateId });
        }

        [HasCredential(RoleId = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            var del1 = new CategoryDAO().Delete(id);
            return RedirectToAction("Index", new { currentPage = 1 });
        }
    }
}