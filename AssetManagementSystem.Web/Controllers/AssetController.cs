using AssetManagementSystem.Core.Repositories;
using AssetManagementSystem.Db.Entities;
using AssetManagementSystem.Web.Services.Interfaces;
using AssetManagementSystem.Web.ViewModels.Assets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AssetManagementSystem.Web.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;
        private readonly ILogger<AssetController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IRepository<Category> _categoryRepo;
        private readonly IRepository<Department> _departmentRepo;
        private readonly IRepository<Location> _locationRepo;

        public AssetController(IAssetService assetService, ILogger<AssetController> logger,
            UserManager<ApplicationUser> userManager, IRepository<Category> categoryRepo, IRepository<Department> departmentRepo,
        IRepository<Location> locationRepo)
        {
            _assetService = assetService;
            _logger = logger;
            _userManager = userManager;
            _categoryRepo = categoryRepo;
            _departmentRepo = departmentRepo;
            _locationRepo = locationRepo;
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
        public async Task<IActionResult> Create()
        {
            var model = new AssetCreateViewModel();

            // --- เติมของใส่ Dropdown ก่อนส่งไป View ---
            await PopulateDropdowns(model);

            return View(model);
        }

        // POST: /Asset/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssetCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // --- ถ้า Validation พลาด ต้องเติม Dropdown ใหม่ ไม่งั้นหน้าเว็บจะว่าง ---
                await PopulateDropdowns(model);
                return View(model);
            }

            try
            {
                var userId = Guid.Parse(_userManager.GetUserId(User));

                var (result, id) = await _assetService.CreateAsync(model, userId);

                if (result.Succeeded)
                {
                    TempData["Success"] = $"Asset '{model.Name}' created successfully.";
                    return RedirectToAction(nameof(Index));
                }

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

            // --- ถ้า Save ไม่ผ่าน ก็ต้องเติม Dropdown ใหม่เหมือนกัน ---
            await PopulateDropdowns(model);
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

        // GET: /Assets/Edit/{Guid}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                // ดึงข้อมูล Asset เดิมมา (Service จะคืนค่า CategoryId, DeptId มาให้แล้ว)
                var model = await _assetService.GetForEditAsync(id);

                if (model == null)
                {
                    return NotFound();
                }

                // [สำคัญ!] ต้องเติม Dropdown ก่อนส่งไป View ไม่งั้น Dropdown ว่าง
                await PopulateDropdowns(model);

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
                // [สำคัญ!] ถ้า Validation ไม่ผ่าน (User กรอกผิด)
                // ต้องเติม Dropdown ใหม่ก่อนส่งกลับไป ไม่งั้น Dropdown จะหายไปเลย
                await PopulateDropdowns(model);
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

            // [สำคัญ!] ถ้า Save ไม่ผ่าน (เช่น DB Error) ก็ต้องเติม Dropdown ด้วย
            await PopulateDropdowns(model);
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

        //Helpers Methods
        private async Task PopulateDropdowns(AssetCreateViewModel model)
        {
            // 1. ดึงข้อมูลดิบจาก Repo
            var categories = await _categoryRepo.GetAllAsync();
            var departments = await _departmentRepo.GetAllAsync();
            var locations = await _locationRepo.GetAllAsync();

            // 2. แปลงเป็น SelectListItem ใส่ลงใน ViewModel
            model.Categories = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            model.Departments = departments.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name });
            model.Locations = locations.Select(l => new SelectListItem { Value = l.Id.ToString(), Text = l.Name });
        }

        private async Task PopulateDropdowns(AssetEditViewModel model)
        {
            var categories = await _categoryRepo.GetAllAsync();
            var departments = await _departmentRepo.GetAllAsync();
            var locations = await _locationRepo.GetAllAsync();

            model.Categories = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            model.Departments = departments.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name });
            model.Locations = locations.Select(l => new SelectListItem { Value = l.Id.ToString(), Text = l.Name });
        }
    }
}
