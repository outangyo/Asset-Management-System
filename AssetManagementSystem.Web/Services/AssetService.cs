using AssetManagementSystem.Db.Data;
using AssetManagementSystem.Db.Entities;
using AssetManagementSystem.Web.Models;
using AssetManagementSystem.Web.Models.Assets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Web.Services
{
    public class AssetService : IAssetService
    {

        private readonly ApplicationDbContext _context;

        public AssetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<AssetListItemViewModel>> GetAssetsAsync(AssetListFilterViewModel filter)
        {
            var query = _context.Assets.AsNoTracking();

            // Filter for search
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var searchTerm = filter.Search.Trim();
                query = query.Where(a => a.Name.Contains(searchTerm)
                                      || a.Category.Contains(searchTerm)
                                      || a.Code.Contains(searchTerm)
                                      || a.Department.Contains(searchTerm)
                                      || a.Location.Contains(searchTerm));
            }

            // Filter by Status (IsActive) ---
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
                    Category = a.Category,
                    Department = a.Department,
                    Location = a.Location,
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

        public async Task<(IdentityResult result, Guid id)> CreateAsync(AssetCreateViewModel model, Guid userId)
        {
            // สร้าง Entity ใหม่จาก ViewModel
            var asset = new Asset
            {
                Id = Guid.NewGuid(), // สร้าง Guid ใหม่
                Code = model.Code,
                Name = model.Name,
                Description = model.Description,
                Category = model.Category,
                Department = model.Department,
                Location = model.Location,
                Note = model.Note,
                DateRegister = model.DateRegister,
                IsActive = model.IsActive,
                UserId = userId // บันทึกว่าใครเป็นคนสร้าง
            };

            // เพิ่มลงใน DbContext
            _context.Assets.Add(asset);

            // บันทึกการเปลี่ยนแปลงลงฐานข้อมูล
            var saved = await _context.SaveChangesAsync();

            // ตรวจสอบว่าบันทึกสำเร็จหรือไม่
            if (saved > 0)
            {
                // ถ้าสำเร็จ คืนค่า Success และ Id ของ Asset ใหม่
                return (IdentityResult.Success, asset.Id);
            }
            else
            {
                // ถ้าล้มเหลว คืนค่า Failed
                return (IdentityResult.Failed(new IdentityError { Description = "Could not save asset to the database." }), Guid.Empty);
            }
        }

        public async Task<AssetDetailsViewModel?> GetDetailsAsync(Guid id)
        {
            var asset = await _context.Assets
                .Include(a => a.User) // <-- ใช้ .Include() เพื่อดึงข้อมูล User ที่เกี่ยวข้องมาพร้อมกัน
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asset == null)
            {
                return null; // ถ้าหา Asset ไม่เจอ
            }

            // แปลง Entity เป็น ViewModel
            return new AssetDetailsViewModel
            {
                Id = asset.Id,
                Code = asset.Code,
                Name = asset.Name,
                Description = asset.Description,
                Category = asset.Category,
                Department = asset.Department,
                Location = asset.Location,
                Note = asset.Note,
                DateRegister = asset.DateRegister,
                IsActive = asset.IsActive,
                // ดึงชื่อ User มาจาก Navigation Property
                CreatedByUserName = asset.User?.UserName
            };
        }
    }
}
