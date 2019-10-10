using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monitor2.Models;
using Monitor2.Services;

namespace Monitor2.Controllers
{
    public class ServiceController : Controller
    {
        readonly IDBService _dbService;

        public ServiceController(IDBService dbService)
        {
            _dbService = dbService;
        }

        public void AddService(string url, string tokenKey, string tokenValue)
        {
            var service = new ServiceModel(url, tokenKey, tokenValue);

            _dbService.SetData(service);
        }

        public ServiceModel[] GetServices()
        {
            return _dbService.GetServices();
        }

        public ServiceModel GetService(int id)
        {
            return _dbService.GetService(id);
        }
    }
}