using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Monitor2.Models;
using Monitor2.Services;

namespace Monitor2.Controllers
{
    public class RequestController : Controller
    {
        readonly IRequestDBService _requestDBService;

        public RequestController(IRequestDBService requestDBService)
        {
            _requestDBService = requestDBService;
        }

        public IActionResult ResponseService(string url, int offset, int count)
        {
            var services = _requestDBService.GetResponseService(url, offset, count);

            return View(services);
        }
    }
}