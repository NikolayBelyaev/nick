using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monitor2.DAL.Entities
{
    public class ResponseServiceEntity
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public float? Duration { get; set; }
        public DateTime RequestTime { get; set; }
    }
}
