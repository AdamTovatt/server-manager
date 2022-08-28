using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerManager.Models
{
    public class RequestHeader
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public RequestHeader(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public static RequestHeader GetAuthorizationHeader(string accessKey)
        {
            return new RequestHeader("Authorization", string.Format("Bearer {0}", accessKey));
        }
    }
}
