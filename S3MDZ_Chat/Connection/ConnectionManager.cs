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
        private static IPEndPoint ipEndPointSend;
        private static IPEndPoint ipEndPointReceive;
        public static void InitializeConnectionManager(string testIp)
        {
            udpClient = new UdpClient();
            udpClientListener = new UdpClient(11000);
            ipEndPointSend = new IPEndPoint(IPAddress.Parse(testIp), 11000);
            ipEndPointReceive = new IPEndPoint(IPAddress.Parse(testIp), 0);
        }
        public static void Send()
        {
            Byte[] sendBytes = Encoding.ASCII.GetBytes("Is anybody there?");
            try
            {
                udpClient.Send(sendBytes, sendBytes.Length, ipEndPointSend);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        
        
        public async static void Receive()
        {
            ThreadStart start = new ThreadStart(Receiver);
            chatListener = new Thread(start);
            chatListener.IsBackground = true;
            chatListener.Start();



            try
            {
                // Blocks until a message returns on this socket from a remote host.
                byte[] receiveBytes = udpClientListener.ReceiveAsync().Result.Buffer;

                string returnData = Encoding.ASCII.GetString(receiveBytes);

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
        private static void Receiver()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            

            while (true)
            {
                byte[] data = udpClientListener.Receive(ref endPoint);
                string message = Encoding.ASCII.GetString(data);
                MessageReceived(message);
                
            }
        }
        private void MessageReceived(string message)
        {
            Console.WriteLine(message);
        }

    }
}
