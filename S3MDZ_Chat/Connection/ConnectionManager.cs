using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S3MDZ_Chat.Connection
{
    public class ConnectionManager
    {
        private static Thread chatListener;
        private static Thread guestListener;
        private static IPEndPoint ipEndPointReceive;
        private static bool waitForGuest = true;
        private static string guestIp = "";

        public static void StartChat(string testIp)
        {
            var udpClient = new UdpClient();
            guestIp = testIp;
            var endPointConnect = new IPEndPoint(IPAddress.Parse(testIp), 11001);
            ipEndPointReceive = new IPEndPoint(IPAddress.Parse(testIp), 0);
            Byte[] sendBytes = Encoding.ASCII.GetBytes("Ovo je nesto");
            udpClient.Send(sendBytes, sendBytes.Length, endPointConnect);
            waitForGuest = false;
        }

        public static void InitializeConnectionManager(string testIp)
        {
        }

        public static void Send(string text)
        {
            var udpClient = new UdpClient();
            var ipEndPointSend = new IPEndPoint(IPAddress.Parse(guestIp), 11000);
            Byte[] sendBytes = Encoding.ASCII.GetBytes(text);
            udpClient.Send(sendBytes, sendBytes.Length, ipEndPointSend);
        }

        public static void ListenForRemoteGuest(Action onChartStarted)
        {
            var udpClientListener = new UdpClient(11001);
            ThreadStart start = new ThreadStart(() =>
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                while (true)
                {
                    byte[] data = udpClientListener.Receive(ref endPoint);
                    if (waitForGuest)
                    {
                        string message = Encoding.ASCII.GetString(data);
                        ipEndPointReceive = new IPEndPoint(IPAddress.Parse(endPoint.Address.ToString()), 0);
                        InitializeConnectionManager(endPoint.Address.ToString());
                        onChartStarted();
                        waitForGuest = false;
                    }
                }
            });
            guestListener = new Thread(start);
            guestListener.SetApartmentState(ApartmentState.STA);
            guestListener.IsBackground = true;
            guestListener.Start();
        }


        public static void ReceiveMessage(Action<string> messageReceived)
        {
            ThreadStart start = new ThreadStart(() =>
            {
                var udpClientListener = new UdpClient(11000);
                while (true)
                {
                    byte[] receiveBytes = udpClientListener.Receive(ref ipEndPointReceive);

                    string returnData = Encoding.ASCII.GetString(receiveBytes);
                     messageReceived(returnData.ToString());
                }
            });
            chatListener = new Thread(start);
            chatListener.IsBackground = true;
            chatListener.Start();
        }
    }
}
