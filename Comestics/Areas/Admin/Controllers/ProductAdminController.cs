using Comestics.Areas.Admin.Data;
using Comestics.Common;
using Models.DAO;
using Models.EFrame;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Comestics.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductAdminController : Controller
    {
        // GET: Admin/ProductAdmin
        [HasCredential(RoleId = "VIEW_USER")]
        public ActionResult Index(int? currentPage,int pageSize = 1)
        {
            ViewBag.Title = "Danh sách sản phẩm";
            ViewBag.PageListPR = new ProductDAO().PageList(currentPage, pageSize);
            return View();
        }
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult Detail(long id)
        {
            var find = new ProductDAO().FindProduct(id);
            ViewBag.Title = "Chi tiết sản phẩm";
            ViewBag.ProductId = find;
            ViewBag.Category = new CategoryDAO().FindCategory(find.cateId);
            ViewBag.Producer = new ProducerDAO().FindProducer(find.prodId);
            ViewBag.CaletedPr = new ProductDAO().ProductCategoryDifferent(id);
            ViewBag.CategoryList = new CategoryDAO().ListAll();
            return View();

        }
        [HasCredential(RoleId = "EDIT_USER")]
        [HasCredential(RoleId = "EDIT_CONTENT")]
        public ActionResult AddProduct()
        {
            ViewBag.Producer = new ProducerDAO().ListAll();
            ViewBag.Category = new CategoryDAO().ListAll();
            return View();
        }
        [HttpPost]
        [HasCredential(RoleId = "EDIT_USER")]
        [HasCredential(RoleId = "EDIT_CONTENT")]
        public ActionResult AddProduct(HttpPostedFileBase fileBase, Product product)
        {
            if (product != null && fileBase != null)
            {

                string fileName = Path.GetFileNameWithoutExtension(fileBase.FileName);
                fileName += "_" + product.prId;
                fileName += Path.GetExtension(fileBase.FileName);

                string folderPath = Server.MapPath("~") + @"Images\product";

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string path = Path.Combine(folderPath, fileName);

                fileBase.SaveAs(path);

                product.images = fileName;
                product.status = 1;
                var addpr = new ProductDAO().AddPr(product);
                if (addpr != null) { 
                    return RedirectToAction("Detail", new { id = addpr.prId});
                }
                ViewBag.Category = new CategoryDAO().ListAll();
                ViewBag.Producer = new ProducerDAO().ListAll();
                return View();
            }
            else
            {
                return View();
            }
        }
        [HasCredential(RoleId = "EDIT_USER")]
        [HasCredential(RoleId = "EDIT_CONTENT")]
        public ActionResult Update(long id)
        {
            var find = new ProductDAO().FindProduct(id);
            ViewBag.Title = "Sửa sản phẩm";
            ViewBag.ProductId = find;
            ViewBag.Category = new CategoryDAO().FindCategory(find.cateId);
            ViewBag.Producer = new ProducerDAO().FindProducer(find.prodId);
            ViewBag.CaletedPr = new ProductDAO().ProductCategoryDifferent(id);
            ViewBag.CategoryList = new CategoryDAO().ListAll();
            return View();
        }
        [HttpPost]
        [HasCredential(RoleId = "EDIT_USER")]
        [HasCredential(RoleId = "EDIT_CONTENT")]
        public ActionResult Update(HttpPostedFileBase fileBase,UpdateProductModel updateProduct)
        {
            var find = new ProductDAO().FindProduct(updateProduct.Id);
            string images;
            if (fileBase.ContentLength > 0)
            {
                string fileName = Path.GetFileNameWithoutExtension(fileBase.FileName);
                fileName += "_" + updateProduct.Id;
                fileName += Path.GetExtension(fileBase.FileName);

                string folderPath = Server.MapPath("~") + @"Images\product";

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string path = Path.Combine(folderPath, fileName);

                fileBase.SaveAs(path);
                images = fileName;
            }
            else
            {
                images = find.images;
            }
            var product = new Product()
            {
                prId = updateProduct.Id,
                prName = updateProduct.Name,
                images = images,
                cateId = updateProduct.Category,
                prodId = find.prodId,
                description = updateProduct.Description,
                price = updateProduct.Price,
                amount = updateProduct.Amounts
            };
            var dao = new ProductDAO().UpdateProduct(product);
            if (dao != null)
            {
                ViewBag.ProductId = dao;
                ViewBag.Category = new CategoryDAO().FindCategory(dao.cateId);
                ViewBag.Producer = new ProducerDAO().FindProducer(dao.prodId);
                ViewBag.CaletedPr = new ProductDAO().ProductCategoryDifferent(dao.prId);
                ViewBag.CategoryList = new CategoryDAO().ListAll();

                return RedirectToAction("Update", new { id = dao.prId });
            }
            return RedirectToAction("NotFound", "Error");
        }
    }
}