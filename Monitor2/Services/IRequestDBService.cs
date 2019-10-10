using Monitor2.Models;
using Monitor2.Models.Veiw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monitor2.Services
{
    public interface IRequestDBService
    {
        void SetData(ResponseServiceModel responceServiceModel);
        StatServiceViewModel GetResponseService(string url, int offset, int count);
        int GetFailCountPerTime(int serviceId, TimeSpan time);
        float[] GetDurationsPerTime(int serviceId, TimeSpan time);
    }
}
