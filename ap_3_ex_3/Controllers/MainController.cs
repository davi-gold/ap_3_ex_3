    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ap_3_ex_3.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        double lon;
        double lat;
        [HttpGet]
        public ActionResult display(string ip, int port)
        {
            Models.Client.Instance.connect(ip, port);
            lon = Models.Client.Instance.getLon();
            lat = Models.Client.Instance.getLat();
           // Console.WriteLine(lon);
           // Console.WriteLine(lat);
            ViewBag.lon = lon;
            ViewBag.lat = lat;

            return View();
        }
    }
}