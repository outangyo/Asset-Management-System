using AssetManagementSystem.Core.Repositories;
using AssetManagementSystem.Db.Data;
using AssetManagementSystem.Db.Entities;
using AssetManagementSystem.Web.Services.Interfaces;
using AssetManagementSystem.Web.ViewModels.Shared;
using AssetManagementSystem.Web.ViewModels.Suppliers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Web.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IRepository<Supplier> _supplierRepo;
        private readonly ApplicationDbContext _context;

        public SupplierService(IRepository<Supplier> supplierRepo, ApplicationDbContext context)
        {
            _supplierRepo = supplierRepo;
            _context = context;
        }

        public async Task<SupplierIndexViewModel> GetSuppliersAsync(SupplierListFilterViewModel filter)
        {
            // 1. สร้าง IQueryable (ตัวสร้าง query)
            // (เราต้องใช้ _context.Suppliers ตรงๆ เพื่อให้ .Where ทำงานที่ SQL Server)
            // (การใช้ _supplierRepo.GetAllAsync() จะดึง *ทั้งหมด* มาใน Memory ก่อน ซึ่งช้า)
            var query = _context.Suppliers.AsQueryable();

            // 2. กรอง (Filter) ตาม Status
            if (filter.IsActive.HasValue)
            {
                query = query.Where(s => s.IsActive == filter.IsActive.Value);
            }

            // 3. กรอง (Filter) ตามคำค้นหา (Search)
            if (!string.IsNullOrEmpty(filter.Search))
            {
                var searchTerm = filter.Search.ToLower();
                query = query.Where(s =>
                    s.Name.ToLower().Contains(searchTerm) ||
                    s.SupplierCode.ToLower().Contains(searchTerm) ||
                    (s.ContactName != null && s.ContactName.ToLower().Contains(searchTerm)) ||
                    (s.ContactPhone != null && s.ContactPhone.ToLower().Contains(searchTerm)) ||
                    (s.ContactEmail != null && s.ContactEmail.ToLower().Contains(searchTerm))
                );
            }

            // 4. นับจำนวนผลลัพธ์ทั้งหมด (ก่อนแบ่งหน้า)
            var totalCount = await query.CountAsync();

            // 5. แบ่งหน้า (Paging)
            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            // 6. แปลง List<Supplier> (Entity) เป็น List<SupplierListItemViewModel>
            var supplierViewModels = items.Select(supplier => new SupplierListItemViewModel
            {
                Id = supplier.Id,
                SupplierCode = supplier.SupplierCode,
                Name = supplier.Name,
                ContactName = supplier.ContactName,
                ContactEmail = supplier.ContactEmail,
                ContactPhone = supplier.ContactPhone,
                IsActive = supplier.IsActive
            }).ToList();

            // 7. สร้าง ViewModel ตัวหลัก
            var viewModel = new SupplierIndexViewModel
            {
                Filter = filter,
                PagedSuppliers = new PagedResult<SupplierListItemViewModel>
                {
                    Items = supplierViewModels,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize,
                    TotalCount = totalCount
                }
            };

            return viewModel;
        }

        public async Task<SupplierDetailsViewModel?> GetDetailsAsync(Guid id)
        {
            var supplier = await _supplierRepo.GetByIdAsync(id);

            if (supplier == null)
            {
                return null;
            }

            var viewModel = new SupplierDetailsViewModel
            {
                Id = supplier.Id,
                SupplierCode = supplier.SupplierCode,
                Name = supplier.Name,
                ContactName = supplier.ContactName,
                ContactEmail = supplier.ContactEmail,
                ContactPhone = supplier.ContactPhone,
                WebsiteURL = supplier.WebsiteURL,
                TaxId = supplier.TaxId,
                IsActive = supplier.IsActive,
                DateAdded = supplier.DateAdded,
                Notes = supplier.Notes
            };

            return viewModel;
        }

        // (ใช้ Generic Repo)
        public async Task<SupplierEditViewModel?> GetForEditAsync(Guid id)
        {
            // 1. "คนงาน" (Repo) ไปหาของ
            var supplier = await _supplierRepo.GetByIdAsync(id);

            if (supplier == null)
            {
                return null;
            }

            // 2. "ผู้จัดการ" (Service) แปลง Entity เป็น EditViewModel
            var viewModel = new SupplierEditViewModel
            {
                Id = supplier.Id,
                SupplierCode = supplier.SupplierCode,
                Name = supplier.Name,
                ContactName = supplier.ContactName,
                ContactEmail = supplier.ContactEmail,
                ContactPhone = supplier.ContactPhone,
                WebsiteURL = supplier.WebsiteURL,
                TaxId = supplier.TaxId,
                IsActive = supplier.IsActive,
                DateAdded = supplier.DateAdded, // (สำคัญมาก ต้องส่งค่าเดิมกลับไป)
                Notes = supplier.Notes
            };

            return viewModel;
        }

        // POST Edit (ใช้ Generic Repo) ---
        public async Task<IdentityResult> UpdateAsync(SupplierEditViewModel model)
        {
            // 1. "คนงาน" (Repo) ไปดึง Entity *ตัวเต็ม* จากฐานข้อมูล
            var supplierToUpdate = await _supplierRepo.GetByIdAsync(model.Id);

            if (supplierToUpdate == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Supplier not found." });
            }

            // 2. "ผู้จัดการ" (Service) เอาค่าจาก ViewModel ไป "ทับ" Entity
            supplierToUpdate.SupplierCode = model.SupplierCode;
            supplierToUpdate.Name = model.Name;
            supplierToUpdate.ContactName = model.ContactName;
            supplierToUpdate.ContactEmail = model.ContactEmail;
            supplierToUpdate.ContactPhone = model.ContactPhone;
            supplierToUpdate.WebsiteURL = model.WebsiteURL;
            supplierToUpdate.TaxId = model.TaxId;
            supplierToUpdate.IsActive = model.IsActive;
            supplierToUpdate.Notes = model.Notes;
            // (เราไม่แตะ DateAdded เพราะมันคือค่าเดิม)

            try
            {
                // 3. "คนงาน" (Repo) "ปักธง" ว่าจะ Update
                _supplierRepo.Update(supplierToUpdate);

                // 4. "Service" กด "ปุ่ม Save"
                await _context.SaveChangesAsync();

                return IdentityResult.Success;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // (จัดการ Error ถ้ามีคนแก้พร้อมกัน)
                return IdentityResult.Failed(new IdentityError { Description = "Failed to update supplier. Please try again." });
            }
        }

        public async Task<IdentityResult> DeleteAsync(Guid id)
        {
            // 1. "คนงาน" (Repo) ไปหาของที่จะลบ
            var supplierToDelete = await _supplierRepo.GetByIdAsync(id);

            if (supplierToDelete == null)
            {
                // ไม่เจออะไรให้ลบ
                return IdentityResult.Failed(new IdentityError { Description = "Supplier not found." });
            }

            try
            {
                // 2. "คนงาน" (Repo) "ปักธง" ว่าจะ Delete
                _supplierRepo.Delete(supplierToDelete);

                // 3. "Service" กด "ปุ่ม Save"
                await _context.SaveChangesAsync();

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                // (เผื่อมี Error ตอน Save เช่น มี Asset ผูกอยู่)
                return IdentityResult.Failed(new IdentityError { Description = $"Error deleting supplier: {ex.Message}" });
            }
        }

        // --- (ใช้ Generic Repo) ---
        public async Task<(IdentityResult result, Guid newId)> CreateAsync(SupplierCreateViewModel model)
        {
            // 1. "ผู้จัดการ" (Service) แปลง ViewModel เป็น Entity
            var newSupplier = new Supplier
            {
                // 2. ตั้งค่า Server-side
                Id = Guid.NewGuid(),
                DateAdded = DateTime.UtcNow,

                // 3. แมพค่าจากฟอร์ม
                SupplierCode = model.SupplierCode,
                Name = model.Name,
                ContactName = model.ContactName,
                ContactEmail = model.ContactEmail,
                ContactPhone = model.ContactPhone,
                WebsiteURL = model.WebsiteURL,
                TaxId = model.TaxId,
                IsActive = model.IsActive,
                Notes = model.Notes
            };

            try
            {
                // 4. "คนงาน" (Repo) "เตรียมสร้าง"
                await _supplierRepo.CreateAsync(newSupplier);

                // 5. "Service" กด "ปุ่ม Save"
                await _context.SaveChangesAsync();

                return (IdentityResult.Success, newSupplier.Id);
            }
            catch (Exception ex)
            {
                // (เผื่อ Error ตอน Save)
                return (IdentityResult.Failed(new IdentityError { Description = ex.Message }), Guid.Empty);
            }
        }

    }
}
