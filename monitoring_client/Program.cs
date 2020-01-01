using monitoring_client.utils;
using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Text.Json;
using monitoring_client.model;

namespace monitoring_client
{
    class Program
    {
        static string url;
        static int interval = 4000;
        static void Main(string[] args)
        {
            int a = 1000;
            int b = 234;
            var c = a / b;

            var sss = SystemHelper.ConvertByte(12345);
            var s1 = sss.Item1;
            var s2 = sss.Item2;

            if (args.Length == 0)
            {
                Console.WriteLine("请输入服务器地址:");
                url = Console.ReadLine();
            }
            else
            {
                url = args[0];
            }
            //url = "http://imghosting.dcces.com/api/upload?inajax=1&ssl=1";

            var networks = NetworkInterface.GetAllNetworkInterfaces();
            long lastBytesRec = 0;
            long lastBytesSend = 0;
            while (true)
            {
                long nowBytesRec = 0;
                long nowBytesSend = 0;
                nowBytesSend = SystemHelper.getSendCount();
                nowBytesRec = SystemHelper.getRecCount();
                if (lastBytesRec == 0)
                {
                    lastBytesRec = nowBytesRec;
                    lastBytesSend = nowBytesSend;
                }
                var recinterval = nowBytesRec - lastBytesRec;
                var sendinterval = nowBytesSend - lastBytesSend;
                Console.WriteLine($"下载速度速度:{SystemHelper.getSpeed(recinterval, interval)}" +
                    $"上传速度速度:{SystemHelper.getSpeed(sendinterval, interval)}" +
                    $"下载总量:{SystemHelper.getCount(nowBytesRec)}" +
                    $"上传总量:{SystemHelper.getCount(nowBytesSend)}" +
                    $"内存使用量:{SystemHelper.getMemory()}");
                var model = new TranModel()
                {
                    RecSpeed = SystemHelper.getSpeed(recinterval, interval),
                    SendSpeed = SystemHelper.getSpeed(sendinterval, interval),
                    RecCount = SystemHelper.getCount(nowBytesRec),
                    SendCount = SystemHelper.getCount(nowBytesSend)
                };
                new WebHelper().doPost(url, JsonSerializer.Serialize(model));
                lastBytesRec = nowBytesRec;
                lastBytesSend = nowBytesSend;
                Thread.Sleep(interval);
            }
        }


    }
}
