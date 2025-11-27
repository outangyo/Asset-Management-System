using AssetManagementSystem.Web.ViewModels.Common;

namespace AssetManagementSystem.Web.ViewModels.Categories
{
    public class CategoryListFilterViewModel : BaseFilterViewModel
    {
        public string? Search { get; set; }
        public bool? IsActive { get; set; }
    }
}
