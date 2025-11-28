using AssetManagementSystem.Web.ViewModels.Common;

namespace AssetManagementSystem.Web.ViewModels.Locations
{
    public class LocationListFilterViewModel : BaseFilterViewModel
    {
        public string? Search { get; set; }
        public bool? IsActive { get; set; }
    }
}
