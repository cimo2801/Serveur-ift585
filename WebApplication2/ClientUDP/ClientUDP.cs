using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WebApplication2.ClientUDP;

public class ClientUDP
{
    private const int listenPort = 6969;
    private const int sendPort = 6969;

    private static void StartListener()
    {
        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        UdpClient listener = new UdpClient(listenPort);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

        try
        {
            while (true)
            {
                Console.WriteLine("Waiting for connection request..");
                byte[] bytes = listener.Receive(ref groupEP);
                IPAddress clientIPAddress = groupEP.Address;

                Console.WriteLine($"Received connection request from {groupEP} :");
                Console.WriteLine($" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");

                Thread.Sleep(2000);

                byte[] sendbuf = Encoding.ASCII.GetBytes("adffasf1n18dffd0fs");
                IPEndPoint ep = new IPEndPoint(clientIPAddress, sendPort);
                s.SendTo(sendbuf, ep);

                Console.WriteLine("Token sent to the client address");

                bytes = listener.Receive(ref groupEP);
                Console.WriteLine($"Received hash password from {groupEP} :");
                Console.WriteLine($" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");

                sendbuf = Encoding.ASCII.GetBytes("Connection success");
                s.SendTo(sendbuf, ep);
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            listener.Close();
        }
    }

    public static void Main()
    {
        StartListener();
    }
}
