using AssetManagementSystem.Db.Data;
using AssetManagementSystem.Web.Models;
using AssetManagementSystem.Web.Models.Assets;
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

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                query = query.Where(a => a.Name.Contains(filter.Search)
                                      || a.Category.Contains(filter.Search)
                                      || a.Code.Contains(filter.Search)); // <-- เพิ่มการค้นหาด้วย Code
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
    }
}
