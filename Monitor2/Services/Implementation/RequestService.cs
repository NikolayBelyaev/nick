using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Monitor2.Models;
using Monitor2.Models.Enums;
using Monitor2.Models.Veiw;
using Monitor2.Services.DataMappers;

namespace Monitor2.Services.Implementation
{
    public class RequestService : IRequestService
    {
        IHttpService _httpService;
        IDBService _dbService;
        IRequestDBService _requestDBService;
        ServicesOptions _config;

        public RequestService(IHttpService httpService, IDBService dbService, IRequestDBService requestDBService, IOptions<ServicesOptions> options)
        {
            _httpService = httpService;
            _dbService = dbService;
            _requestDBService = requestDBService;
            _config = options.Value;
        }
        public async Task<List<ViewModel>> CheckServicesAsync()
        {
            var services = _dbService.GetServices();

            List<ResponseServiceModel> responseServices = new List<ResponseServiceModel>();

            foreach(var service in services)
            {
                responseServices.Add(await _httpService.CheckService(service));
            }

            List<ViewModel> views = new List<ViewModel>();
            foreach(var responseService in responseServices)
            {
                var view = new ViewModel
                {
                    Id = responseService.ServiceId,
                    Url = responseService.ServiceName,
                    Availability = responseService.IsSuccessStatusCode ? Availability.Available.ToViewString() : Availability.NotAvailable.ToViewString(),
                    Duration = responseService.Duration,
                    FailCountPerDay = _requestDBService.GetFailCountPerTime(responseService.ServiceId, TimeSpan.FromHours(24)),
                    FailCountPerHour = _requestDBService.GetFailCountPerTime(responseService.ServiceId, TimeSpan.FromHours(1)),
                };
                var durationsHour = _requestDBService.GetDurationsPerTime(responseService.ServiceId, TimeSpan.FromHours(1));
                var durationsDay = _requestDBService.GetDurationsPerTime(responseService.ServiceId, TimeSpan.FromHours(24));

                if (responseService.Duration > _config.StandartDuration * 2f)
                    view.DurationDivClass = "text-error";

                if(durationsHour.Count() != 0)
                    if (durationsHour.Average() > _config.StandartDuration * 2f)
                        view.MaxHourDuration = durationsHour.Max();

                if(durationsDay.Count() != 0)
                    if (durationsDay.Average() > _config.StandartDuration * 2f)
                        view.MaxDayDuration = durationsDay.Max();

                views.Add(view);
            }

            return views;
        }
    }
}
