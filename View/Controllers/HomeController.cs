using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public class Information
        {
            public int id { get; set; } = 1;
        }
        public IActionResult Index()
        {
            WebApplication1.Service.RequestService requestService = new Service.RequestService();
            Information information = new Information()
            {
                id = 4
            };
            var s = requestService.Post(information);
            var data = JsonConvert.DeserializeObject<Information>(s);
            TempData["response"] = data.id;
            return View();
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