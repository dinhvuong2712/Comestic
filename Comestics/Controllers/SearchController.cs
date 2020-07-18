using Comestics.Models;
using Models.DAO;
using System.Web.Mvc;

namespace Comestics.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult SearchWrite()
        {
            return View();
        }
        public ActionResult SearchList()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchList(SearchModel search)
        {
            ViewBag.Title = "Sản phẩm vừa tìm";
            ViewBag.SearchList = new ProductDAO().Search(search.keyword, search.currentPage, 10);
            return View();
        }
    }
}