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
        const string testIp = "";

        private static UdpClient udpClient;
        private static UdpClient udpClientListener;
        private static Thread chatListener;
        private static IPEndPoint ipEndPoint;
        public static void InitializeConnectionManager()
        {
            udpClient = new UdpClient();
            udpClientListener = new UdpClient();
            ipEndPoint = new IPEndPoint(IPAddress.Parse(testIp), 11004);
        }
        public static void Send()
        {
            Byte[] sendBytes = Encoding.ASCII.GetBytes("Is anybody there?");
            try
            {
                udpClient.Send(sendBytes, sendBytes.Length, ipEndPoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void Receive()
        {
            try
            {
                // Blocks until a message returns on this socket from a remote host.
                Byte[] receiveBytes = udpClientListener.Receive(ref ipEndPoint);

                string returnData = Encoding.ASCII.GetString(receiveBytes);

                Console.WriteLine("This is the message you received " +
                                             returnData.ToString());
                Console.WriteLine("This message was sent from " +
                                            ipEndPoint.Address.ToString() +
                                            " on their port number " +
                                            ipEndPoint.Port.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
