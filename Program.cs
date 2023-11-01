using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace UDP_Sever
{
    class Program
    {
        static void Main()
        {
            UdpClient udpServer = new UdpClient();
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);

            try
            {
                Console.WriteLine("Streaming gestartet...");

                // Öffne die Videodatei
                FileStream videoFileStream = File.OpenRead("D:/Schule/MyStack/video/test.mp3");

                byte[] buffer = new byte[1024];

                int bytesRead;
                while ((bytesRead = videoFileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    // Sende Frame mit Sequenznummer
                    udpServer.Send(buffer, bytesRead, clientEndPoint);

                    // Kurze Pause zwischen den Frames (simuliert die Übertragungsgeschwindigkeit)
                    System.Threading.Thread.Sleep(50);
                }

                Console.WriteLine("Streaming beendet.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                udpServer.Close();
            }
        }
    }
}
