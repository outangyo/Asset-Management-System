using Microsoft.AspNetCore.Authorization;
using AssetManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AssetManagementSystem.Web.ViewModels.Users;
using AssetManagementSystem.Web.ViewModels.Home;

namespace AssetManagementSystem.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IDashboardService _dashboardService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IDashboardService dashboardService)
        {
            _logger = logger;
            _userService = userService;
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // เรียกข้อมูลตัวเลขทั้งหมดมา
            var viewModel = await _dashboardService.GetDashboardDataAsync();

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

        [AllowAnonymous]
        public IActionResult NonSecureMethod()
        {
            return View();
        }

        [Authorize]
        public IActionResult SecureMethod()
        {
            return View();
        }
    }
}
