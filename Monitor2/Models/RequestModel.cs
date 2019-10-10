using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monitor2.Models
{
    public class Result
    {
        public float Duration { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
    }

    public class RequestModel
    {
        public Result Result { get; set; }
    }
}
