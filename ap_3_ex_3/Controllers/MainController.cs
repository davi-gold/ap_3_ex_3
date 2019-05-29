using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

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
        public ActionResult display(string ip, int port, int? time)
        {
            Models.Client.Instance.connect(ip, port);
            if (time == null)
                Session["timer"] = 0;
            else
                Session["timer"] = time;
            return View();
        }

        [HttpPost]
        public string getLonAndLat()
        {
            string lon = Models.Client.Instance.getLon().ToString();
            string lat = Models.Client.Instance.getLat().ToString();
            Console.WriteLine(lon);
            Console.WriteLine(lat);
            return ToXml(lon, lat);
        }

        private string ToXml(string lat, string lon)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();

            writer.WriteStartElement("Position");
            writer.WriteElementString("Lon", lon);
            writer.WriteElementString("Lat", lat);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }
    }
}