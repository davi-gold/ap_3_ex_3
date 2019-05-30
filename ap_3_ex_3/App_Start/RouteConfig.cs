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
            //I ADDED FNAME = NOTHING SO I COULD USE THE DISPLAY FUNCTION FOR ALL URL'S
            routes.MapRoute("display", "display/{ip}/{port}/{time}",
                defaults: new { controller = "Main", action = "display", ip = "127.0.0.1",
                    port = "5400", time = UrlParameter.Optional, fname = "nothing" });

            /*routes.MapRoute("display", "display/{ip}/{port}/{time}/{length}/{fName}",
                defaults: new
                {
                    controller = "Main",
                    action = "display",
                    ip = "127.0.0.1",
                    port = "5400",
                    time = UrlParameter.Optional,
                    length = UrlParameter.Optional,
                    fName = UrlParameter.Optional
                });*/
        }
    }
}
