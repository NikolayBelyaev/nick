using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monitor2.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string TokenKey { get; set; }
        public string TokenValue { get; set; }

        public ServiceModel(string url, string tokenKey, string tokenValue)
        {
            Url = url;
            TokenKey = tokenKey;
            TokenValue = tokenValue;
        }

        public ServiceModel() { }
    }
}
