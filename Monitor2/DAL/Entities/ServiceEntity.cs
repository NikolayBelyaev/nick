using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monitor2.DAL.Entities
{
    public class ServiceEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string TokenKey { get; set; }
        public string TokenValue { get; set; }
    }
}
