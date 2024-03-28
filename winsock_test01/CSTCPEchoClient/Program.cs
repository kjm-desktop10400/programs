using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace socketC
{
    class C
    {
        public static void Main()
        {
            //今回送るHello World!
            string st = "Hello World!";
            SocketClient();
        }


        public static void SocketClient()
        {

            IPAddress servIP = new IPAddress(0xFFFFFFFF);

            if(IPAddress.TryParse("127.0.0.1",out servIP))
            {
                Console.WriteLine("servIP was set.");
            }

            IPEndPoint endPoint = new IPEndPoint(servIP, 5000);

            TcpClient client = new TcpClient("127.0.0.1", 5000);
            Console.WriteLine("connected server ({0}:{1}) by ({2}:{3})",
                ((IPEndPoint)client.Client.RemoteEndPoint).Address,
                ((IPEndPoint)client.Client.RemoteEndPoint).Port,
                ((IPEndPoint)client.Client.LocalEndPoint).Address,
                ((IPEndPoint)client.Client.LocalEndPoint).Port
                );

            NetworkStream ns = client.GetStream();

            ns.ReadTimeout = 100000;
            ns.WriteTimeout = 1000000;

            byte[] msg = Encoding.UTF8.GetBytes("Echo This!!");
            ns.Write(msg, 0, msg.Length);

            byte[] rescieve = new byte[4096];

            ns.Read(rescieve,0, rescieve.Length);

            Console.WriteLine("rescieved message : {0}", Encoding.UTF8.GetString(rescieve));

            ns.Close();
            client.Close();

        }
    }
}
