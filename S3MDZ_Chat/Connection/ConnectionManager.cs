using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace S3MDZ_Chat.Connection
{
    public class ConnectionManager
    {
        private static IPEndPoint ipEndPointReceive;
        private static bool waitForGuest = true;
        private static string guestIp = "";

        public static void StartChat(string guestIp)
        {
            var udpClient = new UdpClient();
            ConnectionManager.guestIp = guestIp;
            ipEndPointReceive = new IPEndPoint(IPAddress.Parse(guestIp), 0);
            var ipEndPointConnect = new IPEndPoint(IPAddress.Parse(guestIp), 11001);
            Byte[] sendBytes = Encoding.ASCII.GetBytes("1");
            udpClient.Send(sendBytes, sendBytes.Length, ipEndPointConnect);
            waitForGuest = false;
        }

        public static void ListenForRemoteGuest(Action onChartStarted, Action<Action<string>> onRequestReceived)
        {
            RunThread(() =>
            {
                var udpClientListener = new UdpClient(11001);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                while (true)
                {
                    byte[] data = udpClientListener.Receive(ref endPoint);
                    string message = Encoding.ASCII.GetString(data);
                    guestIp = endPoint.Address.ToString();
                    if (message == "1")
                    {
                        onRequestReceived((string choice) => {
                            var udpClient = new UdpClient();
                            IPEndPoint ipEndPointConnect = new IPEndPoint(IPAddress.Parse(guestIp), 11001);
                            Byte[] sendBytes = Encoding.ASCII.GetBytes(choice);
                            udpClient.Send(sendBytes, sendBytes.Length, ipEndPointConnect);
                        });
                    }
                    else if (message == "2")
                    {
                        ipEndPointReceive = new IPEndPoint(IPAddress.Parse(endPoint.Address.ToString()), 0);
                        onChartStarted();
                    }
                    else if (message == "3")
                    {
                        MessageBox.Show("Your connection request was refused");
                    }
                    if (waitForGuest)
                    {
                        waitForGuest = false;
                    }
                }
            });
        }

        public static void Send(string text)
        {
            var udpClient = new UdpClient();
            var ipEndPointSend = new IPEndPoint(IPAddress.Parse(guestIp), 11000);
            Byte[] sendBytes = Encoding.ASCII.GetBytes(text);
            udpClient.Send(sendBytes, sendBytes.Length, ipEndPointSend);
        }

        public static void ReceiveMessage(Action<string> messageReceived)
        {
            RunThread(() =>
            {
                var udpClientListener = new UdpClient(11000);
                while (true)
                {
                    byte[] receiveBytes = udpClientListener.Receive(ref ipEndPointReceive);

                    string returnData = Encoding.ASCII.GetString(receiveBytes);
                    messageReceived(returnData.ToString());
                }
            });
        }

        private static void RunThread(Action callback)
        {
            ThreadStart start = new ThreadStart(callback);
            Thread thread = new Thread(start);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
