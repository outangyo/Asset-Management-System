using AssetManagementSystem.Web.ViewModels.Shared;

namespace AssetManagementSystem.Web.ViewModels.Suppliers
{
    public class SupplierIndexViewModel
    {
        public SupplierListFilterViewModel Filter { get; set; } = new SupplierListFilterViewModel();

        public PagedResult<SupplierListItemViewModel> PagedSuppliers { get; set; } = new PagedResult<SupplierListItemViewModel>();
    }
}
