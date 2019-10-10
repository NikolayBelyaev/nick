using Monitor2.Models.Veiw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monitor2.Services
{
    public interface IRequestService
    {
        Task<List<ViewModel>> CheckServicesAsync();
    }
}
