using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Monitor2.Models;
using Monitor2.Services;

namespace Monitor2.Controllers
{
    public class HomeController : Controller
    {
        readonly IRequestService _requestService;

        public HomeController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public IActionResult Index()
        {
            var view = _requestService.CheckServicesAsync();

            return View(view.Result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
