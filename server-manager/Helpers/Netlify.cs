using ServerManager.Models;
using ServerManager.Models.JsonObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServerManager
{
    internal class Netlify
    {
        private string accessKey;

        public Netlify(string accessKey)
        {
            this.accessKey = accessKey;
        }

        public static string ToDnsZone(string domain)
        {
            return String.Join("_", domain.Split(".").TakeLast(2));
        }

        public async Task<NetlifyDnsRecord> GetDnsRecordAsync(string domain)
        {
            string requestUrl = string.Format("https://api.netlify.com/api/v1/dns_zones/{0}/dns_records", ToDnsZone(domain));

            string response = await WebRequester.GetAsync(requestUrl, RequestHeader.GetAuthorizationHeader(accessKey));

            if (response.Length <= 2)
                return null;

            NetlifyDnsRecords records = NetlifyDnsRecords.FromJson(response);

            return records.First(domain);
        }

        public async Task<bool> DeleteDnsRecordAsync(NetlifyDnsRecord record)
        {
            string requestUrl = string.Format("https://api.netlify.com/api/v1/dns_zones/{0}/dns_records/{1}", ToDnsZone(record.Hostname), record.Id);

            await WebRequester.PostAsync(requestUrl, "", "", "DELETE", RequestHeader.GetAuthorizationHeader(accessKey));

            return true;
        }

        public async Task<NetlifyDnsRecord> AddDnsRecordAsync(NetlifyDnsRecord record)
        {
            string requestUrl = string.Format("https://api.netlify.com/api/v1/dns_zones/{0}/dns_records", ToDnsZone(record.Hostname));

            string response = await WebRequester.PostAsync(requestUrl, record.ToJson(), "application/json", "POST", RequestHeader.GetAuthorizationHeader(accessKey));

            return NetlifyDnsRecord.FromJson(response);
        }
    }
}
