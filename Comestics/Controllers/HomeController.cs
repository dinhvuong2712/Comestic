using Comestics.Models;
using Models.DAO;
using System;
using System.Web.Mvc;

namespace Comestics.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Category = new CategoryDAO().ListAll();
            ViewBag.Product = new ProductDAO().ListTop(4);
            ViewBag.News = new NewsDAO().ListAll();

            var cart = Session["Cart"];
            if (cart != null)
            {
                return View();
            }
            else
            {
                ViewBag.Message = "Error";
                return View();
            }
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactModel contactModel)
        {
            try
            {
                ContactDAO contactDAO = new ContactDAO();
                int res = contactDAO.AddContact(contactModel.FirstName, contactModel.LastName,
                    Convert.ToInt32(contactModel.Phone), contactModel.Email, contactModel.Address);
                if (res > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Gửi thành công");
                    return View();
                }
            }
            catch(System.Exception ex)
            {
                var x = ex.Message;
                return View();
            }
        }
    }
}