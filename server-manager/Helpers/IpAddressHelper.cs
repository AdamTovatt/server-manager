using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerManager
{
    internal class IpAddressHelper
    {
        public static IPAddress GetIpAddress()
        {
            string externalIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
            return IPAddress.Parse(externalIpString);
        }
    }
}
