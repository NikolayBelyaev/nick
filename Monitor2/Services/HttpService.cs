using Monitor2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Monitor2.Services
{
    public class HttpService : IHttpService
    {
        readonly HttpClient _httpClient;

        public HttpService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        public async Task<ResponseServiceModel> CheckService(ServiceModel service)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, service.Url);
            if(service.TokenKey != null && service.TokenValue != null)
                requestMessage.Headers.Add(service.TokenKey, service.TokenValue);

            HttpResponseMessage responce = await _httpClient.SendAsync(requestMessage);

            RequestModel res = new RequestModel();

            if(service.TokenKey != null)
                res = JsonConvert.DeserializeObject<RequestModel>(responce.Content.ReadAsStringAsync().Result);


            var requestModel = new ResponseServiceModel
            {
                ServiceId = service.Id,
                ServiceName = service.Url,
                IsSuccessStatusCode = responce.IsSuccessStatusCode,
                RequestTime = DateTime.UtcNow,
                Duration = res.Result?.Duration ?? null
            };

            return requestModel;
        }
    }
}
