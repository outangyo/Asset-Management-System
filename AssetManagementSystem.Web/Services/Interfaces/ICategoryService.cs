using AssetManagementSystem.Web.ViewModels.Categories;
using Microsoft.AspNetCore.Identity;

namespace AssetManagementSystem.Web.Services.Interfaces
{
    public interface ICategoryService
    {
        // หน้า List (ใช้ DbContext)
        Task<CategoryIndexViewModel> GetCategoriesAsync(CategoryListFilterViewModel filter);

        // หน้า Create/Edit/Delete (ใช้ Generic Repo)
        Task<CategoryCreateViewModel?> GetForEditAsync(Guid id);
        Task<IdentityResult> CreateAsync(CategoryCreateViewModel model);
        Task<IdentityResult> UpdateAsync(CategoryCreateViewModel model); // ใช้ ViewModel เดียวกันหรือแยกก็ได้
        Task<IdentityResult> DeleteAsync(Guid id);
    }
}
