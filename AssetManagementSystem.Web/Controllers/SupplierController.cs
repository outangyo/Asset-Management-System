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

        // --- 1. GET: /Supplier (หน้า List) ---
        public async Task<IActionResult> Index([FromQuery] SupplierListFilterViewModel filter)
        {
            var viewModel = await _supplierService.GetSuppliersAsync(filter);
            return View(viewModel);
        }

        // --- 2. GET: /Supplier/Details/guid-xxxx ---
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    // (ในอนาคตคุณอาจจะอยากใช้ Include ที่นี่ แต่ตอนนี้ GetById ก็พอ)
        //    var supplier = await _supplierService.GetByIdAsync(id.Value);
        //    if (supplier == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(supplier);
        //}

        //// --- 3. GET: /Supplier/Create (แสดงฟอร์มเปล่า) ---
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// --- 4. POST: /Supplier/Create (รับค่าจากฟอร์ม) ---
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,SupplierCode,Name,ContactName,ContactEmail,ContactPhone,WebsiteURL,TaxId,IsActive,Notes")] Supplier supplier)
        //{
        //    // (เราใช้ [Bind] เพื่อป้องกัน Overposting)
        //    // (ในชีวิตจริงควรใช้ ViewModel)

        //    // สร้าง Id ใหม่และตั้งค่าเริ่มต้น
        //    supplier.Id = Guid.NewGuid();
        //    supplier.DateAdded = DateTime.UtcNow;

        //    if (ModelState.IsValid)
        //    {
        //        await _supplierRepo.CreateAsync(supplier);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    // ถ้า Model ไม่ Valid ก็กลับไปหน้าเดิมพร้อมข้อมูลที่กรอกมา
        //    return View(supplier);
        //}

        //// --- 5. GET: /Supplier/Edit/guid-xxxx (แสดงฟอร์มที่มีข้อมูลเดิม) ---
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var supplier = await _supplierRepo.GetByIdAsync(id.Value);
        //    if (supplier == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(supplier);
        //}

        //// --- 6. POST: /Supplier/Edit/guid-xxxx (รับค่าจากฟอร์มที่แก้ไข) ---
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Id,SupplierCode,Name,ContactName,ContactEmail,ContactPhone,WebsiteURL,TaxId,IsActive,DateAdded,Notes")] Supplier supplier)
        //{
        //    if (id != supplier.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _supplierRepo.Update(supplier); // "ปักธง" ว่าจะ Update
        //            await _context.SaveChangesAsync(); // "กด Save"
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            // (จัดการ Error ถ้ามีคนแก้ข้อมูลนี้พร้อมกัน)
        //            var exists = await _supplierRepo.GetByIdAsync(id);
        //            if (exists == null)
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(supplier);
        //}

        //// --- 7. GET: /Supplier/Delete/guid-xxxx (แสดงหน้ายืนยันการลบ) ---
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var supplier = await _supplierRepo.GetByIdAsync(id.Value);
        //    if (supplier == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(supplier); // ส่ง Model ไปให้ View "Delete.cshtml"
        //}

        //// --- 8. POST: /Supplier/Delete/guid-xxxx (ยืนยันการลบจริง) ---
        //[HttpPost, ActionName("Delete")] // ActionName("Delete") เพื่อให้ URL ตรงกัน
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var supplierToDelete = await _supplierRepo.GetByIdAsync(id);
        //    if (supplierToDelete != null)
        //    {
        //        _supplierRepo.Delete(supplierToDelete); // "ปักธง" ว่าจะ Delete
        //        await _context.SaveChangesAsync(); // "กด Save"
        //    }

        //    return RedirectToAction(nameof(Index));
        //}
    }
}