using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Monitor2.DAL;
using Monitor2.DAL.Entities;
using Monitor2.Models;
using Monitor2.Models.Veiw;
using Monitor2.Services.DataMappers;

namespace Monitor2.Services.Implementation
{
    public class RequestDBService : IRequestDBService
    {
        MonitorDBContext _monitorDBContext;

        public RequestDBService(MonitorDBContext monitorDBContext)
        {
            _monitorDBContext = monitorDBContext;
        }

        public void SetData(ResponseServiceModel responceServiceModel)
        {
            if (responceServiceModel == null)
                throw new Exception("Model is null");

            _monitorDBContext.Add(responceServiceModel.ToEntity());
            _monitorDBContext.SaveChanges();
        }

        public int GetFailCountPerTime(int serviceId, TimeSpan time)
        {
            var dt = DateTime.UtcNow - time;
            var count = _monitorDBContext.ResponseServices
                .Where(x => x.ServiceId == serviceId)
                .Where(x => x.IsSuccessStatusCode == false)
                .Where(x => x.RequestTime > dt)
                .Count();
            return count;
        }

        public float[] GetDurationsPerTime(int serviceId, TimeSpan time)
        {
            var dt = DateTime.UtcNow - time;
            var durations = _monitorDBContext.ResponseServices
                .Where(x => x.ServiceId == serviceId)
                .Where(x => x.RequestTime > dt)
                .Where(x => x.Duration != null)
                .Select(x => x.Duration.Value)
                .ToArray();

            if (durations == null)
                throw new Exception("Wrong serviceId or no data in database");

            return durations;
        }

        public StatServiceViewModel GetResponseService(string url, int offset, int count)
        {
            var stats = _monitorDBContext.ResponseServices
                .Where(x => x.ServiceName == url)
                .OrderByDescending(x => x.RequestTime)
                .Skip(offset)
                .Take(count)
                .Select(x => x.ToModel())
                .ToArray();

            return new StatServiceViewModel
            {
                ServiceName = url,
                StatService = stats
            };
        }
    }
}
