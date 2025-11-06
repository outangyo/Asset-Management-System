using AssetManagementSystem.Core.Repositories;
using AssetManagementSystem.Db.Data;
using AssetManagementSystem.Db.Entities;
using AssetManagementSystem.Web.Services;
using AssetManagementSystem.Web.ViewModels.Shared;
using AssetManagementSystem.Web.ViewModels.Suppliers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Web.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SupplierController( ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // GET: /Supplier (หน้า List) ---
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] SupplierListFilterViewModel filter)
        {
            var viewModel = await _supplierService.GetSuppliersAsync(filter);
            return View(viewModel);
        }

        // GET: /Supplier/Details/guid-xxxx
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound(); // ถ้าไม่มี id ส่งมา
            }

            // 1. "ผู้จัดการสาขา" (Controller) สั่ง "ผู้จัดการแผนก" (Service)
            var viewModel = await _supplierService.GetDetailsAsync(id.Value);

            // 2. Service คืนค่ามาว่า "ไม่เจอ"
            if (viewModel == null)
            {
                return NotFound(); // ถ้าหา Supplier ไม่เจอ
            }

            // 3. ส่ง ViewModel ไปที่หน้า View
            return View(viewModel);
        }

        // ---  GET: /Supplier/Edit/guid-xxxx ---
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // "ผู้จัดการสาขา" (Controller) สั่ง Service ไปเอาข้อมูลมา
            var viewModel = await _supplierService.GetForEditAsync(id.Value);

            if (viewModel == null)
            {
                return NotFound();
            }

            // ส่ง ViewModel (ที่มีข้อมูล) ไปให้หน้า View
            return View(viewModel);
        }

        // --- POST: /Supplier/Edit/guid-xxxx ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            // ตรวจสอบว่าข้อมูลที่ส่งมา (เช่น [Required]) ถูกต้องหรือไม่
            if (ModelState.IsValid)
            {
                // "ผู้จัดการสาขา" (Controller) สั่ง Service ให้ไป Save
                var result = await _supplierService.UpdateAsync(model);

                if (result.Succeeded)
                {
                    // ถ้าสำเร็จ ให้กลับไปหน้า Index
                    return RedirectToAction(nameof(Index));
                }

                // ถ้าไม่สำเร็จ (เช่น Error จาก Service)
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // ถ้า ModelState ไม่ Valid (กรอกไม่ครบ) หรือ Save ไม่สำเร็จ
            // ให้กลับไปหน้า Edit ฟอร์มเดิม พร้อมข้อมูลที่กรอกมา
            return View(model);
        }

    }
}