using Models.DAO;
using System.Web.Mvc;

namespace Comestics.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int currentpage, int pagesize = 1)
        {
            ViewBag.Title = "Danh sách sản phẩm";
            ViewBag.ListPR = new ProductDAO().PageList(currentpage,pagesize);
            return View();
        }
        public ActionResult Detail(long productId)
        {
            var find = new ProductDAO().FindProduct(productId);
            ViewBag.ProductId = find;
            ViewBag.Category = new CategoryDAO().FindCategory(find.cateId);
            ViewBag.Producer = new ProducerDAO().FindProducer(find.prodId);
            ViewBag.CaletedPr = new ProductDAO().ProductCategoryDifferent(productId);
            ViewBag.News = new NewsDAO().ListAll();
            return View();
        }
    }
}