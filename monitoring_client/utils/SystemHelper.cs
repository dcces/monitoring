using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;

namespace monitoring_client.utils
{
    public class SystemHelper
    {
        /// <summary>
        /// 流量总计
        /// </summary>
        /// <param name="byteCount"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static string getCount(long byteCount, int timestamp = 1000)
        {
            var tuple = ConvertByte(byteCount);
            return $"{(tuple.Item1)}{ tuple.Item2}";
        }
        /// <summary>
        /// 流量速度
        /// </summary>
        /// <param name="byteCount"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static string getSpeed(long byteCount, int timestamp = 1000)
        {
            var tuple = ConvertByte(byteCount * 1000 / timestamp);
            return $"{(tuple.Item1)}{ tuple.Item2}/s";
        }
        public static long getSendCount()
        {
            var networks = NetworkInterface.GetAllNetworkInterfaces();
            long nowBytesSend = 0;
            foreach (var network in networks)
            {
                nowBytesSend += network.GetIPv4Statistics().BytesSent;
            }
            return nowBytesSend;
        }
        public static long getRecCount()
        {
            var networks = NetworkInterface.GetAllNetworkInterfaces();
            long nowBytesRec = 0;
            foreach (var network in networks)
            {
                nowBytesRec += network.GetIPv4Statistics().BytesReceived;
            }
            return nowBytesRec;
        }
        /// <summary>
        /// 获取内存信息
        /// </summary>
        /// <returns></returns>
        public static string getMemory()
        {
            var memoryInfos = File.ReadAllLines(@"/proc/meminfo");

            foreach (string i in memoryInfos)
            {
                Console.WriteLine(i.ToString() + "\r");
            }
            var memoryTotalInfo = memoryInfos[0];

            var processes = Process.GetProcesses();
            long memoryCount = 0;
            foreach (var process in processes)
            {
                memoryCount += process.WorkingSet64;

            }
            var tuple = ConvertByte(memoryCount);
            var s = Environment.WorkingSet;
            return tuple.Item1 + tuple.Item2;

        }

        /// <summary>
        /// 根据字节数 返回对应的数据
        /// </summary>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        public static Tuple<double, string> ConvertByte(long byteCount)
        {
            string[] units = new string[] { "b", "kb", "mb", "gb", "tb", "pb" };
            long mod = 1024;
            int i = 0;
            double value = byteCount;
            while (value >= mod)
            {
                // byteCount /= mod * 1.0;
                //value = value / (mod * 1.0);
                value /= (mod * 1.0);
                i++;
            }
            return new Tuple<double, string>(Math.Round(Convert.ToDouble(value), 2), units[i]);
        }


    }
}
