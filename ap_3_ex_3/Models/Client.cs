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
            string response = sendStream(getLonCommand);
            lon = makeDouble(response);
            return lon;
        }


        public double getLat()
        {
            string response = sendStream(getLatCommand);
            lat = makeDouble(response);
            return lat;
        }

        public double getRudder()
        {
            string response = sendStream(getRudCommand);
            rudder = makeDouble(response);
            return rudder;
        }

        public double getThrottle()
        {
            string response = sendStream(getThrotCommand);
            throttle = makeDouble(response);
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
    }
}
