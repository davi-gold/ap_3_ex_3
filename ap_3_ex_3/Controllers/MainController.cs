using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public bool is4;

        [HttpGet]
        public ActionResult save(string ip, int port, int time, int length, string fname)
        {
            //TAKE THIS OUT OF COMMENT
            //Models.Client.Instance.connect(ip, port);
            Session["timer"] = time;
            Session["lenght"] = length;
            Session["fname"] = 1;
            Models.Client.Instance.fname = fname;
            return View("display");
        }

        [HttpGet]
        public ActionResult display(string ip_fname, int port_time, int? time)
        {
            try
            {
                //is 1 or 2
                IPAddress ipTest = IPAddress.Parse(ip_fname);
                Models.Client.Instance.is4 = false;
                //TAKE THIS OUT OF COMMENT
                //Models.Client.Instance.connect(ip, port);
                if (time == null)
                    Session["timer"] = 0;
                else
                    Session["timer"] = time;
            }
            catch
            {
                Models.Client.Instance.is4 = true;
                Models.Client.Instance.fname = ip_fname;
                Session["timer"] = port_time;
            }
            //these two paramters are usless here so i'm automatically assigning them 0
            Session["length"] = 0;
            Session["fname"] = 0;
            return View();
        }

        [HttpPost]
        public string getLonAndLat()
        {
            string lon;
            string lat;
            if (Models.Client.Instance.is4)
            {
                if((lon = Models.Client.Instance.lonFromFile())==null)
                    lon = "#";
                if ((lat = Models.Client.Instance.latFromFile()) == null)
                    lat = "#";
            }
            else
            {
                lon = Models.Client.Instance.getLon().ToString();
                lat = Models.Client.Instance.getLat().ToString();
            }
            Console.WriteLine(lon);
            Console.WriteLine(lat);
            return LonLatToXml(lon, lat);
        }

        public double randDouble()
        {
            Random random = new Random();
            return random.NextDouble() * 700;
        }

        public string LonLatToXml(string lon, string lat)
        {
            return ToXml("Position", "Lon", lon, "Lat", lat);
        }

        public string getRudAndThrot()
        {
            string rudder = Models.Client.Instance.getRudder().ToString();
            string throttle = Models.Client.Instance.getRudder().ToString();
            return RudThroToXml(rudder, throttle);
        }

        public string RudThroToXml(string rudder, string throttle)
        {
            return ToXml("Velocity", "Rudder", rudder, "Throttle", throttle);
        }
        private string ToXml(string type, string title1, string value1, string title2, string value2)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();

            writer.WriteStartElement(type);
            writer.WriteElementString(title1, value1);
            writer.WriteElementString(title2, value2);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }

        public void WriteFlightData()
        {
            //will be called from view ever x amount of time
            Models.Client.Instance.writeToFile();
        }
    }
}