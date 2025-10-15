using AssetManagementSystem.Web.Models.Assets;
using AssetManagementSystem.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementSystem.Web.Controllers
{
    [Authorize]
    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;
        private readonly ILogger<AssetController> _logger;

        public AssetController(IAssetService assetService, ILogger<AssetController> logger)
        {
            _assetService = assetService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] AssetListFilterViewModel filter)
        {
            try
            {
                var result = await _assetService.GetAssetsAsync(filter);
                return View(result); // ส่ง PagedResult ไปให้ View
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching assets.");
                return View("Error");
            }
        }
    }
}
