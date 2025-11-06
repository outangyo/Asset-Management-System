using AssetManagementSystem.Core.Repositories;
using AssetManagementSystem.Db.Data;
using AssetManagementSystem.Db.Entities;
using AssetManagementSystem.Web.ViewModels.Shared;
using AssetManagementSystem.Web.ViewModels.Suppliers;
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

    }
}
