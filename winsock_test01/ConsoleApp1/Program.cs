using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace socketS
{
    class S
    {
        public static void Main()
        {
            SocketServer();
        }

        public static void SocketServer()
        {

            IPAddress servIP = new IPAddress(0xFFFFFFFF);

            string hostname = Dns.GetHostName();
            IPAddress[] iPAddresses = Dns.GetHostAddresses(hostname);

            foreach (IPAddress iP in iPAddresses)
            {
                if (iP.AddressFamily == AddressFamily.InterNetwork)
                {
                    servIP = iP;
                    break;
                }
            }

            TcpListener listener = new TcpListener(IPAddress.Any, 5000);

            listener.Start();

            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("クライアント({0}:{1})と接続しました。", ((IPEndPoint)client.Client.RemoteEndPoint).Address, ((IPEndPoint)client.Client.RemoteEndPoint).Port);

            NetworkStream ns = client.GetStream();

            ns.ReadTimeout = 100000;
            ns.WriteTimeout = 100000;

            Byte[] rescieve = new byte[4096];
            int resSize = ns.Read(rescieve, 0, rescieve.Length);

            string rescievedMessage = Encoding.UTF8.GetString(rescieve);

            Console.WriteLine("recived : {0}", rescievedMessage);

            Byte[] sendMSG = Encoding.UTF8.GetBytes(rescievedMessage);

            ns.Write(sendMSG, 0, sendMSG.Length);

            ns.Close();
            client.Close();



        }
    }
}
