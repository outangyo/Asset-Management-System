using AssetManagementSystem.Web.ViewModels.Common;

namespace AssetManagementSystem.Web.ViewModels.Departments
{
    public class DepartmentListFilterViewModel : BaseFilterViewModel
    {
        public string? Search { get; set; }
        public bool? IsActive { get; set; }
    }
}
