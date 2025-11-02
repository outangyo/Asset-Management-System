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

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] UserListFilterViewModel filter)
        {
            try
            {
                var pagedUsers = await _userService.GetUsersAsync(filter);
                var model = new UserIndexViewModel
                {
                    PagedUsers = pagedUsers,
                    Filter = filter
                };
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching users for the dashboard.");
                return View("Error");
            }
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
