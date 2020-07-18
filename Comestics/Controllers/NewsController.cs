using Models.DAO;
using System.Web.Mvc;

namespace Comestics.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            ViewBag.NewsList = new NewsDAO().ListAll();
            return View();
        }
        public ActionResult News(long newsId)
        {
            if(newsId == 0)
            {
                return View("Index");
            }
            else
            {
                ViewBag.NewsId = new NewsDAO().ListId(newsId);
                return View();
            }
        }
    }
}