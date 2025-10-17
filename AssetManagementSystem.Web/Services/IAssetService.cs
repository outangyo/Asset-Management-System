using AssetManagementSystem.Web.Models;
using AssetManagementSystem.Web.Models.Assets;
using Microsoft.AspNetCore.Identity;

namespace AssetManagementSystem.Web.Services
{
    public interface IAssetService
    {
        Task<PagedResult<AssetListItemViewModel>> GetAssetsAsync(AssetListFilterViewModel filter);
        Task<(IdentityResult result, Guid id)> CreateAsync(AssetCreateViewModel model, Guid userId);
        Task<AssetDetailsViewModel?> GetDetailsAsync(Guid id);
        Task<AssetEditViewModel?> GetForEditAsync(Guid id);
        Task<IdentityResult> UpdateAsync(AssetEditViewModel model);
        Task<IdentityResult> DeleteAsync(Guid id);
    }
}
