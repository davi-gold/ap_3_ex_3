    using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public ActionResult display(string ip, int port)
        {
            return View();
        }
    }
}