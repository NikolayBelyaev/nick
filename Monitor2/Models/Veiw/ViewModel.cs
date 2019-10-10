using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monitor2.Models.Veiw
{
    public class ViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Availability { get; set; }
        public float? Duration { get; set; }
        public string DurationDivClass { get; set; }
        public int FailCountPerHour { get; set; }
        public float? MaxHourDuration { get; set; }
        public int FailCountPerDay { get; set; }
        public float? MaxDayDuration { get; set; }
    }
}
