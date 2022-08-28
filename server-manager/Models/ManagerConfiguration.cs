using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerManager.Models
{
    public class ManagerConfiguration
    {
        [JsonProperty("accessKey")]
        public string AccessKey { get; set; }

        [JsonProperty("domains")]
        public List<string> Domains { get; set; }

        [JsonProperty("interval")]
        public int Interval { get; set; } = 1800;

        [JsonProperty("enableLogging")]
        public bool EnableLogging { get; set; }

        public static ManagerConfiguration FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ManagerConfiguration>(json);
        }
    }
}
