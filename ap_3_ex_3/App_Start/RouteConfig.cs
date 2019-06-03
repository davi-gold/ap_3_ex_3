using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ap_3_ex_3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "Default",
            url: "{action}/{id}",
            defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute("display", "display/{ip}/{port}/{time}/{fname}",
                defaults: new
                {
                    controller = "Main",
                    action = "display",
                    ip = "127.0.0.1",
                    port = "5400",
                    time = UrlParameter.Optional,
                    fname = UrlParameter.Optional
                });
        }

    }
}
