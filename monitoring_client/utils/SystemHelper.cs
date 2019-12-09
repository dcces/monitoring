using System;
using System.Collections.Generic;
using System.Text;

namespace monitoring_client.utils
{
    public class SystemHelper
    {
        public static string getSpeed(long byteCount, int timestamp = 1000)
        {
            return getNumber(byteCount, timestamp) + getUnit(byteCount, timestamp);
        }
        private static string getNumber(long byteCount, int timestamp)
        {
            if (byteCount <= 1024)
            {
                return byteCount.ToString();
            }
            if (byteCount <= 1024 * 1024)
            {
                return Math.Round((byteCount / (1024.0)), 2).ToString();
            }
            return Math.Round((byteCount / (1024 * 1024.0)), 2).ToString();
        }
        private static string getUnit(long byteCount, int timestamp)
        {
            if (byteCount <= 1024)
            {
                return "b/s";
            }
            if (byteCount <= 1024 * 1024)
            {
                return "kb/s";
            }
            return "mb/s";
        }
    }
}
