using DataProtection.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DataProtection.Controllers
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
            string critical = "Burada bağlantı cümleniz kayıtlı";
            DataProtector protector = new DataProtector("info.txt");
            var length = protector.EncryptData(critical);
            var decrpted = protector.DecryptData(length);
            ViewBag.Decrypted = decrpted;
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
