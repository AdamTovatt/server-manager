using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerManager.Models.JsonObjects
{
    public class NetlifyDnsRecord
    {
        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }

        [JsonProperty("priority")]
        public object Priority { get; set; }

        [JsonProperty("weight")]
        public object Weight { get; set; }

        [JsonProperty("port")]
        public object Port { get; set; }

        [JsonProperty("flag")]
        public object Flag { get; set; }

        [JsonProperty("tag")]
        public object Tag { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("site_id")]
        public object SiteId { get; set; }

        [JsonProperty("dns_zone_id")]
        public string DnsZoneId { get; set; }

        [JsonProperty("errors")]
        public List<object> Errors { get; set; }

        [JsonProperty("managed")]
        public bool Managed { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        public static NetlifyDnsRecord FromJson(string json)
        {
            return JsonConvert.DeserializeObject<NetlifyDnsRecord>(json);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
