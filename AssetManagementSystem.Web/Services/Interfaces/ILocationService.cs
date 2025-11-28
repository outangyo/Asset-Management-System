using AssetManagementSystem.Web.ViewModels.Locations;
using Microsoft.AspNetCore.Identity;

namespace AssetManagementSystem.Web.Services.Interfaces
{
    public interface ILocationService
    {
        Task<LocationIndexViewModel> GetLocationsAsync(LocationListFilterViewModel filter);
        Task<LocationCreateViewModel?> GetForEditAsync(Guid id);
        Task<IdentityResult> CreateAsync(LocationCreateViewModel model);
        Task<IdentityResult> UpdateAsync(LocationCreateViewModel model);
        Task<IdentityResult> DeleteAsync(Guid id);
    }
}
