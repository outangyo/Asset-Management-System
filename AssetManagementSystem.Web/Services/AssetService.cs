using AssetManagementSystem.Core.Repositories;
using AssetManagementSystem.Db;
using AssetManagementSystem.Db.Data;
using AssetManagementSystem.Db.Entities;
using AssetManagementSystem.Web.Services.Interfaces;
using AssetManagementSystem.Web.ViewModels.Assets;
using AssetManagementSystem.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Web.Services
{
    public class AssetService : IAssetService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Asset> _assetRepo; // 1. เพิ่ม Repo

        public AssetService(ApplicationDbContext context, IRepository<Asset> assetRepo)
        {
            _context = context;
            _assetRepo = assetRepo;
        }

        // --- 1. Get All (ซับซ้อน -> ใช้ DbContext ตรงๆ) ---
        public async Task<PagedResult<AssetListItemViewModel>> GetAssetsAsync(AssetListFilterViewModel filter)
        {
            // ต้อง Include ตารางลูก เพื่อให้ Search ชื่อเจอ และเอาชื่อไปแสดง
            var query = _context.Assets
                .Include(a => a.Category)
                .Include(a => a.Department)
                .Include(a => a.Location)
                .AsNoTracking();

            // Filter Search (ค้นหาจากชื่อในตารางลูก)
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var searchTerm = filter.Search.Trim();
                query = query.Where(a => a.Name.Contains(searchTerm)
                                      || a.Code.Contains(searchTerm)
                                      || a.Category.Name.Contains(searchTerm)    // ค้นหาจาก Category Name
                                      || a.Department.Name.Contains(searchTerm)  // ค้นหาจาก Department Name
                                      || a.Location.Name.Contains(searchTerm));  // ค้นหาจาก Location Name
            }

            if (filter.IsActive.HasValue)
            {
                query = query.Where(a => a.IsActive == filter.IsActive.Value);
            }

            var total = await query.CountAsync();

            var items = await query
                .OrderBy(a => a.Name)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(a => new AssetListItemViewModel
                {
                    Id = a.Id,
                    Code = a.Code,
                    Name = a.Name,
                    // Map ชื่อจากตารางลูกมาใส่ ViewModel
                    Category = a.Category.Name,
                    Department = a.Department.Name,
                    Location = a.Location.Name,
                    DateRegister = a.DateRegister,
                    IsActive = a.IsActive
                })
                .ToListAsync();

            return new PagedResult<AssetListItemViewModel>
            {
                Items = items,
                TotalCount = total,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }

        // --- 2. Create (ง่าย -> ใช้ Generic Repo) ---
        public async Task<(IdentityResult result, Guid id)> CreateAsync(AssetCreateViewModel model, Guid userId)
        {
            var asset = new Asset
            {
                Id = Guid.NewGuid(),
                Code = model.Code,
                Name = model.Name,
                Description = model.Description,

                // บันทึกเป็น ID (Guid) แทน String
                CategoryId = model.CategoryId,     // ต้องแก้ ViewModel เป็น Guid
                DepartmentId = model.DepartmentId, // ต้องแก้ ViewModel เป็น Guid
                LocationId = model.LocationId,     // ต้องแก้ ViewModel เป็น Guid

                Note = model.Note,
                DateRegister = model.DateRegister,
                IsActive = model.IsActive,
                UserId = userId
            };

            try
            {
                // ใช้ Repo
                await _assetRepo.CreateAsync(asset);
                await _context.SaveChangesAsync();

                return (IdentityResult.Success, asset.Id);
            }
            catch (Exception ex)
            {
                return (IdentityResult.Failed(new IdentityError { Description = ex.Message }), Guid.Empty);
            }
        }

        // --- 3. Details (ซับซ้อนเพราะต้อง Include ชื่อ -> ใช้ DbContext) ---
        public async Task<AssetDetailsViewModel?> GetDetailsAsync(Guid id)
        {
            // เราไม่ใช้ _assetRepo.GetByIdAsync() เพราะมันไม่ Include ชื่อมาให้
            var asset = await _context.Assets
                .Include(a => a.User)
                .Include(a => a.Category)    // Include เพิ่ม
                .Include(a => a.Department)  // Include เพิ่ม
                .Include(a => a.Location)    // Include เพิ่ม
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asset == null) return null;

            return new AssetDetailsViewModel
            {
                Id = asset.Id,
                Code = asset.Code,
                Name = asset.Name,
                Description = asset.Description,

                // แสดงเป็นชื่อ
                Category = asset.Category.Name,
                Department = asset.Department.Name,
                Location = asset.Location.Name,

                Note = asset.Note,
                DateRegister = asset.DateRegister,
                IsActive = asset.IsActive,
                CreatedByUserName = asset.User?.UserName
            };
        }

        // --- 4. Get For Edit (ง่าย -> ใช้ Repo ได้) ---
        public async Task<AssetEditViewModel?> GetForEditAsync(Guid id)
        {
            // ใช้ Repo ดึงข้อมูลดิบ (เราต้องการ ID ไป bind ลง Dropdown ไม่ใช่ชื่อ)
            var asset = await _assetRepo.GetByIdAsync(id);

            if (asset == null) return null;

            return new AssetEditViewModel
            {
                Id = asset.Id,
                Code = asset.Code,
                Name = asset.Name,
                Description = asset.Description,

                // ส่ง ID กลับไปเพื่อให้ Dropdown เลือกค่าเดิมถูก
                CategoryId = asset.CategoryId,
                DepartmentId = asset.DepartmentId,
                LocationId = asset.LocationId,

                Note = asset.Note,
                DateRegister = asset.DateRegister,
                IsActive = asset.IsActive
            };
        }

        // --- 5. Update (ง่าย -> ใช้ Repo) ---
        public async Task<IdentityResult> UpdateAsync(AssetEditViewModel model)
        {
            // ดึงข้อมูลตัวเต็มมาเพื่อแก้ไข
            var assetToUpdate = await _assetRepo.GetByIdAsync(model.Id);

            if (assetToUpdate == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Asset not found." });
            }

            // Update ค่า
            assetToUpdate.Code = model.Code;
            assetToUpdate.Name = model.Name;
            assetToUpdate.Description = model.Description;

            // เปลี่ยน ID (ย้ายหมวด/ย้ายแผนก)
            assetToUpdate.CategoryId = model.CategoryId;
            assetToUpdate.DepartmentId = model.DepartmentId;
            assetToUpdate.LocationId = model.LocationId;

            assetToUpdate.Note = model.Note;
            assetToUpdate.DateRegister = model.DateRegister;
            assetToUpdate.IsActive = model.IsActive;

            try
            {
                // ใช้ Repo ปักธง Update
                _assetRepo.Update(assetToUpdate);
                await _context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        // --- 6. Delete (ง่าย -> ใช้ Repo) ---
        public async Task<IdentityResult> DeleteAsync(Guid id)
        {
            var assetToDelete = await _assetRepo.GetByIdAsync(id);

            if (assetToDelete == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Asset not found." });
            }

            // ใช้ Repo ปักธง Delete
            _assetRepo.Delete(assetToDelete);
            await _context.SaveChangesAsync();

            return IdentityResult.Success;
        }
    }
}