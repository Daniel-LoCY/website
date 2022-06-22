using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using Newtonsoft.Json;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel();
            return View(viewModel);
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

        public IActionResult TestResponse([Bind(Prefix = "Information")] Information information)
        {
            WebApplication1.Service.RequestService requestService = new Service.RequestService();
            var s = requestService.Post(information, "test/post");
            var data = JsonConvert.DeserializeObject<Information>(s);
            var viewModel = new HomeViewModel();
            viewModel.Information.id = data.id;

            return View("index", viewModel);
        }
    }
}