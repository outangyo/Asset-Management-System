using AssetManagementSystem.Web.ViewModels.Shared;

namespace AssetManagementSystem.Web.ViewModels.Categories
{
    public class CategoryIndexViewModel
    {
        public CategoryListFilterViewModel Filter { get; set; } = new CategoryListFilterViewModel();
        public PagedResult<CategoryListItemViewModel> PagedCategories { get; set; } = new PagedResult<CategoryListItemViewModel>();
    }
}
