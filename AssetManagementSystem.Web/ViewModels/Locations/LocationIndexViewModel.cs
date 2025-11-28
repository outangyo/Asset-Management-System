using AssetManagementSystem.Web.ViewModels.Shared;

namespace AssetManagementSystem.Web.ViewModels.Locations
{
    public class LocationIndexViewModel
    {
        public LocationListFilterViewModel Filter { get; set; } = new LocationListFilterViewModel();
        public PagedResult<LocationListItemViewModel> PagedLocations { get; set; } = new PagedResult<LocationListItemViewModel>();
    }
}
