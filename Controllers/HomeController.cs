using FridgeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FridgeManagement.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult CustomerLiaison()
        {
            return View();
        }

        public IActionResult InventoryLiaison()
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
