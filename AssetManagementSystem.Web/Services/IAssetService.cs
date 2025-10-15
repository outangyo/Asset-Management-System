using AssetManagementSystem.Web.Models;
using AssetManagementSystem.Web.Models.Assets;

namespace AssetManagementSystem.Web.Services
{
    public interface IAssetService
    {
        Task<PagedResult<AssetListItemViewModel>> GetAssetsAsync(AssetListFilterViewModel filter);
    }
}
