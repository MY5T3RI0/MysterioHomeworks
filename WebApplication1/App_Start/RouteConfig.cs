using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            ////routes.MapRoute("Account", "Account/{action}/{id}", new { controller = "Account", action = "Index", id = UrlParameter.Optional },
            ////   new[] { "Homework.Controllers" });

            ////routes.MapRoute("SidebarPartial", "Pages/SidebarPartial", new { controller = "Pages", action = "SidebarPartial" },
            ////   new[] { "Homework.Controllers" });

            //routes.MapRoute("Shop", "Shop/{action}/{name}", new { controller = "Homeworks", action = "Index", name = UrlParameter.Optional },
            //   new[] { "WebApplication1.Controllers" });

            ////routes.MapRoute("PagesMenuPartial", "Pages/PagesMenuPartial", new { controller = "Pages", action = "PagesMenuPartial" },
            ////   new[] { "Homework.Controllers" });

            //routes.MapRoute("Sidebar", "{sidebar}", new { controller = "Sidebar", action = "Index" },
            //   new[] { "WebApplication1.Controllers" });

            //routes.MapRoute("Pages", "{page}", new { controller = "Page", action = "Index" },
            //   new[] { "WebApplication1.Controllers" });

            //routes.MapRoute("Default", "", new { controller = "Page", action = "Index" },
            //    new[] { "WebApplication1.Controllers" });

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
