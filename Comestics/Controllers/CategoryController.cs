using Models.DAO;
using System.Web.Mvc;

namespace Comestics.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(long cateId)
        {
            var find = new CategoryDAO().FindCategory(cateId);
            ViewBag.ProductCate = new ProductDAO().ProductCategory(cateId);
            ViewBag.News = new NewsDAO().ListAll();
            return View(find);
        }
    }
}