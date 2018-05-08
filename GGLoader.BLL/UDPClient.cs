using System;
using System.Net.Sockets;
using System.Text;

namespace GGLoader.BLL.Domain
{
    public class UDPClient
    {
        public async static void Fire(Process process, string testNumber)
        {
            var cont = 0;

            while (cont++ < process.Shoots)
            {
                SendMessage(process.Port, "127.0.0.1", 50000, Encoding.ASCII.GetBytes(String.Format("TestNumber: {0}, Shoot # {1}, Process: {2}, Port: {3}, ID: {4}", testNumber, cont.ToString(), process.Name, process.Port, Guid.NewGuid())));
            }
        }
        static void SendMessage(int srcPort, string dstIp, int dstPort, byte[] data)
        {
            using (UdpClient c = new UdpClient(srcPort))
                c.Send(data, data.Length, dstIp, dstPort);
        }
    }
}
