using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web;

namespace ap_3_ex_3.Models
{
    public class Client
    {
        TcpClient tcpClient;
        const string getLonCommand = "get /position/longitude-deg\r\n";
        const string getLatCommand = "get /position/latitude-deg\r\n";
        const string getRudCommand = "get /controls/flight/rudder\r\n";
        const string getThrotCommand = "get /controls/engines/current-engine/throttle\r\n";

        public bool isConnected
        {
            get;
            set;
        }

        private static Client m_Instance = null;
        public static Client Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new Client();
                return m_Instance;
            }
        }
        public double lon { get; set; }
        public double lat { get; set; }
        public int time { get; set; }
        public double rudder { get; set; }
        public double throttle { get; set; }
        public string fname { get; set; }
        public bool is4 {get; set;}
        public Client()
        {
            isConnected = false;
        }

        public void connect(string ip, int port)
        {
            //Int32 p = Convert.ToInt32(port); // client port
            string server = ip; // server ip
            tcpClient = new TcpClient(server, port);
            Console.WriteLine("Command channel :You are connected");
            isConnected = true;

        }

        private int ToInt32(int port)
        {
            throw new NotImplementedException();
        }

        public double getLon()
        {
            //NEED TO TAKE THIS OUT OF COMMENT
            //string response = sendStream(getLonCommand);
            //lon = makeDouble(response);
            lon = randDouble();
            return lon;
        }


        public double getLat()
        {
            //NEED TO TAKE THIS OUT OF COMMENT
            //string response = sendStream(getLatCommand);
            //lat = makeDouble(response);
            lat = randDouble();
            return lat;
        }

        public double getRudder()
        {
            //NEED TO TAKE THIS OUT OF COMMENT
            //string response = sendStream(getRudCommand);
            //rudder = makeDouble(response);
            rudder = randDouble();
            return rudder;
        }

        public double getThrottle()
        {
            //NEED TO TAKE THIS OUT OF COMMENT
            //string response = sendStream(getThrotCommand);
            //throttle = makeDouble(response);
            throttle = randDouble();
            return throttle;
        }

        public double makeDouble(string response)
        {
            int index = response.IndexOf("'");
            response = response.Substring(index + 1);
            index = response.IndexOf("'");
            int count = response.Length;
            response = response.Remove(index, count - index);
            double dLon = Convert.ToDouble(response);
            return dLon;
        }

        public string sendStream(string message)
        {
            if (isConnected)
            {
                NetworkStream ns = tcpClient.GetStream();
                byte[] buff = Encoding.ASCII.GetBytes(message);
                ns.Write(buff, 0, buff.Length);
                StreamReader sr = new StreamReader(ns);
                string response = sr.ReadLine();
                return response;
            }
            else
                return null;

        }

        public void disconnect()
        {
            isConnected = false;
            tcpClient.Close();
        }

        public const string SCENARIO_FILE = "~/App_Data/{0}.txt";

        public void writeToFile()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\" + fname + ".txt";

            using (StreamWriter streamWriter = System.IO.File.AppendText(filePath))
            {
                streamWriter.WriteLine(Convert.ToString(lon) + ',' + Convert.ToString(lat) + 
                    ',' + Convert.ToString(throttle) + ',' + Convert.ToString(rudder));
            }
        }

        public double randDouble()
        {
            Random random = new Random();
            return random.NextDouble() * 700;
        }

        public string[] readFlightInfo()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\" + fname + ".txt";
            string[] data = System.IO.File.ReadAllLines(path);
            return data;
        }

        public string lonFromFile()
        {
            return readFlightInfo()[0];
        }

        public string latFromFile()
        {
            return readFlightInfo()[1];
        }
    }
}
