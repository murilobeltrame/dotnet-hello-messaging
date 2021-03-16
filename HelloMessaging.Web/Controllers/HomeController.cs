using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HelloMessaging.Web.Models;
using HelloMessaging.Lib;
using System.Threading.Tasks;

namespace HelloMessaging.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBusService _busService;

        public HomeController(ILogger<HomeController> logger, IBusService busService)
        {
            _logger = logger;
            _busService = busService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ChatMessage chatMessage) {
            await _busService.Client("chatting").SendMessageAsync(chatMessage);
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
