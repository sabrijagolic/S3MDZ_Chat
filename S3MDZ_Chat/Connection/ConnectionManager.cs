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
        const int port = 54545;
        //const string testIp = "31.223.138.210";

        private static UdpClient udpClient;
        private static UdpClient udpClientListener;
        private static Thread chatListener;
        private static Thread guestListener;
        private static IPEndPoint ipEndPointSend;
        private static IPEndPoint ipEndPointReceive;


        public static void StartChat(string testIp)
        {
            ipEndPointSend = new IPEndPoint(IPAddress.Parse(testIp), 11000);
            ipEndPointReceive = new IPEndPoint(IPAddress.Parse(testIp), 0);
            Byte[] sendBytes = Encoding.ASCII.GetBytes("Ovo je nesto");
            udpClient.Send(sendBytes, sendBytes.Length, ipEndPointSend);
            guestListener.Abort();
        }

        public static void InitializeConnectionManager(string testIp)
        {
            ipEndPointSend = new IPEndPoint(IPAddress.Parse(testIp), 11000);
        }

        public static void Send(string text)
        {
            Byte[] sendBytes = Encoding.ASCII.GetBytes(text);
            try
            {
                udpClient.Send(sendBytes, sendBytes.Length, ipEndPointSend);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void ListenForRemoteGuest(Action onChartStarted)
        {
            udpClient = new UdpClient();
            udpClientListener = new UdpClient(11000);
            ThreadStart start = new ThreadStart(() =>
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);

                while (true)
                {
                    byte[] data = udpClientListener.Receive(ref endPoint);
                    string message = Encoding.ASCII.GetString(data);
                    Console.WriteLine(message);
                    Console.WriteLine(endPoint.Address.ToString());
                    ipEndPointReceive = new IPEndPoint(IPAddress.Parse(endPoint.Address.ToString()), 0);
                    InitializeConnectionManager(endPoint.Address.ToString());
                    onChartStarted();
                    guestListener.Abort();
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
                while (true)
                {
                    try
                    {
                        // Blocks until a message returns on this socket from a remote host.
                        byte[] receiveBytes = udpClientListener.Receive(ref ipEndPointReceive);

                        string returnData = Encoding.ASCII.GetString(receiveBytes);
                        messageReceived(returnData.ToString());
                        Console.WriteLine("This is the message you received " +
                                                     returnData.ToString());
                        Console.WriteLine("This message was sent from " +
                                                    ipEndPointReceive.Address.ToString() +
                                                    " on their port number " +
                                                    ipEndPointReceive.Port.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            });
            chatListener = new Thread(start);
            chatListener.SetApartmentState(ApartmentState.STA);
            chatListener.IsBackground = true;
            chatListener.Start();
        }
    }
}
