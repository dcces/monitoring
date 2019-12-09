using monitoring_client.utils;
using System;
using System.Net.NetworkInformation;
using System.Threading;

namespace monitoring_client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var networks = NetworkInterface.GetAllNetworkInterfaces();
            long lastBytesRec = 0;
            long lastBytesSend = 0;
            while (true)
            {
                long nowBytesRec = 0;
                long nowBytesSend = 0;
                foreach (var network in networks)
                {
                    nowBytesRec += network.GetIPv4Statistics().BytesReceived;
                    nowBytesSend += network.GetIPv4Statistics().BytesSent;
                }
                var recinterval = nowBytesRec - lastBytesRec;
                var sendinterval = nowBytesSend - lastBytesSend;
                Console.WriteLine($"下载速度速度:{SystemHelper.getSpeed(recinterval, 2000)}");
                Console.WriteLine($"上传速度速度:{SystemHelper.getSpeed(sendinterval, 2000)}");
                Console.WriteLine($"下载总量:{SystemHelper.getSpeed(nowBytesRec)}");
                Console.WriteLine($"上传总量:{SystemHelper.getSpeed(nowBytesSend)}");
                lastBytesRec = nowBytesRec;
                lastBytesSend = nowBytesSend;
                Thread.Sleep(1000);
            }
        }


    }
}
