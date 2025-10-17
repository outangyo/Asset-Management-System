using AssetManagementSystem.Db.Entities;
using AssetManagementSystem.Web.Models.Assets;
using AssetManagementSystem.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementSystem.Web.Controllers
{
    [Authorize]
    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;
        private readonly ILogger<AssetController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public AssetController(IAssetService assetService, ILogger<AssetController> logger, UserManager<ApplicationUser> userManager)
        {
            _assetService = assetService;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] AssetListFilterViewModel filter)
        {
            try
            {
                var pagedResult = await _assetService.GetAssetsAsync(filter);

                var model = new AssetIndexViewModel
                {
                    PagedAssets = pagedResult,
                    Filter = filter
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching assets.");
                return View("Error");
            }
        }

        // GET: /Asset/Create
        [HttpGet]
        public IActionResult Create()
        {
            // ส่ง ViewModel เปล่าๆ ไปให้ View เพื่อสร้างฟอร์ม
            return View(new AssetCreateViewModel());
        }

        // POST: /Asset/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssetCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // ดึง UserId ของคนที่ล็อกอินอยู่
                var userId = Guid.Parse(_userManager.GetUserId(User));

                // เรียกใช้ Service เพื่อสร้าง Asset
                var (result, id) = await _assetService.CreateAsync(model, userId);

                if (result.Succeeded)
                {
                    // ตั้งค่า TempData เพื่อแสดงข้อความ "Success" ในหน้า Index
                    TempData["Success"] = $"Asset '{model.Name}' created successfully.";
                    return RedirectToAction(nameof(Index));
                }

                // ถ้า Service คืนค่า Error มา
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating asset.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred while creating the asset.");
            }

            return View(model);
        }

        // ใน AssetController.cs
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(); // ถ้าไม่มี Id ส่งมา
            }

            try
            {
                var model = await _assetService.GetDetailsAsync(id);

                if (model == null)
                {
                    return NotFound(); // ถ้า Service หา Asset ไม่เจอ
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching details for asset ID {AssetId}", id);
                return View("Error");
            }
        }

        // GET: /Assets/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                var model = await _assetService.GetForEditAsync(id);
                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching asset for edit with ID {AssetId}", id);
                return View("Error");
            }
        }

        // POST: /Assets/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AssetEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var result = await _assetService.UpdateAsync(model);
                if (result.Succeeded)
                {
                    TempData["Success"] = $"Asset '{model.Name}' updated successfully.";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating asset {AssetId}", model.Id);
                ModelState.AddModelError(string.Empty, "An unexpected error occurred.");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                var result = await _assetService.DeleteAsync(id);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Asset deleted successfully.";
                }
                else
                {
                    // นำ Error แรกมาแสดง (ถ้ามี)
                    TempData["Error"] = result.Errors.FirstOrDefault()?.Description ?? "Failed to delete asset.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting asset {AssetId}", id);
                TempData["Error"] = "An unexpected error occurred while deleting the asset.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
