using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Init.Models;
using Restaurant_Init.Services;
using System.Diagnostics;

namespace Restaurant_Init.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CommonService _commonService;

        public HomeController(ILogger<HomeController> logger, CommonService commonService)
        {
            _logger = logger;
            _commonService = commonService;
        }
        //[Authorize]
        public IActionResult Index()
        {
            
            return View();
            
        }
        [Authorize]
        public IActionResult BackStage()
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