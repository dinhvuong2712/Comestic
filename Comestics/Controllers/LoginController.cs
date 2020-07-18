using Comestics.Common;
using Comestics.Models;
using Models.DAO;
using Models.EFrame;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace Comestics.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User customers)
        {
            if (Membership.ValidateUser(customers.Username, customers.Password) && ModelState.IsValid)
            {
                var user = new UserDAO();
                var result = user.LoginCheck(customers.Username, customers.Password, true);
                var login = user.UserLogin(customers.Username);

                if (result == 1)
                {
                    FormsAuthentication.SetAuthCookie(customers.Username, customers.RememberMe);
                    var usersession = new UserSession()
                    {
                        UserID = login.customerId,
                        UserName = login.username,
                        GroupId = login.GroupId
                    };
                    var getcredential = user.GetListCredential(customers.Username);
                    Session.Add(CommonConstants.Credential_User, getcredential);
                    Session.Add(CommonConstants.User_SESSION, usersession);
                    if (Session[CommonConstants.Cart_Session] == null)
                    {
                        Session[CommonConstants.Cart_Session] = new List<CartSessionModel>();
                    }
                    List<CartSessionModel> cartSessions = Session[CommonConstants.Cart_Session] as List<CartSessionModel>;
                    var userLogin = Session[CommonConstants.User_SESSION] as UserSession;
                    if (userLogin.GroupId == "MEMBER")
                    {
                        return RedirectToAction("Locked", "Error");
                    }
                    /*Tạo cart*/
                    /*Khi đã đăng nhập*/
                    
                    var findcart = new DetailCartDAO().FindList(userLogin.UserID,1);
                    if (userLogin != null)
                    {
                        if (cartSessions != null || cartSessions.Count != 0)
                        {
                            if (findcart != null || findcart.Count != 0)
                            {
                                foreach (var item in cartSessions)
                                {
                                    var findPr = new ProductDAO().FindProduct(Convert.ToUInt32(item.PrId));
                                    DetailCart detailCart = new DetailCart()
                                    {
                                        customerId = userLogin.UserID,
                                        prId = findPr.prId,
                                        price = findPr.price,
                                        amounts = item.Amounts,
                                        status = 1
                                    };
                                    var addCart = new DetailCartDAO().AddDetailOrder(detailCart);
                                }
                                cartSessions.Clear();
                                foreach (var itemcart in findcart)
                                {
                                    var find = new ProductDAO().FindProduct(Convert.ToUInt32(itemcart.prId));
                                    CartSessionModel cartModel = new CartSessionModel()
                                    {
                                        PrId = itemcart.prId,
                                        PrName = find.prName,
                                        Price = find.price,
                                        Amounts = itemcart.amounts,
                                        AmountsMax = find.amount,
                                        Images = find.images
                                    };
                                    cartSessions.Add(cartModel);
                                }
                                return Redirect("~/Admin/CartCustomer/Index");
                            }
                            return Redirect("~/Admin/CartCustomer/Index");
                        }
                        else
                        {
                            foreach (var item in findcart)
                            {
                                var findPr = new ProductDAO().FindProduct(Convert.ToUInt32(item.prId));
                                CartSessionModel cartModel = new CartSessionModel()
                                {
                                    PrId = item.prId,
                                    PrName = findPr.prName,
                                    Price = findPr.price,
                                    Amounts = item.amounts,
                                    AmountsMax = findPr.amount,
                                    Images = findPr.images
                                };
                                cartSessions.Add(cartModel);
                            }
                            return Redirect("~/Admin/CartCustomer/Index");
                        }
                    }
                    /*Hết truy xuất giỏ hàng*/

                    return Redirect("~/Admin/CartCustomer/Index");
                }
                else if(result == 0)
                {
                    return RedirectToAction("LockedAccount", "Error");
                }
                else if (result == -1)
                {
                    return RedirectToAction("AccountError", "Error");
                }
                else if (result == -2)
                {
                    return RedirectToAction("AccountError", "Error");
                }
                else if (result == -3)
                {
                    var usersession = new UserSession()
                    {
                        UserID = login.customerId,
                        UserName = login.username,
                        GroupId = login.GroupId
                    };
                    Session.Add(CommonConstants.User_SESSION, usersession);
                    ModelState.AddModelError("", "Tài khoản của bạn không có quyền truy cập.");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Server không phản hồi");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session[CommonConstants.User_SESSION] = null;
            List<CartSessionModel> cartSessions = Session[CommonConstants.Cart_Session] as List<CartSessionModel>;
            if(cartSessions != null)
            {
                cartSessions.Clear();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}