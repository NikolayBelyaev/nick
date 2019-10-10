using Monitor2.DAL.Entities;
using Monitor2.Models;
using Monitor2.Models.Enums;
using Monitor2.Models.Veiw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monitor2.Services.DataMappers
{
    public static class ServiceDataMapper
    {
        public static ServiceEntity ToEntity(this ServiceModel serviceModel)
        {
            return new ServiceEntity
            {
                Url = serviceModel.Url,
                TokenKey = serviceModel.TokenKey,
                TokenValue = serviceModel.TokenValue
            };
        }

        public static ResponseServiceEntity ToEntity(this ResponseServiceModel responseServiceModel)
        {
            return new ResponseServiceEntity
            {
                ServiceId = responseServiceModel.ServiceId,
                Duration = responseServiceModel.Duration,
                IsSuccessStatusCode = responseServiceModel.IsSuccessStatusCode,
                RequestTime = responseServiceModel.RequestTime,
                ServiceName = responseServiceModel.ServiceName
            };
        }

        public static StatServiceModel ToModel(this ResponseServiceEntity responseServiceEntity)
        {
            return new StatServiceModel
            {
                Duration = responseServiceEntity.Duration,
                Status = responseServiceEntity.IsSuccessStatusCode ? Availability.Available.ToViewString() : Availability.NotAvailable.ToViewString(),
                RequestTime = responseServiceEntity.RequestTime
            };
        }

        public static ServiceModel ToModel(this ServiceEntity serviceEntity)
        {
            return new ServiceModel
            {
                Id = serviceEntity.Id,
                Url = serviceEntity.Url,
                TokenKey = serviceEntity.TokenKey,
                TokenValue = serviceEntity.TokenValue
            };
        }

        public static string ToViewString(this Availability availability)
        {
            switch(availability)
            {
                case Availability.Available:
                    return "Доступен";
                default:
                    return "Недоступен";
            }
        }
    }
}
