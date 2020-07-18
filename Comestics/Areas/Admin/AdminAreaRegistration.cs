using System.Web;
using System.Web.Mvc;

namespace Comestics.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ProductAdmin",
                "Admin/ProductAdmin",
                new { Controller = "ProductAdmin", action = "Index" }
            );
            context.MapRoute(
                "CategoryAdmin",
                "Admin/CategoryAdmin",
                new { Controller = "CategoryAdmin", action = "Index" }
            );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}