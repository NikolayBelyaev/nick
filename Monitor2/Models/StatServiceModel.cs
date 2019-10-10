using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monitor2.Models
{
    public class StatServiceModel
    {
        public string Status { get; set; }
        public float? Duration { get; set; }
        public DateTime RequestTime { get; set; }
    }
}
