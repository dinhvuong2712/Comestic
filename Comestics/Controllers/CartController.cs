using Comestics.Common;
using Comestics.Models;
using Models.DAO;
using Models.EFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Comestics.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            if (Session[CommonConstants.Cart_Session] == null)
            {
                Session[CommonConstants.Cart_Session] = new List<CartSessionModel>();
            }
            List<CartSessionModel> cartSessions = Session[CommonConstants.Cart_Session] as List<CartSessionModel>;
            var user = Session[CommonConstants.User_SESSION] as UserSession;
            if (user != null)
            {
                var findcart = new DetailCartDAO().FindList(user.UserID,1);
                if (cartSessions != null && cartSessions.Count != 0)
                {
                    cartSessions.Clear();
                    if (findcart != null || findcart.Count != 0)
                    {
                        foreach(var item in cartSessions)
                        {
                            var findPr = new ProductDAO().FindProduct(Convert.ToUInt32(item.PrId));
                            DetailCart detailCart = new DetailCart()
                            {
                                customerId = user.UserID,
                                prId = findPr.prId,
                                price = findPr.price,
                                amounts = item.Amounts,
                                status = 1
                            };
                            var addCart = new DetailCartDAO().AddDetailOrder(detailCart);
                        }
                        foreach (var itemcart in findcart)
                        {
                            CartSessionModel cartModel = new CartSessionModel()
                            {
                                IdCart = itemcart.id,
                                PrId = itemcart.prId,
                                PrName = itemcart.Product.prName,
                                Price = itemcart.Product.price,
                                Amounts = itemcart.amounts,
                                AmountsMax = itemcart.Product.amount,
                                Images = itemcart.Product.images
                            };
                            cartSessions.Add(cartModel);
                        }
                        return View();
                    }
                    ViewBag.Title = "Giỏ hàng";
                    ViewBag.StatusCart = "0";
                    ViewBag.Message = "Hiện tại không có sản phẩm trong giỏ";
                    ViewBag.MessageOrder = "Hãy quay lại trang sản phẩm để mua thêm";
                    return View();
                }
                else
                {
                    foreach (var item in findcart)
                    {
                        CartSessionModel cartModel = new CartSessionModel()
                        {
                            IdCart = item.id,
                            PrId = item.prId,
                            PrName = item.Product.prName,
                            Price = item.Product.price,
                            Amounts = item.amounts,
                            AmountsMax = item.Product.amount,
                            Images = item.Product.images
                        };
                        cartSessions.Add(cartModel);
                    }
                }
                return View();
            }
            else if (cartSessions != null && cartSessions.Count != 0)
            {
                ViewBag.Title = "Giỏ hàng";
                ViewBag.StatusCart = "1";
                return View();
            }
            else
            {
                ViewBag.Title = "Giỏ hàng";
                ViewBag.StatusCart = "0";
                ViewBag.Message = "Hiện tại không có sản phẩm trong giỏ";
                ViewBag.MessageOrder = "Hãy quay lại trang sản phẩm để mua thêm";
                return View();
            }
        }
        public ActionResult AddCart(long id, int amounts)
        {
            if (Session[CommonConstants.Cart_Session] == null)
            {
                Session[CommonConstants.Cart_Session] = new List<CartSessionModel>();
            }
            List<CartSessionModel> cartSessions = Session[CommonConstants.Cart_Session] as List<CartSessionModel>;
            var user = Session[CommonConstants.User_SESSION] as UserSession;
            if (user != null)
            {
                cartSessions.Clear();
                var findlistcart = new DetailCartDAO().FindList(user.UserID,1);
                var findPr = new ProductDAO().FindProduct(Convert.ToUInt32(id));
                DetailCart detailCart = new DetailCart()
                {
                    customerId = user.UserID,
                    prId = findPr.prId,
                    price = findPr.price,
                    amounts = amounts,
                    status = 1
                };

                var addCart = new DetailCartDAO().AddDetailOrder(detailCart);
                if(addCart == 1)
                {
                    foreach (var item in findlistcart)
                    {
                        var find = new ProductDAO().FindProduct(Convert.ToUInt32(item.prId));
                        CartSessionModel cartModel = new CartSessionModel()
                        {
                            IdCart = item.id,
                            PrId = item.prId,
                            PrName = find.prName,
                            Price = find.price,
                            Amounts = item.amounts,
                            AmountsMax = find.amount,
                            Images = findPr.images
                        };
                        cartSessions.Add(cartModel);
                    }
                }
            }
            else
            {
                if (cartSessions.FirstOrDefault(m => m.PrId == id) == null)
                {
                    Product find = new ProductDAO().FindProduct(id);
                    CartSessionModel cartModel = new CartSessionModel()
                    {
                        IdCart = Convert.ToInt32(id),
                        PrId = find.prId,
                        PrName = find.prName,
                        Price = find.price,
                        Images = find.images,
                        Amounts = amounts,
                        AmountsMax = find.amount
                    };
                    cartSessions.Add(cartModel);
                }
                else
                {
                    CartSessionModel cartModel = cartSessions.FirstOrDefault(m => m.PrId == id);
                    cartModel.Amounts += amounts;
                }
            }
            return RedirectToAction("Index");
        }


        public ActionResult DeletedCart(long id)
        {
            List<CartSessionModel> cartSessions = Session[CommonConstants.Cart_Session] as List<CartSessionModel>;
            CartSessionModel deleted = cartSessions.FirstOrDefault(x => x.IdCart == id);
            var user = Session[CommonConstants.User_SESSION] as UserSession;
            if (user != null)
            {
                var findlistcart = new DetailCartDAO().FindList(user.UserID,1);
                var deleteCart = new DetailCartDAO().DeleteCart(Convert.ToInt32(id));
                if (deleted != null)
                {
                    if(deleteCart == 1)
                    {
                        cartSessions.Remove(deleted);
                    }
                }
                return RedirectToAction("Index");
            }
            if (deleted != null)
            {
                cartSessions.Remove(deleted);
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeletedAll()
        {
            List<CartSessionModel> cartSessions = Session[CommonConstants.Cart_Session] as List<CartSessionModel>;
            var user = Session[CommonConstants.User_SESSION] as UserSession;
            if (user != null)
            {
                var findlistcart = new DetailCartDAO().FindList(user.UserID,1);
                if(findlistcart != null || findlistcart.Count != 0)
                {
                    foreach(var item in findlistcart)
                    {
                        var delALL = new DetailCartDAO().DeleteCart(item.id);
                    }
                }
            }
            cartSessions.Clear();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult UpdateCart(List<RolesModel> objects)
        {
            List<CartSessionModel> cartSessions = Session[CommonConstants.Cart_Session] as List<CartSessionModel>;
            if (objects != null)
            {
                var user = Session[CommonConstants.User_SESSION] as UserSession;
                if (user!=null)
                {
                    foreach (var item in objects)
                    {
                        Product find = new ProductDAO().FindProduct(Convert.ToInt32(item.Id));
                        DetailCart detailCart = new DetailCart()
                        {
                            customerId = user.UserID,
                            prId = find.prId,
                            price = find.price,
                            amounts = Convert.ToInt32(item.Amounts),
                            status = 1
                        };
                        var addCart = new DetailCartDAO().AddDetailOrder(detailCart);
                    }
                    foreach (var item in objects)
                    {
                        var id = Convert.ToInt32(item.Id);
                        CartSessionModel update = cartSessions.FirstOrDefault(x => x.PrId == id);
                        if (update != null)
                        {
                            update.Amounts = Convert.ToInt32(item.Amounts);
                        }
                    }
                }
                else
                {
                    foreach (var item in objects)
                    {
                        var id = Convert.ToInt32(item.Id);
                        CartSessionModel update = cartSessions.FirstOrDefault(x => x.PrId == id);
                        if (update != null)
                        {
                            update.Amounts = Convert.ToInt32(item.Amounts);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult BuyOne(int idcart)
        {
            List<CartSessionModel> cartSessions = Session[CommonConstants.Cart_Session] as List<CartSessionModel>;
            var user = Session[CommonConstants.User_SESSION] as UserSession;
            CartSessionModel cartdeleted = cartSessions.FirstOrDefault(x => x.IdCart == idcart);
            var buy = new DetailCartDAO().ChangeDetailCart("mua", idcart);
            if(user != null)
            {
                if (buy == 1)
                {
                    if (Session[CommonConstants.Buy_Session] == null)
                    {
                        Session[CommonConstants.Buy_Session] = new List<CartSessionModel>();
                    }
                    List<CartSessionModel> buySessions = Session[CommonConstants.Buy_Session] as List<CartSessionModel>;
                    CartSessionModel FindbuySessions = buySessions.FirstOrDefault(x => x.IdCart == idcart);

                    if (FindbuySessions == null)
                    {
                        CartSessionModel waiteBuy = new CartSessionModel()
                        {
                            IdCart = cartdeleted.IdCart,
                            PrId = idcart,
                            PrName = cartdeleted.PrName,
                            Price = cartdeleted.Price,
                            Images = cartdeleted.Images,
                            Amounts = cartdeleted.Amounts,
                            AmountsMax = cartdeleted.AmountsMax,
                            Status = 2
                        };
                        buySessions.Add(waiteBuy);
                        var change = cartSessions.Remove(cartdeleted);
                    }
                    else
                    {
                        FindbuySessions.Amounts += cartdeleted.Amounts;
                    }
                    return RedirectToAction("Waite");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("NotAccount", "Error");
            }
        }
        public ActionResult BuyAll()
        {
            List<CartSessionModel> cartSessions = Session[CommonConstants.Cart_Session] as List<CartSessionModel>;
            var user = Session[CommonConstants.User_SESSION] as UserSession;
            if(user != null)
            {
                var findlistcart = new DetailCartDAO().FindList(user.UserID,1);
                if (findlistcart != null || findlistcart.Count == 0)
                {
                    foreach (var item in findlistcart)
                    {
                        var addCart = new DetailCartDAO().ChangeDetailCart("mua", item.id);
                    }
                    cartSessions.Clear();
                }
                return RedirectToAction("Waite");
            }
            else
            {
                return RedirectToAction("NotAccount", "Error");
            }
        }
        public ActionResult Waite()
        {
            if (Session[CommonConstants.Buy_Session] == null)
            {
                Session[CommonConstants.Buy_Session] = new List<CartSessionModel>();
            }
            List<CartSessionModel> buySessions = Session[CommonConstants.Buy_Session] as List<CartSessionModel>;
            var user = Session[CommonConstants.User_SESSION] as UserSession;
            if(user != null)
            {
                var findcart = new DetailCartDAO().FindList(user.UserID, 2);
                if (buySessions != null && buySessions.Count != 0)
                {
                    buySessions.Clear();
                    if (findcart != null || findcart.Count != 0)
                    {
                        foreach (var item in buySessions)
                        {
                            var findPr = new ProductDAO().FindProduct(Convert.ToUInt32(item.PrId));
                            DetailCart detailCart = new DetailCart()
                            {
                                customerId = user.UserID,
                                prId = findPr.prId,
                                price = findPr.price,
                                amounts = item.Amounts,
                                status = 2
                            };
                            var addCart = new DetailCartDAO().AddDetailOrder(detailCart);
                        }
                        foreach (var itemcart in findcart)
                        {
                            CartSessionModel cartModel = new CartSessionModel()
                            {
                                IdCart = itemcart.id,
                                PrId = itemcart.prId,
                                PrName = itemcart.Product.prName,
                                Price = itemcart.Product.price,
                                Amounts = itemcart.amounts,
                                AmountsMax = itemcart.Product.amount,
                                Images = itemcart.Product.images,
                                Status = itemcart.status
                            };
                            buySessions.Add(cartModel);
                        }
                        return View();
                    }
                }
                else
                {
                    foreach (var item in findcart)
                    {
                        CartSessionModel cartModel = new CartSessionModel()
                        {
                            IdCart = item.id,
                            PrId = item.prId,
                            PrName = item.Product.prName,
                            Price = item.Product.price,
                            Amounts = item.amounts,
                            AmountsMax = item.Product.amount,
                            Images = item.Product.images
                        };
                        buySessions.Add(cartModel);
                    }
                    ViewBag.Title = "Giỏ hàng";
                    ViewBag.StatusCart = "0";
                    ViewBag.Message = "Hiện chưa có sản phẩm nào được đặt";
                    ViewBag.MessageOrder = "Hãy quay lại trang giỏ hàng để đặt hàng";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("NotAccount","Error");
            }
            if (buySessions != null && buySessions.Count != 0)
            {
                ViewBag.Title = "Sản phẩm đã đặt";
                ViewBag.StatusCart = "1";
                return View();
            }
            else
            {
                ViewBag.Title = "Giỏ hàng";
                ViewBag.StatusCart = "0";
                ViewBag.Message = "Hiện chưa có sản phẩm nào được đặt";
                ViewBag.MessageOrder = "Hãy quay lại trang giỏ hàng để đặt hàng";
                return View();
            }
        }
        public ActionResult CancelBuy(int idpr)
        {
            List<CartSessionModel> buySessions = Session[CommonConstants.Buy_Session] as List<CartSessionModel>;
            var user = Session[CommonConstants.User_SESSION] as UserSession;
            CartSessionModel cartdeleted = buySessions.FirstOrDefault(x => x.IdCart == idpr);
            var buy = new DetailCartDAO().ChangeDetailCart("huy", idpr);
            if(user != null)
            {
                if (buy == 1)
                {
                    if (Session[CommonConstants.Cart_Session] == null)
                    {
                        Session[CommonConstants.Cart_Session] = new List<CartSessionModel>();
                    }
                    List<CartSessionModel> cartSessions = Session[CommonConstants.Cart_Session] as List<CartSessionModel>;
                    CartSessionModel FindbuySessions = cartSessions.FirstOrDefault(x => x.IdCart == idpr);

                    if (FindbuySessions == null)
                    {
                        CartSessionModel waiteBuy = new CartSessionModel()
                        {
                            IdCart = cartdeleted.IdCart,
                            PrId = idpr,
                            PrName = cartdeleted.PrName,
                            Price = cartdeleted.Price,
                            Images = cartdeleted.Images,
                            Amounts = cartdeleted.Amounts,
                            AmountsMax = cartdeleted.AmountsMax,
                            Status = 1
                        };
                        cartSessions.Add(waiteBuy);
                        var change = buySessions.Remove(cartdeleted);
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("NotAccount", "Error");
            }
        }
    }
}