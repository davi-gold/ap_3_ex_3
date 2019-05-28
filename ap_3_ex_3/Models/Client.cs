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
        const string getLonCommand = "get /position/longitude-deg";
        const string getLatCommand = "get /position/latitude-deg";

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
        public Client()
        {
            isConnected = false;
        }

        public void connect(string ip, int port)
        {
            Int32 p = Convert.ToInt32(port); // client port
            string server = ip; // server ip
            tcpClient = new TcpClient(server, p);
            Console.WriteLine("Command channel :You are connected");
            isConnected = true;

        }

        private int ToInt32(int port)
        {
            throw new NotImplementedException();
        }

        public string getLon()
        {
            string lon = sendStream(getLonCommand);
            return lon;
        }


        public string getLat()
        {
            string lat = sendStream(getLatCommand);
            return lat;
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
