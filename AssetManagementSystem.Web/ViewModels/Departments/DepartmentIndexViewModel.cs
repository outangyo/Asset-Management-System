using AssetManagementSystem.Web.ViewModels.Categories;
using AssetManagementSystem.Web.ViewModels.Shared;

namespace AssetManagementSystem.Web.ViewModels.Departments
{
    public class DepartmentIndexViewModel
    {
        public DepartmentListFilterViewModel Filter { get; set; } = new DepartmentListFilterViewModel();
        public PagedResult<DepartmentListItemViewModel> PagedDepartments { get; set; } = new PagedResult<DepartmentListItemViewModel>();
    }
}
