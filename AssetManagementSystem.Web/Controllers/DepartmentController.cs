using AssetManagementSystem.Web.Services.Interfaces; // หรือ .Services
using AssetManagementSystem.Web.ViewModels.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementSystem.Web.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: Department
        public async Task<IActionResult> Index([FromQuery] DepartmentListFilterViewModel filter)
        {
            var model = await _departmentService.GetDepartmentsAsync(filter);
            return View(model);
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            return View(new DepartmentCreateViewModel { IsActive = true });
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _departmentService.CreateAsync(model);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Department created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _departmentService.GetForEditAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _departmentService.UpdateAsync(model);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Department updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }

        // POST: Department/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _departmentService.DeleteAsync(id);
            if (result.Succeeded)
            {
                TempData["Success"] = "Department deleted successfully.";
            }
            else
            {
                TempData["Error"] = result.Errors.First().Description;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}