using System.Web.Mvc;
using System.Web.Routing;

namespace Comestics
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Category",
                url: "Category/{metatitle}-{cateId}",
                defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Comestic.Controller" }
            );
            routes.MapRoute(
                name: "Product Index",
                url: "Product/Index",
                defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Comestic.Controller" }
            );
            routes.MapRoute(
                name: "Product Detail",
                url: "Product/{metatitle}-{productId}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "Comestic.Controller" }
            );
            routes.MapRoute(
                name: "News",
                url: "News/{metatitle}-{newsId}",
                defaults: new { controller = "News", action = "News", id = UrlParameter.Optional },
                namespaces: new[] { "Comestic.Controller" }
            );

            /*CART*/
            routes.MapRoute(
                name: "AddCart",
                url: "AddCart",
                defaults: new { controller = "Cart", action = "AddCart", id = UrlParameter.Optional, amounts = UrlParameter.Optional },
                namespaces: new[] { "Comestic.Controller" }
            );
            routes.MapRoute(
                name: "UpdateCart",
                url: "UpdateCart",
                defaults: new { controller = "Cart", action = "UpdateCart", id = UrlParameter.Optional, amounts = UrlParameter.Optional },
                namespaces: new[] { "Comestic.Controller" }
            );
            routes.MapRoute(
                name: "DeletedCart",
                url: "DeletedCart",
                defaults: new { controller = "Cart", action = "DeletedCart", id = UrlParameter.Optional },
                namespaces: new[] { "Comestic.Controller" }
            );
            routes.MapRoute(
                name: "Cart Product",
                url: "Cart",
                defaults: new { controller = "Cart", action = "Index"},
                namespaces: new[] { "Comestic.Controller" }
            );
            /*END_CART*/
            routes.MapRoute(
                name: "Home_Index",
                url: "Home/Index",
                defaults: new { controller = "Error", action = "NotFound", id = UrlParameter.Optional },
                namespaces: new[] { "Comestic.Controller" }
            );
            /*Home*/
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] {"Comestic.Controller"}
            );
            /*EndHome*/
        }
    }
}
