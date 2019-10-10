using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monitor2.Models.Veiw
{
    public class StatServiceViewModel
    {
        public string ServiceName { get; set; }
        public StatServiceModel[] StatService { get; set; }
    }
}
