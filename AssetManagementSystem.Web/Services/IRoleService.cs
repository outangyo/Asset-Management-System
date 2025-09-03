using AssetManagementSystem.Web.Models.Roles;
using AssetManagementSystem.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AssetManagementSystem.Web.Services
{
    public interface IRoleService
    {
        Task<PagedResult<RoleListItemViewModel>> GetRolesAsync(RoleListFilterViewModel filter);
        Task<(IdentityResult Result, Guid? RoleId)> CreateAsync(RoleCreateViewModel model);
        Task<RoleEditViewModel?> GetForEditAsync(Guid id);
        Task<IdentityResult> UpdateAsync(RoleEditViewModel model);
        Task<IdentityResult> DeleteAsync(Guid id);
        Task<RoleDetailsViewModel?> GetDetailsAsync(Guid id, int pageNumber, int pageSize);
    }
}
