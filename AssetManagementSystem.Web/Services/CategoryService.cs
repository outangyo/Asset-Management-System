using AssetManagementSystem.Core.Repositories;
using AssetManagementSystem.Db.Data;
using AssetManagementSystem.Db.Entities;
using AssetManagementSystem.Web.Services.Interfaces;
using AssetManagementSystem.Web.ViewModels.Categories;
using AssetManagementSystem.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Category> _categoryRepo;

        public CategoryService(ApplicationDbContext context, IRepository<Category> categoryRepo)
        {
            _context = context;
            _categoryRepo = categoryRepo;
        }

        // --- 1. Get List (ใช้ DbContext ตรงๆ เพื่อ Performance) ---
        public async Task<CategoryIndexViewModel> GetCategoriesAsync(CategoryListFilterViewModel filter)
        {
            var query = _context.Categories.AsNoTracking();

            // Filter Search
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var searchTerm = filter.Search.Trim().ToLower();
                query = query.Where(c => c.Name.ToLower().Contains(searchTerm)
                                      || (c.Description != null && c.Description.ToLower().Contains(searchTerm)));
            }

            // Filter Status
            if (filter.IsActive.HasValue)
            {
                query = query.Where(c => c.IsActive == filter.IsActive.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(c => c.Name)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(c => new CategoryListItemViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    IsActive = c.IsActive
                })
                .ToListAsync();

            return new CategoryIndexViewModel
            {
                Filter = filter,
                PagedCategories = new PagedResult<CategoryListItemViewModel>
                {
                    Items = items,
                    TotalCount = totalCount,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize
                }
            };
        }

        // --- 2. Get For Edit (ใช้ Generic Repo) ---
        public async Task<CategoryCreateViewModel?> GetForEditAsync(Guid id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null) return null;

            return new CategoryCreateViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            };
        }

        // --- 3. Create (ใช้ Generic Repo) ---
        public async Task<IdentityResult> CreateAsync(CategoryCreateViewModel model)
        {
            // (Optional) เช็คชื่อซ้ำ
            // if (await _context.Categories.AnyAsync(c => c.Name == model.Name)) return IdentityResult.Failed(...)

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                IsActive = model.IsActive
            };

            try
            {
                await _categoryRepo.CreateAsync(category);
                await _context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        // --- 4. Update (ใช้ Generic Repo) ---
        public async Task<IdentityResult> UpdateAsync(CategoryCreateViewModel model)
        {
            var category = await _categoryRepo.GetByIdAsync(model.Id);
            if (category == null) return IdentityResult.Failed(new IdentityError { Description = "Category not found" });

            category.Name = model.Name;
            category.Description = model.Description;
            category.IsActive = model.IsActive;

            try
            {
                _categoryRepo.Update(category);
                await _context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        // --- 5. Delete (ใช้ Generic Repo) ---
        public async Task<IdentityResult> DeleteAsync(Guid id)
        {
            // (Optional) เช็คก่อนลบว่ามี Asset ใช้อยู่ไหม
            // if (await _context.Assets.AnyAsync(a => a.CategoryId == id)) ...

            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null) return IdentityResult.Failed(new IdentityError { Description = "Category not found" });

            try
            {
                _categoryRepo.Delete(category);
                await _context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Cannot delete category because it is being used." });
            }
        }

    }
}
