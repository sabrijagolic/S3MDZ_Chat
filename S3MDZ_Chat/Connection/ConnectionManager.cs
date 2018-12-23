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
        private static UdpClient udpClient;
        private static Thread chatListener;
        private static Thread guestListener;
        private static IPEndPoint ipEndPointSend;
        private static IPEndPoint ipEndPointReceive;


        public static void StartChat(string testIp)
        {
            guestListener.Interrupt();
            ipEndPointSend = new IPEndPoint(IPAddress.Parse(testIp), 11000);
            ipEndPointReceive = new IPEndPoint(IPAddress.Parse(testIp), 0);
            Byte[] sendBytes = Encoding.ASCII.GetBytes("Ovo je nesto");
            udpClient.Send(sendBytes, sendBytes.Length, ipEndPointSend);
        }

        public static void InitializeConnectionManager(string testIp)
        {
            ipEndPointSend = new IPEndPoint(IPAddress.Parse(testIp), 11000);
        }

        public static void Send(string text)
        {
            Byte[] sendBytes = Encoding.ASCII.GetBytes(text);
            udpClient.Send(sendBytes, sendBytes.Length, ipEndPointSend);

        }

        public static void ListenForRemoteGuest(Action onChartStarted)
        {
            udpClient = new UdpClient();
            var udpClientListener = new UdpClient(11000);
            ThreadStart start = new ThreadStart(() =>
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);

                while (true)
                {
                    byte[] data = udpClientListener.Receive(ref endPoint);
                    string message = Encoding.ASCII.GetString(data);
                    ipEndPointReceive = new IPEndPoint(IPAddress.Parse(endPoint.Address.ToString()), 0);
                    InitializeConnectionManager(endPoint.Address.ToString());
                    guestListener.Interrupt();
                    onChartStarted();
                }
            });
            guestListener = new Thread(start);
            guestListener.SetApartmentState(ApartmentState.STA);
            guestListener.IsBackground = true;
            guestListener.Start();
        }


        public static void ReceiveMessage(Action<string> messageReceived)
        {
            var udpClientListener = new UdpClient(11000);
            ThreadStart start = new ThreadStart(() =>
            {
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
