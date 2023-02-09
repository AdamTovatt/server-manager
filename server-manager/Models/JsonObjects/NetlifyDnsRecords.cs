using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerManager.Models.JsonObjects
{
    public class NetlifyDnsRecords
    {
        public List<NetlifyDnsRecord> Records { get; set; }

        public static NetlifyDnsRecords FromJson(string json)
        {
            NetlifyDnsRecords result = new NetlifyDnsRecords();
            result.Records = JsonConvert.DeserializeObject<List<NetlifyDnsRecord>>(json);
            return result;
        }

        internal NetlifyDnsRecord First()
        {
            if (Records == null || Records.Count == 0)
                return null;

            return Records.Where(x => x.Type == "A").FirstOrDefault();
        }
    }
}
