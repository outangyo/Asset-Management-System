using AssetManagementSystem.Web.ViewModels.Categories;
using AssetManagementSystem.Web.ViewModels.Departments;
using Microsoft.AspNetCore.Identity;

namespace AssetManagementSystem.Web.Services.Interfaces
{
    public interface IDepartmentService
    {
        // หน้า List (ใช้ DbContext)
        Task<DepartmentIndexViewModel> GetDepartmentsAsync(DepartmentListFilterViewModel filter);

        // หน้า Create/Edit/Delete (ใช้ Generic Repo)
        Task<DepartmentCreateViewModel?> GetForEditAsync(Guid id);
        Task<IdentityResult> CreateAsync(DepartmentCreateViewModel model);
        Task<IdentityResult> UpdateAsync(DepartmentCreateViewModel model); // ใช้ ViewModel เดียวกันหรือแยกก็ได้
        Task<IdentityResult> DeleteAsync(Guid id);
    }
}
