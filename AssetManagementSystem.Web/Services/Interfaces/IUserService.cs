using AssetManagementSystem.Web.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using AssetManagementSystem.Web.ViewModels.Shared;

namespace AssetManagementSystem.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<PagedResult<UserListItemViewModel>> GetUsersAsync(UserListFilterViewModel filter);
        Task<(IdentityResult Result, Guid? UserId)> CreateAsync(UserCreateViewModel model);
        Task<UserEditViewModel?> GetForEditAsync(Guid id);
        Task<IdentityResult> UpdateAsync(UserEditViewModel model);
        Task<UserDetailsViewModel?> GetDetailsAsync(Guid id);
        Task<IdentityResult> DeleteAsync(Guid id);
        Task<UserRolesEditViewModel?> GetRolesForEditAsync(Guid userId);
        Task<IdentityResult> UpdateRolesAsync(Guid userId, IEnumerable<Guid> selectedRoleIds);
    }
}
