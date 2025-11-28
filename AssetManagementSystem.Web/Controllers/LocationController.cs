using AssetManagementSystem.Web.Services.Interfaces;
using AssetManagementSystem.Web.ViewModels.Locations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementSystem.Web.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        // GET: Location
        public async Task<IActionResult> Index([FromQuery] LocationListFilterViewModel filter)
        {
            var model = await _locationService.GetLocationsAsync(filter);
            return View(model);
        }

        // GET: Location/Create
        public IActionResult Create()
        {
            return View(new LocationCreateViewModel { IsActive = true });
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocationCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _locationService.CreateAsync(model);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Location created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }

        // GET: Location/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _locationService.GetForEditAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LocationCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _locationService.UpdateAsync(model);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Location updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }

        // POST: Location/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _locationService.DeleteAsync(id);
            if (result.Succeeded)
            {
                TempData["Success"] = "Location deleted successfully.";
            }
            else
            {
                TempData["Error"] = result.Errors.First().Description;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}